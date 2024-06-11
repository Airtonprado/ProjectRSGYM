using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace D00_Utility
{

    public class Utility
    {
        public static void SetUnicodeConsole()
        {
            //onsole.WriteLine("á Á à À ã Ã â Â ç Ç º ª");
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Console.WriteLine("á Á à À ã Ã â Â ç Ç º ª");
        }

        public static void WriteTitle(string title, string begin = "")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 40)); //para repetir x vezes
            Console.WriteLine(title);
            Console.WriteLine(new string('-', 40)); //para repetir x vezes
            Console.ForegroundColor = ConsoleColor.White;

        }

        public static void WriteMessage(string text, string text1, string begin = "", string end = "")
        {
            Console.WriteLine($"{begin}{text1}{text}{end}");
        }
        public static void TerminateConsole()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\n\n\nAperte qualquer tecla para terminar");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.Clear();

        }
        public static void PauseConsole()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\n\n\n\"Continuando após a pausa...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.Clear();

        }

    }
}


