# RadioDNS lookup library for .NET
This is a .NET library for RadioDNS Service lookups. 
The library will allow you to find the FQDN (Fully Qualified Domain Name) for a radio station (Service) based on broadcast parameters. 
Using this information, you can then make an IP connection to the desired service.

For more information on RadioDNS, please visit https://radiodns.org/technical/documentation/

## Installation
Either install the NuGet package, or add the project to your solution.

## Usage and examples
The library can be either called using the namespace `RadioDNS` or by adding an include reference.
```include RadioDNS;```

This library requires [DnsClient](https://www.nuget.org/packages/DnsClient/).

### Looking up an FM service
In this example, we look up Service Information for The Breeze North Dorset on 97.4 FM.
```
RadioDNS.Lookup rdns = new RadioDNS.Lookup();
RadioDNS.Service service = rdns.LookupFMService("ce1", "c3bb", 97.40);
RadioDNS.Application application = service.GetApplication(RadioDNS.Application.RADIOEPG);
if(null != application)
{
    foreach (Record record in application.GetRecords())
    {
        Console.WriteLine(string.Format("Host: {0}:{1:D}, Priority: {2}, Weight {3}",
            record.Target, record.Port, record.Priority, record.Weight));
    }
} else
{
    Console.WriteLine("Application specified does not exist for this Service.");
}
```
### Looking up a DAB service
In this example, we look up Service Information for Sam FM Bristol on DAB.
```
RadioDNS.Lookup rdns = new RadioDNS.Lookup();
RadioDNS.Service service = rdns.LookupDABService("xe1", "c18c", "cae3", "0");
RadioDNS.Application application = service.GetApplication(RadioDNS.Application.RADIOEPG);
if (null != application)
{
    foreach (Record record in application.GetRecords())
    {
        Console.WriteLine(string.Format("Host: {0}:{1:D}, Priority: {2}, Weight {3}",
            record.Target, record.Port, record.Priority, record.Weight));
    }
}
else
{
    Console.WriteLine("Application specified does not exist for this Service.");
}
```