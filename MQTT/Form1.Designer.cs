namespace MQTT
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
            this.SendButton = new System.Windows.Forms.Button();
            this.MessageOutputBox = new System.Windows.Forms.RichTextBox();
            this.DataBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(271, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(190, 386);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // MessageOutputBox
            // 
            this.MessageOutputBox.Location = new System.Drawing.Point(59, 254);
            this.MessageOutputBox.Name = "MessageOutputBox";
            this.MessageOutputBox.Size = new System.Drawing.Size(287, 92);
            this.MessageOutputBox.TabIndex = 2;
            this.MessageOutputBox.Text = "";
            this.MessageOutputBox.TextChanged += new System.EventHandler(this.MessageBox_TextChanged);
            // 
            // DataBox
            // 
            this.DataBox.Location = new System.Drawing.Point(59, 12);
            this.DataBox.Name = "DataBox";
            this.DataBox.ReadOnly = true;
            this.DataBox.Size = new System.Drawing.Size(287, 198);
            this.DataBox.TabIndex = 1;
            this.DataBox.Text = "";
            this.DataBox.TextChanged += new System.EventHandler(this.DataBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 424);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.MessageOutputBox);
            this.Controls.Add(this.DataBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.RichTextBox MessageOutputBox;
        private System.Windows.Forms.RichTextBox DataBox;
    }
}

