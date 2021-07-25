using System;

namespace wheatherproject
{
    public class SouImg
    {
        public string ImgPath; //the path the image is stored at
        public int ImgSize; //the size the image is analized at // default value 128x128px
        public DaTi DateandTime; //stores the date and time the image was taken
        public Coords Coordinate; //stores the location the image was taken
        public int BlueCount; //the amount of pixels which are deemed to be blue
        public int WhiteCount;// the amount of pixels which are deemed to be white
        public int GrayCount;// the amount of pixels which are deemed to be dark gray
        public string PredWeather; // the predicted weahter in the image
        public string RealWeather; //the weather from the api
        public bool Success; //stores if the guess was right or not
        public SouImg(string imPath, string imHour, string imDate, string imLat, string imLon)
        {
            if (imPath != null && imPath.Length != 0) //checks that the image path isnt empty
            {
                ImgPath = "s_img/" + imPath; //sets the image path
            }
            else
            {
                //tells you that u didnt write anything and closes the programm
                Console.WriteLine("you didnt write the name of an image");
                System.Environment.Exit(1);
            }
            //setting size & the count variables to their default value
            ImgSize = 128;
            BlueCount = 0;
            WhiteCount = 0;
            GrayCount = 0;

            if (Program.ParseanInt(imHour) == 0) // checks if the hour is 0
            {
                imHour = "24"; //hour gets set to 24
            }
            //sets the date and time from this format example hour: 22 ,date: 25.07.2021 // hour is -= 1 to get the right timezone
            DateandTime = new DaTi(Program.ParseanInt(imDate.Substring(6, 4)), Program.ParseanInt(imDate.Substring(3, 2)), Program.ParseanInt(imDate.Substring(0, 2)), Program.ParseanInt(imHour)-1);
            //sets the location from the given coordinates
            Coordinate = new Coords(Program.ParseaFloat(imLat), Program.ParseaFloat(imLon));
        }

        //prints out all values which are store in this class
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
    public class DaTi  //stores a given date and time
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

        //prints out all values which are store in this class
        public void PrintInfo()
        {
            Console.WriteLine("Time and Date: " + (Hour+1).ToString()+ ":00 " + Day.ToString() + "." + Month.ToString() + "." + Year.ToString());
        }
    }
    public class Coords   //stores a given location in form of coordinates
    {
        public float Latitude;
        public float Longitude;
        public Coords(float lat, float lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        //prints out all values which are store in this class
        public void PrintInfo()
        {
            Console.WriteLine("Coordinates: " + Latitude.ToString() + ", " + Longitude.ToString());
        }
    }
}