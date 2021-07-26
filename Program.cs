using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace weatherproject
{
    class Program
    {
        HttpClient client = new HttpClient();   //http client for the api call
        public static string ApiKey = "09e41fcb0bfaccd128ff77e484531da3"; // the api key for the open weather api
        public static int Tries;    //Number of times the program tried to guess the weather
        public static int Successes; //Number of times the program guessed right
        public static float SuccessRate; //the Rate at which the program is guessing right
        public static float BlueThresh; //the value which decides when the weather is clear
        public static float DGrayThresh; //the value which decides when the weather is rainy
        public static string SavePath = "Saves/Data.txt"; //the path to the textdocument which saves the values of: Tries, Successes, BlueThresh, DGrayThresh
        static async Task Main(string[] args)
        {
            ReadSave(SavePath);     //loads the saved values into the program
            
            //getting all the imput for the image (image Name, Time, Date, and Location)
            Console.WriteLine("Image Name:");
            var ImPath = Console.ReadLine();
            Console.WriteLine("Image Time (Hour only [miltary time]):");
            var ImHour = Console.ReadLine();
            Console.WriteLine("Image Date (Day.Month.Year):");
            var ImDate = Console.ReadLine();
            Console.WriteLine("Image Latitude (2 decimals):");
            var ImLat = Console.ReadLine();
            Console.WriteLine("Image Longitude (2 decimals):");
            var ImLon = Console.ReadLine();

            //creating an Image object with the given values
            var Image = new SouImg(ImPath, ImHour, ImDate, ImLat, ImLon);
            
            AnalyzeWeather(Image);  //Sends the Image object into an function which guesses its weather
            await new Program().Compare(Image); //Compares the results with real world data from the api

            Image.PrintInfo(); //Prints out all data of the image Object including results from the guess and the comparison
            Console.WriteLine("Tries: " + Tries.ToString() + " Successes: " + Successes.ToString()); //prints out the number of time the programm guessed an the number of times it was right
            SuccessRate = ((float)Successes/Tries)*100;     //calculating the success rate
            Console.WriteLine("Success Rate over Time: "+ SuccessRate.ToString() + "%");    //prints out the success rate of the progamm

            Console.WriteLine("BlueThresh: " + BlueThresh.ToString() + ", DGrayThresh= " + DGrayThresh.ToString());     //prints out the current values of the threshholds

            SaveSave(SavePath); // stores the new number of tries and successes and the potentially new values of the threshholds in the .txt
        }
        static void AnalyzeWeather(SouImg img)
        {
            // Creates a new Bitmap from the image so we can analyze it with system.drawing
            // also stretches the image to a smaller size, to shorten the run time and make all images take the same amount of time to analyze
            Bitmap objBitmap = new Bitmap(new Bitmap(Image.FromFile(img.ImgPath)), new Size(img.ImgSize, img.ImgSize));
            
            // scans through all pixels of the image in the same order a tv would display it (row after row)
            for (int i = 0; i < img.ImgSize-1; i++)
            {
                for (int j = 0; j < img.ImgSize-1; j++) 
                {
                    var Col = objBitmap.GetPixel(j, i); //gets the RGB values of the current pixel
                    var CDS = (Col.R + Col.B + Col.G)/3; //gets the average value of the R G and B value

                    //checks if the pixel is blue by checking if the B value is bigger than the G and if the R value is the lowest. also checks for a certain brightness
                    if (Col.B > Col.G && Col.G*0.8 > Col.R && Col.R + Col.G + Col.B > 150)
                    {
                        img.BlueCount++; //picel is deemed to be blue
                    }
                    // checks if the rgb value are relatively close togheter to deem the as eighter white or gray
                    else if (Col.R > CDS - 20 && Col.R < CDS + 20 && Col.B > CDS - 20 && Col.B < CDS + 20 && Col.G > CDS - 20 && Col.G < CDS + 20)
                    {
                        if (CDS*3 < 240)
                        {
                            img.GrayCount++; //pixel is deemed to be dark gray
                        }
                        else
                        {
                            img.WhiteCount++; // pixel is deemed to be white
                        }
                    }
                }
            }
            //checks if the percentage of blue pixels is over a certain threshhold
            if ((float)img.BlueCount/(img.ImgSize*img.ImgSize) > BlueThresh)
            {
                img.PredWeather = "clear"; //the image is deemed to display clear weather
            }
            //checks is the percentage of dark gray pixel is over a certain threshhold
            else if((float)img.GrayCount/(img.ImgSize*img.ImgSize) > DGrayThresh)
            {
                img.PredWeather = "rainy"; //the image is deemed to display rainy weather
            }
            else
            {
                img.PredWeather = "overcast"; //the image is deemed to display an overcast sky
            }
            
        }
        private async Task Compare(SouImg Image)
        {
            //constructing the url for the api call whith the input data from the image object
            string url = "http://api.openweathermap.org/data/2.5/onecall/timemachine?lat=" + Image.Coordinate.Latitude.ToString() + "&lon=" + Image.Coordinate.Longitude.ToString() +
                         "&dt=" + ((DateTimeOffset)new DateTime(Image.DateandTime.Year, Image.DateandTime.Month, Image.DateandTime.Day, Image.DateandTime.Hour, 0, 0)).ToUnixTimeSeconds().ToString() 
                         + "&appid=" + ApiKey;
            //getting a big string from the api
            string response = await client.GetStringAsync(url);
            //Deserializing the big string with help of the wheaRes class which represents the structure of the api response
            wheaRes OnWhea = JsonConvert.DeserializeObject<wheaRes>(response);

            //checking the status of the weather with help of the weather id from the api response
            if(OnWhea.current.weather[0].id < 300)
            {
                Image.RealWeather = "rainy";
            }
            else if(OnWhea.current.weather[0].id < 502)
            {
                Image.RealWeather = "overcast";
            }
            else if(OnWhea.current.weather[0].id < 600)
            {
                Image.RealWeather = "rainy";
            }
            else if(OnWhea.current.weather[0].id < 800)
            {
                Image.RealWeather = "overcast";
            }
            else if(OnWhea.current.weather[0].id < 803)
            {
                Image.RealWeather = "clear";
            }
            else if(OnWhea.current.weather[0].id < 805)
            {
                Image.RealWeather = "overcast";
            }
            //checking if the predicted weather is the same as the one the api said
            if(Image.PredWeather == Image.RealWeather)
            {
                Image.Success = true; // the guess is deemed successful
                Tries++;    //the number of total tries increases by 1
                Successes++; // the number of successfull guesses increases by 1
            }
            else // == guess is false
            {
                Image.Success = false; // the guess is deemed unsuccessful
                Tries++; //the number of total tries increases by 1

                //Programm changes internal Threshholds by +-1% to hopefully improve further guesses
                if (Image.PredWeather == "clear")
                {
                    BlueThresh += 0.01f;
                }
                else if (Image.PredWeather == "overcast")
                {
                    if(Image.RealWeather == "clear")
                    {
                        BlueThresh -= 0.01f;
                    }
                    else{DGrayThresh -= 0.01f;}
                }
                else if (Image.PredWeather == "rain")
                {
                    if(Image.RealWeather == "clear")
                    {
                        BlueThresh -= 0.01f;
                    }
                    else{DGrayThresh += 0.01f;}
                }
            }
        }
        private static void ReadSave(string Path)
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader(Path); //opening the save txt file to start reading values
                
                line = sr.ReadLine();//reading the first line
                if (line != null && line != "")//checking if the line isnt empty
                {
                    Tries = ParseanInt(line);//setting total Tries to saved value
                }
                else{Tries = 0; Console.WriteLine("init Save 1/4");} //setting default value

                line = sr.ReadLine();//reading the next line
                if (line != null && line != "")//checking if the line isnt empty
                {
                    Successes = ParseanInt(line);//setting total successful guesses to saved value
                }
                else{Successes = 0; Console.WriteLine("init Save 2/4");} //setting default value
                
                line = sr.ReadLine();//reading the next line
                if (line != null && line != "")//checking if the line isnt empty
                {
                    BlueThresh = ParseaFloat(line);//setting Blue Threshold to saved value
                }
                else{BlueThresh = 0.6f; Console.WriteLine("init Save 3/4");} //setting default value

                line = sr.ReadLine();//reading the next line
                if (line != null && line != "")//checking if the line isnt empty
                {
                    DGrayThresh = ParseaFloat(line);//setting Dark Gray Threshold to saved value
                }
                else{DGrayThresh = 0.2f; Console.WriteLine("init Save 4/4");} //setting default value
                
                sr.Close();//closing the txt because we read/set all the values already
            }
            catch(Exception e) //cathing weird exeptions like the txt file missing
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        private static void SaveSave(string Path)
        {
            try
            {
                StreamWriter sw = new StreamWriter(Path); // opening the txt file to start saving data
                sw.WriteLine(Tries.ToString()); // saving the total number of tries in the first line
                sw.WriteLine(Successes.ToString());// saving the total number of correct guesses in the second line
                sw.WriteLine(BlueThresh.ToString());//saving the current value of BlueThresh in the third line
                sw.WriteLine(DGrayThresh.ToString()); //saving the current value of DarkGrayThresh in the fourth line
                sw.Close(); //closing the txt file because we are done saving data
            }
            catch(Exception e)  //cathing weird exeptions like the txt file missing
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public static int ParseanInt(string value)
        {
            try
            {
                int number = Int32.Parse(value); //converting the given string to an integer
                return number;
            }
            catch (FormatException) //catches problems with the conversion and ends the program
            {
                Console.WriteLine("Unable to convert '{0}' to int.", value);
                System.Environment.Exit(1);
                return 0;
            }
        }
        public static float ParseaFloat(string value)
        {
            try
            {
                float number = float.Parse(value); //converting the given string to a float
                return number;
            }
            catch (FormatException) //catches problems with the conversion and ends the program
            {
                Console.WriteLine("Unable to convert '{0}' to float.", value);
                System.Environment.Exit(1);
                return 0;
            }
        }
    }
}
