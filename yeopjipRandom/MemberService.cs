using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yeopjipRandom
{
    public class MemberService
    {
        private static MemberService instance = new MemberService();
        public static MemberService GetInstance()
        {
            return instance;
        }
        public List<string> LoadAll()
        {
            try
            {
                return LoadMembers(Constants.MEMBER_LIST_PATH);
            }
            catch (FileNotFoundException fe)
            {
                List<string> memberList = new List<string>();
                string[] members = { "장한얼", "신원희", "윤혜진", "이태식", "김재원", "김진태", "박대성", "유창곤", "윤주성", "권도균", "김규영", "김대식", "김보형", "김승덕", "김용현",
                "김용호", "김정대", "김희연", "박가은", "방승호", "신치호", "양만재", "오민정", "오정태", "유환수", "윤은비", "이상현", "이수빈", "이인호", "이현수",
                "이호림", "장재혁", "하영택", "허성지", "홍보람", "황유빈"};
                memberList.AddRange(members);
                SaveMembers(memberList, Constants.MEMBER_LIST_PATH);
                return memberList;
            }
        }
        public void SaveAll(List<string> memberList)
        {
            SaveMembers(memberList, Constants.MEMBER_LIST_PATH);
        }

        public List<string> LoadFavorite()
        {
            try
            {
                return LoadMembers(Constants.FAVORITE_LIST_PATH);
            }catch(FileNotFoundException fe)
            {
                return new List<string>();
            }
        }
        public void SaveFavorite(List<string> memberList)
        {
            SaveMembers(memberList, Constants.FAVORITE_LIST_PATH);
        }
        private List<string> LoadMembers(string filePath)
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                string fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<string>>(fileContents);
            }
            catch (FileNotFoundException fe) {
                throw fe;
            }finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        private void SaveMembers(List<string> memberList, string filePath)
        {
            TextWriter writer = null;
            try
            {
                string objectToWriteToJson = JsonConvert.SerializeObject(memberList);
                writer = new StreamWriter(filePath, false);
                writer.Write(objectToWriteToJson);
            }
            catch (FileNotFoundException fe) { }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
