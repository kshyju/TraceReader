namespace TraceReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var filePath =args.Length > 0 ? args[0] : @"C:\devtools\Perfview\funchost_windows.nettrace";

            //filePath = @"C:\devtools\Perfview\PerfViewData_4.1039.100-ci.25175.0_HelloHttpNet9NoProxy.etl";

            var reader = new TraceReader(filePath);
            reader.ReadTrace();
        }
    }
}
