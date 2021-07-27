# Sky.NET
### Softwareentwicklung-Projekt

example of the program being used: <br>
![programm output](https://cdn.discordapp.com/attachments/481454988002852897/869358754699816980/unknown.png)

## description
Sky.Net is an image processing program that can determine the weather from an image displaying the sky. <br>
It self-evaluates itself against a [weather-API](https://github.com/Rob-Ku/Softwareentwicklung-Projekt/wiki/Used-API) which provides real-world data. <br>
It also has a primitive way of self-correcting internal thresholds if it guesses wrong consistently.

## functionality
Sky.Net works by analyzing the Color Data of a given image to determine how much of it is blue, white, and dark gray. <br>
It uses this data and their proportions to each other to guess which weather is shown in the image. <br>
It compares this guess with real-world data from the [open-weather API](https://github.com/Rob-Ku/Softwareentwicklung-Projekt/wiki/Used-API) to see if it is right or wrong. <br>
If it guesses wrong it shifts internal thresholds by 1% to hopefully improve the algorithm for future guesses.

## [how to install/use this program](https://github.com/Rob-Ku/Softwareentwicklung-Projekt/wiki/Tutorial)
Open the link above to get detailed instructions on how to install and use this program.

## structure
Program.cs contains all of the main functions of the program. <br>
The main Class, which handles the Images and their additional information, is written in SouImg.cs. <br>
weaRes.cs contains a replica of the class used in the API-Response, which is used for the deserialization of the API-Response. <br>

You can also look at the [UML-Diagrams](https://github.com/Rob-Ku/Softwareentwicklung-Projekt/wiki/UML-Diagrams) to get a better view of how the program is structured

## [documentation](https://github.com/Rob-Ku/Softwareentwicklung-Projekt/wiki/Documentation)
Open the link above to get detailed instructions on all variables, functions, and classes used in the program.
