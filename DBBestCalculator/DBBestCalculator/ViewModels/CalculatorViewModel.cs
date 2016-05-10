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
        private string display;
        private string fullExpression;

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
            set { display = value; OnPropertyChanged("Display"); }
        }
        #endregion

        public CalculatorViewModel()
        {
            this.calculation = new Calculator();
            this.Display = "0";
            this.LastOperation = string.Empty;
            this.FullExpression = string.Empty;
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
        /// a method that is executed by pressing the button operation
        /// </summary>
        /// <param name="operation"></param>
        public void OperationButtonPress(string operation)
        {
            calculation.OperationButtonPress(operation, ref display, ref lastOperation, ref fullExpression);
            Display = display;
            FullExpression = fullExpression;
        }

        public void DigitButtonPress(string operation)
        {
            calculation.DigitButtonPress(operation, ref display, ref lastOperation, ref fullExpression);
            Display = display;
            FullExpression = fullExpression;
        }
    }
}
