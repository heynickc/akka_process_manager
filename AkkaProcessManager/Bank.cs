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
        private string _bankId;
        private double _primeRate;
        private double _ratePremium;
        private Random _randomQuoteId = new Random();
        public Random _randomDiscount = new Random();

        public Bank(string bankId, double primeRate, double ratePremium) {
            _bankId = bankId;
            _primeRate = primeRate;
            _ratePremium = ratePremium;

            Receive<QuoteLoanRate>(
                quoteLoanRate => 
                    QuoteLoanRateHandler(quoteLoanRate));
        }

        private void QuoteLoanRateHandler(QuoteLoanRate message) {
            var interestRate =
                CalculateInterestRate(
                    (double) message.Amount,
                    (double) message.TermInMonths,
                    (double) message.CreditScore);
            var quoted =
                new BankLoanRateQuoted(
                    this._bankId,
                    this._randomQuoteId.Next(1000).ToString(),
                    message.LoadQuoteReferenceId,
                    message.TaxId,
                    interestRate);
            Sender.Tell(quoted);
        }

        private double CalculateInterestRate(double amount, double months, double crediScore) {
            var creditScoreDiscount = crediScore / 100.0 / 10.0 - (this._randomDiscount.Next(5) * 0.05);
            return _primeRate + _ratePremium + ((months / 12.0) / 10.0) - creditScoreDiscount;
        }
    }
}