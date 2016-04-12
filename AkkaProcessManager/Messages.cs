using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Util.Internal;


// Yet, when designing full-fledged actor-based systems and integrating among them and legacy systems, it generally works out to your advantage to define a Canonical Message Model (333)
// https://www.safaribooksonline.com/library/view/reactive-messaging-patterns/9780133846904/ch04.html#ch04

namespace AkkaProcessManager {

    class QuoteLoanRate {
        public string TaxId { get; }
        public int Amount { get; }
        public int TermInMonths { get; }

        public QuoteLoanRate(string taxId, int amount, int termInMonths) {
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
        }
    }

    class BestLoanRateQuoted {
        public string BankId { get; }
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }
        public int Amount { get; }
        public int TermInMonths { get; }
        public int CreditScore { get; }
        public double InterestRate { get; }

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
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }
        public int Amount { get; }
        public int TermInMonths { get; }
        public int CreditScore { get; }

        public BestLoanRateDenied(string loanRateQuoteId, string taxId, int amount, int termInMonths, int creditScore) {
            LoanRateQuoteId = loanRateQuoteId;
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
            CreditScore = creditScore;
        }
    }
}
