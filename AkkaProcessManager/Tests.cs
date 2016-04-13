using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.TestKit.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace AkkaProcessManager {
    public class Tests : TestKit {
        private readonly ITestOutputHelper _output;

        public Tests(ITestOutputHelper output) {
            _output = output;
        }

        [Fact]
        public void can_spin_up_processManagerDriver_actors() {

            var creditBureau = Sys.ActorOf(Props.Create(() => new CreditBureau()), "creditBureau");
            var bank1 = Sys.ActorOf(Props.Create(() => new Bank("bank1", 2.75, 0.30)), "bank1");
            var bank2 = Sys.ActorOf(Props.Create(() => new Bank("bank2", 2.73, 0.31)), "bank2");
            var bank3 = Sys.ActorOf(Props.Create(() => new Bank("bank3", 2.80, 0.29)), "bank3");
            var loanBroker = Sys.ActorOf(Props.Create(() => new LoanBroker()), "loanBroker");

            var creditBureauPath = ActorSelection(@"/user/creditBureau")
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result.Path.ToString();
            var bank1Path = ActorSelection(@"/user/bank1")
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result.Path.ToString();
            var bank2Path = ActorSelection(@"/user/bank2")
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result.Path.ToString();
            var bank3Path = ActorSelection(@"/user/bank3")
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result.Path.ToString();
            var loanBrokerPath = ActorSelection(@"/user/loanBroker")
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result.Path.ToString();

            Assert.Equal(creditBureauPath, @"akka://test/user/creditBureau");
            Assert.Equal(bank1Path, @"akka://test/user/bank1");
            Assert.Equal(bank2Path, @"akka://test/user/bank2");
            Assert.Equal(bank3Path, @"akka://test/user/bank3");
            Assert.Equal(loanBrokerPath, @"akka://test/user/loanBroker");
        }
    }
}
