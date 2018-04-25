using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ChatClient.View
{
    /// <summary>
    /// Interaktionslogik für Contacts.xaml
    /// </summary>
    public partial class Contacts : Window
    {

        public Contacts()
        {
            InitializeComponent();
            InitProfileInfo();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InitProfileInfo()
        {
            NameBigLBL.Content = CurrentUser.GetCurrentUser().UserName;
            NameLBL.Content = CurrentUser.GetCurrentUser().UserName + ":";
        }

        private void ViewProfileBTN_Click(object sender, RoutedEventArgs e)
        {
            ViewProfile prof = new ViewProfile();
            prof.Show();
            prof.Focus();
        }
    }
}
