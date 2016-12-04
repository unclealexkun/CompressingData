using System.Drawing;
using СompressionData.Enumes;
using СompressionData.Interfaces;

namespace СompressionData.Classes
{
    public class Compression:ICompression
    {
        public byte[] Compressing(Bitmap data, Method method)
        {
            switch (method)
            {
                case Method.VectorQuantization:
                    {
                        return null;
                    }
                    break;
                default: return null;
            }
        }

        public Bitmap Decompressing(byte[] data, Method method) 
        {
            switch (method)
            {
                case Method.VectorQuantization:
                    {
                        return null;
                    }
                    break;
                default: return null;
            }
        }
    }
} 