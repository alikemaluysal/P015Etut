using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Abstract;

namespace App.Data.Example
{
    internal class Test
    {
        public void TestMethod()
        {
            IService service = new CallService();



            service.DoSomething();
            service.DoSomething();
            service.DoSomething();
            service.DoSomething();
            service.DoSomething();

        }
    }
}
