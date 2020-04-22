/***
 * RadioDNS library for .NET
 * 
 * Description: Library for RadioDNS resolutions and service lookups.
 *              See https://radiodns.org/technical/
 * Author: Matt Ribbins (mribbins@celador.co.uk)
 * 
 * (C) 2020 Celador Radio
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace RadioDNS
{
    /// <summary>
    /// RadioDNS instance
    /// </summary>
    public class Lookup
    {
        /// <summary>
        /// RadioDNS class constructor
        /// </summary>
        public Lookup()
        {

        }

        /// <summary>
        /// Lookup a FM service with FM broadcast parameters
        /// </summary>
        /// <param name="gcc">Global Country Code (GCC)</param>
        /// <param name="pi">Programme Identification (PI) code</param>
        /// <param name="frequency">Frequency in MHz (e.g. 106.50)</param>
        public Service LookupFMService(string gcc, string pi, double frequency)
        {
            return new FMService(gcc, pi, frequency);
        }

        /// <summary>
        /// Lookup a DAB service with DAB broadcast parameters
        /// </summary>
        /// <param name="gcc">Global Country Code< (GCC)/param>
        /// <param name="eid">Ensemble Identifier (EId)</param>
        /// <param name="sid">Service Identifier (SId)</param>
        /// <param name="scids">Service Component Identifier within the Service (SCIdS)</param>
        public Service LookupDABService(string gcc, string eid, string sid, string scids)
        {
            return new DABService(gcc, eid, sid, scids);
        }

        /// <summary>
        /// Lookup a DAB service with DAB broadcast parameters + X-PAD
        /// </summary>
        /// <param name="gcc">Global Country Code< (GCC)/param>
        /// <param name="eid">Ensemble Identifier (EId)</param>
        /// <param name="sid">Service Identifier (SId)</param>
        /// <param name="scids">Service Component Identifier within the Service (SCIdS)</param>
        /// <param name="uatype">User Application Type (UAtype)</param>
        public Service LookupDABService(string gcc, string eid, string sid, string scids, string uatype)
        {
            return new DABService(gcc, eid, sid, scids, uatype);
        }

        /// <summary>
        /// Lookup an AM service based on AM broadcast parameters
        /// </summary>
        /// <param name="type">Type of AM service ("drm", "amss")</param>
        /// <param name="sid">Service Identifier (SId)</param>
        public Service LookupAMService(string type, string sid)
        {
            return new AMService(type, sid);
        }

        /// <summary>
        /// Lookup a service with HD (IBOC) broadcast parameters
        /// </summary>
        /// <param name="tx">Transmitter Identifier</param>
        /// <param name="cc">Country Code</param>
        public Service LookupHDService(string tx, string cc)
        {
            return new HDService(tx, cc);
        }

        /// <summary>
        /// Lookup a service with HD (IBOC) broadcast parameters
        /// </summary>
        /// <param name="tx">Transmitter Identifier</param>
        /// <param name="cc">Country Code</param>
        /// <param name="mid">Multicast Supplemental Program Service (SPS) channel</param>
        public Service LookupHDService(string tx, string cc, string mid)
        {
            return new HDService(tx, cc, mid);
        }
    }
}
