using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaProcessManager {

    public class Bank : ReceiveActor {
        private readonly string _bankId;
        private readonly double _primeRate;
        private readonly double _ratePremium;
        private readonly Random _randomDiscount;
        private readonly Random _randomQuoteId;

        public Bank(string bankId, double primeRate, double ratePremium) {
            _bankId = bankId;
            _primeRate = primeRate;
            _ratePremium = ratePremium;
            _randomDiscount = new Random();
            _randomQuoteId = new Random();
        }

        private double calculateInterestRate(double amount, double months, double creditScore) {
            var creditScoreDiscount = creditScore / 100.0 / 10.0 - (_randomDiscount.Next(5) * 0.05);
            return _primeRate + _ratePremium + ((months / 12.0) / 10.0) - creditScoreDiscount;
        }

        private void QuoteLoanRateHandler(QuoteLoanRate quoteLoanRate) {
            
        }
    }
}
