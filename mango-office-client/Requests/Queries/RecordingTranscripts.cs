using System;
using System.Collections.Generic;
using System.Text;

namespace MangoOfficeClient.Requests.Queries
{
    /// <summary>
    /// Получение диалога
    /// </summary>
    public class RecordingTranscripts
    {
        /// <summary>
        /// Идентификаторы записи
        /// </summary>
        public string[] recording_id { get; set; }
    }
}
