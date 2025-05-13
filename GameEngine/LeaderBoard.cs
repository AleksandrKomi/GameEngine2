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
    public partial class LeaderBoard : Form
    {

        public LeaderBoard()
        {
            InitializeComponent();

        }

        private async void LeaderBoard_Load(object sender, EventArgs e)
        {
          
           var leaders = await LeaderAPI.GetLeaders();

            LeadersList.Items.AddRange(leaders.Select(l => $"{l.Name} - {l.Score}").ToArray());

        }
    }
}
