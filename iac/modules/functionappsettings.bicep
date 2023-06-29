param functionAppName string
param storageAccountConnectionString string
param appInsightsKey string
param webappUrl string
param cosmosDbConnectionString string
param signalRConnectionString string


resource functionAppAppsettings 'Microsoft.Web/sites/config@2022-09-01' = {
  name: '${functionAppName}/appsettings'
  properties: {
    APPINSIGHTS_INSTRUMENTATIONKEY: appInsightsKey
    AzureWebJobsStorage: storageAccountConnectionString
    WEBSITE_CONTENTAZUREFILECONNECTIONSTRING: storageAccountConnectionString
    WEBSITE_CONTENTSHARE: toLower(functionAppName)
    FUNCTIONS_EXTENSION_VERSION: '~4'
    FUNCTIONS_WORKER_RUNTIME: 'dotnet'
    CosmosDBConnectionSetting: cosmosDbConnectionString
    AzureSignalRConnectionString: signalRConnectionString
    cors: {
      allowedOrigins: [
        'https://portal.azure.com'
         webappUrl
      ]
      supportCredentials: true
    }
  }
}
