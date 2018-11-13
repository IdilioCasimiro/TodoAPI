using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Models
{
    public class TodoItem
    {
        public int ID { get; set; }

        [MinLength(4)]
        public string Name { get; set; }
        public bool Done { get; set; }
    }
}
