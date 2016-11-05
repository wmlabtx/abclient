namespace ABClient.Helpers
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
            EntryPoint = "DeleteUrlCacheEntryA",
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

        /// <summary>
        /// Структура для InternetSetOption.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct InternetProxyInfo : IDisposable
        {
            /// <summary>
            /// Тип доступа.
            /// </summary>
            private readonly int DwAccessType;

            /// <summary>
            /// Указатель на прокси.
            /// </summary>
            private readonly IntPtr PtrProxy;

            /// <summary>
            /// Указатель на прохождение прокси.
            /// </summary>
            private readonly IntPtr PtrProxyBypass;

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
                DwAccessType = dwordAccessType;
                PtrProxy = ptrProxy;
                PtrProxyBypass = ptrProxyBypass;
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