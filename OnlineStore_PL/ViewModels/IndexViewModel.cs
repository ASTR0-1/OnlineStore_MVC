using OnlineStore_DAL.Models;
using System.Collections.Generic;

namespace OnlineStore_PL.ViewModels
{
    public class IndexViewModel<T> where T : class
    {
        public List<T> Values{ get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
