using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transactions.core.Model;
using transactions.core.Repository;

namespace transactions.core.Services
{
    /// <summary>
    ///     Manages transactions and their lifecycle
    /// </summary>
    public class ShopTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopTransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Get all transactions currently stored in the repository
        /// </summary>
        /// <returns>Enumerable of all transactions</returns>
        public IEnumerable<ShopTransaction> GetAllTransactions()
        {
            return _unitOfWork.TransactionRepository.GetAll();
        }

        /// <summary>
        ///     Get a single transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Transaction if found, null if not found</returns>
        public ShopTransaction? GetTransaction(int id)
        {
            return _unitOfWork.TransactionRepository.Get(id);
        }

        /// <summary>
        ///     Create a new transaction with a nonzero amount
        /// </summary>
        /// <param name="transaction">New transaction</param>
        /// <returns>Transaction id</returns>
        /// <exception cref="ArgumentException">Transaction has zero for the amount</exception>
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

        /// <summary>
        ///     Update an existing transaction
        /// </summary>
        /// <param name="transaction"></param>
        public void UpdateTransaction(ShopTransaction transaction)
        {
            _unitOfWork.TransactionRepository.Update(transaction);
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Update the status of a transaction
        /// </summary>
        /// <param name="id">Transaction id</param>
        /// <param name="newStatus">New status of the transaction</param>
        /// <returns>The updated transaction</returns>
        /// <exception cref="ArgumentException">Transaction not found</exception>
        public ShopTransaction UpdateTransactionStatus(int id, ShopTransactionStatusType newStatus)
        {
            var transaction = _unitOfWork.TransactionRepository.Get(id);

            if(transaction == null)
            {
                throw new ArgumentException("Transaction with Id " + id + " not found!", nameof(id));
            }

            transaction.Status = newStatus;

            _unitOfWork.TransactionRepository.Update(transaction);
            _unitOfWork.Save();

            return transaction;
        }
    }
}
