using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using СompressionData.Classes;
using СompressionData.Enumes;
using Form = System.Windows.Forms;

namespace СompressionData
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _path;
        private string _directory;
        public MainWindow()
        {
            _path = String.Empty;
            _directory = String.Empty;
            InitializeComponent();
        }

        private void bCompression_Click(object sender, RoutedEventArgs e)
        {
            var compressor = new Compression();
            var bitmap = new Bitmap(_path);

            compressor.Compressing(bitmap, Method.VectorQuantization);
            imageCompress.Source = BitmapToImageSource(compressor.GetImage(Method.VectorQuantization));
        }

        private void bOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Form.OpenFileDialog
            {
                RestoreDirectory = false,
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = @"BMP files(*.bmp) |*.bmp| All files(*.*) |*.*",
                FilterIndex = 0
            };
            if (dialog.ShowDialog() == Form.DialogResult.OK)
            {
                _directory = dialog.InitialDirectory;
                _path = dialog.FileName;
                imageOriginal.Source = new BitmapImage(new Uri(_path));
            }
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Form.SaveFileDialog
            {
                InitialDirectory = _directory,
                Filter = @"PNG files(*.png) |*.png"
            };
            if (saveFileDialog.ShowDialog() == Form.DialogResult.OK)
            {
                SaveToPng(imageCompress,saveFileDialog.FileName);
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (var outStream = new MemoryStream())
            {
                var enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                var bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        void SaveToBmp(FrameworkElement visual, string fileName)
        {
            var encoder = new BmpBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }

        void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }

        void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }
    }
}
