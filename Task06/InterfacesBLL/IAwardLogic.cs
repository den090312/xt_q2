using Entities;

namespace InterfacesBLL
{
    public interface IAwardLogic
    {
        Award CreateAward(string title);

        void AddAward(Award award);

        void RemoveAwards(string title);

        void PrintAwards();

        void EraseAward(string awardID);
    }
}
