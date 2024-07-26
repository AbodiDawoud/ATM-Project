using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ATM_Project
{
    internal class Program
    {
        //<-----------------------[Variables]----------------------->

        enum FileToOpen
        {
            E_MainMenu,
            E_AccountInfo,
            E_Deposit,
            E_Withdraw,
            E_GeneratePage,
            E_ResetPassword,
            E_DonatePage
        }

        public static readonly string[] ErrorMessages = { "Bad Input", "Wrong Input", "Not Valid Input", "Try Again" };
        private static readonly string[] SecurityQuistions = 
        {
            "In what city were you born?",
            "What is the name of your favorite pet?",
            "What is your mother's maiden name?",
            "What high school did you attend?",
            "What was the name of your elementary school?",
            "What was your favorite food as a child?",
            "What was your first car?",
        };
        private static readonly Random random = new Random();


        static string FirstName = string.Empty;  // Index 0
        static string Surname = string.Empty; // Index 1
        static string Password = string.Empty; // Index 2
        static string SecurityQuistionPresented = string.Empty; // Index 3
        static string SecurityAnswer = string.Empty; // Index 4
        static string CardNumber = string.Empty; // Index 5
        static string PinCode = string.Empty; // Index 6

        static double Balance; // Index 7
        static readonly double DepositMaxAmount = 1500;
        private static double DepositMaded; // Index 8


        static bool HasLoggedInBefore = false; // Index 9
        static bool HasChangedPinCodeBefore = false; // Index 10
        static bool HasGenerateCardBefore = false; // Index 11

        static int Today; // Index 12
        private static readonly string ClientPath = @"Client.txt";

        //<-----------------------[Begin Play]----------------------->
        static void Main()
        {
            Console.Title = "ATM";
            Load();
            DrawMainMenu();
            Console.ReadKey();
        }


        //<-----------------------[Main Menu]----------------------->
        public static void DrawMainMenu()
        {
            Console.Clear();
            Console.WriteLine("\u001b[33m<---------------[Main Menu]--------------->\n\u001b[0m");
            Console.WriteLine("Hello and wellcome to my ATM console app.");
            Console.WriteLine("You have the abilities to Deposit and Withdraw with a limited amount of money daily.");
            Console.WriteLine($"Day is: {DateTime.Now.DayOfWeek} - {DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
            Console.Write("\n");
            Console.WriteLine("\u001b[35;1m1-\u001b[0m Account Info");
            Console.WriteLine("\u001b[35;1m2-\u001b[0m Settings");
            Console.WriteLine("\u001b[35;1m3-\u001b[0m Quit App");
            switch (AskForChoice())
            {
                case 1: AccountInfo(); break;
                case 2: Settings.DisplaySettingsMenu(); break;
                case 3: QuitApp(); break;
                default: WrongChoice(1700); break;
            }
        }


        //<-----------------------[Ask you user input]----------------------->
        public static int AskForChoice()
        {
            Console.Write("\n\n");
            Console.Write("\u001b[35;1m-->\u001b[0m \u001b[1mEnter your choice: \u001b[0m");
            string input = Console.ReadLine();
            bool success = int.TryParse(input, out int value);
            if (success)
            {
                return value;
            }
            else
            {
                return -1;
            }
        }


        //<-----------------------[Account Info]----------------------->
        static void AccountInfo(bool bAskForChoice = true)
        {
            Console.Clear();
            if (HasLoggedInBefore is false)
            {
                Console.WriteLine("\u001b[34;1m<---------------\u001b[0m[ Quick Message ]\u001b[34;1m--------------->\u001b[0m");
                Console.Write("\n");
                Console.WriteLine("If you dont have an account you have to register one before doing any transitions or money depositing");
                Console.WriteLine("If you do have one please login");
                Console.Write("\n\n");
                Console.WriteLine("<----------[Actions]---------->");
                Console.WriteLine("\u001b[35;1m1-\u001b[0m Login");
                Console.WriteLine("\u001b[35;1m2-\u001b[0m Create Account");
                Console.WriteLine("\u001b[35;1m3-\u001b[0m Reset Account");
                Console.WriteLine("\u001b[35;1m4-\u001b[0m Main Menu");
                switch (AskForChoice())
                {
                    case 1: Login(); break;
                    case 2: Signup(); break;
                    case 3: ResetPasswordWidget(); break;
                    case 4: DrawMainMenu(); break;
                    default: WrongChoice(OpenFile: FileToOpen.E_AccountInfo); break;
                }
            }
            else
            {
                Console.WriteLine("\u001b[34;1m<---------------\u001b[0m[ Account Info ]\u001b[34;1m--------------->\u001b[0m");
                Console.Write("\n");
                Console.WriteLine("Those informations is very sensitive so be kind and not show them to any person.\n");
                string upperUsername = FirstName.ToUpper();
                string upperSurname = Surname.ToUpper();
                Console.WriteLine($"\u001b[35;1m-->\u001b[0m Username: Mr. {upperUsername} {upperSurname}");
                Console.WriteLine($"\u001b[35;1m-->\u001b[0m Your current balance is: ${Balance}");
                Console.WriteLine($"\u001b[35;1m-->\u001b[0m Password: {Password}");
                Console.WriteLine($"\u001b[35;1m-->\u001b[0m {SecurityQuistionPresented}: {SecurityAnswer}");
                Console.WriteLine($"\u001b[35;1m-->\u001b[0m Card Number: {CardNumber}");
                Console.WriteLine($"\u001b[35;1m-->\u001b[0m Pin Code: {PinCode}");

                Console.Write("\n\n");
                Console.WriteLine("<----------[Actions]---------->");
                Console.WriteLine("\u001b[35;1m1-\u001b[0m Deposit Money");
                Console.WriteLine("\u001b[35;1m2-\u001b[0m Withdraw Money");
                Console.WriteLine("\u001b[35;1m3-\u001b[0m Donate Money");
                Console.WriteLine("\u001b[35;1m4-\u001b[0m Change password");
                Console.WriteLine("\u001b[35;1m5-\u001b[0m Change pin code");
                Console.WriteLine("\u001b[35;1m6-\u001b[0m Regenerate a new card number");
                Console.WriteLine("\u001b[35;1m7-\u001b[0m Main Menu");
                Console.WriteLine("\u001b[35;1m8-\u001b[0m Delete Account");
                Console.WriteLine("\u001b[35;1m9-\u001b[0m Logut");
                if (bAskForChoice)
                {
                    switch (AskForChoice())
                    {
                        case 1: DepositWidget(); break;
                        case 2: WithdrawWidget(); break;
                        case 3: DonateWidget(); AccountInfo(); break;
                        case 4: ChangePassword(); break;
                        case 5: ChangePinCode(); break;
                        case 6: GeneratePage(); break;
                        case 7: DrawMainMenu(); break;
                        case 8: DeleteAccount(); break;
                        case 9: Logut(); break;
                        default: WrongChoice(1800, FileToOpen.E_AccountInfo); break;
                    }
                }
            }
        }

        static void Login()
        {
            Console.Clear();

            Console.WriteLine("\u001b[34;1m<---------------[Login Page]--------------->\u001b[0m\n");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (username != string.Empty && username == FirstName && password != string.Empty && password == Password)
            {
                HasLoggedInBefore = true;
                FileManager.UpdateLineAtIndex(ClientPath, 9, HasLoggedInBefore.ToString());
                Console.Write("\n");
                Console.Write("\u001b[32;1mSuccessfully Logged in\u001b[0m");
                Task.Delay(1800).Wait();
                AccountInfo();
            }
            else
            {
                string[] Error = { "You've Entered invalid info", "Something is not currect", "Invalid username or password", "Incorrect Inputs" };
                Console.Write("\u001b[31;1m" + Error[random.Next(0, Error.Length)] + "\u001b[0m");
                Console.Write("\nDo you want to try again? ");
                Console.Write("\u001b[42m[1. Yes]\u001b[0m - \u001b[41m[2. No]\u001b[0m: ");
                string input = Console.ReadLine();
                if (input.Equals("1"))
                {
                    Login();
                }
                else
                {
                    AccountInfo();
                }
            }
        }

        static void Signup()
        {
            Console.Clear();
            Console.WriteLine("\u001b[34;1m<---------------[Signup Page]--------------->\u001b[0m\n");

            Console.Write("Enter your first name exactly like in your ID card: ");
            FirstName = Console.ReadLine();

            Console.Write("Enter your surname like in your ID card: ");
            Surname = Console.ReadLine();

            Console.Write("Create an unique password: ");
            Password = Console.ReadLine();

            Console.WriteLine("Answer the security question and keep it in mind you will need it on account reseting");
            SecurityQuistionPresented = SecurityQuistions[random.Next(0, SecurityQuistions.Length)];
            Console.Write($"{SecurityQuistionPresented}: ");
            SecurityAnswer = Console.ReadLine();

            Console.WriteLine("\nWe automatically going to generate a new Card Number with a pin code for you ready to use");
            Console.WriteLine("You find them in your account page");
            Console.Write("Tap any key to continue.. ");
            Console.ReadKey();

            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(SecurityAnswer))
            {
                HasLoggedInBefore = true;
                HasChangedPinCodeBefore = false;
                HasGenerateCardBefore = false;
                DepositMaded = 0;
                Balance = 1500;
                Console.Write("\n");
                Console.Write("\u001b[32mSuccessfully signed up\u001b[0m");
                GenerateCardNumber();
                GeneratePinCode();
                if (!FileManager.DoesFileExist(ClientPath))
                {
                    FileManager.CreateNewFile(ClientPath);
                    Save();
                }
                else
                {
                    FileManager.RemoevSavedFile(ClientPath);
                    FileManager.CreateNewFile(ClientPath);
                    Save();
                }
                Task.Delay(1800).Wait();
                AccountInfo();
            }

            else
            {
                HasLoggedInBefore = false;
                Console.Write("\u001b[31;1m Something went wrong please try again..\u001b[0m");
                Task.Delay(2800).Wait();
                Signup();
            }
        }

        static void ChangePinCode()
        {
            if (HasChangedPinCodeBefore is false)
            {
                Console.WriteLine("\n\n\u001b[35;1m-->\u001b[0m Please note that once you changed you pin code, You cant do it anymore");
                Console.WriteLine("\u001b[35;1m-->\u001b[0m And the pin code has to 4 digits no less or more");
                Console.Write("Do you want to continue? ");
                Console.Write("\u001b[42m[1. Yes]\u001b[0m - \u001b[41m[2. No]\u001b[0m: ");
                string ConfirmInput = Console.ReadLine();
                if (ConfirmInput == "1")
                {
                    Console.Write("Enter your new pin code: ");
                    string input = Console.ReadLine();
                    bool doesSucces = int.TryParse(input, out int code);
                    if (doesSucces)
                    {
                        if (input.Length == 4)
                        {
                            HasChangedPinCodeBefore = true;
                            PinCode = code.ToString();
                            FileManager.UpdateLineAtIndex(ClientPath, 6, PinCode.ToString());
                            FileManager.UpdateLineAtIndex(ClientPath, 10, HasChangedPinCodeBefore.ToString());
                            Console.Write("\u001b[32mSuccessfully changed the pin code\u001b[0m");
                            Task.Delay(1500).Wait();
                            AccountInfo();
                        }
                        else
                        {
                            Console.Write("\u001b[31mNot Valid Pin Code..\u001b[0m");
                            Task.Delay(1200).Wait();
                            AccountInfo(false);
                            ChangePinCode();
                        }
                    }
                    else
                    {
                        Console.Write("\u001b[31mNot Valid Pin Code..\u001b[0m");
                        Task.Delay(1200).Wait();
                        AccountInfo(false);
                        ChangePinCode();
                    }
                }
                else
                {
                    AccountInfo();
                }
            }
            else
            {
                Console.Write("\u001b[31;1mYou have aldreay changed your pin code..\u001b[0m");
                Task.Delay(1500).Wait();
                AccountInfo();
            }
        }

        static void ChangePassword()
        {
            Console.Write("\n\n--> Enter the new password: ");
            string password = Console.ReadLine();
            if (!string.IsNullOrEmpty(password))
            {
                Password = password;
                FileManager.UpdateLineAtIndex(ClientPath, 2, Password);
                Console.Write("\u001b[32mSuccessfully changed the password\u001b[0m");
                Task.Delay(1900).Wait();
                AccountInfo();
            }
            else
            {
                Console.Write("\u001b[31;1mSomething went wrong please try again later..\u001b[0m");
                Task.Delay(1300).Wait();
                AccountInfo();
            }
        }

        static void GenerateCardNumber()
        {
            string digits = "0123456789";
            char[] randomChars = new char[16 + (16 / 4)]; // Increase the length to account for hyphens
            int hyphenIndex = 0;

            for (int i = 0; i < 16; i++)
            {
                if (i > 0 && i % 4 == 0)
                {
                    randomChars[hyphenIndex++] = '-';
                }

                randomChars[hyphenIndex++] = digits[random.Next(0, digits.Length)];
            }
            CardNumber = new string(randomChars);
        }

        static string GenerateCardWithReturnValue()
        {
            string digits = "0123456789";
            char[] randomChars = new char[16 + (16 / 4)]; // Increase the length to account for hyphens
            int hyphenIndex = 0;

            for (int i = 0; i < 16; i++)
            {
                if (i > 0 && i % 4 == 0)
                {
                    randomChars[hyphenIndex++] = '-';
                }

                randomChars[hyphenIndex++] = digits[random.Next(0, digits.Length)];
            }
            return new string(randomChars);
        }

        static void GeneratePinCode()
        {
            PinCode = random.Next(1000, 9999).ToString();
        }

        static void DeleteAccount()
        {
            if (Confirm("delete your bank account"))
            {
                FirstName = string.Empty;
                Surname = string.Empty;
                Password = string.Empty;
                CardNumber = string.Empty;
                SecurityAnswer = string.Empty;
                SecurityQuistionPresented = string.Empty;
                PinCode = string.Empty;
                Balance = 1500;
                DepositMaded = 0;
                HasLoggedInBefore = false;
                HasChangedPinCodeBefore = false;
                HasGenerateCardBefore = false;
                FileManager.RemoevSavedFile(ClientPath);
                DrawMainMenu();
            }
            else
            {
                AccountInfo();
            }
        }

        static void GeneratePage()
        {
            if (HasGenerateCardBefore is false)
            {
                Console.Clear();
                Console.WriteLine("\u001b[34;1m<---------------\u001b[0m[ Generate Page ]\u001b[34;1m--------------->\u001b[0m\n");
                Console.WriteLine("\u001b[35;1m-->\u001b[0m Please note that you only able to change your card number once, no more chances");
                Console.WriteLine("\u001b[35;1m-->\u001b[0m You can generate as many as you want until you accept one");
                string newCard = GenerateCardWithReturnValue();
                Console.WriteLine($"\n\u001b[1mThe New Card Number Is: {newCard}\u001b[0m");
                Console.WriteLine("\n<----------[Actions]---------->");
                Console.WriteLine("\u001b[35;1m1-\u001b[0m Accept");
                Console.WriteLine("\u001b[35;1m2-\u001b[0m Generate New One");
                Console.WriteLine("\u001b[35;1m3-\u001b[0m Return");
                switch(AskForChoice())
                {
                    case 1:    // Accept New Card Case
                        CardNumber = newCard;
                        FileManager.UpdateLineAtIndex(ClientPath, 5, CardNumber);
                        HasGenerateCardBefore = true;
                        FileManager.UpdateLineAtIndex(ClientPath, 11, HasGenerateCardBefore.ToString());
                        AccountInfo(); break;
                    case 2: GeneratePage();  break;
                    case 3: AccountInfo(); break;
                    default: WrongChoice(1300, FileToOpen.E_GeneratePage); break;
                }
            }
            else
            {
                Console.Write("\u001b[31;1mYou have aldreay generated a new card number..\u001b[0m");
                Task.Delay(1600).Wait();
                AccountInfo();
            }
        }

        static void Logut()
        {
            if (Confirm("logout"))
            {
                HasLoggedInBefore = false;
                FileManager.UpdateLineAtIndex(ClientPath, 9, HasLoggedInBefore.ToString());
                Console.Write("\n");
                Console.Write("\u001b[32mSuccessfully logged out\u001b[0m");
                Task.Delay(1400).Wait();
                DrawMainMenu();
            }
            else
            {
                AccountInfo();
            }
        }


        //<-----------------------[Deposit Money]----------------------->
        static void DepositWidget()
        {
            ResetDepositMoney();
            Console.Clear();
            double amount = DepositMaxAmount - DepositMaded;
            Console.WriteLine("\u001b[34;1m<---------------\u001b[0m[ Deposit Page ]\u001b[34;1m--------------->\u001b[0m");
            Console.Write("\n");
            Console.WriteLine($"\u001b[35;1m-->\u001b[0m Your current balance is: ${Balance}");
            Console.WriteLine($"\u001b[35;1m-->\u001b[0m You are able to deposit ${amount} for today..");
            Console.Write("\n\n");
            Console.WriteLine("<----------[Actions]---------->");
            Console.WriteLine("\u001b[35;1m1-\u001b[0m Deposit an amount");
            Console.WriteLine("\u001b[35;1m2-\u001b[0m Back To Bank Account");
            Console.WriteLine("\u001b[35;1m3-\u001b[0m Main Menu");
            switch (AskForChoice())
            {
                case 1: DepositAction(); break;
                case 2: AccountInfo(); break;
                case 3: DrawMainMenu(); break;
                default: WrongChoice(1600, FileToOpen.E_Deposit); break;
            }
        }
        static void DepositAction()
        {
            double moneyLeftToPlayWith = DepositMaxAmount - DepositMaded;
            int delayTime = 2000;

            if (moneyLeftToPlayWith > 0)
            {
                Console.Write("\u001b[35;1m-->\u001b[0m How much do you want to deposit: ");
                string input = Console.ReadLine();
                bool doesSucces = int.TryParse(input, out int amount);
                if (doesSucces && amount <= moneyLeftToPlayWith)
                {
                    Console.Write("\u001b[35;1m-->\u001b[0m Enter your pin code: ");
                    string input2 = Console.ReadLine();
                    bool bIsRightCode = int.TryParse(input2, out int code);
                    if (bIsRightCode && PinCode == code.ToString())
                    {
                        DepositMaded += amount;
                        Balance += amount;
                        FileManager.UpdateLineAtIndex(ClientPath, 7, Balance.ToString());
                        FileManager.UpdateLineAtIndex(ClientPath, 8, DepositMaded.ToString());
                        Console.Write("\u001b[32;1mSuccessfully deposited money.. \u001b[0m");
                        Task.Delay(delayTime).Wait();
                        DepositWidget();
                    }
                    else
                    {
                        Console.Write("\u001b[31;1mNot valid pin code.. \u001b[0m");
                        Task.Delay(delayTime).Wait();
                        DepositWidget();
                    }
                }
                else
                {
                    Console.Write("\u001b[31;1mNot valid amount.. \u001b[0m");
                    Task.Delay(delayTime).Wait();
                    DepositWidget();
                }
            }
            else
            {
                Console.Write("\u001b[31;1mYou've reached the maximum amount of daily depositing.. \u001b[0m");
                Task.Delay(delayTime + 1000).Wait();
                DepositWidget();
            }
        }


        //<-----------------------[Withdraw Money]----------------------->
        static void WithdrawWidget()
        {
            Console.Clear();
            Console.WriteLine("\u001b[34;1m<---------------\u001b[0m[ Withdraw Page ]\u001b[34;1m--------------->\u001b[0m");
            Console.Write("\n\n");
            Console.WriteLine($"\u001b[35;1m-->\u001b[0m Your current balance is: {Balance}$");
            Console.Write("\n\n");
            Console.WriteLine("<----------[Actions]---------->");
            Console.WriteLine("\u001b[35;1m1-\u001b[0m Withdraw an amount");
            Console.WriteLine("\u001b[35;1m2-\u001b[0m Back To Bank Account");
            Console.WriteLine("\u001b[35;1m3-\u001b[0m Main Menu");
            switch (AskForChoice())
            {
                case 1: WithdrawAction(); break;
                case 2: AccountInfo(); break;
                case 3: DrawMainMenu(); break;
                default: WrongChoice(OpenFile: FileToOpen.E_Withdraw); break;
            }
        }
        static void WithdrawAction()
        {
            int delayTime = 2000;
            if (Balance > 0)
            {
                Console.Write("\u001b[35;1m-->\u001b[0m How much do you want to withdraw: ");
                string input = Console.ReadLine();
                bool doesSucces = int.TryParse(input, out int amount);
                if (doesSucces && amount <= Balance)
                {
                    Console.Write("\u001b[35;1m-->\u001b[0m Enter your pin code: ");
                    string input2 = Console.ReadLine();
                    bool bIsRightCode = int.TryParse(input2, out int code);
                    if (bIsRightCode && PinCode == code.ToString())
                    {
                        Balance -= amount;
                        FileManager.UpdateLineAtIndex(ClientPath, 7, Balance.ToString());
                        Console.Write("\u001b[32;1mSuccessfully withdrawed money.. \u001b[0m");
                        Task.Delay(delayTime).Wait();
                        DrawMainMenu();
                    }
                    else
                    {
                        Console.Write("\u001b[31;1mNot valid pin code.. \u001b[0m");
                        Task.Delay(delayTime).Wait();
                        WithdrawWidget();
                    }
                }
                else
                {
                    Console.Write("\u001b[31;1mYou dont own so much money.. \u001b[0m");
                    Task.Delay(delayTime + 1000).Wait();
                    WithdrawWidget();
                }
            }
            else
            {
                Console.Write("\u001b[31;1mYou dont have any money.. \u001b[0m");
                Task.Delay(delayTime).Wait();
                WithdrawWidget();
            }
        }


        //<-----------------------[Donate Money]----------------------->
        static void DonateWidget()
        {
            Console.Clear();
            Console.WriteLine("\u001b[34;1m<---------------\u001b[0m[ Donate Page ]\u001b[34;1m--------------->\u001b[0m");
            Console.WriteLine("\nYour donation, no matter how big or small, can make a world of difference.");
            Console.WriteLine($"Your current balance is: {Balance}$");
            Console.WriteLine("\n<----------[Actions]---------->");
            Console.WriteLine("\u001b[35;1m1-\u001b[0m Donate an amount");
            Console.WriteLine("\u001b[35;1m2-\u001b[0m Back To Bank Account");
            Console.WriteLine("\u001b[35;1m3-\u001b[0m Main Menu");
            switch (AskForChoice())
            {
                case 1: DonateAction(); break;
                case 2: AccountInfo(); break;
                case 3: DrawMainMenu(); break;
                default: WrongChoice(OpenFile: FileToOpen.E_DonatePage); break;
            }
        }
        static void DonateAction()
        {
            int delayTime = 2000;
            if (Balance > 0)
            {
                Console.Write("\u001b[35;1m-->\u001b[0m How much do you want to donate: ");
                string input = Console.ReadLine();
                bool doesSucces = int.TryParse(input, out int amount);
                if (doesSucces && amount <= Balance)
                {
                    Console.Write("\u001b[35;1m-->\u001b[0m Enter your pin code: ");
                    string input2 = Console.ReadLine();
                    bool bIsRightCode = int.TryParse(input2, out int code);
                    if (bIsRightCode && code.ToString() == PinCode)
                    {
                        Balance -= amount;
                        FileManager.UpdateLineAtIndex(ClientPath, 7, Balance.ToString());
                        Console.Write($"\u001b[32;1mThank you Mr. {FirstName}, Successfully Donated ${amount}.. \u001b[0m");
                        Task.Delay(delayTime + 1000).Wait();
                        DrawMainMenu();
                    }
                    else
                    {
                        Console.Write("\u001b[31;1mNot valid pin code.. \u001b[0m");
                        Task.Delay(delayTime).Wait();
                        DonateWidget();
                    }
                }
                else
                {
                    Console.Write("\u001b[31;1mNot valid amount.. \u001b[0m");
                    Task.Delay(delayTime).Wait();
                    DonateWidget();
                }
            }
            else
            {
                Console.Write("\u001b[31;1mYou don't have enough money to donate..\u001b[0m");
                Task.Delay(delayTime + 500).Wait();
                DonateWidget();
            }
        }


        //<-----------------------[Quit Console and other Functionality]----------------------->
        static void QuitApp()
        {
            if (Confirm("quit the app"))
            {
                Environment.Exit(0);
            }
            else
            {
                DrawMainMenu();
            }
        }

        static void WrongChoice(int DelayTime = 2000, FileToOpen OpenFile = FileToOpen.E_MainMenu)
        {
            Console.Write("--> \u001b[31;1m" + ErrorMessages[random.Next(0, ErrorMessages.Length)] + "\u001b[0m");
            Task.Delay(DelayTime).Wait();

            switch (OpenFile)
            {
                case FileToOpen.E_MainMenu: DrawMainMenu(); break;
                case FileToOpen.E_AccountInfo: AccountInfo(); break;
                case FileToOpen.E_Deposit: DepositWidget(); break;
                case FileToOpen.E_Withdraw: WithdrawWidget(); break;
                case FileToOpen.E_GeneratePage: GeneratePage(); break;
                case FileToOpen.E_ResetPassword: ResetPasswordWidget(); break;
                case FileToOpen.E_DonatePage: DonateWidget(); break;
            }
        }

        static bool Confirm(string message)
        {
            Console.WriteLine("\n\n--> You are about to " + message + ", Are you sure?");
            Console.Write("\u001b[41m[1. Confirm]\u001b[0m - \u001b[42m[2. Cancel]\u001b[0m: ");
            string input = Console.ReadLine();
            return input == "1";
        }

        static void ResetPasswordWidget()
        {
            Console.Clear();
            Console.WriteLine("\u001b[34;1m<---------------\u001b[0m[ Reset Page ]\u001b[34;1m--------------->\u001b[0m");
            if (FileManager.DoesFileExist(ClientPath))
            {
                string username = FileManager.GetLineAtIndex(ClientPath, 0);
                Console.WriteLine($"\nHi Mr. {username}, Please answer our security question before you reseting your account");
                Console.WriteLine("\n\n<----------[Actions]---------->");
                Console.WriteLine("\u001b[35;1m1-\u001b[0m Go on");
                Console.WriteLine("\u001b[35;1m2-\u001b[0m Return");
                switch(AskForChoice())
                {
                    case 1: ResetPasswordAction(); break;
                    case 2: AccountInfo(); break;
                    default : WrongChoice(1200, FileToOpen.E_ResetPassword); break;
                }
            }
            else
            {
                Console.WriteLine("\nThere is no data or any bank account saved on your computer");
                Console.WriteLine("Please make sure you signup and have existing account before reseting your password");
                Console.WriteLine("\n\n<----------[Actions]---------->");
                Console.WriteLine("\u001b[35;1m1-\u001b[0m Create Account");
                Console.WriteLine("\u001b[35;1m2-\u001b[0m Main Menu");
                switch (AskForChoice())
                {
                    case 1: Signup(); break;
                    case 2: DrawMainMenu(); break;
                    default: WrongChoice(1200, FileToOpen.E_ResetPassword); break;
                }
            }
        }

        private static void ResetPasswordAction()
        {
            string que = FileManager.GetLineAtIndex(ClientPath, 3);
            Console.Write($"\u001b[35;1m-->\u001b[0m {que}: ");
            string answer = Console.ReadLine();
            if (answer == FileManager.GetLineAtIndex(ClientPath, 4))
            {
                Console.Write("\u001b[35;1m-->\u001b[0m Enter your new password: ");
                string newPass = Console.ReadLine();

                Console.Write("\u001b[35;1m-->\u001b[0m Confirm your new password: ");
                string conPass = Console.ReadLine();

                if (newPass == conPass)
                {
                    Password = newPass;
                    FileManager.UpdateLineAtIndex(ClientPath, 2, Password);
                    Console.Write("\u001b[32;1mSuccessully reseted your bank account, Go on and login..\u001b[0m");
                    Task.Delay(3000).Wait();
                    AccountInfo();
                }

                else
                {
                    Console.Write("\u001b[31;1mThe passwords does not matches, try again..\u001b[0m");
                    Task.Delay(2500).Wait();
                    ResetPasswordWidget();
                }

            }
            else
            {
                Console.Write("\u001b[31;1mIncorrect Answer, Please try again..\u001b[0m");
                Task.Delay(2000).Wait();
                ResetPasswordWidget();
            }
        }

        static void ResetDepositMoney()
        {
            Today = Convert.ToInt32(FileManager.GetLineAtIndex(ClientPath, 12));
            if (DateTime.Now.Day != Today)
            {
                DepositMaded = 0;
                FileManager.UpdateLineAtIndex(ClientPath, 12, DateTime.Now.Day.ToString());
            }
        }

        static void Save()
        {
            FileManager.AddLineToFile(ClientPath, FirstName);
            FileManager.AddLineToFile(ClientPath, Surname);
            FileManager.AddLineToFile(ClientPath, Password);
            FileManager.AddLineToFile(ClientPath, SecurityQuistionPresented);
            FileManager.AddLineToFile(ClientPath, SecurityAnswer);
            FileManager.AddLineToFile(ClientPath, CardNumber);
            FileManager.AddLineToFile(ClientPath, PinCode.ToString());
            FileManager.AddLineToFile(ClientPath, Balance.ToString());
            FileManager.AddLineToFile(ClientPath, DepositMaded.ToString());
            FileManager.AddLineToFile(ClientPath, HasLoggedInBefore.ToString());
            FileManager.AddLineToFile(ClientPath, HasChangedPinCodeBefore.ToString());
            FileManager.AddLineToFile(ClientPath, HasGenerateCardBefore.ToString());
            FileManager.AddLineToFile(ClientPath, DateTime.Now.Day.ToString());
        }

        static void Load()
        {
            if (FileManager.DoesFileExist(ClientPath))
            {
                FirstName = FileManager.GetLineAtIndex(ClientPath, 0);
                Surname = FileManager.GetLineAtIndex(ClientPath, 1);
                Password = FileManager.GetLineAtIndex(ClientPath, 2);
                SecurityQuistionPresented = FileManager.GetLineAtIndex(ClientPath, 3);
                SecurityAnswer = FileManager.GetLineAtIndex(ClientPath, 4);
                CardNumber = FileManager.GetLineAtIndex(ClientPath, 5);
                PinCode = FileManager.GetLineAtIndex(ClientPath, 6);
                Balance = int.Parse(FileManager.GetLineAtIndex(ClientPath, 7));
                DepositMaded = int.Parse(FileManager.GetLineAtIndex(ClientPath, 8));
                HasLoggedInBefore = Convert.ToBoolean(FileManager.GetLineAtIndex(ClientPath, 9));
                HasChangedPinCodeBefore = Convert.ToBoolean(FileManager.GetLineAtIndex(ClientPath, 10));
                HasGenerateCardBefore = Convert.ToBoolean(FileManager.GetLineAtIndex(ClientPath, 11));
                Today = int.Parse(FileManager.GetLineAtIndex(ClientPath, 12));
            }
        }
    }
}
