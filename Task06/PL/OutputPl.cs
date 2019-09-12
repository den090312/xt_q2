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
            var userNum = 1;

            foreach (var user in users)
            {
                PrintSingleUser(user, userNum);
                userNum++;

                PrintAwardsByUser(user);
            }
        }

        private void PrintSingleUser(User user, int userNum)
        {
            Console.WriteLine($"{userNum}.{user.Name}---{user.DateOfBirth}---{user.Age}");
        }

        private void PrintAwardsByUser(User user)
        {
            var awards = new DependencyResolver()?.UserAwardBll?.GetAwardsByUser(user);
            var awardNum = 1;

            foreach (var award in awards)
            {
                PrintAward(award, awardNum);
                awardNum++;
            }
        }

        private void PrintAward(Award award, int awardNum)
        {
            Console.WriteLine($"---{awardNum}.{award.Title}");
        }

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
            PrintUsers(out int userNum, out Dictionary<int, Guid> userNumList);

            var inputPl = new InputPl();

            var chosenNum = inputPl.GetKeyFromConsole(userNum);

            return userNumList[chosenNum];
        }

        private void PrintUsers(out int userNum, out Dictionary<int, Guid> userNumList)
        {
            var users = new DependencyResolver()?.UserBll?.GetAll();
            userNum = 1;
            userNumList = new Dictionary<int, Guid>();

            foreach (var user in users)
            {
                userNumList.Add(userNum, user.UserGuid);
                PrintSingleUser(user, userNum);
                userNum++;
            }
        }

        internal Guid GetChosenAwardGuid()
        {
            var awards = new DependencyResolver()?.AwardBll?.GetAll();
            var awardNum = 1;
            var awardNumList = new Dictionary<int, Guid>();

            foreach (var award in awards)
            {
                awardNumList.Add(awardNum, award.AwardGuid);
                PrintAward(award, awardNum);
                awardNum++;
            }

            var inputPl = new InputPl();

            var chosenNum = inputPl.GetKeyFromConsole(awardNum);

            return awardNumList[chosenNum];
        }

        internal User CreateUser(string dateFormat)
        {
            var inputPl = new InputPl();

            return new DependencyResolver()?.UserBll?.CreateUser(inputPl.GetUserString("name"), inputPl.GetUserDate(dateFormat));
        }

        internal void RemoveUser()
        {
            var dr = new DependencyResolver();

            Console.WriteLine("Choose user by number:");
            Console.WriteLine("----------------------");

            var userGuid = GetChosenUserGuid();
            Console.WriteLine();

            if (dr.UserBll.UserRemoved(userGuid) && dr.UserAwardBll.UserRemoved(userGuid))
            {
                Console.WriteLine($"---user '{userGuid}' deleted---");
            }
            else
            {
                Console.WriteLine($"---user '{userGuid}' NOT deleted---");
            }
        }

        internal void RemoveAward()
        {
            throw new NotImplementedException();
        }

        internal void PrintAwards()
        {
            throw new NotImplementedException();
        }

        internal Award CreateAward()
        {
            var inputPl = new InputPl();

            return new DependencyResolver()?.AwardBll?.CreateAward(inputPl.GetUserString("title"));
        }

        internal bool UserAdded(User user) => new DependencyResolver().UserBll.UserAdded(user);

        internal bool AwardAdded(Award award) => new DependencyResolver().AwardBll.AwardAdded(award);
    }
}