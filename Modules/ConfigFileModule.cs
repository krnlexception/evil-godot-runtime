using EVIL.Ceres.ExecutionEngine.Collections;
using EVIL.Ceres.ExecutionEngine.Concurrency;
using EVIL.Ceres.ExecutionEngine.TypeSystem;
using EVIL.Ceres.Runtime.Extensions;
using EVIL.CommonTypes.TypeSystem;
using Godot;

namespace EVIL.Ceres.Runtime.Modules;

public class ConfigFileModule : RuntimeModule
{
    public override string FullyQualifiedName => "configfile";

    [RuntimeModuleFunction("new")]
    [EvilDocFunction(
        "Creates a new instance of ConfigFile",
        Returns = "Instance of ConfigFile",
        ReturnType = DynamicValueType.NativeObject
    )]
    private static DynamicValue New(Fiber _, params DynamicValue[] args)
    {
        args.ExpectNone();

        return new DynamicValue(new ConfigFile());
    }

    [RuntimeModuleFunction("clear")]
    [EvilDocFunction(
        "Removes the entire contents of the config.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "config",
        "ConfigFile type containing opened config",
        DynamicValueType.NativeObject
    )]
    private static DynamicValue Clear(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var config);

        (config as ConfigFile).Clear();
        return new DynamicValue();
    }

    [RuntimeModuleFunction("encode_to_text")]
    [EvilDocFunction(
        "Obtain the text version of this config file (the same text that would be written to a file).",
        Returns = "Config file as string",
        ReturnType = DynamicValueType.String
    )]
    [EvilDocArgument(
        "config",
        "ConfigFile type containing opened config",
        DynamicValueType.NativeObject
    )]
    private static DynamicValue EncodeToText(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var config);

        return (config as ConfigFile).EncodeToText();
    }

    [RuntimeModuleFunction("erase_section")]
    [EvilDocFunction(
        "Deletes the specified section along with all the key-value pairs inside. Raises an error if the section does not exist.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "config",
        "ConfigFile type containing opened config",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "section",
        "Path to a section",
        DynamicValueType.String
    )]
    private static DynamicValue EraseSection(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var config)
            .ExpectStringAt(1, out var path);

        (config as ConfigFile).EraseSection(path);
        return new DynamicValue();
    }

    [RuntimeModuleFunction("erase_section_key")]
    [EvilDocFunction(
        "Deletes the specified key in a section. Raises an error if either the section or the key do not exist.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "config",
        "ConfigFile type containing opened config",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "section",
        "Path to a section",
        DynamicValueType.String
    )]
    [EvilDocArgument(
        "key",
        "Path to a key",
        DynamicValueType.String
    )]
    private static DynamicValue EraseSectionKey(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(3)
            .ExpectNativeObjectAt(0, out var config)
            .ExpectStringAt(1, out var path)
            .ExpectStringAt(2, out var key);

        (config as ConfigFile).EraseSectionKey(path, key);
        return new DynamicValue();
    }

    [RuntimeModuleFunction("get_section_keys")]
    [EvilDocFunction(
        "Returns an array of all defined key identifiers in the specified section. Raises an error and returns an empty array if the section does not exist.",
        ReturnType = DynamicValueType.Table
    )]
    [EvilDocArgument(
        "config",
        "ConfigFile type containing opened config",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "section",
        "Path to a section",
        DynamicValueType.String
    )]
    private static DynamicValue GetSectionKeys(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var config)
            .ExpectStringAt(1, out var path);

        string[] keys = (config as ConfigFile).GetSectionKeys(path);
        var array = new Array(keys.Length);

        for (int i = 0; i < keys.Length; i++)
        {
            array[i] = keys[i];
        }

        return array;
    }

    [RuntimeModuleFunction("get_sections")]
    [EvilDocFunction(
        "Returns an array of all defined section identifiers.",
        ReturnType = DynamicValueType.Table
    )]
    [EvilDocArgument(
        "config",
        "ConfigFile type containing opened config",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "section",
        "Path to a section",
        DynamicValueType.String
    )]
    private static DynamicValue GetSections(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var config);

        string[] sections = (config as ConfigFile).GetSections();
        var array = new Array(sections.Length);

        for (int i = 0; i < sections.Length; i++)
        {
            array[i] = sections[i];
        }

        return array;
    }

    [RuntimeModuleFunction("get_value")]
    [EvilDocFunction(
        "Returns the current value for the specified section and key. If either the section or the key do not exist, the method returns the fallback default value. If default is not specified or set to null, an error is also raised.",
        ReturnType = DynamicValueType.String
    )]
    [EvilDocArgument(
        "config",
        "ConfigFile type containing opened config",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "section",
        "Path to a section",
        DynamicValueType.String
    )]
    [EvilDocArgument(
        "key",
        "Path to a key",
        DynamicValueType.String
    )]
    [EvilDocArgument(
        "default",
        "Default value (non-required)",
        DynamicValueType.String
    )]
    private static DynamicValue GetValue(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(4)
            .ExpectNativeObjectAt(0, out var config)
            .ExpectStringAt(1, out var path)
            .ExpectStringAt(2, out var key)
            .OptionalStringAt(3, "", out var defaultValue);

        return (config as ConfigFile).GetValue(path, key, defaultValue).ToString();
    }
}