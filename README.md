# Repository Details
This repository is for the CSAD project for the academic year 2024-2025. It contains various tasks and projects related to our studies.

## Task Details
**Task 1:** Initiate GIT repository

## Student Details
- **Name:** Oleksandr Shevchuk
- **Student Number:** 22
- **Group:** KI-404
- **Table 1 Details:** Game: rock paper scissors, config format: XML.

## Technologies and Tools
- **Programming Language:** C#
- **Hardware:** Arduino
- **Tools:** Visual Studio, Git, GitHub

## How to Build the Project

1. **Clone the repository:**
    ```sh
    git clone https://github.com/OleksandrShevchukKI/csad2425ki404oleksandrshevchuk22.git
    cd csad2425ki404oleksandrshevchuk22
    ```

2. **Open the solution file in Visual Studio:**
    - Navigate to the `SerialPort` directory and open `SerialPort.sln`.

3. **Restore dependencies:**
    - Open the Package Manager Console in Visual Studio and run:
    ```sh
    dotnet restore
    ```

4. **Build the project:**
    - In Visual Studio, select `Build` > `Build Solution` or use the following command:
    ```sh
    dotnet build --configuration Release
    ```

## How to Run the Project

1. **Run the Client Application:**
    - In Visual Studio, set `SerialPortClient` as the startup project and run it by pressing `F5` or by selecting `Debug` > `Start Debugging`.

2. **Upload the Arduino Sketch:**
    - Connect your Arduino board to your computer.
    - Open the Arduino IDE, navigate to `File` > `Open` and open the `SerialPort/SerialPortServer/SerialPortServer.ino` file.
    - Select the correct board and port from the `Tools` menu.
    - Click the `Upload` button to upload the sketch to the Arduino board.

## Version Number

- **Current Version:** 2.0.0