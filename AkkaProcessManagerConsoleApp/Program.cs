using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaProcessManager;

namespace AkkaProcessManagerConsoleApp {
    class Program {
        public static ActorSystem MyActorSystem;
        static void Main(string[] args) {
            Console.WriteLine("Type \"quote\" to start up to get a new loan quote");
            MyActorSystem = ActorSystem.Create("MyActorSystem");
            var creditBureau = MyActorSystem.ActorOf(Props.Create(() => new CreditBureau()), "creditBureau");
            var bank1 = MyActorSystem.ActorOf(Props.Create(() => new Bank("bank1", 2.75, 0.30)), "bank1");
            var bank2 = MyActorSystem.ActorOf(Props.Create(() => new Bank("bank2", 2.73, 0.31)), "bank2");
            var bank3 = MyActorSystem.ActorOf(Props.Create(() => new Bank("bank3", 2.80, 0.29)), "bank3");
            var loanBroker =
                MyActorSystem.ActorOf(
                    Props.Create(() => new LoanBroker(creditBureau, new List<IActorRef>() { bank1, bank2, bank3 })), "loanBroker");
            while (true) {
                var entry = Console.ReadLine();
                if (entry == "") {
                    loanBroker.Tell(new QuoteBestLoanRate("111-11-111", 100000, 84));
                } else if (entry == "exit") {
                    break;
                }
            }
        }
    }
}
