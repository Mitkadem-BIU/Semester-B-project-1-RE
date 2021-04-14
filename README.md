## Readme File - Semester B Advanced Programming Project

#### 1) General Explanation
This program's project is made to simulating a short segment of an airplane flight, showing its control bar in sync with the location of the plane. In addition to that, multiple graphs show corellations between variables like time, speed, elevator, ruttle ,etc. By inserting 2 relevant CSV files, one for normal learning and one for anomaly detection respectively, the program can fullfill its purposes.

#### 2) The Project's Structure
According to the MVVM data structure for Desktop apps, the code files in this project are divided into three folders: models, view models and views. That was done in order to represent the files in the same order of the flow of information of the program.
The main parts are the MainControllerModel, MainControllerViewModel and the NewWindow which is the main view.
Besides those, there are other MVVM threesomes for other secondary parts of the app, such as: plot view, joystick...
In addition, there are several directories which have no code in them:
* Planning and Design: some documents regarding the design of the project and the steps we took in the way to the final product.
* Plugins: dlls of different algorithms. More about that later.

#### 3) Prerequisite
Pre-aquired programs to be unstalled before initial running:
##### FlightGear
The software can be downloaded (for free) via this link: https://www.flightgear.org/download/
We used the 2020.3.6 release while our developement.
The installation is quite simple. There are instructions in FlightGear's website.
##### C# Libraries
We used two C# Libraries whose dependencies we had to manualy add:
* `OxyPlot` which is used for plotting graphs
* `LumenWorks.Framework.IO.Csv` for reading the CSV files.

#### 4) Installation instructions and initial running:
##### FlightGear
First, you have to install FlightGear from the link above.
Open the FlightGear app. It should look like this:

![image](https://user-images.githubusercontent.com/62245924/114727207-af584000-9d46-11eb-8eb8-76a6bbae4136.png) <width="100" height="100">

Press the Setting button on the left and scroll down to Additional Settings:

![image](https://user-images.githubusercontent.com/62245924/114727406-da429400-9d46-11eb-9c5e-d2479f258e2f.png)

Most of the settings are replacable since we rely on them in our program. Some of them however can be replaced according to your wish:
`--generic=socket,in,10,<ip to listen to>,<port to listen to>,tcp,playback_small
--fdm=null`
You can go back to the Main Page by pressing the house button. You can fly the plane if you wish by pressing the Fly button, but since we want the plane to listen to the port, nothing would happen for now.
##### Get the Project
Clone the project to your computer using `git clone` command. Note that the Visual Studio project is inside the `AdvancedProgrammingProject1` Directory.
Open the project and add the needed dependency mentioned above.
##### Run the App
Run pressing the Start button in Visual Studio. the following window would open:

![image](https://user-images.githubusercontent.com/62245924/114729684-e29bce80-9d48-11eb-83e4-6cfd5f5a7b4d.png)

Fill it with the following data:
* XML file: the same `playback_small.xml` setting file which was supported in the FlightGear settings
* Learning CSV File: file to be sent to the detection algorithm. The data in this file would not be shown
* Anomaly CSV File: file with data from anomaly run. This flight would be broadcasted in our app and would be inspected as you wish
* IP and Port: Address to which the FlightGear listens. Press `Connect` before you press `Save` and make sure conncetion was made
Note that the CSV files sould be accepted **without column names**. It would not be tested and might crash the app.
If everything works, the following window should open:

![image](https://user-images.githubusercontent.com/62245924/114730630-b7fe4580-9d49-11eb-9de7-5517cc5adf52.png)

You can pick different attributes from the list and see them and the one who's most similar. You can also go back and forth in time and change the speed. In the right you can see a graphical review of specific attributes. Of course, you can see this entire time the course of the flighing of the plane, in the FlightGear app (which should stay on!).
On the buttom-right, you can load different anomaly file or load an anomaly detector dll. We provide 2 dlls with basic ablities but you can add some more as you wish. Notice the have to fit the `IAlgorithm` interface and also, the name of the namespace should be ? and the name of the class should be the name of the file.

#### 5) Further Documentation
Link to documentation about the main classes: Here is the link: (add link!)

#### 6) Showing Video
link to the 5~ minutes video
