namespace Worker.Calculations
{
    public class IdentityCalculationService : ICalculationService
    {
        public ICalculationResult Calculate(double input)
        {
            return new CalculationResult { Method = "Identity", Value = input };
        }
    }
}