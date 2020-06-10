using System;
using System.Collections.Generic;

namespace CarApi5.Models
{
    public partial class Cars
    {
        public long Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Secret { get; set; }
    }
}
