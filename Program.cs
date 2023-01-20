namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = "";
            do
            {
                Console.WriteLine("What script to run?");
                name = Console.ReadLine();
                Console.WriteLine($"{Environment.NewLine}Running script {name}!");

                switch (name)
                {
                    case "05":
                    case "5":
                        Console.WriteLine($"{Environment.NewLine} Script {name} starting!");
                        var script5 = new Script05();
                        script5.run();
                        break;
                    case "05b":
                    case "5b":
                        Console.WriteLine($"{Environment.NewLine} Script {name} starting!");
                        var script5b = new Script05b();
                        script5b.run();
                        break;
                    case "12":
                        Console.WriteLine($"{Environment.NewLine} Script {name} starting!");
                        var script = new Script12();
                        script.run();
                        break;
                    case "12b":
                        Console.WriteLine($"{Environment.NewLine} Script {name} starting!");
                        var script12b = new Script12b();
                        script12b.run();
                        break;
                    default:
                        Console.WriteLine($"{Environment.NewLine} Script {name} not known!");
                        break;
                }

            } while (name != "x");





            Console.Write($"{Environment.NewLine}Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}