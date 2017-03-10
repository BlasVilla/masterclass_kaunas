using System;

namespace Worker.Calculations
{
    public class SquareRootCalculationService : ICalculationService
    {
        public ICalculationResult Calculate(double input)
        {
            return new CalculationResult { Method = "Square Root", Value = Math.Sqrt(input) };
        }
    }
}