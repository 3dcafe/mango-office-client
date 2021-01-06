using System;
using System.Collections.Generic;
using System.Linq;
using MangoOfficeClient;
using MangoOfficeClient.Extensions;

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
        static string vpbx_api_key = "*";
        /// <summary>
        /// Key to create signatur
        /// </summary>
        /// <summary xml:lang="ru">
        /// Ключ для создания подписи
        /// </summary>
        static string vpbx_api_salt = "*";


        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //1. Check generate token
            string token = MangoOfficeClient.Extensions.MangoSignHelper.GetSign(vpbx_api_key, string.Empty, vpbx_api_salt);

            MangoClient client = new MangoClient(vpbx_api_key, vpbx_api_salt);
            //Test get balance
            var balance = await client.GetBalance();
            var users = await client.GetAllUsers();
            var idCall = await client.GetStatsId(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), "41");
            var user = users.Where(x => x.general.name == "*").ToList();
            var calls = await client.GetStatResult(idCall,7);
            var lastcalls = calls.OrderByDescending(x => x.finish).ToList();
            var getDialogs = await client.GetRecordingTranscripts("*");
            var audioLink = await client.GetRecordAudio("*", "C:\\ffmpeg\\");


            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
        //"MToxMDAxNTE0OToxMDUzMTI4NTA0Mjow]"
        //https://tula.mango-office.ru/support/integratsiya-api/spisok_integratsiy/emulyator_api_virtualnoy_ats/?PATH=%2Fintegratsiya-api%2Fspisok_integratsiy%2Femulyator_api_virtualnoy_ats#/
        //https://www.mango-office.ru/upload/api/MangoOffice_VPBX_API_v1.9.pdf
    }
}
