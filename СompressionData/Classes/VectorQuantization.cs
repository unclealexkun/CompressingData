using System;
using System.Drawing;
using System.Collections.Generic;
using СompressionData.Classes.Model;

namespace СompressionData.Classes
{
    public class VectorQuantization
    {
        private readonly Bitmap _image;
        private readonly int _width;
        private readonly int _height;

        public VectorQuantization(Bitmap image)
        {
            _image = image;
            if (_image == null)
            {
                Console.WriteLine(@"Объект VectorQuantization получил null элемент");
            }
            else
            {
                _width = _image.Width;
                _height = _image.Height;
            }                 
        }

        public void Compressing(int numberBit)
        {
            if (_image == null)
            {
                Console.WriteLine(@"Сжатие не возможно в VectorQuantization получен null элемент");
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

                var sum = new Vector();

                foreach (Vector pic in picture)
                {
                    sum.x += pic.x;
                    sum.y += pic.y;
                }

                var firstAvg = new Vector
                {
                    x = (float) Math.Round(sum.x/picture.Count),
                    y = (float) Math.Round(sum.y/picture.Count)
                };

                var levels = new List<Level>
                {
                    new Level()
                };
                levels[0].avgs.Add(new Avg(firstAvg.x, firstAvg.y));
                levels[0].avgs[0].elements = picture;

                for (int i = 1; i < numberBit; i++)
                {
                    levels.Add(new Level());
                    int c = 0;

                    for (int j = 0; j < levels[i-1].avgs.Count; j++)
                    {
                        var left = new Vector();
                        var right = new Vector();

                        var leftElements = new List<Vector>();
                        var rightElements = new List<Vector>();

                        left.x = levels[i - 1].avgs[j].x - 1;
                        left.y = levels[i - 1].avgs[j].y - 1;
                        right.x = levels[i - 1].avgs[j].x + 1;
                        right.y = levels[i - 1].avgs[j].y + 1;

                        var sum1 = new Vector();
                        var sum2 = new Vector();
                        for (int k = 0; k < levels[i - 1].avgs[j].elements.Count; k++)
                        {
                            float change1 = 0, change2 = 0;
                            change1 = (levels[i - 1].avgs[j].elements[k].x - left.x) * (levels[i - 1].avgs[j].elements[k].x - left.x)
                                    + (levels[i - 1].avgs[j].elements[k].y - left.y) * (levels[i - 1].avgs[j].elements[k].y - left.y);
                            change2 = (levels[i - 1].avgs[j].elements[k].x - right.x) * (levels[i - 1].avgs[j].elements[k].x - right.x)
                                    + (levels[i - 1].avgs[j].elements[k].y - right.y) * (levels[i - 1].avgs[j].elements[k].y - right.y);

                            if (change1 >= change2)
                            {
                                rightElements.Add(levels[i - 1].avgs[j].elements[k]);
                            }
                            else leftElements.Add(levels[i - 1].avgs[j].elements[k]);
                        }

                        for (int z = 0; z < leftElements.Count; z++)
                        {
                            sum1.x += leftElements[z].x;
                            sum1.y += leftElements[z].y;
                        }

                        for (int z = 0; z < rightElements.Count; z++)
                        {
                            sum2.x += rightElements[z].x;
                            sum2.y += rightElements[z].y;
                        }

                        levels[i].avgs.Add(new Avg((float) Math.Round(sum1.x / leftElements.Count), (float) Math.Round(sum1.y / leftElements.Count)));
                        levels[i].avgs[c].elements = leftElements;

                        c++;

                        levels[i].avgs.Add(new Avg((float) Math.Round(sum2.x / rightElements.Count), (float) Math.Round(sum2.y / rightElements.Count)));
                        levels[i].avgs[c].elements = rightElements;

                        c++;
                    }

                    int currentLevel = i;

                }
            }
        }
    }
}