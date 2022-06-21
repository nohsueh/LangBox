using Ookii.Dialogs.Wpf;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace LangBox.Pages
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public static string SelectedLangPath { get; set; }
        public static string GitHubPath = "www.github.com";

        public MainPage()
        {
            InitializeComponent();
            PathInput.SetBinding(TextBox.TextProperty, "SelectedLangPath");
            SelectedPath.SetBinding(TextBox.TextProperty, "SelectedLangPath");
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", GitHubPath);
        }

        //全选
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

        //点击浏览
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.SelectedPath = SelectedLangPath;
            dialog.ShowDialog();
            PathInput.Text = dialog.SelectedPath;
            PathInput.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        //改变文本框内容
        private void PathInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PathCheck(PathInput.Text))
            {
                InstallButton.IsEnabled = true;
            }
            else
            {
                InstallButton.IsEnabled = false;
            }
            SelectedPath.Text = "安装位置："+PathInput.Text;
        }

        private bool PathCheck(string path)
        {
            if (!Directory.Exists(path))
            {
                PathValidity.Text = "The path does not exist.";
                return false;
            }

            if (InculdeIllegal(path))
            {
                PathValidity.Text = "The path contains spaces or special symbols.";
                return false;
            }

            PathValidity.Text = "";
            return true;
        }

        private bool InculdeIllegal(string text)
        {
            Regex regex = new Regex(@"[^a-zA-Z0-9:_\\]");
            if (regex.Match(text).Success)
                return true;
            return false;
        }
    }
}
