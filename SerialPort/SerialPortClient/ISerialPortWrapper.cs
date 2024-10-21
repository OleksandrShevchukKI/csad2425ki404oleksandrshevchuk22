namespace SerialPortClient
{
    public interface ISerialPortWrapper
    {
        void Open();
        void Close();
        bool IsOpen { get; }
        void WriteLine(string message);
        string ReadLine();
    }
}