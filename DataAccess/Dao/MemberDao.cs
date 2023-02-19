using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao {
    public class MemberDao {
        private static MemberDao instance;

        public static MemberDao Instance {
            get {
                if (MemberDao.instance == null) {
                    MemberDao.instance = new MemberDao();

                }
                return MemberDao.instance;
            }
            private set => instance = value;
        }

        private MemberDao() {
        }

        public Member? LoginMember(string username,string password) {
            try {
                Member check = DataProvider.Instance.DB.Members
.Where(x => x.Email.TrimEnd().Equals(username) && x.Password.TrimEnd().Equals(password))
.FirstOrDefault();
                if (check != null) {

                    return check;

                }

                return null;
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return null;
            }
        }

        public bool AddMember(Member newMember) {

            try {

                DataProvider.Instance.DB.Members.Add(newMember);
                DataProvider.Instance.SaveChange();


                return true;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return false;
            }


        }

        public int getMaxMemberId() {

            try {
                int maxId = DataProvider.Instance.DB.Members.Max(x => x.MemberId);

                return maxId;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return -1;
            }


        }

        public bool UpdateMember(Member updateMember) {

            try {

                var currentMember = DataProvider.Instance.DB.Members
                    .Where(x => x.MemberId.Equals(updateMember.MemberId)).FirstOrDefault();

                if (currentMember != null) {
                    currentMember = updateMember;
                    DataProvider.Instance.DB.Members.Update(currentMember);
                    DataProvider.Instance.SaveChange();

                    return true;
                }

                return false;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return false;
            }
        }

        public bool RemoveMember(Member removeMember) {

            try {

                var currentMember = DataProvider.Instance.DB.Members
                    .Where(x => x.MemberId.Equals(removeMember.MemberId)).FirstOrDefault();

                if (currentMember != null) {

                    DataProvider.Instance.DB.Members.Remove(removeMember);
                    DataProvider.Instance.SaveChange();

                    return true;
                }

                return false;

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return false;
            }
        }

        public List<Member> LoadAllMembers() {

            try {

                return DataProvider.Instance.DB.Members.ToList();

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                return new List<Member>();
            }
        }


    }
}
