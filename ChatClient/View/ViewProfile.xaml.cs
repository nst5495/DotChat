using ChatClient.WebService;
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
    /// Interaction logic for ViewProfile.xaml
    /// </summary>
    public partial class ViewProfile : Window
    {
        public ViewProfile()
        {
            InitializeComponent();
            InitLabels();
        }

        private void InitLabels()
        {
            UserAccount cuser = CurrentUser.GetCurrentUser();
            UsernameContentLBL.Content = cuser.UserName;
            FirstNameContentLBL.Content = cuser.FirstName;
            LastNameContentLBL.Content = cuser.LastName;
            //@Todo:Profile image
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if(PasswordTB.Password == RepeatPasswordTB.Password)
            {
                if(Regex.IsMatch(PasswordTB.Password,Strings.Register.Regex.Password))
                {
                    //Just use the Login Function to update the CurrentUsers password
                    CurrentUser.Login(WebServiceProvider.getInstance().ChangePassword(CurrentUser.GetCurrentUser().UserName, PasswordTB.Password));
                    Close();
                }
                else
                {
                    ErrorLBL.Content = Strings.Register.Errors.Password;
                }
            }
            else
            {
                ErrorLBL.Content = Strings.Register.Errors.PasswordRep;
            }
        }

        private void PasswordTB_GotFocus(object sender, RoutedEventArgs e)
        {
            TooltipLBL.Content = Strings.Register.Tooltips.Password;
        }

        private void PasswordTB_LostFocus(object sender, RoutedEventArgs e)
        {
            TooltipLBL.Content = "";
        }
    }
}
