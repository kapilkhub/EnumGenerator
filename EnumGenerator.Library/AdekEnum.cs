using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Text;

namespace EnumGenerator.Library
{

    [Generator]
    public class AdekEnum : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterSourceOutput(context.CompilationProvider, (spc, compilation) =>
            {
                var enums = GetEnumsFromDatabase();
                foreach (var enumData in enums)
                {
                    var enumSource = GenerateEnumSource(enumData);
                    spc.AddSource($"{enumData.Name}.g.cs", enumSource);
                }
            });
        }

        private IEnumerable<EnumData> GetEnumsFromDatabase()
        {
            var enums = new List<EnumData>
            {
                new EnumData
                {
                    Name = "MyEnum",
                    Values = new List<string> { "Value1", "Value2", "Value3" }
                }
            };

            // Replace with your actual database connection string


            return enums;
        }

        private string GenerateEnumSource(EnumData enumData)
        {
            var sb = new StringBuilder();
            sb.AppendLine("namespace GeneratedEnums");
            sb.AppendLine("{");
            sb.AppendLine($"    public enum {enumData.Name}");
            sb.AppendLine("    {");

            foreach (var value in enumData.Values)
            {
                sb.AppendLine($"        {value},");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private class EnumData
        {
            public string Name { get; set; }
            public List<string> Values { get; set; }
        }
    }
}
