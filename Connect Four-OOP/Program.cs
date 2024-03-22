using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_Four_OOP
{
    internal class Program
    {

        //creating player abstract class:
        public abstract class player
        {
            public string Name;
            public char Symbole;

            public player(string name, char symbol)
            {
                Name = name;
                Symbole = symbol;
            }

            public override string ToString()
            {
                return $"Name: {Name}, Symbole: {Symbole}";
            }
        }
        // Subclass of the abstract class:
        public class Humanplayer : player
        {
            public Humanplayer(string name, char symbol) : base(name, symbol)
            {

            }

            public override string ToString()
            {
                return base.ToString(); // use the inf set on the base class.
            }
        }

        static void Main(string[] args)
        {
            // Creating instances of the subclasses( base class player)
            Humanplayer player1 = new Humanplayer("Player X", 'X');
            Humanplayer player2 = new Humanplayer("Player O", 'O');

            Console.WriteLine(player1);
            Console.WriteLine(player2);

        }
    }
}
