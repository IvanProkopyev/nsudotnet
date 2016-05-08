using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuesser
{
    class Program
    {

        public static string DisgustingQuotes() {
            String[] quotes = { "u a fckn looser!", "What the hell are you doing?", "Get the hell out of here!", "Come on, you fuckin’ pussy!" , "Bastard", "Bullshit" };
            Random random = new Random();
            int number = random.Next(0, quotes.Length);
            return quotes[number];


        }

        public static void Game(String name) {
            int[] usernumbers = new int[1000];
            int userNumber;
            Random random = new Random();
            int number = random.Next(0, 100);
            Console.WriteLine(number);
            for (int i = 1; i < 1000; ++i) {
                Console.WriteLine("Enter number:");
                String command = Console.ReadLine();

                if (command.Equals("q")){
                    Console.WriteLine("Sorry, bruh...");
                    return;

                }

                if (!Int32.TryParse(command, out userNumber)) {
                    Console.WriteLine("Hey, bro! Use correct commands!");
                }

                else
                {
                    userNumber = int.Parse(command);
                    usernumbers[i - 1] = userNumber;
                    if (userNumber < number)
                    {
                        Console.WriteLine("bol'she");
                        if (i % 4 == 0)
                        {
                            Console.WriteLine(name + " " + DisgustingQuotes());
                        }
                    }
                    if (userNumber > number)
                    {
                        Console.WriteLine("men'she");
                        if (i % 4 == 0)
                        {
                            Console.WriteLine(name + " " + DisgustingQuotes());
                        }
                    }

                    if (userNumber == number)
                    {
                        Console.WriteLine("ti smog tol'ko lish s " + i + "-oi popitky, neudashnik!");
                        for (int k = 0; k < i; ++k)
                        {
                            Console.WriteLine(usernumbers[k]);

                        }
                        return;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name pls");
            String username = Console.ReadLine();
            Game(username);

        }
    }
}
