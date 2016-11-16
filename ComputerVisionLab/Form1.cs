using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.Util;

namespace ComputerVisionLab
{
    public partial class Form1 : Form
    {
        private Mat image;
        private Mat outputImage;
        private Canny cannyMethod;
        private int minThreshold = 22;
        private int maxThreshold = 46;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            imageBox1.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;
            metroTrackBar1.Value = minThreshold;
            metroTrackBar2.Value = maxThreshold;
            metroLabel3.Text = metroTrackBar1.Value.ToString();
            metroLabel4.Text = metroTrackBar2.Value.ToString();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files | *.png; *.jpg; *.bmp; | All Files (*.*) | *.* ";
            dialog.Title = "Open File...";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filename = dialog.FileName;
                image = CvInvoke.Imread(filename, Emgu.CV.CvEnum.LoadImageType.AnyColor);
                SetWindowSize();
                imageBox1.Image = image;
                imageBox1.Refresh();
                outputImage = new Mat();
            }
        }

        private void edgeDetectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshCanny();
        }

        private void RefreshCanny()
        {
            if (image != null)
            {
                Mat destImage = new Mat();
                image.CopyTo(destImage);
                cannyMethod = new Canny(destImage, minThreshold, maxThreshold);
                cannyMethod.getOutputImage().CopyTo(outputImage);
                imageBox1.Image = outputImage;
                //imageBox1.Image = cannyMethod.getOutputImage();
                //imageBox1.Image = cannyMethod.getOutputImageOverSourceImage();
                imageBox1.Refresh();
            }
        }

        private void SetWindowSize()
        {
            if (image == null)
                return;
            Size maximumWindowSize = SystemInformation.MaxWindowTrackSize;
            if (image.Width <= maximumWindowSize.Width - 42) // changing size of window depending on size of loaded image
            {
                if (image.Width < 650)
                    Width = 650;
                else
                    Width = image.Width + 42;
                imageBox1.Width = image.Width;
            }
            else
            {
                Width = maximumWindowSize.Width;
                imageBox1.Width = Width - 42;
            }
            if (image.Height <= maximumWindowSize.Height - 128)
            {
                Height = image.Height + 128;
                imageBox1.Height = image.Height;
            }
            else
            {
                Height = maximumWindowSize.Height - 60;
                imageBox1.Height = Height - 128;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = @"JPG Image | *.jpg | PNG Image | *.png | Bitmap Image | *.bmp",
                Title = @"Save As..."
            };
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        imageBox1.Image.Bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case 2:
                        imageBox1.Image.Bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case 3:
                        imageBox1.Image.Bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                }
                fs.Close();
            };
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void metroTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (metroTrackBar1.Value <= maxThreshold - 1)
            {
                metroLabel3.Text = metroTrackBar1.Value.ToString();
                minThreshold = metroTrackBar1.Value;
                RefreshCanny();
            }
        }

        private void metroTrackBar2_ValueChanged(object sender, EventArgs e)
        {
            if (metroTrackBar2.Value >= minThreshold + 1)
            {
                metroLabel4.Text = metroTrackBar2.Value.ToString();
                maxThreshold = metroTrackBar2.Value;
                RefreshCanny();
            }
        }

        private void dilateAndErodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                Canny dest = new Canny(image);
                dest.Dilation(outputImage).CopyTo(outputImage);
                imageBox1.Image = outputImage;
                imageBox1.Refresh();
            }
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                Canny dest = new Canny(image);
                Mat destImage = new Mat();
                destImage = dest.Grayscale(outputImage);
                if (destImage != null)
                {
                    destImage.CopyTo(outputImage);
                    imageBox1.Image = outputImage;
                    imageBox1.Refresh();
                }
            }
        }

        private void findContoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                Canny dest = new Canny(image);
                Mat destImage = new Mat();
                destImage = dest.FindContours(outputImage);
                if (destImage != null)
                {
                    destImage.CopyTo(outputImage);
                    imageBox1.Image = outputImage;
                    imageBox1.Refresh();
                }
            }
        }
    }
}
