using Microsoft.Bot.Connector.DirectLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BotClientAppXamarin
{
    public partial class MainPage : ContentPage
    {
        private Conversation conversation;

        private DirectLineClient client = new DirectLineClient("STorKRaOUnA.cwA.yJM.hD0kN9beuFy7gQ9ROibb2DFTqbsGRIwfZY49Cqb0Hlg");

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Click(object o, EventArgs e)
        {
            if (conversation == null)
            {
                conversation = await client.Conversations.StartConversationAsync();
            }

            var message = Activity.CreateMessageActivity() as Activity;

            message.From = new ChannelAccount { Id = "chomado" };

            message.Text = inputForm.Text;

            // send message
            var response = await client.Conversations.PostActivityAsync(conversation.ConversationId, message);

            // recieve message
            var returnedMessages = await client.Conversations.GetActivitiesAsync(conversation.ConversationId);

            foreach (var returnedMessage in returnedMessages.Activities)
            {
                Debug.WriteLine(returnedMessage.Text);
            }

            chatList.ItemsSource = returnedMessages.Activities.Select(i => i.Text);
            inputForm.Text = "";
        }
    }
}
