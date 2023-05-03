using System.Net.Sockets;

namespace caro
{
    public class QuanLiServerBase
    {

        private bool SendData(Socket target, byte[] Data)
        {
            return target.Send(Data) == 1 ? true : false;
        }
    }
}