namespace LostarkSimulator
{
    partial class DetailedInfo
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
            this.DetailedInfoBeforeHalf = new System.Windows.Forms.TextBox();
            this.DetailedInfoAfterHalf = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DetailedInfoBeforeHalf
            // 
            this.DetailedInfoBeforeHalf.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DetailedInfoBeforeHalf.Location = new System.Drawing.Point(27, 81);
            this.DetailedInfoBeforeHalf.Multiline = true;
            this.DetailedInfoBeforeHalf.Name = "DetailedInfoBeforeHalf";
            this.DetailedInfoBeforeHalf.ReadOnly = true;
            this.DetailedInfoBeforeHalf.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DetailedInfoBeforeHalf.Size = new System.Drawing.Size(676, 793);
            this.DetailedInfoBeforeHalf.TabIndex = 161;
            // 
            // DetailedInfoAfterHalf
            // 
            this.DetailedInfoAfterHalf.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DetailedInfoAfterHalf.Location = new System.Drawing.Point(736, 81);
            this.DetailedInfoAfterHalf.Multiline = true;
            this.DetailedInfoAfterHalf.Name = "DetailedInfoAfterHalf";
            this.DetailedInfoAfterHalf.ReadOnly = true;
            this.DetailedInfoAfterHalf.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DetailedInfoAfterHalf.Size = new System.Drawing.Size(676, 793);
            this.DetailedInfoAfterHalf.TabIndex = 162;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label22.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label22.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label22.Location = new System.Drawing.Point(287, 22);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(127, 45);
            this.label22.TabIndex = 310;
            this.label22.Text = "약무 전";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(1012, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 45);
            this.label1.TabIndex = 311;
            this.label1.Text = "약무 후";
            // 
            // DetailedInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1451, 897);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.DetailedInfoAfterHalf);
            this.Controls.Add(this.DetailedInfoBeforeHalf);
            this.Name = "DetailedInfo";
            this.Text = "상세 정보";
            this.Load += new System.EventHandler(this.DetailedInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DetailedInfoBeforeHalf;
        private System.Windows.Forms.TextBox DetailedInfoAfterHalf;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label1;
    }
}