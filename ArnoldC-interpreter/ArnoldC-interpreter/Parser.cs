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
    	// lists for making the semantic analyzer's life easier
    	List<List<string>> assignment_expressions = new List<List<string>>();
    	List<List<string>> reassign_expressions = new List<List<string>>();	
    	List<List<string>> print_expressions = new List<List<string>>();		
    	List<List<string>> math_expressions = new List<List<string>>();
		bool valid_main_method;
		int i = 1;

		public List<List<string>> get_assignment_expressions() {
			return assignment_expressions;
		}

		public List<List<string>> get_reassign_expressions() {
			return reassign_expressions;
		}

		public List<List<string>> get_print_expressions() {
			return print_expressions;
		}

		public List<List<string>> get_math_expressions() {
			return math_expressions;
		}

		public Dictionary<int, Tuple<string, string>> get_symbol_table() {
			return symbol_table;
		}

		// finished!
		public void main_method(Dictionary<int, Tuple<string, string>> lexeme_collection) {

			List<string> main_method_expr = new List<string>();
			int lexeme_count = lexeme_collection.Count;

			foreach (KeyValuePair<int, Tuple<string, string>> lexeme in lexeme_collection) {

				if (lexeme.Key == 0) {
					main_method_expr.Add(lexeme.Value.Item2);
				} else if (lexeme.Key == (lexeme_count - 1)) {
					main_method_expr.Add(lexeme.Value.Item2);
				}
			}

			if (main_method_expr.Contains("IT'S SHOWTIME") && main_method_expr.Contains("YOU HAVE BEEN TERMINATED")) {
				valid_main_method = true;
				// program_expressions.Add(main_method_expr);
			} else if (!main_method_expr.Contains("IT'S SHOWTIME")) {
				Console.WriteLine("Error: Invalid main method. Missing keyword IT'S SHOWTIME.");
				valid_main_method = false;
			} else if (!main_method_expr.Contains("YOU HAVE BEEN TERMINATED")) {
				Console.WriteLine("Error: Invalid main method. Missing keyword YOU HAVE BEEN TERMINATED.");
				valid_main_method = false;
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
                assignment_expressions.Add(list);
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
                print_expressions.Add(list);
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

							if ((lexeme_collection[lexeme.Key + 3].Item1 == "Variable identifier") 
								|| (lexeme_collection[lexeme.Key + 3].Item1 == "Integer literal")) {
    
    							// Console.WriteLine(lexeme_collection[lexeme.Key + 3].Item1 + " " + lexeme_collection[lexeme.Key + 3].Item2);
    							// Console.WriteLine("MUMSH DAFUQ NAGPALIT LANG AKO NG INDEX");
								temp_str = lexeme_collection[lexeme.Key + 3].Item2;
								reassign_var.Add(temp_str);
								total_piece_count = total_piece_count + 3;
	
							}

						}

					}

    			} else if (lexeme.Value.Item2 == "ENOUGH TALK") {
    				temp_str = lexeme.Value.Item2;
    				reassign_var.Add(temp_str);
    				total_piece_count = total_piece_count + 1;
    				group_count = group_count + 1;
    			}

    			if (reassign_var.Count == expression_size) {
					List<string> copy = new List<string>(reassign_var);
					temp.Add(copy);
					reassign_var.Clear();
				}

    		}

    		if (group_count != 0) {					// if there's nothing in the group then nothing gets written to reassign_expressions
	    		foreach (var list in temp) {
	                reassign_expressions.Add(list);
	            }
    		}

		}

    	// finished!
    	public void arithmetic_ops(Dictionary<int, Tuple<string, string>> lexeme_collection) {
			
			List<List<string>> temp = new List<List<string>>();
			List<string> arithmetic_expr = new List<string>();

			string temp_str;
			int math_expr_size = 2;				// Count() is a LINQ extension method that does something I'm not entirely sure of
												// so used .Count instead
			int total_piece_count = 0;
			int group_count = 0;

			foreach (KeyValuePair<int, Tuple<string, string>> lexeme in lexeme_collection) {
				if (lexeme.Value.Item2 == "GET UP") {

					temp_str = lexeme.Value.Item2;
					arithmetic_expr.Add(temp_str);
					total_piece_count = total_piece_count + 1;

					if ((lexeme_collection[lexeme.Key + 1].Item1 == "Integer literal") ||
						(lexeme_collection[lexeme.Key + 1].Item1 == "Variable identifier")) {

						temp_str = lexeme_collection[lexeme.Key + 1].Item2;
						arithmetic_expr.Add(temp_str);
						total_piece_count = total_piece_count + 1;						
						group_count = group_count + 1;

						if (arithmetic_expr.Count == math_expr_size) {
							List<string> copy = new List<string>(arithmetic_expr);
							temp.Add(copy);
							arithmetic_expr.Clear();
						}

					} else {
						Console.WriteLine("Error: missing variable or integer value.");
						break;
					}

				} else if (lexeme.Value.Item2 == "GET DOWN") {

					temp_str = lexeme.Value.Item2;
					arithmetic_expr.Add(temp_str);
					total_piece_count = total_piece_count + 1;

					if ((lexeme_collection[lexeme.Key + 1].Item1 == "Integer literal") ||
						(lexeme_collection[lexeme.Key + 1].Item1 == "Variable identifier")) {

						temp_str = lexeme_collection[lexeme.Key + 1].Item2;
						arithmetic_expr.Add(temp_str);
						total_piece_count = total_piece_count + 1;						
						group_count = group_count + 1;

						if (arithmetic_expr.Count == math_expr_size) {
							List<string> copy = new List<string>(arithmetic_expr);
							temp.Add(copy);
							arithmetic_expr.Clear();
						}

					} else {
						Console.WriteLine("Error: missing variable or integer value.");
						break;
					}

				} else if (lexeme.Value.Item2 == "YOU'RE FIRED") { 

					temp_str = lexeme.Value.Item2;
					arithmetic_expr.Add(temp_str);
					total_piece_count = total_piece_count + 1;

					if ((lexeme_collection[lexeme.Key + 1].Item1 == "Integer literal") ||
						(lexeme_collection[lexeme.Key + 1].Item1 == "Variable identifier")) {

						temp_str = lexeme_collection[lexeme.Key + 1].Item2;
						arithmetic_expr.Add(temp_str);
						total_piece_count = total_piece_count + 1;						
						group_count = group_count + 1;

						if (arithmetic_expr.Count == math_expr_size) {
							List<string> copy = new List<string>(arithmetic_expr);
							temp.Add(copy);
							arithmetic_expr.Clear();
						}

					} else {
						Console.WriteLine("Error: missing variable or integer value.");
						break;
					}

				} else if (lexeme.Value.Item2 == "HE HAD TO SPLIT") {

					temp_str = lexeme.Value.Item2;
					arithmetic_expr.Add(temp_str);
					total_piece_count = total_piece_count + 1;

					if ((lexeme_collection[lexeme.Key + 1].Item1 == "Integer literal") ||
						(lexeme_collection[lexeme.Key + 1].Item1 == "Variable identifier")) {

						temp_str = lexeme_collection[lexeme.Key + 1].Item2;
						arithmetic_expr.Add(temp_str);
						total_piece_count = total_piece_count + 1;						
						group_count = group_count + 1;

						if (arithmetic_expr.Count == math_expr_size) {
							List<string> copy = new List<string>(arithmetic_expr);
							temp.Add(copy);
							arithmetic_expr.Clear();
						}

					} else {
						Console.WriteLine("Error: missing variable or integer value.");
						break;
					}

				}

			}

			if (group_count != 0) {
				if ((total_piece_count / group_count) == math_expr_size) {
		    		foreach (var list in temp) {
		               	math_expressions.Add(list);
		           	}
		    	}
			}

		}    	

		public void update_symbol_table(List<List<string>> exprs, Dictionary<int, Tuple<string, string>> table) {

			if (valid_main_method == true) {		// only puts in the initial values of the variables

				foreach (var expr in exprs) {
	
					if (expr.Contains("HEY CHRISTMAS TREE")){
						var new_variable = expr[1];
						var new_value = expr[3];
						var token = Tuple.Create(new_variable, new_value);

						this.symbol_table.Add(this.i, token);
						this.i = this.i + 1;	
					}
					
				}

			}

			this.i = 1;
			
		} 

		// did not do these due to time constraints
		// public void logical_ops(Dictionary<int, Tuple<string, string>> lexeme_collection) {
			
		// }   	  	    	

		// public void if_else(Dictionary<int, Tuple<string, string>> lexeme_collection) {
			
		// }    	

		// public void while_loop(Dictionary<int, Tuple<string, string>> lexeme_collection) {
			
		// } 


		// does all of the above 
		public void parse_file(Dictionary<int, Tuple<string, string>> lexeme_collection) {
			this.main_method(lexeme_collection);
			if (valid_main_method == true) {
				this.assign_variable(lexeme_collection);
				this.talk_to_the_hand(lexeme_collection);
				this.reassign_variable(lexeme_collection);
				this.arithmetic_ops(lexeme_collection);
				this.update_symbol_table(assignment_expressions, lexeme_collection);
			}
		}    	

    }
}
