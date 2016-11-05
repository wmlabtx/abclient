using System.Net;
using System.Net.Sockets;

namespace ABClient.ABProxy
{
    internal class ClientPipe
    {
        private readonly Socket _baseSocket;

        internal IPAddress Address
        {
            get
            {
                IPAddress result;
                try
                {
                    if (_baseSocket == null || _baseSocket.RemoteEndPoint == null)
                    {
                        result = new IPAddress(0L);
                    }
                    else
                    {
                        result = ((IPEndPoint) _baseSocket.RemoteEndPoint).Address;
                    }
                }
                catch
                {
                    result = new IPAddress(0L);
                }

                return result;
            }
        }

        public int Port
        {
            get
            {
                int result;
                try
                {
                    if (_baseSocket != null && _baseSocket.RemoteEndPoint != null)
                    {
                        result = ((IPEndPoint) _baseSocket.RemoteEndPoint).Port;
                    }
                    else
                    {
                        result = 0;
                    }
                }
                catch
                {
                    result = 0;
                }

                return result;
            }
        }

        internal ClientPipe(Socket oSocket)
        {
            _baseSocket = oSocket;
            _baseSocket.NoDelay = true;
        }

        internal void setReceiveTimeout()
        {
            _baseSocket.ReceiveTimeout = 60000;
        }
    }
}
