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

        private string[] compresserSymbols = {")","!","@","#","$","%","^","&","(" };

        public Compresser() {

        }
        public string CompressAscii(Container container) {
            List<Color> colList = AsciiToColorList(container.String);
            return CompressListForAscii(colList);
        }
        public List<byte> CompressBytes(Container container) {
            List<Color> colList = BytesToColorList(container.Queue);
            return CompressListForBytes(colList);
        }
        private string CompressListForAscii(List<Color> pixels) {
            StringBuilder strBuilder = new StringBuilder();
            int count;

            for (int i = 0; i < pixels.Count; i+=count) {
                count = 0;
                for (int j = i; j < pixels.Count; j++) {
                    if (pixels[i] == pixels[j]) {
                        count++;
                    } else {
                        break;
                    }
                }
                strBuilder.Append(count+"\n");
                strBuilder.Append(pixels[i].R + "\n");
                strBuilder.Append(pixels[i].G + "\n");
                strBuilder.Append(pixels[i].B + "\n");
            }
            return strBuilder.ToString();
        }
        //make a check to see if count is above 255, if so, get the excess as many times as it takes and repeat
        private List<byte> CompressListForBytes(List<Color> pixels) {
            List<byte>    bytes = new List<byte>();
            int count;

            for (int i = 0; i < pixels.Count; i+=count) {
                count = 0;
                for (int j = i; j < pixels.Count; j++) {
                    if (pixels[i] == pixels[j]) {
                        count++;
                    } else {
                        break;
                    }
                }
                bytes.Add(Convert.ToByte(count));
                bytes.Add(Convert.ToByte(pixels[i].R));
                bytes.Add(Convert.ToByte(pixels[i].G));
                bytes.Add(Convert.ToByte(pixels[i].B));
            }
            return bytes;
        }
        private List<Color> BytesToColorList(Queue<byte> bytesQ) {
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
        private List<Color> AsciiToColorList(string asciiValues) {
            List<Color> pixels = new List<Color>();
            string r = "", g = "", b = "";
            int count = 0;
            int start = 0;
            bool searching = true;
            //find where the color data starts
            while (searching) {
                if (asciiValues[start] != '\n') {
                    start++;
                } else {
                    start++;
                    count++;
                }

                if (count == 4) {
                    searching = false;
                }
            }

            //read each line till new line grabbing total value
            for (int i = start; i < asciiValues.Length; i++) {
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
                } else {
                    count++;
                }
            }
            return pixels;
        }
        private Color ColorFromString(string r, string g, string b) {
            return Color.FromArgb(int.Parse(r), int.Parse(g), int.Parse(b));
        }
        public string Compress(string str, byte[] bytes) {
            StringBuilder strBuild = new StringBuilder();

            int j;
            int count;

            for (int i = 0; i < str.Length; i+=count){
                count = 0;
                for (j = i; j < str.Length; j++) {

                    if (str[i] == str[j]) {
                        count++;
                    } else {
                        break;
                    }
                }
                if (count > 1) {
                    strBuild.Append(count.ToString()+str[i]);                    
                } else {
                    strBuild.Append(str[i]);
                }
            }

            for (int i = 0; i < bytes.Length; i+=count){
                count = 0;
                for (j = i; j < bytes.Length; j++) {
                    if (bytes[i] == bytes[j]) {
                        count++;
                    } else {
                        break;
                    }
                }
                if (count > 1) {
                    strBuild.Append(count.ToString()+bytes[i]);                    
                } else {
                    strBuild.Append(bytes[i]);
                }
            }

            return strBuild.ToString();
        }
        public string Compress(string str) {
            StringBuilder strBuild = new StringBuilder();

            int j;
            int count;

            for (int i = 0; i < str.Length; i+=count){
                count = 0;
                for (j = i; j < str.Length; j++) {

                    if (str[i] == str[j]) {
                        count++;
                    } else {
                        break;
                    }
                }
                if (count > 1) {
                    strBuild.Append(count.ToString()+str[i]);                    
                } else {
                    strBuild.Append(str[i]);
                }
            }
            return strBuild.ToString();
        }
        private string NumToSymbol(int num) {
            StringBuilder strBuilder = new StringBuilder();
            if (num < 10) {
                return compresserSymbols[num];
            } else {
                string numAsString = num.ToString();
                foreach (char letter in numAsString) {
                    strBuilder.Append(compresserSymbols[Convert.ToInt32(letter)]);
                }
                return strBuilder.ToString();
            }
        }
        private string SymbolToNum(string symbol) {
            for (int i = 0; i < compresserSymbols.Length; i++) {
                if (compresserSymbols[i] == symbol) {
                    return i.ToString();
                }
            }
            return "-1";
        }
        private bool IsSymbol(byte check) {
            return compresserSymbols.Contains(check.ToString());
        }
        public Bitmap DecompressAscii(string ascii, int bitmapWidth, int bitmapHeight) {
            //assume these bytes are pixels
            List<Color> pixels = new List<Color>();
            Bitmap bmp = new Bitmap(bitmapWidth, bitmapHeight);
            int  increment;
            bool colorTime = false;
            string toCopy = "";
            //read till not a symbol to get the number of upcoming pixels, then copy that many pixels
            //to the list


            //
            for (int i = 0; i < ascii.Length; i+=increment) {
                //check if current is a symbol
                increment = 1;
                
                if (!colorTime) {
                    //get the number value of the symbol(s)
                    //throw error if -1 is returned (not a symbol) so add exception handling outside
                    
                    //should be a index out of range but double check
                    toCopy += SymbolToNum(ascii[i].ToString());
                } else {
                    //grab the next three bytes (pixel) and copy that color to the list however many times
                    Color col = Color.FromArgb(ascii[i], ascii[i+1], ascii[i+2]);
                    
                    //convert string to number
                    int copiedPixels = int.Parse(toCopy);
                    
                    //copy the next 3 bytes that many times
                    for (int j = 0; j < copiedPixels; j++) {
                        pixels.Add(col);
                    }
                    //skip over the read bytes
                    increment = 3*copiedPixels;
                    toCopy = "";
                }
            }
        }
        public List<Color> DecompressBinary(byte[] bytes) {
            //assume these bytes are pixels
            List<Color> pixels = new List<Color>();
            int increment;
            string toCopy = "";
            //read till not a symbol to get the number of upcoming pixels, then copy that many pixels
            //to the list

            //jump the number of pixels copied
            for (int i = 0; i < bytes.Length; i+=increment) {
                //check if current is a symbol
                increment = 1;
                
                if (IsSymbol(bytes[i])) {
                    //get the number value of the symbol(s)
                    //throw error if -1 is returned (not a symbol) so add exception handling outside
                    
                    //should be a index out of range but double check
                    toCopy += SymbolToNum(bytes[i].ToString());
                } else {
                    //grab the next three bytes (pixel) and copy that color to the list however many times
                    Color col = Color.FromArgb(bytes[i], bytes[i+1], bytes[i+2]);
                    
                    //convert string to number
                    int copiedPixels = int.Parse(toCopy);
                    
                    //copy the next 3 bytes that many times
                    for (int j = 0; j < copiedPixels; j++) {
                        pixels.Add(col);
                    }
                    //skip over the read bytes
                    increment = 3*copiedPixels;
                    toCopy = "";
                }
            }
            return pixels;
        }
    }
}
