using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine
{
    public partial class EnterNameWindow : Form
    {
        public string PlayerName { get; private set; }


        public EnterNameWindow()
        {
            InitializeComponent();
            NameTextBox.Text = NickNameSaver.Read();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlayerName = NameTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
                                     
            NickNameSaver.Save(PlayerName);


        }
    }
}
