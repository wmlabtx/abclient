// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelperExterns.cs" company="Murad Ismayilov">
//   Murad Ismayilov
// </copyright>
// <summary>
//   Класс для работы со внешними функциями.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ABClient.MyHelpers
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using Tabs;

    /// <summary>
    /// Класс для работы со внешними функциями.
    /// </summary>
    internal sealed class HelperExterns
    {
        [DllImport("ole32.dll", SetLastError = false)]
        internal static extern IntPtr OleDraw(IntPtr ptrUnk, int intAspect, IntPtr hdcDraw, ref Rectangle lprcBounds);

        [DllImport("user32.dll", SetLastError = false)]
        internal static extern IntPtr FlashWindow(IntPtr hwnd, bool boolInvert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wordParam, ref TcHitTestInfo longParam);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, EntryPoint = "FindFirstUrlCacheEntryA", CallingConvention = CallingConvention.StdCall, SetLastError = false)]
        internal static extern IntPtr FindFirstUrlCacheEntry([MarshalAs(UnmanagedType.LPTStr)] string lpszUrlSearchPattern, IntPtr longpFirstCacheEntryInfo, ref int lpdwFirstCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", EntryPoint = "DeleteUrlCacheEntryA", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr DeleteUrlCacheEntry(IntPtr lpszUrlName);

        [DllImport("wininet.dll", EntryPoint = "FindNextUrlCacheEntryA", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool FindNextUrlCacheEntry(IntPtr handleFind, IntPtr longpNextCacheEntryInfo, ref int lpdwNextCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", SetLastError = false)]
        internal static extern IntPtr InternetSetOption(IntPtr handleInternet, int dwordOption, IntPtr longpBuffer, int lpdwBufferLength);

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

        /// <summary>
        /// Структура элемента кеша.
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 80)]
        internal struct InternetCacheEntryInfoA
        {
            /// <summary>
            /// Тип элемента.
            /// </summary>
            [FieldOffset(12)]
            internal uint CacheEntryType;

            /// <summary>
            /// Урл элемента кеша.
            /// </summary>
            [FieldOffset(4)]
            internal IntPtr LpszSourceUrlName;
        }
    }
}
