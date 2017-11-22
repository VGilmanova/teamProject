using DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegrammClient
{
    public class Client
    {
        private TelegramBotClient client;
        private Repository Repo { get; set; }

        public Client(Repository _repo)
        {
            Repo = _repo;
            string token = Properties.Resources.Token;
            client = new TelegramBotClient(token);
        }

        public void Start()
        {
            client.StartReceiving();
        }

        public void Stop()
        {
            client.StopReceiving();
        }

        private void MessageProcesor(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            switch (e.Message.Type)
            {
                case Telegram.Bot.Types.Enums.MessageType.TextMessage:
                    TextProcessor(e.Message);
                    break;
            }
        }

        private void TextProcessor(Telegram.Bot.Types.Message msg)
        {
            if (msg.Text.Substring(0, 1) == "/") //command starts with "/"
                CommandProcessor(msg, msg.Text.Substring(1));
            else //answer with string
            {

            }

        }

        private void CommandProcessor(Telegram.Bot.Types.Message msg, string command)
        {

        }

        public void Show(Location location)
        {
            //send description
            //send buttons
            //add to this game's log new key - location
        }

        public void Answered(int chatId, int buttonId)
        {
            string postDescription = Repo.GetAnswer(buttonId).PostDescrption;
            //send postDescription
            int new_location_id = Repo.GetLocation(Repo.GetAnswer(buttonId).ToLocation.Id).Id;
            Repo.ChangeLocation(chatId, new_location_id);
            Show(Repo.GetLocation(new_location_id));
            //done1
        }

    }
}
