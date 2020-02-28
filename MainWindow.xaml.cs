using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Project_Encode {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        PopupInput popup;
        Bitmap bitty;
        Bitmap temp;
        int    letterIncrement;
        int    maxLength;
        string message;
        string currentPath;
        bool   isLoaded = false;
        readonly string PasswordHash = "P@@Sw0rd";
        readonly string SaltKey = "S@LT&KEY";
        readonly string VIKey = "@1B2c3D4e5F6g7H8";
        System.Drawing.Point startPoint;
        public MainWindow() {
            InitializeComponent();
        }
        private void MenuOpenFile_Click(object sender, RoutedEventArgs e) {
            FileClass fileHandler = new FileClass();
            
            //attempt to load chosen file
            try {
                bitty = fileHandler.LoadFile();

                //if the file loaded into the bitmap, convert to image source
                if (bitty != null) {
                    BitmapConverter bmpConvert = new BitmapConverter(bitty);
                    imgBox.Source = bmpConvert.ConvertToImageSource();
                    //save path for savefile later
                    currentPath = fileHandler.FilePath;
                    letterIncrement = (int)Math.Round(((double)bitty.Width/5) + ((double)bitty.Height/5)/5);
                    maxLength = ((bitty.Width * bitty.Height) / letterIncrement)-1;
                    isLoaded= true;
                }
        
            }catch(IOException){ 
                ShowPopUpMessage("File is being used by another process","ERROR");
            }catch{
                ShowPopUpMessage("Is this a P3 or P6 PPM file?", "ERROR");
            }
        }
        
        private void MnuSaveFile_Click(object sender, RoutedEventArgs e) {
            FileClass fileHandler = new FileClass();
            
            //if bitmap exists, convert to PPM and save file
            if (temp != null && bitty != null) {
                ConvertPPM convert = new ConvertPPM(temp);
                convert.File = currentPath;
                fileHandler.SaveFile(convert.BitmapToPPM());
            }
        }
        
        private void MnuHelp_Click(object sender, RoutedEventArgs e) {
            //content for "Help" pop up
            string content =                 
                "• Open any image with a PPM extension.\n\n"+
                "• Once image has loaded, enter the message you want to encode into the pop-up window, and click OK.\n\n"+
                "• Under 'Edit' on the menu, click 'Enter Message.' You may re-submit your message as needed\n\n"+
                "• Click 'Encode' under 'File.'\n\n"+ 
                "• Save the newly encoded image as a PPM under 'File.'"+
                "• Choose colorful areas if possible, favoring BLUE, then RED then GREEN."+
                "• The larger the image, the better. Recommended size is 1000x700 at minimum.";
            string header = "Instructions";
            ShowPopUpMessage(content, header);
        }
        
        private void MnuEdit_Click(object sender, RoutedEventArgs e) {
            //generate and show pop up    
            if (isLoaded) {
                popup = new PopupInput(maxLength);
                popup.Show();
            } else {
                ShowPopUpMessage("You must load an image before setting your message.","Load Image");
            }
                      
        }
        #region POPUP
        private void ShowPopUpMessage(string message, string header) {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(message,header,button);
        }
        #endregion
        #region GETTING COORDINATES
        private void ImgBox_MouseMove(object sender, MouseEventArgs e) {
            //make a bitmap image to access the pixel width/height
            BitmapImage bmp = (BitmapImage)imgBox.Source;
            //get the mouse position 
            txtX.Text = (e.GetPosition(imgBox).X * (double)bmp.PixelWidth/imgBox.ActualWidth).ToString("###");
            txtY.Text = (e.GetPosition(imgBox).Y * (double)bmp.PixelHeight/imgBox.ActualHeight).ToString("###");
        }
        private void ImgBox_MouseDown(object sender, MouseButtonEventArgs e) {
            BitmapImage bmp = (BitmapImage)imgBox.Source;
            startPoint.X = Convert.ToInt32(e.GetPosition(imgBox).X * (double)bmp.PixelWidth/imgBox.ActualWidth);
            startPoint.Y = Convert.ToInt32(e.GetPosition(imgBox).Y * (double)bmp.PixelHeight/imgBox.ActualHeight);
            //show chosen point
            txtChosenPoint.Text = $"X {startPoint.X.ToString()} Y {startPoint.Y.ToString()}";
            
            //do encoding
            EncodeMessage();
        }
        #endregion
        private void EncodeMessage() {
            try {
                if (popup != null) {
                    //grab message (dummy check)
                    message = popup.result;
                    FileClass fh = new FileClass();
                    
                    //pass args to encoder
                    Encode myEncoder = new Encode(message, bitty, startPoint, letterIncrement);
                    
                    //overwrite encoded message to bitmap
                    temp = myEncoder.HideMessage();
        
                    //shows the image if bitmap exists
                    if (temp != null) {
                        //converts to Image Source as WPF image box does not support regular bitmaps
                        BitmapConverter bmpConvert = new BitmapConverter(temp);
                        imgBox.Source = bmpConvert.ConvertToImageSource();
                        
        
                        //add the comment here
                        string xy = startPoint.X.ToString()+","+startPoint.Y.ToString();
                        Debug.WriteLine(xy);
                        txtHashBox.Text = "Coordinate hash: " + Encrypt(xy);
        
                    } else {
                        //error pop-up : tell user to pick a better location
                        ShowPopUpMessage(
                            "Starting location would overwrite previous piece of data, try using a shorter message or a new image.\n"+
                            "Please reference the 'HELP' menu for assistance", "Overwrite Error");
                    }
                } else {
                    ShowPopUpMessage("Enter message", "ERROR");
                }
                //exceptional handling
            }catch(ArgumentOutOfRangeException){
                ShowPopUpMessage("Probable sizing error.\n"+
                                 "Try choosing a new location.","Improper Increment");
            } catch {                
                ShowPopUpMessage("Is this a P3 or P6 PPM file?", "ERROR");
            }
        }
        private string Encrypt(string plainText)
		{
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

			byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
			var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
			
			byte[] cipherTextBytes;

			using (var memoryStream = new MemoryStream())
			{
				using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
				{
					cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
					cryptoStream.FlushFinalBlock();
					cipherTextBytes = memoryStream.ToArray();
					cryptoStream.Close();
				}
				memoryStream.Close();
			}
			return Convert.ToBase64String(cipherTextBytes);
		}
    }
}
