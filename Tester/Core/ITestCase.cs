using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.TestCases.Core
{
    public interface ITestCase
    {
        void Prepare();

        void Run();

        void Finish();
    }
}
