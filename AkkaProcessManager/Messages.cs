using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Util.Internal;


// Yet, when designing full-fledged actor-based systems and integrating among them and legacy systems, it generally works out to your advantage to define a Canonical Message Model (333)
// https://www.safaribooksonline.com/library/view/reactive-messaging-patterns/9780133846904/ch04.html#ch04

namespace AkkaProcessManager {

    // LoanBroker
    class QuoteBestLoanRate {
        public string TaxId { get; private set; }
        public int Amount { get; private set; }
        public int TermInMonths { get; private set; }

        public QuoteBestLoanRate(string taxId, int amount, int termInMonths) {
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
        }
    }

    class BestLoanRateQuoted {
        public string BankId { get; private set; }
        public string LoanRateQuoteId { get; private set; }
        public string TaxId { get; private set; }
        public int Amount { get; private set; }
        public int TermInMonths { get; private set; }
        public int CreditScore { get; private set; }
        public double InterestRate { get; private set; }

        public BestLoanRateQuoted(string bankId, string loanRateQuoteId, string taxId, int amount, int termInMonths, int creditScore, double interestRate) {
            BankId = bankId;
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
            CreditScore = creditScore;
            InterestRate = interestRate;
        }
    }

    public class BestLoanRateDenied {
        public string LoanRateQuoteId { get; private set; }
        public string TaxId { get; private set; }
        public int Amount { get; private set; }
        public int TermInMonths { get; private set; }
        public int CreditScore { get; private set; }

        public BestLoanRateDenied(string loanRateQuoteId, string taxId, int amount, int termInMonths, int creditScore) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
            CreditScore = creditScore;
        }
    }

    // LoanRateQuote
    public class StartLoanRateQuote {
        public int ExpectedLoanRateQuotes { get; private set; }

        public StartLoanRateQuote(int expectedLoanRateQuotes) {
            ExpectedLoanRateQuotes = expectedLoanRateQuotes;
        }
    }

    public class LoanRateQuoteStarted {
        public string LowRateQuoteId { get; private set; }
        public string TaxId { get; private set; }

        public LoanRateQuoteStarted(string lowRateQuoteId, string taxId) {
            LowRateQuoteId = lowRateQuoteId;
            TaxId = taxId;
        }
    }
}
