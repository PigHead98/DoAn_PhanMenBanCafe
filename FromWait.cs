using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_LTWin
{
    public partial class FromWait : Form
    {   public    int i = 0;
        public FromWait()
        {
            InitializeComponent();

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            i++;
            if (i == 5)
            {
                timer1.Stop();
                Login f = new Login();
                f.ShowDialog();
                this.Close();
            }           
        }
    }
}
