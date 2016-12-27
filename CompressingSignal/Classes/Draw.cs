using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace CompressingSignal.Classes
{
    public class Draw
    {
        public static bool DrawWave(WaveFile wave, Chart chartcontrol)
        {
            bool isComplited = true;

            if (wave != null)
            {
                // Установка меток оси
                chartcontrol.ChartAreas[0].AxisX.Title = "t";
                chartcontrol.ChartAreas[0].AxisY.Title = "A";
                // Минимальное значение по оси Х, максимальное значение, установки интервала масштабирования
                chartcontrol.ChartAreas[0].AxisX.Minimum = 0;
                chartcontrol.ChartAreas[0].AxisX.Maximum = wave.RightData.Length;
                chartcontrol.ChartAreas[0].AxisX.Interval = wave.SamplingRate;
                // Минимальное значение Y-оси, максимальное значение
                chartcontrol.ChartAreas[0].AxisY.Minimum = wave.RightData.Min();
                chartcontrol.ChartAreas[0].AxisY.Maximum = wave.RightData.Max();

                // Отображение полосы прокрутки
                AxisScaleView sv = chartcontrol.ChartAreas[0].AxisX.ScaleView;
                sv.SmallScrollSize = wave.SamplingRate;
                sv.Position = 1;
                sv.Size = wave.SamplingRate * 10;
                chartcontrol.ChartAreas[0].AxisX.ScaleView = sv;

                AxisScrollBar sb = chartcontrol.ChartAreas[0].AxisX.ScrollBar;
                sb.ButtonStyle = ScrollBarButtonStyles.All;
                sb.IsPositionedInside = true;
                chartcontrol.ChartAreas[0].AxisX.ScrollBar = sb;

                Series series = new Series();
                series.ChartType = SeriesChartType.FastLine;

                for (int i = 0; i < wave.LeftData.Length; i++)
                    series.Points.AddXY(i, wave.RightData[i]);

                chartcontrol.Series.Add(series);

                chartcontrol.Invalidate();
            }
            else isComplited = false;

            return isComplited;
        }
    }
}