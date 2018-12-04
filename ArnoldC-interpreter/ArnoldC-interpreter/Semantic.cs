using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArnoldCinterpreter {

    public class Semantic {

    	Dictionary<int, Tuple<string,string>> symbol_table = new Dictionary<int, Tuple<string,string>>();
    	int variable_value = 0;
    	int i = 0;         // for indexing

    	public Dictionary<int, Tuple<string,string>> get_symbol_table() {
    		return symbol_table;
    	}

        public void run_program(List<List<string>> prog_expr, List<List<string>> print, List<List<string>> assign, 
        List<List<string>> reassign, List<List<string>> equations) {

        	foreach (var expr in prog_expr) {


    			if (expr.Contains("TALK TO THE HAND")) {
    				// Console.WriteLine("i'd like to die");
    				string print_this = expr[1];

    				Console.WriteLine(print_this);				// if TALK TO THE HAND is printing a string or an integer this should do


    			} else if (expr.Contains("HEY CHRISTMAS TREE")) {

    				string variable_name = expr[1];				// the variable name
    				string variable_value = expr[3];			// the variable's value

    				var token = Tuple.Create(variable_name, variable_value);

    				symbol_table.Add(i, token);

    				i = i + 1;

    			} else if (expr.Contains("GET TO THE CHOPPER")) {

    				string variable_name = expr[1];
    				string new_value;
    				int temp;
    				int i = 2;
    				bool is_a_num;

    				if (expr[i] == "HERE IS MY INVITATION") {
    					Int32.TryParse(expr[i+1], out temp);
    					variable_value = temp;

    					int j = i + 2;

    					do {

    						is_a_num = Int32.TryParse(expr[j], out temp); // converts strings to int

    						if (is_a_num) {
    							if (expr[j-1] == "GET UP") {
    								// Console.WriteLine("adding {0} to {1}", temp, variable_value);
    								variable_value = variable_value + temp;
    							} else if (expr[j-1] == "GET DOWN") {
    								// Console.WriteLine("subtracting {0} from {1}", temp, variable_value);
    								variable_value = variable_value - temp;
    							} else if (expr[j-1] == "YOU'RE FIRED") {
    								// Console.WriteLine("multiplying {0} by {1}", variable_value, temp);
    								variable_value = variable_value * temp;
    							} else if (expr[j-1] == "HE HAD TO SPLIT") {
    								// Console.WriteLine("dividing {0} by {1}", variable_value, temp);
    								variable_value = variable_value * temp;
    							}
    						}

    						j = j + 1;

    					} while (expr[j] != "ENOUGH TALK");

	    				new_value = variable_value.ToString();

    					// put the new value in the symbol table
    					var token = Tuple.Create(variable_name, new_value);

    					Dictionary<int, Tuple<string,string>> st_copy = new Dictionary<int, Tuple<string,string>>(symbol_table);

                        // still the same dictionary issues here
    					foreach (var item in st_copy) {
    						if (item.Value.Item1 == variable_name) {
    							int temp_key = item.Key;
    							symbol_table.Remove(item.Key);
    							symbol_table.Add(temp_key, token);
    						}
    					}

    				}

    			}

        	}

        }

    }
    
}
