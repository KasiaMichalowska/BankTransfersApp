using System;
using System.Collections.Generic;
using System.Globalization;

namespace BankTransfers
{
    public class Transfer
    {
        public bool _domestic;
        private Guid _sourceAccountNumberGuid;
        private Guid _destinationAccountNumberGuid;
        private string _destinationAccountNumber;
        public string _transferTitle;
        public decimal _transferAmount;
        public DateTime _transferDate;

        public void PerformDomesticTransfer(BankAccount source, BankAccount destination, string transferTitle, decimal transferAmount, DateTime transferDate)
        {
            _domestic = true;
            _sourceAccountNumberGuid = source.AccountNumber;
            _destinationAccountNumberGuid = destination.AccountNumber;
            _transferTitle = transferTitle;
            _transferAmount = transferAmount;
            _transferDate = transferDate;

            source.AccountBalance -= transferAmount;
            destination.AccountBalance += transferAmount;
        }

        public void PerformOutgoingTransfer(BankAccount source, string destination, decimal transferAmount, string transferTitle,
     DateTime transferDate)
        {
            _domestic = false;
            _sourceAccountNumberGuid = source.AccountNumber;
            _destinationAccountNumber = destination;
            _transferTitle = transferTitle;
            _transferAmount = transferAmount;
            _transferDate = transferDate;

            source.AccountBalance -= transferAmount;
        }

        public override string ToString()
        {
            String transferType = _domestic ? "Domestic transfer" : "Outgoing transfer";
            String source = _sourceAccountNumberGuid.ToString();
            String destination = _domestic ? _destinationAccountNumberGuid.ToString() : _destinationAccountNumber;
            return $"{transferType}\n" +
                   $"   From:   {source}\n" +
                   $"   To:     {destination}\n" +
                   $"   Title:  {_transferTitle}\n" +
                   $"   Date:   {_transferDate.ToString(CultureInfo.CurrentCulture)}\n" +
                   $"   Amount: ${_transferAmount}\n";
        }
    }
}