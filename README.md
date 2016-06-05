# RecordProcessor

## Input
A record consists of the following 5 fields: last name, first name, gender, date of birth and favorite color. The input is 3 files, each containing records stored in a different format. You may generate these files yourself, and you can make certain assumptions if it makes solving your problem easier.
* The pipe-delimited file lists each record as follows: 
  * LastName | FirstName | Gender | FavoriteColor | DateOfBirth
* The comma-delimited file looks like this: 
  * LastName, FirstName, Gender, FavoriteColor, DateOfBirth
* The space-delimited file looks like this: 
  * LastName FirstName Gender FavoriteColor DateOfBirth

You can run the application via the command line by passing in 3 file paths to the files containing records along with a sort option 1 thru 3: 

![Image of Args](https://raw.githubusercontent.com/RyanFerretti/RecordProcessor/master/console.PNG)


  
**Assumptions**
* All record data contained in the files contains valid data.  No fields are empty and dates are in the format M/D/YYYY.
* The project's base directory is C:\RecordProcessor
  * If you are not running under this directory you will need to update paths to the record files to get the Api working.
  * You can change the paths in the `Global.asax.cs` file.  Find the field `RecordPaths` and update to your current base path.
  
## Output
Create and display 3 different views of the data you read in:
* Output 1 – sorted by gender (females before males) then by last name ascending.
* Output 2 – sorted by birth date, ascending.
* Output 3 – sorted by last name, descending.

## API
Within the same code base, build a standalone REST API with the following endpoints:
* POST /records - Post a single data line in any of the 3 formats supported by your existing code
* GET /records/gender - returns records sorted by gender
* GET /records/birthdate - returns records sorted by birthdate
* GET /records/name - returns records sorted by name

It's your choice how you render the output from these endpoints as long as it well structured data. These endpoints should return JSON.
