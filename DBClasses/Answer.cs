﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClasses
{
    public class Answer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Location ToLocation { get; set; }
        public string PostDescrption { get; set; }

    }
}