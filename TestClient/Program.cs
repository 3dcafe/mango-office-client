using System;
using MangoOfficeClient;

namespace TestClient
{
    class Program
    {
        /// <summary>
        /// Unique code of your PBX
        /// </summary>
        /// <summary xml:lang="ru">
        /// Уникальный код вашей АТС
        /// </summary>
        static string vpbx_api_key = "demo";
        /// <summary>
        /// Key to create signatur
        /// </summary>
        /// <summary xml:lang="ru">
        /// Ключ для создания подписи
        /// </summary>
        static string vpbx_api_salt = "demo";

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //1. Check generate token
            string token = MangoOfficeClient.Extensions.MangoSignHelper.GetSign(vpbx_api_key, string.Empty, vpbx_api_salt);

            MangoClient client = new MangoClient(vpbx_api_key, vpbx_api_salt);
            //Test get balance
            var balance = await client.GetBalance();
            var users = await client.GetAllUsers();
            var idCall = await client.GetStatsId(DateTime.Now.AddDays(-7), DateTime.Now, "018");
            Console.WriteLine("Hello World!");
        }


        //https://www.mango-office.ru/upload/api/MangoOffice_VPBX_API_v1.9.pdf
    }
}
