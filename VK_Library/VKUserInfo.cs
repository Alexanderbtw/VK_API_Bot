using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK_Library
{
    public class VKUserInfo
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo_100 { get; set; }

        public override string ToString()
        {
            return $"{first_name} {last_name} {id}";
        }

        public string FullName
        {
            get
            {
                return $"{first_name} {last_name}";
            }
        }
    }
}
