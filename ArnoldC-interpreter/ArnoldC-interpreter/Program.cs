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
            Dictionary<int, Tuple<string, string>> lexeme_dictionary = lexer.Lexical_analyzer();
            Parser parser = new Parser();
            parser.parse_file(lexeme_dictionary);
        
            // List<List<string>> exprs = parser.get_program_expressions();

            // Console.WriteLine("\n");
            // foreach (var list in exprs) {

            //     foreach (var item in list) {
            //         Console.WriteLine(item);
            //     }

            // }

            Dictionary<int, Tuple<string, string>> symbol_table = parser.get_symbol_table();

            foreach (var token in symbol_table) {
                Console.WriteLine(token);
            }

        }
    }
}
