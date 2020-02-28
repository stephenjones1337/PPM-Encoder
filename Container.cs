using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Encode {
     class Container {
        public string String {get;set;}
        public Queue<byte> Queue {get;set;}
        public Container(string str, Queue<byte> que) {
            String = str;
            Queue = que;
        }
        public Container() {

        }
    }
}
