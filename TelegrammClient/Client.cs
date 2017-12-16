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

        public Client(Repository _repo) //конструктор класса, удаляет игры при запуске, подключается к Telegramm
        {
            Repo = _repo;
            Repo.DeleteGames();
            string token = Properties.Resources.Token;
            client = new TelegramBotClient(token);
            client.OnMessage += MessageProcessor;
        }

        public void Start() //начинает прием сообщений
        {
            client.StartReceiving();
            //
        }

        public void Stop() //прекращает прием сообщений
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
                case Telegram.Bot.Types.Enums.MessageType.TextMessage: //if message type - text
                    TextProcessor(e.Message);
                    break;
                default:
                    //if type of message would be not text
                    client.SendTextMessageAsync(e.Message.Chat.Id, string.Format("Don't understand you, I don't know {0}", e.Message.Type));
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
            if (Repo.GetGame(msg.Chat.Id).Id == -1)//если игры не найдет в бд, то вернет -1
            {
                CommandProcessor(msg, "start");
            }
            else
            {
                if (msg.Text.Substring(0, 1) == "/") //commands in Telegramm starts with "/"
                    CommandProcessor(msg, msg.Text.Substring(1));
                else //normal game
                {
                    int pushed_button;
                    if (!int.TryParse(msg.Text.Split('.')[0], out pushed_button)) //проверка на int введенного пользователем
                    {
                        client.SendTextMessageAsync(msg.Chat.Id, "Please, send valid variant"); //если не nt
                        return;
                    } //eto norm cod
                    if (pushed_button != -1)
                    {
                        Answered(msg, pushed_button); //метод обработки полученного от пользователя ответа
                        return;
                    }
                    client.SendTextMessageAsync(msg.Chat.Id, "Please, send valid variant");
                }
            }
        }

        private void CommandProcessor(Telegram.Bot.Types.Message msg, string command) //метод работы с командами(можно добавлять поддержку команд)
        {
            if (command == "start" && Repo.GetGame(msg.Chat.Id).Id == -1) //start the game
            {
                string location_desc = Repo.StartGame(msg.Chat.Id);
                List<string> buttons = new List<string>();
                var answer_1 = Repo.GetAnswer(1); buttons.Add(answer_1.Id.ToString() + ". " + answer_1.Description);
                var answer_2 = Repo.GetAnswer(2); buttons.Add(answer_2.Id.ToString() + ". " + answer_2.Description);
                var answer_3 = Repo.GetAnswer(3); buttons.Add(answer_3.Id.ToString() + ". " + "lol");//answer_3.Description);
                Telegram.Bot.Types.ReplyMarkups.IReplyMarkup returned_markup = Buttons(msg, buttons);
                client.SendTextMessageAsync(msg.Chat.Id, location_desc); //send postDescription
                client.SendTextMessageAsync(msg.Chat.Id, "What do you choose?", replyMarkup: returned_markup);
            }
        }

        public void Answered(Telegram.Bot.Types.Message msg, int buttonId) //Метод, формирующий ответ пользователю
        {
            List<string> buttons = new List<string>();
            string new_location_desc = Repo.AnswerRecieved(msg.Chat.Id, buttonId, out buttons);
            Telegram.Bot.Types.ReplyMarkups.IReplyMarkup returned_markup = Buttons(msg, buttons);
            client.SendTextMessageAsync(msg.Chat.Id, new_location_desc); //send postDescription
            client.SendTextMessageAsync(msg.Chat.Id, "What do you choose?", replyMarkup: returned_markup);

        }

        public Telegram.Bot.Types.ReplyMarkups.IReplyMarkup Buttons(Telegram.Bot.Types.Message msg, List<string> buttons) //метод, формирующий разметку кнопок для пользователя
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
