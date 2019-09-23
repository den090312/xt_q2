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
            var awards = DependencyResolver.UserAwardLogic?.GetAwardsByUserGuid(user.Guid);
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
            var awardName = DependencyResolver.AwardLogic?.GetByGuid(awardGuid)?.Title;

            if (awardName == string.Empty)
            {
                throw new Exception($"Award '{awardGuid.ToString()}' not found!");
            }

            return awardName;
        }

        internal string GetUserNameByGuid(Guid userGuid)
        {
            var userName = DependencyResolver.UserLogic?.GetByGuid(userGuid)?.Name;

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
            var users = DependencyResolver.UserLogic?.GetAll();
            userNum = 0;
            userNumList = new Dictionary<int, Guid>();

            foreach (var user in users)
            {
                userNum++;
                userNumList.Add(userNum, user.Guid);
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
            var awards = DependencyResolver.AwardLogic?.GetAll();
            awardNum = 0;
            awardNumList = new Dictionary<int, Guid>();

            foreach (var award in awards)
            {
                awardNum++;
                awardNumList.Add(awardNum, award.Guid);
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

            return DependencyResolver.UserLogic?.Create(name, dateBirth);
        }

        internal void RemoveUser() => RunUserRemove(GetChosenUserGuid());

        private void RunUserRemove(Guid userGuid)
        {
            Console.WriteLine();

            if (DependencyResolver.UserAwardLogic.RemoveUserAwards(userGuid))
            {
                Console.WriteLine($"---user '{userGuid}' deleted---");
            }
            else
            {
                Console.WriteLine($"---user '{userGuid}' NOT deleted---");
            }
        }

        internal void RemoveAward() => RunAwardRemove(GetChosenAwardGuid());

        private void RunAwardRemove(Guid awardGuid)
        {
            Console.WriteLine();

            if (DependencyResolver.UserAwardLogic.RemoveAwardUsers(awardGuid))
            {
                Console.WriteLine($"---user '{awardGuid}' deleted---");
            }
            else
            {
                Console.WriteLine($"---user '{awardGuid}' NOT deleted---");
            }
        }

        internal Award CreateAward() => DependencyResolver.AwardLogic?.Create(new InputPl()?.GetUserString("title"));

        internal bool UserAdded(User user) => DependencyResolver.UserLogic.Add(user);

        internal bool AwardAdded(Award award) => DependencyResolver.AwardLogic.Add(award);
    }
}