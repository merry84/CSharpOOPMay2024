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
        public string RevealPrivateMethods(string className)
        {
            Type type = Type.GetType(className);
            MethodInfo[] classMetods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            var sb = new StringBuilder();
            /*All Private Methods of Class: {className}
                Base Class: {baseClassName}
                */
            sb.AppendLine($"All Private Methods of Class: {className}");
            sb.AppendLine($"Base Class: {type.BaseType.Name}");

            foreach (MethodInfo method in classMetods)
            {
                sb.AppendLine(method.Name);
            }
            return sb.ToString().Trim();
        }


        /*Print to console each getter on a new line in the format:
         "{name} will return {Return Type}"
         Then print all of the setters in the format:
         "{name} will set field of {Parameter Type}"
         */
        public string CollectGetterAndSetter(string className)
        {
            Type type = Type.GetType(className);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var sb = new StringBuilder();
            foreach (MethodInfo method in methods.Where(n => n.Name.StartsWith("get")))
            {
                sb = sb.AppendLine($"{method.Name} will return {method.ReturnType}");
            }
            foreach (MethodInfo method in methods.Where(n => n.Name.StartsWith("set")))
            {
                sb = sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
            }


            return sb.ToString().Trim();
        }

    }
}
