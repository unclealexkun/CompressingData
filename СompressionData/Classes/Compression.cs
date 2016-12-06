﻿using System.Drawing;
using СompressionData.Enumes;
using СompressionData.Interfaces;

namespace СompressionData.Classes
{
    public class Compression:ICompression
    {
        public Bitmap Decompressing(Method method)
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

        public void Compressing(Bitmap data, Method method)
        {
            switch (method)
            {
                case Method.VectorQuantization:
                    {
                        var comp = new VectorQuantization(data);
                        comp.Compressing(4);
                    }
                    break;
            }
        }
    }
} 