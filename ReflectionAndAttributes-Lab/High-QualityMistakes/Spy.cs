using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string className, params string[] fieldsInvestigate)
        {
            Type classType = Type.GetType(className);
            FieldInfo[] classFields = classType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            var sb = new StringBuilder();
            Object classInstance = Activator.CreateInstance(classType, new object[] { });
            sb.AppendLine($"Class under investigation: {className}");

            foreach (FieldInfo field in classFields.Where(a => fieldsInvestigate.Contains(a.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }
            return sb.ToString().Trim();
        }
        public string AnalyzeAccessModifiers(string className)
        {
            Type type = Type.GetType(className);
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            StringBuilder sb = new StringBuilder();

            foreach (var field in fields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            MethodInfo[] publicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);

            foreach (var method in publicMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }

            MethodInfo[] nonpublicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var method in nonpublicMethods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} have to be public!");
            }

            return sb.ToString().Trim();
        }

    }
}
