namespace GameEngine
{
    partial class EnterNameWindow
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
            SaveButton = new Button();
            NameTextBox = new TextBox();
            SuspendLayout();
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(38, 53);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(155, 23);
            SaveButton.TabIndex = 0;
            SaveButton.Text = "Сохранить";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += button1_Click;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(27, 12);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(166, 23);
            NameTextBox.TabIndex = 1;
            // 
            // EnterNameWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(224, 88);
            Controls.Add(NameTextBox);
            Controls.Add(SaveButton);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "EnterNameWindow";
            Text = "Введите имя";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SaveButton;
        private TextBox NameTextBox;
    }
}