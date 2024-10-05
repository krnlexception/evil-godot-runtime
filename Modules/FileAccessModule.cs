using EVIL.Ceres.ExecutionEngine.Collections;
using EVIL.Ceres.ExecutionEngine.Concurrency;
using EVIL.Ceres.ExecutionEngine.TypeSystem;
using EVIL.Ceres.Runtime.Extensions;
using EVIL.Ceres.TranslationEngine.Diagnostics;
using EVIL.CommonTypes.TypeSystem;
using Godot;

namespace EVIL.Ceres.Runtime.Modules;

public class FileAccessModule : RuntimeModule
{
    public override string FullyQualifiedName => "fileaccess";

    private static Table _fileModes = new Table()
    {
        { "read", (int)FileAccess.ModeFlags.Read },
        { "write", (int)FileAccess.ModeFlags.Write },
        { "read_write", (int)FileAccess.ModeFlags.ReadWrite },
        { "write_read", (int)FileAccess.ModeFlags.WriteRead }
    }.Freeze();
    [RuntimeModuleGetter("modeflags")]
    [EvilDocProperty(
        EvilDocPropertyMode.Get,
        "Table containing file access modes  \n" +
        "```\n" +
        "{\n" +
        "  Read,\n" +
        "  Write,\n" +
        "  ReadWrite,\n" +
        "  WriteRead\n" +
        "}\n" +
        "```\n"
    )]
    private static DynamicValue ModeFlags(DynamicValue _)
        => _fileModes;

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
        args.ExpectExactly(1)
            .ExpectStringAt(0, out var path);
        return FileAccess.GetFileAsString(path);
    }

    [RuntimeModuleFunction("get_file_as_bytes")]
    [EvilDocFunction(
        "Returns the whole path file contents as a byte array without any decoding.",
        Returns = "File contents as byte array",
        ReturnType = DynamicValueType.Table
    )]
    [EvilDocArgument(
        "path",
        "Path to file",
        DynamicValueType.String
    )]
    public static DynamicValue GetFileAsBytes(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectStringAt(0, out var path);
        byte[] file = FileAccess.GetFileAsBytes(path);
        var array = new ExecutionEngine.Collections.Array(file.Length);

        for (int i = 0; i < file.Length; i++)
        {
            array[i] = file[i];
        }

        return array;
    }

    [RuntimeModuleFunction("open")]
    [EvilDocFunction(
        "Opens a file.",
        Returns = "File contents as byte array",
        ReturnType = DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "path",
        "Path to file",
        DynamicValueType.String
    )]
    [EvilDocArgument(
        "access_mode",
        "File access mode",
        DynamicValueType.Number
    )]
    public static DynamicValue Open(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectStringAt(0, out var path)
            .ExpectNumberAt(1, out var mode);
        return new DynamicValue(FileAccess.Open(path, (FileAccess.ModeFlags)(int)mode));
    }

    [RuntimeModuleFunction("close")]
    [EvilDocFunction(
        "Closes the currently opened file and prevents subsequent read/write operations.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue Close(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);
        (file as FileAccess).Close();
        return DynamicValue.Nil;;
    }

    [RuntimeModuleFunction("get_as_text")]
    [EvilDocFunction(
        "Returns the whole file as a String. Text is interpreted as being UTF-8 encoded.",
        Returns = "File contents as string",
        ReturnType = DynamicValueType.String
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue GetAsText(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);
        return (file as FileAccess).GetAsText();
    }

    [RuntimeModuleFunction("store_string")]
    [EvilDocFunction(
        "Appends string to the file without a line return, encoding the text as UTF-8.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "content",
        "String to store in file",
        DynamicValueType.String
    )]
    public static DynamicValue StoreString(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var file)
            .ExpectStringAt(1, out var content);
        (file as FileAccess).StoreString(content);
        return DynamicValue.Nil;
    }
}