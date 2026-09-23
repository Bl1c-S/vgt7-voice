using System.ComponentModel;
using System.Reflection;
using Application.Models.AiModel;
using Application.Models.AiProvider;

namespace Application.Models.Extensions;

public static class EnumExtension
{
    extension(Enum value)
    {
        public string GetDescription()
        {
            var attribute = GetAttribute<DescriptionAttribute>(value);
            return attribute.Description;
        }

        public AiProviderTypes GetAiProvider()
        {
            var attribute = GetAttribute<AiModelAttribute>(value);
            return attribute.ProviderTypes;
        }
    }

    private static T GetAttribute<T>(Enum value) where T : Attribute
    {
        var field = FieldValidation(value);
        return AttributeValidation<T>(field);
    }

    private static T AttributeValidation<T>(FieldInfo field) where T : Attribute
    {
        var attribute = field.GetCustomAttribute<T>();
        return attribute ?? throw new ArgumentNullException($"Attribute cant implement from field: {field}");
    }
    
    private static FieldInfo FieldValidation(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        return field ?? throw new ArgumentNullException($"Field cant implement from value: {value}");
    }
}