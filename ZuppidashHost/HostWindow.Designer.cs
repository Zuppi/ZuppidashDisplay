namespace ZuppidashHost
{
    partial class HostWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostWindow));
            this.btnExit = new System.Windows.Forms.Button();
            this.stateLabel = new System.Windows.Forms.Label();
            this.btnConfig = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.connectButton = new System.Windows.Forms.Button();
            this.autoConnectCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(193, 89);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(12, 22);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(73, 13);
            this.stateLabel.TabIndex = 1;
            this.stateLabel.Text = "Disconnected";
            this.stateLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(112, 89);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(75, 23);
            this.btnConfig.TabIndex = 2;
            this.btnConfig.Text = "Configure";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Zuppidash";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(193, 60);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // autoConnectCheckbox
            // 
            this.autoConnectCheckbox.AutoSize = true;
            this.autoConnectCheckbox.Location = new System.Drawing.Point(71, 64);
            this.autoConnectCheckbox.Name = "autoConnectCheckbox";
            this.autoConnectCheckbox.Size = new System.Drawing.Size(116, 17);
            this.autoConnectCheckbox.TabIndex = 4;
            this.autoConnectCheckbox.Text = "Connect on startup";
            this.autoConnectCheckbox.UseVisualStyleBackColor = true;
            this.autoConnectCheckbox.CheckedChanged += new System.EventHandler(this.autoConnectCheckbox_CheckedChanged);
            // 
            // HostWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 124);
            this.Controls.Add(this.autoConnectCheckbox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.btnExit);
            this.Name = "HostWindow";
            this.Text = "Zuppidash Host";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HostWindow_FormClosing);
            this.Resize += new System.EventHandler(this.hostWindow_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.CheckBox autoConnectCheckbox;
    }
}

