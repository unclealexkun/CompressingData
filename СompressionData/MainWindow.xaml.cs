using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
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
            imageCompress.Source = imageOriginal.Source;
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
                
            }
        }
    }
}
