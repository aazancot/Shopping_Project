using PL.Purchases_Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BE;

namespace PL.Commands
{
    class DeleteProductOrderCommand : ICommand
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
            CurrentVM.DeleteProductOrder(po);     
        }

        public PurchasesValidationViewModel CurrentVM { get; set; }

        public DeleteProductOrderCommand(PurchasesValidationViewModel currentVM)
        {
            CurrentVM = currentVM;
        }


    }
}





