using System;
using Akka.Actor;

namespace AkkaProcessManager {

    public class QuoteLoanRate {
        public string LoadQuoteReferenceId { get; }
        public string TaxId { get; }
        public int CreditScore { get; }
        public int Amount { get; }
        public int TermInMonths { get; }

        public QuoteLoanRate(string loadQuoteReferenceId, string taxId, int creditScore, int amount, int termInMonths) {
            LoadQuoteReferenceId = loadQuoteReferenceId;
            TaxId = taxId;
            CreditScore = creditScore;
            Amount = amount;
            TermInMonths = termInMonths;
        }
    }

    public class BankLoanRateQuoted {
        public string BankId { get; }
        public string BankLoanRateQuoteId { get; }
        public string LoanQuoteReferenceId { get; }
        public string TaxId { get; }
        public double InterestRate { get; }

        public BankLoanRateQuoted(string bankId, string bankLoanRateQuoteId, string loanQuoteReferenceId, string taxId, double interestRate) {
            BankId = bankId;
            BankLoanRateQuoteId = bankLoanRateQuoteId;
            LoanQuoteReferenceId = loanQuoteReferenceId;
            TaxId = taxId;
            InterestRate = interestRate;
        }
    }

    public class Bank : ReceiveActor {
        public string BankId { get; }
        public double PrimeRate { get; }
        public double RatePremium { get; }
        public Random RandomDiscount { get; }
        public Random RandomQuoteId { get; }

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