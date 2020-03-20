using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Encode {
    class Compresser {
        //add RLE compression & decompression(on decode)

        //TODO: compression is optional
        //TODO: ASCII = P7, BINARY = P8
        //TODO: able to read compressed images as well
        //TODO: decoder detects if file is P7 or P8, decompresses, then extracts the message

        //when decompressing bytes, always put at least 1 before the pixel color data, and each grouping.
        //this way, we can copy the run of pixels as well as the colors
        public Compresser() {            
        }
        public Container Compress(Container container) {
            
            if (container.Header[1] == '3') {
                return RLEAscii(container);
            } else if(container.Header[1] == '6') {
                return RLEBytes(container);
            }
            
            return null;
        }
        public Container LzwCompression(Container container) {
            //VARIABLES
            string sub_sequence          = "";
            string current_sequence      = "";
            bool   current_sequence_seen = false;
            int    subsequence_position  = 0;

        //INSTANCES
            Container result = new Container();
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            List<int> lst_encoded_output = new List<int>();

            //build new header

        //BUILD THE INITIAL DICTIONARY --- ENCODE EACH ASCII CHAR AND ITS VALUE TO A SLOT
            for (int index = 0; index < 128; index += 1) {
                    char letter = (char)index;
                    dictionary.Add(letter.ToString(), index);
            }//end for

            string data;
            bool byteData = false;

            if (container.ByteData.Count > 0) {
                StringBuilder decompressed = new StringBuilder();

                while(container.ByteData.Count > 0) {
                    decompressed.Append($"{container.ByteData.Dequeue()}\n");
                }

                data = decompressed.ToString();
                byteData = true;
            } else {
                data = container.AsciiData;
            }

            foreach (char current_letter in data) {
                //BUILD THE CURRENT SEQUENCE
                current_sequence = sub_sequence + current_letter;

                //DETERMINE IF THE CURRENT SEQUENCE IS IN OUR DICTIONARY
                current_sequence_seen = dictionary.ContainsKey(current_sequence);

                if (current_sequence_seen) {
                    sub_sequence = current_sequence; //since we saw this make it the new sub_sequence
                }else{
                    //SINCE WE HAVENT SEEN THIS CURRENT SEQUENCE FIND THE POSITION OF THE SUBSEQUENCE WE KNOW IS IN THE DICTIONARY 
                    subsequence_position = dictionary[sub_sequence];
                    
                    //THEN STORE THAT POSITION INTO OUR ENCODED OUTPUT
                    lst_encoded_output.Add(subsequence_position);

                    //ADD OUR CURRENTLY "UNSEEN" SEQUENCE INTO OUR DICTIONARY
                    dictionary.Add(current_sequence, dictionary.Count);

                    //START TRYING TO BUILD A NEW SEQUENCE FROM THE CURRENT LETTER
                    sub_sequence = current_letter.ToString();
                }//end if
            }//end for

        // IF WE HAVE A REMAINING SUB_SEQUENCE ENCODE IT TOO
            if (sub_sequence.Length > 0){
                lst_encoded_output.Add(dictionary[sub_sequence]);
            }//end if

            if (byteData) {
                result.ByteData = new Queue<byte>();
                foreach(int num in lst_encoded_output) {
                    result.ByteData.Enqueue((byte)num);
                }
                result.AsciiData = "";
            } else {
                //add ascii body
                StringBuilder str = new StringBuilder();
                foreach(int num in lst_encoded_output) {
                    str.Append(num.ToString()+",");
                }

                result.AsciiData = str.ToString();
                result.ByteData = new Queue<byte>();
            }

            result.Header = ChangeHeaderLZW(container, byteData);
            return result;
        }//end function
        private Container RLEAscii(Container container) {
            List<Color> colList = AsciiToColorList(container);
            return CompressListForAscii(colList, container);
        }
        private Container RLEBytes(Container container) {
            List<Color> colList = BytesToColorList(container);
            return CompressListForBytes(colList, container);
        }
        private Container CompressListForAscii(List<Color> pixels, Container container) {
            StringBuilder strBuilder = new StringBuilder();
            int count;

            for (int i = 0; i < pixels.Count; i+=count) {
                count = 0;
                //scan & count each identical pixel from current till break
                for (int j = i; j < pixels.Count; j++) {
                    if (pixels[i] == pixels[j]) {
                        count++;
                    } else {
                        break;
                    }
                }
                //add the colors to string
                AddColors(strBuilder, count, pixels[i]);
            }
            container.ByteData  = new Queue<byte>();
            container.AsciiData = strBuilder.ToString();
            return container;
        }
        private Container CompressListForBytes(List<Color> pixels, Container container) {
            Queue<byte> bytes = new Queue<byte>();
            int count;

            for (int i = 0; i < pixels.Count; i+=count) {
                count = 0;
                //scan & count each identical pixel from current till break
                for (int j = i; j < pixels.Count; j++) {
                    if (pixels[i] == pixels[j]) {
                        count++;
                    } else {
                        break;
                    }
                }
                //if count is 256 or above, split it up so its pieces may be represented by one byte
                if (count < 256) {
                    AddColors(bytes, count, pixels[i]);
                } else {
                    int divideNum = count/255;
                    int leftover = count%255;
                    for (int j = 0; j < divideNum; j++) {
                        AddColors(bytes, 255, pixels[i]);
                    }
                    //add any leftover pixels
                    if (leftover > 0)
                        AddColors(bytes, leftover, pixels[i]);
                }
            }
            container.ByteData = bytes;
            return container;
        }
        private void AddColors(Queue<byte> bytes, int count, Color pixel) {
            bytes.Enqueue(Convert.ToByte(count));
            bytes.Enqueue(Convert.ToByte(pixel.R));
            bytes.Enqueue(Convert.ToByte(pixel.G));
            bytes.Enqueue(Convert.ToByte(pixel.B));
        }
        private void AddColors(StringBuilder strBuilder, int count, Color pixel) {
            strBuilder.Append(count+"\n");
            strBuilder.Append(pixel.R + "\n");
            strBuilder.Append(pixel.G + "\n");
            strBuilder.Append(pixel.B + "\n");
        }
        private List<Color> BytesToColorList(Container container) {
            container.Header = BuildHeader(container);
            Queue<byte> bytesQ = container.ByteData;
            //assume bytes are only pixel data
            List<Color>  pixels   = new List<Color>();
            //convert bytes to colors
            while(bytesQ.Count > 0) {
                byte r = bytesQ.Dequeue(),
                     g = bytesQ.Dequeue(),
                     b = bytesQ.Dequeue();

                pixels.Add(Color.FromArgb(r,g,b));
            }

            return pixels;
        }
        private List<Color> AsciiToColorList(Container container) {
            container.Header = BuildHeader(container);
            string asciiValues = container.AsciiData;
            List<Color> pixels = new List<Color>();
            string r = "", g = "", b = "";
            int count = 0;

            //read each line till new line grabbing total value
            for (int i = 0; i < asciiValues.Length; i++) {
                //get the first value
                while(asciiValues[i] != '\n') {
                    if (count == 0) {
                        r += asciiValues[i];
                    }else if(count == 1) {
                        g += asciiValues[i];
                    } else {
                        b += asciiValues[i];
                    }
                    i++;
                }
                if (count == 2) {
                    //make the color
                    pixels.Add(ColorFromString(r,g,b));
                    count = 0;
                    r = "";
                    g = "";
                    b = "";
                } else {
                    count++;
                }
            }
            return pixels;
        }
        private Color ColorFromString(string r, string g, string b) {
            return Color.FromArgb(int.Parse(r), int.Parse(g), int.Parse(b));
        }
        private string BuildHeader(Container container) {
            StringBuilder str = new StringBuilder();
            //split the header into an array for manipulation
            char[] headerArr = new char[container.Header.Length];
            int index = 0;
            foreach (char item in container.Header) {
                headerArr[index] = item;
                index++;
            }

            if (headerArr[1] == '3') {
                //change 3 to 7
                headerArr[1] = '7';
            }else {
                //change 6 to 8
                headerArr[1] = '8';
            }
            //add back to a string
            foreach (char item in headerArr) {
                str.Append(item);
            }
            return str.ToString();
        }
        private string ChangeHeaderLZW(Container container, bool byteData) {
            StringBuilder str = new StringBuilder();

            if (byteData) {
                str.Append("P10\n");                

            } else {
                str.Append("P9\n");
            }
            for (int i = 3; i < container.Header.Length; i++) {
                str.Append(container.Header[i]);
            }

            return str.ToString();
        }
    }
}
