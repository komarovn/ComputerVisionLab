using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.Util;

namespace ComputerVisionLab
{
    class Canny
    {
        private readonly Mat sourceImage;
        private Mat outputImage;
        private Image<Gray, byte> gradients;
        private Image<Gray, byte> angles;
        private readonly int height;
        private readonly int width;
        private readonly int minThreshold;
        private readonly int maxThreshold;

        public Canny(Mat image)
        {
            sourceImage = image;
        }

        public Canny(Mat image, int minThresh, int maxThresh)
        {
            minThreshold = minThresh;
            maxThreshold = maxThresh;
            sourceImage = image;
            outputImage = new Mat();
            sourceImage.CopyTo(outputImage);
            height = sourceImage.Rows;
            width = sourceImage.Cols;
            if (sourceImage.NumberOfChannels == 3)
                CvInvoke.CvtColor(sourceImage, outputImage, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            gaussianBlur();
            calculateGradients();
            nonMaximumSuppression();
            doubleThresholding();
            blobAnalysis();
        }

        private void gaussianBlur()
        {
            Mat dest = new Mat();
            sourceImage.CopyTo(dest);
            CvInvoke.GaussianBlur(outputImage, dest, new System.Drawing.Size(7, 7), 1.4, 1.4);
            dest.CopyTo(outputImage);
        }

        private void calculateGradients()
        {
            Image<Gray, byte> temp = outputImage.ToImage<Gray, byte>();
            gradients = new Image<Gray, byte>(temp.Bitmap);
            angles = new Image<Gray, byte>(temp.Bitmap);
            for (var i = 1; i < width - 1; i++)
                for (var j = 1; j < height - 1; j++)
                {
                    var p1 = temp.Data[j - 1, i - 1, 0]; // upper left
                    var p2 = temp.Data[j - 1, i, 0]; // upper center
                    var p3 = temp.Data[j - 1, i + 1, 0]; // upper right
                    var p4 = temp.Data[j, i - 1, 0]; // center left
                    var p6 = temp.Data[j, i + 1, 0]; // center right
                    var p7 = temp.Data[j + 1, i - 1, 0]; // lower left
                    var p8 = temp.Data[j + 1, i, 0]; // lower center
                    var p9 = temp.Data[j + 1, i + 1, 0]; // lower right

                    var Gx = -p1 - 2*p4 - p7 + p3 + 2*p6 + p9; // x-gradient
                    var Gy = p1 + 2*p2 + p3 - p7 - 2*p8 - p9; // y-gradient

                    float G = (float) Math.Sqrt(Gx*Gx + (float) Gy*Gy); // value of gradient
                    gradients.Data[j, i, 0] = (byte) G;

                    float direction = (float)(Math.Atan(Gy / (float)Gx) * 180.0f / Math.PI); // direction of gradient
                    if (((direction < 22.5) && (direction >= -22.5)) || (direction >= 157.5) || (direction < -157.5))
                        direction = 0;
                    if (((direction >= 22.5) && (direction < 67.5)) || ((direction < -112.5) && (direction >= -157.5)))
                        direction = 45;
                    if (((direction >= 67.5) && (direction < 112.5)) || ((direction < -67.5) && (direction >= -112.5)))
                        direction = 90;
                    if (((direction >= 112.5) && (direction < 157.5)) || ((direction < -22.5) && (direction >= -67.5)))
                        direction = 135;

                    angles.Data[j, i, 0] = (byte) direction;
                }
        }

        private void nonMaximumSuppression()
        {
            Image<Gray, byte> dest = outputImage.ToImage<Gray, byte>();
            for (var i = 1; i < width - 1; i++)
                for (var j = 1; j < height - 1; j++)
                {
                    if (((angles.Data[j, i, 0] == 0) && (gradients.Data[j, i, 0] >= gradients.Data[j, i - 1, 0]) &&
                         (gradients.Data[j, i, 0] >= gradients.Data[j, i + 1, 0])) ||
                        ((angles.Data[j, i, 0] == 45) && (gradients.Data[j, i, 0] >= gradients.Data[j - 1, i + 1, 0]) &&
                         (gradients.Data[j, i, 0] >= gradients.Data[j + 1, i - 1, 0])) ||
                        ((angles.Data[j, i, 0] == 90) && (gradients.Data[j, i, 0] >= gradients.Data[j - 1, i, 0]) &&
                         (gradients.Data[j, i, 0] >= gradients.Data[j + 1, i, 0])) ||
                        ((angles.Data[j, i, 0] == 135) && (gradients.Data[j, i, 0] >= gradients.Data[j - 1, i - 1, 0]) &&
                         (gradients.Data[j, i, 0] >= gradients.Data[j + 1, i + 1, 0])))
                        dest.Data[j, i, 0] = gradients.Data[j, i, 0];
                    else
                        dest.Data[j, i, 0] = 0;
                }
            outputImage = dest.Mat;
        }

        private void doubleThresholding()
        {
            Image<Gray, byte> dest = outputImage.ToImage<Gray, byte>();
            for (var i = 1; i < width - 1; i++)
                for (var j = 1; j < height - 1; j++)
                {
                    var pixel = dest.Data[j, i, 0];
                    if (pixel >= maxThreshold)
                        pixel = 255;
                    else if (pixel <= minThreshold)
                        pixel = 0;
                    else
                        pixel = 127;
                    dest.Data[j, i, 0] = pixel;
                }
            outputImage = dest.Mat;
        }

        private void blobAnalysis()
        {
            Image<Gray, byte> dest = outputImage.ToImage<Gray, byte>();
            for (var i = 1; i < width - 1; i++)
                for (var j = 1; j < height - 1; j++)
                    if (dest.Data[j, i, 0] == 255)
                    {
                        checkNeighbour(dest, j - 1, i - 1, 0);
                        checkNeighbour(dest, j - 1, i, 0);
                        checkNeighbour(dest, j - 1, i + 1, 0);
                        checkNeighbour(dest, j, i - 1, 0);
                        checkNeighbour(dest, j, i + 1, 0);
                        checkNeighbour(dest, j + 1, i - 1, 0);
                        checkNeighbour(dest, j + 1, i, 0);
                        checkNeighbour(dest, j + 1, i + 1, 0);
                    }
            for (var i = 1; i < width - 1; i++)
                for (var j = 1; j < height - 1; j++)
                    if (dest.Data[j, i, 0] == 127)
                        dest.Data[j, i, 0] = 0;
            outputImage = dest.Mat;
        }

        private void checkNeighbour(Image<Gray, byte> dest, int j, int i, int level)
        {
            level++;
            if (level > 200)
                return;
            if (i < width && i > 1 && j < height && j > 1)
            {
                if (dest.Data[j, i, 0] != 127)
                    return;
                dest.Data[j, i, 0] = 255;

                checkNeighbour(dest, j - 1, i - 1, level);
                checkNeighbour(dest, j - 1, i, level);
                checkNeighbour(dest, j - 1, i + 1, level);
                checkNeighbour(dest, j, i - 1, level);
                checkNeighbour(dest, j, i + 1, level);
                checkNeighbour(dest, j + 1, i - 1, level);
                checkNeighbour(dest, j + 1, i, level);
                checkNeighbour(dest, j + 1, i + 1, level);
            }
        }

        public Mat getOutputImage()
        {
            return outputImage;
        }

        public Mat getOutputImageOverSourceImage()
        {
            Image<Bgr, byte> dest = sourceImage.ToImage<Bgr, byte>();
            Image<Gray, byte> outputImageCopy = outputImage.ToImage<Gray, byte>();
            for(int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (outputImageCopy.Data[j, i, 0] == 255)
                    {
                        Bgr color = new Bgr(2, 250, 127);
                        dest[j, i] = color;
                    }
                }
            return dest.Mat;
        }

        public Mat Dilation(Mat src)
        {
            Mat dest = new Mat();
            Mat dest2 = new Mat();
            CvInvoke.Dilate(src, dest, null, new Point(-1, -1), 2, BorderType.Reflect, new MCvScalar());
            CvInvoke.Erode(dest, dest2, null, new Point(-1, -1), 2, BorderType.Reflect, new MCvScalar());
            //CvInvoke.Dilate(dest2, dest, null, new Point(-1, -1), 1, BorderType.Reflect, new MCvScalar());
            return dest2;
        }

        public Mat Grayscale(Mat src)
        {
            if (src.NumberOfChannels < 3)
                return null;
            Mat dest = new Mat();
            CvInvoke.CvtColor(src, dest, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            return dest;
        }

        public Mat FindContours(Mat src)
        {
            //Mat dest = new Mat(src.Rows, src.Cols, DepthType.Cv8U, 3);
            Mat dest = new Mat();
            sourceImage.CopyTo(dest);
            if (src.NumberOfChannels == 3)
                return null;

            using (Mat hierachy = new Mat())
            using (VectorOfVectorOfPoint contoursAfterCannyEdgeDetection = new VectorOfVectorOfPoint())
            using (VectorOfVectorOfPoint contoursAfterCriteriaApplying = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(src, contoursAfterCannyEdgeDetection, hierachy, RetrType.Tree, ChainApproxMethod.ChainApproxNone);

                for (int contourIndex = 0; contourIndex < contoursAfterCannyEdgeDetection.Size; contourIndex++)
                {
                    Rectangle boundingRectangle = CvInvoke.BoundingRectangle(contoursAfterCannyEdgeDetection[contourIndex]);
                    MCvScalar color = new MCvScalar(0, 0, 255);
                    double contourArea = CvInvoke.ContourArea(contoursAfterCannyEdgeDetection[contourIndex], false); //  Find the area of contour
                    double contourPerimeter = CvInvoke.ArcLength(contoursAfterCannyEdgeDetection[contourIndex], true);
                    
                    if (contourArea >= 7)
                    {
                        if (((boundingRectangle.Width < 25) || (boundingRectangle.Height < 25)) && 
                            ((boundingRectangle.Width / (float)boundingRectangle.Height < 1.8f) && 
                            (boundingRectangle.Height / (float)boundingRectangle.Width < 1.8f)))
                        {
                            if (contourArea/(contourPerimeter*contourPerimeter) > 0.05 && contourArea/(contourPerimeter*contourPerimeter) < 0.30)
                            {
                                contoursAfterCriteriaApplying.Push(contoursAfterCannyEdgeDetection[contourIndex]);
                                //CvInvoke.DrawContours(dest, contoursAfterCannyEdgeDetection, contourIndex, new MCvScalar(100, 40, 200), 1, LineType.EightConnected/*, hierachy*/);
                            }
                        }
                    }
                }

                int quantityOfLabels = (int)Math.Ceiling((double)contoursAfterCriteriaApplying.Size/254);
                int sizeOfLastLabels = contoursAfterCriteriaApplying.Size%255;
                Image<Gray, byte> srcImage = sourceImage.ToImage<Gray, byte>();

                for (int k = 0; k < quantityOfLabels; k++)
                {
                    Mat labels = new Mat(src.Rows, src.Cols, DepthType.Cv8U, 2);
                    /*for(int i = 0; i < src.Cols; i++)
                        for(int j = 0; j < src.Rows; j++)
                            labels.SetValue(j, i, (byte)255);*/
                    int hh = (k + 1) * 255;
                    int vv = 255;
                    if (k == quantityOfLabels - 1)
                    {
                        hh = k * 255 + sizeOfLastLabels;
                        vv = sizeOfLastLabels%255;
                    }
                    for (int contourIndex = k * 255; contourIndex < hh; contourIndex++)
                    {
                        CvInvoke.DrawContours(labels, contoursAfterCriteriaApplying, contourIndex,
                                new MCvScalar(contourIndex % 255 + 1), -1);
                            // draw contour[contourIndex] with (contourIndex + 1) gray color 
                    }
                    int[] averageContourIntensity = new int[255];
                    int[] counts = new int[255];

                    for (int i = 0; i < src.Cols; i++)
                        for (int j = 0; j < src.Rows; j++)
                        {
                            byte label = (byte) labels.GetValue(j, i); //ddd[j * src.Cols + i];
                            if (label == 0)
                                continue;
                            label -= 1;
                            byte value = (byte) srcImage[j, i].Intensity;
                            averageContourIntensity[label] += value;
                            ++counts[label];
                        }
                    for (int i = 0; i < vv; i++)
                    {
                        averageContourIntensity[i] /= counts[i];
                        if (averageContourIntensity[i] < 110)
                            CvInvoke.DrawContours(dest, contoursAfterCriteriaApplying, i + k * 255, new MCvScalar(108, 240, 3), 2,
                                LineType.EightConnected /*, hierachy*/);
                        //else
                        //{
                          //  ;
                            //CvInvoke.DrawContours(dest, contoursAfterCriteriaApplying, i, new MCvScalar(0, 100, 230), 1, LineType.EightConnected/*, hierachy*/);
                        //}
                    }
                }
            }

            return dest;
        }
    }
}