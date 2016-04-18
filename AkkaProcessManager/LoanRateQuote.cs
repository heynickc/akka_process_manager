using Akka.Actor;

namespace AkkaProcessManager {

    public class StartLoanRateQuote {
        public int ExpectedLoanRateQuotes { get; }

        public StartLoanRateQuote(int expectedLoanRateQuotes) {
            ExpectedLoanRateQuotes = expectedLoanRateQuotes;
        }
    }

    public class LoanRateQuoteStarted {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }

        public LoanRateQuoteStarted(string loanRateQuoteId, string taxId) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
        }
    }

    public class TerminateLoanRateQuote {
    }

    public class LoanRateQuoteTerminated {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }

        public LoanRateQuoteTerminated(string loanRateQuoteId, string taxId) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
        }
    }

    public class EstablishCreditScoreForLoanRateQuote {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }
        public int Score { get; }

        public EstablishCreditScoreForLoanRateQuote(string loanRateQuoteId, string taxId, int score) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Score = score;
        }
    }

    public class CreditScoreForLoanRateQuoteEstablished {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }
        public int Score { get; }
        public int Amount { get; }
        public int TermInMonths { get; }

        public CreditScoreForLoanRateQuoteEstablished(string loanRateQuoteId, string taxId, int score, int amount, int termInMonths) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Score = score;
            Amount = amount;
            TermInMonths = termInMonths;
        }
    }

    public class CreditScoreForLoanRateQuoteDenied {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }
        public int Score { get; }
        public int Amount { get; }
        public int TermInMonths { get; }

        public CreditScoreForLoanRateQuoteDenied(string loanRateQuoteId, string taxId, int score, int amount, int termInMonths) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Score = score;
            Amount = amount;
            TermInMonths = termInMonths;
        }
    }

    public class RecordLoanRateQuote {
        public string BankId { get; }
        public string BankLoanRateQuoteId { get; }
        public double InterestRate { get; }

        public RecordLoanRateQuote(string bankId, string bankLoanRateQuoteId, double interestRate) {
            BankId = bankId;
            BankLoanRateQuoteId = bankLoanRateQuoteId;
            InterestRate = interestRate;
        }
    }

    public class LoanRateQuoteRecorded {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }
        public BankLoanRateQuote BankLoanRateQuote { get; }

        public LoanRateQuoteRecorded(string loanRateQuoteId, string taxId, BankLoanRateQuote bankLoanRateQuote) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            BankLoanRateQuote = bankLoanRateQuote;
        }
    }

    public class LoanRateBestQuoteFilled {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }
        public int Amount { get; }
        public int TermInMonths { get; }
        public int CreditScore { get; }
        public BankLoanRateQuote BestBankLoanRateQuote { get; }

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
        public string BankId { get; }
        public string BankLoanRateQuoteId { get; }
        public double InterestRate { get; }

        public BankLoanRateQuote(string bankId, string bankLoanRateQuoteId, double interestRate) {
            BankId = bankId;
            BankLoanRateQuoteId = bankLoanRateQuoteId;
            InterestRate = interestRate;
        }
    }

    public class LoanRateQuote : ReceiveActor {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }

    }
}