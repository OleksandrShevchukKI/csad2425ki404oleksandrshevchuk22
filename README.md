# Repository Details
This repository is for the CSAD project for the academic year 2024-2025. It contains various tasks and projects related to our studies.

## Task Details
**Task 1:** Initiate GIT repository  
**Task 2:** Create a simple communication schema SW(client) <-> UART <-> HW(server). Create YML file  
**Task 3:** Implement Server (HW) and Client (SW) parts of game (FEF)  
**Task 4:** Create doxygen documentation  
**Task 5:** Implement automated tests

## Student Details
- **Name:** Oleksandr Shevchuk
- **Student Number:** 22
- **Group:** KI-404
- **Table 1 Details:** Game: Rock-Paper-Scissors, config format: XML.

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
    - Navigate to the `RockPaperScissors` directory and open `RockPaperScissors.sln`.

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
    - In Visual Studio, set `RockPaperScissorsClient` as the startup project and run it by pressing `F5` or by selecting `Debug` > `Start Debugging`.

2. **Upload the Arduino Sketch:**
    - Connect your Arduino board to your computer.
    - Open the Arduino IDE, navigate to `File` > `Open` and open the `RockPaperScissors/SerialPortServer/SerialPortServer.ino` file.
    - Select the correct board and port from the `Tools` menu.
    - Click the `Upload` button to upload the sketch to the Arduino board.

## CI Pipeline
A Continuous Integration (CI) pipeline is configured to automate the build and testing processes. It triggers on every push and pull request to any branch. 

## Doxygen Documentation

Doxygen documentation has been added to the project for both the client and server parts.

## Test coverage using Coverlet
Using the Coverlet tool Visual Studio:
![image](https://github.com/user-attachments/assets/c2dc89f7-a907-44eb-b2e1-e4d4d4a32712)   
The low percentage of test coverage is due to the use of many private methods for initialization, loading configurations, and using the SerialPort class.

## Version Number

- **Current Version:** 5.0.0
