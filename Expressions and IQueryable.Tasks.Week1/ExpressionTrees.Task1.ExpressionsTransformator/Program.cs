/*
 * Create a class based on ExpressionVisitor, which makes expression tree transformation:
 * 1. converts expressions like <variable> + 1 to increment operations, <variable> - 1 - into decrement operations.
 * 2. changes parameter values in a lambda expression to constants, taking the following as transformation parameters:
 *    - source expression;
 *    - dictionary: <parameter name: value for replacement>
 * The results could be printed in console or checked via Debugger using any Visualizer.
 */
using System;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expression Visitor for increment/decrement.");
            Console.WriteLine();

            // todo: feel free to add your code here
            Expression<Func<int, int>> increment = number => number + 1;
            Console.WriteLine(increment.ToString());

            Expression<Func<int, int>> decrement = number => number - 1;
            Console.WriteLine(decrement.ToString());

            IncDecExpressionVisitor incDecVisitor = new IncDecExpressionVisitor();
            Expression modifiedExpression = incDecVisitor.Visit(increment);
            Console.WriteLine(modifiedExpression.ToString());

            Expression modifiedDecrementExpression = incDecVisitor.Visit(decrement);
            Console.WriteLine(modifiedDecrementExpression.ToString());

            Console.ReadLine();
        }
    }
}
