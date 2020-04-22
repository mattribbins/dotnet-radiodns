/***
 * RadioDNS Service
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DnsClient;
using DnsClient.Protocol;

namespace RadioDNS
{
    public abstract class Service
    {
        /// <summary>
        /// Get RadioDNS FQDN
        /// </summary>
        /// <returns>RadioDNS FQDB</returns>
        public abstract string GetRadioDNSFqdn();

        /// <summary>
        /// Get Authoritative FQDN
        /// </summary>
        /// <returns>Authoritative FQDN</returns>
        public string GetAuthoritativeFqdn()
        {
            return ResolveAuthoritativeFqdn().Result;
        }

        /// <summary>
        /// Get the RadioDNS Application
        /// </summary>
        /// <param name="applicationId">RadioDNS Application Identifier</param>
        /// <returns>RadioDNS Application</returns>
        public Application GetApplication(string applicationId)
        {
            return ResolveApplication(applicationId).Result;
        }

        /// <summary>
        /// Get the RadioDNS Application
        /// </summary>
        /// <param name="applicationId">Application Identifier</param>
        /// <returns>RadioDNS Application</returns>
        private async Task<Application> ResolveApplication(string applicationId)
        {
            string authoritativeFqdn = GetAuthoritativeFqdn();
            string applicationFqdn;
            
            // Check params exist
            if (applicationId == null)
            {
                throw new ArgumentNullException("Application ID not provided.");
            }
            if (authoritativeFqdn == null)
            {
                throw new NullReferenceException("Unable to retrive Authoritative FQDN.");
            }

            applicationFqdn = string.Format("_{0}._tcp.{1}", applicationId.ToLower(), authoritativeFqdn);

            // Perform DNS lookup of SRV record(s)
            try
            {
                LookupClient client = new LookupClient();
                IDnsQueryResponse response = await client.QueryAsync(applicationFqdn, QueryType.SRV);
                if (response.HasError)
                {
                    throw new DnsResponseException(string.Format("Response had an error. {0}", response.ErrorMessage));
                }
                IReadOnlyList<DnsResourceRecord> records = response.Answers;
                if (records.Count > 0)
                {
                    // We have SRV records. Copy them to our records object and return a valid Application.
                    List<Record> applicationRecords = new List<Record>();
                    foreach (SrvRecord r in records)
                    {
                        applicationRecords.Add(new Record(r));
                    }
                    return new Application(applicationId, applicationRecords);
                }
                else
                {
                    throw new DnsResponseParseException("Response had no results.");
                }
                ;
            }
            catch (Exception)
            {
                return null;
            }
 
        }

        /// <summary>
        /// Resolve the Authoritative FQDN CNAME record
        /// </summary>
        /// <returns>Authoritative FQDN</returns>
        private async Task<string> ResolveAuthoritativeFqdn()
        {
            string authoritativeFqdn;
            // Perform DNS lookup of CNAME record
            try
            {
                LookupClient client = new LookupClient();
                IDnsQueryResponse response = await client.QueryAsync(GetRadioDNSFqdn(), QueryType.CNAME);
                if (response.HasError)
                {
                    throw new DnsResponseParseException("Response had an error.");
                }

                IReadOnlyList<DnsResourceRecord> records = response.Answers;
                if (records.Count > 0)
                {
                    // We have a CNAME record. Take the first record.
                    authoritativeFqdn = ((CNameRecord)records[0]).CanonicalName;
                }
                else
                {
                    throw new DnsResponseParseException("Response had no results.");
                }
                return authoritativeFqdn;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
