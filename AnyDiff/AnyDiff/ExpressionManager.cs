using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            var names = new List<string>();
            switch (expression)
            {
                case MemberExpression m:
                    {
                        var parent = m.Expression as MemberExpression;
                        if (parent != null)
                        {
                            var fullnames = GetPropertyNames(parent);
                            names.AddRange(fullnames.Reverse());
                            //names.Add(parent.Member.Name);
                        }
                        names.Add(m.Member.Name);
                    }
                    break;
                case UnaryExpression u when u.Operand is MemberExpression m:
                    {
                        var parent = m.Expression as MemberExpression;
                        if (parent != null)
                        {
                            var fullnames = GetPropertyNames(parent);
                            names.AddRange(fullnames.Reverse());
                            //names.Add(parent.Member.Name);
                        }
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
                        {
                            var fullnames = GetPropertyNames(parent);
                            names.AddRange(fullnames.Reverse());
                            //names.Add(parent.Member.Name);
                        }
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
                            {
                                var fullnames = GetPropertyNames(opExp);
                                names.AddRange(fullnames.Reverse());
                                //names.Add(opExp.Member.Name);
                            }
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

        private IEnumerable<string> GetPropertyNames(MemberExpression body)
        {
            while (body != null)
            {
                yield return body.Member.Name;
                var inner = body.Expression;
                switch (inner.NodeType)
                {
                    case ExpressionType.MemberAccess:
                        body = inner as MemberExpression;
                        break;
                    default:
                        body = null;
                        break;

                }
            }
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
