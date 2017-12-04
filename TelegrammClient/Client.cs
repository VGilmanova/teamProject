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
            string s = e.Message.Text;
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
            if (Repo.GetGame(msg.Chat.Id).Id == -1)
            {
                StartGame(msg);
            }
            else
            {
                if (msg.Text.Substring(0, 1) == "/") //command starts with "/"
                    CommandProcessor(msg, msg.Text.Substring(1));
                else //answer with string
                {
                    Game game = Repo.GetGame(msg.Chat.Id);
                }
            }
        }

        private void StartGame(Telegram.Bot.Types.Message msg)
        {
            Location startlocation = Repo.GetLocation(2);
            Game newGame = new Game(msg.Chat.Id);
            //newGame.Location = startlocation;
            Repo.AddGame(newGame);
            client.SendTextMessageAsync(msg.Chat.Id, string.Format("Игра началась! \n Активных сейчас: {0}", Repo.GetActivePlayers()));
            Game game = Repo.GetGame(msg.Chat.Id);
        }

        private void CommandProcessor(Telegram.Bot.Types.Message msg, string command)
        {
            if (command == "start" && Repo.GetGame(msg.Chat.Id).Id == -1)
            {

                //client.SendTextMessageAsync(msg.Chat.Id, "desc =" + game.Location.Description );
                //Show(msg, Repo.GetGame(msg.Chat.Id).Location);
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
            //int new_location_id = Repo.GetLocation(Repo.GetAnswer(buttonId).ToLocation.Id).Id;
            //Repo.ChangeLocation(msg.Chat.Id, new_location_id);
            //Show(msg, Repo.GetLocation(new_location_id));
            //done1
        }

        

    }
}
