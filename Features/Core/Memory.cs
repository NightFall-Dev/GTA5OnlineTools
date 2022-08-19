namespace GTA5OnlineTools.Features.Core;

public static class Memory
{
    public static Process process;
    public static IntPtr windowHandle;
    public static IntPtr processHandle;
    public static long baseAddress;

    public struct WindowData
    {
        public int Left;
        public int Top;
        public int Width;
        public int Height;
    }

    public static bool Initialize(string ProcessName)
    {
        var pArray = Process.GetProcessesByName(ProcessName);
        if (pArray.Length > 0)
        {
            process = pArray[0];
            windowHandle = process.MainWindowHandle;
            processHandle = WinAPI.OpenProcess(WinAPI.PROCESS_VM_READ | WinAPI.PROCESS_VM_WRITE | WinAPI.PROCESS_VM_OPERATION, false, process.Id);
            if (process.MainModule != null)
            {
                baseAddress = process.MainModule.BaseAddress.ToInt64();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static int GetProcessID()
    {
        return process.Id;
    }

    public static void CloseHandle()
    {
        if (processHandle != IntPtr.Zero)
            WinAPI.CloseHandle(processHandle);
    }

    public static long GetBaseAddress()
    {
        return baseAddress;
    }

    public static int GetModule(string moduleName)
    {
        foreach (ProcessModule module in process.Modules)
        {
            if (module.ModuleName == (moduleName))
            {
                return (int)module.BaseAddress;
            }
        }

        return 0;
    }

    public static bool IsForegroundWindow()
    {
        return windowHandle == WinAPI.GetForegroundWindow();
    }

    public static void SetForegroundWindow()
    {
        WinAPI.SetForegroundWindow(windowHandle);
    }

    public static bool IsValid(long Address)
    {
        return Address >= 0x10000 && Address < 0x000F000000000000;
    }

    public static long FindPattern(string pattern)
    {
        long address = 0;

        List<byte> tempArray = new List<byte>();
        foreach (var each in pattern.Split(' '))
        {
            if (each == "??")
            {
                tempArray.Add(Convert.ToByte("0", 16));
            }
            else
            {
                tempArray.Add(Convert.ToByte(each, 16));
            }
        }

        byte[] patternByteArray = tempArray.ToArray();

        int moduleSize = process.MainModule.ModuleMemorySize;
        if (moduleSize == 0) throw new Exception($"模块 {process.MainModule.ModuleName} 大小无效");

        byte[] localModulebytes = new byte[moduleSize];
        WinAPI.ReadProcessMemory(processHandle, baseAddress, localModulebytes, moduleSize, out _);

        for (int indexAfterBase = 0; indexAfterBase < localModulebytes.Length; indexAfterBase++)
        {
            bool noMatch = false;

            if (localModulebytes[indexAfterBase] != patternByteArray[0])
                continue;

            for (var MatchedIndex = 0; MatchedIndex < patternByteArray.Length && indexAfterBase + MatchedIndex < localModulebytes.Length; MatchedIndex++)
            {
                if (patternByteArray[MatchedIndex] == 0x0)
                    continue;
                if (patternByteArray[MatchedIndex] != localModulebytes[indexAfterBase + MatchedIndex])
                {
                    noMatch = true;
                    break;
                }
            }

            if (!noMatch)
                return baseAddress + indexAfterBase;
        }

        return address;
    }

    public static long Rip_37(long address)
    {
        return address + Read<int>(address + 0x03, null) + 0x07;
    }

    public static long Rip_6A(long address)
    {
        return address + Read<int>(address + 0x06, null) + 0x0A;
    }

    public static long Rip_389(long address)
    {
        return address + Read<int>(address + 0x03, null) - 0x89;
    }

    public static WindowData GetGameWindowData()
    {
        // 获取指定窗口句柄的窗口矩形数据和客户区矩形数据
        WinAPI.GetWindowRect(windowHandle, out W32RECT windowRect);
        WinAPI.GetClientRect(windowHandle, out W32RECT clientRect);

        // 计算窗口区的宽和高
        int windowWidth = windowRect.Right - windowRect.Left;
        int windowHeight = windowRect.Bottom - windowRect.Top;

        // 处理窗口最小化
        if (windowRect.Left == -32000)
        {
            return new WindowData()
            {
                Left = 0,
                Top = 0,
                Width = 1,
                Height = 1
            };
        }

        // 计算客户区的宽和高
        int clientWidth = clientRect.Right - clientRect.Left;
        int clientHeight = clientRect.Bottom - clientRect.Top;

        // 计算边框
        int borderWidth = (windowWidth - clientWidth) / 2;
        int borderHeight = windowHeight - clientHeight - borderWidth;

        return new WindowData()
        {
            Left = windowRect.Left += borderWidth,
            Top = windowRect.Top += borderHeight,
            Width = clientWidth,
            Height = clientHeight
        };
    }

    private static long GetPtrAddress(long pointer, int[] offset)
    {
        if (offset != null)
        {
            byte[] buffer = new byte[8];
            WinAPI.ReadProcessMemory(processHandle, pointer, buffer, buffer.Length, out _);

            for (int i = 0; i < (offset.Length - 1); i++)
            {
                pointer = BitConverter.ToInt64(buffer, 0) + offset[i];
                WinAPI.ReadProcessMemory(processHandle, pointer, buffer, buffer.Length, out _);
            }

            pointer = BitConverter.ToInt64(buffer, 0) + offset[offset.Length - 1];
        }

        return pointer;
    }

    public static T Read<T>(long basePtr, int[] offsets) where T : struct
    {
        byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
        WinAPI.ReadProcessMemory(processHandle, GetPtrAddress(basePtr, offsets), buffer, buffer.Length, out _);
        return ByteArrayToStructure<T>(buffer);
    }

    public static T Read<T>(long address) where T : struct
    {
        byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
        WinAPI.ReadProcessMemory(processHandle, address, buffer, buffer.Length, out _);
        return ByteArrayToStructure<T>(buffer);
    }

    public static void Write<T>(long basePtr, int[] offsets, T value) where T : struct
    {
        byte[] buffer = StructureToByteArray(value);
        WinAPI.WriteProcessMemory(processHandle, GetPtrAddress(basePtr, offsets), buffer, buffer.Length, out _);
    }

    public static void Write<T>(long address, T value) where T : struct
    {
        byte[] buffer = StructureToByteArray(value);
        WinAPI.WriteProcessMemory(processHandle, address, buffer, buffer.Length, out _);
    }

    public static float[] ReadMatrix<T>(long address, int MatrixSize) where T : struct
    {
        int ByteSize = Marshal.SizeOf(typeof(T));
        byte[] buffer = new byte[ByteSize * MatrixSize];
        WinAPI.ReadProcessMemory(processHandle, address, buffer, buffer.Length, out _);
        return ConvertToFloatArray(buffer);
    }

    public static string ReadString(long basePtr, int[] offsets, int size)
    {
        byte[] buffer = new byte[size];
        WinAPI.ReadProcessMemory(processHandle, GetPtrAddress(basePtr, offsets), buffer, size, out _);

        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] == 0)
            {
                byte[] _buffer = new byte[i];
                Buffer.BlockCopy(buffer, 0, _buffer, 0, i);
                return Encoding.UTF8.GetString(_buffer);
            }
        }

        return Encoding.UTF8.GetString(buffer);
    }

    public static void WriteString(long basePtr, int[] offsets, string str)
    {
        byte[] buffer = new ASCIIEncoding().GetBytes(str);
        WinAPI.WriteProcessMemory(processHandle, GetPtrAddress(basePtr, offsets), buffer, buffer.Length, out _);
    }

    public static byte[] ReadBytes(long basePtr, int size)
    {
        byte[] buffer = new byte[size];

        WinAPI.ReadProcessMemory(processHandle, basePtr, buffer, size, out _);
        return buffer;
    }

    public static void WriteBytes(long basePtr, byte[] bytes)
    {
        WinAPI.WriteProcessMemory(processHandle, basePtr, bytes, bytes.Length, out _);
    }

    #region Conversion
    private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
    {
        var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        try
        {
            return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        }
        finally
        {
            handle.Free();
        }
    }

    private static byte[] StructureToByteArray(object obj)
    {
        int length = Marshal.SizeOf(obj);
        byte[] array = new byte[length];
        IntPtr pointer = Marshal.AllocHGlobal(length);
        Marshal.StructureToPtr(obj, pointer, true);
        Marshal.Copy(pointer, array, 0, length);
        Marshal.FreeHGlobal(pointer);
        return array;
    }

    private static float[] ConvertToFloatArray(byte[] bytes)
    {
        if (bytes.Length % 4 != 0)
        {
            throw new ArgumentException();
        }

        float[] floats = new float[bytes.Length / 4];
        for (int i = 0; i < floats.Length; i++)
        {
            floats[i] = BitConverter.ToSingle(bytes, i * 4);
        }
        return floats;
    }
    #endregion
}
