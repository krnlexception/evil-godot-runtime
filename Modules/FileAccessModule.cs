using EVIL.Ceres.ExecutionEngine.Concurrency;
using EVIL.Ceres.ExecutionEngine.TypeSystem;
using EVIL.Ceres.Runtime.Extensions;
using EVIL.CommonTypes.TypeSystem;
using Godot;

namespace EVIL.Ceres.Runtime.Modules;

public class FileAccessModule : RuntimeModule
{
    public override string FullyQualifiedName => "fileaccess";

    [RuntimeModuleFunction("get_file_as_string")]
    [EvilDocFunction(
        "Returns the whole `path` file contents as a String. Text is interpreted as being UTF-8 encoded.",
        Returns = "File contents as string",
        ReturnType = DynamicValueType.String
    )]
    [EvilDocArgument(
        "path",
        "Path to file",
        DynamicValueType.String
    )]
    public static DynamicValue GetFileAsString(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1);
        return FileAccess.GetFileAsString(args[0].String);
    }
}