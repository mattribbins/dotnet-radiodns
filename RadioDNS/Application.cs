/***
 * RadioDNS Application
 */
using System.Collections.Generic;

namespace RadioDNS
{
    public class Application
    {
        private string _applicationID;
        private List<Record> _records;

        // Supported Applications
        public static string RADIOEPG = "radioepg";            // TS 102 818
        public static string RADIOSPI = "radiospi";            // TS 102 818
        public static string RADIOVIS = "radiovis";            // TS 101 499 (STOMP)
        public static string RADIOVIS_HTTP = "radiovis-http";  // TS 101 499 (HTTP)
        public static string RADIOTAG = "radiotag";            // RTAG01 Draft

        public string ID { get => _applicationID; private set => _applicationID = value; }
        public List<Record> Records { get => _records; private set => _records = value; }

        public Application(string applicationID, List<Record> records)
        {
            _applicationID = applicationID;
            _records = records;
        }

        public List<Record> GetRecords()
        {
            return _records;
        }
    }
}
