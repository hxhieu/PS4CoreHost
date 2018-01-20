using System.Net;

namespace PS4CoreHost.Utils
{
    public interface IPayloadSender
    {
        /// <summary>
        /// Send a payload the the exploited PS4
        /// </summary>
        /// <param name="host">IP address</param>
        /// <param name="data">The payload</param>
        /// <param name="port">Default specter's 9020</param>
        void Send(string host, byte[] data, int port = 9020);
    }
}
