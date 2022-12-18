using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        // todo: feel free to add your code here

        protected override Expression VisitBinary(BinaryExpression b)
        {
            var value = int.Parse(b.Right.ToString());
            if (b.NodeType == ExpressionType.Add && value == 1)
            {
                Expression left = Visit(b.Left);
                
                return Expression.Increment(left);
            }
            else if(b.NodeType == ExpressionType.Subtract && value == 1)
            {
                var left = Visit(b.Left);

                return Expression.Decrement(left);
            }

            return base.VisitBinary(b);
        }
    }
}
