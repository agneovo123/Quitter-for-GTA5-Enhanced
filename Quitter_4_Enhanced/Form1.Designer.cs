
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
            this.button1 = new System.Windows.Forms.Button();
            this.button_suspend = new System.Windows.Forms.Button();
            this.button_continue = new System.Windows.Forms.Button();
            this.button_kill = new System.Windows.Forms.Button();
            this.button_get_networks = new System.Windows.Forms.Button();
            this.button_ON = new System.Windows.Forms.Button();
            this.button_OFF = new System.Windows.Forms.Button();
            this.button_UnegisterHotkey = new System.Windows.Forms.Button();
            this.button_RegisterHotkey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "get processes test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_suspend
            // 
            this.button_suspend.Location = new System.Drawing.Point(17, 71);
            this.button_suspend.Name = "button_suspend";
            this.button_suspend.Size = new System.Drawing.Size(143, 23);
            this.button_suspend.TabIndex = 1;
            this.button_suspend.Text = "button_suspend";
            this.button_suspend.UseVisualStyleBackColor = true;
            this.button_suspend.Click += new System.EventHandler(this.button_suspend_Click);
            // 
            // button_continue
            // 
            this.button_continue.Location = new System.Drawing.Point(17, 100);
            this.button_continue.Name = "button_continue";
            this.button_continue.Size = new System.Drawing.Size(143, 23);
            this.button_continue.TabIndex = 2;
            this.button_continue.Text = "button_continue";
            this.button_continue.UseVisualStyleBackColor = true;
            this.button_continue.Click += new System.EventHandler(this.button_continue_Click);
            // 
            // button_kill
            // 
            this.button_kill.Location = new System.Drawing.Point(17, 129);
            this.button_kill.Name = "button_kill";
            this.button_kill.Size = new System.Drawing.Size(143, 23);
            this.button_kill.TabIndex = 3;
            this.button_kill.Text = "button_kill";
            this.button_kill.UseVisualStyleBackColor = true;
            this.button_kill.Click += new System.EventHandler(this.button_kill_Click);
            // 
            // button_get_networks
            // 
            this.button_get_networks.Location = new System.Drawing.Point(196, 22);
            this.button_get_networks.Name = "button_get_networks";
            this.button_get_networks.Size = new System.Drawing.Size(143, 23);
            this.button_get_networks.TabIndex = 4;
            this.button_get_networks.Text = "button_get_networks";
            this.button_get_networks.UseVisualStyleBackColor = true;
            this.button_get_networks.Click += new System.EventHandler(this.button_get_networks_Click);
            // 
            // button_ON
            // 
            this.button_ON.Location = new System.Drawing.Point(196, 100);
            this.button_ON.Name = "button_ON";
            this.button_ON.Size = new System.Drawing.Size(143, 23);
            this.button_ON.TabIndex = 6;
            this.button_ON.Text = "button_ON";
            this.button_ON.UseVisualStyleBackColor = true;
            this.button_ON.Click += new System.EventHandler(this.button_ON_Click);
            // 
            // button_OFF
            // 
            this.button_OFF.Location = new System.Drawing.Point(196, 71);
            this.button_OFF.Name = "button_OFF";
            this.button_OFF.Size = new System.Drawing.Size(143, 23);
            this.button_OFF.TabIndex = 5;
            this.button_OFF.Text = "button_OFF";
            this.button_OFF.UseVisualStyleBackColor = true;
            this.button_OFF.Click += new System.EventHandler(this.button_OFF_Click);
            // 
            // button_UnegisterHotkey
            // 
            this.button_UnegisterHotkey.Location = new System.Drawing.Point(373, 100);
            this.button_UnegisterHotkey.Name = "button_UnegisterHotkey";
            this.button_UnegisterHotkey.Size = new System.Drawing.Size(143, 23);
            this.button_UnegisterHotkey.TabIndex = 8;
            this.button_UnegisterHotkey.Text = "button_UnegisterHotkey";
            this.button_UnegisterHotkey.UseVisualStyleBackColor = true;
            this.button_UnegisterHotkey.Click += new System.EventHandler(this.button_UnegisterHotkey_Click);
            // 
            // button_RegisterHotkey
            // 
            this.button_RegisterHotkey.Location = new System.Drawing.Point(373, 71);
            this.button_RegisterHotkey.Name = "button_RegisterHotkey";
            this.button_RegisterHotkey.Size = new System.Drawing.Size(143, 23);
            this.button_RegisterHotkey.TabIndex = 7;
            this.button_RegisterHotkey.Text = "button_RegisterHotkey";
            this.button_RegisterHotkey.UseVisualStyleBackColor = true;
            this.button_RegisterHotkey.Click += new System.EventHandler(this.button_RegisterHotkey_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 410);
            this.Controls.Add(this.button_UnegisterHotkey);
            this.Controls.Add(this.button_RegisterHotkey);
            this.Controls.Add(this.button_ON);
            this.Controls.Add(this.button_OFF);
            this.Controls.Add(this.button_get_networks);
            this.Controls.Add(this.button_kill);
            this.Controls.Add(this.button_continue);
            this.Controls.Add(this.button_suspend);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_suspend;
        private System.Windows.Forms.Button button_continue;
        private System.Windows.Forms.Button button_kill;
        private System.Windows.Forms.Button button_get_networks;
        private System.Windows.Forms.Button button_ON;
        private System.Windows.Forms.Button button_OFF;
        private System.Windows.Forms.Button button_UnegisterHotkey;
        private System.Windows.Forms.Button button_RegisterHotkey;
    }
}

