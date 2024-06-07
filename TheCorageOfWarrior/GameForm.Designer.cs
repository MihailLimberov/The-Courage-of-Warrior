namespace The_Courage_of_a_Warrior
{
    partial class GameForm
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
            this.gameField = new System.Windows.Forms.PictureBox();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.textBoxHelp = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gameField)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameField
            // 
            this.gameField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.gameField.Location = new System.Drawing.Point(0, 0);
            this.gameField.Name = "gameField";
            this.gameField.Size = new System.Drawing.Size(6840, 5815);
            this.gameField.TabIndex = 0;
            this.gameField.TabStop = false;
            this.gameField.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdateGameField);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHelp.Location = new System.Drawing.Point(1784, 12);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(127, 61);
            this.buttonHelp.TabIndex = 2;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.DimGray;
            this.panel.Controls.Add(this.textBoxHelp);
            this.panel.Controls.Add(this.gameField);
            this.panel.Location = new System.Drawing.Point(68, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1710, 970);
            this.panel.TabIndex = 4;
            // 
            // textBoxHelp
            // 
            this.textBoxHelp.Enabled = false;
            this.textBoxHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxHelp.Location = new System.Drawing.Point(0, 0);
            this.textBoxHelp.Multiline = true;
            this.textBoxHelp.Name = "textBoxHelp";
            this.textBoxHelp.Size = new System.Drawing.Size(418, 378);
            this.textBoxHelp.TabIndex = 5;
            this.textBoxHelp.Visible = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(790, 575);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.buttonHelp);
            this.KeyPreview = true;
            this.Name = "GameForm";
            this.Text = "The Courage of a Warrior";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.gameField)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox gameField;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TextBox textBoxHelp;
    }
}

