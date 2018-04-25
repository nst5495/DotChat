using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatClient.View
{
    /// <summary>
    /// Interaktionslogik für Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            UserAccount user = new UserAccount();
            if (Regex.Match(NameTB.Text, Strings.Register.Regex.Name).Success)
            {
                user.FirstName = NameTB.Text;
            }
            else
            {
                error += Strings.Register.Errors.FirstName + "\n";
            }
            if(Regex.Match(SurnameTB.Text,Strings.Register.Regex.Name).Success)
            {
                user.LastName = SurnameTB.Text;
            }
            else
            {
                error += Strings.Register.Errors.LastName+ "\n";
            }
            if(Regex.Match(PasswordTB.Text,Strings.Register.Regex.Password).Success)
            {
                user.Password = PasswordTB.Text;
            }
            else
            {
                error += Strings.Register.Errors.Password + "\n";
            }
            if (PasswordTB.Text != RepPasswordTB.Text)
            {
                error += Strings.Register.Errors.PasswordRep + "\n";
            }
            if(Regex.Match(UsernameTB.Text,Strings.Register.Regex.Username).Success)
            {
                user.UserName = UsernameTB.Text;
            }
            else
            {
                error += Strings.Register.Errors.Username + "\n";
            }
            if(String.IsNullOrEmpty(error))
            {
                
                bool userregistered = WebService.WebServiceProvider.getInstance().Register(user);
                if(userregistered)
                {
                    MainWindow win = new MainWindow();
                    win.Show();
                    this.Close();
                }
                else
                {
                    error += Strings.Register.Errors.DuplicateUsername + "\n";
                }
            }
            ErrorLBL.Content = error;

        }

        private void NameTB_OnClick(object sender,RoutedEventArgs e)
        {
        }

        private void SurnameTB_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow log = new MainWindow();
            log.Show();
            this.Close();
        }

        private void NameTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NameTB_GotFocus(object sender, RoutedEventArgs e)
        {
            TooltipLBL.Text = Strings.Register.Tooltips.FirstName;
        }

        private void SurnameTB_GotFocus(object sender, RoutedEventArgs e)
        {
            TooltipLBL.Text = Strings.Register.Tooltips.LastName;
        }

        private void UsernameTB_GotFocus(object sender, RoutedEventArgs e)
        {
            TooltipLBL.Text = Strings.Register.Tooltips.Username;
        }

        private void PasswordTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PasswordTB_GotFocus(object sender, RoutedEventArgs e)
        {
            TooltipLBL.Text = Strings.Register.Tooltips.Password;
        }

        private void RepPasswordTB_GotFocus(object sender, RoutedEventArgs e)
        {
            TooltipLBL.Text = Strings.Register.Tooltips.Password;
        }
    }
}
