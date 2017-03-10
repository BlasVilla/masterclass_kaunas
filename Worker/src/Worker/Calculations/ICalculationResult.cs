namespace Worker.Calculations
{
    public interface ICalculationResult
    {
        string Method { get; }

        double Value { get; }
    }
}