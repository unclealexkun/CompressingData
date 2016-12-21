using System.Drawing;
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
            }
        }
    }
} 