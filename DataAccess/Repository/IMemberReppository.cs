using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObject.Models;
namespace DataAccess.Repository
{
    public interface IMemberReppository
    {
        IEnumerable<TblMember> GetMembers();
        TblMember GetMemberByID(int id);
        TblMember GetMemberByEmail(string email);
        void InsertMember(TblMember member);
        void DeleteMember(int id);
        void UpdateMember(TblMember member);
        bool checkLogin(string email, string password);


    }
}
