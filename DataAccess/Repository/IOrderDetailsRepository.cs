using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailsRepository
    {
        TblOrderDetail GetOrderDetailByID(int id);
        void InsertOrdersDetails(TblOrderDetail order);
        void DeleteOrdersDetails(int orderId);
        void UpdateOrdersDetails(TblOrderDetail order);
    }
}
