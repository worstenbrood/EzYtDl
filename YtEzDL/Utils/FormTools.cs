using MetroFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace YtEzDL.Utils
{
    internal static class FormTools
    {
        public static void AddCheckedBinding<T>(this CheckBox c, T data, Expression<Func<T, bool>> property, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
        {
            c.DataBindings.Add(nameof(CheckBox.Checked), data, CommonTools.GetMemberName(property), false, updateMode);
        }

        public static void AddEnumBinding<TEnum, T>(this ComboBox c, T data, Expression<Func<T, TEnum>> property, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
            where TEnum : struct, Enum
        {
            c.DataSource = Enum.GetValues(typeof(TEnum));
            c.DataBindings.Add(nameof(ComboBox.SelectedItem), data, CommonTools.GetMemberName(property), false, updateMode);
        }

        public static void AddTextBinding<T>(this Control c, T data, Expression<Func<T, string>> property, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
        {
            c.DataBindings.Add(nameof(CheckBox.Text), data, CommonTools.GetMemberName(property), false, updateMode);
        }

        public static void AddRangeBinding<T>(this ComboBox c, T data, Expression<Func<T, int>> property, int start, int count, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
        {
            c.DataSource = Enumerable.Range(start, count).ToArray();
            c.DataBindings.Add(nameof(ComboBox.SelectedItem), data, CommonTools.GetMemberName(property), false, updateMode);
        }

        public static void AddRangeBinding<T>(this ComboBox c, T data, Expression<Func<T, float>> property, float start, int count, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
        {
            var list = new List<float>();
            for (var f = start; f < start + count; f++)
            {
                list.Add(f);
            }

            c.DataSource = list;
            c.DataBindings.Add(nameof(ComboBox.SelectedItem), data, CommonTools.GetMemberName(property), false, updateMode);
        }

        public static int GetTextHeight(this Control c)
        {
            return TextRenderer.MeasureText(c.Text, c.Font, c.ClientSize,
                TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl).Height;
        }

        public static Dictionary<MetroColorStyle, Color> ColorMapping =
            new Dictionary<MetroColorStyle, Color>()
            {
                {MetroColorStyle.Black, MetroColors.Black},
                {MetroColorStyle.White, MetroColors.White},
                {MetroColorStyle.Silver, MetroColors.Silver},
                {MetroColorStyle.Blue, MetroColors.Blue},
                {MetroColorStyle.Green, MetroColors.Green},
                {MetroColorStyle.Lime, MetroColors.Lime},
                {MetroColorStyle.Teal, MetroColors.Teal},
                {MetroColorStyle.Orange, MetroColors.Orange},
                {MetroColorStyle.Brown, MetroColors.Brown},
                {MetroColorStyle.Pink, MetroColors.Pink},
                {MetroColorStyle.Magenta, MetroColors.Magenta},
                {MetroColorStyle.Purple, MetroColors.Purple},
                {MetroColorStyle.Red, MetroColors.Red},
                {MetroColorStyle.Yellow, MetroColors.Orange}
            };

        public static bool ShowActiveForm<T>()
            where T: MetroForm
        {
            var active = Application.OpenForms.OfType<T>()
                .FirstOrDefault();
            if (active != null)
            {
                active.FocusMe();
                return true;
            }
            
            return false;
        }
    }
}
