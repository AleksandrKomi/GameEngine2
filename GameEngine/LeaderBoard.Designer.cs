namespace GameEngine
{
    partial class LeaderBoard
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
            LeadersList = new ListBox();
            SuspendLayout();
            // 
            // LeadersList
            // 
            LeadersList.FormattingEnabled = true;
            LeadersList.ItemHeight = 15;
            LeadersList.Location = new Point(28, 10);
            LeadersList.Name = "LeadersList";
            LeadersList.Size = new Size(232, 154);
            LeadersList.TabIndex = 0;
            // 
            // LeaderBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(294, 175);
            Controls.Add(LeadersList);
            Name = "LeaderBoard";
            Text = "LeaderBoard";
            Load += LeaderBoard_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox LeadersList;
    }
}