param functionAppName string
param storageAccountName string
param cosmosDbName string
param signalRName string
param appinsightsName string

resource appIns 'Microsoft.Insights/components@2020-02-02'  existing = {
  name: appinsightsName
}

resource stg 'Microsoft.Storage/storageAccounts@2021-02-01' existing = {
  name: storageAccountName
}

resource account 'Microsoft.DocumentDB/databaseAccounts@2022-05-15' existing = {
  name: cosmosDbName
}

resource signalR 'Microsoft.SignalRService/signalR@2022-02-01' existing = {
  name: signalRName
}

resource functionAppAppsettings 'Microsoft.Web/sites/config@2022-09-01' = {
  name: '${functionAppName}/appsettings'
  properties: {
    APPINSIGHTS_INSTRUMENTATIONKEY: appIns.properties.InstrumentationKey
    AzureWebJobsStorage: 'DefaultEndpointsProtocol=https;AccountName=${storageAccountName};AccountKey=${stg.listKeys().keys[0].value};EndpointSuffix=core.windows.net'
    WEBSITE_CONTENTAZUREFILECONNECTIONSTRING: 'DefaultEndpointsProtocol=https;AccountName=${storageAccountName};AccountKey=${stg.listKeys().keys[0].value};EndpointSuffix=core.windows.net'
    WEBSITE_CONTENTSHARE: toLower(functionAppName)
    FUNCTIONS_EXTENSION_VERSION: '~4'
    FUNCTIONS_WORKER_RUNTIME: 'dotnet'
    CosmosDBConnectionSetting: account.listConnectionStrings().connectionStrings[0].connectionString
    AzureSignalRConnectionString: signalR.listKeys('2022-02-01').primaryConnectionString
  }
}
