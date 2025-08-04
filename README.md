# Your Team Name Here

This is the team repository for SU25_Team5.

## Personal Retrospective
As the team manager and lead I had to balance betweeen designing the system and leading people with different skill levels, viewpoints, and priorities.
I created a minimal app skeleton, i.e. I added classes in support of the architecture and design but didn't provide implementations for most of them, in
order to give my team an opportunity to learn because this is an academic setting. I knew that it would be challenging to touch a tech stack that they
likely had little experience with, and learning a design pattern (MVVM) that they likely had not used before.
While I pivoted many times to adapt to team skill and knowledge limitations, including that I reduced the scope of tickets and made them much smaller, it
would have been better if I had planned the work to start with completely decomposed features and built on them iteratively, ex. make a name field work end-to-end,
then add a description field, and so on.

Where I could have done better is scheduling a 1 to 1 with each team member during the first weeks of the project in order to observe and feel out their
strengths, weaknesses, and proclivities, and to give them an opportunity to ask questions directly. From this I could have better balanced giving them a challenge
and opportunity to grow in an academic setting, with moving the project forward by cultivating their strengths.
Another area I could have done better is focusing on decomposition of features and iterating on them; it is better to have 5 fields that fully work than 10 fields
that only work partially or not at all.

## Project

### Project Name
Meal Brain

### Project Description  
Describe the problem solved and/or what the project is doing

This project solves the problem of meal planning and prepping being tedious, time consuming and complicated by simplifying the process. 
By storing digital versions of recipe cards, allowing the creation of meal plans from said cards, and generation of grocery lists from said meal plans, 
this application will simplify meal prepping and planning for all users, especially large families.

This project also solves the problem of keeping a large catalog of physical
cards and making sure you have enough ingredients to make them.

Meal Brain allows users to create digitial recipe cards.

Users can then create weekly meal plans by selecting from their recipe cards, and
generate grocery lists with the total amount of each ingredient needed.

#### Stretch Goal
Users can register for a cloud account which will allow them to sync their
recipes across devices.

## Team

Removed per instructor directive

## Prerequisites

List tech stack (including version if possible) on both backend and frontend (Database).

Visual Studio 2022 v17.14.1

.Net 8

Sqlite-net <br/>
https://learn.microsoft.com/en-us/training/modules/store-local-data/

https://github.com/praeclarum/sqlite-net/wiki/Features#features

Sqlite-net Extensions <br/>
https://bitbucket.org/twincoders/sqlite-net-extensions/src/master/

SqliteBrowser <br/>
https://sqlitebrowser.org/

.Net MAUI <br/>
https://learn.microsoft.com/en-us/dotnet/maui/?view=net-maui-9.0&WT.mc_id=dotnet-35129-website

.Net MAUI Architecture free PDF (link on page) <br/>
https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm

.Net MAUI Community Toolkit <br/>
https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/

.Net Datasync Community Toolkit <br/>
https://communitytoolkit.github.io/Datasync/tutorial/server/part-1/

NUnit 3
https://nunit.org/

Moq
https://www.nuget.org/packages/Moq

Appium/Selenium/UI Automation (Stretch goal)
https://learn.microsoft.com/en-us/samples/dotnet/maui-samples/uitest-appium-nunit/

## Set Up and Installation

Details on how to set up the project follow.

Install the following tools:

Visual Studio 2022 v17.14.1

SqlLite (this may be bundled in the repo code)

Git

## Running the application
This will basically be run the .net maui app for the windows configuration

Open up system settings on your computer,
Select Update & Security -> For Developers,
Enable Developer Mode, Click Yes,
In Visual Studio select the MealBrain launch profile,
Press F5 or click the Start button

Do not forget to disable Developer Mode when done.
