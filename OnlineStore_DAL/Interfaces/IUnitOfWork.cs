using OnlineStore_DAL.Repositories;

namespace OnlineStore_DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public CategoryRepository CategoryRepository { get; }
        public ImageRepository ImageRepository { get; }
        public ProductRepository ProductRepository { get; }
        public ReceiptRepository ReceiptRepository { get; }
        public ShoppingCartRepository ShoppingCartRepository { get; }
        public WishListRepository WishListRepository { get; }
    }
}
