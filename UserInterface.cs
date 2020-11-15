using System;
using System.Collections.Generic;

namespace BankTransfers
{
    public class UserInterface
    {
        private void WritePrompt(string prompt)
        {
            Console.Write($"\n---- {prompt}: ");
        }      

        //private void WriteError(string error)
        //{
        //    Console.WriteLine($"\n{error}\n");
        //}

        private int ReadIntegerValue(string prompt)
        {
            int userChoice;
            WritePrompt(prompt);
            while (!int.TryParse(Console.ReadLine(), out userChoice))
            {
                Console.Write("Incorrect option - try again...");
                WritePrompt(prompt);
            }

            return userChoice;
        }

        private decimal ReadDecimalValue(string prompt)
        {
            decimal userValue;
            WritePrompt(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out userValue))
            {
                Console.Write("Incorrect decimal value - try again...");
                WritePrompt(prompt);
            }

            return userValue;
        }

        private string ReadStringValue(string prompt, bool allowEmpty = true)
        {
            WritePrompt(prompt);
            var userValue = Console.ReadLine();
            if (!allowEmpty)
            {
                while (userValue != null && userValue.Trim().Length == 0)
                {
                    Console.Write("Provided value cannot be empty");
                    WritePrompt(prompt);
                }
            }

            return userValue;
        }

        public void DisplayMenu()
        {
            // implementacja po nazwie jest bardziej uciążliwa w obsłudze (wymaga wpisywania nazwy opcji menu zamiast numerka)
            Console.WriteLine(" Bank Transfer App - Menu: ");
            Console.WriteLine(" 1. Create account");
            Console.WriteLine(" 2. Domestic transfer");
            Console.WriteLine(" 3. Outgoing transfer");
            Console.WriteLine(" 4. Accounts balance");
            Console.WriteLine(" 5. Transfers history");
            Console.WriteLine(" 6. Exit");
        }

        public int ReadMenu()
        {
            return ReadIntegerValue("Choose option");
        }

        public void DisplayCreateAccountInfo()
        {
            Console.WriteLine("Creating new account");
        }

        public void DisplayAccountInfo(BankAccount account)
        {
            Console.WriteLine(account.ToString());
        }

        public string GetAccountName()
        {
            return ReadStringValue("Provide account name", false);
        }

        public void DisplayTransferStart(List<BankAccount> accounts, bool isDomestic)
        {
            if (isDomestic)
            {
                Console.WriteLine("Domestic Transfer");
            }
            else
            {
                Console.WriteLine("Outgoing Transfer");
            }

            int position = 1;
            foreach (var bankaccount in accounts)
            {
                Console.WriteLine($"{position}. {bankaccount.Name}\t{bankaccount.AccountNumber}\tBalance: ${bankaccount.AccountBalance}");
                position++;
            }
        }

        public void DisplayTransferSummary(Transfer transfer)
        {
            Console.WriteLine("Transfer summary: ");
            Console.WriteLine(transfer.ToString());
        }
        

        public int GetSourceAccountIndex()
        {
            int accountIndex = ReadIntegerValue("Provide source bank account");
            accountIndex--;
            return accountIndex;
        }

        public int GetDestinationAccountIndex()
        {
            int accountIndex = ReadIntegerValue("Provide destination bank account");
            accountIndex--;
            return accountIndex;
        }

        public string GetExternalAccountNumber()
        {
            return ReadStringValue("Provide destination (external) bank account number");
        }

        public String GetTransferTitle()
        {
            return ReadStringValue("Provide transfer title");
        }

        public decimal GetTransferAmount()
        {
            return ReadDecimalValue("Provide transfer amount");
        }

        public void DisplayLessThan2AccountsDomesticError()
        {
            Console.WriteLine("There are less than 2 domestic accounts, cannot do a transfer");
        }

        public void DisplayLessThan1AccountsOutgoingError()
        {
            Console.WriteLine("To do a transfer there has to be at least 1 domestic account");
        }

        public void DisplayIncorrectAccountsError()
        {
            Console.WriteLine("Source or destination account is invalid, cannot do a transfer");
        }

        public void DisplayTransferToTheSameAccountDomesticError()
        {
            Console.WriteLine("Source and destination account is the same, cannot do a transfer");
        }

        public void Display0OrLessTransferAmountError()
        {
            Console.WriteLine("$0 or less cannot be transferred");
        }
        public void DisplayGreaterThanSourceBalanceError()
        {
            Console.WriteLine("Amount greater than source account balance cannot be transferred");
        }

        public void DisplayAccountsBalanceStart()
        {
            Console.WriteLine("Accounts balance");
        }

        public void DisplayAccountsBalance(List<BankAccount> accounts)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts has been created");
            }
            foreach (var account in accounts)
            {
                Console.WriteLine($"Name: {account.Name}, Guid: {account.AccountNumber.ToString()}, Balance: {account.AccountBalance}");
            }
        }
        public void DisplayTransferListStart()
        {
            Console.WriteLine("Transfer history");
        }

        public void DisplayTransfers(List<Transfer> transfers)
        {
            if (transfers.Count == 0)
            {
                Console.WriteLine("No transfers has been sent");
            }
            else
            {
                foreach (var transfer in transfers)
                {
                    Console.WriteLine(transfer.ToString());
                }
            }
        }
    }
}