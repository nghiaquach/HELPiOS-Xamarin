using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HELPiOS
{
    public class Lecturer
    {
        public int id { get; set; }
        public string staffID { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public bool inactive { get; set; }
		public string archived { get; set; }
    }
}