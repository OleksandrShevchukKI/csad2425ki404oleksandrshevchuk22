namespace SerialPortClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Display available ports
            SerialPortClient client = new SerialPortClient(new SerialPortWrapper("COM1"));
            string[] availablePorts = client.GetAvailablePorts();

            Console.WriteLine("Available COM Ports:");
            foreach (var port in availablePorts)
            {
                Console.WriteLine(port);
            }

            // Prompt the user to enter a port name
            Console.Write("Select a COM port to open: ");
            string selectedPort = Console.ReadLine()!;

            if (string.IsNullOrEmpty(selectedPort))
            {
                Console.WriteLine("Port can not be null or empty");
                return;
            }

            // Open the selected port
            try
            {
                client.OpenPort(selectedPort);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening port: {ex.Message}");
                return;
            }

            // Main loop for sending and receiving messages
            while (true)
            {
                Console.Write("Enter message to send (or type 'exit' to exit): ");
                string message = Console.ReadLine()!;
                if (string.IsNullOrEmpty(message))
                {
                    break;
                }
                if (message.ToLower() == "exit")
                {
                    break;
                }

                // Send the message
                client.SendMessage(message);

                // Wait for a response
                client.ReceiveMessage();
            }

            // Close the port when done
            client.ClosePort();
            Console.WriteLine("Port closed. Exiting...");
        }
    }
}