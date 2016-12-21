using System.Drawing;
using СompressionData.Enumes;

namespace СompressionData.Interfaces
{
    public interface ICompression
    {
        void Encoding(Bitmap data, Method method);
        Bitmap Decoding(Method method);
    }
}