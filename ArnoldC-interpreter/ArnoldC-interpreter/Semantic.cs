using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArnoldCinterpreter {

    public class Semantic {

    	public void reassign_variables(List<List<string>> reassign, List<List<string>> equations, 
    	Dictionary<int, Tuple<string,string>> symbol_table) {

    		foreach (var token in symbol_table) {

    			for (int i = 0; i < reassign.Count; i++) {
	    			// Console.WriteLine();
	    			for (int j = 0; j < reassign[i].Count; j++) {				
	    				// if GET TO THE CHOPPER <var> is the same as the token in the symbol table
	    				// attack na
	    				if (reassign[i][j+1] == token.Value.Item1) {
	    					int new_initial_value = reassign[i][j+2];			// HERE IS MY INVITATION <it gets this thing here>
	    				}

	    			}
	    		}

    		}

    	}

        public void run_program(List<List<string>> print, List<List<string>> assign, 
        List<List<string>> reassign, List<List<string>> equations, 
        Dictionary<int, Tuple<string,string>> symbol_table) {

        	this.reassign_variables(reassign, equations, symbol_table);

        }

    }
    
}
