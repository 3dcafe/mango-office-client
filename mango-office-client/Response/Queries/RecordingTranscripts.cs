using System;
using System.Collections.Generic;
using System.Text;

namespace MangoOfficeClient.Response.Queries
{
    public class Names
    {
        public string client { get; set; }
        public string @operator { get; set; }
    }

    public class Datum
    {
        public string recording_id { get; set; }
        public Names names { get; set; }
        public List<List<string>> phrases { get; set; }
    }

    public class RecordingTranscripts
    {
        public int result { get; set; }
        public List<Datum> data { get; set; }
    }


}
