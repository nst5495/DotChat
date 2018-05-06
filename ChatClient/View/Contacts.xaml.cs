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
using System.Timers;
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
        public ObservableCollection<ViewChat> chatlist;
        public Contacts()
        {
            InitializeComponent();
            InitProfileInfo();
            InitChatList();
            StatusTB.Text = CurrentUser.GetCurrentUser().StatusMessage;
            var timer = new System.Timers.Timer(15000);
            timer.Elapsed += UpdateChats;
            timer.Start();

            
            if(CurrentUser.chats.FirstOrDefault() != null)
            {
                ChatsList.SelectedItem = CurrentUser.chats.FirstOrDefault();
            }
            
            
            
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
            chatlist = new ObservableCollection<ViewChat>(CurrentUser.chats);
            ChatsList.ItemsSource = chatlist;
        }

        private void ViewProfileBTN_Click(object sender, RoutedEventArgs e)
        {
            ViewProfile prof = new ViewProfile();
            prof.Show();
            prof.Focus();
        }

        private void ChatsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ChatsList.Items.Count == 0)
            {
                return;
            }
            ChatViewer.Document = InitDoc((((ViewChat)ChatsList.SelectedItem).Messages));
            string participants = "";
            foreach(UserAccount uc in ((ViewChat)ChatsList.SelectedItem).Members)
            {
                participants += uc.UserName + ",";
            }
            participants.Remove(participants.Length - 1);
            ParticipantsLBL.Text = participants;
            if (WebServiceProvider.getInstance().UserIsAdmin(((ViewChat)ChatsList.SelectedItem).Id, CurrentUser.GetCurrentUser().Id))
            {
                DeleteBTN.IsEnabled = true;
            }
            else
            {
                DeleteBTN.IsEnabled = false;
            }
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

        private void UpdateChats(object sender, EventArgs e)
        {

            List<Chat> chats = WebServiceProvider.getInstance().CheckForNewChats(CurrentUser.chats.LastOrDefault().Id, CurrentUser.GetCurrentUser().Id);
            if (chats.Count > 0)
                {
                    foreach (Chat c in chats)
                    {
                        ViewChat vc = new ViewChat();
                        vc.Id = c.Id;
                        vc.Members = WebServiceProvider.getInstance().GetMembersForChat(c.Id);
                        vc.Messages = new List<ViewMessage>();
                        vc.Title = c.Title;
                        CurrentUser.chats.Add(vc);
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
                    newmsgs = WebServiceProvider.getInstance().CheckForNewMessages(DateTime.MinValue, c.Id);
                }
                if (newmsgs != null)
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
                Dispatcher.Invoke(() =>UpdateChatsAndMessages());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(MessageTB.Text.Length > 0)
            {
                WebServiceProvider.getInstance().AddMessage(((ViewChat)ChatsList.SelectedItem).Id, MessageTB.Text, CurrentUser.GetCurrentUser().Id);
            }
            //We dont actually use the Event related parameters at all so this should be fine
            UpdateChats(null,null);
            UpdateChatsAndMessages();
        }

        private void UpdateChatsAndMessages()
        {
            chatlist.Clear();
            foreach(ViewChat c in CurrentUser.chats)
            {
                chatlist.Add(c);
            }
            if(CurrentUser.chats.Count > 0)
            {
                if(ChatsList.SelectedItem != null)
                {
                    ChatViewer.Document = InitDoc((((ViewChat)ChatsList.SelectedItem).Messages));
                }
                else
                {
                    ChatsList.SelectedItem = chatlist.FirstOrDefault();
                    ChatViewer.Document = InitDoc((((ViewChat)ChatsList.SelectedItem).Messages));
                }
            }

        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show(Strings.Common.Common.YesorNo,"",System.Windows.MessageBoxButton.YesNo);
            if(mbr == MessageBoxResult.Yes)
            {
                WebServiceProvider.getInstance().DeleteChat(((ViewChat)ChatsList.SelectedItem).Id);
                CurrentUser.chats.Remove((ViewChat)ChatsList.SelectedItem);
                UpdateChatsAndMessages();
            }
        }

        private void UpdateStatusBTN_Click(object sender, RoutedEventArgs e)
        {
            WebServiceProvider.getInstance().UpdateStatus(CurrentUser.GetCurrentUser().Id, StatusTB.Text);
        }
    }
}
