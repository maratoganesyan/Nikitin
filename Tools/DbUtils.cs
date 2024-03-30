using Geocoding.Microsoft;
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

        public static class CarStatuses
        {
            public static int Working = 1;

            public static int Remont = 2;

            public static int Free = 3;
        }

        public static class RequestStatuses
        {
            public static int InProcess = 1;

            public static int Done = 2;

            public static int InPlan = 3;
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

        public static async Task<string> GetAddress(double latitude, double longitude)
        {
            BingMapsGeocoder geocoder = new BingMapsGeocoder("An0KvRLGMUMOfUqeHxq3oipPsb61_0OXP75rO2N5Bj_kDDTXMc2Y-9AEQSK6iaHm");
            geocoder.Culture = "ru";
            var addresses = await geocoder.ReverseGeocodeAsync(latitude, longitude);
            var firstAddress = addresses?.FirstOrDefault();

            if (firstAddress != null)
            {
                return firstAddress.AddressLine;
            }
            else
            {
                // Обработка ошибки, если адрес не найден
                return "Адрес не найден";
            }
        }
    }
}
