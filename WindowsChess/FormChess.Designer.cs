namespace WindowsChess
{
    partial class FormChess
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
            this.Board = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Board
            // 
            this.Board.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Board.Location = new System.Drawing.Point(12, 12);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(450, 450);
            this.Board.TabIndex = 0;
            this.Board.Paint += new System.Windows.Forms.PaintEventHandler(this.Board_Paint);
            this.Board.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_MouseClick);
            // 
            // FormChess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 482);
            this.Controls.Add(this.Board);
            this.MaximizeBox = false;
            this.Name = "FormChess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Chess";
            this.Load += new System.EventHandler(this.FormChess_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Board;
    }
}

