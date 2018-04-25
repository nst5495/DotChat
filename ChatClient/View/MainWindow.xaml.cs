using ChatClient.View;
using ChatClient.WebService;
using DataConnection;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
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
using WebApplication1.Controllers;

namespace ChatClient
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
            InitializeComponent();
            //Check for Remember me
            if(File.Exists(Environment.SpecialFolder.ApplicationData + Strings.Login.IOInfo.RememberMePath))
            {
                using (var sr = new StreamReader(Environment.SpecialFolder.ApplicationData + Strings.Login.IOInfo.RememberMePath))
                {
                    UsernameTB.Text = sr.ReadLine();
                    PasswordTB.Password = sr.ReadLine();
                }
            }
        }

        private void LoginBT_Click(object sender, RoutedEventArgs e)
        {
            UserAccount user = WebServiceProvider.getInstance().Login(UsernameTB.Text, PasswordTB.Password);
            if (user != null)
            {
                CurrentUser.Login(user);
                if((bool)RememberCB.IsChecked)
                {
                    Directory.CreateDirectory(Environment.SpecialFolder.ApplicationData + Strings.Login.IOInfo.RememberMeFolder);
                    using (File.Create(Environment.SpecialFolder.ApplicationData + Strings.Login.IOInfo.RememberMePath))
                    {

                    }
                    using (var sw = new StreamWriter(Environment.SpecialFolder.ApplicationData + Strings.Login.IOInfo.RememberMePath))
                    {
                        sw.WriteLine(CurrentUser.GetCurrentUser().UserName);
                        sw.WriteLine(CurrentUser.GetCurrentUser().Password);
                    }

                }
                else
                {
                    if(File.Exists(Environment.SpecialFolder.ApplicationData + Strings.Login.IOInfo.RememberMePath))
                    {
                        File.Delete(Environment.SpecialFolder.ApplicationData + Strings.Login.IOInfo.RememberMePath);
                    }
                }
                Contacts con = new Contacts();
                con.Show();
                this.Close();
      
            }
            else
            {
                ErrorLBL.Content = "Account not found!";
                
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Close();
        }
    }
}
