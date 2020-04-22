/***
 * RadioDNS FM Service
 * 
 * (C) Bauer Media Group 2020
 */
using System;
/***
 * FM Service
 */
using System.Text.RegularExpressions;

namespace RadioDNS
{
    internal class FMService : Service
    {
        private string _gcc;
        private string _pi;
        private double _frequency;

        private static readonly double MIN_FREQUENCY = 76.0;
        private static readonly double MAX_FREQUENCY = 108.000;

        public string GCC { get => _gcc; set => _gcc = value; }
        public string PI { get => _pi; set => _pi = value; }
        public double Frequency { get => _frequency; set => _frequency = value; }

        /// <summary>
        /// FM Service
        /// </summary>
        /// <param name="gcc"></param>
        /// <param name="pi"></param>
        /// <param name="frequency">Frequency in MHz</param>
        /// <exception cref="ArgumentNullException">Thrown when parameters missing.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when Frequency out of range.</exception>
        /// <exception cref="ArgumentException">Thrown when GCC or PI code is not valid.</exception>
        public FMService(string gcc, string pi, double frequency)
        {
            // Check params exist
            if (gcc == null || pi == null)
            {
                throw new ArgumentNullException("Missing parameters.");
            }
            // Check frequency in range
            if ((frequency >= MIN_FREQUENCY) && (frequency >= MAX_FREQUENCY))
            {
                throw new ArgumentOutOfRangeException("Frequency out of range.");
            }
            gcc = gcc.ToLower();
            pi = pi.ToLower();
            // Check GCC value valid
            if (!Regex.IsMatch(gcc, "^[0-9a-f]{3}"))
            {
                throw new ArgumentException("GCC value invalid.");
            }
            // Check PI code valid
            if (!Regex.IsMatch(pi, "^[0-9a-f]{4}"))
            {
                throw new ArgumentException("PI value invalid");
            }
            // We're satisfied.
            _gcc = gcc;
            _pi = pi;
            _frequency = frequency;
        }

        /// <summary>
        /// Get RadioDNS FQDN
        /// </summary>
        /// <returns>RadioDNS FQDN</returns>
        public override string GetRadioDNSFqdn()
        {
            return string.Format("{0:D5}.{1}.{2}.fm.radiodns.org", (int)(_frequency * 100), _pi, _gcc);
        }
    }


}