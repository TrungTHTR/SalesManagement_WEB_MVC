using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<TblOrder> GetOrders();
        TblOrder GetOrderByID(int id);
        void InsertOrders(TblOrder order);
        void DeleteOrders(int orderId);
        void UpdateOrders(TblOrder order);
        IEnumerable<TblOrder> Report(DateTime startDate, DateTime endDate);

    }
}
