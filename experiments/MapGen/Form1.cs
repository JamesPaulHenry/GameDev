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
        private int current = -1;


        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            current = -1;
            button1.Enabled = false;
            _map = new Map();
            var rng = new Random();
            _map.Generate();

            pictureBox1.Image = _map.Image;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;


            pictureBox1.Refresh();
            button1.Enabled = true;
            listBox1.Items.Add($"Star count {_map.SystemsCount}");
            button3.Enabled = true;
            label1.Text = "Galaxy";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            current--;
            try
            {

                pictureBox1.Image = _map.Regions[current].Image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Refresh();

                label1.Text = $"Region: {current}";
                if (current == 0)
                {
                    current = -1;
                }
            }
            catch (Exception ex) { ex.Data.Clear(); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            current++;
            try
            {
                pictureBox1.Image = _map.Regions[current].Image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Refresh();
                label1.Text = $"Region: {current}";

                if (current == 100)
                {
                    current = 1;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

        }
    }
}