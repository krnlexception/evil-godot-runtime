namespace EVIL.Ceres.Runtime.Modules;

using Godot;
using System.Linq;
using EVIL.Ceres.ExecutionEngine.Concurrency;
using EVIL.Ceres.ExecutionEngine.TypeSystem;
using EVIL.Ceres.Runtime.Extensions;
using EVIL.CommonTypes.TypeSystem;

public class IoModule : RuntimeModule
{
    public override string FullyQualifiedName => "io";

    [RuntimeModuleFunction("print")]
    [EvilDocFunction(
        "Writes string representations of the provided values, separated by the tabulator character, to the standard output stream.",
        Returns = "Length of the printed string, including the tabulator characters, if any.",
        ReturnType = DynamicValueType.Number
    )]
    [EvilDocArgument(
        "...",
        "__At least 1__ value to be printed.",
        CanBeNil = true
    )]
    private static DynamicValue Print(Fiber _, params DynamicValue[] args)
    {
        args.ExpectAtLeast(1);

        var str = string.Join(
            "\t",
            args.Select(x => x.ConvertToString().String)
        );

        GD.Print(str);
        return str.Length;
    }
}
