using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace YtEzDL.Utils
{
    internal static class FormTools
    {
        public static string GetMemberName<TDelegate>(Expression<TDelegate> p)
            where TDelegate : class, Delegate
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
        
        public static void AddCheckedBinding<T>(this CheckBox c, T data, Expression<Func<T, bool>> property, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
        {
            c.DataBindings.Add(nameof(CheckBox.Checked), data, GetMemberName(property), false, updateMode);
        }

        public static void AddEnumBinding<TEnum,T>(this ComboBox c, T data, Expression<Func<T, TEnum>> property, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
            where TEnum : struct, Enum
        {
            c.DataSource = Enum.GetValues(typeof(TEnum));
            c.DataBindings.Add(nameof(ComboBox.SelectedItem), data, GetMemberName(property), false, updateMode);
        }

        public static void AddTextBinding<T>(this Control c, T data, Expression<Func<T, string>> property, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
        {
            c.DataBindings.Add(nameof(CheckBox.Text), data, GetMemberName(property), false, updateMode);
        }

        public static void AddRangeBinding<T>(this ComboBox c, T data, Expression<Func<T, int>> property, int start, int count, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
        {
            c.DataSource = Enumerable.Range(1, Environment.ProcessorCount).ToArray();
            c.DataBindings.Add(nameof(ComboBox.SelectedItem), data, GetMemberName(property), false, updateMode);
        }
    }
}
