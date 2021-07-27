## general info
* the only system requirement is `.NET 5.0`
* used images should show sky **exclusively** (or it could mess up the guess of the weather)
* used images have to been taken **during the day** (between sunrise and sunset)
* used images have to be from the last 5 day (limitation of the used API)
* used image have to be located in the `s_img` folder, which is located in the installation folder of the project
* to reset all saved data and internal thresholds you just have to open the `Data.txt` file, which is located inside the `Saves` folder, and delete everything that is written in it, before you run the program

## installation
### 1. clone the repository
`git clone https://github.com/Rob-Ku/Softwareentwicklung-Projekt`
#### or download the [latest release](https://github.com/Rob-Ku/Softwareentwicklung-Projekt/releases) from github and unzip it in a folder

### 2. open the installation folder in your terminal
`cd Softwareentwicklung-Projekt`

### 3. install all dependencies
`dotnet restore`

## using the program
### 1. run the program
make sure that you are inside the installation folder <br>
then run the command: `dotnet run` <br>
_[alternatively you can run the command `dotnet run alldata` to get additional data in your output [(example)](https://cdn.discordapp.com/attachments/481454988002852897/869558605827960842/unknown.png)]_

### 2. input an image
`Image Name:` <br>
write the name of the image into the console (for example: `a.jpg`)

### 3. input the time the image was taken
`Image Time (Hour only [miltary time]):` <br>
write the time the image was taken in the german time zone and in the 24h format (for example: `21` or `2`)

### 4. input the date at which the image was taken
`Image Date (Day.Month.Year):` <br>
write the date at which the image was taken (for example: `25.07.2021`)

### 5. input the latitude of where the picture was taken
`Image Latitude (2 decimals):` <br>
write the latitude of the approximate location the image was taken (for example: `46.21`)

### 5. input the longitude of where the picture was taken
`Image Longitude (2 decimals):` <br>
write the longitude of the approximate location the image was taken (for example: `32.15`)

### 6. wait for the output
example output: <br>
![example output](https://cdn.discordapp.com/attachments/481454988002852897/869558403914166292/unknown.png)
