using System;
using System.Windows.Forms;

namespace RadioListener
{
    public partial class TV : Form
    {
        public TV()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.settings.autoStart = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = textBox1.Text;
            axWindowsMediaPlayer1.Ctlcontrols.play();

        } 

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
