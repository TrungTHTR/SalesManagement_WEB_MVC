
using System.Collections.Generic;
using System;
using System.Data;
using BussinessObject.Models;
using DataAccess;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess
{
    public class MemberDAO 
    {

        //Using Singleton Pattern
        private static MemberDAO instance = null;

        private static readonly object instanceLock = new object();
        public static MemberDAO Instance
        {

            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        public TblMember readJson()
        {
            IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
            var strConn = config["DefaultUser"];
            TblMember ac = JsonSerializer.Deserialize<TblMember>(strConn); // đọc file Json
            return ac;
        }

        //--------------------------------------------------------------//
        public IEnumerable<TblMember> GetMembers()
        {
            var mem = new List<TblMember>();
            try
            {
                using var context = new SalesManagementContext();
                mem = context.TblMembers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }
        //--------------------------------------------------------------//
        public TblMember GetMemberByID(int id)
        {
            TblMember mem = null;
            try
            {
                using var context = new SalesManagementContext();
                mem = context.TblMembers.SingleOrDefault(c => c.MemberId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }
        //--------------------------------------------------------------//
        public void AddNew(TblMember mem)
        {
            try
            {
                TblMember member = GetMemberByID(mem.MemberId);
                if (member == null)
                {
                    using var context = new SalesManagementContext();
                    context.TblMembers.Add(mem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Member is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TblMember GetMemberByEmail(string email)
        {
            TblMember mem = null;
            try
            {
                using var context = new SalesManagementContext();
                mem = context.TblMembers.SingleOrDefault(c => c.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }
        //--------------------------------------------------------------//
        public void Update(TblMember mem)
        {
            try
            {
                TblMember member = GetMemberByID(mem.MemberId);
                if (member != null)
                {
                    using var context = new SalesManagementContext();
                    context.TblMembers.Update(mem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Member does not already exist.");
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
                TblMember member = GetMemberByID(memId);
                if (member != null)
                {
                    using var context = new SalesManagementContext();
                    context.TblMembers.Remove(member);
                }
                else
                {
                    throw new Exception("The car does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckLogin(string Email, string password)
        {
            TblMember defaultUser = readJson();
            using var context = new SalesManagementContext();
            TblMember member = context.TblMembers.SingleOrDefault(memb => memb.Email == Email && memb.Password == password);
            if (member != null)
            {
                return true;
            }
            else
            {
                if (defaultUser != null)
                {
                    if (Email.Equals(defaultUser.Email) && password.Equals(defaultUser.Password))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        } // end checklogin   


    }//endclass
}
