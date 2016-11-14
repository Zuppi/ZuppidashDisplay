using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace ZuppidashHost
{
    public partial class ConfigForm : Form
    {
        private class ComboBoxItem
        {
            private string name;

            public ComboBoxItem(string name)
            {
                this.name = name;
            }

            public override string ToString()
            {
                return this.name;
            }
        }

        Properties.Settings appSettings;
        public ConfigForm()
        {
            InitializeComponent();
            appSettings = Properties.Settings.Default;
            LoadSettings();
        }

        private void LoadSettings()
        {
            foreach (string port in SerialPort.GetPortNames())
            {
                ComboBoxItem item = new ComboBoxItem(port);
                comPortComboBox.Items.Add(item);
            }


            if (appSettings["ComPort"] != null)
            {
                comPortComboBox.SelectedIndex = comPortComboBox.FindStringExact(appSettings.ComPort);
            }

            if (appSettings["BaudRate"] == null)
            {
                appSettings.BaudRate = 9600;
                appSettings.Save();
            }

            if (appSettings["RefreshRate"] == null)
            {
                appSettings.RefreshRate = 10;
                appSettings.Save();
            }

            baudRateBox.Text = appSettings.BaudRate.ToString();
            refreshRateBox.Text = appSettings.RefreshRate.ToString();
        }

        private void SaveSettings()
        {
            try
            {
                int baudRate = Int32.Parse(baudRateBox.Text);
                int refreshRate = Int32.Parse(refreshRateBox.Text);
                appSettings.BaudRate = baudRate;
                appSettings.RefreshRate = refreshRate;
                appSettings.ComPort = comPortComboBox.SelectedItem.ToString();
                appSettings.Save();
            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveSettingsBtn_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
