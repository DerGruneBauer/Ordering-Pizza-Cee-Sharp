using System;
using System.Collections.Generic;

namespace Pizza_Party
{
    class Program
    {
        static void Main(string[] args)
        {
            Start: //unsure on best practice to restart app or restart ordering process. tried to use goto? do/while loop?
            Console.WriteLine("So you want to have a pizza party? Lets start with some basic questions. ");
            Console.WriteLine("");

            Console.Write("How many friends are you inviting? ");
            var numOfFriendsString = Console.ReadLine();
            UInt32 numOfFriends;
            while(!UInt32.TryParse(numOfFriendsString, out numOfFriends) ) {
                Console.WriteLine("That is not a valid number.");
                Console.WriteLine("How many friends are you inviting?");
                numOfFriendsString = Console.ReadLine();
            }

            Console.Write("How many slices do these pizzas contain? ");
            var pizzaSlicesString = Console.ReadLine();
            UInt32 pizzaSlices;
            while(!UInt32.TryParse(pizzaSlicesString, out pizzaSlices) ) {
                Console.WriteLine("That is not a valid number.");
                Console.WriteLine("How many slices do these pizzas contain?");
                pizzaSlicesString = Console.ReadLine();
            }
            Convert.ToDouble(pizzaSlices);

            Console.WriteLine("What are your three preferred pizza types? ");
            List<string> pizzaTypes = new List<string>();
            for (var i = 0; i < 3; i++)
            {
                Console.Write($"Pizza No.{i + 1}: ");
                string name = Console.ReadLine();
                while(string.IsNullOrEmpty(name)){
                    Console.WriteLine("This name cant be empty.");
                    Console.WriteLine("What is the name of your preferred pizza?");
                    name = Console.ReadLine();
                }
                pizzaTypes.Add(name);
            }
            Console.WriteLine("Ok, great. So far we know how many friends you're inviting, your favorite 3 pizzas, and the amount of slices in each pizza.");
            Console.WriteLine("");
            Console.WriteLine($"Now lets collect some information about your {numOfFriends} friends.");

            List<Friend> friendList = new List<Friend>();

            for (var i = 0; i < numOfFriends; i++)
            {
                Console.WriteLine($"Information for friend number {i + 1}");
                Console.WriteLine("Please enter their name: ");
                string name = Console.ReadLine();
                 while(string.IsNullOrEmpty(name)){
                    Console.WriteLine("This name cant be empty.");
                    Console.WriteLine("What is the name of your friend?");
                    name = Console.ReadLine();
                }
                // if(name.ToUpper() == "RESTART"){
                //     Console.WriteLine("Restarting...");
                //     Console.WriteLine("");
                //     goto Start;
                // }

                Console.WriteLine($"Please enter their preferred pizza type ({pizzaTypes[0]}, {pizzaTypes[1]}, or {pizzaTypes[2]}): ");
                string preferredPizza = Console.ReadLine();
                 while(string.IsNullOrEmpty(preferredPizza) || preferredPizza.ToLower() != pizzaTypes[0].ToLower() && preferredPizza.ToLower() != pizzaTypes[1].ToLower() && preferredPizza.ToLower() != pizzaTypes[2].ToLower()){
                     if(string.IsNullOrEmpty(preferredPizza)){
                        Console.WriteLine("This type cant be empty.");
                        Console.WriteLine("What is the name of their preferred pizza?");
                        preferredPizza = Console.ReadLine();
                     } else {
                         Console.WriteLine("This type is not one of the three choices.");
                         Console.WriteLine($"Please choose an option from the list: {pizzaTypes[0]}, {pizzaTypes[1]}, or {pizzaTypes[2]} ");
                        preferredPizza = Console.ReadLine();
                     }

                }

                Console.WriteLine($"Please enter the number of slices {name} will eat: ");
                var numSlicesEatString = Console.ReadLine();
                UInt32 numSlicesEat;
            while(!UInt32.TryParse(numSlicesEatString, out numSlicesEat) ) {
                Console.WriteLine("That is not a valid number.");
                Console.WriteLine($"Please enter the number of slices {name} will eat: ");
                numSlicesEatString = Console.ReadLine();
            }

                friendList.Add(new Friend(name, preferredPizza, numSlicesEat));
                Console.WriteLine("");
            }

            Console.WriteLine("I'm assuming you'll be eating pizza as well, so while we're at it let's add your information too.");
            Console.WriteLine("What is your name?");
            string userName = Console.ReadLine();
                 while(string.IsNullOrEmpty(userName)){
                    Console.WriteLine("This name cant be empty.");
                    Console.WriteLine("What is your name?");
                    userName = Console.ReadLine();
                }

            Console.WriteLine($"Which pizza do you plan on eating ({pizzaTypes[0]}, {pizzaTypes[1]}, or {pizzaTypes[2]})?: ");
            string userPreferredPizza = Console.ReadLine();
             while(string.IsNullOrEmpty(userPreferredPizza) || userPreferredPizza.ToLower() != pizzaTypes[0].ToLower() && userPreferredPizza.ToLower() != pizzaTypes[1].ToLower() && userPreferredPizza.ToLower() != pizzaTypes[2].ToLower()){
                     if(string.IsNullOrEmpty(userPreferredPizza)){
                        Console.WriteLine("This type cant be empty.");
                        Console.WriteLine("What is the name of their preferred pizza?");
                        userPreferredPizza = Console.ReadLine();
                     } else {
                         Console.WriteLine("This type is not one of the three choices.");
                         Console.WriteLine($"Please choose an option from the list: {pizzaTypes[0]}, {pizzaTypes[1]}, or {pizzaTypes[2]} ");
                        userPreferredPizza = Console.ReadLine();
                     }
                }

            Console.WriteLine($"Please enter the number of slices you will eat: ");
             var userNumSlicesEatString = Console.ReadLine();
                UInt32 userNumSlices;
            while(!UInt32.TryParse(userNumSlicesEatString, out userNumSlices) ) {
                Console.WriteLine("That is not a valid number.");
                Console.WriteLine($"Please enter the number of slices {userName} will eat: ");
                userNumSlicesEatString = Console.ReadLine();
            }

            friendList.Add(new Friend(userName, userPreferredPizza, userNumSlices));

            double[] numSlicesPizza = new double[3];
            //I dont feel like this is the most elegant solution. Hard coding the array/list numbers. Only works for short lists/arrays.
            for (int i = 0; i < friendList.Count; i++)
            {
                if (friendList[i].preferredPizza == pizzaTypes[0])
                {
                    numSlicesPizza[0] = numSlicesPizza[0] + friendList[i].numSlicesEat;
                }
                else if (friendList[i].preferredPizza == pizzaTypes[1])
                {
                    numSlicesPizza[1] = numSlicesPizza[1] + friendList[i].numSlicesEat;
                }
                else
                {
                    numSlicesPizza[2] = numSlicesPizza[2] + friendList[i].numSlicesEat;
                }
            }
            Console.WriteLine($"Pizza 1: {numSlicesPizza[0]}, Pizza 2: {numSlicesPizza[1]}, Pizza 3: {numSlicesPizza[2]}");

            double[] numPizzas = new double[3];
            double[] numHalfPizzas = new double[3];
            double[] numSlices = new double[3];
            for (int i = 0; i < pizzaTypes.Count; i++)
            {

                if ((numSlicesPizza[i] % pizzaSlices) == 0)
                {
                    numPizzas[i] = numSlicesPizza[i] / pizzaSlices;

                }
                else if ((numSlicesPizza[i] / pizzaSlices) > 1)
                {
                    double wholeNum = Math.Truncate(numSlicesPizza[i] / pizzaSlices);
                    numPizzas[i] = wholeNum;
                    double remainder = (numSlicesPizza[i] % pizzaSlices);
                    if (remainder == (pizzaSlices * .5))
                    {
                        numHalfPizzas[i] = 1;
                    }
                    else if (remainder > (pizzaSlices * .5))
                    {
                        numHalfPizzas[i] = 1;
                        numSlices[i] = (remainder - (pizzaSlices * .5));
                    }
                    else
                    {
                        numSlices[i] = remainder;
                    }
                }
                else if ((numSlicesPizza[i] / pizzaSlices) == 0.5)
                {
                    numHalfPizzas[i] = 1;
                }
                else if ((numSlicesPizza[i] / pizzaSlices) < 1 && (numSlicesPizza[i] / pizzaSlices) > 0.5)
                {

                    numHalfPizzas[i] = 1;
                    numSlices[i] = numSlicesPizza[i] - (pizzaSlices * .5);

                }
                else
                {
                    numSlices[i] = numSlicesPizza[i];
                }
            }

            //LANGUAGE FOR WHOLE PIZZAS
            bool pizzasEmpty = true;
            foreach (var item in numPizzas)
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
                for (var i = 0; i < pizzaTypes.Count; i++)
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
            for (var i = 0; i < pizzaTypes.Count; i++)
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
                for (var i = 0; i < pizzaTypes.Count; i++)
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
                for (var i = 0; i < pizzaTypes.Count; i++)
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
                for (var i = 0; i < pizzaTypes.Count - 1; i++)
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
            foreach (var item in numSlices)
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
                for (var i = 0; i < pizzaTypes.Count; i++)
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
