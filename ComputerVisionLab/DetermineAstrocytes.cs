using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
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
    class DetermineAstrocytes
    {
        private readonly Mat sourceImage;
        private Mat outputImage;

        private readonly int height;
        private readonly int width;
        private readonly int minThreshold;
        private readonly int maxThreshold;
        private int totalNumberOfAstrocytes;
        private int[] astrocytesCenters;

        private Image<Gray, byte> gradients;
        private Image<Gray, byte> angles;

        public DetermineAstrocytes(Mat image)
        {
            sourceImage = image;
        }

        public DetermineAstrocytes(Mat image, int minThresh, int maxThresh, Channels channel)
        {
            minThreshold = minThresh;
            maxThreshold = maxThresh;
            sourceImage = image;
            outputImage = new Mat();
            sourceImage.CopyTo(outputImage);
            height = sourceImage.Rows;
            width = sourceImage.Cols;
            if (sourceImage.NumberOfChannels == 3)
            {
                if (channel == Channels.GRAY)
                {
                    CvInvoke.CvtColor(sourceImage, outputImage, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                }
                else
                {
                    Image<Gray, Byte>[] channelsMat = sourceImage.ToImage<Bgr, Byte>().Split();
                    if (channel == Channels.BLUE)
                    {
                        outputImage = channelsMat[0].Mat;
                    }
                    if (channel == Channels.GREEN)
                    {
                        outputImage = channelsMat[1].Mat;
                    }
                    if (channel == Channels.RED)
                    {
                        outputImage = channelsMat[2].Mat;
                    }
                }
            }

            gaussianBlur();
            Mat dest = new Mat();
            outputImage.CopyTo(dest);
            CvInvoke.Canny(outputImage, dest, minThreshold, maxThreshold, 3, true);
            dest.CopyTo(outputImage);

            //gaussianBlur();
            //calculateGradients();
            //nonMaximumSuppression();
            //doubleThresholding();
            //blobAnalysis();
        }

        private void gaussianBlur()
        {
            Mat dest = new Mat();
            sourceImage.CopyTo(dest);
            CvInvoke.GaussianBlur(outputImage, dest, new System.Drawing.Size(9, 9), 1.4, 1.4);
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

        /**
         * Making dilation and erosion with contours after Canny method applying.
         */
        public Mat MathMorphology(Mat src)
        {
            Mat destDilate = new Mat();
            Mat destErode = new Mat();
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(5, 5), new Point(2, 2));
            CvInvoke.MorphologyEx(src, destErode, MorphOp.Close, element, new Point(-1, -1), 1, BorderType.Constant, new MCvScalar());
            //CvInvoke.Dilate(destDilate, destErode, element, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
            //CvInvoke.Erode(src, destErode, element, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
            return destErode;
        }

        /**
         * Converting color image into gray image.
         * @param src - source color image
         * @return dest - grayscale image
         */
        public Mat Grayscale(Mat src)
        {
            if (src.NumberOfChannels < 3)
                return null;
            Mat dest = new Mat();
            CvInvoke.CvtColor(src, dest, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
            return dest;
        }

        /** 
         * @param src - source white-black image after Canny edge detection 
         * @return dest - color image with contours
         */
        public Mat FindContours(Mat src)
        {
            if (src.NumberOfChannels == 3)
                return null;

            Mat dest = new Mat();
            sourceImage.CopyTo(dest);

            Image<Gray, byte> srcImageGray = sourceImage.ToImage<Gray, byte>();
            totalNumberOfAstrocytes = 0;

            using (Mat hierachy = new Mat())
            using (VectorOfVectorOfPoint contoursAfterCannyEdgeDetection = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(src, contoursAfterCannyEdgeDetection, hierachy, RetrType.Tree, ChainApproxMethod.ChainApproxTc89L1);

                for (int contourIndex = 0; contourIndex < contoursAfterCannyEdgeDetection.Size; contourIndex++)
                {
                    Rectangle boundingRectangle = CvInvoke.BoundingRectangle(contoursAfterCannyEdgeDetection[contourIndex]);
                    int xCoordOfAstrocyteCenter = boundingRectangle.Left + boundingRectangle.Width / 2;
                    int yCoordOfAstrocyteCenter = boundingRectangle.Top + boundingRectangle.Height / 2;
                    Point astrocyteCenter = new Point(xCoordOfAstrocyteCenter, yCoordOfAstrocyteCenter);
                    int boundRectSize = 0;
                    int intensityValue = 0;
                    int contourAreaValue = 0;

                    if (astrocyteCenter.Y <= 484)
                    {
                        contourAreaValue = 7;
                        boundRectSize = 25;
                        intensityValue = 110;
                    }
                    else if (astrocyteCenter.Y > 484 && astrocyteCenter.Y <= 1265)
                    {
                        contourAreaValue = 7;
                        boundRectSize = 25;
                        intensityValue = 110;
                    }
                    else if (astrocyteCenter.Y > 1265 && astrocyteCenter.Y <= 1561)
                    {
                        contourAreaValue = 7;
                        boundRectSize = 25;
                        intensityValue = 110;
                    }
                    else if (astrocyteCenter.Y > 1561 && astrocyteCenter.Y <= 2276)
                    {
                        contourAreaValue = 7;
                        boundRectSize = 25;
                        intensityValue = 110;
                    }
                    else if (astrocyteCenter.Y > 2276 && astrocyteCenter.Y <= 3203)
                    {
                        contourAreaValue = 100;
                        boundRectSize = 200;
                        intensityValue = 190;
                    }
                    else if (astrocyteCenter.Y > 3203)
                    {
                        contourAreaValue = 7;
                        boundRectSize = 100;
                        intensityValue = 150;
                    }

                    if (boundingRectangle.Height <= 18 && boundingRectangle.Width <= 18)
                    {
                        contourAreaValue = 9;
                        boundRectSize = 18;
                        intensityValue = 130;
                    }
                    else
                    {
                        contourAreaValue = 12;
                        boundRectSize = 50;
                        intensityValue = 110;
                    }
                    
                    analizeContours(contoursAfterCannyEdgeDetection, contourIndex, boundingRectangle, srcImageGray, astrocyteCenter, contourAreaValue, boundRectSize, intensityValue, ref dest);
                }
            }

            drawLayerDelimiters(ref dest);
            return dest;
        }

        private void analizeContours(VectorOfVectorOfPoint contours, int contourIndex, Rectangle boundingRectangle, Image<Gray, Byte> srcImageGray, Point astrocyteCenter, int contourAreaValue, int boundRectSize, int intensityValue, ref Mat dest)
        {
            double contourArea = CvInvoke.ContourArea(contours[contourIndex]);
            double contourPerimeter = CvInvoke.ArcLength(contours[contourIndex], true);

            if (contourArea >= contourAreaValue)
            {
                if (((boundingRectangle.Width < boundRectSize) || (boundingRectangle.Height < boundRectSize)) &&
                    (boundingRectangle.Width / (float)boundingRectangle.Height < 1.8f) &&
                    (boundingRectangle.Height / (float)boundingRectangle.Width < 1.8f))
                {
                    if (contourArea / (contourPerimeter * contourPerimeter) > 0.05 && contourArea / (contourPerimeter * contourPerimeter) < 0.30)
                    {
                        int averageIntensityInsideContour = 0;
                        int quantityOfPixelsInsideContour = 0;
                        for (int xCoord = boundingRectangle.Left; xCoord <= boundingRectangle.Right; xCoord++)
                        {
                            for (int yCoord = boundingRectangle.Top; yCoord <= boundingRectangle.Bottom; yCoord++)
                            {
                                if (CvInvoke.PointPolygonTest(contours[contourIndex], new Point(xCoord, yCoord), false) > 0)
                                {
                                    averageIntensityInsideContour += (byte)srcImageGray[yCoord, xCoord].Intensity;
                                    quantityOfPixelsInsideContour++;
                                }
                            }
                        }
                        averageIntensityInsideContour /= quantityOfPixelsInsideContour;
                        if (averageIntensityInsideContour < intensityValue)
                        {
                            CvInvoke.Circle(dest, astrocyteCenter, 3, new MCvScalar(108, 240, 3), 1);
                            //CvInvoke.DrawContours(dest, contours, contourIndex, new MCvScalar(79, 123, 255));
                            totalNumberOfAstrocytes++;
                        }
                        else
                        {
                            ;//CvInvoke.DrawContours(dest, contours, contourIndex, new MCvScalar(255, 12, 36));
                        }
                    }
                }
            }
        }

        public int getNumberOfAstrocytes()
        {
            return totalNumberOfAstrocytes;
        }

        private void drawLayerDelimiters(ref Mat src)
        {
            var dest = src.ToImage<Bgr, Byte>();
            Point leftPoint = new Point(0, 484);
            Point rightPoint = new Point(src.Width - 1, 484);
            LineSegment2D line = new LineSegment2D(leftPoint, rightPoint);
            dest.Draw(line, new Bgr(Color.BlueViolet), 2);

            leftPoint = new Point(0, 1265);
            rightPoint = new Point(src.Width - 1, 1265);
            line = new LineSegment2D(leftPoint, rightPoint);
            dest.Draw(line, new Bgr(Color.BlueViolet), 2);

            leftPoint = new Point(0, 1561);
            rightPoint = new Point(src.Width - 1, 1561);
            line = new LineSegment2D(leftPoint, rightPoint);
            dest.Draw(line, new Bgr(Color.BlueViolet), 2);

            leftPoint = new Point(0, 2276);
            rightPoint = new Point(src.Width - 1, 2276);
            line = new LineSegment2D(leftPoint, rightPoint);
            dest.Draw(line, new Bgr(Color.BlueViolet), 2);

            leftPoint = new Point(0, 3203);
            rightPoint = new Point(src.Width - 1, 3203);
            line = new LineSegment2D(leftPoint, rightPoint);
            dest.Draw(line, new Bgr(Color.BlueViolet), 2);
            src = dest.Mat;
        }
    }
}