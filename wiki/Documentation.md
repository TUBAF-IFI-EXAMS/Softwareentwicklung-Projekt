# Program.cs

## variables
| Name | Type | Description |
|------|------|-------------|
| ApiKey | String | stores the API-Key provided by your API account |
| Tries | Integer | stores the total Amount of times the program tried to guess the weather of an image |
| Successes | Integer | stores the total Amount of times the program guessed the weather of an image successfully |
| SuccessRate | Float | is the rate at which the program guesses right, is calculated at runtime |
| BlueThresh | Float | saves a value between 0 and 1 which decides when the weather is guessed as clear, can change at runtime |
| DGrayThresh | Float | saves a value between 0 and 1 which decides when the weather is guessed as rainy, can change at runtime |
| SavePath | String | stores the path to a .txt file which stores the values of: Tries, Successes, BlueThresh, DGrayThresh |

## functions
### static void AnalyzeWeather(SouImg img)
#### description:
A method which analyzes at a given image of the sky and guesses the weather which is displayed between the options: clear, overcast, rainy. <br>
After analyzing it sets the predicted weather of the SouImg object, which is associated with the image, to the resulting option.
#### parameter:
A SouImg object which stores a Image.
### private async Task Compare(SouImg img)
#### description:
A method which compares the results of the AnalyzeWeather method with real weather data from the OpenWeather API. <br>
It uses resuts from the AnalyzeWeather method. Therefore it must be run before this funtion for any given image. <br>
After analyzing the API response it sets the real weather of the SouImg object, which is associated with the image, to the resulting option.
#### parameter:
A SouImg object which stores a Date, Time, Location and Predicted Weather.
### private static void ReadSave(string Path)
#### description:
A method which reads out saved values of: Tries, Successes, BlueThresh and DGrayThresh from a .txt file.
#### parameter:
The path to a .txt file. 
### private static void SaveSave(string Path)
#### description:
A method which saves the current values of: Tries, Successes, BlueThresh and DGrayThresh into a .txt file.
#### parameter:
The path to a .txt file.
### public static int ParseanInt(string value)
#### description:
A function which converts a String to an Integer. If it fails it closes the program.
#### parameter:
A String which displays an Integer.
### public static float ParseaFloat(string value)
#### description:
A function which converts a String to a Float. If it fails it closes the program.
#### parameter:
A String which displays a Float.
### public static void PrintName()
#### description:
A function which Prints out a splashscreen into the console. Sky.NET

# SouImg.cs
## classes
### SouImg [Source Image]
#### variables:
| Name | Type | Description |
|------|------|-------------|
| ImgPath | String | stores the path the image is stored at |
| ImgSize | Integer | the size the image is analyzed at, default value 128 |
| DateandTime | DaTi | stores the date and time the image was taken |
| Coordinate | Coords | stores the location the image was taken |
| BlueCount | Integer | the amount of pixels of the image which are deemed to be blue |
| WhiteCount | Integer | the amount of pixels of the image which are deemed to be white |
| GrayCount | Integer | the amount of pixels of the image which are deemed to be dark gray |
| PredWeather | String | stores the predicted weather |
| RealWeather | String | stores the weather from the api |
| Success | Bool | stores if a guess was right or not |

#### functions:
| Name | Description |
|------|-------------|
| PrintInfo() | Prints out all saved values |
| PrintOutput() | Prints out all values that are relevant to an average end-user |

#### constructor:
```
public SouImg(string imPath, string imHour, string imDate, string imLat, string imLon)
```
Creates a new SouImg object. Sets ImgPath, DateandTime and Coordinate to the given values and sets BlueCount, WhiteCount, GrayCount to 0 and ImgSize to 128.
### DaTi [Date and Time]
#### variables:
| Name | Type | Description |
|------|------|-------------|
| Year | Integer | stores the year of a date |
| Month | Integer | stores the month of a date |
| Day | Integer | stores the day of a date |
| Hour | Integer | stores the hour in military time |

#### functions:
| Name | Description |
|------|-------------|
| PrintInfo() | Prints out all saved values |

#### constructor:
```
public DaTi(int ye, int mo, int da, int ho)
```
Creates a new DaTi object. Sets Year, Month, Day and Hour to the given values.

### Coords [location in coordinates]
#### variables
| Name | Type | Description |
|------|------|-------------|
| Latitude | Float | stores the Latitude of a Location |
| Longitude | Float | stores the Longitude of a Location |

#### functions:
| Name | Description |
|------|-------------|
| PrintInfo | Prints out all saved values |

#### constructor:
```
public Coords(float lat, float lon)
```
Creates a new Coords object. Sets Latitude and Longitude to the given values.

# weaRes.cs
This is a replica of the class system of the API-response and used for the Deserialization of it. <br>
It is described [here](https://openweathermap.org/api/one-call-api).