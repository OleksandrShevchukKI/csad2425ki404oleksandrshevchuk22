using MauiGithubActionsSample.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace RockPaperScissorsClient.Test
{
    public class MockSerialPortService : ISerialPortService
    {
        private event SerialDataReceivedEventHandler _serialDataReceived;
        public event EventHandler<CustomSerialDataReceivedEventArgs> DataReceived;

        public bool IsOpen { get; private set; }
        private Queue<string> _responses = new Queue<string>();

        event SerialDataReceivedEventHandler ISerialPortService.DataReceived
        {
            add
            {
                _serialDataReceived += value;
            }
            remove
            {
                _serialDataReceived -= value;
            }
        }

        public void Open() => IsOpen = true;
        public void Close() => IsOpen = false;

        public void WriteLine(string text)
        {
            // Simulate a response from the serial port for testing
            DataReceived?.Invoke(this, new CustomSerialDataReceivedEventArgs(_responses.Count > 0 ? _responses.Dequeue() : string.Empty));
        }

        public string ReadLine() => _responses.Count > 0 ? _responses.Dequeue() : string.Empty;

        public void AddResponse(string response)
        {
            _responses.Enqueue(response);
        }

        public void InvokeDataReceived(CustomSerialDataReceivedEventArgs e)
        {
            DataReceived?.Invoke(this, e);
        }
    }


    public class CustomSerialDataReceivedEventArgs : EventArgs
    {
        public string ReceivedData { get; set; }

        public CustomSerialDataReceivedEventArgs(string data)
        {
            ReceivedData = data;
        }
    }
}
