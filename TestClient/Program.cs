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
        static string vpbx_api_key = "vpbx_api_key";
        /// <summary>
        /// Key to create signatur
        /// </summary>
        /// <summary xml:lang="ru">
        /// Ключ для создания подписи
        /// </summary>
        static string vpbx_api_salt = "vpbx_api_salt";

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //1. Check generate token
            string token = MangoOfficeClient.Extensions.MangoSignHelper.GetSign(vpbx_api_key, string.Empty, vpbx_api_salt);

            MangoClient client = new MangoClient(vpbx_api_key, vpbx_api_salt);
            //Test get balance
            var balance = await client.GetBalance();
            Console.WriteLine("Hello World!");
        }
    }
}
