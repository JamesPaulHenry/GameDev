using System;
using System.CodeDom;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapGen
{
    public partial class Form1 : Form
    {
        private Map _map;



        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            button1.Enabled = false;
            _map = new Map();
            var rng = new Random();
            _map.Generate();

            pictureBox1.Image = _map.Image;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            
           
            pictureBox1.Refresh();
            button1.Enabled = true;
            listBox1.Items.Add($"Star count {_map.SystemsCount}");
        }
    }
}