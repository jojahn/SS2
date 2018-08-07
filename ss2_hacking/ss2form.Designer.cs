namespace ss2_hacking
{
    partial class ss2form
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button0_0 = new System.Windows.Forms.Button();
            this.button1_0 = new System.Windows.Forms.Button();
            this.button0_2 = new System.Windows.Forms.Button();
            this.button0_1 = new System.Windows.Forms.Button();
            this.button1_1 = new System.Windows.Forms.Button();
            this.button1_2 = new System.Windows.Forms.Button();
            this.button2_0 = new System.Windows.Forms.Button();
            this.button2_2 = new System.Windows.Forms.Button();
            this.button2_1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button0_0
            // 
            this.button0_0.Location = new System.Drawing.Point(103, 125);
            this.button0_0.Name = "button0_0";
            this.button0_0.Size = new System.Drawing.Size(79, 54);
            this.button0_0.TabIndex = 0;
            this.button0_0.Text = "0,0";
            this.button0_0.UseVisualStyleBackColor = true;
            this.button0_0.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1_0
            // 
            this.button1_0.Location = new System.Drawing.Point(103, 209);
            this.button1_0.Name = "button1_0";
            this.button1_0.Size = new System.Drawing.Size(79, 54);
            this.button1_0.TabIndex = 2;
            this.button1_0.Text = "1,0";
            this.button1_0.UseVisualStyleBackColor = true;
            this.button1_0.Click += new System.EventHandler(this.button1_0_Click);
            // 
            // button0_2
            // 
            this.button0_2.Location = new System.Drawing.Point(350, 125);
            this.button0_2.Name = "button0_2";
            this.button0_2.Size = new System.Drawing.Size(79, 54);
            this.button0_2.TabIndex = 3;
            this.button0_2.Text = "0,2";
            this.button0_2.UseVisualStyleBackColor = true;
            this.button0_2.Click += new System.EventHandler(this.button0_2_Click);
            // 
            // button0_1
            // 
            this.button0_1.Location = new System.Drawing.Point(233, 125);
            this.button0_1.Name = "button0_1";
            this.button0_1.Size = new System.Drawing.Size(79, 54);
            this.button0_1.TabIndex = 4;
            this.button0_1.Text = "0,1";
            this.button0_1.UseVisualStyleBackColor = true;
            this.button0_1.Click += new System.EventHandler(this.button0_1_Click);
            // 
            // button1_1
            // 
            this.button1_1.Location = new System.Drawing.Point(233, 209);
            this.button1_1.Name = "button1_1";
            this.button1_1.Size = new System.Drawing.Size(79, 54);
            this.button1_1.TabIndex = 5;
            this.button1_1.Text = "1,1";
            this.button1_1.UseVisualStyleBackColor = true;
            this.button1_1.Click += new System.EventHandler(this.button1_1_Click);
            // 
            // button1_2
            // 
            this.button1_2.Location = new System.Drawing.Point(350, 209);
            this.button1_2.Name = "button1_2";
            this.button1_2.Size = new System.Drawing.Size(79, 54);
            this.button1_2.TabIndex = 6;
            this.button1_2.Text = "1,2";
            this.button1_2.UseVisualStyleBackColor = true;
            this.button1_2.Click += new System.EventHandler(this.button1_2_Click);
            // 
            // button2_0
            // 
            this.button2_0.Location = new System.Drawing.Point(103, 285);
            this.button2_0.Name = "button2_0";
            this.button2_0.Size = new System.Drawing.Size(79, 54);
            this.button2_0.TabIndex = 7;
            this.button2_0.Text = "2,0";
            this.button2_0.UseVisualStyleBackColor = true;
            this.button2_0.Click += new System.EventHandler(this.button2_0_Click);
            // 
            // button2_2
            // 
            this.button2_2.Location = new System.Drawing.Point(350, 285);
            this.button2_2.Name = "button2_2";
            this.button2_2.Size = new System.Drawing.Size(79, 54);
            this.button2_2.TabIndex = 8;
            this.button2_2.Text = "2,2";
            this.button2_2.UseVisualStyleBackColor = true;
            this.button2_2.Click += new System.EventHandler(this.button2_2_Click);
            // 
            // button2_1
            // 
            this.button2_1.Location = new System.Drawing.Point(233, 285);
            this.button2_1.Name = "button2_1";
            this.button2_1.Size = new System.Drawing.Size(79, 54);
            this.button2_1.TabIndex = 9;
            this.button2_1.Text = "2,1";
            this.button2_1.UseVisualStyleBackColor = true;
            this.button2_1.Click += new System.EventHandler(this.button2_1_Click);
            // 
            // ss2form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2_1);
            this.Controls.Add(this.button2_2);
            this.Controls.Add(this.button2_0);
            this.Controls.Add(this.button1_2);
            this.Controls.Add(this.button1_1);
            this.Controls.Add(this.button0_1);
            this.Controls.Add(this.button0_2);
            this.Controls.Add(this.button1_0);
            this.Controls.Add(this.button0_0);
            this.Name = "ss2form";
            this.Text = "SS2";
            this.Load += new System.EventHandler(this.form_load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button0_0;
        private System.Windows.Forms.Button button1_0;
        private System.Windows.Forms.Button button0_2;
        private System.Windows.Forms.Button button0_1;
        private System.Windows.Forms.Button button1_1;
        private System.Windows.Forms.Button button1_2;
        private System.Windows.Forms.Button button2_0;
        private System.Windows.Forms.Button button2_2;
        private System.Windows.Forms.Button button2_1;
    }
}

