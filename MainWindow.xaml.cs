using LangBox.Forms;
using LangBox.Operators;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace LangBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ConfigHelper cfg = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists("Logs"))
            {
                Directory.Delete("Logs", true);
            }


            Title = "LangBox "+cfg.GetVersion();
            Task task = new Task(() =>
            {
                bool hasUpdate = UpdateChecker.HasUpdate();
                if (hasUpdate)
                {
                    MessageBoxResult r = MessageBox.Show("发现最新版: "+UpdateChecker.GetVersion()+"\n是否前往更新？", "更新提示", MessageBoxButton.YesNo);
                    if (r == MessageBoxResult.Yes)
                    {
                        Process.Start("explorer.exe", "https://github.com/NOhsueh/LangBox/releases/latest");
                    }
                }
            });
            task.Start();

            //this.Content = new LoginPage();
            this.Content = new MainPage();
        }
    }
}
