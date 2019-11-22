using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace AnyDiff
{
    /// <summary>
    /// Expression manager
    /// </summary>
    public class ExpressionManager
    {
        /// <summary>
        /// Get a property path based on an expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>A dot-notation path to the property</returns>
        public string GetPropertyPath(Expression expression)
        {
            return $".{GetPropertyPathInternal(expression)}";
        }

        /// <summary>
        /// Recursive function that builds a path from an expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>A dot-notation path to the property</returns>
        private string GetPropertyPathInternal(Expression expression)
        {
            // todo: there's a lot of room for cleanup here, I was still trying to understand expression parsing.
            var names = new List<string>();
            switch (expression)
            {
                case MemberExpression m:
                    {
                        var parent = m.Expression as MemberExpression;
                        if (parent != null)
                            names.Add(parent.Member.Name);
                        names.Add(m.Member.Name);
                    }
                    break;
                case UnaryExpression u when u.Operand is MemberExpression m:
                    {
                        var parent = m.Expression as MemberExpression;
                        if (parent != null)
                            names.Add(parent.Member.Name);
                        var methodExp = m.Expression as MethodCallExpression;
                        if (methodExp != null)
                        {
                            names.AddRange(ParseArguments(methodExp.Arguments));
                        }
                        names.Add(m.Member.Name);
                    }
                    break;
                case LambdaExpression l when l.Body is MemberExpression m:
                    {
                        var parent = m.Expression as MemberExpression;
                        if (parent != null)
                            names.Add(parent.Member.Name);
                        var methodExp = m.Expression as MethodCallExpression;
                        if (methodExp != null)
                        {
                            names.AddRange(ParseArguments(methodExp.Arguments));
                        }
                        names.Add(m.Member.Name);
                    }
                    break;
                case LambdaExpression l when l.Body is UnaryExpression u:
                    {
                        var operand = (MemberExpression)u.Operand;
                        var exp = operand.Expression as MemberExpression;
                        if (exp != null)
                        {
                            var methodExp = exp.Expression as MethodCallExpression;
                            if (methodExp != null)
                            {
                                names.AddRange(ParseArguments(methodExp.Arguments));
                            }
                            var opExp = operand.Expression as MemberExpression;
                            if (opExp != null)
                                names.Add(opExp.Member.Name);
                        }
                        names.Add(operand.Member.Name);
                    }
                    break;
                case LambdaExpression l when l.Body is MethodCallExpression m:
                    {
                        names.AddRange(ParseArguments(m.Arguments));
                    }
                    break;
                case MethodCallExpression mc when mc.Method is MethodInfo m:
                    {
                        names.AddRange(ParseArguments(mc.Arguments));
                    }
                    break;
                default:
                    throw new NotImplementedException(expression.GetType().ToString());
            }
            return string.Join(".", names);
        }

        private List<string> ParseArguments(ReadOnlyCollection<Expression> expressions)
        {
            var names = new List<string>();
            foreach (var expression in expressions)
            {
                names.Add(GetPropertyPathInternal(expression));
            }
            return names;
        }
    }
}
