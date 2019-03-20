using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BuildLogSeparator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var sb = new StringBuilder();

            var ex = e.Exception;

            while (ex != null)
            {
                sb.AppendLine($"=== {ex.GetType().Name}");
                sb.AppendLine(ex.Message);
                sb.AppendLine();
                sb.AppendLine(ex.StackTrace); 

                ex = ex.InnerException;

                if (ex != null)
                {
                    sb.AppendLine();
                    sb.AppendLine();
                }
            }

            MessageBox.Show(sb.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true;
        }
    }
}
