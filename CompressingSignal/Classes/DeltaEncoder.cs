namespace CompressingSignal.Classes
{
    public class DeltaEncoder
    {
        public WaveFile Encode(WaveFile input)
        {
            short lastLeft = 0;
            short lastRight = 0;
            short tempLeft;
            short tempRight;

            WaveFile output = new WaveFile()
            {
                RiffId = input.RiffId,
                FileSize = input.FileSize,
                WaveId = input.WaveId,
                FmtId = input.FmtId,
                FmtSize = input.FmtSize,
                Format = input.Format,
                Channels = input.Channels,
                SamplingRate = input.SamplingRate,
                BytePerSec = input.BytePerSec,
                BlockSize = input.BlockSize,
                BitPerSampling = input.BitPerSampling,
                DataId = input.DataId,
                DataSize = input.DataSize,
                RightData = input.RightData,
                LeftData = input.LeftData
            };

            for (long i = 0; i < output.LeftData.Length; ++i)
            {      
                tempLeft = output.LeftData[i];
                output.LeftData[i] -= lastLeft;
                lastLeft = tempLeft;

                tempRight = output.RightData[i];
                output.RightData[i] -= lastRight;
                lastRight = tempRight;
            }
            return output;
        }

        public WaveFile Decode(WaveFile input)
        {
            WaveFile output = new WaveFile()
            {
                RiffId = input.RiffId,
                FileSize = input.FileSize,
                WaveId = input.WaveId,
                FmtId = input.FmtId,
                FmtSize = input.FmtSize,
                Format = input.Format,
                Channels = input.Channels,
                SamplingRate = input.SamplingRate,
                BytePerSec = input.BytePerSec,
                BlockSize = input.BlockSize,
                BitPerSampling = input.BitPerSampling,
                DataId = input.DataId,
                DataSize = input.DataSize,
                RightData = input.RightData,
                LeftData = input.LeftData
            };

            short lastLeft = 0;
            short lastRight = 0;
            for (long i = 0; i < output.LeftData.Length; ++i)
            {
                output.LeftData[i] += lastLeft;
                lastLeft = output.LeftData[i];

                output.RightData[i] += lastRight;
                lastRight = output.RightData[i];
            }

            return output;
        }
    }
}