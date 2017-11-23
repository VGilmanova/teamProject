using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClasses
{
    public class Game
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public TimeSpan WorkTime { get; set; }

        /// <summary>
        /// Json-String like OrderDictionary/Dictionary of key: id location int and value: answer string 
        /// </summary>
        public string Log { get; set; } 

        public Location Location { get; set; }

        public Game(long chatId)
        {
            ChatId = chatId;
            DateTime now = DateTime.Now;
            WorkTime = new TimeSpan(now.Hour, now.Minute, now.Second);
        }
        public Game(int id)
        {
            Id = id;
        }

        public Game()
        {

        }
    }
}
