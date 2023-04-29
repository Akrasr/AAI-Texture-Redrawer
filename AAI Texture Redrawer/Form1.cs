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
    public partial class Form1 : Form
    {
        public static Color Back;
        MainManager mm;
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.DarkGray;
            listBox1.Items.AddRange(MainManager.names);
            Back = this.BackColor;
        }

        private void Open()
        {
            TextureType tt = MainManager.GetType((string)listBox1.SelectedItem);
            mm = new MainManager(MainManager.GetPath((string)listBox1.SelectedItem), tt, pictureBox1.CreateGraphics());
            Initialize();
        }

        private void OpenTextFile()
        {
            string path = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) //Getting file's path
            {
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Open text file";
                openFileDialog.Filter = "Text file (*.txt)|*.txt|Any file (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                DialogResult dr = openFileDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    path = openFileDialog.FileName;
                }
                else if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            mm.SaveAllImagesFromFile(path);
        }

        private void Initialize()
        {
            trackBar1.Maximum = mm.GetCount() - 1;
            trackBar1.Value = 0;
            textBox1.Lines = new string[] { "", "", "" };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mm.SetTranslatedText(textBox1.Lines);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mm.SaveTranslatedText();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenTextFile();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mm.DrawLast();
            trackBar1.Value = trackBar1.Value - 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            trackBar1.Value = trackBar1.Value + 1;
            mm.DrawNext();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            mm.DrawImages(trackBar1.Value);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainManager.OpenSettings();
        }
    }
}
