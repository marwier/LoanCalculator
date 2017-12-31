namespace Tester.TestCases.Core
{
    public interface ITestCase
    {
        void Prepare();

        void Run();

        void Finish();
    }
}
