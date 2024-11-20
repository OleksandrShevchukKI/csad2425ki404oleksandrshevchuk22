using MauiGithubActionsSample.Interfaces;
using System.IO.Ports;

namespace MauiGithubActionsSample.Services
{
    public class SerialPortService : ISerialPortService
    {
        private readonly SerialPort _serialPort;

        public SerialPortService(string portName, int baudRate)
        {
            _serialPort = new SerialPort(portName, baudRate);
            _serialPort.DataReceived += (sender, e) => DataReceived?.Invoke(sender, e);
        }

        public event SerialDataReceivedEventHandler DataReceived;

        public void Open() => _serialPort.Open();
        public void Close() => _serialPort.Close();
        public bool IsOpen => _serialPort.IsOpen;
        public void WriteLine(string text) => _serialPort.WriteLine(text);
        public string ReadLine() => _serialPort.ReadLine();
    }

}
