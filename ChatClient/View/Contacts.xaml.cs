using ChatClient.WebService;
using Domain;
using Domain.DBClasses;
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
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(15);
            var timer = new System.Threading.Timer((e) =>
            {
                UpdateChats();
            }, null, startTimeSpan, periodTimeSpan);
            
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
            string participants = "";
            foreach(UserAccount uc in ((ViewChat)ChatsList.SelectedItem).Members)
            {
                participants += uc.UserName + ",";
            }
            participants.Remove(participants.Length - 1);
            ParticipantsLBL.Text = participants;
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

        private void UpdateChats()
        {
            List<Chat> newchats = WebServiceProvider.getInstance().CheckForNewChats(CurrentUser.chats.OrderByDescending(x => x.Id).FirstOrDefault().Id, CurrentUser.GetCurrentUser().Id);
            if (newchats.Count != 0)
            {
                foreach(Chat c in newchats)
                {
                    ViewChat vc = new ViewChat();
                    vc.Id = c.Id;
                    vc.Members = WebServiceProvider.getInstance().GetMembersForChat(c.Id);
                    vc.Messages = new List<ViewMessage>();
                    vc.Title = c.Title;
                }
            }
            foreach (ViewChat c in CurrentUser.chats)
            {
                List<Chat_Message> newmsgs = new List<Chat_Message>();
                if (c.Messages.Count > 0)
                {
                    newmsgs = WebServiceProvider.getInstance().CheckForNewMessages(c.Messages.LastOrDefault().TimeStamp, c.Id);
                }
                else
                {
                    //TODO: Fix this query
                    newmsgs = WebServiceProvider.getInstance().CheckForNewMessages(DateTime.MinValue, c.Id);
                }
                if (newmsgs.Count > 0)
                {
                    foreach (Chat_Message cm in newmsgs)
                    {
                        ViewMessage vm = new ViewMessage
                        {
                            Chat = CurrentUser.chats.Where(x => x.Id == cm.Chatid).FirstOrDefault(),
                            Id = cm.Id
                        };
                        vm.Sender = vm.Chat.Members.Where(x => x.Id == cm.Senderid).FirstOrDefault();
                        vm.Message = cm.Message;
                        vm.TimeStamp = cm.Timestamp;
                        CurrentUser.chats.Where(x => x == vm.Chat).FirstOrDefault().Messages.Add(vm);
                    }
                }
            }
        }
    }
}
