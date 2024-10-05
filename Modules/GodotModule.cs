using Godot;
using EVIL.Ceres.ExecutionEngine;
using EVIL.Ceres.ExecutionEngine.Concurrency;
using EVIL.Ceres.ExecutionEngine.TypeSystem;
using EVIL.Ceres.Runtime;
using EVIL.Ceres.Runtime.Extensions;
using EVIL.CommonTypes.TypeSystem;

public class GodotModule : RuntimeModule
{
    public override string FullyQualifiedName => "godot";

    [RuntimeModuleFunction("quit")]
    [EvilDocFunction(
        "Quits the game.",
        ReturnType = DynamicValueType.Nil
    )]
    private static DynamicValue Quit(Fiber _, params DynamicValue[] args)
    {
        args.ExpectNone();

        (_.VirtualMachine as GodotVM).Tree.Root.PropagateNotification(1006);
        (_.VirtualMachine as GodotVM).Tree.Quit();

        return DynamicValue.Nil;
    }

    [RuntimeModuleFunction("globalize_path")]
    [EvilDocFunction(
        "Returns globalized path, from res:// or user:// into OS path.",
        Returns = "Globalized path",
        ReturnType = DynamicValueType.String
    )]
    [EvilDocArgument(
        "path",
        "Godot local path",
        DynamicValueType.String
    )]
    private static DynamicValue GlobalizePath(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectStringAt(0, out var path);
        return ProjectSettings.GlobalizePath(path);
    }
}
