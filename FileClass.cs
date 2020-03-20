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
        private bool _isPPM;
        public string FilePath {get;set;}
        public bool   IsPPM {get {return _isPPM;} }
        public FileClass() {
        }
        private bool CheckExtension(string filePath) {
            if (filePath.ToLower().EndsWith(".ppm")) return true;
            else return false;
        }
        public Bitmap LoadFile() {
            openFile = new OpenFileDialog{ 
                Filter = "PPM (*.PPM)|*.PPM|All files (*.*)|*.*" 
            };

            try {
                if (openFile.ShowDialog() == DialogResult.OK) {
                    FilePath = openFile.FileName;
                    openFile.Dispose();
                    if (CheckExtension(FilePath)) {
                        _isPPM = true;
                        ConvertPPM conv = new ConvertPPM(FilePath);
                        return conv.PPMtoBitmap();
                    } else {
                        _isPPM = false;
                        return new Bitmap(FilePath);
                    }
                    //var binRead = new BinaryReader(new FileStream(FilePath, FileMode.Open));
                    //Debug.WriteLine(binRead.BaseStream.Length + "bytes in original file.");
                    //binRead.Close();
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
            //unpack container
            string asciis = myContainer.Header+myContainer.AsciiData;
            Queue<byte> bytes = myContainer.ByteData;

            if (saveFile.ShowDialog() == DialogResult.OK) {
                if ((stream = saveFile.OpenFile()) != null) {
                    //write each byte from the string to file
                    Debug.WriteLine(asciis.Length + "bytes in compressed file.");
                    foreach (byte bit in asciis) {
                        stream.WriteByte(bit);
                    }
                    stream.Close();
                    //if binary, append the bytes with binary writer to file
                    if (bytes.Count > 0) {
                        binWriter = new BinaryWriter(new FileStream(saveFile.FileName, FileMode.Append));                        
                        //write bytes till queue is empty
                        while(bytes.Count > 0) {
                            binWriter.Write(bytes.Dequeue());
                        }
                        binWriter.Close();
                    }

                }
            }
        }
    }
}
