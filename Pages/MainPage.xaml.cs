using System.Diagnostics;
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
    }
}
