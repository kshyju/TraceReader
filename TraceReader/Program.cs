namespace TraceReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var filePath =args.Length > 0 ? args[0] : @"D:\devtools\perfview\azfunc_HelloHttpNet9NoProxy_Linux.nettrace";

            var reader = new TraceReader(filePath);
            reader.ReadTrace();
        }
    }
}
