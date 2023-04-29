using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAI_Texture_Redrawer
{
    public partial class Form2 : Form
    {
        TextBox[] boxes;
        public Form2()
        {
            InitializeComponent();
            SetPaths();
        }


        public void SetPaths()
        {
            string[] paths = MainManager.GetSettingsPaths();
            boxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9 };
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].Text = paths[i];
            }
        }

        public void Apply()
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                MainManager.SetPath(MainManager.names[i], boxes[i].Text);
            }
            MainManager.SaveSettings();
        }

        public void ChooseDirectory(int ind)
        {
            string directory = "";
            using (SaveFileDialog sfd = new SaveFileDialog()) //Getting file Path
            {
                sfd.RestoreDirectory = true;
                sfd.Title = "Select Output Folder";
                sfd.FileName = "folder";
                sfd.FilterIndex = 1;
                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    directory = sfd.FileName.Substring(0, sfd.FileName.Length - 7);
                }
                else if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            boxes[ind].Text = directory;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseDirectory(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChooseDirectory(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseDirectory(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChooseDirectory(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChooseDirectory(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChooseDirectory(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChooseDirectory(6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChooseDirectory(7);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChooseDirectory(8);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Apply();
        }
    }
}
