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
    }
}
