using System;

namespace OnlineStore_PL.ViewModels
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalsPages { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalsPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalsPages);
            }
        }
    }
}
