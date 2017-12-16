using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClasses
{
    public class Repository
    {
        public int LogNumber;

        private class Log //У каждой игры - список логов, то есть история игры: куда ходил, что выбирал
                          //можно использовать позже для результатах, основанных на предыдущем выборе
        {
            public int LogId { get; set; }
            public int Location_Id { get; set; }
            public int Answer_Id { get; set; }

            public Log(int LogNumber)
            {
                LogId = LogNumber;
            }
        }

        public Repository() //конструктор репозитория, начинает цепочку id дл логов
        {
            LogNumber = 0;
        }

        public void AddGame(Game game)
        {
            using (Context context = new Context())
            {
                context.Entry(game).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }

        public int GetActivePlayers()
        {
            using (Context context = new Context())
                return context.Games.Count();
        }

        public Answer GetAnswer(int answer_id)
        {
            using (Context context = new Context())
                return context.Answers.First(a => a.Id == answer_id);
        }

        public Location GetLocation(int location_id)
        {
            Location location = new Location();
            using (Context context = new Context())
            {
                location = context.Location.First(a => a.Id == location_id);
            }
            return location;
        }

        public Location GetLocation(long game_chat_id)
        {
            Location location = new Location();
            Game game = GetGame(game_chat_id);
            List<Log> log = JsonConvert.DeserializeObject<List<Log>>(game.Log);
            location = GetLocation(log.Last().Location_Id);
            return location;
        }


        public Game GetGame(long game_chat_id) //ищет игру по id чата с пользователем, если не нашел - -1
        {
            using (Context context = new Context())
            {
                Game returngame = new Game();
                foreach (Game game in context.Games)
                {
                    if (game.ChatId == game_chat_id)
                    {
                        returngame = context.Games.First(a => a.ChatId == game_chat_id);
                        return returngame;
                    }
                }
                returngame = new Game(-1); //if not found returns -1
                return returngame;
            }

        }

        public void DeleteGames() //удаление всех игр и логов из бд при открытии приложения
        {
            using (Context context = new Context())
            {
                foreach (var game in context.Games)
                {
                    context.Entry(game).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChangesAsync();
                }
            }
        }

        public string StartGame(long game_chat_id) //начинает игру
                                                     // ищет игру по chat.id в бд, создает для нее лог, сериализует его и записывает игру в бд
        {
            using (Context context = new Context())
            {
                var new_game = new Game(game_chat_id); // игра с таким то chat_id
                var start_location = context.Location.First(a => a.Id == 1);//у первой локации id = 1
                List<Log> new_log = new List<Log>();
                LogNumber += 1;
                Log lg = new Log(LogNumber); //написать конструктор чтобы ID сделать
                lg.Location_Id = start_location.Id;
                new_log.Add(lg);
                string jsoned_log = JsonConvert.SerializeObject(new_log);
                new_game.Log = jsoned_log;
                AddGame(new_game);
                return start_location.Description;
            }
        }

        public int CheckAnswer(long game_chat_id, string message_text)
        {
            //проверка ответа на корректность: целый ли он, соответствует ли вообще списоку возможных ответов на текущей локации
            using (Context context = new Context())
            {
                int users_answer;
                Game this_game = GetGame(game_chat_id);
                Location current_location = GetLocation(game_chat_id);
                var answers = current_location.Ints.Split(',');
                List<int> ints = new List<int>();
                foreach (var answer in answers)
                    ints.Add(int.Parse(answer));
                if (!int.TryParse(message_text, out users_answer))
                    return -1; //if answer is not int
                else
                    if (ints.Exists(a => a == users_answer))
                    return users_answer; //returns int answer
                return -1; //if it is int but not valid
            }
        }

        public string AnswerRecieved(long game_chat_id, int new_answer_id, out List<string> buttons)
        {
            //1 метод принятия ответа от пользователя
            //ищет игру по ее chat.id, десериализует лог, записывает выбранный пользователем ответ, сериализует лог и фиксирует изменения в бд
            //Ищет ответ по его Id, затем вызывает метод ChangeLocaton, из него - описание следующей локации и кноки
            //возвращает описание локаций и кнопки
            using (Context context = new Context())
            {
                string post_description = "";
                var game = context.Games.First(a => a.ChatId == game_chat_id);
                List<Log> log = JsonConvert.DeserializeObject<List<Log>>(game.Log);
                log.Last().Answer_Id = new_answer_id;//последняя запись в логе
                game.Log = JsonConvert.SerializeObject(log);//we fix user's answer
                context.Entry(game).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                buttons = new List<string>();
                context.Entry(game).State = System.Data.Entity.EntityState.Modified; //load to db
                Answer chosed_answer = GetAnswer(new_answer_id);
                post_description = ChangedLocation(game_chat_id, int.Parse(chosed_answer.PostDescrption), out buttons);
                if (GetAnswer(new_answer_id).Description == "EndGame") // not endgame
                {
                    post_description = "Thanks for playing. Wnat to restart?";
                }
                return post_description;
            }
        }

        public string ChangedLocation(long game_chat_id, int new_location_id, out List<string> buttons_list)
        {
            //2 метод обработки сообщений
            //ищет игру, десериализует лог, добавляет в него новую запись с новой локацией, сериализует лог, фиксирует изменения
            //вызывает метод Buttons
            //возвращает описие новой локации и список кнопок
            using (Context context = new Context())
            {
                string return_description = "";
                var game = context.Games.First(a => a.ChatId == game_chat_id);
                List<Log> log = JsonConvert.DeserializeObject<List<Log>>(game.Log);
                LogNumber += 1;
                Log newLog = new Log(LogNumber) { Location_Id = new_location_id, Answer_Id = -1 };
                log.Add(newLog); // -1 - no answer, default
                game.Log = JsonConvert.SerializeObject(log);
                context.Entry(game).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                string new_location_name = context.Location.First(a => a.Id == new_location_id).Name;
                var checkgame = context.Games.First(a => a.ChatId == game_chat_id);
                buttons_list = new List<string>();
                context.Entry(game).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                buttons_list = ShowButtons(game_chat_id);//здесь проверку игры в контэксте
                return_description = context.Location.First(a => a.Id == new_location_id).Description;
                return return_description;
            }
        }

        public List<string> ShowButtons(long game_chat_id)
        {
            //3 метод обработки сообщений
            //ищет игру, десериализует лог, получает id последней локации
            //получает id ответов для этой локации через десериализацию Ints
            //итерируетс по всем ответам, добавляет в список их строковое представление (id. описание)
            using (Context context = new Context())
            {
                Game checkgame1 = context.Games.First(aaa => aaa.ChatId == game_chat_id);
                List<string> buttons = new List<string>();
                var game = context.Games.First(a => a.ChatId == game_chat_id); //здесь лог БЕЗ ДОБАВЛЕНИЯ
                List<Log> log = JsonConvert.DeserializeObject<List<Log>>(game.Log);
                int check = log[log.Count() - 1].Location_Id;
                Location current_loc = context.Location.First(a => a.Id == check);
                var answers_ids = current_loc.Ints.Split(','); //здесь ошибка
                Game checkgame2 = context.Games.First(aaa => aaa.ChatId == game_chat_id);
                foreach (var answ in answers_ids)
                {
                    int answ_id = int.Parse(answ);
                    var found_answ = context.Answers.First(a => a.Id == answ_id);// теперь здесь
                    buttons.Add(found_answ.Id.ToString() + ". " + found_answ.Description);
                }
                return buttons;
            }
        }





    }
}
