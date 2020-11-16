using System.Collections.Generic;

namespace BankTransfers
{
    public class Bank
    {
        private readonly List<BankAccount> _accounts;
        private readonly List<Transfer> _transfers;

        public Bank()
        {
            _accounts = new List<BankAccount>();
            _transfers = new List<Transfer>();
        }

        public BankAccount CreateAccount(string accountName)
        {
            BankAccount newAccount = new BankAccount(accountName);
            _accounts.Add(newAccount);

            return newAccount;
        }

        public List<BankAccount> GetAccounts()
        {
            return _accounts;
        }

        public void RegisterTransfer(Transfer transfer)
        {
            _transfers.Add(transfer);
        }

        public BankAccount GetBankAccount(int index)
        {
            if (index >= 0 && index < _accounts.Count)
            {
                return _accounts[index];
            }

            return null;
        }

        public List<Transfer> GetTransfers()
        {
            return _transfers;
        }
    }
}