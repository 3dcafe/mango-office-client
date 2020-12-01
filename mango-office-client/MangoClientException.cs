using System;
using System.Collections.Generic;
using System.Text;

namespace MangoOfficeClient
{
    /// <summary>
    /// The request was unable to be fulfilled by the mango server and this exception contains the response.
    /// </summary>
    [Serializable]
    public class MangoClientException : Exception
    {
        /// <summary>
        /// Base constructor for Exception
        /// </summary>
        /// <param name="message"></param>
        public MangoClientException(string message) : base(message) { }
    }
}
