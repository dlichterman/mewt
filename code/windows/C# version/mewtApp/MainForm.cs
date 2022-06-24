using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreAudio;
using System.IO.Ports;
using System.Timers;

namespace mewtApp
{
    public partial class MainForm : Form
    {
        private SerialPort port;
        bool found = false;
        bool globalMuteState = false;

        System.Timers.Timer tmrDisconnectCheck;
        System.Timers.Timer tmrVolume;

        NotifyIcon trayIcon;

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            trayIcon = new NotifyIcon()
            {
                Icon = this.Icon,
                Visible = true
            };
            trayIcon.MouseClick += new MouseEventHandler(iconOnClick);
            this.ShowInTaskbar = false;
        }

        public void ChangeLabel(string msg, Label label)
        {
            if (label.InvokeRequired)
                label.Invoke(new MethodInvoker(delegate { label.Text = msg; }));
            else
                label.Text = msg;
        }

        private void iconOnClick(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = true;
                this.Show();
                this.WindowState = FormWindowState.Normal;

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            Task.Delay(TimeSpan.FromMilliseconds(1000))
                .ContinueWith(task => findDevice());
        }

        private void SetMute(bool muteState)
        {
            globalMuteState = muteState;
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            var q = enumerator.EnumerateAudioEndPoints(EDataFlow.eCapture, DEVICE_STATE.DEVICE_STATE_ACTIVE);
            foreach (var ep in q)
            {
                foreach (var ep2 in ep.AudioSessionManager2.Sessions)
                {
                    ep2.SimpleAudioVolume.Mute = muteState;
                }
            }
        }

        private void setUpConnection(string comPort)
        {
            try
            {
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

                // Begin communications
                port.WriteLine("101");

                //Check current state to show correct color
                MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                var q = enumerator.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eCommunications);
                bool muteState = q.AudioSessionManager2.Sessions.FirstOrDefault().SimpleAudioVolume.Mute;
                //Invert the bool because 0 = muted
                port.WriteLine(muteState ? "0" : "1");

                //timer to check if we're still connected
                tmrDisconnectCheck = new System.Timers.Timer();
                tmrDisconnectCheck.Interval = 2500;
                tmrDisconnectCheck.Elapsed += tmrDisconnectCheck_Elapsed;
                tmrDisconnectCheck.Start();

                //Timer to send volume
                tmrVolume = new System.Timers.Timer();
                tmrVolume.Interval = 100;
                tmrVolume.Elapsed += tmrVolume_elapsed;
                tmrVolume.Start();
            }
            catch(Exception ex)
            {
                lostConnection();
            }
        }

        private void tmrVolume_elapsed(object? sender, ElapsedEventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            var q = enumerator.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eCommunications);
            int vol = (int)Math.Round(q.AudioMeterInformation.MasterPeakValue * 100);
            try
            {
                if (vol > 0)
                {
                    port.WriteLine(vol.ToString());
                    ChangeLabel(vol.ToString(), lblVolVal);
                }
                else
                {
                    port.WriteLine(getMuteFromBool(globalMuteState));
                    ChangeLabel("0", lblVolVal);
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        private void lostConnection()
        {
            if (tmrDisconnectCheck != null)
            {
                tmrDisconnectCheck.Stop();
                tmrDisconnectCheck.Dispose();
            }
            if (tmrVolume != null)
            {
                tmrVolume.Stop();
            }
            found = false;
            ChangeLabel("Lost connection", lblStatusVal);
            //Console.Write("Lost connection");
            findDevice();
        }

        private void tmrDisconnectCheck_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (!port.IsOpen)
            {
                lostConnection();
            }
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            string s = port.ReadExisting();
            if (s == "0\r\n")
            {
                SetMute(true);
                ChangeLabel("Mute", lblLastRecdVal);
            }
            else if (s == "1\r\n")
            {
                SetMute(false);
                ChangeLabel("Unmute", lblLastRecdVal);
            }
            Console.Write(s);
        }

        private void findDevice()
        {
            ChangeLabel("Searching for COM Port", lblStatusVal);
            while (!found)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    try
                    {
                        ChangeLabel("Trying port " + s, lblStatusVal);
                        port = new SerialPort(s, 9600, Parity.None, 8, StopBits.One);
                        port.DtrEnable = true;
                        port.RtsEnable = true;
                        port.ReadTimeout = 50;
                        port.Open();
                        port.DataReceived += new SerialDataReceivedEventHandler(port_searchDataRecd);
                        Thread.Sleep(5000);
                        if (!found)
                        {
                            port.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        //
                    }
                }
            }
        }

        private void port_searchDataRecd(object sender, SerialDataReceivedEventArgs e)
        {
            string dataRecd = port.ReadExisting();
            if (dataRecd.Contains("IdentifyMewtDevice"))
            {
                ChangeLabel("Connected port " + ((SerialPort)sender).PortName, lblStatusVal);
                //Console.Write("Connected port " + ((SerialPort)sender).PortName);
                found = true;
                port.Write("IdentifyMewtDevice");
                Thread.Sleep(1000);
                port.DataReceived -= new SerialDataReceivedEventHandler(port_searchDataRecd);
                setUpConnection(((SerialPort)sender).PortName);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string getMuteFromBool(bool mute)
        {
            if(mute)
            {
                return "0";
            }
            else
            {
                return "1";
            }

        }
    }
}
