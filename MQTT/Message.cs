using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT
{
    class Message
    {
        public String _name { get; set; }
        public String _msg { get; set; }
        public Message(String name, String msg)
        {
            _name = name;
            _msg = msg;
        }

    }
}
