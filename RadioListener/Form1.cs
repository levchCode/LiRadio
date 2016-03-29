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

        public bool Parse()
        {
            try
            {
                using (StreamReader file = File.OpenText(path))
                {
                    stations = JsonConvert.DeserializeObject<BindingList<Station>>(File.ReadAllText(path));
                }
                listBox1.DataSource = stations;
                return true;
            }
            catch (JsonReaderException e)
            {
                MessageBox.Show("It seems that list of stations is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return false;
            }
        }

        public bool CheckStations() //Что-то не робит
        {
            foreach (Station s in stations)
            {
                if (s.Name == null || s.URL == null)
                {
                    return false;
                }
            }
            return true;
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
            if (!CheckStations())
            {
                MessageBox.Show("Some stations have incorrect data.");
            }
            //Parse();
            listBox1.DisplayMember = "Name";
            if (Parse())
            {
                player.URL = stations[0].URL;

                player.controls.play();
            }
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             player.controls.stop();
             
             selected = (Station)listBox1.SelectedItem;
             player.URL = selected.URL;
             player.controls.play();
             toolStripProgressBar1.Value = 0;
             toolStripProgressBar1.Value = player.network.bufferingProgress;
             
             
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
                player.controls.stop();
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

    }
}
