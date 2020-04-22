/***
 * AM Service
 */
using System;
using System.Text.RegularExpressions;

namespace RadioDNS
{
    internal class AMService : Service
    {
        private string _type;
        private string _sid;


        /// <summary>
        /// AM Service
        /// </summary>
        /// <param name="type">AM service type</param>
        /// <param name="sid">Service ID</param>
        /// <exception cref="ArgumentNullException">Thrown when parameters missing.</exception>
        /// <exception cref="ArgumentException">Thrown when a parameter is not valid.</exception>
        public AMService(string type, string sid)
        {
            // Check params exist
            if (type == null || sid == null)
            {
                throw new ArgumentNullException("Missing parameters.");
            }
            // Check valid type
            if (type != "drm" && type != "amss")
            {
                throw new ArgumentException("Invalid service type.");
            }
            // Check SId value valid
            if (!Regex.IsMatch(sid, "^[0-9a-f]{6}"))
            {
                throw new ArgumentException("SId value invalid");
            }
            // We're satisfied
            _type = type;
            _sid = sid;
        }

        /// <summary>
        /// Get RadioDNS FQDN
        /// </summary>
        /// <returns></returns>
        public override string GetRadioDNSFqdn()
        {
            switch(_type)
            {
                case "drm":
                    return string.Format("{0}.drm.radiodns.org", _sid);
                case "amss":
                    return string.Format("{0}.amss.radiodns.org", _sid);
                default:
                    return null;
            }
            
        }
    }
}