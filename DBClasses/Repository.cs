﻿using Newtonsoft.Json;
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

        private class Log
        {
            public int LogId { get; set; }
            public int Location { get; set; }
            public int Answer { get; set; }

            public Log(int LogNumber)
            {
                LogId = LogNumber;
            }
        }

        public Repository()
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
                List<Location> locations = context.Location.ToList();
                location = context.Location.First(a => a.Id == location_id);
            }
            return location;
        }

        public Location GetLocation(long game_chat_id)
        {
            Location location = new Location();
            using (Context context = new Context())
            {
                Game game = GetGame(game_chat_id);
                List<Log> log = JsonConvert.DeserializeObject<List<Log>>(game.Log);
                location = GetLocation(log[log.Count() - 1].Location);
            }
            return location;
        }


        public Game GetGame(long game_chat_id)
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

        public Location StartGame(long game_chat_id)
        {
            using (Context context = new Context())
            {
                var new_game = new Game(game_chat_id); // игра с таким то chat_id
                var start_location = context.Location.First(a => a.Id == 1);//у первой локации id = 1
                List<Log> new_log = new List<Log>();
                LogNumber += 1;
                Log lg = new Log(LogNumber); //написать конструктор чтобы ID сделать
                lg.Location = start_location.Id;
                new_log.Add(lg);
                string jsoned_log = JsonConvert.SerializeObject(new_log);
                new_game.Log = jsoned_log;
                AddGame(new_game);
                return start_location;
            }
        }

        public string ChangedLocation(long game_chat_id, int new_location_id)
        {
            using (Context context = new Context())
            {
                var game = context.Games.First(a => a.ChatId == game_chat_id);
                List<Log> log = JsonConvert.DeserializeObject<List<Log>>(game.Log);
                LogNumber += 1;
                log.Add(new Log(LogNumber) {Location = new_location_id, Answer = -1 }); // -1 - нет ответа, дефолтный
                game.Log = JsonConvert.SerializeObject(log);
                context.Entry(game).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return context.Location.First(a => a.Id == new_location_id).Description;
            }
        }


        public string AnswerRecieved(long game_chat_id, int new_answer_id)
        {
            using (Context context = new Context())
            {
                var game = context.Games.First(a => a.ChatId == game_chat_id);
                List<Log> log = JsonConvert.DeserializeObject<List<Log>>(game.Log);
                log[log.Count() - 1].Answer = new_answer_id;//последняя запись в логе
                game.Log = JsonConvert.SerializeObject(log);
                context.Entry(game).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                Answer chosed_answer = GetAnswer(new_answer_id);
                string post_description = ChangedLocation(game_chat_id,int.Parse(chosed_answer.PostDescrption));
                return post_description;
            }
        }
    }
}
