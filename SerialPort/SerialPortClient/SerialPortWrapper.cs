using System.IO.Ports;

namespace SerialPortClient
{
    public class SerialPortWrapper : ISerialPortWrapper
    {
        private readonly SerialPort _serialPort;

        public SerialPortWrapper(string portName)
        {
            _serialPort = new SerialPort(portName, 9600)
            {
                ReadTimeout = 5000
            };
        }

        public void Open()
        {
            _serialPort.Open();
        }

        public void Close()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        public bool IsOpen => _serialPort.IsOpen;

        public void WriteLine(string message)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.WriteLine(message);
            }
        }

        public string ReadLine()
        {
            return _serialPort.ReadLine();
        }
    }
}