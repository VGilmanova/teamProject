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

        /// <summary>
        /// Method for handling a message from user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageProcessor(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            //client.SendTextMessageAsync(e.Message.Chat.Id, "Получил сообщение");
            switch (e.Message.Type)
            {
                case Telegram.Bot.Types.Enums.MessageType.TextMessage:
                    TextProcessor(e.Message);
                    break;
                default:
                    //if type of message would be not text
                    client.SendTextMessageAsync(e.Message.Chat.Id, string.Format("Не понимаю о чем ты, не знаю формата {0}", e.Message.Type));
                    break;
            }
        }

        /// <summary>
        /// Method for handling only text messages
        /// </summary>
        /// <param name="msg"></param>
        private void TextProcessor(Telegram.Bot.Types.Message msg)
        {
            //if there is no such a game in the database
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
                    int pushed_button = int.Parse(msg.Text.Split('.')[0]);
                    pushed_button = Repo.CheckAnswer(msg.Chat.Id, msg.Text.Split('.')[0]);                 
                        if (pushed_button != -1)
                        {
                            Answered(msg, pushed_button);
                            return;
                        }
                        client.SendTextMessageAsync(msg.Chat.Id, "Пришлите, пожалуйста, возможный номер ответа");
                    }
                }
        }

        private void CommandProcessor(Telegram.Bot.Types.Message msg, string command)
        {
            if (command == "start" && Repo.GetGame(msg.Chat.Id).Id == -1) //start the game
            {
                Location start_loc = Repo.StartGame(msg.Chat.Id);
                Telegram.Bot.Types.ReplyMarkups.IReplyMarkup returned_markup = Buttons(msg);
                client.SendTextMessageAsync(msg.Chat.Id, start_loc.Description);
                client.SendTextMessageAsync(msg.Chat.Id, "What do you choose?", replyMarkup: returned_markup);
            }
        }

        public void Answered(Telegram.Bot.Types.Message msg, int buttonId)
        {
            List<string> buttons = new List<string>();
            string new_location_desc = Repo.AnswerRecieved(msg.Chat.Id, buttonId, out buttons);
            Telegram.Bot.Types.ReplyMarkups.IReplyMarkup returned_markup = Buttons(msg,buttons);
            client.SendTextMessageAsync(msg.Chat.Id, new_location_desc); //send postDescription
            client.SendTextMessageAsync(msg.Chat.Id, "What do you choose?", replyMarkup: returned_markup);

        }

        public Telegram.Bot.Types.ReplyMarkups.IReplyMarkup Buttons(Telegram.Bot.Types.Message msg, List<string> buttons)
        {
            List<string> buttons_descs = buttons;
            int length = buttons_descs.Count;
            List<Telegram.Bot.Types.KeyboardButton> keys = new List<Telegram.Bot.Types.KeyboardButton>();
            foreach (string buttons_desc in buttons_descs) //like '1. Description...'
            {
                Telegram.Bot.Types.KeyboardButton b = new Telegram.Bot.Types.KeyboardButton(buttons_desc);
                keys.Add(b);
            }
            var markup = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup(keys.ToArray(), true, true);
            return markup;
        }

        public Telegram.Bot.Types.ReplyMarkups.IReplyMarkup Buttons(Telegram.Bot.Types.Message msg)
        {
            List<string> buttons_descs = Repo.ShowButtons(msg.Chat.Id);
            int length = buttons_descs.Count;
            List<Telegram.Bot.Types.KeyboardButton> keys = new List<Telegram.Bot.Types.KeyboardButton>();
            foreach (string buttons_desc in buttons_descs) //like '1. Description...'
            {
                Telegram.Bot.Types.KeyboardButton b = new Telegram.Bot.Types.KeyboardButton(buttons_desc);
                keys.Add(b);
            }
            var markup = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup(keys.ToArray(), true, true);
            return markup;
        }



    }
}
