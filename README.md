# Calculator Demo - AI-Assisted Development

A C# WPF calculator application designed to demonstrate the capabilities of generative AI in code development. This project showcases modern development practices, comprehensive documentation, and structured logging.

## 🎯 Project Overview

This calculator application serves as a demonstration of AI-assisted software development, featuring:

- **Modern WPF UI**: Casio-style calculator interface with responsive design
- **Comprehensive Logging**: Structured logging with Serilog and Microsoft.Extensions.Logging
- **Best Practices**: Following established programming guidelines and conventions
- **Full Documentation**: Extensive code documentation and comments
- **Error Handling**: Robust error handling and user feedback
- **Keyboard Support**: Full keyboard navigation and input support

## ✨ Features

### Basic Calculator Operations
- **Arithmetic Operations**: Addition (+), Subtraction (−), Multiplication (×), Division (÷)
- **Clear Functions**: Clear All (C), Clear Entry (CE), Backspace (⌫)
- **Decimal Support**: Full decimal number support
- **Equals Operation**: Calculate results with Enter key or = button

### Advanced Mathematical Functions
- **Percentage**: Calculate percentage of current number
- **Square Root**: Calculate square root (√)
- **Square**: Calculate square of number (x²)
- **Inverse**: Calculate reciprocal (1/x)

### User Interface Features
- **Dual Display**: Expression history and current result display
- **Responsive Design**: Modern dark theme with hover effects
- **Keyboard Support**: Full numeric keypad and operation key support
- **Error Messages**: User-friendly error dialogs for invalid operations

### Development Features
- **Structured Logging**: JSON-formatted logs with timestamps and context
- **File Logging**: Daily rolling log files in `logs/` directory
- **Console Logging**: Real-time logging to console during development
- **Comprehensive Documentation**: XML documentation and inline comments

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code
- Windows 10/11 (for WPF support)

### Installation

1. **Clone the repository**:
   ```bash
   git clone <repository-url>
   cd CalculatorDemo
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the application**:
   ```bash
   dotnet build
   ```

4. **Run the application**:
   ```bash
   dotnet run
   ```

### Building for Release
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

## 🎮 Usage

### Mouse/Touch Interface
- Click number buttons (0-9) to input numbers
- Click operation buttons (+, −, ×, ÷) to perform calculations
- Click equals (=) to see the result
- Use function buttons for advanced operations

### Keyboard Interface
- **Numbers**: Use number keys (0-9) or numpad
- **Operations**: 
  - `+` for addition
  - `-` for subtraction
  - `*` for multiplication
  - `/` for division
- **Enter/Return**: Calculate result
- **Escape**: Clear all
- **Backspace**: Remove last character
- **Period**: Add decimal point

### Advanced Functions
- **%**: Calculate percentage (e.g., 50% = 0.5)
- **√**: Calculate square root
- **x²**: Calculate square
- **1/x**: Calculate inverse

## 📁 Project Structure

```
CalculatorDemo/
├── CalculatorDemo.sln          # Visual Studio solution file
├── CalculatorDemo/
│   ├── CalculatorDemo.csproj   # Project file with dependencies
│   ├── App.xaml               # Application-level resources and styles
│   ├── App.xaml.cs            # Application entry point and logging setup
│   ├── MainWindow.xaml        # Main calculator UI
│   └── MainWindow.xaml.cs     # Calculator logic and event handlers
├── Docs/
│   └── programming guidelines.md  # Development guidelines
└── README.md                  # This file
```

## 🔧 Development Guidelines

This project follows the development guidelines specified in `/Docs/programming guidelines.md`:

### Logging Standards
- Structured JSON logging with Serilog
- Context-aware logging with module identification
- File and console logging support
- Proper error handling and logging

### Documentation Standards
- XML documentation for all public methods
- File header comments with module identification
- Comprehensive inline comments
- Clear method and parameter descriptions

### Code Quality
- Nullable reference types enabled
- Proper exception handling
- Input validation and error messages
- Clean separation of concerns

## 📊 Logging

The application generates structured logs in the following locations:

- **Console**: Real-time logging during development
- **File**: `logs/calculator-demo-YYYY-MM-DD.log` (daily rolling)

### Log Levels
- **Information**: Application lifecycle events
- **Debug**: User interactions and state changes
- **Warning**: Non-critical issues and edge cases
- **Error**: Exceptions and critical failures

### Example Log Entry
```json
{
  "timestamp": "2024-01-15T10:30:45.123Z",
  "level": "Information",
  "message": "Calculation completed: 5 + 3 = 8",
  "properties": {
    "PreviousNumber": 5,
    "Operation": "+",
    "CurrentNumber": 3,
    "Result": 8
  }
}
```

## 🧪 Testing

The application includes comprehensive error handling and validation:

- **Input Validation**: All user inputs are validated
- **Error Handling**: Graceful handling of division by zero, invalid operations
- **Edge Cases**: Proper handling of decimal points, negative numbers
- **User Feedback**: Clear error messages for invalid operations

## 🎨 UI Design

The calculator features a modern, Casio-inspired design:

- **Dark Theme**: Professional dark color scheme
- **Button Styles**: Distinct styles for different button types
- **Hover Effects**: Visual feedback on button interactions
- **Responsive Layout**: Grid-based layout that adapts to window size
- **Typography**: Clear, readable fonts with appropriate sizing

### Color Scheme
- **Background**: Dark gray (#1E1E1E)
- **Display**: Medium gray (#2C2C2C)
- **Number Buttons**: Dark gray (#3C3C3C)
- **Operator Buttons**: Orange (#FF9500)
- **Function Buttons**: Gray (#666666)
- **Clear Button**: Red (#CC0000)
- **Equals Button**: Green (#00CC00)

## 🤝 Contributing

This project demonstrates AI-assisted development practices. When contributing:

1. Follow the programming guidelines in `/Docs/programming guidelines.md`
2. Ensure all code is properly documented
3. Add appropriate logging for new features
4. Test thoroughly before submitting changes
5. Use conventional commit messages

## 📝 License

This project is created for educational and demonstration purposes.

## 🙏 Acknowledgments

This project demonstrates the capabilities of generative AI in software development, showcasing:

- Automated code generation with proper structure
- Comprehensive documentation generation
- Modern development practices implementation
- UI/UX design with accessibility considerations
- Robust error handling and logging

---

**Note**: This application serves as a demonstration of AI-assisted development capabilities and should be used for educational purposes. 