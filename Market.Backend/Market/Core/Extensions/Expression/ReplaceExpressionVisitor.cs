using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Extensions.Expression
{
    internal class ReplaceExpressionVisitor
        : ExpressionVisitor
    {
        private readonly System.Linq.Expressions.Expression _oldValue;
        private readonly System.Linq.Expressions.Expression _newValue;

        public ReplaceExpressionVisitor(System.Linq.Expressions.Expression oldValue, System.Linq.Expressions.Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override System.Linq.Expressions.Expression Visit(System.Linq.Expressions.Expression node) => node == _oldValue ? _newValue : base.Visit(node);
    }
}
