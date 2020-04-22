/***
 * SRV DNS Record abstraction
 */
using DnsClient.Protocol;

namespace RadioDNS
{
    /// <summary>
    /// Abstraction layer for SRV DNS record.
    /// </summary>
    public class Record
    {
        SrvRecord _record;

        public int TimeToLive { get => _record.TimeToLive; }
        public int Priority { get => _record.Priority; }
        public int Weight { get => _record.Weight; }
        public int Port { get => _record.Port; }
        public string Target { get => _record.Target; }

        public Record(SrvRecord record)
        {
            _record = record;
        }
    }
}