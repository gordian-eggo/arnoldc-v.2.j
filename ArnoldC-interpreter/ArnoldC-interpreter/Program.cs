using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Gtk;

namespace ArnoldCinterpreter {

    class MainClass {

        static void Main(string[] args) {

            // Application.Init();
            // MainWindow win = new MainWindow();
            // win.Show();
            // Application.Run();

            Lexer lexer = new Lexer(); 
            Parser parser = new Parser();
            Dictionary<int, Tuple<string, string>> lexeme_dictionary = lexer.Lexical_analyzer(); 
            List<List<string>> program_expressions = parser.get_program_expressions();
            List<List<string>> print_statements = parser.get_print_expressions(); 
            List<List<string>> assignment_expressions = parser.get_assignment_expressions(); 
            List<List<string>> reassign_statements = parser.get_reassign_expressions(); 
            List<List<string>> arithmetic_equations = parser.get_math_expressions(); 
            Dictionary<int, Tuple<string,string>> symbol_table = parser.get_symbol_table();
            
            parser.parse_file(lexeme_dictionary);

            // uncomment to check contents of statement lists
            if (parser.valid_main_method == true) {
                // Console.WriteLine("symbol table data");
                // foreach (var token in symbol_table) {
                //     Console.WriteLine(token);
                // }

                Console.WriteLine("program expressions\n");
                foreach (var expr in program_expressions) {
                    foreach (var content in expr) {
                        Console.WriteLine(content);
                    }    
                    Console.WriteLine();
                }
                

                // Console.WriteLine("\n");
                // Console.WriteLine("print statements");
                // foreach (var expr in print_statements) {
                //     foreach (var content in expr) {
                //         Console.WriteLine(content);
                //     }    
                // }

                // Console.WriteLine("\n");
                // Console.WriteLine("assignment expressions");
                // foreach (var expr in assignment_expressions) {
                //     foreach (var content in expr) {
                //         Console.WriteLine(content);
                //     }    
                // }

                // Console.WriteLine("\nreassign statements");
                // foreach (var expr in reassign_statements) {
                //     Console.WriteLine("Statement: ");
                //     foreach (var content in expr) {
                //         Console.WriteLine(content);
                //     }    
                // }

                // Console.WriteLine("\nequations");
                // Console.WriteLine("# of equations: " + arithmetic_equations.Count);
                // foreach (var expr in arithmetic_equations) {
                //     Console.WriteLine("Equation: ");
                //     foreach (var content in expr) {
                //         Console.WriteLine(content);
                //     }    
                // }
            }

        }
    }
}
