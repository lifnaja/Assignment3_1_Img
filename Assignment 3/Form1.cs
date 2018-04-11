using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assignment_3
{
    public partial class Form1 : Form
    {
        private Bitmap f_image = null;
        public Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileImage = new OpenFileDialog();
            OpenFileImage.Filter = "bitmap(*.bmp) | *.bmp";
            OpenFileImage.FilterIndex = 1;
            if (OpenFileImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (f_image != null)
                        f_image.Dispose();
                    f_image =
                    (Bitmap)Bitmap.FromFile(OpenFileImage.FileName, false);

                }
                catch (Exception)
                {
                    MessageBox.Show("Can not open file", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            pictureBox1.Image = f_image;
            CreateHistogram();


        }

        public void CreateHistogram()
        {
            int[] arrGray = new int[256];

            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {
                    Color PixelColor = f_image.GetPixel(i, j);
                    arrGray[PixelColor.R]++;
                }
            }


            for (int i = 1; i < arrGray.Length; i++)
            {
                this.Histogram.Series["Color"].Points.AddXY(i, arrGray[i]);
             
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            image = new Bitmap(f_image.Width, f_image.Height);
            int color_1 = Convert.ToInt32(txt_color1.Text);
            int color_2 = Convert.ToInt32(txt_color2.Text);
            int color_3 = Convert.ToInt32(txt_color3.Text);
            int color_4 = Convert.ToInt32(txt_color4.Text);
            int color_5 = Convert.ToInt32(txt_color5.Text);
            int color_6 = Convert.ToInt32(txt_color6.Text);

            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {
                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                   
                    if(C_gray >= color_6)
                    {
                        image.SetPixel(i, j, Color.FromArgb(218, 112, 214));
                    }
                    else if(C_gray >= color_5)
                    {
                        image.SetPixel(i, j, Color.FromArgb(0, 192, 192));
                    }
                    else if (C_gray >= color_4)
                    {
                        image.SetPixel(i, j, Color.FromArgb(255, 255, 0));
                    }
                    else if (C_gray >= color_3)
                    {
                        image.SetPixel(i, j, Color.FromArgb(0, 0, 255));
                    }
                    else if (C_gray >= color_2)
                    {
                        image.SetPixel(i, j, Color.FromArgb(0, 255, 0));
                    }
                    else
                    {
                        image.SetPixel(i, j, Color.FromArgb(255, 0, 0));
                    }


                }
            }
            pictureBox2.Image = image;
        }

       
    }
}
