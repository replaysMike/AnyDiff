using System;
using System.Collections.Generic;
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
                    names.Add(m.Member.Name);
                    break;
                case UnaryExpression u when u.Operand is MemberExpression m:
                    {
                        var parent = m.Expression as MemberExpression;
                        if (parent != null)
                            names.Add(parent.Member.Name);
                        names.Add(m.Member.Name);
                    }
                    break;
                case LambdaExpression l when l.Body is MemberExpression m:
                    {
                        var parent = m.Expression as MemberExpression;
                        if (parent != null)
                            names.Add(parent.Member.Name);
                        names.Add(m.Member.Name);
                    }
                    break;
                case LambdaExpression l when l.Body is UnaryExpression u:
                    names.Add(((MemberExpression)u.Operand).Member.Name);
                    break;
                case LambdaExpression l when l.Body is MethodCallExpression m:
                    {
                        var args = m.Arguments;
                        var childNames = new List<string>();
                        foreach (var arg in args)
                            childNames.Add(GetPropertyPathInternal(arg));
                        var parent = ((MemberExpression)args.First()).Expression as MemberExpression;
                        if (parent != null)
                            names.Add(parent.Member.Name);
                        names.AddRange(childNames);
                    }
                    break;
                case MethodCallExpression mc when mc.Method is MethodInfo m:
                    {
                        var args = mc.Arguments;
                        var childNames = new List<string>();
                        foreach (var arg in args)
                            childNames.Add(GetPropertyPathInternal(arg));
                        var parent = ((MemberExpression)args.First()).Expression as MemberExpression;
                        if (parent != null)
                            names.Add(parent.Member.Name);
                        names.AddRange(childNames);
                    }
                    break;
                default:
                    throw new NotImplementedException(expression.GetType().ToString());
            }
            return string.Join(".", names);
        }
    }
}
