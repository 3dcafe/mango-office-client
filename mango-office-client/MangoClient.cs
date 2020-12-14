using MangoOfficeClient.Account;
using MangoOfficeClient.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MangoOfficeClient
{
    public class MangoClient : IDisposable
    {
        #region Private property
        /// <summary>
        /// Unique code of your PBX
        /// </summary>
        /// <summary xml:lang="ru">
        /// Уникальный код вашей АТС
        /// </summary>
        private string vpbx_api_key { get; set; }
		/// <summary>
		/// Key to create signatur
		/// </summary>
		/// <summary xml:lang="ru">
		/// Ключ для создания подписи
		/// </summary>
		private string sign { get; set; }
        #endregion

        /// <summary>
        /// Base constructor Mango Client
        /// </summary>
        /// <param name="vpbx_api_key">Unique code of your PBX</param>
        /// <param name="sign"> Key to create signatur</param>
        public MangoClient(string vpbx_api_key, string sign)
        {
			this.vpbx_api_key = vpbx_api_key;
			this.sign = sign;
		}

		public void Dispose()
        {
#warning todo dispose 1
		}
        #region Users
        /// <summary>
        /// Getting the balance of the personal account
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получение баланса лицевого счета.
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<Balance> GetBalance()
		{
			var response = await PerformCommandAsync<Balance>("account/balance");
			return response;
		}
		/// <summary>
		/// Getting all users and informations
		/// </summary>
		/// <summary xml:lang="ru">
		/// Получить всех пользователей и информацию 
		/// </summary>
		/// <returns></returns>
		public async System.Threading.Tasks.Task<List<Users.User>> GetAllUsers()
		{
			var response = await PerformCommandAsync<Users.RootUser>("config/users/request");
			return response.users;
		}
		/// <summary>
		/// Get an identifier to access call statistics
		/// Starting statistics generation
		/// </summary>
		/// <summary xml:lang="ru">
		/// Получить идентификатор для доступа к статистике звонков
		/// Запуск формирования статистики 
		/// </summary>
		/// <returns>Id for stats</returns>
		public async System.Threading.Tasks.Task<Stats.BaseKey> GetStatsId(DateTime start, DateTime finish,string extension)
		{
			var response = await PerformCommandAsync<Stats.BaseKey>("stats/request", new Requests.Stats.Request(start, finish, extension));
			return response;
		}

		public async System.Threading.Tasks.Task<List<Stats.Result>> GetStatResult(Stats.BaseKey key,int waitSecons = 0)
		{
			System.Threading.Thread.Sleep(waitSecons*1000);
			List<Stats.Result> recors = new List<Stats.Result>();
			var csv = await ExecuteCommand("stats/result", key);
            if (!string.IsNullOrEmpty(csv))
            {
				var items = csv.Split('\n');
				if (items.Length > 0)
				{
					foreach (var item in items)
					{
						if (!string.IsNullOrEmpty(item))
						{
							var data = item.Split(';');
							if (data.Length == 8)
							{
								Stats.Result r = new Stats.Result();
								r.records = data[0].Split(',');
								r.start = DateTimeHelper.UnixTimeStampToDateTime(Double.Parse(data[1]));
								r.finish = DateTimeHelper.UnixTimeStampToDateTime(Double.Parse(data[2]));
								r.from_extension = data[3];
								r.from_number = data[4];
								r.to_extension = data[5];
								r.to_number = data[6];
								r.disconnect_reason = data[7].RemoveSpecialCharacters();
								recors.Add(r);
							}
						}
					}
				}
			}
			return recors;
		}
		#endregion


		/// <summary>
		/// Execute http request on mango service
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="url"></param>
		/// <param name="json"></param>
		/// <returns></returns>
		private async Task<T> PerformCommandAsync<T>(string url, Object objSend = null)
        {
			var response_json = await ExecuteCommand(url, objSend);
			return JsonSerializer.Deserialize<T>(response_json);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="objSend"></param>
		/// <returns></returns>
		private async Task<string> ExecuteCommand(string url, Object objSend = null)
		{
			string json = "{}";
			if (objSend != null)
				json = JsonSerializer.Serialize(objSend);
			var api_token = Extensions.MangoSignHelper.GetSign(vpbx_api_key, json, sign);
			IList<KeyValuePair<string, string>> nameValueCollection = new List<KeyValuePair<string, string>>
			{
				{ new KeyValuePair<string, string>("vpbx_api_key", vpbx_api_key) },
				{ new KeyValuePair<string, string>("sign", api_token) },
				{ new KeyValuePair<string, string>("json", json) },
			};
			var response = await new HttpClient().PostAsync("https://app.mango-office.ru/vpbx/" + url, new FormUrlEncodedContent(nameValueCollection));
			if ((int)response.StatusCode >= 200 && (int)response.StatusCode <= 300)
			{
				var response_json = await response.Content.ReadAsStringAsync();
				return response_json;
			}
			else
				throw new MangoClientException(response.StatusCode.ToString());
		}
	}
}
