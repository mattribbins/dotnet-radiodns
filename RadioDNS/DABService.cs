/***
 * DAB Service
 */
using System;
using System.Text.RegularExpressions;
namespace RadioDNS
{
    /// <summary>
    /// DAB Service
    /// </summary>
    internal class DABService : Service
    {
        private string _gcc;
        private string _eid;
        private string _sid;
        private string _scids;
        private string _uatype;
        public string GCC { get => _gcc; set => _gcc = value; }
        public string EId { get => _eid; set => _eid = value; }
        public string SId { get => _sid; set => _sid = value; }
        public string SCIdS { get => _scids; set => _scids = value; }
        public string UAtype { get => _uatype; set => _uatype = value; }

        /// <summary>
        /// DAB Service
        /// </summary>
        /// <param name="gcc">Global Country Code< (GCC)/param>
        /// <param name="eid">Ensemble Identifier (EId)</param>
        /// <param name="sid">Service Identifier (SId)</param>
        /// <param name="scids">Service Component Identifier within the Service (SCIdS)</param>
        /// <exception cref="ArgumentNullException">Thrown when parameters missing.</exception>
        /// <exception cref="ArgumentException">Thrown when a parameter is not valid.</exception>
        public DABService(string gcc, string eid, string sid, string scids)
        {
            if(gcc == null || eid == null || sid == null || scids == null)
            {
                throw new ArgumentException("Missing parameters.");
            }
            gcc = gcc.ToLower();
            eid = eid.ToLower();
            sid = sid.ToLower();
            scids = scids.ToLower();
            // Check GCC value valid
            if (!Regex.IsMatch(gcc, "^[0-9a-f]{3}"))
            {
                throw new ArgumentException("GCC value invalid");
            }
            // Check EId value valid
            if (!Regex.IsMatch(eid, "^[0-9a-f]{4}"))
            {
                throw new ArgumentException("EId value invalid");
            }
            // Check SId value valid
            if (!Regex.IsMatch(sid, "^[0-9a-f]{4}|^[0-9a-f]{8}"))
            {
                throw new ArgumentException("SId value invalid");
            }
            // Check SCIdS value valid
            if (!Regex.IsMatch(scids, "^[0-9a-f]{1}"))
            {
                throw new ArgumentException("SCIdS value invalid");
            }
            // We're satisfied.
            _gcc = gcc;
            _eid = eid;
            _sid = sid;
            _scids = scids;
        }

        /// <summary>
        /// DAB Service
        /// </summary>
        /// <param name="gcc">Global Country Code< (GCC)/param>
        /// <param name="eid">Ensemble Identifier (EId)</param>
        /// <param name="sid">Service Identifier (SId)</param>
        /// <param name="scids">Service Component Identifier within the Service (SCIdS)</param>
        /// <param name="uatype">User Application Type (UAtype)</param>
        /// <exception cref="ArgumentNullException">Thrown when parameters missing.</exception>
        /// <exception cref="ArgumentException">Thrown when a parameter is not valid.</exception>
        public DABService(string gcc, string eid, string sid, string scids, string uatype) : this(gcc, eid, sid, scids)
        {
            if (uatype == null)
            {
                throw new ArgumentException("Missing parameters.");
            }
            uatype = uatype.ToLower();
            // Check UAtype value valid
            if (!Regex.IsMatch(scids, "^[0-9a-f]{3}"))
            {
                throw new ArgumentException("UAtype value invalid");
            }
            // UAtype is fine
            _uatype = uatype;
        }


        /// <summary>
        /// Get RadioDNS FQDN
        /// </summary>
        /// <returns>RadioDNS FQDN</returns>
        public override string GetRadioDNSFqdn()
        {
            string fqdn = string.Format("{0}.{1}.{2}.{3}.dab.radiodns.org", _scids, _sid, _eid, _gcc);
            // Add UAtype if defined
            if (_uatype != null)
            {
                fqdn = string.Format("{0}.{1}", _uatype, fqdn);
            }
            return fqdn;
        }
    }
}