using System;
using System.Drawing;
using System.Threading.Tasks;

namespace СompressionData.Classes
{
    public class DCT
    {
        public DCT(int width, int height)
        {
            Width = width;
            Height = height;
        }

        //Размер всех матриц
        public int Width;
        public int Height;

        private const int normOffset = 128;

        //Включите матрицы DCT в битовом RGB для вывода
        public Bitmap MatricesToBitmap(double[][,] matrices, bool offset = true)
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    double r = matrices[0][x, y];
                    double g = matrices[1][x, y];
                    double b = matrices[2][x, y];

                    byte R = (byte)(normOut(r, offset));
                    byte G = (byte)(normOut(g, offset));
                    byte B = (byte)(normOut(b, offset));
                    bitmap.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return bitmap;
        }

        private double normOut(double a, bool offset)
        {
            double o = offset ? normOffset : 0d;

            return Math.Min(Math.Max(a + o, 0), 255);
        }

        //Создание матрицы из растрового изображения RGB
        public double[][,] BitmapToMatrices(Bitmap b)
        {
            double[][,] matrices = new double[3][,];

            for (int i = 0; i < 3; i++)
            {
                matrices[i] = new double[Width, Height];
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    matrices[0][x, y] = b.GetPixel(x, y).R - normOffset;
                    matrices[1][x, y] = b.GetPixel(x, y).G - normOffset;
                    matrices[2][x, y] = b.GetPixel(x, y).B - normOffset;
                }
            }
            return matrices;
        }

        //Выполнить DCT2D на 3-каналированной группы матриц
        public double[][,] DCTMatrices(double[][,] matrices)
        {
            var outMatrices = new double[3][,];
            Parallel.For(0, 3, i =>
            {
                outMatrices[i] = DCT2D(matrices[i]);
            });
            return outMatrices;
        }

        //Выполнить обратное преобразование DCT2D на 3-каналированной группы матриц
        public double[][,] IDCTMatrices(double[][,] matrices)
        {
            var outMatrices = new double[3][,];
            Parallel.For(0, 3, i =>
            {
                outMatrices[i] = IDCT2D(matrices[i]);
            });
            return outMatrices;
        }

        //Выполнить DCT2D на одной матрице
        public double[,] DCT2D(double[,] input)
        {
            double[,] coeffs = new double[Width, Height];

            //Для того, чтобы инициализировать все значения [и, v] в таблице коэффициентов
            for (int u = 0; u < Width; u++)
            {
                for (int v = 0; v < Height; v++)
                {
                    //Cумма базисной функции для каждого значения [х, у] на входе растрового
                    double sum = 0d;



                    for (int x = 0; x < Width; x++)
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            double a = input[x, y];
                            sum += BasisFunction(a, u, v, x, y);
                        }
                    }
                    coeffs[u, v] = sum * beta * alpha(u) * alpha(v);
                }
            }
            return coeffs;
        }

        //Выполнить обратное преобразование DCT2D на одной матрице
        public double[,] IDCT2D(double[,] coeffs)
        {
            double[,] output = new double[Width, Height];

            //Инициализация каждого значения [х, у] в выходном растровом
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    //Сумма базисной функции для каждого [U, V] значения в таблице коэффициентов
                    double sum = 0d;

                    for (int u = 0; u < Width; u++)
                    {
                        for (int v = 0; v < Height; v++)
                        {
                            double a = coeffs[u, v];
                            sum += BasisFunction(a, u, v, x, y) * alpha(u) * alpha(v);
                        }
                    }

                    output[x, y] = sum * beta;
                }
            }
            return output;
        }

        public double BasisFunction(double a, double u, double v, double x, double y)
        {
            double b = Math.Cos(((2d * x + 1d) * u * Math.PI) / (2 * Width));
            double c = Math.Cos(((2d * y + 1d) * v * Math.PI) / (2 * Height));

            return a * b * c;
        }

        private double alpha(int u)
        {
            if (u == 0)
                return 1 / Math.Sqrt(2);
            return 1;
        }

        //Нормализующее значение
        private double beta
        {
            get { return (1d / Width + 1d / Height); }
        }
    }
}