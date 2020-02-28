using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Project_Encode {
    class Encode {

        private List<Point> usedPoints;
        private string message;
        private Point  currentPoint;
        private Bitmap bmp;
        private string end = $"{(char)4}";
        private int increment;
        public readonly int maxMessageLength;

        public Encode(string msg, Bitmap Map, Point pnt, int letterIncrement) {            
            message      = msg + end;
            currentPoint = pnt;
            bmp          = CopyBitmap(Map);
            increment    = letterIncrement; //potential 
            usedPoints   = new List<Point>();
            maxMessageLength = (bmp.Width * bmp.Height)/increment;
        }
        #region METHODS
        public Bitmap HideMessage() {
            //copy bitmap for easy overwrite
            for (int index = 0; index < message.Length; index++) {
                //encodes one char at a time
                if(!HideChar(message[index]))
                    //if a char cannot be encoded, end loop and return null
                    return null;
            }
            return bmp;
        }
        #region PRIVATE
        private Bitmap CopyBitmap(Bitmap bmp) {
            Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height);
            for (int y = 0; y < bmp.Height; y++) {
                for (int x = 0; x < bmp.Width; x++) {
                    newBmp.SetPixel(x,y, bmp.GetPixel(x,y));
                }
            }
            return newBmp;
        }
        private bool HideChar(char target) {
            //get the current pixel RGB values
            if (usedPoints.Contains(currentPoint))
                return false;

            Color col = bmp.GetPixel(currentPoint.X, currentPoint.Y);
            Color newCol;

            //grab the blue value
            int workingNum = col.B;

            //get the difference
            int dif = target - workingNum;

            //create new color with the appropriate changed value
            newCol = MakeNewColor(dif, col);

            //Add the new values to the old and create a new pixel
            ChangePixel(newCol);
            NextPoint();
            return true;
        }
        private Color MakeNewColor(int dif, Color col) {
            //change the rgb value
            return Color.FromArgb(col.R, col.G, (col.B+dif));
        }
        
        private void ChangePixel(Color col) {
            //overwrites pixel with a given color
            bmp.SetPixel(currentPoint.X, currentPoint.Y,col);
        }
        
        private void NextPoint() {
            //add last used point to the list
            usedPoints.Add(currentPoint);
            //add the traversal amount to X
            currentPoint.X += increment;
            Debug.WriteLine(increment.ToString());

            if (currentPoint.X >= bmp.Width) {
                //if excess, run over to the X, and add one to Y
                currentPoint.X = (currentPoint.X - bmp.Width);
                currentPoint.Y = currentPoint.Y < bmp.Height-1 ? currentPoint.Y+=1 : currentPoint.Y = 0;
                
            }

        }
        #endregion
        #endregion
    }
}
