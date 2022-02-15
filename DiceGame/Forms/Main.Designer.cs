namespace DiceGame.Forms
{
    partial class Main
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
            this.NameInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Settings = new System.Windows.Forms.Button();
            this.SessionOpen = new System.Windows.Forms.Button();
            this.Roll = new System.Windows.Forms.Button();
            this.Players = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Kick = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Result = new System.Windows.Forms.Label();
            this.End = new System.Windows.Forms.Button();
            this.History = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DiceList = new System.Windows.Forms.ComboBox();
            this.Join = new System.Windows.Forms.Button();
            this.ServerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NameInput
            // 
            this.NameInput.Location = new System.Drawing.Point(364, 26);
            this.NameInput.Name = "NameInput";
            this.NameInput.Size = new System.Drawing.Size(148, 20);
            this.NameInput.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(361, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // Settings
            // 
            this.Settings.Location = new System.Drawing.Point(364, 345);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(148, 26);
            this.Settings.TabIndex = 2;
            this.Settings.Text = "DiceList";
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // SessionOpen
            // 
            this.SessionOpen.Location = new System.Drawing.Point(364, 97);
            this.SessionOpen.Name = "SessionOpen";
            this.SessionOpen.Size = new System.Drawing.Size(75, 34);
            this.SessionOpen.TabIndex = 3;
            this.SessionOpen.Text = "Start Public Session";
            this.SessionOpen.UseVisualStyleBackColor = true;
            this.SessionOpen.Click += new System.EventHandler(this.SessionOpen_Click);
            // 
            // Roll
            // 
            this.Roll.Location = new System.Drawing.Point(364, 407);
            this.Roll.Name = "Roll";
            this.Roll.Size = new System.Drawing.Size(148, 52);
            this.Roll.TabIndex = 4;
            this.Roll.Text = "Roll Dice";
            this.Roll.UseVisualStyleBackColor = true;
            this.Roll.Click += new System.EventHandler(this.Roll_Click);
            // 
            // Players
            // 
            this.Players.FormattingEnabled = true;
            this.Players.Location = new System.Drawing.Point(364, 150);
            this.Players.Name = "Players";
            this.Players.Size = new System.Drawing.Size(148, 160);
            this.Players.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Players:";
            // 
            // Kick
            // 
            this.Kick.Location = new System.Drawing.Point(364, 316);
            this.Kick.Name = "Kick";
            this.Kick.Size = new System.Drawing.Size(75, 23);
            this.Kick.TabIndex = 7;
            this.Kick.Text = "Kick";
            this.Kick.UseVisualStyleBackColor = true;
            this.Kick.Click += new System.EventHandler(this.Kick_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 438);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Roll:";
            // 
            // Result
            // 
            this.Result.AutoSize = true;
            this.Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Result.Location = new System.Drawing.Point(52, 438);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(0, 24);
            this.Result.TabIndex = 9;
            // 
            // End
            // 
            this.End.Location = new System.Drawing.Point(445, 316);
            this.End.Name = "End";
            this.End.Size = new System.Drawing.Size(67, 23);
            this.End.TabIndex = 10;
            this.End.Text = "End";
            this.End.UseVisualStyleBackColor = true;
            this.End.Click += new System.EventHandler(this.End_Click);
            // 
            // History
            // 
            this.History.FormattingEnabled = true;
            this.History.Location = new System.Drawing.Point(9, 36);
            this.History.Name = "History";
            this.History.Size = new System.Drawing.Size(346, 381);
            this.History.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 12;
            this.label4.Text = "History";
            // 
            // DiceList
            // 
            this.DiceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DiceList.FormattingEnabled = true;
            this.DiceList.Location = new System.Drawing.Point(367, 381);
            this.DiceList.Name = "DiceList";
            this.DiceList.Size = new System.Drawing.Size(145, 21);
            this.DiceList.TabIndex = 13;
            this.DiceList.SelectedIndexChanged += new System.EventHandler(this.DiceList_SelectedIndexChanged);
            // 
            // Join
            // 
            this.Join.Location = new System.Drawing.Point(445, 97);
            this.Join.Name = "Join";
            this.Join.Size = new System.Drawing.Size(66, 34);
            this.Join.TabIndex = 14;
            this.Join.Text = "Join";
            this.Join.UseVisualStyleBackColor = true;
            this.Join.Click += new System.EventHandler(this.Join_Click);
            // 
            // ServerName
            // 
            this.ServerName.Location = new System.Drawing.Point(365, 71);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(147, 20);
            this.ServerName.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(364, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Server Name:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 471);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ServerName);
            this.Controls.Add(this.Join);
            this.Controls.Add(this.DiceList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.History);
            this.Controls.Add(this.End);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Kick);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Players);
            this.Controls.Add(this.Roll);
            this.Controls.Add(this.SessionOpen);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameInput);
            this.MaximumSize = new System.Drawing.Size(540, 510);
            this.MinimumSize = new System.Drawing.Size(540, 510);
            this.Name = "Main";
            this.Text = "DiceGane";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Button SessionOpen;
        private System.Windows.Forms.Button Roll;
        private System.Windows.Forms.ListBox Players;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Kick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Result;
        private System.Windows.Forms.Button End;
        private System.Windows.Forms.ListBox History;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox DiceList;
        private System.Windows.Forms.Button Join;
        private System.Windows.Forms.TextBox ServerName;
        private System.Windows.Forms.Label label5;
    }
}

