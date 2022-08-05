using LangBox.Connections;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace LangBox.Forms
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = PgsqlHelper.ExecuteQuery("SELECT password FROM public.user WHERE username='" + txtEmail.Text + "';");
            string password;
            if (dt.Rows.Count > 0)
            {
                password = dt.Rows[0]["password"].ToString().Trim();
            }
            else
            {
                MessageBox.Show("请输入正确的账号密码");
                return;
            }

            if (txtPassword.Password==password)
            {
                Application.Current.MainWindow.Content = new MainPage();
            }
            else
            {
                MessageBox.Show("请输入正确的账号密码");
                return;
            }
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
