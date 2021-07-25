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
        public SouImg(string Path)//, string DateTime, float tlat, float tlon) // 22_22-07-2021_50.93-13.34
        {
            if (Path == null || Path.Length == 0)
            {
                Path = "22_22-07-2021_50-93_13-34.jpg";
            }
            ImgPath = "s_img/" + Path;
            ImgSize = 128;
            BlueCount = 0;
            WhiteCount = 0;
            GrayCount = 0;
            if (Int32.Parse(Path.Substring(0, 2)) /*Char.ToString(Path[0]) + Char.ToString(Path[1]))*/ != 0)
            {

                DateandTime = new DaTi(Int32.Parse(Path.Substring(9, 4)), Int32.Parse(Path.Substring(6, 2)), Int32.Parse(Path.Substring(3, 2)), Int32.Parse(Path.Substring(0, 2))-1);
                //DateandTime = new DaTi(Int32.Parse(Char.ToString(Path[9]) + Char.ToString(Path[10]) + Char.ToString(Path[11]) + Char.ToString(Path[12])),
                //0 + Int32.Parse(Char.ToString(Path[6]) + Char.ToString(Path[7])), 0 + Int32.Parse(Char.ToString(Path[3]) + Char.ToString(Path[4])),
                //Int32.Parse(Char.ToString(Path[0]) + Char.ToString(Path[1]))-1);
            }
            else
            {
                DateandTime = new DaTi(Int32.Parse(Path.Substring(9, 4)), Int32.Parse(Path.Substring(6, 2)), Int32.Parse(Path.Substring(3, 2)), 23);
                //DateandTime = new DaTi(Int32.Parse(Char.ToString(Path[9]) + Char.ToString(Path[10]) + Char.ToString(Path[11]) + Char.ToString(Path[12])),
                //0 + Int32.Parse(Char.ToString(Path[6]) + Char.ToString(Path[7])), 0 + Int32.Parse(Char.ToString(Path[3]) + Char.ToString(Path[4])), 23);
            }
            Coordinate = new Coords(float.Parse(Path.Substring(14, 2) + "," + Path.Substring(17, 2)),
                                    float.Parse(Path.Substring(20, 2) + "," + Path.Substring(23, 2)));
            //Coordinate = new Coords(float.Parse(Char.ToString(Path[14]) + Char.ToString(Path[15]) + "." + Char.ToString(Path[17]) + Char.ToString(Path[18])),
            //                        float.Parse(Char.ToString(Path[20]) + Char.ToString(Path[21]) + "." + Char.ToString(Path[23]) + Char.ToString(Path[24])));
            // das , war ein . in 94 wird aber ein , erwartet.
            
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