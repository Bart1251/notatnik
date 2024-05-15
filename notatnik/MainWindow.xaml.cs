using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace notatnik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = "";
        bool saved = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            text.Copy();
            Delete();
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            text.Copy();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            text.Paste();
        }

        private void DeleteEv(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            int selectionStart = text.SelectionStart;
            text.Text = text.Text.Remove(selectionStart, text.SelectionLength);
            text.Select(selectionStart, 0);
        }

        
    }
}