using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        private readonly int PADDING_HEIGHT = 205;

        private Mat image;
        private Mat outputImage;
        private DetermineAstrocytes determineAstrocytes;
        private int minThreshold = 22;
        private int maxThreshold = 46;
        private Channels channel = Channels.GRAY;

        public Form1()
        {
            InitializeComponent();
            var pos1 = this.PointToScreen(label2.Location);
            pos1 = imageBox1.PointToClient(pos1);
            label2.Parent = imageBox1;
            label2.Location = pos1;
            label2.BackColor = Color.Transparent;
            var pos2 = this.PointToScreen(label1.Location);
            pos2 = imageBox1.PointToClient(pos2);
            label1.Parent = imageBox1;
            label1.Location = pos2;
            label1.BackColor = Color.Transparent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            imageBox1.FunctionalMode = ImageBox.FunctionalModeOption.PanAndZoom;
            metroTrackBar1.Value = minThreshold;
            metroTrackBar2.Value = maxThreshold;
            metroLabel3.Text = metroTrackBar1.Value.ToString();
            metroLabel4.Text = metroTrackBar2.Value.ToString();
            setChannel();
        }

        private void setChannel()
        {
            if (radioGray.Checked)
                channel = Channels.GRAY;
            if (radioRed.Checked)
                channel = Channels.RED;
            if (radioGreen.Checked)
                channel = Channels.GREEN;
            if (radioBlue.Checked)
                channel = Channels.BLUE;
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
            setChannel();
            if (image != null)
            {
                Mat destImage = new Mat();
                image.CopyTo(destImage);
                determineAstrocytes = new DetermineAstrocytes(destImage, minThreshold, maxThreshold, channel);
                determineAstrocytes.getOutputImage().CopyTo(outputImage);
                imageBox1.Image = outputImage;
                //imageBox1.Image = determineAstrocytes.getOutputImage();
                //imageBox1.Image = determineAstrocytes.getOutputImageOverSourceImage();
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
            if (image.Height <= maximumWindowSize.Height - PADDING_HEIGHT)
            {
                Height = image.Height + PADDING_HEIGHT;
                imageBox1.Height = image.Height;
            }
            else
            {
                Height = maximumWindowSize.Height - 60;
                imageBox1.Height = Height - PADDING_HEIGHT;
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
                ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 400L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        imageBox1.Image.Bitmap.Save(fs, jgpEncoder, myEncoderParameters);
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

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
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
            }
        }

        private void metroTrackBar2_ValueChanged(object sender, EventArgs e)
        {
            if (metroTrackBar2.Value >= minThreshold + 1)
            {
                metroLabel4.Text = metroTrackBar2.Value.ToString();
            }
        }

        private void dilateAndErodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                DetermineAstrocytes dest = new DetermineAstrocytes(image);
                dest.MathMorphology(outputImage).CopyTo(outputImage);
                imageBox1.Image = outputImage;
                imageBox1.Refresh();
            }
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                DetermineAstrocytes dest = new DetermineAstrocytes(image);
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
                DetermineAstrocytes dest = new DetermineAstrocytes(image);
                Mat destImage = new Mat();
                destImage = dest.FindContours(outputImage);
                if (destImage != null)
                {
                    label2.Text = dest.getNumberOfAstrocytes().ToString();
                    destImage.CopyTo(outputImage);
                    imageBox1.Image = outputImage;
                    imageBox1.Refresh();
                }
            }
        }

        private void metroTrackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if (metroTrackBar1.Value <= maxThreshold - 1)
            {
                metroLabel3.Text = metroTrackBar1.Value.ToString();
                minThreshold = metroTrackBar1.Value;
                RefreshCanny();
            }
        }

        private void metroTrackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            if (metroTrackBar2.Value >= minThreshold + 1)
            {
                metroLabel4.Text = metroTrackBar2.Value.ToString();
                maxThreshold = metroTrackBar2.Value;
                RefreshCanny();
            }
        }

        private void radioGray_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
