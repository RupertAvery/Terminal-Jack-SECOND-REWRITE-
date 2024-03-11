using System;
using System.Threading.Channels;

namespace MyApp
{
    enum PlayerChoice
    {
        None,
        Hit,
        Call
    }

    internal class Program
    {
        static bool startFinished = false;
        static int handTotal = 0;
        static int cardsTotal = 0;
        static int[] dealer = new int[21];
        static int[] hand = new int[5];
        static Random rng = new Random();
        static bool gameOver = false;
        static int milliseconds = 1500;
        static int dealerTotal = 0;

        static void Main(string[] args)
        {
            Start();
            Spaces();
            Console.WriteLine("Press any key to close the window.");
            Console.ReadKey();
        }

        static void Hit()
        {
            hand[cardsTotal] = rng.Next(1, 14);
            if (hand[cardsTotal] > 10)
            {
                hand[cardsTotal] = 10;
            }
            handTotal += hand[cardsTotal];
            Console.WriteLine(hand[cardsTotal]);
            cardsTotal++;
        }

        static void Call()
        {
            Console.WriteLine("Your total is " + handTotal + "\nThe dealer will now draw cards.");
            Thread.Sleep(milliseconds);
            for (int f = 0; dealerTotal <= handTotal; f++)
            {
                dealer[f] = rng.Next(1, 14);
                if (dealer[f] > 10)
                {
                    dealer[f] = 10;
                }
                dealerTotal += dealer[f];
                Thread.Sleep(milliseconds);
                Console.WriteLine(dealer[f]);
            }
        }
        
        static PlayerChoice GetPlayerChoice()
        {
            PlayerChoice playerChoice = PlayerChoice.None;
            while (playerChoice == PlayerChoice.None)
            {
                Console.Write("HIT or CALL? ");
                string choice = Console.ReadLine().ToLower();
                if (choice == "hit")
                {
                    playerChoice =  PlayerChoice.Hit;
                }
                else if (choice == "call")
                {
                    playerChoice =  PlayerChoice.Call;
                }
                else
                {
                    Console.WriteLine("Try Again");
                }
            }

            return playerChoice;
        }
        
        static void CheckHitResult()
        {
            if (cardsTotal == 5 && handTotal <= 21)
            {
                Spaces();
                Console.WriteLine("YOU WIN!\nYou managed to draw five cards without busting.");
                gameOver = true;
            }
            else if (handTotal > 21)
            {
                Spaces();
                Console.WriteLine("GAME OVER!\nYou busted!");
                gameOver = true;
            }
            else if (handTotal == 21)
            {
                Spaces();
                Console.WriteLine("BLACKJACK! YOU WIN!");
                gameOver = true;
            }
        }

        static void CheckCallResult()
        {
            Console.WriteLine("Dealer total is " + dealerTotal + ".");
            if (dealerTotal > 21)
            {
                Thread.Sleep(milliseconds);
                Spaces();
                Console.WriteLine("YOU WIN!\nDealer busts!");
            }
            else if (handTotal > dealerTotal)
            {
                Thread.Sleep(milliseconds);
                Spaces();
                Console.WriteLine("YOU WIN!\nYou are closer!");
            }
            else
            {
                Thread.Sleep(milliseconds);
                Spaces();
                Console.WriteLine("GAME OVER.\nDealer is closer.");
            }
            gameOver = true;
        }

        static void Start()
        {
            Console.WriteLine("Welcome to TERMINAL-JACK!\n-------------------------\nPress any key to continue.");
            Console.ReadKey();
            Console.WriteLine("\nYou will now be dealt two cards");
            Spaces();
            Hit();
            Hit();

            while (!gameOver)
            {
                Console.WriteLine($"Your total is now {handTotal}");
                var choice = GetPlayerChoice();
                switch (choice)
                {
                    case PlayerChoice.Hit:
                        Hit();
                        CheckHitResult();
                        break;  
                    case PlayerChoice.Call:
                        Call();
                        CheckCallResult();
                        break;
                }
            }
        }



        static void Spaces()
        {
            Console.WriteLine("-------------------------");
        }
    }
}
