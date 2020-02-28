using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Project_Encode {
    class ConvertPPM {
        private Bitmap  myMap;
        private Color[,] colArr;
        private BinaryReader reader;
        public string File {get;set;}
        public ConvertPPM(Bitmap map) {
            myMap = map;
            colArr = new Color[myMap.Width,myMap.Height];
        }
        public ConvertPPM(Image img) {
            myMap  = new Bitmap(img);
            colArr = new Color[myMap.Width,myMap.Height];
        }
        public ConvertPPM(string file) {
            File = file;
        }
        #region METHODS
        public Bitmap PPMtoBitmap() {
            ////check which ppm file it is
            if (PpmCheck()) {
                //convert to p3
                return P3Conversion();
            } else {
                //convert to p6
                return P6Conversion();
            }
        }
        public Container BitmapToPPM() {
            PopulateArray(myMap);
            return BuildPPM(PpmCheck());

        }
        #region PRIVATES
        #region BINARY STUFF
        private Bitmap P3Conversion() {
            Bitmap bitmap;
            string widths = "", heights = "";
            int width, height;
            char temp;
            //eat new line
            reader.ReadChar();

            //eat comment
            while((temp = reader.ReadChar()) != '\n') {
                // yum yum
            }

            //grab the width and height of the img
            while((temp = reader.ReadChar()) != ' ') {
                widths += temp;
            }
            while((temp = reader.ReadChar()) >= '0' && temp <= '9') {
                heights += temp;
            }

            //check color spec or whatever
            if (reader.ReadChar() != '2' || reader.ReadChar() != '5' || reader.ReadChar() != '5')
                return null;
            
            //Eat the last newline
            reader.ReadChar(); 
            
            //save the bitmap size
            width = int.Parse(widths);
            height = int.Parse(heights);
            
            //create bitmap
            bitmap = new Bitmap(width, height);

            //fill bitmap
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {

                    int r = Convert.ToInt32(RGBGrabber()),
                        g = Convert.ToInt32(RGBGrabber()),
                        b = Convert.ToInt32(RGBGrabber());

                    Color col = Color.FromArgb(r,g,b);                        
                    bitmap.SetPixel(x,y,col);
                }
            }
            for (int y = 390; y < bitmap.Height; y++) {
                for (int x = 20; x < bitmap.Width; x++) {
                    var tempCol = bitmap.GetPixel(x,y);
                }
            }
            reader.Close();
            return bitmap;
        }
        private Bitmap P6Conversion() {
            Bitmap bitmap;
            string widths = "", heights = "";
            int width, height;
            char temp;

            //eat new line
            reader.ReadChar();

            //eat comment
            while(reader.ReadChar() != '\n') {
                // ?
            }

            //grab the width and height of the img
            while((temp = reader.ReadChar()) != ' ') {
                widths += temp;
            }
            while((temp = reader.ReadChar()) >= '0' && temp <= '9') {
                heights += temp;
            }
            
            //check color spec or whatever
            if (reader.ReadChar() != '2' || reader.ReadChar() != '5' || reader.ReadChar() != '5')
                return null;
            
            //Eat the last newline
            reader.ReadChar(); 
            
            //save the bitmap size
            width = int.Parse(widths);
            height = int.Parse(heights);
            
            //create bitmap
            bitmap = new Bitmap(width, height);
            
            //fill bitmap
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    int r = Convert.ToInt32(reader.ReadByte()),
                        g = Convert.ToInt32(reader.ReadByte()),
                        b = Convert.ToInt32(reader.ReadByte());
                    
                    Color col = Color.FromArgb(r,g,b);                        
                    bitmap.SetPixel(x,y,col);
                }
            }
            reader.Close();
            return bitmap;
        }
        private string RGBGrabber() {
            char temp;
            string colVal = "";

            while((temp = reader.ReadChar()) != '\n') {
                colVal += temp;
            }
            return colVal;
        }
        #endregion
        #region ASCII STUFF
        private bool PpmCheck() {
            //Debug.WriteLine(File);
            reader = new BinaryReader(new FileStream(File, FileMode.Open));


            //get the first two chars of the file
            char[] id = reader.ReadChars(2);

            //the second chars should be '3' or '6'
            if (id[1] == '3') {
                return true;
            }else if (id[1] == '6') {
                return false;
            } else {
                throw new Exception("Data in improper format - Is this a P3 or P6 PPM file?");
            }
        }
        private void PopulateArray(Bitmap img) {
            //scan bitmap and store its contents to array
            for (int y = 0; y < img.Height; y++) {
                for (int x = 0; x < img.Width; x++) {                    
                    colArr[x,y] = img.GetPixel(x,y);
                }
            }
        }
        private string ColToString(int x, int y) {
            string newLine = "\n";
            //return the rgb values as a string
            return colArr[x,y].R.ToString() + newLine 
                 + colArr[x,y].G.ToString() + newLine 
                 + colArr[x,y].B.ToString() + newLine;
        }
        private byte[] ColToByte(int x, int y) {
            //return the rgb values as an array of bytes
            byte[] bytes = {colArr[x,y].R, colArr[x,y].G, colArr[x,y].B};
            return bytes;
        } 
        private Container BuildPPM(bool ppmType) {
            StringBuilder str = new StringBuilder();
            Container myContainer = new Container();

            //construct the "header" portion of the ppm file
            str = BuildStart(ppmType, str);

            //true for P3, false for P6
            if (ppmType) {
                reader.Close();
                return BuildString(str, myContainer);
            } else {
                reader.Close();
                return BuildBytes(str,myContainer);
            }
        }
        private Container BuildString(StringBuilder str, Container myContainer) {
            //traverse the array of colors
            for (int y = 0; y < colArr.GetLength(1); y++) {
                for (int x = 0; x < colArr.GetLength(0); x++) {
                    //add each pixel's color to the string
                    str.Append(ColToString(x,y));
                }                
            }
            myContainer.String = str.ToString();

            //add empty queue for saveFile checks
            myContainer.Queue  = new Queue<byte>();
            return myContainer;
        }
        private Container BuildBytes(StringBuilder str, Container myContainer) {
            Queue<byte> myBytes = new Queue<byte>();
            //traverse the array of colors
            for (int y = 0; y < colArr.GetLength(1); y++) {
                for (int x = 0; x < colArr.GetLength(0); x++) {
                    //add each pixel's color to the string
                    byte[] bytes = ColToByte(x,y);

                    for (int i = 0; i < 3; i++) {
                        myBytes.Enqueue(bytes[i]);
                    }
                }                
            }
            myContainer.String = str.ToString();
            myContainer.Queue = myBytes;
            Debug.WriteLine($"{myBytes.Count} bytes saved myBytes");
            return myContainer;
        }
        private StringBuilder BuildStart(bool p3, StringBuilder str) {
            string newLine = "\n";

            if (p3) {
                str.Append("P3"+newLine);
                str.Append("# "+newLine);
                str.Append($"{colArr.GetLength(0)} {colArr.GetLength(1)}"+newLine);
                str.Append("255"+newLine);
            } else {
                str.Append("P6"+newLine);
                str.Append("# "+newLine);
                str.Append($"{colArr.GetLength(0)} {colArr.GetLength(1)}"+newLine);
                str.Append("255"+newLine);
            }
            return str;
        }
        #endregion
        #endregion
        #endregion
    }
}
