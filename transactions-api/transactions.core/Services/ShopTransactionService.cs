using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transactions.core.Model;
using transactions.core.Repository;

namespace transactions.core.Services
{
    public class ShopTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopTransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ShopTransaction GetTransaction(int id)
        {
            return _unitOfWork.TransactionRepository.Get(id);
        }

        public void CreateTransaction(ShopTransaction transaction)
        {
            if(transaction.Amount == 0)
            {
                throw new ArgumentException("Invalid transaction amount: cannot have an amount equal to 0", nameof(transaction));
            }

            transaction.Id = _unitOfWork.TransactionRepository.Create(transaction);
            _unitOfWork.Save();
        }

        public void UpdateTransaction(ShopTransaction transaction)
        {
            _unitOfWork.TransactionRepository.Update(transaction);
            _unitOfWork.Save();
        }
    }
}
