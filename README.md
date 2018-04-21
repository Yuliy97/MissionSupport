--- Mission Support Web and Mobile App Installation Guide ---

Pre-requisites:
You must be working on an internet-enabled device and have a web browser. You must have some familiarity with using terminal to run the applications.

Dependant Libraries: 
The web version of Mission Support was built with a NodeJS and MongoDB. Please install the following dependances below to be able to run the application in your local machine. Instructions for downloading the software can be found in the provided links. Please follow the steps before proceeding to download Instructions. 
  1) Install NodeJS with steps at https://docs.npmjs.com/getting-started/installing-node
  2) Install MongoDB with steps at https://treehouse.github.io/installation-guides/mac/mongo-mac.html

The mobile version of Mission Support was built with Xamarin Forms. The two main dependent libraries we used were .NET Standard projects and Portable Class Library (PCL).  See the link below for more descriptions.  You don’t have to manually install them as they are built in within visual studio.
https://docs.microsoft.com/en-us/xamarin/cross-platform/app-fundamentals/pcl?tabs=vsmac
 
Web Install Instructions: 
You will need to clone the git repository to your local machine. First open up terminal, and cd to a folder you want to place the code for Mission Support. The step below will clone a folder called MissionSupport to your current folder. 
  1) In terminal, type:
    git clone https://github.com/Yuliy97/MissionSupport.git
 
For the web application, you will need to install the following software: Nodemon and Angular. Follow the steps below to do this. 
  1) In terminal, go to the folder MissionSupport, then type:
    npm install -g nodemon
  2) In the same folder MissionSupport, also type:
    npm install -g @angular/cli
 
Web Build Instructions:
You will need to run these build instructions everytime you edit the code to ensure that the application updates the changes. Otherwise, you will only need to run the following instructions once to build the application.
  1) In terminal, go into the folder MissionSupport/web and type:
    npm install
  2) Then go to the folder MissionSupport/web/ms-src
    npm install
 
Web Run Instructions:
  1) Open up two terminal windows. 
  2) In first terminal window, cd into MissionSupport/web and type:
    nodemon
  3) In second terminal window, cd into MissionSupport/web/ms-src and type :
    ng serve
  4) Go to your browser window, go to the site http://localhost:4200 

This is where where the web application will run on your local host.

Web Troubleshoot Instructions:
If something is failing, try and run instructions from the Web Build instructions once again. A software dependency may have been updated that you need to install. 
  
Mobile Install Instruction:
For the mobile application, you will need to install the following softwares: Visual Studio, Xcode. Follow the steps below to do this. 
  1) In your browser, go to https://www.visualstudio.com/downloads/ and download the specify visual studio version that works with your computer. 
  2) You will also need a working version of Xcode for the iOS emulator to run on your computer.  Go to https://developer.apple.com/xcode/ and install the appropriate Xcode version if you don’t have one yet.
 
Mobile Build Instructions:
Once you have installed Visual Studio, you can use it to edit, build, and run the code on either iOS or Android emulators as they are already provided.  You will only have to edit the files in the main Mission Support folder as any changes made in these files will be cascaded to the iOS and Android version.
  1) In visual studio, click open then browse to the location of cloned repository folder then get into mobile folder, click the MissionSupport.sln file to open the project.
  2) Files to edit are mostly from the Model and View Folder within main Mission Support Folder.  The Model Folder contains the backend materials including a temporary local database.  The View Folder hold all .xaml files which are view files for the frontend and .xaml.cs files which are controllers for each of these view files.
  3) You might need to install and/or update a few Nuget packages as you continue to develop the application.  To add them, simply go to Project tab at top, then either select Add Nuget Packages or Update Nuget Packages accordingly.

Mobile Run Instructions:
After editing the code, you can directly run and test your application on Visual Studio using their Build function. 
  1) Select your desired emulator (iOS vs Android) and their appropriate versions (newest versions recommended).
  2) Then, run your application using the Build Button at the top of the screen (). 
  3) Once the emulator and application is launched, you can simply access and test the functionality of your app just like any other apps you’re using on your phone.

Mobile Troubleshoot Instructions:
 If the application is unable to build due to missing libraries, make sure that you’ve added and/or updated the according Nuget Packages within your app.  Also, don’t forget to update your Xcode.
