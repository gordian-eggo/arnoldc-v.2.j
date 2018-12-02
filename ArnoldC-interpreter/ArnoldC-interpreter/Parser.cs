using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ArnoldCinterpreter {

    public class Parser {

    	Dictionary<int, Tuple<string, string>> symbol_table = new Dictionary<int, Tuple<string, string>>();
    	List<List<string>> program_expressions = new List<List<string>>();
    
		bool valid_main_method;

		public List<List<string>> get_program_expressions() {
			return program_expressions;
		}

		// finished!
		public void main_method(Dictionary<int, Tuple<string, string>> lexeme_collection) {

			List<string> main_method_expr = new List<string>();
			int lexeme_count = lexeme_collection.Count();

			foreach (KeyValuePair<int, Tuple<string, string>> lexeme in lexeme_collection) {

				if (lexeme.Key == 0) {
					main_method_expr.Add(lexeme.Value.Item2);
				} else if (lexeme.Key == (lexeme_count - 1)) {
					main_method_expr.Add(lexeme.Value.Item2);
				}
			}

			if (main_method_expr.Contains("IT'S SHOWTIME") && main_method_expr.Contains("YOU HAVE BEEN TERMINATED")) {
				valid_main_method = true;
				program_expressions.Add(main_method_expr);
			} else if (!main_method_expr.Contains("IT'S SHOWTIME")) {
				Console.WriteLine("Error: Invalid main method. Missing keyword IT'S SHOWTIME.");
			} else if (!main_method_expr.Contains("YOU HAVE BEEN TERMINATED")) {
				Console.WriteLine("Error: Invalid main method. Missing keyword YOU HAVE BEEN TERMINATED.");
			}

		}    	

		// finished!
		public void assign_variable(Dictionary<int, Tuple<string, string>> lexeme_collection) {

			List<List<string>> temp = new List<List<string>>();
			List<string> assign_var = new List<string>();
			
			string temp_str;
			// size is 4 because variable assignment in ArnoldC needs
			// both HEY CHRISTMAS TREE and YOU SET US UP to function
			// so I thought it might be easier on the semantic analyzer later on if 
			// they came together in one expression.
			int expression_size = 4;	

			foreach(KeyValuePair<int, Tuple<string, string>> lexeme in lexeme_collection) {

				if (lexeme.Value.Item2 == "HEY CHRISTMAS TREE") {
					temp_str = lexeme.Value.Item2;
					assign_var.Add(temp_str);

					if (lexeme_collection[lexeme.Key + 1].Item1 == "Variable identifier") {
						temp_str = lexeme_collection[lexeme.Key + 1].Item2;
						assign_var.Add(temp_str);
					} else {
						Console.WriteLine("Error: missing variable.");
						break;
					}

					if (lexeme_collection[lexeme.Key + 2].Item2 == "YOU SET US UP") {
						// Console.WriteLine("correct syntax!");
						assign_var.Add(lexeme_collection[lexeme.Key + 2].Item2);

						if (lexeme_collection[lexeme.Key + 3].Item1 == "Integer literal") {
							temp_str = lexeme_collection[lexeme.Key + 3].Item2;
							assign_var.Add(temp_str);

							int assign_var_size = assign_var.Count;
			
							if (assign_var_size == expression_size) {
								/*
									Because of copy issues, I had to make a copy of the listthen add the copy to the 
									list of program expressions so as not to have problems.
								*/
								List<string> copy = new List<string>(assign_var);
								temp.Add(copy);
								assign_var.Clear();

							}

						}

					} else {
						Console.WriteLine("Error: missing keyword YOU SET US UP");
					}

				} 

			}

			foreach (var list in temp) {
                program_expressions.Add(list);
            }
			
		}   

		// finished!
    	public void talk_to_the_hand(Dictionary<int, Tuple<string, string>> lexeme_collection) {

    		List<List<string>> temp = new List<List<string>>();
    		List<string> print_value = new List<string>();

    		string temp_str;
    		int expression_size = 2;

    		foreach(KeyValuePair<int, Tuple<string, string>> lexeme in lexeme_collection) {
    			if (lexeme.Value.Item2 == "TALK TO THE HAND") {
    				temp_str = lexeme.Value.Item2;
    				print_value.Add(temp_str);

    				if (lexeme_collection[lexeme.Key + 1].Item1 == "Variable identifier") {
    
						temp_str = lexeme_collection[lexeme.Key + 1].Item2;
						print_value.Add(temp_str);

						if (print_value.Count == expression_size) {
							List<string> copy = new List<string>(print_value);
							temp.Add(copy);
							print_value.Clear();
						}

					} else if (lexeme_collection[lexeme.Key + 1].Item1 == "String literal") {
						
						temp_str = lexeme_collection[lexeme.Key + 1].Item2;
						print_value.Add(temp_str);

						if (print_value.Count == expression_size) {
							List<string> copy = new List<string>(print_value);
							temp.Add(copy);
							print_value.Clear();
						}

					} else if (lexeme_collection[lexeme.Key + 1].Item1 == "Integer literal") {
						
						temp_str = lexeme_collection[lexeme.Key + 1].Item2;
						print_value.Add(temp_str);


						if (print_value.Count == expression_size) {
							List<string> copy = new List<string>(print_value);
							temp.Add(copy);
							print_value.Clear();
						}

					} else {
						Console.WriteLine("Error: Invalid print combination.");
					}

    			}
    		}

    		foreach (var list in temp) {
                program_expressions.Add(list);
            }

    	}

    	// finsihed!
    	public void reassign_variable(Dictionary<int, Tuple<string, string>> lexeme_collection) {
			List<List<string>> temp = new List<List<string>>();
    		List<string> reassign_var = new List<string>();

    		string temp_str;
    		int expression_size = 5;		// GET TO THE CHOPPER + var/int + HERE IS MY INVITATION + var/int + ENOUGH TALK = 5 pieces
    		int total_piece_count = 0;	
    		int group_count = 0;

    		foreach(KeyValuePair<int, Tuple<string, string>> lexeme in lexeme_collection) {
    			if (lexeme.Value.Item2 == "GET TO THE CHOPPER") {
    				temp_str = lexeme.Value.Item2;
    				reassign_var.Add(temp_str);
    				total_piece_count = total_piece_count + 1;

    				if ((lexeme_collection[lexeme.Key + 1].Item1 == "Variable identifier") 			// pwede pala yung ganitong syntax wow
    					|| (lexeme_collection[lexeme.Key + 1].Item1 == "Integer literal")) {
    
						temp_str = lexeme_collection[lexeme.Key + 1].Item2;
						reassign_var.Add(temp_str);
						total_piece_count = total_piece_count + 1;

						if (lexeme_collection[lexeme.Key + 2].Item2 == "HERE IS MY INVITATION") {
    
							temp_str = lexeme_collection[lexeme.Key + 2].Item2;
							reassign_var.Add(temp_str);
							total_piece_count = total_piece_count + 1;

							if ((lexeme_collection[lexeme.Key + 1].Item1 == "Variable identifier") 
								|| (lexeme_collection[lexeme.Key + 1].Item1 == "Integer literal")) {
    
								temp_str = lexeme_collection[lexeme.Key + 1].Item2;
								reassign_var.Add(temp_str);
								total_piece_count = total_piece_count + 1;
	
							}

						}

					}

    			} else if (lexeme.Value.Item2 == "ENOUGH TALK") {
    				temp_str = lexeme.Value.Item2;
    				reassign_var.Add(temp_str);
    				total_piece_count = total_piece_count + 1;
    				group_count = group_count + 1;

    				if (reassign_var.Count == expression_size) {
						List<string> copy = new List<string>(reassign_var);
						temp.Add(copy);
						reassign_var.Clear();
					}
    			}
    		}

    		if ((total_piece_count / group_count) == expression_size) {
    			foreach (var list in temp) {
                	program_expressions.Add(list);
            	}
    		}

		}

    	// prioritize this!
    	public void arithmetic_ops(Dictionary<int, Tuple<string, string>> lexeme_collection) {
			
			List<List<string>> temp = new List<List<string>>();
			List<string> arithmetic_expr = new List<string>();

			string temp_str;
			int math_expr_size = 2;				// Count() is a LINQ extension method, usage explained in readme.txt

			foreach (KeyValuePair<int, Tuple<string, string>> lexeme in lexeme_collection) {
				if (lexeme.Value.Item2 == "GET UP") {
					Console.WriteLine("am here!!");
					arithmetic_expr.Add(lexeme.Value.Item2);
				} else if (lexeme.Value.Item2 == "GET DOWN") {
					arithmetic_expr.Add(lexeme.Value.Item2);
				} else if (lexeme.Value.Item2 == "YOU'RE FIRED") { 
					arithmetic_expr.Add(lexeme.Value.Item2);
				} else if (lexeme.Value.Item2 == "HE HAD TO SPLIT") {
					arithmetic_expr.Add(lexeme.Value.Item2);
				} else if (lexeme.Value.Item2 == "Integer literal") {
					Console.WriteLine("now am here!!");
					arithmetic_expr.Add(lexeme.Value.Item2);
				}

				Console.WriteLine(math_expr_size);
				Console.WriteLine(arithmetic_expr.Count);

				if (arithmetic_expr.Count == math_expr_size) {
					string math_expr = string.Join(" ", arithmetic_expr.ToArray());
					Console.WriteLine(math_expr);
					break;
				}

			}


			if (arithmetic_expr.Contains("GET UP") && arithmetic_expr.Contains("Integer literal")) {
				Console.WriteLine("Add method valid.");
			} else if (arithmetic_expr.Contains("GET DOWN") && arithmetic_expr.Contains("Integer literal")) {
				Console.WriteLine("Subtract method valid.");
			} else if (arithmetic_expr.Contains("YOU'RE FIRED") && arithmetic_expr.Contains("Integer literal")) {
				Console.WriteLine("Multiply method valid.");
			} else if (arithmetic_expr.Contains("HE HAD TO SPLIT") && arithmetic_expr.Contains("Integer literal")) {
				Console.WriteLine("Divide method valid.");
			}

			// I'm supposed to add this knowledge to the symbol table but I'm not entirely sure what I'm supposed to put there.

		}    	

		public void logical_ops(Dictionary<int, Tuple<string, string>> lexeme_collection) {
			
		}   	  	    	

		public void if_else(Dictionary<int, Tuple<string, string>> lexeme_collection) {
			
		}    	

		public void while_loop(Dictionary<int, Tuple<string, string>> lexeme_collection) {
			
		} 

		// try to implement a function that will automatically do all of the above. 

		// public void parse_file(Dictionary<int, Tuple<string, string>> lexeme_collection) {
		// 	this.main_method(lexeme_collection);
		// }    	

    }
}
