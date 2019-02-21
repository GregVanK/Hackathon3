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
            this.connectButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.MessageOutputBox = new System.Windows.Forms.RichTextBox();
            this.DataBox = new System.Windows.Forms.RichTextBox();
            this.sendFleetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(361, 475);
            this.connectButton.Margin = new System.Windows.Forms.Padding(4);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(100, 28);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(253, 475);
            this.SendButton.Margin = new System.Windows.Forms.Padding(4);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(100, 28);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // MessageOutputBox
            // 
            this.MessageOutputBox.Location = new System.Drawing.Point(79, 313);
            this.MessageOutputBox.Margin = new System.Windows.Forms.Padding(4);
            this.MessageOutputBox.Name = "MessageOutputBox";
            this.MessageOutputBox.Size = new System.Drawing.Size(381, 112);
            this.MessageOutputBox.TabIndex = 2;
            this.MessageOutputBox.Text = "";
            this.MessageOutputBox.TextChanged += new System.EventHandler(this.MessageBox_TextChanged);
            // 
            // DataBox
            // 
            this.DataBox.Location = new System.Drawing.Point(79, 15);
            this.DataBox.Margin = new System.Windows.Forms.Padding(4);
            this.DataBox.Name = "DataBox";
            this.DataBox.ReadOnly = true;
            this.DataBox.Size = new System.Drawing.Size(381, 243);
            this.DataBox.TabIndex = 1;
            this.DataBox.Text = "";
            this.DataBox.TextChanged += new System.EventHandler(this.DataBox_TextChanged);
            // 
            // sendFleetButton
            // 
            this.sendFleetButton.Location = new System.Drawing.Point(12, 475);
            this.sendFleetButton.Name = "sendFleetButton";
            this.sendFleetButton.Size = new System.Drawing.Size(115, 28);
            this.sendFleetButton.TabIndex = 4;
            this.sendFleetButton.Text = "Send Fleet";
            this.sendFleetButton.UseVisualStyleBackColor = true;
            this.sendFleetButton.Click += new System.EventHandler(this.sendFleetButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 522);
            this.Controls.Add(this.sendFleetButton);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.MessageOutputBox);
            this.Controls.Add(this.DataBox);
            this.Controls.Add(this.connectButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.RichTextBox MessageOutputBox;
        private System.Windows.Forms.RichTextBox DataBox;
        private System.Windows.Forms.Button sendFleetButton;
    }
}

