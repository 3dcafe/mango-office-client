using MangoOfficeClient.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangoOfficeClient.Requests.Stats
{
    /// <summary>
    /// Request stats prepare for get id calls
    /// </summary>
    /// <summary xml:lang="ru">
    /// Запрос на получение статистики по звонкам
    /// </summary>
    public class Request
    {
        private DateTime start;
        private DateTime finish;

        public string date_from
        {
            get
            {
                return start.ToUnixTimeStamp().ToString();
            }
        }
        public string date_to
        {
            get
            {
                return finish.ToUnixTimeStamp().ToString();
            }
        }
        public string fields
        {
            get
            {
                return "records, start, finish, from_extension, from_number,to_extension, to_number, disconnect_reason";
            }
        }
        public CallParty call_party { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start">Start date calls</param>
        /// <param name="finish">Finish date calls</param>
        /// <param name="extension">the PBX employee ID for the caller</param>
        public Request(DateTime start, DateTime finish,string extension)
        {
            this.start = start;
            this.finish = finish;
            this.call_party = new CallParty()
            {
                extension = extension,
                request_id = Guid.NewGuid().ToString()
            };
        }
    }
}
