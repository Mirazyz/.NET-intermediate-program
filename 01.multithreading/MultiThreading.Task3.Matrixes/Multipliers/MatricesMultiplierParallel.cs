using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            Stopwatch watch = new Stopwatch();
            var resultMatrix = new Matrix(m1.RowCount, m2.ColCount);

            watch.Start();

            Parallel.For(0, m1.RowCount, (i) =>
            {
                Parallel.For(0, m2.ColCount, (j) =>
                {
                    long sum = 0;

                    Parallel.For(0, m1.ColCount, (k) =>
                    {
                        var result = m1.GetElement(i, k) * m2.GetElement(k, j);
                        Interlocked.Add(ref sum, result);
                    });

                    resultMatrix.SetElement(i, j, sum);
                });
            });

            watch.Stop();

            System.Console.WriteLine($"Time elapsed {watch.Elapsed.Seconds} secons, {watch.Elapsed.Milliseconds} ms, {watch.Elapsed.Ticks} ticks");
            return resultMatrix;
        }
    }
}
