using System;
using System.Drawing;
using System.IO;

namespace СompressionData.Classes
{
    public class VectorQuantization
    {
        private Image _imageInput;

        public VectorQuantization(string file)
        {
            _imageInput = null;
            if (File.Exists(file))
            {
                try
                {
                    _imageInput = Image.FromFile(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Ошибка при открытии файла векторным квантованием: {0}", ex);
                }
            }                 
        }


    }
}