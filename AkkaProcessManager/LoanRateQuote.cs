using System.Collections.Generic;
using System.Xml.Schema;
using Akka.Actor;
using Newtonsoft.Json.Bson;

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

            Receive<StartLoanRateQuote>(
                message => StartLoanRateQuoteHandler(message));
            Receive<EstablishCreditScoreForLoanRateQuote>(
                message => EstablishCreditScoreForLoanRateQuoteHandler(message));
            Receive<RecordLoanRateQuote>(
                message => RecordLoanRateQuoteHandler(message));
        }

        private void StartLoanRateQuoteHandler(StartLoanRateQuote message) {
            _expectedLoanRateQuotes = message.ExpectedLoanRateQuotes;
            _loanBroker.Tell(
                new LoanRateQuoteStarted(
                    _loanRateQuoteId,
                    _taxId));
        }

        private void EstablishCreditScoreForLoanRateQuoteHandler(EstablishCreditScoreForLoanRateQuote message) {
            _creditRatingScore = message.Score;
            if (QuotableCreditScore(_creditRatingScore)) {
                _loanBroker.Tell(
                    new CreditScoreForLoanRateQuoteEstablished(
                        _loanRateQuoteId,
                        _taxId,
                        _creditRatingScore,
                        _amount,
                        _termInMonths));
            }
            else {
                _loanBroker.Tell(
                    new CreditScoreForLoanRateQuoteDenied(
                        _loanRateQuoteId,
                        _taxId,
                        _amount,
                        _termInMonths,
                        _creditRatingScore));
            }
        }

        private void RecordLoanRateQuoteHandler(RecordLoanRateQuote message) {
            var bankLoanRateQuote = 
                new BankLoanRateQuote(
                    message.BankId,
                    message.BankLoanRateQuoteId,
                    message.InterestRate);
            _bankLoanRateQuotes.Add(bankLoanRateQuote);
            _loanBroker.Tell(
                new LoanRateQuoteRecorded(
                    _loanRateQuoteId,
                    _taxId,
                    bankLoanRateQuote));
            if (_bankLoanRateQuotes.Count >= _expectedLoanRateQuotes) {
                _loanBroker.Tell(
                    new LoanRateBestQuoteFilled(
                        _loanRateQuoteId,
                        _taxId,
                        _amount,
                        _termInMonths,
                        _creditRatingScore,
                        BestBankLoanRateQuote()));
            }
        }

        private bool QuotableCreditScore(int score) {
            return score > 399;
        }

        private BankLoanRateQuote BestBankLoanRateQuote() {
            var best = _bankLoanRateQuotes[0];
            foreach (var bankLoanRateQuote in _bankLoanRateQuotes) {
                if (best .InterestRate > bankLoanRateQuote.InterestRate) {
                    best = bankLoanRateQuote;
                }
            }
            return best;
        }
    }
}