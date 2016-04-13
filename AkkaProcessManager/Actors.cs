using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaProcessManager {

    public class ProcessManager : ReceiveActor {
        
    }

    public class LoanBroker : ReceiveActor {
        private readonly IActorRef _creditBureau;
        private readonly List<IActorRef> _banks;
    }

    public class LoanRateQuote : ReceiveActor {
        public string LoanRateQuoteId { get; }
        public string TaxId { get; }

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

        private void QuoteLoanRateHandler(QuoteBestLoanRate quoteBestLoanRate) {
            
        }
    }
}
