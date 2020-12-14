using System;

namespace MangoOfficeClient.Stats
{
    public class Result
    {
        /// <summary xml:lang="ru">
        /// Записи разговоров ссылки на них
        /// </summary>
        public string[] records { get; set; }
        public DateTime start { get; set; }
        public DateTime finish { get; set; }
        public string from_extension { get; set; }
        public string from_number { get; set; }
        public string to_extension { get; set; }
        public string to_number { get; set; }
        public string disconnect_reason { get; set; }
    }
}
