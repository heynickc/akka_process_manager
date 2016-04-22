using System.Collections.Generic;
using Akka.Actor;

namespace AkkaProcessManager {

    public class StartLoanRateQuote {
        public int ExpectedLoanRateQuotes { get; private set; }

        public StartLoanRateQuote(int expectedLoanRateQuotes) {
            ExpectedLoanRateQuotes = expectedLoanRateQuotes;
        }
    }

    public class LoanRateQuoteStarted {
        public string LoanRateQuoteId { get; private set; }
        public string TaxId { get; private set; }

        public LoanRateQuoteStarted(string loanRateQuoteId, string taxId) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
        }
    }

    public class TerminateLoanRateQuote {
    }

    public class LoanRateQuoteTerminated {
        public string LoanRateQuoteId { get; private set; }
        public string TaxId { get; private set; }

        public LoanRateQuoteTerminated(string loanRateQuoteId, string taxId) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
        }
    }

    public class EstablishCreditScoreForLoanRateQuote {
        public string LoanRateQuoteId { get; private set; }
        public string TaxId { get; private set; }
        public int Score { get; private set; }

        public EstablishCreditScoreForLoanRateQuote(string loanRateQuoteId, string taxId, int score) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Score = score;
        }
    }

    public class CreditScoreForLoanRateQuoteEstablished {
        public string LoanRateQuoteId { get; private set; }
        public string TaxId { get; private set; }
        public int Score { get; private set; }
        public int Amount { get; private set; }
        public int TermInMonths { get; private set; }

        public CreditScoreForLoanRateQuoteEstablished(string loanRateQuoteId, string taxId, int score, int amount, int termInMonths) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Score = score;
            Amount = amount;
            TermInMonths = termInMonths;
        }
    }

    public class CreditScoreForLoanRateQuoteDenied {
        public string LoanRateQuoteId { get; private set; }
        public string TaxId { get; private set; }
        public int Score { get; private set; }
        public int Amount { get; private set; }
        public int TermInMonths { get; private set; }

        public CreditScoreForLoanRateQuoteDenied(string loanRateQuoteId, string taxId, int score, int amount, int termInMonths) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Score = score;
            Amount = amount;
            TermInMonths = termInMonths;
        }
    }

    public class RecordLoanRateQuote {
        public string BankId { get; private set; }
        public string BankLoanRateQuoteId { get; private set; }
        public double InterestRate { get; private set; }

        public RecordLoanRateQuote(string bankId, string bankLoanRateQuoteId, double interestRate) {
            BankId = bankId;
            BankLoanRateQuoteId = bankLoanRateQuoteId;
            InterestRate = interestRate;
        }
    }

    public class LoanRateQuoteRecorded {
        public string LoanRateQuoteId { get; private set; }
        public string TaxId { get; private set; }
        public BankLoanRateQuote BankLoanRateQuote { get; private set; }

        public LoanRateQuoteRecorded(string loanRateQuoteId, string taxId, BankLoanRateQuote bankLoanRateQuote) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            BankLoanRateQuote = bankLoanRateQuote;
        }
    }

    public class LoanRateBestQuoteFilled {
        public string LoanRateQuoteId { get; private set; }
        public string TaxId { get; private set; }
        public int Amount { get; private set; }
        public int TermInMonths { get; private set; }
        public int CreditScore { get; private set; }
        public BankLoanRateQuote BestBankLoanRateQuote { get; private set; }

        public LoanRateBestQuoteFilled(string loanRateQuoteId, string taxId, int amount, int termInMonths, int creditScore, BankLoanRateQuote bestBankLoanRateQuote) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
            CreditScore = creditScore;
            BestBankLoanRateQuote = bestBankLoanRateQuote;
        }
    }

    public class BankLoanRateQuote {
        public string BankId { get; private set; }
        public string BankLoanRateQuoteId { get; private set; }
        public double InterestRate { get; private set; }

        public BankLoanRateQuote(string bankId, string bankLoanRateQuoteId, double interestRate) {
            BankId = bankId;
            BankLoanRateQuoteId = bankLoanRateQuoteId;
            InterestRate = interestRate;
        }
    }

    public class LoanRateQuote : ReceiveActor {
        private IList<BankLoanRateQuote> _bankLoanRateQuotes = new List<BankLoanRateQuote>();
        private int _creditRatingScore;
        private int _expectedLoanRateQuotes;

        private int _amount;
        private IActorRef _loanBroker;
        private string _loanRateQuoteId;
        private string _taxId;
        private int _termInMonths;

        public LoanRateQuote(string loanRateQuoteId, string taxId, int amount, int termInMonths, IActorRef loanBroker) {
            this._loanRateQuoteId = loanRateQuoteId;
            this._taxId = taxId;
            this._amount = amount;
            this._termInMonths = termInMonths;
            this._loanBroker = loanBroker;
        }
    }
}