
namespace Quitter_4_Enhanced
{
    partial class Form1
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
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_KillKey = new System.Windows.Forms.TextBox();
            this.comboBox_Networks = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_NetworkTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_NetworkKey = new System.Windows.Forms.TextBox();
            this.textBox_SoloKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_SoloTime = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Suspend = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_Terminate = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_DropConn = new System.Windows.Forms.Label();
            this.timer_autosaver = new System.Windows.Forms.Timer(this.components);
            this.timer_suspend = new System.Windows.Forms.Timer(this.components);
            this.timer_network = new System.Windows.Forms.Timer(this.components);
            this.richTextBox_EventLog = new System.Windows.Forms.RichTextBox();
            this.timer_logger = new System.Windows.Forms.Timer(this.components);
            this.checkBox_selfTerminate = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Terminate game process";
            // 
            // textBox_KillKey
            // 
            this.textBox_KillKey.AcceptsTab = true;
            this.textBox_KillKey.BackColor = System.Drawing.Color.Black;
            this.textBox_KillKey.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox_KillKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.textBox_KillKey.Location = new System.Drawing.Point(21, 29);
            this.textBox_KillKey.Name = "textBox_KillKey";
            this.textBox_KillKey.Size = new System.Drawing.Size(556, 21);
            this.textBox_KillKey.TabIndex = 3;
            this.textBox_KillKey.TabStop = false;
            this.textBox_KillKey.Text = "FillerText (If you see this, something went wrong)";
            this.textBox_KillKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KillGame_KeyDown);
            // 
            // comboBox_Networks
            // 
            this.comboBox_Networks.BackColor = System.Drawing.Color.Black;
            this.comboBox_Networks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Networks.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_Networks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.comboBox_Networks.Items.AddRange(new object[] {
            "FillerText (If you see this, something went wrong)"});
            this.comboBox_Networks.Location = new System.Drawing.Point(21, 80);
            this.comboBox_Networks.MaxDropDownItems = 12;
            this.comboBox_Networks.Name = "comboBox_Networks";
            this.comboBox_Networks.Size = new System.Drawing.Size(557, 23);
            this.comboBox_Networks.TabIndex = 6;
            this.comboBox_Networks.TabStop = false;
            this.comboBox_Networks.SelectedIndexChanged += new System.EventHandler(this.comboBox_Networks_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Network adapter used for internet access";
            // 
            // textBox_NetworkTime
            // 
            this.textBox_NetworkTime.BackColor = System.Drawing.Color.Black;
            this.textBox_NetworkTime.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox_NetworkTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.textBox_NetworkTime.Location = new System.Drawing.Point(20, 129);
            this.textBox_NetworkTime.Name = "textBox_NetworkTime";
            this.textBox_NetworkTime.Size = new System.Drawing.Size(557, 21);
            this.textBox_NetworkTime.TabIndex = 7;
            this.textBox_NetworkTime.TabStop = false;
            this.textBox_NetworkTime.Text = "FillerText (If you see this, something went wrong)";
            this.textBox_NetworkTime.TextChanged += new System.EventHandler(this.textBox_NetworkTime_TextChanged);
            this.textBox_NetworkTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_NetworkTime_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Seconds to wait prior to restoring the connection (N)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Drop network connection for N sec";
            // 
            // textBox_NetworkKey
            // 
            this.textBox_NetworkKey.AcceptsTab = true;
            this.textBox_NetworkKey.BackColor = System.Drawing.Color.Black;
            this.textBox_NetworkKey.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox_NetworkKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.textBox_NetworkKey.Location = new System.Drawing.Point(20, 32);
            this.textBox_NetworkKey.Name = "textBox_NetworkKey";
            this.textBox_NetworkKey.Size = new System.Drawing.Size(557, 21);
            this.textBox_NetworkKey.TabIndex = 5;
            this.textBox_NetworkKey.TabStop = false;
            this.textBox_NetworkKey.Text = "FillerText (If you see this, something went wrong)";
            this.textBox_NetworkKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_DropNetwork_KeyDown);
            // 
            // textBox_SoloKey
            // 
            this.textBox_SoloKey.BackColor = System.Drawing.Color.Black;
            this.textBox_SoloKey.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox_SoloKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.textBox_SoloKey.Location = new System.Drawing.Point(20, 28);
            this.textBox_SoloKey.Name = "textBox_SoloKey";
            this.textBox_SoloKey.Size = new System.Drawing.Size(557, 21);
            this.textBox_SoloKey.TabIndex = 1;
            this.textBox_SoloKey.TabStop = false;
            this.textBox_SoloKey.Text = "FillerText (If you see this, something went wrong)";
            this.textBox_SoloKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_SoloKey_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Solo public session hotkey";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Suspend process interval (milliseconds)";
            // 
            // textBox_SoloTime
            // 
            this.textBox_SoloTime.BackColor = System.Drawing.Color.Black;
            this.textBox_SoloTime.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox_SoloTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.textBox_SoloTime.Location = new System.Drawing.Point(20, 75);
            this.textBox_SoloTime.Name = "textBox_SoloTime";
            this.textBox_SoloTime.Size = new System.Drawing.Size(557, 21);
            this.textBox_SoloTime.TabIndex = 2;
            this.textBox_SoloTime.TabStop = false;
            this.textBox_SoloTime.Text = "FillerText (If you see this, something went wrong)";
            this.textBox_SoloTime.TextChanged += new System.EventHandler(this.textBox_SoloTime_TextChanged);
            this.textBox_SoloTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_SoloTime_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox_SoloTime);
            this.panel1.Controls.Add(this.textBox_SoloKey);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.panel1.Location = new System.Drawing.Point(14, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 108);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.RemoveActiveControl);
            // 
            // label_Suspend
            // 
            this.label_Suspend.AutoSize = true;
            this.label_Suspend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.label_Suspend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.label_Suspend.Location = new System.Drawing.Point(19, 8);
            this.label_Suspend.Name = "label_Suspend";
            this.label_Suspend.Size = new System.Drawing.Size(56, 15);
            this.label_Suspend.TabIndex = 0;
            this.label_Suspend.Text = "Suspend";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.checkBox_selfTerminate);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.textBox_KillKey);
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.panel3.Location = new System.Drawing.Point(14, 136);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(595, 86);
            this.panel3.TabIndex = 2;
            this.panel3.Click += new System.EventHandler(this.RemoveActiveControl);
            // 
            // label_Terminate
            // 
            this.label_Terminate.AutoSize = true;
            this.label_Terminate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.label_Terminate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.label_Terminate.Location = new System.Drawing.Point(19, 128);
            this.label_Terminate.Name = "label_Terminate";
            this.label_Terminate.Size = new System.Drawing.Size(63, 15);
            this.label_Terminate.TabIndex = 0;
            this.label_Terminate.Text = "Terminate";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.comboBox_Networks);
            this.panel4.Controls.Add(this.textBox_NetworkKey);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.textBox_NetworkTime);
            this.panel4.Controls.Add(this.label3);
            this.panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.panel4.Location = new System.Drawing.Point(14, 233);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(595, 160);
            this.panel4.TabIndex = 3;
            this.panel4.Click += new System.EventHandler(this.RemoveActiveControl);
            // 
            // label_DropConn
            // 
            this.label_DropConn.AutoSize = true;
            this.label_DropConn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.label_DropConn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.label_DropConn.Location = new System.Drawing.Point(19, 226);
            this.label_DropConn.Name = "label_DropConn";
            this.label_DropConn.Size = new System.Drawing.Size(224, 15);
            this.label_DropConn.TabIndex = 0;
            this.label_DropConn.Text = "Drop connection (requires Admin rights)";
            // 
            // timer_autosaver
            // 
            this.timer_autosaver.Interval = 1000;
            this.timer_autosaver.Tick += new System.EventHandler(this.timer_autosaver_Tick);
            // 
            // timer_suspend
            // 
            this.timer_suspend.Tick += new System.EventHandler(this.timer_suspend_Tick);
            // 
            // timer_network
            // 
            this.timer_network.Interval = 1000;
            this.timer_network.Tick += new System.EventHandler(this.timer_network_Tick);
            // 
            // richTextBox_EventLog
            // 
            this.richTextBox_EventLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.richTextBox_EventLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.richTextBox_EventLog.DetectUrls = false;
            this.richTextBox_EventLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.richTextBox_EventLog.Location = new System.Drawing.Point(14, 399);
            this.richTextBox_EventLog.Name = "richTextBox_EventLog";
            this.richTextBox_EventLog.ReadOnly = true;
            this.richTextBox_EventLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox_EventLog.Size = new System.Drawing.Size(595, 122);
            this.richTextBox_EventLog.TabIndex = 8;
            this.richTextBox_EventLog.TabStop = false;
            this.richTextBox_EventLog.Text = "FillerText (If you see this, something went wrong)";
            // 
            // timer_logger
            // 
            this.timer_logger.Interval = 1000;
            this.timer_logger.Tick += new System.EventHandler(this.timer_logger_Tick);
            // 
            // checkBox_selfTerminate
            // 
            this.checkBox_selfTerminate.AutoSize = true;
            this.checkBox_selfTerminate.Location = new System.Drawing.Point(21, 56);
            this.checkBox_selfTerminate.Name = "checkBox_selfTerminate";
            this.checkBox_selfTerminate.Size = new System.Drawing.Size(262, 19);
            this.checkBox_selfTerminate.TabIndex = 4;
            this.checkBox_selfTerminate.Text = "Exit quitter when terminating game process";
            this.checkBox_selfTerminate.UseVisualStyleBackColor = true;
            this.checkBox_selfTerminate.CheckedChanged += new System.EventHandler(this.checkBox_selfTerminate_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ClientSize = new System.Drawing.Size(622, 535);
            this.Controls.Add(this.richTextBox_EventLog);
            this.Controls.Add(this.label_DropConn);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label_Terminate);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label_Suspend);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Quitter For GTAV Enhanced v0.3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.RemoveActiveControl);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBox_KillKey;
        public System.Windows.Forms.TextBox textBox_NetworkKey;
        public System.Windows.Forms.TextBox textBox_SoloKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Suspend;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label_Terminate;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.ComboBox comboBox_Networks;
        public System.Windows.Forms.TextBox textBox_NetworkTime;
        public System.Windows.Forms.TextBox textBox_SoloTime;
        public System.Windows.Forms.Timer timer_autosaver;
        public System.Windows.Forms.Timer timer_suspend;
        public System.Windows.Forms.Timer timer_network;
        public System.Windows.Forms.Label label_DropConn;
        public System.Windows.Forms.RichTextBox richTextBox_EventLog;
        private System.Windows.Forms.Timer timer_logger;
        public System.Windows.Forms.CheckBox checkBox_selfTerminate;
    }
}

