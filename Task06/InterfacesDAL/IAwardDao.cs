using Entities;

namespace InterfacesDAL
{
    public interface IAwardDao
    {
        void AddAward(Award award);

        void RemoveAwards(string title);

        void PrintAwards();

        string[] GetAwardIdArray(string awardName);

        string[] GetAllAwards();
    }
}