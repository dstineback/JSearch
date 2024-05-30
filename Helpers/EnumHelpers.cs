using System.ComponentModel;
using System.Reflection;

namespace JSearch.Helpers
{
    public class EnumHelpers
    {
        public static string GetDescription(Enum value)
        {
            Type enumType = value.GetType();
            string name = Enum.GetName(enumType, value);
            if (name != null)
            {
                FieldInfo field = enumType.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return value.ToString();
        }
    }
}
 
