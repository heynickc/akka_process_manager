using System;
using Akka.Actor;

namespace AkkaProcessManager {

    public class QuoteLoanRate {
        public string LoadQuoteReferenceId { get; private set; }
        public string TaxId { get; private set; }
        public int CreditScore { get; private set; }
        public int Amount { get; private set; }
        public int TermInMonths { get; private set; }

        public QuoteLoanRate(string loadQuoteReferenceId, string taxId, int creditScore, int amount, int termInMonths) {
            LoadQuoteReferenceId = loadQuoteReferenceId;
            TaxId = taxId;
            CreditScore = creditScore;
            Amount = amount;
            TermInMonths = termInMonths;
        }
    }

    public class BankLoanRateQuoted {
        public string BankId { get; private set; }
        public string BankLoanRateQuoteId { get; private set; }
        public string LoanQuoteReferenceId { get; private set; }
        public string TaxId { get; private set; }
        public double InterestRate { get; private set; }

        public BankLoanRateQuoted(string bankId, string bankLoanRateQuoteId, string loanQuoteReferenceId, string taxId, double interestRate) {
            BankId = bankId;
            BankLoanRateQuoteId = bankLoanRateQuoteId;
            LoanQuoteReferenceId = loanQuoteReferenceId;
            TaxId = taxId;
            InterestRate = interestRate;
        }
    }

    public class Bank : ReceiveActor {
        public string BankId { get; private set; }
        public double PrimeRate { get; private set; }
        public double RatePremium { get; private set; }
        public Random RandomDiscount { get; private set; }
        public Random RandomQuoteId { get; private set; }

        public Bank(string bankId, double primeRate, double ratePremium) {
            BankId = bankId;
            PrimeRate = primeRate;
            RatePremium = ratePremium;
            RandomDiscount = new Random();
            RandomQuoteId = new Random();
        }

        private double calculateInterestRate(double amount, double months, double creditScore) {
            var creditScoreDiscount = creditScore / 100.0 / 10.0 - (RandomDiscount.Next(5) * 0.05);
            return PrimeRate + RatePremium + ((months / 12.0) / 10.0) - creditScoreDiscount;
        }

        private void QuoteLoanRateHandler(QuoteLoanRate quoteLoanRate) {
            
        }
    }

}