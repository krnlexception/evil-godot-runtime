using EVIL.Ceres.ExecutionEngine.Collections;
using EVIL.Ceres.ExecutionEngine.Concurrency;
using EVIL.Ceres.ExecutionEngine.TypeSystem;
using EVIL.Ceres.Runtime.Extensions;
using EVIL.CommonTypes.TypeSystem;
using Godot;
using Error = EVIL.Ceres.ExecutionEngine.Diagnostics.Error;

namespace EVIL.Ceres.Runtime.Modules;

public class FileAccessModule : RuntimeModule
{
    public override string FullyQualifiedName => "fileaccess";

    private static readonly Table _fileModes = new Table()
    {
        { "read", (int)FileAccess.ModeFlags.Read },
        { "write", (int)FileAccess.ModeFlags.Write },
        { "read_write", (int)FileAccess.ModeFlags.ReadWrite },
        { "write_read", (int)FileAccess.ModeFlags.WriteRead }
    }.Freeze();
    [RuntimeModuleGetter("mode_flags")]
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

    private static readonly Table _unixPermissionFlags = new Table()
    {
        { "unix_read_owner", 256 },
        { "unix_write_owner", 128 },
        { "unix_execute_owner", 64 },
        { "unix_read_group", 32 },
        { "unix_write_group", 16 },
        { "unix_execute_group", 8 },
        { "unix_read_other", 4 },
        { "unix_write_other", 2 },
        { "unix_execute_other", 1 },
        { "unix_set_user_id", 2048 },
        { "unix_set_group_id", 1024 },
        { "unix_unrestricted_delete", 512 }
    }.Freeze();
    [RuntimeModuleGetter("unix_permission_flags")]
    [EvilDocProperty(
        EvilDocPropertyMode.Get,
        "Table containing Unix permission modes  \n" +
        "```\n" +
        "{\n" +
        "  Read owner,\n" +
        "  Write owner,\n" +
        "  Execute owner,\n" +
        "  Read group,\n" +
        "  Write group,\n" +
        "  Execute group,\n" +
        "  Read other,\n" +
        "  Write other,\n" +
        "  Execute other,\n" +
        "  Set User ID,\n" +
        "  Set Group ID,\n" +
        "  Unrestricted delete,\n" +
        "}\n" +
        "```\n"
    )]
    private static DynamicValue UnixPermissionFlags(DynamicValue _)
        => _unixPermissionFlags;

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
        return DynamicValue.Nil;
    }

    [RuntimeModuleFunction("eof_reached")]
    [EvilDocFunction(
        "Returns true if the file cursor has already read past the end of the file.",
        Returns = "If file cursor is beyond EOF",
        ReturnType = DynamicValueType.Boolean
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue EofReached(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);
        return (file as FileAccess).EofReached();
    }

    [RuntimeModuleFunction("file_exists")]
    [EvilDocFunction(
        "Returns true if the file exists in the given path.",
        Returns = "If file exists",
        ReturnType = DynamicValueType.Boolean
    )]
    [EvilDocArgument(
        "file",
        "Path to file",
        DynamicValueType.String
    )]
    public static DynamicValue FileExists(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectStringAt(0, out var file);
        return FileAccess.FileExists(file);
    }

    [RuntimeModuleFunction("flush")]
    [EvilDocFunction(
        "Writes the file's buffer to disk.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue Flush(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);
        (file as FileAccess).Flush();
        return DynamicValue.Nil;
    }

    [RuntimeModuleFunction("get_8")]
    [EvilDocFunction(
        "Returns the next 8 bits from the file as an integer.",
        Returns = "8-bit integer",
        ReturnType = DynamicValueType.Number
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue Get8(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);
        return (file as FileAccess).Get8();
    }

    [RuntimeModuleFunction("get_16")]
    [EvilDocFunction(
        "Returns the next 16 bits from the file as an integer.",
        Returns = "16-bit integer",
        ReturnType = DynamicValueType.Number
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue Get16(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);
        return (file as FileAccess).Get16();
    }

    [RuntimeModuleFunction("get_32")]
    [EvilDocFunction(
        "Returns the next 32 bits from the file as an integer.",
        Returns = "32-bit integer",
        ReturnType = DynamicValueType.Number
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue Get32(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);
        return (file as FileAccess).Get32();
    }

    [RuntimeModuleFunction("get_64")]
    [EvilDocFunction(
        "Returns the next 8 bits from the file as an integer.",
        Returns = "64-bit integer",
        ReturnType = DynamicValueType.Number
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue Get64(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);
        return (file as FileAccess).Get64();
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

    [RuntimeModuleFunction("get_as_text")]
    [EvilDocFunction(
        "Returns next length bytes of the file as a Table",
        Returns = "Next x bytes as a table",
        ReturnType = DynamicValueType.Table
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "length",
        "Number of bytes to read",
        DynamicValueType.Number
    )]
    public static DynamicValue GetBuffer(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var file)
            .ExpectNumberAt(1, out var length);

        byte[] buffer = (file as FileAccess).GetBuffer((int)length);
        var array = new Array(buffer.Length);

        for (int i = 0; i < buffer.Length; i++)
        {
            array[i] = buffer[i];
        }

        return array;
    }

    [RuntimeModuleFunction("get_as_text")]
    [EvilDocFunction(
        "Returns the next value of the file in CSV (Comma-Separated Values) format. You can pass a different delimiter delim to use other than the default \",\" (comma). This delimiter must be one-character long, and cannot be a double quotation mark.",
        Returns = "Next line from CSV file",
        ReturnType = DynamicValueType.Table
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "delim",
        "Delimiter (default: \",\")",
        DynamicValueType.String
    )]

    public static DynamicValue GetCsvLine(Fiber _, params DynamicValue[] args)
    {
        args.ExpectAtLeast(1)
            .ExpectNativeObjectAt(0, out var file)
            .OptionalStringAt(1, ",", out var delim);

        string[] csv = (file as FileAccess).GetCsvLine(delim);
        var array = new Array(csv.Length);

        for (int i = 0; i < csv.Length; i++)
        {
            array[i] = csv[i];
        }

        return array;
    }

    [RuntimeModuleFunction("get_error")]
    [EvilDocFunction(
        "Returns the last error that happened when trying to perform operations.",
        Returns = "Last error code",
        ReturnType = DynamicValueType.Error
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue GetError(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);
        return new DynamicValue(new Error((file as FileAccess).GetError().ToString()));
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
        args.ExpectExactly(1)
            .ExpectStringAt(0, out var path);
        byte[] file = FileAccess.GetFileAsBytes(path);
        var array = new Array(file.Length);

        for (int i = 0; i < file.Length; i++)
        {
            array[i] = file[i];
        }

        return array;
    }

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

    [RuntimeModuleFunction("get_hidden_attribute")]
    [EvilDocFunction(
        "Returns true, if file hidden attribute is set.\n\nNote: This method is implemented on iOS, BSD, macOS, and Windows.",
        Returns = "Status of hidden attribute",
        ReturnType = DynamicValueType.Boolean
    )]
    [EvilDocArgument(
        "path",
        "Path to file",
        DynamicValueType.String
    )]
    public static DynamicValue GetHiddenAttribute(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectStringAt(0, out var file);

        return FileAccess.GetHiddenAttribute(file);
    }

    [RuntimeModuleFunction("get_length")]
    [EvilDocFunction(
        "Returns the size of the file in bytes.",
        Returns = "Length of file in bytes",
        ReturnType = DynamicValueType.Number
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue GetLength(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);

        return (file as FileAccess).GetLength();
    }

    [RuntimeModuleFunction("get_line")]
    [EvilDocFunction(
        "Returns the next line of the file as a String. The returned string doesn't include newline (\\n) or carriage return (\\r) characters, but does include any other leading or trailing whitespace.\n\nText is interpreted as being UTF-8 encoded.",
        Returns = "Next line of opened file",
        ReturnType = DynamicValueType.String
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue GetLine(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);

        return (file as FileAccess).GetLine();
    }

    [RuntimeModuleFunction("get_md5")]
    [EvilDocFunction(
        "Returns an MD5 String representing the file at the given path or an empty String on failure.",
        Returns = "MD5 hash of file in path",
        ReturnType = DynamicValueType.String
    )]
    [EvilDocArgument(
        "path",
        "Path to file",
        DynamicValueType.String
    )]
    public static DynamicValue GetMd5(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectStringAt(0, out var file);

        return FileAccess.GetMd5(file);
    }

    [RuntimeModuleFunction("get_modified_time")]
    [EvilDocFunction(
        "Returns the last time the file was modified in Unix timestamp format, or 0 on error. This Unix timestamp can be converted to another format using the Time singleton.",
        Returns = "File last modified time in Unix timestamp format",
        ReturnType = DynamicValueType.String
    )]
    [EvilDocArgument(
        "path",
        "Path to file",
        DynamicValueType.String
    )]
    public static DynamicValue GetModifiedTime(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectStringAt(0, out var file);

        return FileAccess.GetModifiedTime(file);
    }

    [RuntimeModuleFunction("get_open_error")]
    [EvilDocFunction(
        "Returns the result of the last open call in the current thread.",
        Returns = "Last open error",
        ReturnType = DynamicValueType.Error
    )]
    public static DynamicValue GetOpenError(Fiber _, params DynamicValue[] args)
    {
        args.ExpectNone();

        return new DynamicValue(new Error(FileAccess.GetOpenError().ToString()));
    }

    [RuntimeModuleFunction("get_pascal_string")]
    [EvilDocFunction(
        "Returns the next line of the file as a String. The returned string doesn't include newline (\\n) or carriage return (\\r) characters, but does include any other leading or trailing whitespace.\n\nText is interpreted as being UTF-8 encoded.",
        Returns = "Next line of opened file",
        ReturnType = DynamicValueType.String
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    public static DynamicValue GetPascalString(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(1)
            .ExpectNativeObjectAt(0, out var file);

        return (file as FileAccess).GetPascalString();
    }

    [RuntimeModuleFunction("open")]
    [EvilDocFunction(
        "Opens a file.",
        Returns = "FileAccess with opened file",
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
            .ExpectIntegerAt(1, out var mode);
        return new DynamicValue(FileAccess.Open(path, (FileAccess.ModeFlags)(int)mode));
    }

    [RuntimeModuleFunction("open_compressed")]
    [EvilDocFunction(
        "Opens a compressed file.",
        Returns = "FileAccess with opened file",
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
    [EvilDocArgument(
        "compression_mode",
        "File compression mode",
        DynamicValueType.Number
    )]
    public static DynamicValue OpenCompressed(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(3)
            .ExpectStringAt(0, out var path)
            .ExpectIntegerAt(1, out var mode)
            .OptionalIntegerAt(2, 0, out var compression);
        return new DynamicValue(FileAccess.OpenCompressed(path, (FileAccess.ModeFlags)(int)mode, (FileAccess.CompressionMode)(int)compression));
    }

    [RuntimeModuleFunction("open_encrypted")]
    [EvilDocFunction(
        "Opens a encrypted file.",
        Returns = "FileAccess with opened file",
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
    [EvilDocArgument(
        "key",
        "Encryption key, table of bytes",
        DynamicValueType.Table
    )]
    public static DynamicValue OpenEncrypted(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(3)
            .ExpectStringAt(0, out var path)
            .ExpectIntegerAt(1, out var mode)
            .ExpectTableAt(2, out var key);

        byte[] output = new byte[key.Length];

        for (int i = 0; i < key.Length; i++)
        {
            output[i] = (byte)(key[i].Number);
        }
        return new DynamicValue(FileAccess.OpenEncrypted(path, (FileAccess.ModeFlags)(int)mode, output));
    }

    [RuntimeModuleFunction("open_encrypted_with_pass")]
    [EvilDocFunction(
        "Opens a encrypted file.",
        Returns = "FileAccess with opened file",
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
    [EvilDocArgument(
        "key",
        "Encryption key, string password",
        DynamicValueType.String
    )]
    public static DynamicValue OpenEncryptedWithPass(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(3)
            .ExpectStringAt(0, out var path)
            .ExpectIntegerAt(1, out var mode)
            .ExpectStringAt(2, out var key);

        return new DynamicValue(FileAccess.OpenEncryptedWithPass(path, (FileAccess.ModeFlags)(int)mode, key));
    }

    [RuntimeModuleFunction("seek")]
    [EvilDocFunction(
        "Changes the file reading/writing cursor to the specified position (in bytes from the beginning of the file).",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "position",
        "Position in bytes from beginning of the file",
        DynamicValueType.Number
    )]
    public static DynamicValue Seek(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var file)
            .ExpectNumberAt(1, out var position);

        (file as FileAccess).Seek((ulong)position);
        return DynamicValue.Nil;
    }

    [RuntimeModuleFunction("seek_end")]
    [EvilDocFunction(
        "Changes the file reading/writing cursor to the specified position (in bytes from the end of the file).\nNote: This is an offset, so you should use negative numbers or the cursor will be at the end of the file.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "position",
        "Position in bytes from end of the file",
        DynamicValueType.Number
    )]
    public static DynamicValue SeekEnd(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var file)
            .ExpectNumberAt(1, out var position);

        (file as FileAccess).SeekEnd((long)position);
        return DynamicValue.Nil;
    }

    [RuntimeModuleFunction("store_8")]
    [EvilDocFunction(
        "Stores an integer as 8 bits in the file.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "content",
        "8-bit integer to store",
        DynamicValueType.Number
    )]
    public static DynamicValue Store8(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var file)
            .ExpectIntegerAt(1, out var content);

        (file as FileAccess).Store8((byte)content);
        return DynamicValue.Nil;
    }

    [RuntimeModuleFunction("store_16")]
    [EvilDocFunction(
        "Stores an integer as 16 bits in the file.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "content",
        "16-bit integer to store",
        DynamicValueType.Number
    )]
    public static DynamicValue Store16(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var file)
            .ExpectIntegerAt(1, out var content);

        (file as FileAccess).Store16((ushort)content);
        return DynamicValue.Nil;
    }

    [RuntimeModuleFunction("store_32")]
    [EvilDocFunction(
        "Stores an integer as 32 bits in the file.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "content",
        "32-bit integer to store",
        DynamicValueType.Number
    )]
    public static DynamicValue Store32(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var file)
            .ExpectIntegerAt(1, out var content);

        (file as FileAccess).Store32((uint)content);
        return DynamicValue.Nil;
    }

    [RuntimeModuleFunction("store_64")]
    [EvilDocFunction(
        "Stores an integer as 64 bits in the file.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "content",
        "64-bit integer to store",
        DynamicValueType.Number
    )]
    public static DynamicValue Store64(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var file)
            .ExpectIntegerAt(1, out var content);

        (file as FileAccess).Store64((ulong)content);
        return DynamicValue.Nil;
    }

    [RuntimeModuleFunction("store_buffer")]
    [EvilDocFunction(
        "Stores the given array of bytes in the file.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "content",
        "Table of bytes",
        DynamicValueType.Table
    )]
    public static DynamicValue StoreBuffer(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var file)
            .ExpectTableAt(1, out var content);

        byte[] output = new byte[content.Length];

        for (int i = 0; i < content.Length; i++)
        {
            output[i] = (byte)(content[i].Number);
        }
        (file as FileAccess).StoreBuffer(output);
        return DynamicValue.Nil;
    }

    [RuntimeModuleFunction("store_number")]
    [EvilDocFunction(
        "Stores a floating-point number as 64 bits in the file.",
        ReturnType = DynamicValueType.Nil
    )]
    [EvilDocArgument(
        "file",
        "FileAccess type containing opened file",
        DynamicValueType.NativeObject
    )]
    [EvilDocArgument(
        "content",
        "Number to store",
        DynamicValueType.Number
    )]
    public static DynamicValue StoreNumber(Fiber _, params DynamicValue[] args)
    {
        args.ExpectExactly(2)
            .ExpectNativeObjectAt(0, out var file)
            .ExpectNumberAt(1, out var content);

        (file as FileAccess).StoreDouble(content);
        return DynamicValue.Nil;
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