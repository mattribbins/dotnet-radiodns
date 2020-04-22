/***
 * HD Service (IBOC)
 */
using System;
using System.Text.RegularExpressions;

namespace RadioDNS
{
    internal class HDService : Service
    {
        private string _tx;
        private string _cc;
        private string _mId;
        public string TX { get => _tx; set => _tx = value; }
        public string CC { get => _cc; set => _cc = value; }
        public string MId { get => _mId; set => _mId = value; }

        /// <summary>
        /// HD Service
        /// </summary>
        /// <param name="tx">Transmitter Identifier</param>
        /// <param name="cc">Country Code</param>
        /// <exception cref="ArgumentNullException">Thrown when parameters missing.</exception>
        /// <exception cref="ArgumentException">Thrown when a parameter is not valid.</exception>
        public HDService(string tx, string cc)
        {
            // Check params exist
            if (tx == null || cc == null)
            {
                throw new ArgumentNullException("Missing parameters.");
            }
            tx = tx.ToLower();
            cc = cc.ToLower();
            // Check TX id value valid
            if (!Regex.IsMatch(tx, "^[0-9a-f]{5}"))
            {
                throw new ArgumentException("tx value invalid.");
            }
            // Check CC code valid
            if (!Regex.IsMatch(cc, "^[0-9a-f]{3}"))
            {
                throw new ArgumentException("cc value invalid");
            }
            // We're satisfied.
            _tx = tx;
            _cc = cc;
        }

        /// <summary>
        /// HD Service with SPS channel
        /// </summary>
        /// <param name="tx">Transmitter Identifier</param>
        /// <param name="cc">Country Code</param>
        /// <param name="mid">Multicast Supplemental Program Service (SPS) channel</param>
        /// <exception cref="ArgumentNullException">Thrown when parameters missing.</exception>
        /// <exception cref="ArgumentException">Thrown when a parameter is not valid.</exception>
        public HDService(string tx, string cc, string mid) : this(tx, cc)
        {
            if (mid == null)
            {
                throw new ArgumentNullException("Missing parameters.");
            }
            mid = mid.ToLower();
            // Check mId value valid
            if (!Regex.IsMatch(mid, "^[0-9a-f]{1}"))
            {
                throw new ArgumentException("mId value invalid.");
            }
            // mId is fine
            _mId = mid;
        }

        public override string GetRadioDNSFqdn()
        {
            string fqdn = string.Format("{0}.{1}.hd.radiodns.org", _tx, _cc);
            if (_mId != null)
            {
                fqdn = string.Format("{0}.{1}", _mId, fqdn);
            }
            return fqdn;
        }
    }
}