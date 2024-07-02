using System.Text;
using NJsonSchema.Generation;

namespace Kayord.Pos.Common.Extensions.Swagger;

internal class CustomSchemaNameGenerator : DefaultSchemaNameGenerator, ISchemaNameGenerator
{
    private readonly bool _shortSchemaNames;

    public CustomSchemaNameGenerator(bool shortSchemaNames)
    {
        _shortSchemaNames = shortSchemaNames;
    }

    public override string Generate(Type type)
    {
        bool isGenericType = type.IsGenericType;
        string text = isGenericType ? type.FullName?.Substring(0, type.FullName.IndexOf('`')) ?? type.Name : type.FullName ?? type.Name;
        if (_shortSchemaNames)
        {
            int num = text.LastIndexOf('.');
            num = ((num != -1) ? (num + 1) : 0);
            string text2 = text;
            int num2 = num;
            string text3 = text2.Substring(num2, text2.Length - num2);
            if (!isGenericType)
            {
                return text3;
            }

            return text3 + GenericArgString(type);
        }

        string text4 = text.Replace(".", string.Empty);
        string text5 = AppDomain.CurrentDomain.FriendlyName.Replace(".", string.Empty);
        string oldValue = text5 + "Features";
        text4 = text4.Replace(oldValue, string.Empty);
        text4 = text4.Replace(text5, string.Empty);
        text4 = text4.Replace("FastEndpoints", string.Empty);
        text4 = text4.Replace("Endpoint", string.Empty);
        if (!isGenericType)
        {
            return text4;
        }

        return text4 + GenericArgString(type);
        static string? GenericArgString(Type type)
        {
            if (type.IsGenericType)
            {
                StringBuilder stringBuilder = new StringBuilder();
                Type[] genericArguments = type.GetGenericArguments();
                for (int i = 0; i < genericArguments.Length; i++)
                {
                    Type type2 = genericArguments[i];
                    if (i == 0)
                    {
                        stringBuilder.Append("Of");
                    }

                    stringBuilder.Append(TypeNameWithoutGenericArgs(type2));
                    stringBuilder.Append(GenericArgString(type2));
                    if (i < genericArguments.Length - 1)
                    {
                        stringBuilder.Append("And");
                    }
                }

                return stringBuilder.ToString();
            }

            return type.Name;
        }

        static string TypeNameWithoutGenericArgs(Type type)
        {
            int num3 = type.Name.IndexOf('`');
            num3 = ((num3 != -1) ? num3 : 0);
            return type.Name.Substring(0, num3);
        }
    }
}