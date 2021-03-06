﻿using System;
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
        public string Ints { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Game> Games { get; set; }
    }
}
