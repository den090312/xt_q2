using Common;
using Entities;
using PL;
using System;
using System.Collections.Generic;

namespace Pl
{
    internal class OutputPl
    {
        public void Run()
        {
            new InputPl().Run();
        }

        internal void PrintUserAwards(IEnumerable<User> users)
        {
            var userAwardNum = 0;

            foreach (var user in users)
            {
                userAwardNum++;
                PrintSingleUser(user, userAwardNum);
                PrintAwardsByUser(user);
            }

            if (userAwardNum == 0)
            {
                Console.WriteLine("---No user-awards to print---");
            }
        }

        private void PrintSingleUser(User user, int userNum) => 
            Console.WriteLine($"{userNum}.{user.Name}---{user.DateOfBirth.ToString("dd.MM.yyyy")}---{user.Age}");

        private void PrintAwardsByUser(User user)
        {
            var awards = new DependencyResolver()?.UserAwardBll?.GetAwardsByUser(user);
            var awardNum = 1;

            foreach (var award in awards)
            {
                PrintSingleAward(award, awardNum);
                awardNum++;
            }
        }

        private void PrintSingleAward(Award award, int awardNum) => Console.WriteLine($"---{awardNum}.{award.Title}");

        internal string GetAwardNameByGuid(Guid awardGuid)
        {
            var awardName = new DependencyResolver()?.AwardBll?.GetAwardByGuid(awardGuid)?.Title;

            if (awardName == string.Empty)
            {
                throw new Exception($"Award '{awardGuid.ToString()}' not found!");
            }

            return awardName;
        }

        internal string GetUserNameByGuid(Guid userGuid)
        {
            var userName = new DependencyResolver()?.UserBll?.GetUserByGuid(userGuid)?.Name;

            if (userName == string.Empty)
            {
                throw new Exception($"User '{userGuid.ToString()}' not found!");
            }

            return userName;
        }

        internal Guid GetChosenUserGuid()
        {
            PrintUsersToChoose(out int userNum, out Dictionary<int, Guid> userNumList);

            var chosenNum = new InputPl().GetKeyFromConsole(userNum);

            return userNumList[chosenNum];
        }

        private void PrintUsersToChoose(out int userNum, out Dictionary<int, Guid> userNumList)
        {
            var users = new DependencyResolver()?.UserBll?.GetAll();
            userNum = 0;
            userNumList = new Dictionary<int, Guid>();

            foreach (var user in users)
            {
                userNum++;
                userNumList.Add(userNum, user.UserGuid);
                PrintSingleUser(user, userNum);
            }

            if (userNum == 0)
            {
                Console.WriteLine("---No users to choose---");
            }
        }

        internal Guid GetChosenAwardGuid()
        {
            PrintAwardsToChoose(out int awardNum, out Dictionary<int, Guid> awardNumList);

            var chosenNum = new InputPl().GetKeyFromConsole(awardNum);

            return awardNumList[chosenNum];
        }

        internal void PrintAwardsToChoose(out int awardNum, out Dictionary<int, Guid> awardNumList)
        {
            var awards = new DependencyResolver()?.AwardBll?.GetAll();
            awardNum = 0;
            awardNumList = new Dictionary<int, Guid>();

            foreach (var award in awards)
            {
                awardNum++;
                awardNumList.Add(awardNum, award.AwardGuid);
                PrintSingleAward(award, awardNum);
            }

            if (awardNum == 0)
            {
                Console.WriteLine("---No awards to choose---");
            }
        }

        internal void PrintAwards(IEnumerable<Award> awards)
        {
            var awardNum = 1;

            foreach (var award in awards)
            {
                PrintSingleAward(award, awardNum);
                awardNum++;
            }
        }

        internal User CreateUser(string dateFormat)
        {
            var inputPl = new InputPl();

            var name = inputPl?.GetUserString("name");
            var dateBirth = inputPl.GetUserDate(dateFormat);

            return new DependencyResolver()?.UserBll?.CreateUser(name, dateBirth);
        }

        internal void RemoveUser() => RunUserRemove(new DependencyResolver(), GetChosenUserGuid());

        private void RunUserRemove(DependencyResolver dr, Guid userGuid)
        {
            Console.WriteLine();

            if (dr.UserBll.UserRemoved(userGuid))
            {
                Console.WriteLine($"---user '{userGuid}' deleted from UserBll---");
            }
            else
            {
                Console.WriteLine($"---user '{userGuid}' NOT deleted from UserBll---");
            }

            if (dr.UserAwardBll.UserRemoved(userGuid))
            {
                Console.WriteLine($"---user '{userGuid}' deleted from UserAwardBll---");
            }
            else
            {
                Console.WriteLine($"---user '{userGuid}' NOT deleted from UserAwardBll---");
            }
        }

        internal void RemoveAward() => RunAwardRemove(new DependencyResolver(), GetChosenAwardGuid());

        private void RunAwardRemove(DependencyResolver dr, Guid awardGuid)
        {
            Console.WriteLine();

            if (dr.AwardBll.AwardRemoved(awardGuid))
            {
                Console.WriteLine($"---user '{awardGuid}' deleted from UserBll---");
            }
            else
            {
                Console.WriteLine($"---user '{awardGuid}' NOT deleted from UserBll---");
            }

            if (dr.UserAwardBll.AwardRemoved(awardGuid))
            {
                Console.WriteLine($"---user '{awardGuid}' deleted from UserAwardBll---");
            }
            else
            {
                Console.WriteLine($"---user '{awardGuid}' NOT deleted from UserAwardBll---");
            }
        }

        internal Award CreateAward() => new DependencyResolver()?.AwardBll?.CreateAward(new InputPl()?.GetUserString("title"));

        internal bool UserAdded(User user) => new DependencyResolver().UserBll.UserAdded(user);

        internal bool AwardAdded(Award award) => new DependencyResolver().AwardBll.AwardAdded(award);
    }
}