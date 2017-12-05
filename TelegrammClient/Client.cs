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
            if (Repo.GetGame(msg.Chat.Id).Id == -1)
            {
                CommandProcessor(msg, "start");
            }
            else
            {
                if (msg.Text.Substring(0, 1) == "/") //command starts with "/"
                    CommandProcessor(msg, msg.Text.Substring(1));
                else //normal game
                {
                    //это по идее можно перенести в репозиторий
                    int users_answer;
                    Game this_game = Repo.GetGame(msg.Chat.Id);
                    Location current_location = Repo.GetLocation(msg.Chat.Id);
                    var answers = current_location.AnswersFromLocation;
                    List<int> ints = new List<int>();
                    foreach (Answer answer in answers)
                        ints.Add(answer.Id);
                    if (int.TryParse(msg.Text, out users_answer))
                        Answered(msg, users_answer);
                    else
                    {
                        if (ints.Exists(a => a == users_answer))
                        {
                            client.SendTextMessageAsync(msg.Chat.Id, "Пришлите, пожалуйста, возможный номер ответа");
                            return;
                        }
                        client.SendTextMessageAsync(msg.Chat.Id, "Пришлите, пожалуйста, целочисленный номер ответа");
                    }
                }
            }
        }

        private void CommandProcessor(Telegram.Bot.Types.Message msg, string command)
        {
            if (command == "start" && Repo.GetGame(msg.Chat.Id).Id == -1)
            {
                Location start_loc = Repo.StartGame(msg.Chat.Id);
                Show(msg,start_loc);
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
            string new_location_desc = Repo.AnswerRecieved(msg.Chat.Id, buttonId);
            client.SendTextMessageAsync(msg.Chat.Id, new_location_desc); //send postDescription
            //int new_location_id = Repo.GetLocation(Repo.GetAnswer(buttonId).ToLocation.Id).Id;
            //Repo.ChangeLocation(msg.Chat.Id, new_location_id);
            //Show(msg, Repo.GetLocation(new_location_id));
            //done1
        }

        

    }
}
