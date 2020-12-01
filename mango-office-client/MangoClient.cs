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
		/// <summary>
		/// Generated token
		/// </summary>
		private	string api_token { get; set; }
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
			this.api_token = Extensions.MangoSignHelper.GetSign(vpbx_api_key, string.Empty, sign);
		}

		public void Dispose()
        {
#warning todo dispose 1
		}

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

		private async Task<T> PerformCommandAsync<T>(string url, string json = "{}")
        {
			IList<KeyValuePair<string, string>> nameValueCollection = new List<KeyValuePair<string, string>>
			{
				{ new KeyValuePair<string, string>("vpbx_api_key", vpbx_api_key) },
				{ new KeyValuePair<string, string>("sign", api_token) },
				{ new KeyValuePair<string, string>("json", json) },
			};

			var response = await new HttpClient().PostAsync("https://app.mango-office.ru/vpbx/"+url, new FormUrlEncodedContent(nameValueCollection));
			if ((int)response.StatusCode >= 200 && (int)response.StatusCode <= 300)
			{
				var response_json =  await response.Content.ReadAsStringAsync();
				return JsonSerializer.Deserialize<T>(response_json);
			}
			else
				throw new MangoClientException(response.StatusCode.ToString());
		}

		/*
        private async Task<T> PerformCommandAsync(string command, string json = "{}")
		{

		}*/

	}
}
