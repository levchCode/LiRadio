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
using Newtonsoft.Json;

namespace RadioListener
{
    public partial class NewStation : Form
    {
        
        public NewStation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Station s = new Station();
            s.Name = textBox1.Text;
            s.URL = textBox2.Text;
            
        }
    }
}
