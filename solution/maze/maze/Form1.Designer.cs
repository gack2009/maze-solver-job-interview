namespace maze
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
            this.sltFilebtn = new System.Windows.Forms.Button();
            this.lblFileLocation = new System.Windows.Forms.Label();
            this.URLTextField = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sltFilebtn
            // 
            this.sltFilebtn.Location = new System.Drawing.Point(139, 144);
            this.sltFilebtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sltFilebtn.Name = "sltFilebtn";
            this.sltFilebtn.Size = new System.Drawing.Size(100, 28);
            this.sltFilebtn.TabIndex = 0;
            this.sltFilebtn.Text = "Select File";
            this.sltFilebtn.UseVisualStyleBackColor = true;
            this.sltFilebtn.Click += new System.EventHandler(this.sltFilebtn_Click);
            // 
            // lblFileLocation
            // 
            this.lblFileLocation.AutoSize = true;
            this.lblFileLocation.Location = new System.Drawing.Point(41, 84);
            this.lblFileLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileLocation.Name = "lblFileLocation";
            this.lblFileLocation.Size = new System.Drawing.Size(88, 17);
            this.lblFileLocation.TabIndex = 2;
            this.lblFileLocation.Text = "File Location";
            // 
            // URLTextField
            // 
            this.URLTextField.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.URLTextField.Location = new System.Drawing.Point(139, 84);
            this.URLTextField.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.URLTextField.Name = "URLTextField";
            this.URLTextField.ReadOnly = true;
            this.URLTextField.Size = new System.Drawing.Size(276, 22);
            this.URLTextField.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(316, 144);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 28);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 252);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.URLTextField);
            this.Controls.Add(this.lblFileLocation);
            this.Controls.Add(this.sltFilebtn);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sltFilebtn;
        private System.Windows.Forms.Label lblFileLocation;
        private System.Windows.Forms.TextBox URLTextField;
        private System.Windows.Forms.Button btnStart;
    }
}

