namespace DiceGame.Forms
{
    partial class DiceEdit
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
            this.DiceList = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.Upload = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DiceList
            // 
            this.DiceList.LabelEdit = true;
            this.DiceList.Location = new System.Drawing.Point(12, 12);
            this.DiceList.Name = "DiceList";
            this.DiceList.Size = new System.Drawing.Size(171, 378);
            this.DiceList.TabIndex = 0;
            this.DiceList.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.DiceList_AfterLabelEdit);
            this.DiceList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(190, 344);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SaveDice);
            // 
            // Upload
            // 
            this.Upload.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Upload.Location = new System.Drawing.Point(189, 293);
            this.Upload.Name = "Upload";
            this.Upload.Size = new System.Drawing.Size(146, 45);
            this.Upload.TabIndex = 2;
            this.Upload.Text = "Upload";
            this.Upload.UseVisualStyleBackColor = true;
            this.Upload.Click += new System.EventHandler(this.Upload_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(190, 264);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(273, 264);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Add);
            // 
            // DiceEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 402);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Upload);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DiceList);
            this.Name = "DiceEdit";
            this.Text = "DiceEdit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DiceEdit_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView DiceList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Upload;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}