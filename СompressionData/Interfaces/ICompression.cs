using System.Drawing;
using СompressionData.Enumes;

namespace СompressionData.Interfaces
{
    public interface ICompression
    {
        void Compressing(Bitmap data, Method method);
        Bitmap Decompressing(Method method);
    }
}