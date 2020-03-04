using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project_Encode {
    class FileClass {
        //LOAD, AND SAVE A .PPM IMAGE
        private OpenFileDialog openFile;
        private SaveFileDialog saveFile;
        private Stream stream;
        private BinaryWriter binWriter;
        public string FilePath {get;set;}
        public FileClass() {
        }

        public Bitmap LoadFile() {
            openFile = new OpenFileDialog{ 
                Filter = "PPM (*.PPM)|*.PPM|All files (*.*)|*.*" 
            };
            try {
                if (openFile.ShowDialog() == DialogResult.OK) {
                    FilePath = openFile.FileName;
                    var binRead = new BinaryReader(new FileStream(FilePath, FileMode.Open));
                    Debug.WriteLine(binRead.BaseStream.Length + "bytes in original file.");
                    ConvertPPM conv = new ConvertPPM(FilePath);
                    binRead.Close();
                    openFile.Dispose();
                    return conv.PPMtoBitmap();
                }
                return null;

            } catch(IOException) {
                throw new IOException();
            } catch {
                throw new Exception("Unknown error");
            }
        }
        public void SaveFile(Container myContainer) {
            saveFile = new SaveFileDialog {
                Filter = "PPM (*.ppm)|*.ppm|All files(*.*)|*.*"
            };
            Compresser compresser = new Compresser();

            string asciis = compresser.Compress(myContainer);
            //unpack container
            //string asciis = myContainer.String;
            //Queue<byte> bytes = myContainer.Queue;

            if (saveFile.ShowDialog() == DialogResult.OK) {
                if ((stream = saveFile.OpenFile()) != null) {
                    //write each byte from the string to file
                    Debug.WriteLine(asciis.Length + "bytes in compressed file.");
                    foreach (byte bit in asciis) {
                        stream.WriteByte(bit);
                    }
                    stream.Close();
                    //if P6, append the bytes with binary writer to file
                    //if (bytes.Count > 0) {
                    //    binWriter = new BinaryWriter(new FileStream(saveFile.FileName, FileMode.Append));                        
                    //    //write bytes till queue is empty
                    //    while(bytes.Count > 0) {
                    //        binWriter.Write(bytes.Dequeue());
                    //    }
                    //    binWriter.Close();
                    //}

                }
            }
        }
    }
}
