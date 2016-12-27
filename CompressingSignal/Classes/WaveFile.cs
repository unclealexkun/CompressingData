namespace CompressingSignal.Classes
{
    public class WaveFile
    {
        public byte[] RiffId { get; set; } // Riff
        public uint FileSize { get; set; } // Размер файла -8
        public byte[] WaveId { get; set; } // WAVE
        // Сегмент fmt
        public byte[] FmtId { get; set; } // fmt
        public uint FmtSize { get; set; } // Число байтов FMT фрагмента
        public ushort Format { get; set; } // Формат
        public ushort Channels { get; set; } // Количество каналов
        public uint SamplingRate { get; set; } // Частота дискретизации
        public uint BytePerSec { get; set; } // Скорость передачи данных
        public ushort BlockSize { get; set; } // Размер блока
        public ushort BitPerSampling { get; set; } // Количество квантования бита
        // Блок данных
        public byte[] DataId { get; set; } // Дата
        public uint DataSize { get; set; } // Количество байт данных формы сигнала

        // Музыкальные данные
        public short[] RightData { get; set; }
        public short[] LeftData { get; set; }
    }
}