using System;

namespace BankTransfers
{
    public class BankAccount
    {
        private readonly string _accountName;
        private readonly Guid _accountNumber;
        private decimal _accountBalance;

        public BankAccount(string name)
        {
            _accountName = name;
            _accountNumber = Guid.NewGuid();
            _accountBalance = 1000;
        }

        public decimal AccountBalance
        {
            get => _accountBalance;
            set => _accountBalance = value;
        }

        public string Name => _accountName;

        public Guid AccountNumber => _accountNumber;

        public override string ToString()
        {
            return $"   Account: {_accountName}\n" +
                   $"   Account No: {_accountNumber}\n" +
                   $"   Balance: ${_accountBalance}\n";
        }
    }
}