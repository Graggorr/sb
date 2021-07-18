namespace SB2
{
    partial class Form1
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
            this.SingleRankButton = new System.Windows.Forms.RadioButton();
            this.DoubleRankButton = new System.Windows.Forms.RadioButton();
            this.TripleRankButton = new System.Windows.Forms.RadioButton();
            this.QuadroRankButton = new System.Windows.Forms.RadioButton();
            this.RemoveShipButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SingleRankButton
            // 
            this.SingleRankButton.AutoSize = true;
            this.SingleRankButton.Location = new System.Drawing.Point(3, 168);
            this.SingleRankButton.Name = "SingleRankButton";
            this.SingleRankButton.Size = new System.Drawing.Size(86, 17);
            this.SingleRankButton.TabIndex = 0;
            this.SingleRankButton.TabStop = true;
            this.SingleRankButton.Text = "Single Rank ";
            this.SingleRankButton.UseVisualStyleBackColor = true;
            this.SingleRankButton.Visible = false;
            // 
            // DoubleRankButton
            // 
            this.DoubleRankButton.AutoSize = true;
            this.DoubleRankButton.Location = new System.Drawing.Point(3, 191);
            this.DoubleRankButton.Name = "DoubleRankButton";
            this.DoubleRankButton.Size = new System.Drawing.Size(88, 17);
            this.DoubleRankButton.TabIndex = 1;
            this.DoubleRankButton.TabStop = true;
            this.DoubleRankButton.Text = "Double Rank";
            this.DoubleRankButton.UseVisualStyleBackColor = true;
            this.DoubleRankButton.Visible = false;
            // 
            // TripleRankButton
            // 
            this.TripleRankButton.AutoSize = true;
            this.TripleRankButton.Location = new System.Drawing.Point(3, 214);
            this.TripleRankButton.Name = "TripleRankButton";
            this.TripleRankButton.Size = new System.Drawing.Size(80, 17);
            this.TripleRankButton.TabIndex = 2;
            this.TripleRankButton.TabStop = true;
            this.TripleRankButton.Text = "Triple Rank";
            this.TripleRankButton.UseVisualStyleBackColor = true;
            this.TripleRankButton.Visible = false;
            // 
            // QuadroRankButton
            // 
            this.QuadroRankButton.AutoSize = true;
            this.QuadroRankButton.Location = new System.Drawing.Point(3, 237);
            this.QuadroRankButton.Name = "QuadroRankButton";
            this.QuadroRankButton.Size = new System.Drawing.Size(89, 17);
            this.QuadroRankButton.TabIndex = 3;
            this.QuadroRankButton.TabStop = true;
            this.QuadroRankButton.Text = "Quadro Rank";
            this.QuadroRankButton.UseVisualStyleBackColor = true;
            this.QuadroRankButton.Visible = false;
            // 
            // RemoveShipButton
            // 
            this.RemoveShipButton.Location = new System.Drawing.Point(3, 316);
            this.RemoveShipButton.Name = "RemoveShipButton";
            this.RemoveShipButton.Size = new System.Drawing.Size(119, 23);
            this.RemoveShipButton.TabIndex = 4;
            this.RemoveShipButton.Text = "Remove Ship";
            this.RemoveShipButton.UseVisualStyleBackColor = true;
            this.RemoveShipButton.Visible = false;
            this.RemoveShipButton.Click += new System.EventHandler(this.RemoveShipButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 604);
            this.Controls.Add(this.RemoveShipButton);
            this.Controls.Add(this.QuadroRankButton);
            this.Controls.Add(this.TripleRankButton);
            this.Controls.Add(this.DoubleRankButton);
            this.Controls.Add(this.SingleRankButton);
            this.Name = "Form1";
            this.Text = "Battleship";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton SingleRankButton;
        private System.Windows.Forms.RadioButton DoubleRankButton;
        private System.Windows.Forms.RadioButton TripleRankButton;
        private System.Windows.Forms.RadioButton QuadroRankButton;
        private System.Windows.Forms.Button RemoveShipButton;
    }
}

