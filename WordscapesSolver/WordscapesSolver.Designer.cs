namespace WS.Wscapes
{
    partial class WordscapesSolver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

       

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeviceId = new System.Windows.Forms.TextBox();
            this.lblGameStateTitle = new System.Windows.Forms.Label();
            this.lblGameStateValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(191, 107);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(114, 51);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device Id:";
            // 
            // txtDeviceId
            // 
            this.txtDeviceId.Location = new System.Drawing.Point(243, 29);
            this.txtDeviceId.Name = "txtDeviceId";
            this.txtDeviceId.Size = new System.Drawing.Size(100, 20);
            this.txtDeviceId.TabIndex = 2;
            this.txtDeviceId.Text = "HT7380202634";
            // 
            // lblGameStateTitle
            // 
            this.lblGameStateTitle.AutoSize = true;
            this.lblGameStateTitle.Location = new System.Drawing.Point(150, 71);
            this.lblGameStateTitle.Name = "lblGameStateTitle";
            this.lblGameStateTitle.Size = new System.Drawing.Size(66, 13);
            this.lblGameStateTitle.TabIndex = 3;
            this.lblGameStateTitle.Text = "Game State:";
            // 
            // lblGameStateValue
            // 
            this.lblGameStateValue.AutoSize = true;
            this.lblGameStateValue.Location = new System.Drawing.Point(240, 71);
            this.lblGameStateValue.Name = "lblGameStateValue";
            this.lblGameStateValue.Size = new System.Drawing.Size(0, 13);
            this.lblGameStateValue.TabIndex = 4;
            // 
            // WordscapesSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 184);
            this.Controls.Add(this.lblGameStateValue);
            this.Controls.Add(this.lblGameStateTitle);
            this.Controls.Add(this.txtDeviceId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.MaximizeBox = false;
            this.Name = "WordscapesSolver";
            this.Text = "Wordscapes Solver";
            this.Load += new System.EventHandler(this.WordscapesSolver_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeviceId;
        private System.Windows.Forms.Label lblGameStateTitle;
        private System.Windows.Forms.Label lblGameStateValue;
    }
}

