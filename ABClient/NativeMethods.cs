namespace ABClient
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Unicode,
            EntryPoint = "FindFirstUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindFirstUrlCacheEntry(
            [MarshalAs(UnmanagedType.LPWStr)] string lpszUrlSearchPattern,
            IntPtr lpFirstCacheEntryInfo,
            ref int lpdwFirstCacheEntryInfoBufferSize);

        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindNextUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FindNextUrlCacheEntry(
            IntPtr hFind,
            IntPtr lpNextCacheEntryInfo,
            ref int lpdwNextCacheEntryInfoBufferSize);

        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "DeleteUrlCacheEntry",
            CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteUrlCacheEntry(
            IntPtr lpszUrlName);

        [DllImport(@"wininet",
            SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool InternetSetOption(
            IntPtr handleInternet,
            int dwordOption,
            IntPtr longpBuffer,
            int lpdwBufferLength);

        [DllImport(@"ole32", 
            SetLastError = false)]
        internal static extern int OleDraw(
            IntPtr ptrUnk, 
            int intAspect, 
            IntPtr hdcDraw, 
            ref Rectangle lprcBounds);

        [DllImport(@"user32", 
            SetLastError = false)]
        internal static extern int FlashWindow(
            IntPtr hwnd, 
            int boolInvert);

        [DllImport(@"user32",
            SetLastError = false)]
        internal static extern IntPtr SendMessage(
            IntPtr hWnd, 
            int msg, 
            IntPtr wParam, 
            IntPtr lParam);
        
        internal static bool IsWinXP
        {
            get
            {
                OperatingSystem OS = Environment.OSVersion;
                return (OS.Platform == PlatformID.Win32NT) &&
                    ((OS.Version.Major > 5) || ((OS.Version.Major == 5) && (OS.Version.Minor == 1)));
            }
        }

        internal static bool IsWinVista
        {
            get
            {
                OperatingSystem OS = Environment.OSVersion;
                return (OS.Platform == PlatformID.Win32NT) && (OS.Version.Major >= 6);
            }
        }

        /// <summary>
        /// Структура для InternetSetOption.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct InternetProxyInfo : IDisposable
        {
            /// <summary>
            /// Тип доступа.
            /// </summary>
            private readonly int _dwAccessType;

            /// <summary>
            /// Указатель на прокси.
            /// </summary>
            private readonly IntPtr _ptrProxy;

            /// <summary>
            /// Указатель на прохождение прокси.
            /// </summary>
            private readonly IntPtr _ptrProxyBypass;

            /// <summary>
            /// Initializes a new instance of the <see cref="InternetProxyInfo"/> struct. 
            /// </summary>
            /// <param name="dwordAccessType">
            /// Тип доступа.
            /// </param>
            /// <param name="ptrProxy">
            /// Указатель на прокси.
            /// </param>
            /// <param name="ptrProxyBypass">
            /// Указатель на прохождение прокси.
            /// </param>
            internal InternetProxyInfo(int dwordAccessType, IntPtr ptrProxy, IntPtr ptrProxyBypass)
            {
                _dwAccessType = dwordAccessType;
                _ptrProxy = ptrProxy;
                _ptrProxyBypass = ptrProxyBypass;
            }

            /// <summary>
            /// Фиктивное освобождение.
            /// </summary>
            public void Dispose()
            {
            }
        }
    }
}