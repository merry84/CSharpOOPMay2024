using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ValidationAttributes.Attributes;

namespace ValidationAttributes
{
    public class Validator
    {
        /*Create a static class Validator.
         * It should contain a method - public static bool IsValid(object obj), 
         * which must validate the properties of a given object.*/
        public static bool IsValid(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo[] propertyWithCustomAttr = properties
                .Where(p => Attribute.IsDefined(p, typeof(MyValidationAttribute), inherit: true))
                .ToArray();

            foreach (PropertyInfo property in propertyWithCustomAttr)
            {
                var validationAtributes = property
                    .GetCustomAttributes(typeof(MyValidationAttribute), inherit: true)
                    .Cast<MyValidationAttribute>();

                foreach (var attribute in validationAtributes)
                {
                    object value = property.GetValue(obj);

                    if (attribute.IsValid(value) == false)
                        return false;
                }
            }
            return true;

        }
    }
}
