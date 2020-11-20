using System;
using System.Linq.Expressions;

namespace Core.Extensions.Expression
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(
                System.Linq.Expressions.Expression.AndAlso(left, right), parameter);
        }
    }
}
