using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ivr.Services
{
    public static class Constants
    {
        public const string DatabaseFilename = "ItemsSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        //public const string FirebaseSecret = "tSzYI4BWbe30r5b0uON5eKXUKxi5xGB9AKkgb5ST";
        public const string FirebasePath = "https://time-84b46-default-rtdb.europe-west1.firebasedatabase.app/";
        public const string FirebaseApiKey = "AIzaSyBZrl2YyJ_it4wH6IspuL - 7vmyMyePB1j4";
        public const int Deadline = 0;
        public const int Event = 1;
        public const int SmallTask = 2;
        public const int MediumTask = 3;
        public const int BigTask = 4;
    }
}
