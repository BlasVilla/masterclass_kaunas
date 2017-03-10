namespace Worker.Calculations
{
    public class SquareCalculationService : ICalculationService
    {
        public ICalculationResult Calculate(double input)
        {
            return new CalculationResult { Method = "X^2", Value = input*input };
        }
    }
}