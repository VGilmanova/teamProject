using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClasses
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Answer> AnswersToThisLocation { get; set; }
        public Answer Answer_1 { get; set; }
        public Answer Answer_2 { get; set; }
        public Answer Answer_3 { get; set; }
        public Answer Answer_4 { get; set; }
        public List<Game> Games { get; set; }
    }
}
