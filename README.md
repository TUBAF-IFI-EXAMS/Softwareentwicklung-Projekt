# Sky.NET
Softwareentwicklung-Projekt

You must run the following command to install the project's dependencies:
```
dotnet restore
```

the imags have to only include sky.

Image files need to be in the "s_img" folder.

You also need the following data for your image:

>the hour closest to when it was taken (in military time and german timezone)

>the date the image was taken (example: 25.07.2021)

>the coordinates where the picture was taken (example: Latitude= 46.21, Longitude= 32.15)


The Programm only works with dates from the last 5 day because that is the limitation of the used API

this is the output that the programm currently gives you after inputting an image: <br>

![programm output](https://cdn.discordapp.com/attachments/550280788445495306/868978220496719902/unknown.png)
