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

        public static Func<int, int, double> Add { get; set; } = (v1, v2) => v1 + v2;
        public static Func<int, int, double> Subtract { get; set; } = (v1, v2) => v1 - v2;
        public static Func<int, int, double> Multiply { get; set; } = (v1, v2) => v1 * v2;
        public static Func<int, int, double> Divide { get; set; } = (v1, v2) => v1 / v2;
        
    }
}
