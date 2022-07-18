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

namespace LangBox.Forms
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        private static string GitHubPath = "https://github.com/NOhsueh/LangBox";
        private static ConfigHelper cfg = new ConfigHelper();
        private BackgroundWorker worker;
        ProgressList pl = new ProgressList();
        Dictionary<string, bool> LangMap;

        public MainPage()
        {
            InitializeComponent();
            InitializeConfig();
        }

        private void InitializeConfig()
        {
            PathInput.Text = cfg.GetFilesPath();

            LangMap = cfg.GetLangMap();
            foreach (var item in LangSelect.Children)
            {
                if (item is CheckBox)
                {
                    CheckBox checkBoxItem = (CheckBox)item;
                    checkBoxItem.IsChecked = LangMap[checkBoxItem.Name];
                }
            }

            pl.SetProgressList(LangMap);
            LangInfoShow();
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

            pl.SetProgressList(LangMap);

            LangInfoShow();
        }

        //改变跟选择语言有关的显示
        private void LangInfoShow()
        {
            //右上文本内容
            WorkingWith.Text = pl.AllText();

            if (WorkingProgress.Visibility != Visibility.Hidden)
            {
                WorkingProgress.Visibility = Visibility.Hidden;
            }

            //右下总空间
            double TotalSpace = SpaceCounter.SpaceRequired(LangMap);
            SpaceRequired.Text = TotalSpace.ToString() + "MB 空间需要";
        }

        //private bool IsSelectAll()
        //{
        //    foreach (var item in LangSelect.Children)
        //    {
        //        if (item is CheckBox)
        //        {
        //            CheckBox checkBoxItem = (CheckBox)item;
        //            if(checkBoxItem.IsChecked == false)
        //            {
        //                return false;
        //            }
        //        }
        //    }

        //    return true;
        //}

        //点击浏览文件
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.SelectedPath = PathInput.Text;
            dialog.ShowDialog();
            PathInput.Text = dialog.SelectedPath;
        }

        //改变左下文本框内容
        private void PathInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PathCheck(PathInput.Text))
            {
                ModifyButton.IsEnabled = true;
                SelectedPath.Text = "安装位置：" + PathInput.Text;
            }
            else
            {
                ModifyButton.IsEnabled = false;
                SelectedPath.Text = "路径错误";
            }
        }

        private bool PathCheck(string path)
        {
            if (InculdeIllegal(path))
            {
                PathValidity.Text = "路径包含非法字符。";
                return false;
            }
            else if (!Directory.Exists(path) || path == cfg.GetFilesPath())
            {
                PathValidity.Text = "";
                return true;
            }
            else if (Directory.GetDirectories(path).Length > 0 || Directory.GetFiles(path).Length > 0)
            {
                PathValidity.Text = "目录非空";
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

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            //配置
            cfg.SetFilesPath(PathInput.Text);
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
            WorkingProgress.Value = 0;
            WorkingProgress.Visibility = Visibility.Visible;
            ModifyButton.IsEnabled = false;
            PathInput.IsEnabled = false;
            PathInputButton.IsEnabled = false;
            SelectAll.IsEnabled = false;
            foreach (var item in LangSelect.Children)
            {
                if (item is CheckBox)
                {
                    CheckBox checkBoxItem = (CheckBox)item;
                    checkBoxItem.IsEnabled = false;
                }
            }

            //工作
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += StartModify;
            worker.ProgressChanged += ProgressChanged;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync();
        }

        //worker处理的事情
        private void StartModify(object sender, DoWorkEventArgs args)
        {
            try
            {
                ManageHelper manager = new ManageHelper(LangMap, cfg.GetFilesPath());
                manager.OnProgressChangeEvent += ProgressChangeSend;
                manager.Start();
            }
            catch (Exception err)
            {
                worker.CancelAsync();
            }
        }

        // 收到Installer的事件调用，将内容发送给worker
        private void ProgressChangeSend(int percentProgress)
        {
            worker.ReportProgress(0);
        }

        //worker处理进度
        private void ProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            WorkingProgress.Value+=25;
        }

        //worker完成或停止
        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            WorkingProgress.Value = 100;
            WorkingWith.Text = "Modified successfully!";
            ModifyButton.IsEnabled = true;
            PathInput.IsEnabled = true;
            PathInputButton.IsEnabled = true;
            SelectAll.IsEnabled = true;
            foreach (var item in LangSelect.Children)
            {
                if (item is CheckBox)
                {
                    CheckBox checkBoxItem = (CheckBox)item;
                    checkBoxItem.IsEnabled = true;
                }
            }
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
