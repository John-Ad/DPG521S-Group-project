namespace DPG_prototype_v2
{
    partial class frmHRMenu
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
            this.btnEqpEdit = new System.Windows.Forms.Button();
            this.btnPayRollEdit = new System.Windows.Forms.Button();
            this.btnEmpEdit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnEqpEdit);
            this.panel1.Controls.Add(this.btnPayRollEdit);
            this.panel1.Controls.Add(this.btnEmpEdit);
            this.panel1.Location = new System.Drawing.Point(47, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 371);
            this.panel1.TabIndex = 0;
            // 
            // btnEqpEdit
            // 
            this.btnEqpEdit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEqpEdit.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnEqpEdit.Location = new System.Drawing.Point(20, 198);
            this.btnEqpEdit.Name = "btnEqpEdit";
            this.btnEqpEdit.Size = new System.Drawing.Size(672, 71);
            this.btnEqpEdit.TabIndex = 0;
            this.btnEqpEdit.Text = "Equipment Lookup";
            this.btnEqpEdit.UseVisualStyleBackColor = false;
            this.btnEqpEdit.Click += new System.EventHandler(this.btnEqpEdit_Click);
            // 
            // btnPayRollEdit
            // 
            this.btnPayRollEdit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPayRollEdit.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPayRollEdit.Location = new System.Drawing.Point(20, 111);
            this.btnPayRollEdit.Name = "btnPayRollEdit";
            this.btnPayRollEdit.Size = new System.Drawing.Size(672, 71);
            this.btnPayRollEdit.TabIndex = 0;
            this.btnPayRollEdit.Text = "Payroll Lookup";
            this.btnPayRollEdit.UseVisualStyleBackColor = false;
            this.btnPayRollEdit.Click += new System.EventHandler(this.btnPayRollEdit_Click);
            // 
            // btnEmpEdit
            // 
            this.btnEmpEdit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEmpEdit.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnEmpEdit.Location = new System.Drawing.Point(20, 25);
            this.btnEmpEdit.Name = "btnEmpEdit";
            this.btnEmpEdit.Size = new System.Drawing.Size(672, 71);
            this.btnEmpEdit.TabIndex = 0;
            this.btnEmpEdit.Text = "Employee Lookup";
            this.btnEmpEdit.UseVisualStyleBackColor = false;
            this.btnEmpEdit.Click += new System.EventHandler(this.btnEmpEdit_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBack.Location = new System.Drawing.Point(20, 285);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(672, 71);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmHRMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "frmHRMenu";
            this.Text = "Human Resources Menu";
            this.Load += new System.EventHandler(this.frmMngMenu_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEqpEdit;
        private System.Windows.Forms.Button btnPayRollEdit;
        private System.Windows.Forms.Button btnEmpEdit;
        private System.Windows.Forms.Button btnBack;
    }
}

