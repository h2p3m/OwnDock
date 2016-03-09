using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OwnDock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FileInfo[] Files { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var directoryInfo = new DirectoryInfo(@"c:\Users\Hans-Peter\Desktop\Chrome-Apps-Desktop");
            Files = directoryInfo.GetFiles();

            DataContext = this;
        }

        private void ImagePanel_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            HandleFileOpen(files[0]);
        }

        private void HandleFileOpen(string s)
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_LostFocus(object sender, RoutedEventArgs e)
        {
            //Close();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
                Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Left + (desktopWorkingArea.Width / 2) - (Width / 2);
            Top = desktopWorkingArea.Bottom - Height;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = Pool.SelectedItem as FileInfo;
            var proc = new Process();
            if (selectedItem != null) proc.StartInfo.FileName = selectedItem.FullName;
            proc.Start();
            Process.GetCurrentProcess().Kill();
        }
    }
}
