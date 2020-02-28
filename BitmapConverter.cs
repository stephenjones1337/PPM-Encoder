using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;

namespace Project_Encode {
    class BitmapConverter {
        private Bitmap bitmap;
        public BitmapConverter(Bitmap map) {
            bitmap = map;
        }

        public System.Windows.Media.ImageSource ConvertToImageSource() {
            using (MemoryStream mem = new MemoryStream()) {
                //use memory stream to save BMP format
                bitmap.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp);
                //set stream position
                mem.Position = 0;
                //create new BitmapImage var
                BitmapImage bitmapImage = new BitmapImage();
                //begin conversion
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = mem;
                //cache entire image at load time
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                //end
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public Container ConvertToP3(Bitmap bmp, int ppmType) {
            //collect bitmap data
            Container container = new Container();
            StringBuilder str   = new StringBuilder();

            //do the header part
            container.String =  "P3"+
                                "# comment goes here\n"+
                                $"{bmp.Width} {bmp.Height}\n"+
                                "255";

            //fill the array
            for (int y = 0; y < bmp.Height; y++) {
                for (int x = 0; x < bmp.Width; x++) {
                    //convert the number value to string and append to string builder
                    string[] colorArr = ColToString(bmp.GetPixel(x,y));
                    //add each to the string builder
                    foreach (string color in colorArr) {
                        str.Append(color);
                    }
                }
            }

            return container;
        }
        public Container ConvertToP6(Bitmap bmp) {
            //collect bitmap data
            Container container = new Container();
            Queue<byte> bytes   = new Queue<byte>();

            container.String =  "P6\n"+
                                "# comment goes here\n"+
                                $"{bmp.Width} {bmp.Height}\n"+
                                "255";

            //fill the array
            for (int y = 0; y < bmp.Height; y++) {
                for (int x = 0; x < bmp.Width; x++) {
                    byte[] byteArr = ColToByte(bmp.GetPixel(x, y));
                    //add each value to the queue of bytes
                    foreach (byte color in byteArr) {
                        bytes.Enqueue(color);
                    }
                }
            }

            return container;
        }
        private byte[] ColToByte(Color col) {
            //return the rgb values as an array of bytes
            return new byte[] {col.R, col.G, col.B};
        } 
        private string[] ColToString(Color col) {
            return new string[] {col.R.ToString(), col.B.ToString(), col.B.ToString()};
        }
    }
}
