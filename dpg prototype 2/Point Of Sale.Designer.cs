namespace DPG_prototype_v2
{
    partial class frmPOS
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
            this.btnSale = new System.Windows.Forms.Button();
            this.btnHairProdLkp = new System.Windows.Forms.Button();
            this.btnCustLkp = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSale
            // 
            this.btnSale.AutoSize = true;
            this.btnSale.Location = new System.Drawing.Point(71, 30);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(382, 68);
            this.btnSale.TabIndex = 0;
            this.btnSale.Text = "&Sale";
            this.btnSale.UseVisualStyleBackColor = true;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // btnHairProdLkp
            // 
            this.btnHairProdLkp.AutoSize = true;
            this.btnHairProdLkp.Location = new System.Drawing.Point(71, 146);
            this.btnHairProdLkp.Name = "btnHairProdLkp";
            this.btnHairProdLkp.Size = new System.Drawing.Size(382, 68);
            this.btnHairProdLkp.TabIndex = 0;
            this.btnHairProdLkp.Text = "&Haircut/Product Lookup";
            this.btnHairProdLkp.UseVisualStyleBackColor = true;
            this.btnHairProdLkp.Click += new System.EventHandler(this.btnHairProdLkp_Click);
            // 
            // btnCustLkp
            // 
            this.btnCustLkp.AutoSize = true;
            this.btnCustLkp.Location = new System.Drawing.Point(71, 259);
            this.btnCustLkp.Name = "btnCustLkp";
            this.btnCustLkp.Size = new System.Drawing.Size(382, 68);
            this.btnCustLkp.TabIndex = 0;
            this.btnCustLkp.Text = "Customer Lookup";
            this.btnCustLkp.UseVisualStyleBackColor = true;
            this.btnCustLkp.Click += new System.EventHandler(this.btnCustLkp_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.btnCustLkp);
            this.panel1.Controls.Add(this.btnHairProdLkp);
            this.panel1.Controls.Add(this.btnSale);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 375);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(70, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(556, 381);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // frmPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(687, 460);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPOS";
            this.Text = "Point Of Sale";
            this.Load += new System.EventHandler(this.frmPOS_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.Button btnHairProdLkp;
        private System.Windows.Forms.Button btnCustLkp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

