using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace YtEzDL.Utils
{
    internal static class Tools
    {
        private static string GetMemberName<T>(Expression<T> p)
        {
            switch (p.Body)
            {
                case MemberExpression expression:
                    return ((PropertyInfo)expression.Member).Name;

                case UnaryExpression expression:
                    return ((PropertyInfo)((MemberExpression)expression.Operand).Member).Name;

                default:
                    throw new Exception("Unhandled expression cast.");
            }
        }
        
        public static void AddCheckedBinding<T>(this CheckBox c, T data, Expression<Func<T, bool>> property)
        {
            c.DataBindings.Add(nameof(CheckBox.Checked), data, GetMemberName(property));
        }

        public static void AddEnumBinding<TEnum,T>(this ComboBox c, T data, Expression<Func<T, TEnum>> property)
            where TEnum : struct, Enum
        {
            c.DataSource = Enum.GetValues(typeof(TEnum));
            c.DataBindings.Add(nameof(ComboBox.SelectedItem), data, GetMemberName(property));
        }

        public static void AddTextBinding<T>(this Control c, T data, Expression<Func<T, string>> property)
        {
            c.DataBindings.Add(nameof(CheckBox.Text), data, GetMemberName(property));
        }
    }
}
