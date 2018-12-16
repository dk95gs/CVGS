`https://dk95lp@bitbucket.org/dk95lp/cvgs-iteration_1.git`
## ***IMPORTANT*** HOW TO GET SOLUTION WORKING ON SCHOOL COMPUTERS
If you are working on a computer at school you must follow the following steps to get it to work.
School computers need to be updated

1. Go to https://www.microsoft.com/net/download
1. Under .NET Core 2.1 click on the `Download .NET Core SDK` to download the installer 
3. Use the installer and you are good to go!
---

## Quick Start Guide

Please follow these steps to get the solution working.

1. Open Git Bash and move to the directory you will be working in (Example, the desktop) - `cd desktop`
2. Now clone the repo
3. Navigate to the cloned repo in Git Bash - `cd cvgs-iteration_1`
4. Now checkout your branch to start working (Replace BRANCH with the name of the branch you are checking out - `git fetch && git checkout [BRANCH]` 
5. Complete the steps bellow to get the data base working
---
## MUST DO TO GET DATABASE WORKING

1. Open the NuGet Package Manager Console by going to Tools -> NuGet Package Manager -> Package Manager Console
2. Enter `update-database`
3. Now you should be good to go!
4. Open Microsoft SQL Server Management Studio 17
5. Enter in `(localdb)\mssqllocaldb` in the server name section
6. Select Windows Authentication for the database Authentication
---
## Useful UML diagram examples
https://conestoga.desire2learn.com/d2l/le/content/63593/fullscreen/2616307/View 

## How to commit & push your code when finished 
1. Navigate to the project - `cd cvgs-iteration_1`
2. Type the command - `git add .`
3. Type the command (MESSAGE can be anything, preferably something meaningful to the changes you have made) - `git commit -m "MESSAGE"`
4. Type the command - `git push origin`
5. You branch has now been pushed and can be review