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
            parser.main_method(lexeme_dictionary);
            parser.arithmetic_ops(lexeme_dictionary);

        }
    }
}
