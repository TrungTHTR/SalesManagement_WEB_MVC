using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberReppository
    {
        public bool checkLogin(string email, string password) => MemberDAO.Instance.CheckLogin(email, password);
        public void DeleteMember(int id) => MemberDAO.Instance.Remove(id);

        public TblMember GetMemberByEmail(string email)=> MemberDAO.Instance.GetMemberByEmail(email);

        public void InsertMember(TblMember member) => MemberDAO.Instance.AddNew(member);

        public void UpdateMember(TblMember member) => MemberDAO.Instance.Update(member);

        TblMember IMemberReppository.GetMemberByID(int id) => MemberDAO.Instance.GetMemberByID(id);

        IEnumerable<TblMember> IMemberReppository.GetMembers() => MemberDAO.Instance.GetMembers();
    }
}
