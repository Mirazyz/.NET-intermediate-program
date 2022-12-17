using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }
            else if(node.Method.DeclaringType == typeof(String)
                && node.Method.Name == "StartsWith")
            {
                var member = node.Object as MemberExpression;
                var constant = node.Arguments[0] as ConstantExpression;

                Visit(member);
                _resultStringBuilder.Append($"({constant.Value}*)");

                return node;
            }
            else if(node.Method.DeclaringType == typeof(String)
                && node.Method.Name == "EndsWith")
            {
                var member = node.Object as MemberExpression;
                var constant = node.Arguments[0] as ConstantExpression;

                Visit(member);
                _resultStringBuilder.Append($"(*{constant.Value})");

                return node;
            }
            else if(node.Method.DeclaringType == (typeof(String))
                && node.Method.Name == "Contains")
            {
                var member = node.Object as MemberExpression;
                var constant = node.Arguments[0] as ConstantExpression;

                Visit(member);
                _resultStringBuilder.Append($"(*{constant.Value}*)");

                return node;
            }
            else if (node.Method.DeclaringType == (typeof(String))
                && node.Method.Name == "Equals")
            {
                var member = node.Object as MemberExpression;
                var constant = node.Arguments[0] as ConstantExpression;

                Visit(member);
                _resultStringBuilder.Append($"({constant.Value})");

                return node;
            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    bool memberOnRight = node.Right.NodeType == ExpressionType.MemberAccess;

                    Visit(memberOnRight ? node.Right : node.Left);
                    _resultStringBuilder.Append("(");
                    Visit(memberOnRight ? node.Left : node.Right);
                    _resultStringBuilder.Append(")");
                    break;

                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append(node.Value);

            return node;
        }

        #endregion
    }
}
