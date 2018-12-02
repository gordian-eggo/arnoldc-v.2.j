using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ArnoldCinterpreter {

    public class Lexer {

	    StreamReader arnoldc_reader = new StreamReader("../../lexemes.arnoldc");
	    Dictionary<int, Tuple<string, string>> lexeme_dict = new Dictionary<int, Tuple<string, string>>();
	    int i = 1;          // fuse this later as an index for putting lexemes into lexeme_dict	

	    // these are the regexes for matching later on. I categorized them by usage so they're easier to keep 
	    // track of and to avoid one massive block of code
	    Regex keyword_regex = new Regex(@"\bIT'S SHOWTIME\b|\bYOU HAVE BEEN TERMINATED\b|\bTALK TO THE HAND\b
	                        |\bI LET HIM GO\b|\bLISTEN TO ME VERY CAREFULLY\b|\bGIVE THESE PEOPLE AIR\b
	                        |\bI NEED YOUR CLOTHES YOUR BOOTS AND YOUR MOTORCYCLE\b
	                        |\bI'LL BE BACK\b|\bHASTA LA VISTA, BABY\b|DO IT NOW\b|\bGET YOUR ASS TO MARS\b
	                        |\bI WANT TO ASK YOU A BUNCH OF QUESTIONS AND I WANT TO HAVE THEM ANSWERED IMMEDIATELY\b
	                        |\bWHAT THE FUCK DID I DO WRONG\b");	

	    Regex reassign_variable = new Regex(@"\bGET TO THE CHOPPER\b|HERE IS MY INVITATION\b|\bENOUGH TALK\b");
	    Regex assign_variable = new Regex(@"\bHEY CHRISTMAS TREE\b|YOU SET US UP\b");
	    Regex arithmetic_operations_regex = new Regex(@"\bGET DOWN\b|\bGET UP\b|
	                        |\bYOU'RE FIRED\b|\bHE HAD TO SPLIT\b");	

	    Regex boolean_ops_regex = new Regex(@"\bLET OFF SOME STEAM BENNET\b|\bYOU ARE NOT YOU YOU ARE ME\b
	                            |\bCONSIDER THAT A DIVORCE\b|\bKNOCK KNOCK\b");	

	    Regex print_regex = new Regex(@"\bTALK TO THE HAND\b");	
	    Regex if_loop_regex = new Regex(@"\bBECAUSE I'M GOING TO SAY PLEASE\b");
	    Regex else_if_loop_regex = new Regex(@"\bBULLSHIT\b|\bYOU HAVE NO RESPECT FOR LOGIC\b");	
	    Regex macro_regex = new Regex(@"\bI LIED\b|\bNO PROBLEMO\b");
	    
	    Regex while_regex = new Regex(@"\bSTICK AROUND\b");
	    Regex end_while_regex = new Regex(@"\bCHILL\b");
	    Regex integer_regex = new Regex(@"\b\d+\b");
	    Regex variable_name_regex = new Regex(@"([a-zA-Z0-9]*[a-z0-9]+)");	

	    public Dictionary<int, Tuple<string, string>> Lexical_analyzer() {

	    	string data_string = arnoldc_reader.ReadLine();	    	

	    	while (data_string != null) {       // iterates through the strings in the file

                	MatchCollection string_literals = Regex.Matches(data_string, @"""(.*?)""");
	
                	Match keyword_match = keyword_regex.Match(data_string);
                	Match rv_match = reassign_variable.Match(data_string);
                	Match av_match = assign_variable.Match(data_string);
                	Match math_ops_match = arithmetic_operations_regex.Match(data_string);
                	Match bool_ops_match = boolean_ops_regex.Match(data_string);
	
                	Match if_statement_match = if_loop_regex.Match(data_string);
                	Match else_if_match = else_if_loop_regex.Match(data_string);
	
                	Match while_match = while_regex.Match(data_string);
                	Match end_while_match = end_while_regex.Match(data_string);
	
                	Match print_match = print_regex.Match(data_string);
                	Match integer_match = integer_regex.Match(data_string);
                	Match variable_name_match = variable_name_regex.Match(data_string);
                	Match macro_match = macro_regex.Match(data_string);
	
                	if (end_while_match.Success) {      // separated CHILL from STICK AROUND so if a user decides to put anything else
                                                    	// after CHILL it won't be read            
                    	var end_while_token = Tuple.Create("Keyword", end_while_match.Value);
                    	lexeme_dict.Add(i, end_while_token);
                    	i = i + 1;
	
                	}
	
                	if (while_match.Success) {                  // matches lexemes inside while loops
	
                    	var token_input = Tuple.Create("Keyword", while_match.Value);
                    	lexeme_dict.Add(i, token_input);
                    	i = i + 1;
	
                    	if (variable_name_match.Success) {      // put this here to catch the variable name after STICK AROUND
	
                        	var variable_token = Tuple.Create("Variable identifier", variable_name_match.Value);
                        	lexeme_dict.Add(i, variable_token);
                        	i = i + 1;
	
                    	}
	
                	}
	
                	if (keyword_match.Success) {        // for the other uncategorized keywords
	
                    	var token_input = Tuple.Create("Keyword", keyword_match.Value);
                    	lexeme_dict.Add(i, token_input);
                    	i = i + 1;
	
                	}
	
                	if (print_match.Success) {      // same implementation as the while functionality. It's also used for the other functionalities.
	
                    	var print_token = Tuple.Create("Keyword", print_match.Value);
                    	lexeme_dict.Add(i, print_token);
                    	i = i + 1;
	                    
                    	if (integer_match.Success) {
	
                        	var integer_token = Tuple.Create("Integer literal", integer_match.Value);
                        	lexeme_dict.Add(i, integer_token);
                        	i = i + 1;
	
                    	} else if (variable_name_match.Success) {
	
                        	var variable_token = Tuple.Create("Variable identifier", variable_name_match.Value);
                        	lexeme_dict.Add(i, variable_token);
                        	i = i + 1;
	
                    	} else if (macro_match.Success) {
	
                        	var macro_token = Tuple.Create("False macro", macro_match.Value);
                        	lexeme_dict.Add(i, macro_token);
                        	i = i + 1;
	
                    	}
	
                	}
	
                	if (av_match.Success) {
	
                    	var token_input = Tuple.Create("Keyword", av_match.Value);
                    	lexeme_dict.Add(i, token_input);
                    	i = i + 1;
	
                    	if (integer_match.Success) {
                        	var integer_token = Tuple.Create("Integer literal", integer_match.Value);
                        	lexeme_dict.Add(i, integer_token);
                        	i = i + 1;
                    	} else if (variable_name_match.Success) {
                        	var variable_token = Tuple.Create("Variable identifier", variable_name_match.Value);
                        	lexeme_dict.Add(i, variable_token);
                        	i = i + 1;
                    	} else if (macro_match.Success) {
                        	var macro_token = Tuple.Create("False macro", macro_match.Value);
                        	lexeme_dict.Add(i, macro_token);
                        	i = i + 1;
                    	}
	
                	}
	
	
                	if (rv_match.Success) {
	
                    	var token_input = Tuple.Create("Keyword", rv_match.Value);
                    	lexeme_dict.Add(i, token_input);
                    	i = i + 1;
	
                    	if (integer_match.Success) {
	
                        	var integer_token = Tuple.Create("Integer literal", integer_match.Value);
                        	lexeme_dict.Add(i, integer_token);
                        	i = i + 1;
	
                    	} else if (variable_name_match.Success) {
	
                        	var variable_token = Tuple.Create("Variable identifier", variable_name_match.Value);
                        	lexeme_dict.Add(i, variable_token);
                        	i = i + 1;
	
                    	} else if (macro_match.Success) {
	
                        	var macro_token = Tuple.Create("False macro", macro_match.Value);
                        	lexeme_dict.Add(i, macro_token);
                        	i = i + 1;
	
                    	}                  
	
                	}
	
                	if (bool_ops_match.Success) {
                    	var bool_input = Tuple.Create("Keyword", bool_ops_match.Value);
                    	lexeme_dict.Add(i, bool_input);
                    	i = i + 1;
	
	
                    	if (integer_match.Success) {
	
                        	var integer_token = Tuple.Create("Integer literal", integer_match.Value);
                        	lexeme_dict.Add(i, integer_token);
                        	i = i + 1;
	
                    	} else if (variable_name_match.Success) {
	
                        	var variable_token = Tuple.Create("Variable identifier", variable_name_match.Value);
                        	lexeme_dict.Add(i, variable_token);
                        	i = i + 1;
	
                    	}
	                    
                	}
	
                	if (math_ops_match.Success) {
	
                    	var token_input = Tuple.Create("Keyword", math_ops_match.Value);
                    	lexeme_dict.Add(i, token_input);
                    	i = i + 1;
	
                    	if (integer_match.Success) {
	
                        	var integer_token = Tuple.Create("Integer literal", integer_match.Value);
                        	lexeme_dict.Add(i, integer_token);
                        	i = i + 1;
	
                    	} else if (variable_name_match.Success) {
	
                        	var variable_token = Tuple.Create("Variable identifier", variable_name_match.Value);
                        	lexeme_dict.Add(i, variable_token);
                        	i = i + 1;
	
                    	}
	
                	}
	
                	if (if_statement_match.Success) {
	
                    	var token_input = Tuple.Create("Keyword", if_statement_match.Value);
                    	lexeme_dict.Add(i, token_input);
                    	i = i + 1;
	
                    	if (integer_match.Success) {
	
                        	var integer_token = Tuple.Create("Integer literal", integer_match.Value);
                        	lexeme_dict.Add(i, integer_token);
                        	i = i + 1;
	
                    	} else if (variable_name_match.Success) {
	
                        	var variable_token = Tuple.Create("Variable identifier", variable_name_match.Value);
                        	lexeme_dict.Add(i, variable_token);
                        	i = i + 1;
	
                    	}
	
                	}
	
                	if (else_if_match.Success) {
                    	var token_input = Tuple.Create("Keyword", else_if_match.Value);
                    	lexeme_dict.Add(i, token_input);
                    	i = i + 1;
                	}
	
                	foreach (Match string_literal in string_literals) {         // catches string literals
                    	var token_input = Tuple.Create("String literal", string_literal.Groups[1].Value);
                    	lexeme_dict.Add(i, token_input);
                    	i = i + 1;
                	}
	
                	data_string = arnoldc_reader.ReadLine();
            }	

            // uncomment to check if symbol table's complete
            // foreach (KeyValuePair<int, Tuple<string, string>> item in lexeme_dict) {
            //     Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            // }

            // make a copy of the original lexeme dictionary and use that to edit the original dictionary
            Dictionary<int, Tuple<string, string>> error_fix_copy = new Dictionary<int, Tuple<string, string>>(lexeme_dict);

            // removing the error using the dictionary copy
            foreach (KeyValuePair<int, Tuple<string, string>> item in error_fix_copy) {
                if ((item.Value.Item2 == "TALK TO THE HAND") && (error_fix_copy[item.Key + 2].Item1 == "String literal")) {
                    if (error_fix_copy[item.Key + 1].Item1 == "Variable identifier") {
                        try {
                            lexeme_dict.Remove(item.Key + 1);
                        } catch (Exception e) {
                            Console.WriteLine(e);
                        }
                    }

                }
            }

            // re-declare the copy with the changed dictionary and a new, empty dictionary
            Dictionary<int, Tuple<string, string>> numbering_fix_copy = new Dictionary<int, Tuple<string, string>>(lexeme_dict);
            Dictionary<int, Tuple<string, string>> final_lexeme_dict = new Dictionary<int, Tuple<string, string>>();

            // use this for loop to put the edited values in the final lexeme dictionary
            for (int j = 0; j < numbering_fix_copy.Count; j++) {
                string lexeme = numbering_fix_copy[numbering_fix_copy.Keys.ElementAt(j)].Item1;
                string lexeme_value = numbering_fix_copy[numbering_fix_copy.Keys.ElementAt(j)].Item2;
                var token = Tuple.Create(lexeme, lexeme_value);

                final_lexeme_dict.Add(j, token);
            }

            foreach (KeyValuePair<int, Tuple<string, string>> item in final_lexeme_dict) {
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
            }

            return final_lexeme_dict;

	    }

    }
    
}
