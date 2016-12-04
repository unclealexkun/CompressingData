using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using СompressionData.Classes.Model;

namespace СompressionData.Classes
{
    public class VectorQuantization
    {
        private readonly Bitmap _image;
        private readonly int _width;
        private readonly int _height;

        public VectorQuantization(string file)
        {
            _image = null;
            if (File.Exists(file))
            {
                try
                {
                    _image = new Bitmap(file);

                    _width = _image.Width;
                    _height = _image.Height;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Ошибка при открытии файла векторным квантованием: {0}", ex);
                }
            }                 
        }

        public void Compressing()
        {
            if (_image == null)
            {
                
            }
            else
            {
                var pixels = new int[_height, _width];

                var picture = new List<Vector>();
                picture.Add(new Vector());

                var xDone = false;
                var yDone = false;
                var vectorsCounter = 0;

                for (int x = 0; x < _width; x++)
                {
                    for (int y = 0; y < _height; y++)
                    {
                        var pixel = _image.GetPixel(x, y);

                        var alpha = pixel.A;
                        var r = pixel.R;
                        var g = pixel.G;
                        var b = pixel.B;

                        if (!xDone)
                        {
                            picture[vectorsCounter].x = r;
                            xDone = true;
                        }
                        else if (!yDone)
                             {
                                 picture[vectorsCounter].y = r;
                                 xDone = false;
                                 yDone = false;
                                 picture.Add(new Vector());
                                 vectorsCounter++;
                             }

                        pixels[y, x] = r;
                    }
                }


            }
        }
    }
}