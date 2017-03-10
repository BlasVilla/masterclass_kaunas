namespace Worker.Calculations
{
    public class CalculationResult : ICalculationResult
    {
        public string Method { get; set; }

        public double Value { get; set; }
    }
}