using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClasses
{
    public class Repository
    {
        public void AddGame(Game game)
        {
            using (Context context = new Context())
            {
                context.Entry(game).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }

        public Answer GetAnswer(int answer_id)
        {
            using (Context context = new Context()) 
                return context.Answers.First(a => a.Id == answer_id);
        }

        public Location GetLocation(int location_id)
        {
            using (Context context = new Context())
                return context.Location.First(a => a.Id == location_id);
        }

        public Game GetGame(int game_chat_id)
        {
            using (Context context = new Context())
                return context.Games.First(a => a.ChatId == game_chat_id);
        }

        public void ChangeLocation(int game_chat_id, int new_location_id)
        {
            using (Context context = new Context())
            {
                Location prev_location = (context.Games.First(a => a.ChatId == game_chat_id)).Locaton;
                prev_location.Games.Remove(context.Games.First(a => a.ChatId == game_chat_id));
                context.Games.First(a => a.ChatId== game_chat_id).Locaton = context.Location.First(a => a.Id == new_location_id);
                context.Location.First(a => a.Id == new_location_id).Games.Add(context.Games.First(b => b.ChatId == game_chat_id));
                //context.Games.First(a => a.ChatId == game_chat_id).Log - deserialize JSON string, change it and serialize
                context.Entry(context.Games.First(a => a.ChatId == game_chat_id)).State = System.Data.Entity.EntityState.Modified;
                context.Entry(prev_location).State = System.Data.Entity.EntityState.Modified;
                context.Entry(context.Location.First(a => a.Id == new_location_id)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
