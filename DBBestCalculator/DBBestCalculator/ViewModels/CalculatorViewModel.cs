using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorModel;
using DBBestCalculator.Commands;
using System.Windows.Input;

namespace DBBestCalculator.ViewModels
{
    class CalculatorViewModel:ViewModelIBase
    {
        #region Property
        private Calculator calculation;

        private DelegateCommand<string> digitButtonPressCommand;
        private DelegateCommand<string> operationButtonPressCommand;

        private string lastOperation;
        private bool newDisplayRequired = false;
        private string display;
        private string fullExpression;

        public string FirstOperand
        {
            get { return calculation.FirstOperand; }
            set { calculation.FirstOperand = value; }
        }

        public string SecondOperand
        {
            get { return calculation.SecondOperand; }
            set { calculation.SecondOperand = value; }
        }

        public string Operation
        {
            get { return calculation.Operation; }
            set { calculation.Operation = value; }
        }

        public string LastOperation
        {
            get { return lastOperation; }
            set { lastOperation = value; }
        }

        public string Result
        {
            get { return calculation.Result; }
        }
        public string FullExpression
        {
            get { return fullExpression; }
            set { fullExpression = value; OnPropertyChanged("FullExpression"); }
        }
        public string Display
        {
            get { return display; }
            set{ display = value; OnPropertyChanged("Display");}
        }
        #endregion

        public CalculatorViewModel()
        {
            this.calculation = new Calculator();
            this.display = "0";
            this.FirstOperand = string.Empty;
            this.SecondOperand = string.Empty;
            this.Operation = string.Empty;
            this.lastOperation = string.Empty;
            this.fullExpression = string.Empty;
        }

        public ICommand OperationButtonPressCommand
        {
            get
            {
                if (operationButtonPressCommand == null)
                {
                    operationButtonPressCommand = new DelegateCommand<string>(
                        OperationButtonPress, (string number) => { return true; });
                }
                return operationButtonPressCommand;
            }
        }

        public ICommand DigitButtonPressCommand
        {
            get
            {
                if (digitButtonPressCommand == null)
                {
                    digitButtonPressCommand = new DelegateCommand<string>(
                        DigitButtonPress, (string button) => { return true; });
                }
                return digitButtonPressCommand;
            }
        }
        /// <summary>
        /// a method that is executed by pressing the button
        /// </summary>
        /// <param name="button"></param>
        public void DigitButtonPress(string button)
        {
            switch (button)
            {
                case "C":
                    Display = "0";
                    FirstOperand = string.Empty;
                    SecondOperand = string.Empty;
                    Operation = string.Empty;
                    LastOperation = string.Empty;
                    FullExpression = string.Empty;
                    break;
                case "Del":
                    if (display.Length > 1)
                        Display = display.Substring(0, display.Length - 1);
                    else Display = "0";
                    break;
                case "+/-":
                    if (display.Contains("-") || display == "0")
                    {
                        Display = display.Remove(display.IndexOf("-"), 1);
                    }
                    else Display = "-" + display;
                    break;
                case ",":
                    if (newDisplayRequired)
                    {
                        Display = "0,";
                    }
                    else
                    {
                        if (!display.Contains(","))
                        {
                            Display = display + ",";
                        }
                    }
                    break;
                default:
                    if (display == "0" || newDisplayRequired)
                        Display = button;
                    else
                        Display = display + button;
                    break;
            }
            newDisplayRequired = false;
        }

        /// <summary>
        /// a method that is executed by pressing the button operation
        /// </summary>
        /// <param name="operation"></param>
        public void OperationButtonPress(string operation)
        {
            try
            {
                if (FirstOperand == string.Empty || LastOperation == "=")
                {
                    FirstOperand = display;
                    LastOperation = operation;
                }
                else
                {
                    SecondOperand = display;
                    Operation = lastOperation;
                    calculation.CalculateResult();

                    FullExpression = Math.Round(Convert.ToDouble(FirstOperand), 10) + " " + Operation + " "
                                    + Math.Round(Convert.ToDouble(SecondOperand), 10) + " = "
                                    + Math.Round(Convert.ToDouble(Result), 10);

                    LastOperation = operation;
                    Display = Result;
                    FirstOperand = display;
                    
                }
            }
            catch (Exception)
            {
                Display = Result == string.Empty ? "Error - see event log" : Result;
                // reset values
                this.FirstOperand = string.Empty;
                this.SecondOperand = string.Empty;
                this.Operation = string.Empty;
                this.lastOperation = string.Empty;
                this.fullExpression = string.Empty;
                FullExpression = "";
            }
            newDisplayRequired = true;
        }
    }
}
