using System;
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
                            picture[vectorsCounter].X = r;
                            xDone = true;
                        }
                        else if (!yDone)
                             {
                                 picture[vectorsCounter].Y = r;
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
                    sum.X += pic.X;
                    sum.Y += pic.Y;
                }

                var firstAvg = new Vector
                {
                    X = (float) Math.Round(sum.X/picture.Count),
                    Y = (float) Math.Round(sum.Y/picture.Count)
                };

                var levels = new List<Level>
                {
                    new Level()
                };
                levels[0].avgs.Add(new Avg(firstAvg.X, firstAvg.Y));
                levels[0].avgs[0].Elements = picture;

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

                        left.X = levels[i - 1].avgs[j].X - 1;
                        left.Y = levels[i - 1].avgs[j].Y - 1;
                        right.X = levels[i - 1].avgs[j].X + 1;
                        right.Y = levels[i - 1].avgs[j].Y + 1;

                        var sum1 = new Vector();
                        var sum2 = new Vector();
                        for (int k = 0; k < levels[i - 1].avgs[j].Elements.Count; k++)
                        {
                            float change1, change2;
                            change1 = (levels[i - 1].avgs[j].Elements[k].X - left.X) * (levels[i - 1].avgs[j].Elements[k].X - left.X)
                                    + (levels[i - 1].avgs[j].Elements[k].Y - left.Y) * (levels[i - 1].avgs[j].Elements[k].Y - left.Y);
                            change2 = (levels[i - 1].avgs[j].Elements[k].X - right.X) * (levels[i - 1].avgs[j].Elements[k].X - right.X)
                                    + (levels[i - 1].avgs[j].Elements[k].Y - right.Y) * (levels[i - 1].avgs[j].Elements[k].Y - right.Y);

                            if (change1 >= change2)
                            {
                                rightElements.Add(levels[i - 1].avgs[j].Elements[k]);
                            }
                            else leftElements.Add(levels[i - 1].avgs[j].Elements[k]);
                        }

                        for (int z = 0; z < leftElements.Count; z++)
                        {
                            sum1.X += leftElements[z].X;
                            sum1.Y += leftElements[z].Y;
                        }

                        for (int z = 0; z < rightElements.Count; z++)
                        {
                            sum2.X += rightElements[z].X;
                            sum2.Y += rightElements[z].Y;
                        }

                        levels[i].avgs.Add(new Avg((float) Math.Round(sum1.X / leftElements.Count), (float) Math.Round(sum1.Y / leftElements.Count)));
                        levels[i].avgs[c].Elements = leftElements;

                        c++;

                        levels[i].avgs.Add(new Avg((float) Math.Round(sum2.X / rightElements.Count), (float) Math.Round(sum2.Y / rightElements.Count)));
                        levels[i].avgs[c].Elements = rightElements;

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
                            for (int l = 0; l < levels[currentLevel - 1].avgs.Count; l++)
                            {
                                var summ = new Avg();
                                for (int b = 0; b < levels[currentLevel - 1].avgs[l].Elements.Count; b++)
                                {
                                    summ.X += levels[currentLevel - 1].avgs[l].Elements[b].X;
                                    summ.Y += levels[currentLevel - 1].avgs[l].Elements[b].Y;
                                }
                                levels[currentLevel].avgs.Add(new Avg((float) Math.Round(summ.X / levels[currentLevel - 1].avgs[l].Elements.Count), (float) Math.Round(summ.Y / levels[currentLevel - 1].avgs[l].Elements.Count)));
                            }

                            // Получение векторов
                            float changee = 0;
                            var bestError = 0;
                            for (var b = 0; b < picture.Count; b++)
                            {
                                for (var l = 0; l < levels[currentLevel].avgs.Count; l++)
                                {
                                    if (l == 0)
                                    {
                                        changee = (picture[b].X - levels[currentLevel].avgs[l].X) * (picture[b].X - levels[currentLevel].avgs[l].X)
                                                + (picture[b].Y - levels[currentLevel].avgs[l].Y) * (picture[b].Y - levels[currentLevel].avgs[l].Y);
                                        bestError = 0;
                                    }
                                    else
                                    {
                                        if (changee > ((picture[b].X - levels[currentLevel].avgs[l].X) * (picture[b].X - levels[currentLevel].avgs[l].X)
                                                      + (picture[b].Y - levels[currentLevel].avgs[l].Y) * (picture[b].Y - levels[currentLevel].avgs[l].Y)))
                                        {

                                            changee = ((picture[b].X - levels[currentLevel].avgs[l].X) * (picture[b].X - levels[currentLevel].avgs[l].X)
                                                      + (picture[b].Y - levels[currentLevel].avgs[l].Y) * (picture[b].Y - levels[currentLevel].avgs[l].Y));
                                            bestError = l;
                                        }
                                    }
                                }
                                levels[currentLevel].avgs[bestError].Elements.Add(picture[b]);
                            }

                            // Проверка выполнения алгоритма
                            var same = true;
                            for (var u = 0; u < levels[currentLevel].avgs.Count; u++)
                            {
                                if (levels[currentLevel].avgs[u].X == levels[currentLevel - 1].avgs[u].X && levels[currentLevel].avgs[u].Y == levels[currentLevel - 1].avgs[u].Y)
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
                            for (int g = 0; g < levels[levels.Count - 1].avgs.Count; g++)
                            {
                                writter.WriteLine((int)levels[levels.Count - 1].avgs[g].X + " " + (int)levels[levels.Count - 1].avgs[g].Y);
                            }
                        }

                        using (var writter = new StreamWriter(Directory.GetCurrentDirectory() + @"\\codeBook.txt"))
                        {
                            for (var m = 0; m < picture.Count; m++)
                            {
                                for (var g = 0; g < levels[levels.Count - 1].avgs.Count; g++)
                                {
                                    if (levels[levels.Count - 1].avgs[g].Elements.Contains(picture[m]))
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

                var pixels = new int[height,width];
                var scannerCodeBook = new Scanner(codeBook);

                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height-1; y += 2)
                    {
                        int vec = scannerCodeBook.NextInt();
                        var vectorCode = elementDictionary[vec + 1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        pixels[y,x] = Int32.Parse(vectorCode[0]);
                        pixels[y + 1,x] = Int32.Parse(vectorCode[1]);
                    }
                }

                var image = new Bitmap(width,height);

                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                        image.SetPixel(x,y, Color.FromArgb((pixels[y,x] << 16) | (pixels[y,x] << 8) | (pixels[y,x])));
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