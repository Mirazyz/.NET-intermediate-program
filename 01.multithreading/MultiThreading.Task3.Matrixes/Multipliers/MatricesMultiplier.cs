using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System.Diagnostics;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplier : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var resultMatrix = new Matrix(m1.RowCount, m2.ColCount);
            for (long i = 0; i < m1.RowCount; i++)
            {
                for (byte j = 0; j < m2.ColCount; j++)
                {
                    long sum = 0;
                    for (byte k = 0; k < m1.ColCount; k++)
                    {
                        sum += m1.GetElement(i, k) * m2.GetElement(k, j);
                    }

                    resultMatrix.SetElement(i, j, sum);
                }
            };

            watch.Stop();

            System.Console.WriteLine($"Time elapsed {watch.Elapsed.Seconds} secons, {watch.Elapsed.Milliseconds} ms, {watch.Elapsed.Ticks} ticks");
            return resultMatrix;
        }
    }
}
