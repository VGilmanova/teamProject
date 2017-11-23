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
            client.OnMessage += MessageProcessor;
        }

        public void Start()
        {
            client.StartReceiving();
        }

        public void Stop()
        {
            client.StopReceiving();
        }

        private void MessageProcessor(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
                client.SendTextMessageAsync(e.Message.Chat.Id, "Получил сообщение");
                switch (e.Message.Type)
                {
                    case Telegram.Bot.Types.Enums.MessageType.TextMessage:
                        TextProcessor(e.Message);
                        break;
                    default:
                        client.SendTextMessageAsync(e.Message.Chat.Id, string.Format("Не понимаю о чем ты, не знаю формата {0}", e.Message.Type));
                        break;
                }
        }

        private void TextProcessor(Telegram.Bot.Types.Message msg)
        {
            if (msg.Text.Substring(0, 1) == "/" || Repo.GetGame(msg.Chat.Id).Id == -1) //command starts with "/"
                CommandProcessor(msg, msg.Text.Substring(1));
            else //answer with string
            {
                Game game = Repo.GetGame(msg.Chat.Id);
            }

        }

        private void CommandProcessor(Telegram.Bot.Types.Message msg, string command)
        {
            if (command == "start" || Repo.GetGame(msg.Chat.Id).Id == -1)
            {
                Repo.AddGame(new Game(msg.Chat.Id, Repo.GetLocation(1))); //Location with id = 1 - location from the game starts
                client.SendTextMessageAsync(msg.Chat.Id, "Игра началась!");
            }
        }

        public void Show(Telegram.Bot.Types.Message msg, Location location)
        {
            client.SendTextMessageAsync(msg.Chat.Id, location.Description); //send description
            //send buttons
            //add to this game's log new key - location
        }

        public void Answered(Telegram.Bot.Types.Message msg, int buttonId)
        {
            string postDescription = Repo.GetAnswer(buttonId).PostDescrption;
            client.SendTextMessageAsync(msg.Chat.Id, postDescription); //send postDescription
            int new_location_id = Repo.GetLocation(Repo.GetAnswer(buttonId).ToLocation.Id).Id;
            Repo.ChangeLocation(msg.Chat.Id, new_location_id);
            Show(msg, Repo.GetLocation(new_location_id));
            //done1
        }

    }
}
