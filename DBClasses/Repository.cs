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
        private class Log
        {
            public int LogId { get; set; }
            public int Location { get; set; }
            public int Answer { get; set; }
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

        public void ChangedLocation(long game_chat_id, int new_location_id)
        {
            using (Context context = new Context())
            {
                var game = context.Games.First(a => a.ChatId == game_chat_id);
                List<Log> log = JsonConvert.DeserializeObject<List<Log>>(game.Log);
                log.Add(new Log() { LogId = log.Count(), Location = new_location_id, Answer = 0 }); // 0 - нет ответа, дефолтный
            }
        }


        public void AnswerRecieved(long game_chat_id, int new_answer_id)
        {
            using (Context context = new Context())
            {
                var game = context.Games.First(a => a.ChatId == game_chat_id);
                List<Log> log = JsonConvert.DeserializeObject<List<Log>>(game.Log);
                log[log.Count() - 1].Answer = new_answer_id;//последняя запись в логе
            }
        }
    }
}
