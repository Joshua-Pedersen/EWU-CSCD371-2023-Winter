namespace Calculate
{
    public class Program
    {
        public Program() { }

        public Action<string> WriteLine { get; set; } = Console.WriteLine;

        public Func<string?> ReadLine { get; set; } = Console.ReadLine;

        public static void Main() 
        {
            Program prog = new();
            Calculator calc = new();
            string? input = "q";
            do
            {
                
                prog.WriteLine("Please enter a two variable, single operand equation. \n" +
                    "Format your equation as follows: '2 + 3'");

                prog.WriteLine("Enter 'q' to quit.");
                
                input = prog.ReadLine();

                if (input is not null && !input.Equals('q'))
                {
                    if(calc.TryCalculate(input, out double result))
                    {
                        prog.WriteLine("The result of " + input + " is " + result);
                    }

                    else {

                        prog.WriteLine("Error, invalid calculation");
                    }
                }

            }while (input != "q");
        }
    }
}