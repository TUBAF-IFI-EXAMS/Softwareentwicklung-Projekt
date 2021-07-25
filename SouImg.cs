using System;

namespace wheatherproject
{
    public class SouImg
    {
        public string ImgPath;
        public int ImgSize;
        public DaTi DateandTime;
        public Coords Coordinate;
        public int BlueCount;
        public int WhiteCount;
        public int GrayCount;
        public string PredWeather;
        public string RealWeather;
        public bool Success;
        public SouImg(string imPath, string imHour, string imDate, string imLat, string imLon)//, string DateTime, float tlat, float tlon) // 22_22-07-2021_50.93-13.34
        {
            if (imPath != null && imPath.Length != 0)
            {
                ImgPath = "s_img/" + imPath;
            }
            else
            {
                Console.WriteLine("you didnt write the name of an image");
                System.Environment.Exit(1);
            }
            ImgSize = 128;
            BlueCount = 0;
            WhiteCount = 0;
            GrayCount = 0;
            if (Int32.Parse(imHour) == 0)
            {
                imHour = "24";
            }
            DateandTime = new DaTi(Program.ParseanInt(imDate.Substring(6, 4)), Program.ParseanInt(imDate.Substring(3, 2)), Program.ParseanInt(imDate.Substring(0, 2)), Program.ParseanInt(imHour)-1);

            Coordinate = new Coords(Program.ParseaFloat(imLat), Program.ParseaFloat(imLon));
        }

        public void PrintInfo()
        {
            Console.WriteLine("Path: " + ImgPath.ToString());
            Console.WriteLine("Image size: " + ImgSize.ToString() + "px * " + ImgSize.ToString() + "px");
            Console.WriteLine("Blue Pixel %: " + (((float)BlueCount/(ImgSize*ImgSize))*100f).ToString() );
            Console.WriteLine("White Pixel %: " + (((float)WhiteCount/(ImgSize*ImgSize))*100f).ToString() );
            Console.WriteLine("Gray Pixel %: " + (((float)GrayCount/(ImgSize*ImgSize))*100f).ToString() );
            Console.WriteLine("Predicted: " + PredWeather.ToString());
            Console.WriteLine("Actual: " + RealWeather.ToString());
            Console.WriteLine("Success: " + Success.ToString());
            DateandTime.PrintInfo();
            Coordinate.PrintInfo();
        }
    }
    public class DaTi
    {
        public int Year;
        public int Month;
        public int Day;
        public int Hour;

        public DaTi(int ye, int mo, int da, int ho)
        {
            Year = ye;
            Month = mo;
            Day = da;
            Hour = ho;
        }
        public void PrintInfo()
        {
            Console.WriteLine("Time and Date: " + (Hour+1).ToString()+ ":00 " + Day.ToString() + "." + Month.ToString() + "." + Year.ToString());
        }
    }
    public class Coords
    {
        public float Latitude;
        public float Longitude;
        public Coords(float lat, float lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
        public void PrintInfo()
        {
            Console.WriteLine("Coordinates: " + Latitude.ToString() + ", " + Longitude.ToString());
        }
    }
}