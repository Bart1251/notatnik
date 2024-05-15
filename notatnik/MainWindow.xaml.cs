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

        private void OpenEv(object sender, RoutedEventArgs e)
        {
            Open();
        }

        private void Open()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = "txt files (*.txt)|*.txt";
            ofd.Multiselect = false;
            ofd.Title = "Otwórz";
            if (ofd.ShowDialog() == true)
            {
                text.Text = File.ReadAllText(ofd.FileName);
                path = ofd.FileName;
                saved = true;
            }
        }

        private void SaveAsEv(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private bool SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "txt files (*.txt)|*.txt";
            sfd.AddExtension = true;
            sfd.Title = "Zapisz jako";
            if(sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, text.Text);
                path = sfd.FileName;
                saved = true;
                return true;
            }
            return false;
        }

        private void SaveEv(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if(path == "")
            {
                SaveAs();
            }
            else
            {
                File.WriteAllText(path, text.Text);
                saved = true;
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (path != "")
                if(File.ReadAllText(path) != text.Text)
                    saved = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(!saved)
            {
                MessageBoxResult result = MessageBox.Show("Czy chcesz zapisać zmiany w pliku?", "Notatnik", MessageBoxButton.YesNoCancel);
                if(result == MessageBoxResult.Yes)
                {
                    if(path == "")
                    {
                        if(!SaveAs())
                            e.Cancel = true;
                    }
                    else
                    {
                        Save();
                    }
                }
                else if(result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            base.OnClosing(e);
        }

        private void ShortcutCheck(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Key == Key.O)
                {
                    Open();
                    Keyboard.ClearFocus();
                }
                if (e.Key == Key.S)
                {
                    Save();
                    Keyboard.ClearFocus();
                }
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    if (e.Key == Key.S)
                    {
                        SaveAs();
                        Keyboard.ClearFocus();
                    }

            }
        }
    }
}