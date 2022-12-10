using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteMember(int id) => ProductDAO.Instance.Remove(id);

        public TblProduct GetMemberByID(int id)=> ProductDAO.Instance.GetProductByID(id);

        public IEnumerable<TblProduct> GetProducts() => ProductDAO.Instance.GetProducts();

        public void InsertProduct(TblProduct product) => ProductDAO.Instance.AddNew(product);

        public IEnumerable<TblProduct> SearchProduct(string productName, decimal unitPrice)=> ProductDAO.Instance.SearchProduct(productName, unitPrice);

        public void UpdateProduct(TblProduct member)=> ProductDAO.Instance.Update(member);
    }
}
