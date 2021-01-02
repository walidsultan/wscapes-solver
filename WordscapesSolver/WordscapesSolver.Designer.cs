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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordscapesSolver));
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeviceId = new System.Windows.Forms.TextBox();
            this.lblGameStateTitle = new System.Windows.Forms.Label();
            this.lblGameStateValue = new System.Windows.Forms.Label();
            this.lblIsFourWordsOnlyTItle = new System.Windows.Forms.Label();
            this.lblIsFourWordsOnlyValue = new System.Windows.Forms.Label();
            this.lblSwipeMethod = new System.Windows.Forms.Label();
            this.cbSwipeMethod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNativeSwipePause = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(219, 183);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(82, 29);
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
            this.lblGameStateTitle.Location = new System.Drawing.Point(150, 61);
            this.lblGameStateTitle.Name = "lblGameStateTitle";
            this.lblGameStateTitle.Size = new System.Drawing.Size(66, 13);
            this.lblGameStateTitle.TabIndex = 3;
            this.lblGameStateTitle.Text = "Game State:";
            // 
            // lblGameStateValue
            // 
            this.lblGameStateValue.AutoSize = true;
            this.lblGameStateValue.Location = new System.Drawing.Point(240, 61);
            this.lblGameStateValue.Name = "lblGameStateValue";
            this.lblGameStateValue.Size = new System.Drawing.Size(0, 13);
            this.lblGameStateValue.TabIndex = 4;
            // 
            // lblIsFourWordsOnlyTItle
            // 
            this.lblIsFourWordsOnlyTItle.AutoSize = true;
            this.lblIsFourWordsOnlyTItle.Location = new System.Drawing.Point(127, 89);
            this.lblIsFourWordsOnlyTItle.Name = "lblIsFourWordsOnlyTItle";
            this.lblIsFourWordsOnlyTItle.Size = new System.Drawing.Size(89, 13);
            this.lblIsFourWordsOnlyTItle.TabIndex = 5;
            this.lblIsFourWordsOnlyTItle.Text = "Four Words Only:";
            // 
            // lblIsFourWordsOnlyValue
            // 
            this.lblIsFourWordsOnlyValue.AutoSize = true;
            this.lblIsFourWordsOnlyValue.Location = new System.Drawing.Point(240, 89);
            this.lblIsFourWordsOnlyValue.Name = "lblIsFourWordsOnlyValue";
            this.lblIsFourWordsOnlyValue.Size = new System.Drawing.Size(0, 13);
            this.lblIsFourWordsOnlyValue.TabIndex = 6;
            // 
            // lblSwipeMethod
            // 
            this.lblSwipeMethod.AutoSize = true;
            this.lblSwipeMethod.Location = new System.Drawing.Point(138, 118);
            this.lblSwipeMethod.Name = "lblSwipeMethod";
            this.lblSwipeMethod.Size = new System.Drawing.Size(78, 13);
            this.lblSwipeMethod.TabIndex = 7;
            this.lblSwipeMethod.Text = "Swipe Method:";
            // 
            // cbSwipeMethod
            // 
            this.cbSwipeMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSwipeMethod.FormattingEnabled = true;
            this.cbSwipeMethod.Location = new System.Drawing.Point(243, 118);
            this.cbSwipeMethod.Name = "cbSwipeMethod";
            this.cbSwipeMethod.Size = new System.Drawing.Size(121, 21);
            this.cbSwipeMethod.TabIndex = 8;
            this.cbSwipeMethod.SelectedIndexChanged += new System.EventHandler(this.cbSwipeMethod_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Swipe Pause:";
            // 
            // txtNativeSwipePause
            // 
            this.txtNativeSwipePause.Location = new System.Drawing.Point(243, 155);
            this.txtNativeSwipePause.Name = "txtNativeSwipePause";
            this.txtNativeSwipePause.Size = new System.Drawing.Size(100, 20);
            this.txtNativeSwipePause.TabIndex = 10;
            this.txtNativeSwipePause.TextChanged += new System.EventHandler(this.txtNativeSwipePause_TextChanged);
            this.txtNativeSwipePause.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNativeSwipePause_KeyPress);
            // 
            // WordscapesSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 224);
            this.Controls.Add(this.txtNativeSwipePause);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSwipeMethod);
            this.Controls.Add(this.lblSwipeMethod);
            this.Controls.Add(this.lblIsFourWordsOnlyValue);
            this.Controls.Add(this.lblIsFourWordsOnlyTItle);
            this.Controls.Add(this.lblGameStateValue);
            this.Controls.Add(this.lblGameStateTitle);
            this.Controls.Add(this.txtDeviceId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WordscapesSolver";
            this.Text = "Wordscapes Solver";
            this.Load += new System.EventHandler(this.WordscapesSolver_Load);
            this.Resize += new System.EventHandler(this.WordscapesSolver_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeviceId;
        private System.Windows.Forms.Label lblGameStateTitle;
        private System.Windows.Forms.Label lblGameStateValue;
        private System.Windows.Forms.Label lblIsFourWordsOnlyTItle;
        private System.Windows.Forms.Label lblIsFourWordsOnlyValue;
        private System.Windows.Forms.Label lblSwipeMethod;
        private System.Windows.Forms.ComboBox cbSwipeMethod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNativeSwipePause;
    }
}

