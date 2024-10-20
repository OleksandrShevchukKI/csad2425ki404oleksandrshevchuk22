using Moq;

namespace SerialPortClient.Tests
{
    public class SerialPortClientTests
    {
        private readonly SerialPortClient _client;
        private readonly Mock<ISerialPortWrapper> _serialPortMock;

        public SerialPortClientTests()
        {
            // Mock the ISerialPortWrapper interface
            _serialPortMock = new Mock<ISerialPortWrapper>();
            _client = new SerialPortClient(_serialPortMock.Object);
        }

        [Fact]
        public void GetAvailablePorts_ShouldReturnAvailablePorts()
        {
            // Act
            var ports = _client.GetAvailablePorts();

            // Assert
            Assert.NotNull(ports);
        }

        [Fact]
        public void OpenPort_ShouldOpenSerialPort()
        {
            // Arrange
            _serialPortMock.Setup(sp => sp.Open());

            // Act
            _client.OpenPort("COM1");

            // Assert
            _serialPortMock.Verify(sp => sp.Open(), Times.Once);
        }

        [Fact]
        public void ClosePort_ShouldCloseSerialPort()
        {
            // Arrange
            _serialPortMock.Setup(sp => sp.IsOpen).Returns(true);
            _serialPortMock.Setup(sp => sp.Close());

            // Act
            _client.ClosePort();

            // Assert
            _serialPortMock.Verify(sp => sp.Close(), Times.Once);
        }

        [Fact]
        public void SendMessage_ShouldSendMessageWhenPortIsOpen()
        {
            // Arrange
            string message = "Hello Arduino";
            _serialPortMock.Setup(sp => sp.IsOpen).Returns(true);
            _serialPortMock.Setup(sp => sp.WriteLine(It.IsAny<string>()));

            // Act
            _client.SendMessage(message);

            // Assert
            _serialPortMock.Verify(sp => sp.WriteLine(message), Times.Once);
        }

        [Fact]
        public void ReceiveMessage_ShouldReceiveMessageWhenPortIsOpen()
        {
            // Arrange
            string expectedResponse = "Hello from Arduino";
            _serialPortMock.Setup(sp => sp.IsOpen).Returns(true);
            _serialPortMock.Setup(sp => sp.ReadLine()).Returns(expectedResponse);

            // Act
            var response = _client.ReceiveMessage();

            // Assert
            Assert.Equal(expectedResponse, response);
        }
    }
}