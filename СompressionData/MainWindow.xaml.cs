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
            compressor.Compressing(BitmapToImageSource(imageOriginal.Source));
            imageCompress.Source = BitmapToImageSource(compressor.Decompressing(Method.VectorQuantization));
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
                Filter = @"BMP files(*.bmp) |*.bmp"
            };
            if (saveFileDialog.ShowDialog() == Form.DialogResult.OK)
            {
                var image = BitmapImage2Bitmap(imageCompress.Source);
                
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
    }
}
