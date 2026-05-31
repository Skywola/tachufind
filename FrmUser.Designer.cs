namespace Tachufind
{
    partial class FrmUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUser));
            label1 = new Label();
            rtbUser = new RichTextBox();
            btnAccept = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13F);
            label1.Location = new Point(121, 4);
            label1.Name = "label1";
            label1.Size = new Size(377, 25);
            label1.TabIndex = 0;
            label1.Text = "Tachufind  Version 8.0  User's Agreement";
            // 
            // rtbUser
            // 
            rtbUser.BackColor = SystemColors.ScrollBar;
            rtbUser.Location = new Point(7, 32);
            rtbUser.Name = "rtbUser";
            rtbUser.Size = new Size(628, 516);
            rtbUser.TabIndex = 1;
            rtbUser.TabStop = false;
            rtbUser.Text = resources.GetString("rtbUser.Text");
            // 
            // btnAccept
            // 
            btnAccept.BackColor = Color.RosyBrown;
            btnAccept.Location = new Point(91, 554);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(187, 43);
            btnAccept.TabIndex = 0;
            btnAccept.Text = "Accept";
            btnAccept.UseVisualStyleBackColor = false;
            btnAccept.Click += btnAccept_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.RosyBrown;
            btnCancel.Location = new Point(325, 554);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(187, 43);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmUser
            // 
            AutoScaleDimensions = new SizeF(14F, 29F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(650, 597);
            Controls.Add(btnCancel);
            Controls.Add(btnAccept);
            Controls.Add(rtbUser);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 15F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximumSize = new Size(668, 644);
            MinimumSize = new Size(668, 644);
            Name = "FrmUser";
            Text = "  Tachufind";
            FormClosing += FrmUser_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private RichTextBox rtbUser;
        private Button btnAccept;
        private Button btnCancel;
    }
}