namespace Calculate
{
    public class Calculator
    {
        private IReadOnlyDictionary<char, Func<int, int, double>> MathematicalOperations
        { get; } = new Dictionary<char, Func<int, int, double>>()
            {
                { '+', Add },
                { '-', Subtract },
                { '*', Multiply },
                { '/', Divide }
            };

        public bool TryCalculate(string calculation, out double result)
        {
            bool validity = false;
            result = default;

            string[] calcItems = calculation.Split(' ');

            if (calcItems.Length == 3)
            {
                if (calcItems[1].Any(key => MathematicalOperations.Keys.Contains(key)))
                { 
                    if (int.TryParse(calcItems[0], out int x)
                        && char.TryParse(calcItems[1], out char key)
                        && int.TryParse(calcItems[2], out int y))
                    { 
                        validity = true;
                        Func<int, int, double> func = MathematicalOperations[key];
                        result = func(x, y);
                    
                    }
                }
            }

            return validity;
        }

        private static double Add(int x, int y)
        {
            return x + y;
        }

        private static double Subtract(int x, int y)
        {
            return x - y;
        }

        private static double Multiply(int x, int y)
        {
            return x * y;
        }

        private static double Divide(int x, int y)
        {
            return (double)x / y;
        }

        
    }
}
