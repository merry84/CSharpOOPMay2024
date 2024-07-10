using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        /*You have the information that this hacker is keeping some of his info in private fields. 
         * Create a new class named Spy and add inside a method called - StealFieldInfo, which receives:
        •	string – the name of the class to investigate
        •	an array of string - names of the fields to investigate
        */
        public string StealFieldInfo(string classToInvestagate, params string[] fieldsToInvestagate)
        {
            Type type = Type.GetType(classToInvestagate);

            FieldInfo[] classFields = type.GetFields
                (BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
            var sb = new StringBuilder();

            /*After finding the fields, you must print on the console:
            "Class under investigation: {nameOfTheClass}"
            On the next lines, print info about each field in the following format:
            "{filedName} = {fieldValue}"
            */
            Object classInstance = Activator.CreateInstance(type,new object[] { });

            sb.AppendLine($"Class under investigation: {classToInvestagate}");

            foreach (FieldInfo field in classFields.Where(x => fieldsToInvestagate.Contains(x.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }
            return sb.ToString().Trim();
        }
    }
}
