using System;
using System.ComponentModel;

namespace Лифт_PLCSim.Model
{
    internal class PLCSimTagConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
                return new PLCSimTag((string)value);

            return base.ConvertFrom(context, culture, value);
        }
    }
}