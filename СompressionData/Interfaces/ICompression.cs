using System.Drawing;
using СompressionData.Enumes;

namespace СompressionData.Interfaces
{
    public interface ICompression
    {
        byte[] Compressing(Bitmap data, Method method);
        Bitmap Decompressing(byte[] data, Method method);
    }
}