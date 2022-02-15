namespace DiceGame.Forms
{
    partial class ServerList
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
            this.Sessions = new System.Windows.Forms.ListBox();
            this.closeMenu = new System.Windows.Forms.Button();
            this.Join = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Sessions
            // 
            this.Sessions.FormattingEnabled = true;
            this.Sessions.Location = new System.Drawing.Point(12, 12);
            this.Sessions.Name = "Sessions";
            this.Sessions.Size = new System.Drawing.Size(154, 238);
            this.Sessions.TabIndex = 0;
            // 
            // closeMenu
            // 
            this.closeMenu.Location = new System.Drawing.Point(172, 202);
            this.closeMenu.Name = "closeMenu";
            this.closeMenu.Size = new System.Drawing.Size(104, 48);
            this.closeMenu.TabIndex = 1;
            this.closeMenu.Text = "Close";
            this.closeMenu.UseVisualStyleBackColor = true;
            this.closeMenu.Click += new System.EventHandler(this.Close_Click);
            // 
            // Join
            // 
            this.Join.Location = new System.Drawing.Point(172, 148);
            this.Join.Name = "Join";
            this.Join.Size = new System.Drawing.Size(104, 48);
            this.Join.TabIndex = 2;
            this.Join.Text = "Join";
            this.Join.UseVisualStyleBackColor = true;
            this.Join.Click += new System.EventHandler(this.Join_Click);
            // 
            // ServerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 262);
            this.Controls.Add(this.Join);
            this.Controls.Add(this.closeMenu);
            this.Controls.Add(this.Sessions);
            this.Name = "ServerList";
            this.Text = "ServerList";
            this.Shown += new System.EventHandler(this.OnShow);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Sessions;
        private System.Windows.Forms.Button closeMenu;
        private System.Windows.Forms.Button Join;
    }
}