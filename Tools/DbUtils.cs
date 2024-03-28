using Nikitin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nikitin.Tools
{
    internal static class DbUtils
    {
        public static Db db;

        static DbUtils()
        {
            try
            {
                db = new Db();
            }
            catch(Exception)
            {

            }
        }

        public static class Roles
        {
            public static int Admin = 2;

            public static int Driver = 3;

            public static int Operator = 4;

            public static int Director = 5;
        }

        public static List<T> GetTableAllValues<T>() where T : class
        {
            return db.Set<T>().ToList();
        }

        public static List<T> GetSearchingValues<T>(string searchText) where T : class
        {
            return db.Set<T>().ToList().Where(p => p.ToString().ToLower().Contains(searchText.ToLower())).ToList();
        }

        public static void AddData<T>(T values) where T : class => db.Set<T>().Add(values);

        public static void SaveChanges() => db.SaveChanges();
    }
}
