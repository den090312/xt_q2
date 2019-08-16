﻿using _61_62_USERS_AND_AWARDS.Interfaces;
using _61_62_USERS_AND_AWARDS.Entities;
using _61_62_USERS_AND_AWARDS.Common;
using System;

namespace _61_62_USERS_AND_AWARDS.BLL
{
    public static class StorageManager
    {
        public static IStorable StorageImplementation { get; }

        static StorageManager() => StorageImplementation = Dependencies.StorageImplementation;

        public static void CreateStorage() => StorageImplementation.CreateStorage();

        public static void PrintStoragePaths() => StorageImplementation.PrintStoragePaths();

        public static void AddUser(User user) => StorageImplementation.AddUser(user);

        public static void RemoveUser(string name) => StorageImplementation.RemoveUser(name);

        public static void PrintAllUsers() => StorageImplementation.PrintAllUsers();

        public static void AddAward(Award award) => StorageImplementation.AddAward(award);

        public static void WriteMenu()
        {
            Console.WriteLine("Users operations:");
            Console.WriteLine("\t1: create");
            Console.WriteLine("\t2: delete");
            Console.WriteLine("\t3: print");

            Console.WriteLine("Awards operations:");
            Console.WriteLine("\t4: create");
            Console.WriteLine("\t5: delete");
            Console.WriteLine("\t6: print");

            Console.WriteLine("\t7: exit");
        }
    }
}
