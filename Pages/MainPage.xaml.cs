using Ookii.Dialogs.Wpf;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace LangBox.Pages
{
    /// <summary>
    /// Welcome.xaml 的交互逻辑
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo("explorer.exe", link.NavigateUri.AbsoluteUri));

        }

        private void SelectAll_Checked(object sender, RoutedEventArgs e)
        {
            bool flag = SelectAll.IsChecked == true ? true : false;
            for (int i = 0; i < LangSelect.Children.Count; i++)
            {
                var item = LangSelect.Children[i];
                if (item is CheckBox)
                {
                    CheckBox checkBoxItem = (CheckBox)item;
                    checkBoxItem.IsChecked = flag;
                }
            }
        }

        private bool PathCheck(string path)
        {
            if (!Directory.Exists(path))
            {
                PathError.Text = "路径不存在";
                return false;
            }

            if (InculdeIllegal(path))
            {
                PathError.Text = "路径包含空格或特殊符号";
                return false;
            }

            PathError.Text = "";
            return true;
        }

        private bool InculdeIllegal(string text)
        {
            Regex regex = new Regex(@"[^a-zA-Z0-9:_\\]");
            if (regex.Match(text).Success)
                return true;
            return false;
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.SelectedPath = SelectedGccPath;
            dialog.ShowDialog();
            PathInput.Text = dialog.SelectedPath;
            PathInput.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
