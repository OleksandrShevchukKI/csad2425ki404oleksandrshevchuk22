using System.IO.Ports;

namespace SerialPortClient
{
    public class SerialPortClient
    {
        private readonly ISerialPortWrapper _serialPort;

        public SerialPortClient(ISerialPortWrapper serialPort)
        {
            _serialPort = serialPort;
        }

        public string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        public void OpenPort(string portName)
        {
            _serialPort.Open();
            Console.WriteLine("Port is open.");
        }

        public void ClosePort()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                Console.WriteLine("Port is closed.");
            }
        }

        public void SendMessage(string message)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.WriteLine(message);
                Console.WriteLine("Message sent.");
                Thread.Sleep(5000);
            }
        }

        public string ReceiveMessage()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    Thread.Sleep(500);
                    string response = _serialPort.ReadLine();
                    Console.WriteLine($"Received a message from Arduino: {response}");
                    return response;
                }
            }
            catch (TimeoutException)
            {
                Console.WriteLine("No response received within the timeout period.");
            }
            return string.Empty;
        }
    }
}