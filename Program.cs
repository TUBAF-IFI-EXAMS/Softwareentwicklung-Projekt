using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace wheatherproject
{
    class Program
    {
        HttpClient client = new HttpClient();
        public static string ApiKey = "09e41fcb0bfaccd128ff77e484531da3";
        public static int Tries;
        public static int Successes;
        public static float SuccessRate;
        public static string SavePath = "Saves/Data.txt";
        static async Task Main(string[] args)
        {
            ReadSave(SavePath);
            Console.WriteLine("Image Name:");
            var line = Console.ReadLine();
            var Image = new SouImg(line);
            AnalyzeWeather(Image);
            await new Program().Compare(Image);
            Image.PrintInfo();
            Console.WriteLine("Tries: " + Tries.ToString() + " Successes: " + Successes.ToString());
            SuccessRate = ((float)Successes/Tries)*100;
            Console.WriteLine("Success Rate over Time: "+ SuccessRate.ToString() + "%");

            SaveSave(SavePath);
        }
        static void AnalyzeWeather(SouImg img)
        {
            Bitmap objBitmap = new Bitmap(new Bitmap(Image.FromFile(img.ImgPath)), new Size(img.ImgSize, img.ImgSize));
            
            for (int i = 0; i < img.ImgSize-1; i++)
            {
                for (int j = 0; j < img.ImgSize-1; j++) 
                {
                    var Col = objBitmap.GetPixel(j, i);
                    //Console.WriteLine(Col.B);
                    var CDS = (Col.R + Col.B + Col.G)/3;
                    if (Col.B > Col.G && Col.G*0.8 > Col.R && Col.R + Col.G + Col.B > 150)
                    {
                        img.BlueCount++;
                    }
                    else if (Col.R > CDS - 20 && Col.R < CDS + 20 && Col.B > CDS - 20 && Col.B < CDS + 20 && Col.G > CDS - 20 && Col.G < CDS + 20)
                    {
                        if (CDS*3 < 240)
                        {
                            img.GrayCount++;
                        }
                        else
                        {
                            img.WhiteCount++;
                        }
                    }
                }
            }
            //Console.WriteLine((float)img.BlueCount/(img.ImgSize*img.ImgSize));
            //Console.WriteLine((float)img.WhiteCount/(img.ImgSize*img.ImgSize));
            //Console.WriteLine((float)img.GrayCount/(img.ImgSize*img.ImgSize));
            if ((float)img.BlueCount/(img.ImgSize*img.ImgSize) > 0.6f)
            {
                img.PredWeather = "clear";
            }
            else if((float)img.GrayCount/(img.ImgSize*img.ImgSize) > 0.2f)
            {
                img.PredWeather = "rainy";
            }
            else
            {
                img.PredWeather = "overcast";
            }
            
        }
        private async Task Compare(SouImg Image)
        {
            string url = "http://api.openweathermap.org/data/2.5/onecall/timemachine?lat=" + Image.Coordinate.Latitude.ToString() + "&lon=" + Image.Coordinate.Longitude.ToString() +
                         "&dt=" + ((DateTimeOffset)new DateTime(Image.DateandTime.Year, Image.DateandTime.Month, Image.DateandTime.Day, Image.DateandTime.Hour, 0, 0)).ToUnixTimeSeconds().ToString() 
                         + "&appid=" + ApiKey;
            string response = await client.GetStringAsync(url);
            
            //Console.WriteLine(response);

            wheaRes OnWhea = JsonConvert.DeserializeObject<wheaRes>(response);

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
            if(Image.PredWeather == Image.RealWeather)
            {
                Image.Success = true;
                Tries++;
                Successes++;
            }
            else{Image.Success = false; Tries++;}
            
            //Console.WriteLine(url);
        }
        private static void ReadSave(string Path)
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader(Path);
                
                line = sr.ReadLine();
                if (line != null)
                {
                    Tries = Int32.Parse(line);
                }
                else{Tries = 0; Console.WriteLine("init Save 1/2");}

                line = sr.ReadLine();
                if (line != null)
                {
                    Successes = Int32.Parse(line);
                }
                else{Successes = 0; Console.WriteLine("init Save 2/2");}
                
                sr.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        private static void SaveSave(string Path)
        {
            try
            {
                StreamWriter sw = new StreamWriter(Path);
                sw.WriteLine(Tries.ToString());
                sw.WriteLine(Successes.ToString());
                sw.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
