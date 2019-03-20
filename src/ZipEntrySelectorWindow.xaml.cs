using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
using System.Windows.Shapes;

namespace BuildLogSeparator
{
    /// <summary>
    /// Interaction logic for ZipEntrySelectorWindow.xaml
    /// </summary>
    public partial class ZipEntrySelectorWindow : Window
    {
        public ZipEntrySelectorWindow()
        {
            InitializeComponent();
        }

        public static bool TryGetContent(ZipArchive archive, out string[] lines)
        {
            var win = new ZipEntrySelectorWindow() { DataContext = new ZipEntrySelectorViewModel(archive) };
            var dr = win.ShowDialog();

            if (dr ?? false)
            {
                var vm = win.DataContext as ZipEntrySelectorViewModel;

                using (var stream = vm.SelectedEntry.Open())
                {
                    using (var tr = new StreamReader(stream))
                    {
                        var tmp = new List<string>();
                        string line;
                        while ((line = tr.ReadLine()) != null)
                        {
                            tmp.Add(line);
                        }

                        lines = tmp.ToArray();
                    }
                }

                return true;
            }
            else
            {
                lines = null;
                return false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
