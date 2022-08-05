using OnlineStore_BLL.Interfaces;
using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore_BLL.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReceiptService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(Receipt entity)
        {
            if (!(await _unitOfWork.ReceiptRepository.GetAllAsync()).Contains(entity))
                throw new ArgumentException("There is no such receipt");

            await _unitOfWork.ProductRepository.DeleteAsync(entity.Id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (await _unitOfWork.ReceiptRepository.GetAsync(id) == null)
                throw new ArgumentException($"There is no receipt with such id \"{id}\"");

            await _unitOfWork.ProductRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            var receiptList = await _unitOfWork.ReceiptRepository.GetAllAsync();

            if (receiptList == null)
                throw new ArgumentException("There is no receipts in database");

            return receiptList;
        }

        public async Task<IEnumerable<Receipt>> GetAllUserReceiptsAsync(User entity)
        {
            var receiptList = await _unitOfWork.ReceiptRepository.GetAllAsync();
            var userReceiptList = receiptList.Where(receipt => receipt.Id == entity.Id).ToList();

            return userReceiptList;
        }

        public async Task<Receipt> GetByIdAsync(int id)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetAsync(id);

            if (receipt == null)
                throw new ArgumentException($"There is no receipt with such id \"{id}\"");

            return receipt;
        }
    }
}