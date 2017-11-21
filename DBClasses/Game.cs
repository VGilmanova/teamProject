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
        public int ChatId { get; set; }
        public TimeSpan WorkTime { get; set; }

        /// <summary>
        /// Json-String like OrderDictionary/Dictionary of key: id location int and value: answer string 
        /// </summary>
        public string Log { get; set; } 

        public Location Locaton { get; set; }
    }
}
