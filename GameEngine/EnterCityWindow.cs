using GameEngine.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine
{
    public partial class EnterCityWindow : Form
    {
        public string? NameCity { get; set; }

        public EnterCityWindow()
        {
            InitializeComponent();
            NameTextBox.Text = "Загрузка...";
            NameTextBox.Enabled = false;
            SaveButton.Enabled = false;
            YesButton.Enabled = false;
            NoButton.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NameCity = NameTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
            CitySaver.Save(NameCity);
        }

        private async void EnterCityWindow_Load(object sender, EventArgs e)
        {
            /*string? ip = await IPAPI.GetIP();
            await GeoAPI.GetLocationByIP(ip);
            NameTextBox.Text = ip; //CitySaver.Read();
            NameTextBox.Enabled = true;
            SaveButton.Enabled = true;*/

            string? ip = await IPAPI.GetIP();
            NameCity = await GeoAPI.GetPointLocation(ip);
            NameTextBox.Text = "Ваш город " + NameCity + "?";
            NameTextBox.ReadOnly = true;
            NameTextBox.Enabled = true;
            YesButton.Enabled = true;
            NoButton.Enabled = true;

        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
            CitySaver.Save(NameCity);
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            YesButton.Enabled = false;
            NoButton.Enabled = false;
            SaveButton.Enabled = true;
            NameTextBox.Text = string.Empty;
            NameTextBox.ReadOnly = false;
        }
    }
}
