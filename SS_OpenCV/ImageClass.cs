using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using ZedGraph;
using System.Diagnostics.Contracts;

namespace CG_OpenCV
{
    class ImageClass
    {

        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int step = m.widthStep;
                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            blue = (dataPtr+x*nChan+y*step)[0];
                            green = (dataPtr + x * nChan + y * step)[1];
                            red = (dataPtr + x * nChan + y * step)[2];

                            (dataPtr + x * nChan + y * step)[0] = (byte)(255 - blue);
                            (dataPtr + x * nChan + y * step)[1] = (byte)(255 - green);
                            (dataPtr + x * nChan + y * step)[2] = (byte)(255 - red);


                        }

                       
                    }
                }
            }
        }
        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int step = m.widthStep;
                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            blue = (byte)(int)Math.Round((dataPtr + nChan * x + step * y)[0] * contrast + bright);
                            green = (byte)(int)Math.Round((dataPtr + nChan * x + step * y)[1] * contrast + bright);
                            red = (byte)(int)Math.Round((dataPtr + nChan * x + step * y)[2] * contrast + bright);

                            if(blue >= 255)
                                blue = 255;
                            if(green >= 255)
                                green = 255;
                            if (red >= 255)
                                red = 255;
                            if (blue < 0)
                                blue = 0;
                            if (green < 0)
                                green = 0;
                            if (red < 0)
                                red = 0;



                            (dataPtr + x * nChan + y * step)[0] = (byte)(blue);
                            (dataPtr + x * nChan + y * step)[1] = (byte)(green);
                            (dataPtr + x * nChan + y * step)[2] = (byte)(red);


                        }


                    }
                }
            }
        }

        private static int pixel_modificado(int x, int y)
        {
            throw new NotImplementedException();
        }

        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
               
                int step = m.widthStep;
                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            blue = (dataPtr + x * nChan + y * step)[0];
                            green = (dataPtr + x * nChan + y * step)[1];
                            red = (dataPtr + x * nChan + y * step)[2];

                            (dataPtr + x * nChan + y * step)[0] = red;
                            (dataPtr + x * nChan + y * step)[1] = red;
 


                        }


                    }
                }
            }
        }

        /// <summary>
        /// Convert to gray
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }


    }
}
