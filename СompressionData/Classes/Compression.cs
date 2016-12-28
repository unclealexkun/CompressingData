using System.Drawing;
using System.IO;
using System.Linq;
using СompressionData.Enumes;
using СompressionData.Interfaces;

namespace СompressionData.Classes
{
    public class Compression:ICompression
    {
        private Bitmap GetImage(Method method)
        {
            switch (method)
            {
                case Method.VectorQuantization:
                    {
                        var comp = new VectorQuantization(null);
                        return comp.GetImage();
                    }
                    break;
                case Method.DiscreteCosineTransform:
                {
                    return new Bitmap(Directory.GetCurrentDirectory() + @"\\cycled.png");
                }
                default: return null;
            }
        }

        public Bitmap Decoding(Method method)
        {
            return GetImage(method);
        }

        public void Encoding(Bitmap data, Method method)
        {
            switch (method)
            {
                case Method.VectorQuantization:
                    {
                        var comp = new VectorQuantization(data);
                        comp.Encoding(4);
                    }
                    break;
                case Method.DiscreteCosineTransform:
                    {
                        Bitmap cycleBitmap = new Bitmap(data.Width, data.Height);
                        Graphics cycleGraphics = Graphics.FromImage(cycleBitmap);
                        Bitmap coeffBitmap = new Bitmap(data.Width, data.Height);
                        Graphics coeffGraphics = Graphics.FromImage(coeffBitmap);

                        DCT d = new DCT(data.Width, data.Height);

                        for (int y = 0; y < data.Height; y++)
                        {
                            for (int x = 0; x < data.Width; x++)
                            {
                                Bitmap sector = new Bitmap(data.Width, data.Height);
                                Graphics g = Graphics.FromImage(sector);

                                Rectangle dest = new Rectangle(0, 0, data.Width, data.Height);
                                Rectangle src = new Rectangle(x * data.Width, y * data.Height, data.Width, data.Height);

                                g.DrawImage(data, dest, src, GraphicsUnit.Pixel);

                                double[][,] values = d.BitmapToMatrices(sector);
                                double[][,] coeffs = d.DCTMatrices(values);

                                coeffGraphics.DrawImage(d.MatricesToBitmap(coeffs, false), src, dest, GraphicsUnit.Pixel);

                                values = d.IDCTMatrices(coeffs);
                                cycleGraphics.DrawImage(d.MatricesToBitmap(values), src, dest, GraphicsUnit.Pixel);
                            }
                        }

                        cycleBitmap.Save(Directory.GetCurrentDirectory() + @"\\cycled.png");
                        coeffBitmap.Save(Directory.GetCurrentDirectory() + @"\\coeffs.png");
                    }
                    break;
            }
        }
    }
} 