using ABClient.ABForms;

namespace ABClient.ABProxy
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal sealed class Proxy : IDisposable
    {
        private int _listenPort;
        private Socket _socketAcceptor;

        internal static IPEndPoint Gateway { get; private set; }

        internal static string BasicAuth { get; private set; }

        public void Dispose()
        {
            if (_socketAcceptor == null)
            {
            }
            else
            {
                try
                {
                    _socketAcceptor.LingerState = new LingerOption(true, 0);
                    _socketAcceptor.Close();
                }
                catch
                {
                }
            }
        }

        internal bool Start()
        {
            _listenPort = 8052;
            while (_socketAcceptor == null)
            {
                try
                {
                    if (Socket.OSSupportsIPv6)
                    {
                        _socketAcceptor = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                        if (Environment.OSVersion.Version.Major > 5)
                        {
                            _socketAcceptor.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, 0);
                        }

                        _socketAcceptor.Bind(new IPEndPoint(IPAddress.IPv6Any, _listenPort));
                    }
                    else
                    {
                        _socketAcceptor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        _socketAcceptor.Bind(new IPEndPoint(IPAddress.Any, _listenPort));
                    }
                    
                    _socketAcceptor.Listen(50);
                }
                catch (SocketException)
                {
                    _socketAcceptor = null;
                    _listenPort++;
                }
            }

            Gateway = null;
            AppVars.LocalProxy = null;
            if (AppVars.Profile.DoProxy)
            {
                AppVars.LocalProxy = new WebProxy(new Uri("http://" + AppVars.Profile.ProxyAddress));
                Gateway = Utilities.IPEndPointFromHostPortString(AppVars.Profile.ProxyAddress);
                if (!string.IsNullOrEmpty(AppVars.Profile.ProxyUserName) && !string.IsNullOrEmpty(AppVars.Profile.ProxyPassword))
                {
                    var proxyString = 
                        string.Format(
                            CultureInfo.InvariantCulture, 
                            "{0}:{1}", 
                            AppVars.Profile.ProxyUserName, 
                            AppVars.Profile.ProxyPassword);
                    var bufcode = AppVars.Codepage.GetBytes(proxyString);
                    var code = Convert.ToBase64String(bufcode);
                    BasicAuth = string.Format(CultureInfo.InvariantCulture, "Basic {0}", code);
                    AppVars.LocalProxy.UseDefaultCredentials = false;
                    AppVars.LocalProxy.Credentials = new NetworkCredential(AppVars.Profile.ProxyUserName, AppVars.Profile.ProxyPassword);
                }
            }

            try
            {
                _socketAcceptor.BeginAccept(AcceptConnection, _socketAcceptor);                
            }
            catch (Exception)
            {
                _socketAcceptor = null;
                return false;
            }

            SetProxy(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "localhost:{0}",
                    _listenPort));
            
            return true;
        }

        private static void SetProxy(string strProxy)
        {
            using (var structIPI = new NativeMethods.InternetProxyInfo(3, Marshal.StringToHGlobalAnsi(strProxy), Marshal.StringToHGlobalAnsi("local")))
            {
                var intptrStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(structIPI));
                Marshal.StructureToPtr(structIPI, intptrStruct, true);
                NativeMethods.InternetSetOption(
                    IntPtr.Zero,
                    38,
                    intptrStruct,
                    Marshal.SizeOf(structIPI));
            }
        }

        private void AcceptConnection(IAsyncResult ar)
        {
            try
            {
                ThreadPool.UnsafeQueueUserWorkItem(Session.CreateAndExecute, _socketAcceptor.EndAccept(ar));
                _socketAcceptor.BeginAccept(AcceptConnection, _socketAcceptor);
            }
            catch (ObjectDisposedException)
            {
            }
            catch(SocketException)
            {
            }

            try
            {
                _socketAcceptor.BeginAccept(AcceptConnection, null);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (SocketException)
            {
            }            
        }
    }
}