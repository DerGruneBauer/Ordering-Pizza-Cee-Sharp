using System;
using System.Collections.Generic;

namespace Pizza_Party
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine("So you want to have a pizza party? Lets start with some basic questions. ");
            Console.WriteLine("");

            Console.Write("How many friends are you inviting? ");
            UInt32 numOfFriends = getInt("How many friends are you inviting? ");

            Console.Write("How many slices do these pizzas contain? ");
            UInt32 pizzaSlices = getInt("How many slices do these pizzas contain?");
            Convert.ToDouble(pizzaSlices);

            Console.WriteLine("What are your three preferred pizza types? ");
            List<string> pizzaTypes = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Pizza No.{i + 1}: ");
                string name = getText("What is the name of your preferred pizza?");
                pizzaTypes.Add(name);
            }
            Console.WriteLine("Ok, great. So far we know how many friends you're inviting, your favorite 3 pizzas, and the amount of slices in each pizza.");
            Console.WriteLine("");
            Console.WriteLine($"Now lets collect some information about your {numOfFriends} friends.");

            List<Friend> friendList = new List<Friend>();
            for (int i = 0; i < numOfFriends; i++)
            {
                Console.WriteLine($"Information for friend number {i + 1}");
                Console.WriteLine("Please enter their name: ");
                string name = getText("What is the name of your friend?");

                Console.WriteLine($"Please enter their preferred pizza type ({pizzaTypes[0]}, {pizzaTypes[1]}, or {pizzaTypes[2]}): ");
                string preferredPizza = Console.ReadLine();
                validatePizzaType(preferredPizza, pizzaTypes);

                Console.WriteLine($"Please enter the number of slices {name} will eat: ");
                UInt32 numSlicesEat = getInt($"Please enter the number of slices {name} will eat: ");
                friendList.Add(new Friend(name, preferredPizza, numSlicesEat));
                Console.WriteLine("");
            }

            Console.WriteLine("I'm assuming you'll be eating pizza as well, so while we're at it let's add your information too.");
            Console.WriteLine("What is your name?");
            string userName = getText("What is your name?");

            Console.WriteLine($"Which pizza do you plan on eating ({pizzaTypes[0]}, {pizzaTypes[1]}, or {pizzaTypes[2]})?: ");
            string userPreferredPizza = Console.ReadLine();
            validatePizzaType(userPreferredPizza, pizzaTypes);

            Console.WriteLine($"Please enter the number of slices you will eat: ");
            UInt32 userNumSlices = getInt($"Please enter the number of slices {userName} will eat: ");
            friendList.Add(new Friend(userName, userPreferredPizza, userNumSlices));


            Dictionary<string, double> numSlicesZa = new Dictionary<string, double>()
            {
                { pizzaTypes[0], 0 },
                { pizzaTypes[1], 0 },
                { pizzaTypes[2], 0 },
            };
            List<string> keys = new List<string>(numSlicesZa.Keys);
            for (int i = 0; i < friendList.Count; i++)
            {
                foreach (string key in keys)
                {
                    if (friendList[i].preferredPizza == key)
                    {
                        numSlicesZa[key] = numSlicesZa[key] + friendList[i].numSlicesEat;
                    }
                }
            }

            double[] numPizzas = new double[3];
            double[] numHalfPizzas = new double[3];
            double[] numSlices = new double[3];

            int k = 0;
            foreach (KeyValuePair<string, double> item in numSlicesZa)
            {
                if ((item.Value % pizzaSlices) == 0)
                {
                    numPizzas[k] = item.Value / pizzaSlices;
                }
                else if ((item.Value / pizzaSlices) > 1)
                {
                    double wholeNum = Math.Truncate(item.Value / pizzaSlices);
                    numPizzas[k] = wholeNum;
                    double remainder = (item.Value % pizzaSlices);
                    if (remainder == (pizzaSlices * .5))
                    {
                        numHalfPizzas[k] = 1;
                    }
                    else if (remainder > (pizzaSlices * .5))
                    {
                        numHalfPizzas[k] = 1;
                        numSlices[k] = (remainder - (pizzaSlices * .5));
                    }
                    else
                    {
                        numSlices[k] = remainder;
                    }
                }
                else if ((item.Value / pizzaSlices) == 0.5)
                {
                    numHalfPizzas[k] = 1;
                }
                else if ((item.Value / pizzaSlices) < 1 && (item.Value / pizzaSlices) > 0.5)
                {
                    numHalfPizzas[k] = 1;
                    numSlices[k] = item.Value - (pizzaSlices * .5);
                }
                else
                {
                    numSlices[k] = item.Value;
                }
                k++;
            }

            //LANGUAGE FOR WHOLE PIZZAS
            bool pizzasEmpty = true;
            foreach (int item in numPizzas)
            {
                if (item != 0)
                {
                    pizzasEmpty = false;
                }
            }

            if (pizzasEmpty == false)
            {
                Console.WriteLine("");
                Console.Write("You will need to order ");
                for (int i = 0; i < pizzaTypes.Count; i++)
                {
                    if (numPizzas[i] == 0)
                    {
                        Console.Write($"");
                    }
                    else if (numPizzas[i] == 1)
                    {
                        Console.Write($"{numPizzas[i]} {pizzaTypes[i]} pizza ");
                    }
                    else
                    {
                        Console.Write($"{numPizzas[i]} {pizzaTypes[i]} pizzas ");
                    }
                }
                Console.Write(".");
            }

            //LANGUAGE FOR HALF PIZZAS
            Console.WriteLine("");
            int counter = 0;
            for (int i = 0; i < pizzaTypes.Count; i++)
            {
                if (numHalfPizzas[i] == 1)
                {
                    counter++;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("");
            }
            else if (counter == 1)
            {
                for (int i = 0; i < pizzaTypes.Count; i++)
                {
                    if (numHalfPizzas[i] == 1)
                    {
                        numSlices[i] = numSlices[i] + (pizzaSlices * .5);
                    }
                }
            }
            else if (counter == 2)
            {
                Console.Write("You will need to order one pizza, ");
                for (int i = 0; i < pizzaTypes.Count; i++)
                {
                    if (numHalfPizzas[i] == 1)
                    {
                        Console.Write($"half {pizzaTypes[i]} ");
                    }
                }
                Console.Write(".");
            }
            else if (counter == 3)
            {
                Console.Write("You will need to order one pizza, ");
                for (int i = 0; i < pizzaTypes.Count - 1; i++)
                {
                    if (numHalfPizzas[i] == 1)
                    {
                        Console.Write($"half {pizzaTypes[i]} ");
                    }
                }
                numSlices[2] = numSlices[2] + (pizzaSlices * .5);
                Console.Write(".");
            }

            //LANGUAGE FOR PIZZA SLICES REMAINING.
            bool slicesEmpty = true;
            foreach (int item in numSlices)
            {
                if (item != 0)
                {
                    slicesEmpty = false;
                }
            }

            if (slicesEmpty == false)
            {
                Console.WriteLine("");
                Console.Write("There's ");
                for (int i = 0; i < pizzaTypes.Count; i++)
                {
                    if (numSlices[i] == 0)
                    {
                        Console.Write($"");
                    }
                    else if (numSlices[i] == 1)
                    {
                        Console.Write($"{numSlices[i]} slice of {pizzaTypes[i]} pizza ");
                    }
                    else
                    {
                        Console.Write($"{numSlices[i]} slices of {pizzaTypes[i]} pizza ");
                    }
                }
                Console.Write("remaining. ");
            }
            if (counter == 0 && pizzasEmpty == true)
            {
                Console.WriteLine("");
                Console.WriteLine("You and your friends need to eat more pizza. There's not enough to order a whole pizza.");
            }
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void restartApp(string userChoice)
        {
            if (userChoice.ToUpper() == "RESTART")
            {
                Console.WriteLine(userChoice);
                Main();
            }
        }
        static UInt32 validateNum(string text, UInt32 num, string phrase)
        {
            restartApp(text);
            while (!UInt32.TryParse(text, out num))
            {
                Console.WriteLine("That is not a valid number.");
                Console.WriteLine(phrase);
                text = Console.ReadLine();
            }
            return num;
        }
        static string validateString(string text, string phrase)
        {
            while (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("This name cant be empty.");
                Console.WriteLine(phrase);
                text = Console.ReadLine();
            }
            return text;
        }
        static string validatePizzaType(string preferredPizza, List<string> pizzaTypes)
        {
            restartApp(preferredPizza);
            while (string.IsNullOrEmpty(preferredPizza) || preferredPizza.ToLower() != pizzaTypes[0].ToLower() && preferredPizza.ToLower() != pizzaTypes[1].ToLower() && preferredPizza.ToLower() != pizzaTypes[2].ToLower())
            {
                if (string.IsNullOrEmpty(preferredPizza))
                {
                    Console.WriteLine("This type cant be empty.");
                    Console.WriteLine("What is the name of the preferred pizza?");
                    preferredPizza = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("This type is not one of the three choices.");
                    Console.WriteLine($"Please choose an option from the list: {pizzaTypes[0]}, {pizzaTypes[1]}, or {pizzaTypes[2]} ");
                    preferredPizza = Console.ReadLine();
                }
            }
            return preferredPizza;
        }
        static UInt32 getInt(string phrase)
        {
            string text = Console.ReadLine();
            UInt32 number = 0;
            number = validateNum(text, number, phrase);
            return number;
        }
        static string getText(string phrase)
        {
            string text = Console.ReadLine();
            validateString(text, "What is the name of your preferred pizza?");
            restartApp(text);
            return text;
        }

        public class Friend
        {
            public string name { get; set; }
            public string preferredPizza { get; set; }
            public double numSlicesEat { get; set; }
            public Friend(string name, string preferredPizza, double numSlicesEat)
            {
                this.name = name;
                this.preferredPizza = preferredPizza;
                this.numSlicesEat = numSlicesEat;
            }
        }
    }
}
