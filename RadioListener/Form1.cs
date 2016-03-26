using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WMPLib;
using Newtonsoft.Json;

         

namespace RadioListener
{
    public partial class Form1 : Form
    {
        
        public WindowsMediaPlayer player = new WindowsMediaPlayer();
        string path = "Default.json";
       public BindingList<Station> stations = new BindingList<Station>();
        Station selected = new Station();

        public void Parse()
        {
            
            using (StreamReader file = File.OpenText(path))
            {
                stations = JsonConvert.DeserializeObject<BindingList<Station>>(File.ReadAllText(path));
            }
            listBox1.DataSource = stations;
        }

        public Form1()
        {
            InitializeComponent();
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 100;
            trackBar1.Value = 50;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Parse();

            listBox1.DisplayMember = "Name";

            player.URL = stations[0].URL;

            player.controls.play();
            
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             player.controls.stop();
             
             selected = (Station)listBox1.SelectedItem;
             player.URL = selected.URL;
             player.controls.play();
             
             
        }

        private void Form1_Resize(object sender, EventArgs e)  //НЕ работает
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipText = selected.Name;
                notifyIcon1.ShowBalloonTip(1000);
            }  
        }

        private void newStationListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewStation newS = new NewStation();
            newS.Show();
        }

        private void openStationsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            path = openFileDialog1.FileName;
            Parse();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (player.playState == WMPPlayState.wmppsPlaying)
            {
                button1.Text = "Pause";
                player.controls.pause();
            }
            if(player.playState == WMPPlayState.wmppsPaused)
            {
                button1.Text = "Play";
                player.controls.play();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)   //НЕ работает
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;  
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            player.settings.volume = trackBar1.Value;
        }

    }
}
