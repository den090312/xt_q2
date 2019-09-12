using Common;
using Entities;
using InterfacesBLL;
using PL;
using System;
using System.Collections.Generic;

namespace Pl
{
    internal class OutputPl
    {
        internal IUserLogic userBll;
        internal IAwardLogic awardBll;
        internal IUserAwardLogic userAwardBll;

        public void Run()
        {
            SetBll();
            new InputPl().Run();
        }

        private void SetBll()
        {
            var dr = new DependencyResolver();

            userBll = dr.UserBll;
            awardBll = dr.AwardBll;
            userAwardBll = dr.UserAwardBll;
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
            var awards = userAwardBll.GetAwardsByUser(user);
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
            var awardName = awardBll.GetAwardByGuid(awardGuid)?.Title;

            if (awardName == string.Empty)
            {
                throw new Exception($"Award '{awardGuid.ToString()}' not found!");
            }

            return awardName;
        }

        internal string GetUserNameByGuid(Guid userGuid)
        {
            var userName = userBll.GetUserByGuid(userGuid)?.Name;

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

            var chosenNum = inputPl.GetKeyFromConsole(inputPl.GetKeyArray(userNum));

            return userNumList[chosenNum];
        }

        private void PrintUsers(out int userNum, out Dictionary<int, Guid> userNumList)
        {
            var users = userBll.GetAll();
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
            var awards = awardBll.GetAll();
            var awardNum = 1;
            var awardNumList = new Dictionary<int, Guid>();

            foreach (var award in awards)
            {
                awardNumList.Add(awardNum, award.AwardGuid);
                PrintAward(award, awardNum);
                awardNum++;
            }

            var inputPl = new InputPl();

            var chosenNum = inputPl.GetKeyFromConsole(inputPl.GetKeyArray(awardNum));

            return awardNumList[chosenNum];
        }

        internal User CreateUser(string dateFormat)
        {
            var inputPl = new InputPl();

            return userBll.CreateUser(inputPl.GetUserString("name"), inputPl.GetUserDate(dateFormat));
        }

        internal void RemoveUser()
        {

        }

        internal Award CreateAward()
        {
            var inputPl = new InputPl();

            return awardBll.CreateAward(inputPl.GetUserString("title"));
        }

        internal bool UserAdded(User user) => userBll.UserAdded(user);

        internal bool AwardAdded(Award award) => awardBll.AwardAdded(award);
    }
}