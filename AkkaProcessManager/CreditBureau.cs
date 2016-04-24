using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;

namespace AkkaProcessManager {
    public class CheckCredit {
        public string CreditProcessReferenceId { get; private set; }
        public string TaxId { get; private set; }

        public CheckCredit(string creditProcessReferenceId, string taxId) {
            CreditProcessReferenceId = creditProcessReferenceId;
            TaxId = taxId;
        }
    }

    public class CreditChecked {
        public string CreditProcessingReferenceId { get; private set; }
        public string TaxId { get; private set; }
        public int Score { get; private set; }

        public CreditChecked(string creditProcessingReferenceId, string taxId, int score) {
            CreditProcessingReferenceId = creditProcessingReferenceId;
            TaxId = taxId;
            Score = score;
        }
    }

    class CreditBureau : ReceiveActor {
        private int[] _creditRanges = new int[] { 300, 400, 500, 600, 700 };
        private Random _randomCreditRangeGenerator = new Random();
        private Random _randomCreditScoreGenerator = new Random();

        private readonly ILoggingAdapter _logger = Context.GetLogger();

        public void CheckCreditHandler(CheckCredit message) {
            _logger.Info("CreditBureau recieved CheckCredit message: " + message);
            int range = _creditRanges[_randomCreditRangeGenerator.Next(5)];
            int score = _randomCreditScoreGenerator.Next(20);
            CreditChecked crediChecked =
                new CreditChecked(
                    message.CreditProcessReferenceId,
                    message.TaxId,
                    score);
            _logger.Info("CreditBureau responded with CreditChecked message: " + message);
            Sender.Tell(crediChecked);
        }
    }
}
