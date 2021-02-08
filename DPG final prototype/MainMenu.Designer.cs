namespace DPG_prototype_v2
{
    partial class frmMainMenu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEmpInfo = new System.Windows.Forms.Button();
            this.btnHR = new System.Windows.Forms.Button();
            this.btnMng = new System.Windows.Forms.Button();
            this.btnPOS = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnEmpInfo);
            this.panel1.Controls.Add(this.btnHR);
            this.panel1.Controls.Add(this.btnMng);
            this.panel1.Controls.Add(this.btnPOS);
            this.panel1.Location = new System.Drawing.Point(25, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 402);
            this.panel1.TabIndex = 0;
            // 
            // btnEmpInfo
            // 
            this.btnEmpInfo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEmpInfo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnEmpInfo.Location = new System.Drawing.Point(52, 241);
            this.btnEmpInfo.Name = "btnEmpInfo";
            this.btnEmpInfo.Size = new System.Drawing.Size(672, 71);
            this.btnEmpInfo.TabIndex = 0;
            this.btnEmpInfo.Text = "Employee Information";
            this.btnEmpInfo.UseVisualStyleBackColor = false;
            this.btnEmpInfo.Click += new System.EventHandler(this.btnEmpInfo_Click);
            // 
            // btnHR
            // 
            this.btnHR.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHR.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnHR.Location = new System.Drawing.Point(52, 164);
            this.btnHR.Name = "btnHR";
            this.btnHR.Size = new System.Drawing.Size(672, 71);
            this.btnHR.TabIndex = 0;
            this.btnHR.Text = "Human Resources";
            this.btnHR.UseVisualStyleBackColor = false;
            this.btnHR.Click += new System.EventHandler(this.btnHR_Click);
            // 
            // btnMng
            // 
            this.btnMng.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnMng.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMng.Location = new System.Drawing.Point(52, 87);
            this.btnMng.Name = "btnMng";
            this.btnMng.Size = new System.Drawing.Size(672, 71);
            this.btnMng.TabIndex = 0;
            this.btnMng.Text = "Management";
            this.btnMng.UseVisualStyleBackColor = false;
            this.btnMng.Click += new System.EventHandler(this.btnMng_Click);
            // 
            // btnPOS
            // 
            this.btnPOS.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPOS.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPOS.Location = new System.Drawing.Point(52, 10);
            this.btnPOS.Name = "btnPOS";
            this.btnPOS.Size = new System.Drawing.Size(672, 71);
            this.btnPOS.TabIndex = 0;
            this.btnPOS.Text = "Point Of Sale";
            this.btnPOS.UseVisualStyleBackColor = false;
            this.btnPOS.Click += new System.EventHandler(this.btnPOS_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBack.Location = new System.Drawing.Point(52, 318);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(672, 71);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 453);
            this.Controls.Add(this.panel1);
            this.Name = "frmMainMenu";
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.frmMainMenu_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEmpInfo;
        private System.Windows.Forms.Button btnHR;
        private System.Windows.Forms.Button btnMng;
        private System.Windows.Forms.Button btnPOS;
        private System.Windows.Forms.Button btnBack;
    }
}

