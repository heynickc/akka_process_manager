using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using Newtonsoft.Json;

namespace AkkaProcessManager {

    public class QuoteBestLoanRate {
        public string TaxId { get; private set; }
        public int Amount { get; private set; }
        public int TermInMonths { get; private set; }

        public QuoteBestLoanRate(string taxId, int amount, int termInMonths) {
            TaxId = taxId;
            Amount = amount;
            TermInMonths = termInMonths;
        }
    }

    public class BestLoanRateQuoted {
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

    public class LoanBroker : ProcessManager {
        private readonly IActorRef _creditBureau;
        private readonly List<IActorRef> _banks;

        private readonly ILoggingAdapter _logger = Context.GetLogger();

        public LoanBroker(IActorRef creditBureau, List<IActorRef> banks) {
            _creditBureau = creditBureau;
            _banks = banks;

            Receive<BankLoanRateQuoted>(
                message => BankLoanRateQuotedHandler(message));
            Receive<CreditChecked>(
                message => CreditCheckedHandler(message));
            Receive<CreditScoreForLoanRateQuoteDenied>(
                message => CreditScoreForLoanRateQuoteDeniedHandler(message));
            Receive<CreditScoreForLoanRateQuoteEstablished>(
                message => CreditScoreForLoanRateQuoteEstablishedHandler(message));
            Receive<LoanRateBestQuoteFilled>(
                message => LoanRateBestQuoteFilledHandler(message));
            Receive<LoanRateQuoteRecorded>(
                message => LoanRateQuoteRecordedHandler(message));
            Receive<LoanRateQuoteStarted>(
                message => LoanRateQuoteStartedHandler(message));
            Receive<LoanRateQuoteTerminated>(
                message => LoanRateQuoteTerminatedHandler(message));
            Receive<ProcessStarted>(
                message => ProcessStartedHandler(message));
            Receive<ProcessStopped>(
                message => ProcessStoppedHandler(message));
            Receive<QuoteBestLoanRate>(
                message => QuoteBestLoanRateHandler(message));
        }

        private void BankLoanRateQuotedHandler(BankLoanRateQuoted message) {
            _logger.Info("LoanBroker received BankLoanRateQuoted message:\n{0}",
                JsonConvert.SerializeObject(message));
            ProcessOf(message.LoanQuoteReferenceId).Tell(
                new RecordLoanRateQuote(message.BankId,
                    message.BankLoanRateQuoteId,
                    message.InterestRate));
        }

        private void CreditCheckedHandler(CreditChecked message) {
            _logger.Info("LoanBroker recieved CreditChecked message:\n{0}",
                JsonConvert.SerializeObject(message));
            ProcessOf(message.CreditProcessingReferenceId).Tell(
                new EstablishCreditScoreForLoanRateQuote(message.CreditProcessingReferenceId,
                    message.TaxId,
                    message.Score));
        }

        private void CreditScoreForLoanRateQuoteDeniedHandler(CreditScoreForLoanRateQuoteDenied message) {
            _logger.Info("LoanBroker recieved CreditScoreForLoanRateQuoteDenied message:\n{0}",
                JsonConvert.SerializeObject(message));
            ProcessOf(message.LoanRateQuoteId).Tell(
                new TerminateLoanRateQuote());
            var denied = new BestLoanRateDenied(
                message.LoanRateQuoteId,
                message.TaxId,
                message.Amount,
                message.TermInMonths,
                message.Score);
            _logger.Info("Would be sent to original requester:\n{0}",
                JsonConvert.SerializeObject(denied));
        }

        private void CreditScoreForLoanRateQuoteEstablishedHandler(CreditScoreForLoanRateQuoteEstablished message) {
            _logger.Info("LoanBroker recieved CreditScoreForLoanRateQuoteEstablished message:\n{0}",
                JsonConvert.SerializeObject(message));
            foreach (var bank in _banks) {
                bank.Tell(
                    new QuoteLoanRate(message.LoanRateQuoteId,
                    message.TaxId,
                    message.Score,
                    message.Amount,
                    message.TermInMonths));
            }
        }

        private void LoanRateBestQuoteFilledHandler(LoanRateBestQuoteFilled message) {
            _logger.Info("LoanBroker recieved LoanRateBestQuoteFilled message:\n{0}",
                JsonConvert.SerializeObject(message));
            StopProcess(message.LoanRateQuoteId);
            var best = new BestLoanRateQuoted(
                message.BestBankLoanRateQuote.BankId,
                message.LoanRateQuoteId,
                message.TaxId,
                message.Amount,
                message.TermInMonths,
                message.CreditScore,
                message.BestBankLoanRateQuote.InterestRate);
            _logger.Info("Would be sent to the original requester:\n{0}",
                JsonConvert.SerializeObject(best));
        }

        private void LoanRateQuoteRecordedHandler(LoanRateQuoteRecorded message) {
            _logger.Info("LoanBroker recieved LoanRateQuoteRecorded message:\n{0}",
                JsonConvert.SerializeObject(message));

            // Other processing
        }

        private void LoanRateQuoteStartedHandler(LoanRateQuoteStarted message) {
            _logger.Info("LoanBroker recieved LoanRateQuoteStarted message:\n{0}",
                JsonConvert.SerializeObject(message));
            _creditBureau.Tell(new CheckCredit(
                message.LoanRateQuoteId,
                message.TaxId));
        }

        private void LoanRateQuoteTerminatedHandler(LoanRateQuoteTerminated message) {
            _logger.Info("LoanBroker recieved LoanRateQuoteTerminated message:\n{0}",
                JsonConvert.SerializeObject(message));
            StopProcess(message.LoanRateQuoteId);
        }

        private void ProcessStartedHandler(ProcessStarted message) {
            _logger.Info("LoanBroker recieved ProcessStarted message for ProcessId: {0}", message.ProcessId);
            message.Process.Tell(new StartLoanRateQuote(_banks.Count));
        }

        private void ProcessStoppedHandler(ProcessStopped message) {
            _logger.Info("LoanBroker recieved ProcessStopped message for ProcessId: {0}", message.ProcessId);
            Context.Stop(message.Process);
        }

        private void QuoteBestLoanRateHandler(QuoteBestLoanRate message) {
            var loanRateQuoteId = Guid.NewGuid().ToString();
            _logger.Info("Starting QuoteBestLoanRate:\n{0}",
                JsonConvert.SerializeObject(message));
            IActorRef loanRateQuote = Context.ActorOf(
                Props.Create(() => new LoanRateQuote(
                    loanRateQuoteId,
                    message.TaxId,
                    message.Amount,
                    message.TermInMonths,
                    Self)));
            StartProcess(loanRateQuoteId, loanRateQuote);
        }
    }
}