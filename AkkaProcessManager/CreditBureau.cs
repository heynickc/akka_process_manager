using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

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
    }
}
