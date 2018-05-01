using ChatClient.WebService;
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
    /// Interaktionslogik für Contacts.xaml
    /// </summary>
    public partial class Contacts : Window
    {

        public Contacts()
        {
            InitializeComponent();
            InitProfileInfo();
            InitChatList();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddDialogue dia = new AddDialogue();
            dia.Show();
            dia.Focus();
        }

        private void InitProfileInfo()
        {
            NameBigLBL.Content = CurrentUser.GetCurrentUser().UserName;
            NameLBL.Content = CurrentUser.GetCurrentUser().UserName + ":";
        }

        private void InitChatList()
        {
            ChatsList.ItemsSource = CurrentUser.chats;
        }

        private void ViewProfileBTN_Click(object sender, RoutedEventArgs e)
        {
            ViewProfile prof = new ViewProfile();
            prof.Show();
            prof.Focus();
        }

        private void ChatsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChatViewer.Document = InitDoc((((ViewChat)ChatsList.SelectedItem).Messages));
        }

        private FlowDocument InitDoc(List<ViewMessage> vms)
        {
            FlowDocument fd = new FlowDocument();
            fd.FontSize = 20;
            fd.FontFamily = new FontFamily("Calibri");
            fd.LineHeight = 0.75;
            foreach(ViewMessage vm in vms)
            {
                Run name = new Run(vm.Sender.UserName);
                name.FontStyle = FontStyles.Italic;
                fd.Blocks.Add(new Paragraph(new Run(vm.ToString())));
            }
            return fd;

        }
    }
}
