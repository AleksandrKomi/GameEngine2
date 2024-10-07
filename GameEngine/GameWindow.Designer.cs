namespace GameEngine
{
    partial class GameWindow
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
            this.components = new System.ComponentModel.Container();
            this.UpdateScreenTimer = new System.Windows.Forms.Timer(this.components);
            this.FPSCounterTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // UpdateScreenTimer
            // 
            this.UpdateScreenTimer.Interval = 1;
            this.UpdateScreenTimer.Tick += new System.EventHandler(this.UpdateScreenTimer_Tick);
            // 
            // FPSCounterTimer
            // 
            this.FPSCounterTimer.Interval = 1000;
            this.FPSCounterTimer.Tick += new System.EventHandler(this.FPSCounterTimer_Tick);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameWindow";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer UpdateScreenTimer;
        private System.Windows.Forms.Timer FPSCounterTimer;
    }
}