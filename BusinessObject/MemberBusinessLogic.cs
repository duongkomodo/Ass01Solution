using DataAccess.Dao;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject {

    public interface IMemberBusiness {
        bool Login(string email,string password);

        List<Member> LoadAllMembers();

        bool AddMember(Member member);
        bool UpdateMember(Member member);

        bool RemoveMember(Member member);
    }
    public class MemberBusinessLogic : IMemberBusiness {
        public bool AddMember(Member member) {

            member.MemberId = MemberDao.Instance.getMaxMemberId() + 1;

         return MemberDao.Instance.AddMember(member);
        }

        public List<Member> LoadAllMembers() {
            return MemberDao.Instance.LoadAllMembers();

        }

        public bool Login(string email, string password) {



                if (MemberDao.Instance.LoginMember(email,password) != null) {
                    return true;
                }
                return false;


        }

        public bool RemoveMember(Member member) {
            return MemberDao.Instance.RemoveMember(member);
        }

        public bool UpdateMember(Member member) {
            return MemberDao.Instance.UpdateMember(member);
        }
    }
}
