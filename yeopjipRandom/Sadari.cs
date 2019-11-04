using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yeopjipRandom
{
    class Sadari
    {
        public List<string> sadariList { set; get; }
        public int memberNum { set; get; }
        public Sadari(List<string> sadariList, int memberNum)
        {
            this.sadariList = sadariList;
            this.memberNum = memberNum;
        }

        public List<Member> getTeams()
        {
            Random random = new Random();
            List<Member> teams = new List<Member>();
            int teamNum = sadariList.Count() / memberNum;
            if (sadariList.Count() % memberNum != 0) teamNum = sadariList.Count() / memberNum + 1;
            int[] teamMemberCount = new int[teamNum];
            for(int i = 0; i < teamMemberCount.Length; i++)
            {
                teamMemberCount[i] = 0;
            }
            foreach (string name in sadariList)
            {
                int tmp = 0;
                do
                {
                    tmp = random.Next(1, teamNum + 1);
                } while (teamMemberCount[tmp - 1] >= memberNum);
                teamMemberCount[tmp - 1]++;
                teams.Add(new Member(name, tmp));
            }
            return teams;
        }
    }
}
