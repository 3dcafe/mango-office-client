using System;
using System.Collections.Generic;
using System.Text;

namespace MangoOfficeClient.Requests.Queries
{
    /// <summary>
    /// Get Audio record
    /// </summary>
    /// <summary xml:lang="ru">
    /// Получить запись разговора в аудио формате
    /// </summary>
    public class Recording
    {
        public string recording_id { get; set; }
        public string action { get; set; } = "play";
    }
}
