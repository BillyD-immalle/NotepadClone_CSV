using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Persoon> personen = new List<Persoon>();
        private string currentFile = "";
        private string initialDir;

        public MainWindow()
        {
            InitializeComponent();          
        }

        private void exitItem_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void openItem_Click(object sender, RoutedEventArgs e)
        {            
            StreamReader inputStream;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = initialDir;
            if (dialog.ShowDialog() == true)
            {
                currentFile = dialog.FileName;
                inputStream = File.OpenText(currentFile);
                fileContents.Text = inputStream.ReadToEnd();
                inputStream.Close();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            {
                List<Persoon> parsedPersonen = new List<Persoon>();

                string[] filedata = fileContents.Text.Split('\n');
                try
                {
                    foreach (var row in filedata)
                    {
                        string[] fields = row.Split(';');
                        var p = new Persoon();
                        p.Voornaam = fields[0];
                        p.Achternaam = fields[1];
                        p.GeboorteDatum = DateTime.Parse(fields[2]);
                        parsedPersonen.Add(p);
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }
                people.ItemsSource = parsedPersonen;
            }
        }
        private void ShowPersonenList_Click(object sender, RoutedEventArgs e)
        {
            string s = "";

            foreach (var p in personen)
            {
                s += p.ToString();
                s += Environment.NewLine;
            }

            MessageBox.Show(s);
        }
    }
}
