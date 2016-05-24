using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuesser
{
    class Program
    {

        public static void PrintDisgustingQuotes(String name) {
            String[] quotes = { " {0}, u a fckn looser!",
                "What the hell are you doing, {0} ?",
                "Get the hell out of here!",
                "Come on, {0}, you fuckin’ pussy!" ,
                " {0}, u a Bastard",
                "Bullshit- it's u, {0}" };
            Random random = new Random();
            int number = random.Next(0, quotes.Length);
            Console.WriteLine(quotes[number], name);
            
        }


        public static void Game(String name) {
            
            int[] usernumbers = new int[1000];
            int usernumber;
            Random random = new Random();
            int number = random.Next(0, 100);
            DateTime start = DateTime.Now;
            for (int i = 1; i < 1000; ++i) {
                Console.WriteLine("Enter number:");
                String command = Console.ReadLine();
                if (command.Equals("q")){
                    Console.WriteLine("Sorry, bruh...");
                    return;
                }
                if (!Int32.TryParse(command, out usernumber)) {
                    Console.WriteLine("Hey, bro! Use correct commands!");
                }
                else{
                    usernumber = int.Parse(command);
                    usernumbers[i - 1] = usernumber;
                    if (usernumber < number){
                        Console.WriteLine("bol'she");
                        if (i % 4 == 0){
                            PrintDisgustingQuotes(name);
                        }
                    }
                    if (usernumber > number){
                        Console.WriteLine("men'she");
                        if (i % 4 == 0){
                            PrintDisgustingQuotes(name);
                        }
                    }

                    if (usernumber == number){
                        TimeSpan gameTime = DateTime.Now - start;
                        Console.WriteLine("ti smog tol'ko lish s {0}-oi popitky, neudashnik!", i);
                        String sign = null;
                        for (int k = 0; k < i; ++k){
                            if (usernumbers[k] < number)
                                sign = "<";
                            if (usernumbers[k] > number)
                                sign = ">";


                            Console.WriteLine(String.Format("your number is {0} ,{1}", usernumbers[k], sign));

                        }
                        Console.WriteLine(String.Format("your time {0} ", gameTime.TotalMinutes));
                        return;
                    }
                }
            }
        }

        static void Main(string[] args){
            Console.WriteLine("Enter your name pls");
            String username = Console.ReadLine();
            Game(username);

        }
    }
}
