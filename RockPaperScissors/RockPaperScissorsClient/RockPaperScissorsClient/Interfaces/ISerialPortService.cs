using System.IO.Ports;

namespace MauiGithubActionsSample.Interfaces
{
    public interface ISerialPortService
    {
        event SerialDataReceivedEventHandler DataReceived;
        void Open();
        void Close();
        bool IsOpen { get; }
        void WriteLine(string text);
        string ReadLine();
    }
}
