/**
 * @fileoverview Main window for the Calculator Demo WPF application with calculator functionality
 * @module CalculatorDemo.MainWindow
 */

using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Logger instance for the main window
        /// </summary>
        private readonly ILogger<MainWindow> _logger;

        /// <summary>
        /// Current calculation state
        /// </summary>
        private string _currentNumber = "0";
        
        /// <summary>
        /// Previous number in the calculation
        /// </summary>
        private double _previousNumber = 0;
        
        /// <summary>
        /// Current operation to perform
        /// </summary>
        private string _currentOperation = "";
        
        /// <summary>
        /// Flag indicating if we should start a new number
        /// </summary>
        private bool _shouldStartNewNumber = true;
        
        /// <summary>
        /// Flag indicating if the last operation was equals
        /// </summary>
        private bool _lastWasEquals = false;

        /// <summary>
        /// Initializes a new instance of the MainWindow class
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _logger = App.LoggerFactory.CreateLogger<MainWindow>();
            
            _logger.LogInformation("MainWindow initialized successfully");
            
            // Set up keyboard event handling
            this.KeyDown += MainWindow_KeyDown;
            this.Focus();
        }

        /// <summary>
        /// Handles keyboard input for calculator operations
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">Key event arguments</param>
        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _logger.LogDebug("Key pressed: {Key}", e.Key);

            switch (e.Key)
            {
                case System.Windows.Input.Key.D0:
                case System.Windows.Input.Key.NumPad0:
                    AppendNumber("0");
                    break;
                case System.Windows.Input.Key.D1:
                case System.Windows.Input.Key.NumPad1:
                    AppendNumber("1");
                    break;
                case System.Windows.Input.Key.D2:
                case System.Windows.Input.Key.NumPad2:
                    AppendNumber("2");
                    break;
                case System.Windows.Input.Key.D3:
                case System.Windows.Input.Key.NumPad3:
                    AppendNumber("3");
                    break;
                case System.Windows.Input.Key.D4:
                case System.Windows.Input.Key.NumPad4:
                    AppendNumber("4");
                    break;
                case System.Windows.Input.Key.D5:
                case System.Windows.Input.Key.NumPad5:
                    AppendNumber("5");
                    break;
                case System.Windows.Input.Key.D6:
                case System.Windows.Input.Key.NumPad6:
                    AppendNumber("6");
                    break;
                case System.Windows.Input.Key.D7:
                case System.Windows.Input.Key.NumPad7:
                    AppendNumber("7");
                    break;
                case System.Windows.Input.Key.D8:
                case System.Windows.Input.Key.NumPad8:
                    AppendNumber("8");
                    break;
                case System.Windows.Input.Key.D9:
                case System.Windows.Input.Key.NumPad9:
                    AppendNumber("9");
                    break;
                case System.Windows.Input.Key.OemPeriod:
                case System.Windows.Input.Key.Decimal:
                    AppendDecimal();
                    break;
                case System.Windows.Input.Key.Add:
                case System.Windows.Input.Key.OemPlus:
                    SetOperation("+");
                    break;
                case System.Windows.Input.Key.Subtract:
                case System.Windows.Input.Key.OemMinus:
                    SetOperation("−");
                    break;
                case System.Windows.Input.Key.Multiply:
                    SetOperation("×");
                    break;
                case System.Windows.Input.Key.Divide:
                    SetOperation("÷");
                    break;
                case System.Windows.Input.Key.Enter:
                    CalculateResult();
                    break;
                case System.Windows.Input.Key.Escape:
                    ClearAll();
                    break;
                case System.Windows.Input.Key.Back:
                    Backspace();
                    break;
            }
        }

        /// <summary>
        /// Appends a number to the current display
        /// </summary>
        /// <param name="number">The number to append</param>
        private void AppendNumber(string number)
        {
            if (_shouldStartNewNumber)
            {
                _currentNumber = number;
                _shouldStartNewNumber = false;
            }
            else
            {
                if (_currentNumber == "0" && number != ".")
                {
                    _currentNumber = number;
                }
                else
                {
                    _currentNumber += number;
                }
            }

            UpdateDisplay();
            _logger.LogDebug("Number appended: {Number}, Current display: {CurrentNumber}", number, _currentNumber);
        }

        /// <summary>
        /// Appends a decimal point to the current number
        /// </summary>
        private void AppendDecimal()
        {
            if (_shouldStartNewNumber)
            {
                _currentNumber = "0.";
                _shouldStartNewNumber = false;
            }
            else if (!_currentNumber.Contains("."))
            {
                _currentNumber += ".";
            }

            UpdateDisplay();
            _logger.LogDebug("Decimal appended, Current display: {CurrentNumber}", _currentNumber);
        }

        /// <summary>
        /// Sets the current operation and prepares for the next number
        /// </summary>
        /// <param name="operation">The operation to perform</param>
        private void SetOperation(string operation)
        {
            if (!_shouldStartNewNumber)
            {
                if (!string.IsNullOrEmpty(_currentOperation))
                {
                    CalculateResult();
                }

                if (double.TryParse(_currentNumber, out double number))
                {
                    _previousNumber = number;
                    _currentOperation = operation;
                    _shouldStartNewNumber = true;
                    UpdateExpressionDisplay();
                    _logger.LogDebug("Operation set: {Operation}, Previous number: {PreviousNumber}", operation, _previousNumber);
                }
            }
            else if (!string.IsNullOrEmpty(_currentOperation))
            {
                _currentOperation = operation;
                UpdateExpressionDisplay();
                _logger.LogDebug("Operation changed to: {Operation}", operation);
            }
        }

        /// <summary>
        /// Calculates the result of the current operation
        /// </summary>
        private void CalculateResult()
        {
            if (string.IsNullOrEmpty(_currentOperation) || _shouldStartNewNumber)
            {
                return;
            }

            if (!double.TryParse(_currentNumber, out double currentNumber))
            {
                _logger.LogWarning("Failed to parse current number: {CurrentNumber}", _currentNumber);
                return;
            }

            double result = 0;
            bool calculationSuccessful = true;

            try
            {
                switch (_currentOperation)
                {
                    case "+":
                        result = _previousNumber + currentNumber;
                        break;
                    case "−":
                        result = _previousNumber - currentNumber;
                        break;
                    case "×":
                        result = _previousNumber * currentNumber;
                        break;
                    case "÷":
                        if (currentNumber == 0)
                        {
                            MessageBox.Show("Cannot divide by zero!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            calculationSuccessful = false;
                        }
                        else
                        {
                            result = _previousNumber / currentNumber;
                        }
                        break;
                    default:
                        _logger.LogWarning("Unknown operation: {Operation}", _currentOperation);
                        calculationSuccessful = false;
                        break;
                }

                if (calculationSuccessful)
                {
                    _currentNumber = result.ToString(CultureInfo.InvariantCulture);
                    _previousNumber = result;
                    _currentOperation = "";
                    _shouldStartNewNumber = true;
                    _lastWasEquals = true;
                    
                    UpdateDisplay();
                    UpdateExpressionDisplay();
                    
                    _logger.LogInformation("Calculation completed: {PreviousNumber} {Operation} {CurrentNumber} = {Result}", 
                        _previousNumber, _currentOperation, currentNumber, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during calculation");
                MessageBox.Show("An error occurred during calculation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Clears all calculator state
        /// </summary>
        private void ClearAll()
        {
            _currentNumber = "0";
            _previousNumber = 0;
            _currentOperation = "";
            _shouldStartNewNumber = true;
            _lastWasEquals = false;
            
            UpdateDisplay();
            UpdateExpressionDisplay();
            
            _logger.LogInformation("Calculator cleared");
        }

        /// <summary>
        /// Clears the current entry
        /// </summary>
        private void ClearEntry()
        {
            _currentNumber = "0";
            _shouldStartNewNumber = true;
            
            UpdateDisplay();
            
            _logger.LogDebug("Current entry cleared");
        }

        /// <summary>
        /// Removes the last character from the current number
        /// </summary>
        private void Backspace()
        {
            if (_currentNumber.Length > 1)
            {
                _currentNumber = _currentNumber.Substring(0, _currentNumber.Length - 1);
            }
            else
            {
                _currentNumber = "0";
                _shouldStartNewNumber = true;
            }
            
            UpdateDisplay();
            _logger.LogDebug("Backspace performed, Current display: {CurrentNumber}", _currentNumber);
        }

        /// <summary>
        /// Calculates the percentage of the current number
        /// </summary>
        private void CalculatePercent()
        {
            if (double.TryParse(_currentNumber, out double number))
            {
                double result = number / 100.0;
                _currentNumber = result.ToString(CultureInfo.InvariantCulture);
                _shouldStartNewNumber = true;
                
                UpdateDisplay();
                _logger.LogDebug("Percentage calculated: {Number}% = {Result}", number, result);
            }
        }

        /// <summary>
        /// Calculates the square root of the current number
        /// </summary>
        private void CalculateSquareRoot()
        {
            if (double.TryParse(_currentNumber, out double number))
            {
                if (number < 0)
                {
                    MessageBox.Show("Cannot calculate square root of negative number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                double result = Math.Sqrt(number);
                _currentNumber = result.ToString(CultureInfo.InvariantCulture);
                _shouldStartNewNumber = true;
                
                UpdateDisplay();
                _logger.LogDebug("Square root calculated: √{Number} = {Result}", number, result);
            }
        }

        /// <summary>
        /// Calculates the square of the current number
        /// </summary>
        private void CalculateSquare()
        {
            if (double.TryParse(_currentNumber, out double number))
            {
                double result = number * number;
                _currentNumber = result.ToString(CultureInfo.InvariantCulture);
                _shouldStartNewNumber = true;
                
                UpdateDisplay();
                _logger.LogDebug("Square calculated: {Number}² = {Result}", number, result);
            }
        }

        /// <summary>
        /// Calculates the inverse (1/x) of the current number
        /// </summary>
        private void CalculateInverse()
        {
            if (double.TryParse(_currentNumber, out double number))
            {
                if (number == 0)
                {
                    MessageBox.Show("Cannot calculate inverse of zero!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                double result = 1.0 / number;
                _currentNumber = result.ToString(CultureInfo.InvariantCulture);
                _shouldStartNewNumber = true;
                
                UpdateDisplay();
                _logger.LogDebug("Inverse calculated: 1/{Number} = {Result}", number, result);
            }
        }

        /// <summary>
        /// Updates the main result display
        /// </summary>
        private void UpdateDisplay()
        {
            ResultDisplay.Text = _currentNumber;
        }

        /// <summary>
        /// Updates the expression display showing the current calculation
        /// </summary>
        private void UpdateExpressionDisplay()
        {
            if (!string.IsNullOrEmpty(_currentOperation))
            {
                ExpressionDisplay.Text = $"{_previousNumber} {_currentOperation}";
            }
            else
            {
                ExpressionDisplay.Text = "";
            }
        }

        #region Event Handlers

        /// <summary>
        /// Event handler for number button clicks
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                AppendNumber(button.Content.ToString() ?? "");
            }
        }

        /// <summary>
        /// Event handler for decimal button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            AppendDecimal();
        }

        /// <summary>
        /// Event handler for operator button clicks
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                SetOperation(button.Content.ToString() ?? "");
            }
        }

        /// <summary>
        /// Event handler for equals button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateResult();
        }

        /// <summary>
        /// Event handler for clear button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        /// <summary>
        /// Event handler for clear entry button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            ClearEntry();
        }

        /// <summary>
        /// Event handler for backspace button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            Backspace();
        }

        /// <summary>
        /// Event handler for percent button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            CalculatePercent();
        }

        /// <summary>
        /// Event handler for square root button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateSquareRoot();
        }

        /// <summary>
        /// Event handler for square button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateSquare();
        }

        /// <summary>
        /// Event handler for inverse button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments</param>
        private void InverseButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateInverse();
        }

        #endregion
    }
} 