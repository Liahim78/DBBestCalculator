using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorModel
{
    public class Calculator
    {
        private string result;

        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
        public string Operation { get; set; }
        public string Result { get { return result; } }


        public bool newDisplayRequired { get; set; }

        public Calculator()
        {
            FirstOperand = string.Empty;
            SecondOperand = string.Empty;
            Operation = string.Empty;
            result = string.Empty;
        }

        public Calculator(string firstOperand, string secondOperand, string operation)
        {
            ValidateOperand(firstOperand);
            ValidateOperand(secondOperand);
            ValidateOperation(operation);

            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Operation = operation;
            result = String.Empty;

        }

        public void CalculateResult()
        {
            ValidateOperand(FirstOperand);
            ValidateOperand(SecondOperand);
            ValidateOperation(Operation);
            try
            {
                switch (Operation)
                {
                    case "+":
                        result = (Convert.ToDouble(FirstOperand) + Convert.ToDouble(SecondOperand)).ToString();
                        break;
                    case "-":
                        result = (Convert.ToDouble(FirstOperand) - Convert.ToDouble(SecondOperand)).ToString();
                        break;
                    case "*":
                        result = (Convert.ToDouble(FirstOperand) * Convert.ToDouble(SecondOperand)).ToString();
                        break;
                    case "/":
                        result = (Convert.ToDouble(FirstOperand) / Convert.ToDouble(SecondOperand)).ToString();
                        break;
                }
            }
            catch (Exception)
            {
                result = "Error whilst calculating";
                throw;
            }
        }

        //operand check
        private void ValidateOperand(string operand)
        {
            try
            {
                Convert.ToDouble(operand);
            }
            catch (Exception)
            {
                result = "Invalid Number: " + operand;
                throw;
            }
        }
        //operation check
        private void ValidateOperation(string operation)
        {
            switch (operation)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                    break;
                default:
                    result = "Unknown operation: " + operation;
                    throw new ArgumentException ("Unknown operation: "+ operation, "operation");
            }
        }
        /// <summary>
        /// a method that is executed by pressing the button
        /// </summary>
        /// <param name="button"></param>
        public void DigitButtonPress(string button, ref string Display, ref string LastOperation, ref string FullExpression)
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
                    if (Display.Length > 1)
                        Display = Display.Substring(0, Display.Length - 1);
                    else Display = "0";
                    break;
                case "+/-":
                    if (Display.Contains("-") || Display == "0")
                    {
                        Display = Display.Remove(Display.IndexOf("-"), 1);
                    }
                    else Display = "-" + Display;
                    break;
                case ",":
                    if (newDisplayRequired)
                    {
                        Display = "0,";
                    }
                    else
                    {
                        if (!Display.Contains(","))
                        {
                            Display = Display + ",";
                        }
                    }
                    break;
                default:
                    if (Display == "0" || newDisplayRequired)
                        Display = button;
                    else
                        Display = Display + button;
                    break;
            }
            newDisplayRequired = false;
        }
        /// <summary>
        /// a method that is executed by pressing the button operation
        /// </summary>
        /// <param name="operation"></param>
        public void OperationButtonPress(string operation, ref string Display, ref string LastOperation, ref string FullExpression)
        {
            try
            {
                if (FirstOperand == string.Empty || LastOperation == "=")
                {
                    FirstOperand = Display;
                    LastOperation = operation;
                }
                else
                {
                    SecondOperand = Display;
                    Operation = LastOperation;
                    CalculateResult();

                    FullExpression = Math.Round(Convert.ToDouble(FirstOperand), 10) + " " + Operation + " "
                                    + Math.Round(Convert.ToDouble(SecondOperand), 10) + " = "
                                    + Math.Round(Convert.ToDouble(Result), 10);

                    LastOperation = operation;
                    Display = Result;
                    FirstOperand = Display;

                }
            }
            catch (Exception)
            {
                Display = Result == string.Empty ? "Error - see event log" : Result;
                // reset values
                this.FirstOperand = string.Empty;
                this.SecondOperand = string.Empty;
                this.Operation = string.Empty;
                LastOperation = string.Empty;
                FullExpression = string.Empty;
                FullExpression = "";
            }
            newDisplayRequired = true;
        }
    }
}
