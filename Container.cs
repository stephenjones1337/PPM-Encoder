using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Encode {
     class Container {
        public string Header {get;set;}
        public string AsciiData {get;set;}
        public Queue<byte> ByteData {get;set;}
        public Container(string str, Queue<byte> que, string header) {
            Header = header;
            AsciiData = str;
            ByteData = que;
        }
        public Container() {

        }
    }
}
