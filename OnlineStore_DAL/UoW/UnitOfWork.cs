using OnlineStore_DAL.Interfaces;
using OnlineStore_DAL.Models;
using OnlineStore_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore_DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationContext;

        private CategoryRepository _categoryRepository;
        private ImageRepository _imageRepository;
        private ProductRepository _productRepository;
        private ReceiptRepository _receiptRepository;
        private ShoppingCartRepository _shoppingCartRepository;
        private WishListRepository _wishListRepository;

        public UnitOfWork(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public CategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_applicationContext);

                return _categoryRepository;
            }
        }

        public ImageRepository ImageRepository
        {
            get
            {
                if (_imageRepository == null)
                    _imageRepository = new ImageRepository(_applicationContext);

                return _imageRepository;
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_applicationContext);

                return _productRepository;
            }
        }

        public ReceiptRepository ReceiptRepository
        {
            get
            {
                if (_receiptRepository == null)
                    _receiptRepository = new ReceiptRepository(_applicationContext);

                return _receiptRepository;
            }
        }

        public ShoppingCartRepository ShoppingCartRepository
        {
            get
            {
                if (_shoppingCartRepository == null)
                    _shoppingCartRepository = new ShoppingCartRepository(_applicationContext);

                return _shoppingCartRepository;
            }
        }

        public WishListRepository WishListRepository
        {
            get
            {
                if (_wishListRepository == null)
                    _wishListRepository = new WishListRepository(_applicationContext);

                return _wishListRepository;
            }
        }
    }
}
