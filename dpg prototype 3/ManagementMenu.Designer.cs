namespace DPG_prototype_v2
{
    partial class frmMngMenu
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnHairProdEdit = new System.Windows.Forms.Button();
            this.btnCustEdit = new System.Windows.Forms.Button();
            this.btnRevInfo = new System.Windows.Forms.Button();
            this.btnProfLoss = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnProfLoss);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnHairProdEdit);
            this.panel1.Controls.Add(this.btnCustEdit);
            this.panel1.Controls.Add(this.btnRevInfo);
            this.panel1.Location = new System.Drawing.Point(47, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 378);
            this.panel1.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBack.Location = new System.Drawing.Point(11, 284);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(343, 71);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnHairProdEdit
            // 
            this.btnHairProdEdit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHairProdEdit.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnHairProdEdit.Location = new System.Drawing.Point(11, 198);
            this.btnHairProdEdit.Name = "btnHairProdEdit";
            this.btnHairProdEdit.Size = new System.Drawing.Size(343, 71);
            this.btnHairProdEdit.TabIndex = 0;
            this.btnHairProdEdit.Text = "Haircut/Product Lookup";
            this.btnHairProdEdit.UseVisualStyleBackColor = false;
            this.btnHairProdEdit.Click += new System.EventHandler(this.btnHairProdEdit_Click);
            // 
            // btnCustEdit
            // 
            this.btnCustEdit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCustEdit.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCustEdit.Location = new System.Drawing.Point(11, 111);
            this.btnCustEdit.Name = "btnCustEdit";
            this.btnCustEdit.Size = new System.Drawing.Size(343, 71);
            this.btnCustEdit.TabIndex = 0;
            this.btnCustEdit.Text = "Customer Lookup";
            this.btnCustEdit.UseVisualStyleBackColor = false;
            this.btnCustEdit.Click += new System.EventHandler(this.btnCustEdit_Click);
            // 
            // btnRevInfo
            // 
            this.btnRevInfo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRevInfo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRevInfo.Location = new System.Drawing.Point(11, 25);
            this.btnRevInfo.Name = "btnRevInfo";
            this.btnRevInfo.Size = new System.Drawing.Size(343, 71);
            this.btnRevInfo.TabIndex = 0;
            this.btnRevInfo.Text = "Revenue Information";
            this.btnRevInfo.UseVisualStyleBackColor = false;
            this.btnRevInfo.Click += new System.EventHandler(this.btnRevInfo_Click);
            // 
            // btnProfLoss
            // 
            this.btnProfLoss.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnProfLoss.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnProfLoss.Location = new System.Drawing.Point(358, 25);
            this.btnProfLoss.Name = "btnProfLoss";
            this.btnProfLoss.Size = new System.Drawing.Size(343, 71);
            this.btnProfLoss.TabIndex = 0;
            this.btnProfLoss.Text = "Profit/Loss Information";
            this.btnProfLoss.UseVisualStyleBackColor = false;
            this.btnProfLoss.Click += new System.EventHandler(this.btnProfLoss_Click);
            // 
            // frmMngMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "frmMngMenu";
            this.Text = "Management Menu";
            this.Load += new System.EventHandler(this.frmMngMenu_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnHairProdEdit;
        private System.Windows.Forms.Button btnCustEdit;
        private System.Windows.Forms.Button btnRevInfo;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnProfLoss;
    }
}

