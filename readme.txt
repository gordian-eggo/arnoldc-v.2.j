NOTES FOR CMSC 124 ARNOLDC INTERPRETER:

ArnoldC.jar - the ArnoldC language pack for reference. Usage: ArnoldC [-run|-declaim] [FileToSourceCode]
			- pag na-run ko siya nang maayos update ko kayo via readme/messenger haha

11-14-18, 2014H: caught exception for MatchCollections on lines 40 and 41. Parang di pa niya kaya basahin yung 
				 newline character kaya null yung nirereturn ng match(). Di ko pa sure kung paano ayusin.

		  2031H: fixed contents of Program.cs, nadoble yung laman. Inayos din ibang variable names for readability and added regex for 		   			   variables.	

		  2252H: add variable regex and changed variable names for readability. 

11-15-18, 0229H: took the working parts for variable and function names from Waldo's code. Not a merge, more of an update.
  approx. 0317H: added better matches for macros and and integers. Integers can now be read next to the keywords YOU SET US UP, GET UP,					   and GET DOWN.
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

		  1252H: Parser can now (properly?) detect main method keywords IT'S SHOWTIME and YOU HAVE BEEN TERMINATED. Based process off of Team 		 PLA LOLcode interpreter. Checked https://www.geeksforgeeks.org/symbol-table-compiler/ and it doesn't say that this knowledge 		 should be add to the symbol table so will leave it alone for now.

		  1311H: Tried to make a master parse_file() function that will do all parser methods in one function. Not sure if it's working or 		   infinite looping so will leave it alone for the meantime. As for string/variable identifier error mentioned earlier, might 		   just ignore it? Wil explain the logic at a later date.

		  1343H: Error in the arithmetic parser, but nothing a bit more time won't fix.

12-1-18, 1024H: Beginning proper work a day late, didn't finish reviewer on time, too tired. Ma'am suggests that I save each string then 					check if it's a variable identifier or something like that, but no luck on my end. Will have to try either skirting around the 				   wrong line in the lexeme_dict (which is going to take up a lot of code) or making an algorithm to remove the offending lines				    from the lexeme_dict. I think the latter will be more effective. No progress on UI yet. If I'm feeling my way anywhere, it's 				 with the UI.

		 1042H: Figured out how to access individual dictionary entries. The latter option of making an algorithm to remove offending lines 	   just might be feasible.

		 1100H: Encountered SystemInvalidOperationException while removing the offending line from the dictionary. Researched possible cures 		and found one suggesting that I make a copy of the dictionary then iterate over that, and remove the target from the original 		 dictionary once it's found in the copy. Link in references section.

		 1110H: Successfully removed the offending line, ready to test with parser.

		 1121H: Fix works well enough so far, but will have to adjust and debug parser methods. Won't check "debug lexical analyzer error" off the to-do list just yet, though. I suspect there's a catch.

		 1217H: Came back from food break to an error in main_method(). Wrong item gets written to the expression list. Must be remedied.

		 1232H: I found the catch I was talking about earlier. I'll have to update the original copy of the lexeme dictionary to handle the methods in the parser.

12-2-18, 0826H: Can't change the value of a key in a dictionary, so will have to edit the entire thing before putting it in the dictionary. 				[deleted] this pero [deleted] Ateneo more huhu ba't sila ganyan.
	
		 0856H: FIXED THE LEXICAL ANALYZER ERROR WOOO GO UP MBT KUNG NAIRAOS NIYO KAYA KO RIN WOOO okay back to work.

		 1205H: Reset program version but forgot to copy of readme.txt hays. Having trouble accessing lists in lists.

		 1253H: Successfully added assign_var() functionality. I might cry pero konti lang.

		 1341H: Found new error while fixing talk_to_the_hand(). Strings like "3" are being read as integer literals.

		 1350H: Fixed the error. Work continues.

		 1354H: Successfully added talk_to_the_hand() functionality. 

		 1410H: Food break. Currently working on reassign_variable() functionality because it needs to be in order before the arithemtic_ops() 		  functionality can get anywhere.

		 1531H: Finished reassign_variable() functionality! [deleted] baka keri nga

		 1604H: Finished arithmetic_ops() functionality! [deleted] iiyak na talaga ako baka keri nga. 
		 1605H: Notes for what I have left to finish (because I might forget by the time I come back):
		 			> function in Parser.cs to update the symbol table
		 			> start semantic analyzer yay!!
		 			> more work on UI

12-3-18, 0718H: Slept too long. Implemented the update_symbol_table() function in Parser.cs. Currently it reads assigned variables.

12-4-18, 0608H: Found issues with syntax analyzer that'll make it harder to work with semantic analyzer. Been working on them since approx. 				0530H.

		 0821H: Fixed the first half of the issues with the syntax analyzer. Pwede na ako pumunta ng 8:30 exam with a clear conscience.

		 1209H: Separated expressions into re/assignment, print statement, and arithmetic lists. If the main method is wrong from the start, 		the program should produce an error. If not then it will continue to parse the file. Just have to adjust arithmetic expression 		  reading and the syntax analyzer should be truly ready.

		 1255H: Found yet another bug in the reassign_variable() function, but after fiddling with it for nearly an hour I think the semantic 		 analyzer can do a better job of fixing it.

		 1325H: Syntax analyzer can now detect equations. 

		 1552H: Semantic analyzer should more or less know which statements to execute in what order. Very very basic.



ERROR LIST:

	(1) In main_method(), in Parser.cs: Wrong item getting written to expression list. [Fixed as of 0857H, 12-2-18]
	(2) Missing keys in dictionary due to deletion. [Fixed as pf 0857H, 12-2-18]
	(3) Strings like "3" are being read as integer literals. [Fixed as of 1350H, 12-2-18]



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

References:

	https://stackoverflow.com/questions/15057712/why-am-i-getting-an-exception-invalidoperationexception
	https://www.dotnetperls.com/copy-dictionary (for individual questions regarding C# methods and syntax)
	https://stackoverflow.com/questions/1937847/how-to-modify-key-in-a-dictionary-in-c-sharp
	https://www.monodevelop.com/documentation/stetic-gui-designer/
	https://www.monodevelop.com/documentation/building-a-simple-application-using-the-stetic-gui-designer/
	https://www.youtube.com/watch?v=0P82vSqvt9k (VB and CSharp Create simple console and GTK apps in Monodevelop)

Unused References (delete thiese later):

	https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/index?fbclid=IwAR3ZT_6JvGzoysRxgStT92l6Iww-Q6O7CiUf33A0XSjxhPX4xRl4LiPBMNY
	https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/index?fbclid=IwAR11ExU9QK7-5v7AM49ig9ipVIjhsKj-7IWF-vaLzX8zKvR8XAMAsFpEq8c
	https://docs.microsoft.com/en-us/dotnet/csharp/index?fbclid=IwAR088o7-rvCGgZUX6sHedW-x8cISJ1M25w2kjCvbFFpdN_UxPaIBm6dMeLU
	https://gist.github.com/sanmadjack/be06c8cf8a6de1632ee3?fbclid=IwAR187hT7XNuBkDkuv3aKN8H9_1GZy4dKcfAmp33PY5ZqIX46ZSJa6ifUo6A
	https://stackoverflow.com/questions/20612468/making-gtk-file-chooser-to-select-file-only?fbclid=IwAR2eFM3O2r_mtZEVK-Qj9FTft37aXjd9-fiLUM271r6UYfkxQw-qpSETrQw