using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {

        //Using Singleton Pattern
        private static ProductDAO instance = null;

        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {

            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        //--------------------------------------------------------------//
        public IEnumerable<TblProduct> GetProducts()
        {
            var mem = new List<TblProduct>();
            try
            {
                using var context = new SalesManagementContext();
                mem =  context.TblProducts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }
        //--------------------------------------------------------------//
        public TblProduct GetProductByID(int id)
        {
            TblProduct pro = null;
            try
            {
                using var context = new SalesManagementContext();
                pro = context.TblProducts.SingleOrDefault(c => c.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pro;
        }
        //--------------------------------------------------------------//
        public void AddNew(TblProduct pro)
        {
            try
            {
                TblProduct product = GetProductByID(pro.ProductId);
                if (product == null)
                {
                    using var context = new SalesManagementContext();
                    context.TblProducts.Add(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The product is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //--------------------------------------------------------------//
        public void Update(TblProduct pro)
        {
            try
            {
                TblProduct product = GetProductByID(pro.ProductId);
                if (product != null)
                {
                    using var context = new SalesManagementContext();
                    context.TblProducts.Update(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //--------------------------------------------------------------//
        public void Remove(int memId)
        {
            try
            {
                TblProduct product = GetProductByID(memId);
                if (product != null)
                {
                    using var context = new SalesManagementContext();
                    context.TblProducts.Remove(product);
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TblProduct> SearchProduct(string productName, decimal unitPrice)
        {
            try
            {
                List<TblProduct> list = new List<TblProduct>();
                list = list.Where(a => a.ProductName.Contains(productName) || a.UnitPrice.Equals(unitPrice)).ToList();
                if (list.Count == 0)
                {
                    return null;
                }
                else
                {
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
