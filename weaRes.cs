namespace weatherproject
{
    //this is a replica of the class system of the api response and used for the Deserialisation of it
    public class weaRes
    {
        public float lat{get;set;}
        public float lon{get;set;}
        public string timezone{get;set;}
        public int timezone_offset{get;set;}
        public Status current{get;set;}
        public Status[] hourly{get;set;}

    }
    public class Weather
    {
        public int id{get;set;}
        public string main{get;set;}
        public string description{get;set;}
        public string icon{get;set;}
    }
    public class Status
    {
        public int dt{get;set;}
        public int sunrise{get;set;}
        public int sunset{get;set;}
        public float temp{get;set;}
        public float feels_like{get;set;}
        public int pressure{get;set;}
        public int humidity{get;set;}
        public float dew_point{get;set;}
        public int clouds{get;set;}
        public float wind_speed{get;set;}
        public int wind_deg{get;set;}
        public float wind_gust{get;set;}
        public Weather[] weather{get;set;}
    }
}