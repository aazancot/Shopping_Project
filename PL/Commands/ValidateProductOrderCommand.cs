using BE;
using PL.Purchases_Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
    class ValidateProductOrderCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ProductOrder po = parameter as ProductOrder;
            CurrentVM.ValidateProductOrder(po);
        }

        public PurchasesValidationViewModel CurrentVM { get; set; }

        public ValidateProductOrderCommand(PurchasesValidationViewModel currentVM)
        {
            CurrentVM = currentVM;
        }
    }
}
