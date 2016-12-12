﻿using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
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
                            picture[vectorsCounter].X.R = r;
                            picture[vectorsCounter].X.G = g;
                            picture[vectorsCounter].X.B = b;
                            picture[vectorsCounter].X.Alpha = alpha;
                            xDone = true;
                        }
                        else if (!yDone)
                             {
                                 picture[vectorsCounter].Y.R = r;
                                 picture[vectorsCounter].Y.G = g;
                                 picture[vectorsCounter].Y.B = b;
                                 picture[vectorsCounter].Y.Alpha = alpha;
                                 xDone = false;
                                 yDone = false;
                                 picture.Add(new Vector());
                                 vectorsCounter++;
                             }
                    }
                }

                var sum = new Vector();

                foreach (Vector pic in picture)
                {
                    sum.X.Alpha += pic.X.Alpha;
                    sum.X.R += pic.X.R;
                    sum.X.G += pic.X.G;
                    sum.X.B += pic.X.B;
                    sum.Y.Alpha += pic.Y.Alpha;
                    sum.Y.R += pic.Y.R;
                    sum.Y.G += pic.Y.G;
                    sum.Y.B += pic.Y.Alpha;
                }

                var firstAvg = new Vector
                {
                    X = new Pixel()
                    {
                        Alpha = (float)Math.Round(sum.X.Alpha / picture.Count),
                        R = (float)Math.Round(sum.X.R / picture.Count),
                        G = (float)Math.Round(sum.X.G / picture.Count),
                        B = (float)Math.Round(sum.X.B / picture.Count)
                    },
                    Y = new Pixel()
                    {
                        Alpha = (float)Math.Round(sum.Y.Alpha / picture.Count),
                        R = (float)Math.Round(sum.Y.R / picture.Count),
                        G = (float)Math.Round(sum.Y.G / picture.Count),
                        B = (float)Math.Round(sum.Y.B / picture.Count)
                    }
                };

                var levels = new List<Level>
                {
                    new Level()
                };
                levels[0].Avgs.Add(new Avg(firstAvg.X, firstAvg.Y));
                levels[0].Avgs[0].Elements = picture;

                for (int i = 1; i < numberBit; i++)
                {
                    levels.Add(new Level());
                    int c = 0;

                    for (int j = 0; j < levels[i-1].Avgs.Count; j++)
                    {
                        var left = new Vector();
                        var right = new Vector();

                        var leftElements = new List<Vector>();
                        var rightElements = new List<Vector>();

                        left.X.R = levels[i - 1].Avgs[j].X.R - 1;
                        left.X.G = levels[i - 1].Avgs[j].X.G - 1;
                        left.X.B = levels[i - 1].Avgs[j].X.B - 1;
                        left.X.Alpha = levels[i - 1].Avgs[j].X.Alpha - 1;
                        left.Y.R = levels[i - 1].Avgs[j].Y.R - 1;
                        left.Y.G = levels[i - 1].Avgs[j].Y.G - 1;
                        left.Y.B = levels[i - 1].Avgs[j].Y.B - 1;
                        left.Y.Alpha = levels[i - 1].Avgs[j].Y.Alpha - 1;
                        right.X.Alpha = levels[i - 1].Avgs[j].X.Alpha + 1;
                        right.X.R = levels[i - 1].Avgs[j].X.R + 1;
                        right.X.G = levels[i - 1].Avgs[j].X.G + 1;
                        right.X.B = levels[i - 1].Avgs[j].X.B + 1;
                        right.Y.Alpha = levels[i - 1].Avgs[j].Y.Alpha + 1;
                        right.Y.R = levels[i - 1].Avgs[j].Y.R + 1;
                        right.Y.G = levels[i - 1].Avgs[j].Y.G + 1;
                        right.Y.B = levels[i - 1].Avgs[j].Y.B + 1;

                        var sum1 = new Vector();
                        var sum2 = new Vector();
                        for (int k = 0; k < levels[i - 1].Avgs[j].Elements.Count; k++)
                        {
                            float change1, change2;
                            change1 = (levels[i - 1].Avgs[j].Elements[k].X.R - left.X.R) * (levels[i - 1].Avgs[j].Elements[k].X.R - left.X.R)
                                    + (levels[i - 1].Avgs[j].Elements[k].Y.R - left.Y.R) * (levels[i - 1].Avgs[j].Elements[k].Y.R - left.Y.R);
                            change2 = (levels[i - 1].Avgs[j].Elements[k].X.R - right.X.R) * (levels[i - 1].Avgs[j].Elements[k].X.R - right.X.R)
                                    + (levels[i - 1].Avgs[j].Elements[k].Y.R - right.Y.R) * (levels[i - 1].Avgs[j].Elements[k].Y.R - right.Y.R);

                            if (change1 >= change2)
                            {
                                rightElements.Add(levels[i - 1].Avgs[j].Elements[k]);
                            }
                            else leftElements.Add(levels[i - 1].Avgs[j].Elements[k]);
                        }

                        for (int z = 0; z < leftElements.Count; z++)
                        {
                            sum1.X.Alpha += leftElements[z].X.Alpha;
                            sum1.X.R += leftElements[z].X.R;
                            sum1.X.G += leftElements[z].X.G;
                            sum1.X.B += leftElements[z].X.B;
                            sum1.Y.Alpha += leftElements[z].Y.Alpha;
                            sum1.Y.R += leftElements[z].Y.R;
                            sum1.Y.G += leftElements[z].Y.G;
                            sum1.Y.B += leftElements[z].Y.B;
                        }

                        for (int z = 0; z < rightElements.Count; z++)
                        {
                            sum2.X.Alpha += rightElements[z].X.Alpha;
                            sum2.X.R += rightElements[z].X.R;
                            sum2.X.G += rightElements[z].X.G;
                            sum2.X.B += rightElements[z].X.B;
                            sum2.Y.Alpha += rightElements[z].Y.Alpha;
                            sum2.Y.R += rightElements[z].Y.R;
                            sum2.Y.G += rightElements[z].Y.G;
                            sum2.Y.B += rightElements[z].Y.B;
                        }

                        var avg = new Avg()
                        {
                            X = new Pixel()
                            {
                                Alpha = (float)Math.Round(sum1.X.Alpha / leftElements.Count),
                                R = (float)Math.Round(sum1.X.R / leftElements.Count),
                                G = (float)Math.Round(sum1.X.G / leftElements.Count),
                                B = (float)Math.Round(sum1.X.B / leftElements.Count)
                            },
                            Y = new Pixel()
                            {
                                Alpha = (float)Math.Round(sum1.Y.Alpha / leftElements.Count),
                                R = (float)Math.Round(sum1.Y.R / leftElements.Count),
                                G = (float)Math.Round(sum1.Y.G / leftElements.Count),
                                B = (float)Math.Round(sum1.Y.B / leftElements.Count)
                            }
                        };
                        levels[i].Avgs.Add(avg);
                        levels[i].Avgs[c].Elements = leftElements;

                        c++;

                        avg = new Avg()
                        {
                            X = new Pixel()
                            {
                                Alpha = (float)Math.Round(sum2.X.Alpha / rightElements.Count),
                                R = (float)Math.Round(sum2.X.R / rightElements.Count),
                                G = (float)Math.Round(sum2.X.G / rightElements.Count),
                                B = (float)Math.Round(sum2.X.B / rightElements.Count)
                            },
                            Y = new Pixel()
                            {
                                Alpha = (float)Math.Round(sum2.Y.Alpha / rightElements.Count),
                                R = (float)Math.Round(sum2.Y.R / rightElements.Count),
                                G = (float)Math.Round(sum2.Y.G / rightElements.Count),
                                B = (float)Math.Round(sum2.Y.B / rightElements.Count)
                            }
                        };
                        levels[i].Avgs.Add(avg);
                        levels[i].Avgs[c].Elements = rightElements;

                        c++;
                    }

                    int currentLevel = i;

                    if (i == numberBit-1)
                    {
                        // Оптимизация
                        for (;;)
                        {
                            levels.Add(new Level());
                            currentLevel++;

                            // Добавление в уровень средних значений
                            for (int l = 0; l < levels[currentLevel - 1].Avgs.Count; l++)
                            {
                                var summ = new Avg();
                                for (int b = 0; b < levels[currentLevel - 1].Avgs[l].Elements.Count; b++)
                                {
                                    summ.X.Alpha += levels[currentLevel - 1].Avgs[l].Elements[b].X.Alpha;
                                    summ.X.R += levels[currentLevel - 1].Avgs[l].Elements[b].X.R;
                                    summ.X.G += levels[currentLevel - 1].Avgs[l].Elements[b].X.G;
                                    summ.X.B += levels[currentLevel - 1].Avgs[l].Elements[b].X.B;
                                    summ.Y.Alpha += levels[currentLevel - 1].Avgs[l].Elements[b].Y.Alpha;
                                    summ.Y.R += levels[currentLevel - 1].Avgs[l].Elements[b].Y.R;
                                    summ.Y.G += levels[currentLevel - 1].Avgs[l].Elements[b].Y.G;
                                    summ.Y.B += levels[currentLevel - 1].Avgs[l].Elements[b].Y.B;
                                }

                                Avg avg = new Avg()
                                {
                                    X = new Pixel()
                                    {
                                        Alpha =
                                            (float)
                                                Math.Round(summ.X.Alpha/levels[currentLevel - 1].Avgs[l].Elements.Count),
                                        R = (float) Math.Round(summ.X.R/levels[currentLevel - 1].Avgs[l].Elements.Count),
                                        G = (float) Math.Round(summ.X.G/levels[currentLevel - 1].Avgs[l].Elements.Count),
                                        B = (float) Math.Round(summ.X.B/levels[currentLevel - 1].Avgs[l].Elements.Count)
                                    },
                                    Y = new Pixel()
                                    {
                                        Alpha =
                                            (float)
                                                Math.Round(summ.Y.Alpha/levels[currentLevel - 1].Avgs[l].Elements.Count),
                                        R = (float) Math.Round(summ.Y.R/levels[currentLevel - 1].Avgs[l].Elements.Count),
                                        G = (float) Math.Round(summ.Y.G/levels[currentLevel - 1].Avgs[l].Elements.Count),
                                        B = (float) Math.Round(summ.Y.B/levels[currentLevel - 1].Avgs[l].Elements.Count)
                                    }
                                };

                                levels[currentLevel].Avgs.Add(avg);
                            }

                            // Получение векторов
                            float changee = 0;
                            var bestError = 0;
                            for (var b = 0; b < picture.Count; b++)
                            {
                                for (var l = 0; l < levels[currentLevel].Avgs.Count; l++)
                                {
                                    if (l == 0)
                                    {
                                        changee = (picture[b].X.R - levels[currentLevel].Avgs[l].X.R) * (picture[b].X.R - levels[currentLevel].Avgs[l].X.R)
                                                + (picture[b].Y.R - levels[currentLevel].Avgs[l].Y.R) * (picture[b].Y.R - levels[currentLevel].Avgs[l].Y.R);
                                        bestError = 0;
                                    }
                                    else
                                    {
                                        if (changee > ((picture[b].X.R - levels[currentLevel].Avgs[l].X.R) * (picture[b].X.R - levels[currentLevel].Avgs[l].X.R)
                                                      + (picture[b].Y.R - levels[currentLevel].Avgs[l].Y.R) * (picture[b].Y.R - levels[currentLevel].Avgs[l].Y.R)))
                                        {

                                            changee = ((picture[b].X.R - levels[currentLevel].Avgs[l].X.R) * (picture[b].X.R - levels[currentLevel].Avgs[l].X.R)
                                                      + (picture[b].Y.R - levels[currentLevel].Avgs[l].Y.R) * (picture[b].Y.R - levels[currentLevel].Avgs[l].Y.R));
                                            bestError = l;
                                        }
                                    }
                                }
                                levels[currentLevel].Avgs[bestError].Elements.Add(picture[b]);
                            }

                            // Проверка выполнения алгоритма
                            var same = true;
                            for (var u = 0; u < levels[currentLevel].Avgs.Count; u++)
                            {
                                if (Pixel.Equals(levels[currentLevel].Avgs[u].X, levels[currentLevel - 1].Avgs[u].X) && Pixel.Equals(levels[currentLevel].Avgs[u].Y, levels[currentLevel - 1].Avgs[u].Y))
                                {
                                }
                                else
                                {
                                    same = false;
                                }
                            }

                            if (same)
                            {
                                break;
                            }
                        }

                        using (var writter = new StreamWriter(Directory.GetCurrentDirectory() + @"\\dictionary.txt"))
                        {
                            writter.WriteLine(_width + " " + _height);
                            for (int g = 0; g < levels[levels.Count - 1].Avgs.Count; g++)
                            {
                                writter.WriteLine((int)levels[levels.Count - 1].Avgs[g].X.Alpha + " "+ (int)levels[levels.Count - 1].Avgs[g].X.R + " " + (int)levels[levels.Count - 1].Avgs[g].X.G + " " + (int)levels[levels.Count - 1].Avgs[g].X.B + 
                                    ";" + (int)levels[levels.Count - 1].Avgs[g].Y.Alpha + " " + (int)levels[levels.Count - 1].Avgs[g].Y.R + " " + (int)levels[levels.Count - 1].Avgs[g].Y.G + " " + (int)levels[levels.Count - 1].Avgs[g].Y.B);
                            }
                        }

                        using (var writter = new StreamWriter(Directory.GetCurrentDirectory() + @"\\codeBook.txt"))
                        {
                            for (var m = 0; m < picture.Count; m++)
                            {
                                for (var g = 0; g < levels[levels.Count - 1].Avgs.Count; g++)
                                {
                                    if (levels[levels.Count - 1].Avgs[g].Elements.Contains(picture[m]))
                                    {
                                        writter.Write(g + " ");
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        public Bitmap GetImage()
        {
            try
            {
                string dictionary;
                using (var reader = new StreamReader(Directory.GetCurrentDirectory() + @"\\dictionary.txt"))
                {
                    dictionary = reader.ReadToEnd();
                }

                var elementDictionary = dictionary.Split(new []{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                var parametrImage = elementDictionary[0].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

                var width = System.Convert.ToInt32(parametrImage[0]);
                var height = System.Convert.ToInt32(parametrImage[1]);

                string codeBook;
                using (var reader = new StreamReader(Directory.GetCurrentDirectory() + @"\\codeBook.txt"))
                {
                    codeBook = reader.ReadToEnd();
                }

                var pixels = new Pixel[height,width];
                var scannerCodeBook = new Scanner(codeBook);

                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height-1; y += 2)
                    {
                        int vec = scannerCodeBook.NextInt();
                        var vectorCode = elementDictionary[vec + 1].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        var pixelX = vectorCode[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        var pixelY = vectorCode[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        pixels[y, x] = new Pixel()
                        {
                            Alpha = float.Parse(pixelX[0]),
                            R = float.Parse(pixelX[1]),
                            G = float.Parse(pixelX[2]),
                            B = float.Parse(pixelX[3])
                        };
                        pixels[y + 1,x] = new Pixel()
                        {
                            Alpha = float.Parse(pixelY[0]),
                            R = float.Parse(pixelY[1]),
                            G = float.Parse(pixelY[2]),
                            B = float.Parse(pixelY[3])
                        };
                    }
                }

                var image = new Bitmap(width,height);

                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                        image.SetPixel(x,y, Color.FromArgb((int)pixels[y, x].Alpha, (int)pixels[y, x].R, (int)pixels[y, x].B, (int)pixels[y, x].G));
                    }

                return image;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при рассшифровке файла: {0}",ex);
                return null;
            }        
        }
    }
}