using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int score = 0;
        int timeleft = 10;
        System.Windows.Forms.PictureBox[] picboxes;
 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            picboxes = new System.Windows.Forms.PictureBox[64];

            int px = 0, py=0;

            for (int i = 0; i < 64; ++i)
            {
                picboxes[i] = new System.Windows.Forms.PictureBox();
                picboxes[i].Width = 32;
                picboxes[i].Height = 32;
                picboxes[i].Left = px;
                picboxes[i].Top = py;

                picboxes[i].Click += new System.EventHandler(PicBoxClick);
                panel1.Controls.Add(picboxes[i]);

                px += 40;
                if (px >= 40 * 8)
                {
                    px = 0;
                    py += 40;
                }
            }

            colorize();
        }

        private void colorize() 
        {
            Random rnd = new Random();
            for (int i = 0; i < 64; ++i)
            {
                picboxes[i].BackColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            }
            pictureBox1.BackColor = picboxes[rnd.Next(0, 64)].BackColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorize();
        }

        private void PicBoxClick(Object sender, System.EventArgs e)
        {
            System.Windows.Forms.PictureBox picbox = (System.Windows.Forms.PictureBox)sender;
            pictureBox2.BackColor = picbox.BackColor;

            if (pictureBox1.BackColor == pictureBox2.BackColor)
            {
                score += 10;
                Random rnd = new Random();
                pictureBox1.BackColor = picboxes[rnd.Next(0, 64)].BackColor;
                timeleft = 10;
            }
            else
            {
                score -= 5;
            }

            label3.Text = "Score : " + score;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeleft -= 1;
            label4.Text = "Time Left : " + timeleft + " sec";

            if (timeleft <= 0)
            {
                timer1.Stop();
                DialogResult res = MessageBox.Show("Game Over!! Finale Score : "+score);
                if (res == DialogResult.OK)
                {
                    timer1.Start();
                    timeleft = 10;
                    score = 0;
                    colorize();
                    label3.Text = "Score : 0";
                }
            }
        }
    }
}