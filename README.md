# WebServiceMonitoring
This project can be used to monitoring WCF Calls to another service or to Web Service which were created using SOAP Web Services standards with .NET.

## Log web service SOAP client

 To used the option for monitoring SOAP calls using Web Service client, we can have to follow configuration:

1) Add within of your configuration files the section:

```xml
<configuration>
  <configSections>

    <section name="logSettings" type="WebServiceMonitoring.LogService.Configuration.LogServiceConfiguration" requirePermission="false"/>
  </configSections>
  <logSettings connectionString="myConnectionString" />
</configuration>
```

2) Then create the element (inside the configuration section):

```xml
<configuration>
   <logSettings connectionString="myConnectionString" />
</configuration>
```


The settings must have a valid  connection string:

```xml
<connectionStrings>
    <add name="myConnectionString" connectionString=""/>   
 </connectionStrings>
 ```
 
And to finish, you must have set up the web service configuration section as follow:

```xml
<webServices>
      
      <soapExtensionTypes>
        <add type="WebServiceMonitoring.LogService.Configuration.LogWebServiceCall, WebServiceMonitoring" priority="1" group="High"/>
    </webServices>
  ```
  
  ## Log client web service SOAP
  
  ## Log WCF service SOAP client
  
  The project does not support file configuration for wcf soap client yet at moment. If you want to monitoring/inspect package do the follow:
  
  ```csharp
using (var clientService = new WCFServiceClient())
{
    clientService.Endpoint.EndpointBehaviors.Add(new LogBehavior());               
}
```
  
  
