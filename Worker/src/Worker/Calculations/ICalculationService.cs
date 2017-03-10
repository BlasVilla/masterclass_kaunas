namespace Worker.Calculations
{
    public interface ICalculationService
    {
        ICalculationResult Calculate(double input);
    }
}