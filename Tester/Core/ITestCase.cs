namespace Tester.Core
{
    public interface ITestCase
    {
        void Prepare();

        void Run();

        void Finish();
    }
}
