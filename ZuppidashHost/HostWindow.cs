using System;
using System.Timers;
using System.Windows.Forms;


namespace ZuppidashHost
{
    public partial class HostWindow : Form
    {
        System.Timers.Timer clockTimer;

        DisplayConnection displayConnection;
        IRacingConnection iracingConnection;

        private bool allowVisible;     // ContextMenu's Show command used
        private bool allowClose;       // ContextMenu's Exit command used


        public HostWindow()
        {
            InitializeComponent();

            displayConnection = new DisplayConnection(this);
            iracingConnection = new IRacingConnection(this, displayConnection);

            if (Properties.Settings.Default.AutoConnect)
            {
                autoConnectCheckbox.Checked = Properties.Settings.Default.AutoConnect;
               
                displayConnection.Open(Properties.Settings.Default.ComPort, Properties.Settings.Default.BaudRate);             
                iracingConnection.Open(Properties.Settings.Default.RefreshRate);

                if (displayConnection.Connected())
                {
                    IracingStopped();
                    connectButton.Text = "Disconnect";
                }
            }        
        }

        ~HostWindow()
        {

        }

        protected override void SetVisibleCore(bool value)
        {
            if (!allowVisible)
            {
                value = false;
                if (!this.IsHandleCreated) CreateHandle();
            }
            base.SetVisibleCore(value);
        }



        public void IracingStopped()
        {
            displayConnection.ClearDisplay();
            displayConnection.SendMessage(GetClockString());

            clockTimer = new System.Timers.Timer(60000);
            clockTimer.Elapsed += TimerTick;
            clockTimer.AutoReset = true;
            clockTimer.Enabled = true;
            clockTimer.Start();       
        }

        public void IracingStarted()
        {
            if (clockTimer != null)
            {
                displayConnection.ClearDisplay();
                clockTimer.Stop();
                clockTimer.Dispose();
                clockTimer = null;
            }
        }

        public void SetText(string text)
        {
            this.stateLabel.Text = text;
        }    

        private void TimerTick(Object source, ElapsedEventArgs e)
        {
            displayConnection.SendMessage(GetClockString());
        }

        private string GetClockString()
        {
            return "  " + DateTime.Now.ToString("HHmm") + "|0|16";
        }

        public void RedirectCommand(Enums.CommandTargets target, Enums.Commands command)
        {

        }

        private void HostWindow_FormClosing(Object sender, FormClosingEventArgs e)
        {
            notifyIcon.Dispose();
            displayConnection.Close();
            iracingConnection.Close();

            if (clockTimer != null)
            {
                clockTimer.Stop();
                clockTimer.Dispose();
                clockTimer = null;
            }
           
            Environment.Exit(0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();
            form.Show();
        }

        private void hostWindow_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon.Visible = true;
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon.Visible = false;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if( e.Button == MouseButtons.Left)
            {
                allowVisible = true;
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }        
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (displayConnection.Connected())
            {
                displayConnection.Close();
                stateLabel.Text = "Disconnected";
                connectButton.Text = "Connect";
            }
            else
            {
                if (displayConnection.Open(Properties.Settings.Default.ComPort, Properties.Settings.Default.BaudRate))
                {
                    connectButton.Text = "Disconnect";
                    IracingStopped();
                }              
            }
        }

        private void autoConnectCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoConnect = autoConnectCheckbox.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
