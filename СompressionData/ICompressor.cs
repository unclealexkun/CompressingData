namespace СompressionData
{
    public interface ICompressor
    {
        byte Compression(byte data);
        byte Decompression(byte data);
    }
}