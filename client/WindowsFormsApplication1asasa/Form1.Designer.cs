namespace step1_client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxIp = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxUsn = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxPost = new System.Windows.Forms.TextBox();
            this.buttonDisc = new System.Windows.Forms.Button();
            this.buttonPost = new System.Windows.Forms.Button();
            this.buttonGetPosts = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxIp
            // 
            this.textBoxIp.Location = new System.Drawing.Point(73, 38);
            this.textBoxIp.Name = "textBoxIp";
            this.textBoxIp.Size = new System.Drawing.Size(125, 22);
            this.textBoxIp.TabIndex = 0;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(73, 95);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(125, 22);
            this.textBoxPort.TabIndex = 0;
            // 
            // textBoxUsn
            // 
            this.textBoxUsn.Location = new System.Drawing.Point(97, 155);
            this.textBoxUsn.Name = "textBoxUsn";
            this.textBoxUsn.Size = new System.Drawing.Size(125, 22);
            this.textBoxUsn.TabIndex = 0;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(63, 242);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(126, 25);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxPost
            // 
            this.textBoxPost.Enabled = false;
            this.textBoxPost.Location = new System.Drawing.Point(328, 42);
            this.textBoxPost.Name = "textBoxPost";
            this.textBoxPost.Size = new System.Drawing.Size(206, 22);
            this.textBoxPost.TabIndex = 0;
            // 
            // buttonDisc
            // 
            this.buttonDisc.Enabled = false;
            this.buttonDisc.Location = new System.Drawing.Point(63, 292);
            this.buttonDisc.Name = "buttonDisc";
            this.buttonDisc.Size = new System.Drawing.Size(126, 25);
            this.buttonDisc.TabIndex = 1;
            this.buttonDisc.Text = "Disconnect";
            this.buttonDisc.UseVisualStyleBackColor = true;
            this.buttonDisc.Click += new System.EventHandler(this.buttonDisc_Click);
            // 
            // buttonPost
            // 
            this.buttonPost.Enabled = false;
            this.buttonPost.Location = new System.Drawing.Point(358, 73);
            this.buttonPost.Name = "buttonPost";
            this.buttonPost.Size = new System.Drawing.Size(126, 25);
            this.buttonPost.TabIndex = 1;
            this.buttonPost.Text = "Post";
            this.buttonPost.UseVisualStyleBackColor = true;
            this.buttonPost.Click += new System.EventHandler(this.buttonPost_Click);
            // 
            // buttonGetPosts
            // 
            this.buttonGetPosts.Enabled = false;
            this.buttonGetPosts.Location = new System.Drawing.Point(358, 350);
            this.buttonGetPosts.Name = "buttonGetPosts";
            this.buttonGetPosts.Size = new System.Drawing.Size(126, 25);
            this.buttonGetPosts.TabIndex = 1;
            this.buttonGetPosts.Text = "Get Posts";
            this.buttonGetPosts.UseVisualStyleBackColor = true;
            this.buttonGetPosts.Click += new System.EventHandler(this.buttonGetPosts_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(290, 128);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(279, 215);
            this.logs.TabIndex = 2;
            this.logs.Text = "";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(26, 42);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(24, 17);
            this.label.TabIndex = 3;
            this.label.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(278, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Post";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 480);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.buttonGetPosts);
            this.Controls.Add(this.buttonPost);
            this.Controls.Add(this.buttonDisc);
            this.Controls.Add(this.textBoxPost);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxUsn);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIp);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxIp;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxUsn;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxPost;
        private System.Windows.Forms.Button buttonDisc;
        private System.Windows.Forms.Button buttonPost;
        private System.Windows.Forms.Button buttonGetPosts;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

