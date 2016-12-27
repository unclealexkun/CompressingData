using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CompressingSignal.Classes;

namespace CompressingSignal
{
    public partial class MainForm : Form
    {
        private WaveFile _wav;
        private WaveFile _wavCompress;
        private string _file;
        private bool _press = false;
        public MainForm()
        {
            InitializeComponent();
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = Directory.GetCurrentDirectory();
            open.Filter = "Аудио файлы (*.wav)|*.wav|Все файлы (*.*)|*.*";
            open.FilterIndex = 1;
            open.RestoreDirectory = true;

            if (open.ShowDialog() == DialogResult.OK)
            {
                _file = open.FileName;

                WorkWave work = new WorkWave();
                _wav = work.Reading(open.FileName);

                if (Draw.DrawWave(_wav, chartOriginal))
                {
                    chartOriginal.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                    chartOriginal.MouseWheel += new MouseEventHandler(chData_MouseWheel);
                }
                else
                {
                    chartOriginal.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                    chartOriginal.MouseWheel -= new MouseEventHandler(chData_MouseWheel);
                }
            }
        }

        private void bCompessing_Click(object sender, EventArgs e)
        {
            DeltaEncoder delta = new DeltaEncoder();
            _wavCompress = delta.Encode(_wav);
            if (Draw.DrawWave(_wavCompress, chartCompressing))
            {
                chartCompressing.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                chartCompressing.MouseWheel += new MouseEventHandler(chData_MouseWheel);
            }
            else
            {
                chartCompressing.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                chartCompressing.MouseWheel -= new MouseEventHandler(chData_MouseWheel);
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = Directory.GetCurrentDirectory();
            save.Filter = "Аудио файлы (*.wav)|*.wav|Все файлы (*.*)|*.*";
            save.FilterIndex = 1;
            save.RestoreDirectory = true;

            if (save.ShowDialog() == DialogResult.OK)
            {
                _file = save.FileName;

                WorkWave work = new WorkWave();
                work.Writing(save.FileName, _wavCompress);
            }
        }

        private void chData_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta <= 0)
                {
                    chartOriginal.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    chartOriginal.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }
                else
                {
                    double xMin = chartOriginal.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = chartOriginal.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = chartOriginal.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = chartOriginal.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = chartOriginal.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 2;
                    double posXFinish = chartOriginal.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) +
                                        (xMax - xMin) / 2;
                    double posYStart = chartOriginal.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 2;
                    double posYFinish = chartOriginal.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) +
                                        (yMax - yMin) / 2;

                    chartOriginal.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    chartOriginal.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
