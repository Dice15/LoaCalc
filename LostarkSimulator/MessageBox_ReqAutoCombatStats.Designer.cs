namespace LostarkSimulator
{
    partial class MessageBox_ReqAutoCombatStats
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
            this.Yes = new System.Windows.Forms.Button();
            this.No = new System.Windows.Forms.Button();
            this.Crit_Lower = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Crit_Upper = new System.Windows.Forms.ComboBox();
            this.Spec_Upper = new System.Windows.Forms.ComboBox();
            this.Spec_Lower = new System.Windows.Forms.ComboBox();
            this.Swift_Upper = new System.Windows.Forms.ComboBox();
            this.Swift_Lower = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Yes
            // 
            this.Yes.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Yes.Location = new System.Drawing.Point(107, 228);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(109, 38);
            this.Yes.TabIndex = 0;
            this.Yes.Text = "확인";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // No
            // 
            this.No.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.No.Location = new System.Drawing.Point(222, 228);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(109, 38);
            this.No.TabIndex = 3;
            this.No.Text = "취소";
            this.No.UseVisualStyleBackColor = true;
            this.No.Click += new System.EventHandler(this.No_Click);
            // 
            // Crit_Lower
            // 
            this.Crit_Lower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Crit_Lower.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Crit_Lower.FormattingEnabled = true;
            this.Crit_Lower.IntegralHeight = false;
            this.Crit_Lower.Location = new System.Drawing.Point(78, 60);
            this.Crit_Lower.Name = "Crit_Lower";
            this.Crit_Lower.Size = new System.Drawing.Size(120, 33);
            this.Crit_Lower.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(16, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 30);
            this.label3.TabIndex = 293;
            this.label3.Text = "신속";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(16, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 30);
            this.label2.TabIndex = 292;
            this.label2.Text = "특화";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(16, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 30);
            this.label4.TabIndex = 291;
            this.label4.Text = "치명";
            // 
            // Crit_Upper
            // 
            this.Crit_Upper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Crit_Upper.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Crit_Upper.FormattingEnabled = true;
            this.Crit_Upper.IntegralHeight = false;
            this.Crit_Upper.Location = new System.Drawing.Point(213, 60);
            this.Crit_Upper.Name = "Crit_Upper";
            this.Crit_Upper.Size = new System.Drawing.Size(120, 33);
            this.Crit_Upper.TabIndex = 294;
            // 
            // Spec_Upper
            // 
            this.Spec_Upper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Spec_Upper.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Spec_Upper.FormattingEnabled = true;
            this.Spec_Upper.IntegralHeight = false;
            this.Spec_Upper.Location = new System.Drawing.Point(213, 117);
            this.Spec_Upper.Name = "Spec_Upper";
            this.Spec_Upper.Size = new System.Drawing.Size(120, 33);
            this.Spec_Upper.TabIndex = 296;
            // 
            // Spec_Lower
            // 
            this.Spec_Lower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Spec_Lower.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Spec_Lower.FormattingEnabled = true;
            this.Spec_Lower.IntegralHeight = false;
            this.Spec_Lower.Location = new System.Drawing.Point(78, 117);
            this.Spec_Lower.Name = "Spec_Lower";
            this.Spec_Lower.Size = new System.Drawing.Size(120, 33);
            this.Spec_Lower.TabIndex = 295;
            // 
            // Swift_Upper
            // 
            this.Swift_Upper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Swift_Upper.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Swift_Upper.FormattingEnabled = true;
            this.Swift_Upper.IntegralHeight = false;
            this.Swift_Upper.Location = new System.Drawing.Point(213, 173);
            this.Swift_Upper.Name = "Swift_Upper";
            this.Swift_Upper.Size = new System.Drawing.Size(120, 33);
            this.Swift_Upper.TabIndex = 298;
            // 
            // Swift_Lower
            // 
            this.Swift_Lower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Swift_Lower.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Swift_Lower.FormattingEnabled = true;
            this.Swift_Lower.IntegralHeight = false;
            this.Swift_Lower.Location = new System.Drawing.Point(78, 173);
            this.Swift_Lower.Name = "Swift_Lower";
            this.Swift_Lower.Size = new System.Drawing.Size(120, 33);
            this.Swift_Lower.TabIndex = 297;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(102, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 30);
            this.label5.TabIndex = 299;
            this.label5.Text = "하한값";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(233, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 30);
            this.label6.TabIndex = 300;
            this.label6.Text = "상한값";
            // 
            // MessageBox_ReqAutoCombatStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(370, 287);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Swift_Upper);
            this.Controls.Add(this.Swift_Lower);
            this.Controls.Add(this.Spec_Upper);
            this.Controls.Add(this.Spec_Lower);
            this.Controls.Add(this.Crit_Upper);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Crit_Lower);
            this.Controls.Add(this.No);
            this.Controls.Add(this.Yes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBox_ReqAutoCombatStats";
            this.Text = "특성별 하한값, 상한값 설정";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageBox_ReqAutoCombatStats_FormClosing);
            this.Load += new System.EventHandler(this.MessageBox_ReqAutoCombatStats_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Yes;
        private System.Windows.Forms.Button No;
        private System.Windows.Forms.ComboBox Crit_Lower;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Crit_Upper;
        private System.Windows.Forms.ComboBox Spec_Upper;
        private System.Windows.Forms.ComboBox Spec_Lower;
        private System.Windows.Forms.ComboBox Swift_Upper;
        private System.Windows.Forms.ComboBox Swift_Lower;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}