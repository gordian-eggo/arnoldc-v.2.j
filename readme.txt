NOTES FOR CMSC 124 ARNOLDC INTERPRETER:

ArnoldC.jar - the ArnoldC language pack for reference. Usage: ArnoldC [-run|-declaim] [FileToSourceCode]
			- pag na-run ko siya nang maayos update ko kayo via readme/messenger haha

11-14-18, 2014H: caught exception for MatchCollections on lines 40 and 41. Parang di pa niya kaya basahin yung 
				 newline character kaya null yung nirereturn ng match(). Di ko pa sure kung paano ayusin.

		  2031H: fixed contents of Program.cs, nadoble yung laman. Inayos din ibang variable names for readability and added regex for 		   			   variables.	

		  2252H: add variable regex and changed variable names for readability. 

11-15-18, 0229H: took the working parts for variable and function names from Waldo's code. Not a merge, more of an update.
  approx. 0317H: added better matches for macros and and integers. Integers can now be read next to the keywords YOU SET US UP, GET UP, and GET DOWN.
  		  0748H: encountered exceptions at lines 43 and 72, not sure how to fix it so ask Ma'am. Short description below. 
  		  		 Line 43: System.ArgumentException has been thrown parsing the contents of line 41.
  		  		 Line 72: System.ArgumentNullException has been thrown, value cannot be null.
  		  0801H: possible cause for bug found. The while loop that iterates through the document while data = sr.ReadLine() 
  		  		 is not null stops at YOU HAVE BEEN TERMINATED without having analyzed the line.

11-18-18, 1301H: running sample.arnoldc for reference while making the state diagram. Comments in section below.

		  1437H: finished making sample programs for reference re: ArnoldC syntax. 
		         To run, use the command: clear && java -jar ArnoldC.jar <filename>.arnoldc && java <filename>

11-21-18, 1737H: checked Waldo's code for lexical analyzer using C#'s dictionary. Bugs include:

				 > string literals are read as string literals and identifiers, e.g. "hello" is saved in the dictionary as a string literal and an identifier. Same thing happens for integer literals.

				 > variables are mistakenly saved as just identifiers. They have to be saved as "variable names"/"variable identifiers" to avoid confusion during syntax analysis (Fully fixed as of 2343H)

				 > variable names are allowed to have capital letters, ergo the variable ABC in lexemes.arnoldc should be read and saved as a variable (Fully fixed as of 2343H)

				 NOTE: variable names cannot have special characters like _/&/%/$ and so on. This counts as a bug, but the fix is a bit more advanced than we have time for at the moment.

		  1957H: falling out with Waldo is the last straw in a haystack of issues so will resume code alone. Adjusted variable names for  			   readability again, for the last time.
		  
		  2343H: Fixed the bug where variable names are allowed to have capital letters. Due to time constraints, I might have to assume that 		   the user will adopt a more or less conventional variable naming scheme far from the likes of h4xx0r style.

		  2352H: Partially fixed the bug where integer literals are read as integer literals and identifiers. Moved the conditional loops for 		   it into pre-existing keyword conditional loops.

		  0007H: Encountered another bug. The fix for the first bug where you move conditional loops into the pre-existing keyword loops result 	   in the keyword being written to the dictionary twice. (Fixed as of 0009H)

		  0037H: Having trouble getting the regex for the macros correct. The @ character isn't getting read. But I will not lose hope, for my 		   roommate bought me McDonalds. Additionally, it might need the use of the "\p{}" regex using the Unicode value for @

11-22-18, 1009H: fixed while loop regexes such that the contents can be added to the dictionary of lexemes.

		  1021H: error pushing updated files to my branch. Saved a copy in a different location. (Fixed as of 1051H)

		  1119H: added functionality for boolean operations.

		  1227H: Finally adjusted project for GUI. Original files were somehow made in a different way from what was taught in lab so I moved 		   the contents of the original files into the new Gtk project. Will create a separate repo for this.

		  1240H: holy [deleted] naksimula ako ng GUI I feel the power of technology [deleted]

		  1403H: Monodevelop bug prevents me from progressing further on UI for now so will continue lexical and start on syntax as best as I 		   can. Bandaided the false macro bug, removed @ for the time being but will bring up the issue with ma'am. Presentation has been 		 moved to 1510H.

		  1408H: Closer inspection of the project specifications state that I've covered the required features. I am delighted. Lexical 			   analyzer officialy finished at 1408H.

		  1448H: Lexical analyzer is buggy. I cry. Bugs include:

		  		 > part of some strings are read as variable names

		  		 > adjusting else-if regexes result in the else-if keywords BULLSHIT and YOU HAVE NO RESPECT FOR LOGIC being put in the 		  dictionary twice. (Fixed as of 1453H)

		  		 > BECAUSE I'M GOING TO SAY PLEASE isn't being read. (Fixed as of 1453H)

11-28-18, 1920H: Improved the UI and added the first functionality for the Choose File button. 

		  2000H: Removed functionality for Choose File button due to an error that I didn't know how to debug. Fixed button and label sizes. 		   Not entirely sure how to implement some functionalities but [deleted] that. 

11-29-18, 0344H: Tried to make code block for addressing the last unresolved error found on 11-22-18 at 1448H. Did not work.

		  0412H: Successfully made separate class for lexical analyzer. Will probably have to put the syntax and semantic analyzer in that 	 		same class due to time constraints. Will also have to work around the last error that I mentioned.

		  0509H: Tried to fiddle with it more. Still not working. Naiirita na ako tbh.
		  0519H: Ayaw niya talaga mumsh. One last try, tas magi-iba na ako ng game plan.
		  0534H: Mumsh ayaw talaga. Aayusin ko na lang gamit syntax analyzer, tutal inuupdate naman nun yung symbol table.

		  1200H: Improved the Lexer class. It now has a method that will return a dictionary of lexemes for the syntax analyzer.



COMMENTS ON SYNTAX:

	> Variable names with underscores and possibly other special characters are not allowed.
	> YOU SET US UP doesn't work with string variables
	> free-form positioning
	> the GET TO THE CHOPPER loop completely erases and replaces the value in a variable with a new value defined inside the loop
	> if you do a comparison inside the GET TO THE CHOPPER loop, for example: "temp = 4 > 5" and then print it, it will print either 1(true) or 0(false)
	> if statements are harder, will need more time to make a suitable state diagram for them
	> structure for if-else statement:
		1. initialize a variable with a value
		2. (optional) GET TO THE CHOPPER loop
		3. if/if-else statement
	> if the value on BECAUSE I'M GOING TO SAY PLEASE is greater than 0, it will execute the if-statement
	> if there's no GET TO THE CHOPPER loop, it will automagically execute the if-statement and ignore the "else" art if there is one
	> the value of the condition statement has to be calculated beforehand
	> initialize all variables that you're going to use
	> you have to compare the value for the while loop to a different value in a different variable
	> you can't put a GET TO THE CHOPPER loop and/or a HEY CHRISTMAS TREE variable declaration inside a previous GET TO THE CHOPPER loop
	> you can't put a print statement between HEY CHRISTMAS TREE and YOU SET US UP
	> you can't put if statements/if-else statements inside a GET TO THE CHOPPER loop