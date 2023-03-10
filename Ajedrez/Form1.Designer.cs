namespace Ajedrez;
    partial class Ajedrez
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Tablero = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Tablero
            // 
            this.Tablero.AutoSize = true;
            this.Tablero.Location = new System.Drawing.Point(727, 281);
            this.Tablero.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tablero.Name = "Tablero";
            this.Tablero.Size = new System.Drawing.Size(320, 203);
            this.Tablero.TabIndex = 0;
            this.Tablero.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1296, 365);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // Ajedrez
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1813, 805);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Tablero);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Ajedrez";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajedrez";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;

        #endregion
        private System.Windows.Forms.Panel Tablero;
    }