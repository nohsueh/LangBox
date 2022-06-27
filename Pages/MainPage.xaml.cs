using LangBox.Operaters;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private static string GitHubPath = "https://github.com/NOhsueh/LangBox";
        private static ConfigHelper cfg = new ConfigHelper();
        private BackgroundWorker worker;


        public MainPage()
        {
            InitializeComponent();
            InitializeConfig();
        }

        private void InitializeConfig()
        {
            PathInput.Text = cfg.GetFilesPath();

            Dictionary<string, bool> LangMap = cfg.GetLangMap();
            foreach (var item in LangSelect.Children)
            {
                if (item is CheckBox)
                {
                    CheckBox checkBoxItem = (CheckBox)item;
                    checkBoxItem.IsChecked = LangMap[checkBoxItem.Name];
                }
            }

            SpaceRequiredShow();
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
            LangSelect_Update(sender, e);
        }

        //检查勾选的语言，更新LangMap
        private void LangSelect_Update(object sender, RoutedEventArgs e)
        {
            foreach (var item in LangSelect.Children)
            {
                if (item is CheckBox)
                {
                    CheckBox checkBoxItem = (CheckBox)item;
                    string LMkey = checkBoxItem.Name;
                    bool LMvalue = (bool)checkBoxItem.IsChecked;

                    cfg.SetLangMap(LMkey, LMvalue);
                }
            }

            SpaceRequiredShow();
        }

        //改变所需总空间显示
        private void SpaceRequiredShow()
        {
            int TotalSpace = SpaceCounter.SpaceRequired(cfg.GetLangMap());
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
            if (InculdeIllegal(path))
            {
                PathValidity.Text = "The path contains spaces or special symbols.";
                return false;
            }
            else if (!Directory.Exists(path) || path == cfg.GetFilesPath())
            {
                PathValidity.Text = "";
                return true;
            }
            else if (Directory.GetDirectories(path).Length > 0 || Directory.GetFiles(path).Length > 0)
            {
                PathValidity.Text = "The folder is not empty.";
                return false;
            }

            PathValidity.Text = "";
            return true;
        }

        private bool InculdeIllegal(string text)
        {
            Regex regex = new Regex(@"^([a-zA-Z]:\\)([-\u4e00-\u9fa5\w\s.()~!@#$%^&()\[\]{}+=]+\\?)*$");
            if (!regex.Match(text).Success)
                return true;
            return false;
        }

        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            cfg.SetFilesPath(PathInput.Text);

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += StartInstall;
            worker.ProgressChanged += ProgressChanged;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void StartInstall(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkingProgress.Visibility = Visibility.Visible;

                Installer installer = new Installer(cfg.GetLangMap(), cfg.GetFilesPath());
                installer.OnProgressChangeEvent += ProgressChangeSend;
                installer.Start();
            }
            catch (Exception err)
            {
                //发生异常
            }
        }

        // 收到Installer的事件调用，将内容发送给worker
        private void ProgressChangeSend(string text)
        {
            worker.ReportProgress(0, text);
        }

        //worker处理进度
        private void ProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            WorkingWith.Text = args.UserState as string;
            WorkingProgress.Value = 50;
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            //完成或取消或异常后的工作
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool b)
        {
            worker.Dispose();
            this.Dispose(b);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
            }
        }
    }
}
