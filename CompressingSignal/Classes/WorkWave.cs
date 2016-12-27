using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CompressingSignal.Classes
{
    public class WorkWave
    {
        public string ErrorInfo { get; private set; }
        public bool IsError { get; private set; }

        public WaveFile Reading(string file)
        {
            WaveFile fileWav = new WaveFile();

            List<short> lDataList = new List<short>();
            List<short> rDataList = new List<short>();

            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        try
                        {
                            fileWav.RiffId = br.ReadBytes(4);
                            fileWav.FileSize = br.ReadUInt32();
                            fileWav.WaveId = br.ReadBytes(4);
                            fileWav.FmtId = br.ReadBytes(4);
                            fileWav.FmtSize = br.ReadUInt32();
                            fileWav.Format = br.ReadUInt16();
                            fileWav.Channels = br.ReadUInt16();
                            fileWav.SamplingRate = br.ReadUInt32();
                            fileWav.BytePerSec = br.ReadUInt32();
                            fileWav.BlockSize = br.ReadUInt16();
                            fileWav.BitPerSampling = br.ReadUInt16();
                            fileWav.DataId = br.ReadBytes(4);
                            fileWav.DataSize = br.ReadUInt32();

                            for (int i = 0; i < fileWav.DataSize / fileWav.BlockSize; i++)
                            {
                                lDataList.Add((short)br.ReadUInt16());
                                rDataList.Add((short)br.ReadUInt16());
                            }
                        }
                        finally
                        {
                            if (br != null)
                            {
                                br.Close();
                            }
                            if (fs != null)
                            {
                                fs.Close();
                            }
                        }
                    }
                }

                fileWav.LeftData = lDataList.ToArray();
                fileWav.RightData = rDataList.ToArray();
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.ToString();
                IsError = true;
                return null;
            }

            return fileWav;
        }

        public void Writing(string newFile, WaveFile newWavFile)
        {
            short[] lNewDataList = newWavFile.LeftData;
            short[] rNewDataList = newWavFile.RightData;

            try
            {
                using (FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        try
                        {
                            bw.Write(newWavFile.RiffId);
                            bw.Write(newWavFile.FileSize);
                            bw.Write(newWavFile.WaveId);
                            bw.Write(newWavFile.FmtId);
                            bw.Write(newWavFile.FmtSize);
                            bw.Write(newWavFile.Format);
                            bw.Write(newWavFile.Channels);
                            bw.Write(newWavFile.SamplingRate);
                            bw.Write(newWavFile.BytePerSec);
                            bw.Write(newWavFile.BlockSize);
                            bw.Write(newWavFile.BitPerSampling);
                            bw.Write(newWavFile.DataId);
                            bw.Write(newWavFile.DataSize);

                            for (int i = 0; i < newWavFile.DataSize / newWavFile.BlockSize; i++)
                            {
                                if (i < lNewDataList.Count())
                                {
                                    bw.Write((ushort)lNewDataList[i]);
                                }
                                else
                                {
                                    bw.Write(0);
                                }

                                if (i < rNewDataList.Count())
                                {
                                    bw.Write((ushort)rNewDataList[i]);
                                }
                                else
                                {
                                    bw.Write(0);
                                }
                            }
                        }
                        finally
                        {
                            if (bw != null)
                            {
                                bw.Close();
                            }
                            if (fs != null)
                            {
                                fs.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.ToString();
                IsError = true;
            }
        }
    }
}