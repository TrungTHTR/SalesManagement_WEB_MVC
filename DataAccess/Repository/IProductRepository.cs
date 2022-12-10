using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<TblProduct> GetProducts();
        TblProduct GetMemberByID(int id);
        void InsertProduct(TblProduct member);
        void DeleteMember(int id);
        void UpdateProduct(TblProduct member);

        IEnumerable<TblProduct> SearchProduct(string productName, decimal unitPrice);

    }
}
