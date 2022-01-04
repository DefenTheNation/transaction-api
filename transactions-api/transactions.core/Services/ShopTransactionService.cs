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

        public IEnumerable<ShopTransaction> GetAllTransactions()
        {
            return _unitOfWork.TransactionRepository.GetAll();
        }

        public ShopTransaction? GetTransaction(int id)
        {
            return _unitOfWork.TransactionRepository.Get(id);
        }

        public int CreateTransaction(ShopTransaction transaction)
        {
            if(transaction.Amount == 0)
            {
                throw new ArgumentException("Invalid transaction amount: cannot have an amount equal to 0", nameof(transaction));
            }

            int transactionId = _unitOfWork.TransactionRepository.Create(transaction);
            _unitOfWork.Save();

            return transactionId;
        }

        public void UpdateTransaction(ShopTransaction transaction)
        {
            _unitOfWork.TransactionRepository.Update(transaction);
            _unitOfWork.Save();
        }

        public void CancelTransaction(int id)
        {
            var transaction = _unitOfWork.TransactionRepository.Get(id);

            if (transaction == null)
                throw new ArgumentException("Transaction with Id " + id + " not found!", nameof(id));

            transaction.Status = ShopTransactionStatusType.Cancelled;

            _unitOfWork.TransactionRepository.Update(transaction);
            _unitOfWork.Save();
        }
    }
}
