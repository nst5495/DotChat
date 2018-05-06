using ChatClient.WebService;
using Domain;
using Domain.ViewClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaktionslogik für AddDialogue.xaml
    /// </summary>
    public partial class AddDialogue : Window
    {
        private ObservableCollection<UserAccount> users = new ObservableCollection<UserAccount>();

        public AddDialogue()
        {
            InitializeComponent();
            MemberListView.ItemsSource = users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserAccount acc = WebServiceProvider.getInstance().GetUserForName(UsernameTB.Text);
            if (acc != null)
            {
                ErrorLBL.Content = "";
                users.Add(acc);
            }
            else
            {
                ErrorLBL.Content = Strings.AddDialogue.Errors.UserDoesNotExist;
            }
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if(users.Count != 0)
            {
                List<int> members = (from u in users select u.Id).ToList();
                WebServiceProvider.getInstance().AddChatToUser(members.ToArray(), CurrentUser.GetCurrentUser().Id, TitleTB.Text);
                Close();
            }
        }
    }
}
