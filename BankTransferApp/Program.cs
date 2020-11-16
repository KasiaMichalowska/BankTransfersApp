using System;

namespace BankTransfers
{
    class Program
    {
        private Bank _bank;
        private UserInterface _userInterface;

        private void Run()
        {
            _userInterface = new UserInterface();
            _bank = new Bank();

            do
            {
                _userInterface.DisplayMenu();
                var selectedMenuOption = _userInterface.ReadMenu();

                switch (selectedMenuOption)
                {
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        DomesticTransfer();
                        break;
                    case 3:
                        OutgoingTransfer();
                        break;
                    case 4:
                        ListAccountsBalance();
                        break;
                    case 5:
                        ListTransfers();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Unknown menu option");
                        break;
                }
            } while (true);
        }

        private void CreateAccount()
        {
            _userInterface.DisplayCreateAccountInfo();
            var accountName = _userInterface.GetAccountName();
            var account = _bank.CreateAccount(accountName);
            Console.WriteLine("Creating new account");
        }

        private void DomesticTransfer()
        {
            if (_bank.GetAccounts().Count <= 1)
            {
                _userInterface.DisplayLessThan2AccountsDomesticError();
                return;
            }

            _userInterface.DisplayTransferStart(_bank.GetAccounts(), true);
            BankAccount source = _bank.GetBankAccount(_userInterface.GetSourceAccountIndex());
            BankAccount destination = _bank.GetBankAccount(_userInterface.GetDestinationAccountIndex());

            if (source == null || destination == null)
            {
                _userInterface.DisplayIncorrectAccountsError();
                return;
            }

            if (source == destination)
            {
                _userInterface.DisplayTransferToTheSameAccountDomesticError();
                return;
            }

            string transferTitle = _userInterface.GetTransferTitle();
            decimal transferAmount = _userInterface.GetTransferAmount();

            if (transferAmount <= 0)
            {
                _userInterface.Display0OrLessTransferAmountError();
                return;
            }

            if (transferAmount > source.AccountBalance)
            {
                _userInterface.DisplayGreaterThanSourceBalanceError();
                return;
            }

            Transfer transfer = new Transfer();
            transfer.PerformDomesticTransfer(
                source,
                destination,
                transferTitle,
                transferAmount,
                DateTime.Now);

            _bank.RegisterTransfer(transfer);
            _userInterface.DisplayTransferSummary(transfer);
        }

        private void OutgoingTransfer()
        {
            if (_bank.GetAccounts().Count < 1)
            {
                _userInterface.DisplayLessThan1AccountsOutgoingError();
                return;
            }

            _userInterface.DisplayTransferStart(_bank.GetAccounts(), false);
            BankAccount source = _bank.GetBankAccount(_userInterface.GetSourceAccountIndex());
            String destination = _userInterface.GetExternalAccountNumber();

            if (source == null || destination.Length == 0)
            {
                _userInterface.DisplayIncorrectAccountsError();
                return;
            }

            string transferTitle = _userInterface.GetTransferTitle();
            decimal transferAmount = _userInterface.GetTransferAmount();

            if (transferAmount <= 0)
            {
                _userInterface.Display0OrLessTransferAmountError();
                return;
            }

            if (transferAmount > source.AccountBalance)
            {
                _userInterface.DisplayGreaterThanSourceBalanceError();
                return;
            }

            Transfer transfer = new Transfer();
            transfer.PerformOutgoingTransfer(
                source,
                destination,
                transferAmount,
                transferTitle,
                DateTime.Now);

            _bank.RegisterTransfer(transfer);
            _userInterface.DisplayTransferSummary(transfer);
        }

        private void ListAccountsBalance()
        {
            _userInterface.DisplayAccountsBalanceStart();
            _userInterface.DisplayAccountsBalance(_bank.GetAccounts());
        }

        private void ListTransfers()
        {
            _userInterface.DisplayTransferListStart();
            _userInterface.DisplayTransfers(_bank.GetTransfers());
        }

        public static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }
    }
}