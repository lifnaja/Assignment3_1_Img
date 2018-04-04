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

            int[,] colorPic = new int[f_image.Width, f_image.Height];
            // Loop through the images pixels
            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {
                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.R + PixelColor.G +
                    PixelColor.B) / 3;
                    colorPic[i, j] = C_gray;
                }
            }
        }

        private void btn_Histogram_Click(object sender, EventArgs e)
        {
            double[] runningSum = new double[256];
            int[] countColor = new int[256];
            int pixel = f_image.Height * f_image.Width;
            int color = 255;
            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {
                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    countColor[C_gray]++;
                }
            }
            //running sum
            runningSum[0] = countColor[0];
            for (int i = 1; i < 256; i++)
            {
                runningSum[i] = runningSum[i - 1] + countColor[i];
                //Console.WriteLine(countColor[i] +"//"+ runningSum[i]);
            }

            for (int i = 0; i < 256; i++)
            {
                runningSum[i] = Math.Round(runningSum[i] / pixel * color);
                //Console.WriteLine(runningSum[i]);
            }

            image = new Bitmap(f_image.Width, f_image.Height);
            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {
                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    int NewColor = (int)runningSum[C_gray];
                    image.SetPixel(i, j, Color.FromArgb(NewColor, NewColor, NewColor));
                }
            }
            pictureBox2.Image = image;
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
