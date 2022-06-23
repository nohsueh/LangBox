using LangBox.Operaters;
using Ookii.Dialogs.Wpf;
using System.Collections.Generic;
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
        public static string GitHubPath = "https://github.com/NOhsueh/LangBox";
        public static Dictionary<string, bool> LangMap = new Dictionary<string, bool>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", GitHubPath);
        }

        //全选
        private void SelectAll_Checked(object sender, RoutedEventArgs e)
        {
            bool flag = SelectAll.IsChecked == true ? true : false;
            foreach (var item in LangSelect.Children)
            {
                if (item is CheckBox)
                {
                    CheckBox checkBoxItem = (CheckBox)item;
                    checkBoxItem.IsChecked = flag;
                }
            }
            LangSelect_Click(sender, e);
        }

        //检查勾选的语言
        private void LangSelect_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in LangSelect.Children)
            {
                if (item is CheckBox)
                {
                    CheckBox checkBoxItem = (CheckBox)item;
                    string LMkey = checkBoxItem.Name;
                    bool LMvalue = (bool)checkBoxItem.IsChecked;

                    if (LangMap.ContainsKey(LMkey))
                    {
                        LangMap[LMkey] = LMvalue;
                    }
                    else
                    {
                        LangMap.Add(LMkey, LMvalue);
                    }
                }
            }

            SpaceRequiredShow();
        }

        //改变所需总空间显示
        private void SpaceRequiredShow()
        {
            int TotalSpace = SpaceCounter.SpaceRequired(LangMap);
            SpaceRequired.Text = TotalSpace.ToString() + "MB space required";
        }

        //点击浏览文件
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.SelectedPath = PathInput.Text;
            dialog.ShowDialog();
            PathInput.Text = dialog.SelectedPath;
        }

        //改变文本框内容
        private void PathInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PathCheck(PathInput.Text))
            {
                InstallButton.IsEnabled = true;
                SelectedPath.Text = "Installation Location: " + PathInput.Text;
            }
            else
            {
                InstallButton.IsEnabled = false;
                SelectedPath.Text = "PathError";
            }
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
            Regex regex = new Regex(@"^[^\/\:\*\?\""\<\>\|\,]+$");
            if (regex.Match(text).Success)
                return true;
            return false;
        }

        private void Install_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine(PathInput.Text);
        }
    }
}
