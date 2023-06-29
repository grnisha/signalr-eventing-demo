param location string
param functionAppName string
param planName string

resource functionAppResource 'Microsoft.Web/sites@2022-03-01' = {
  name: functionAppName
  identity: {
    type: 'SystemAssigned'
  }
  location: location
  kind: 'functionapp'
  properties: {
    serverFarmId: planName
    siteConfig: {
      cors: {
        allowedOrigins: [ '*' ]
        supportCredentials: true
      }
    }
  }
}

output prodFunctionAppName string = functionAppResource.name
output productionTenantId string = functionAppResource.identity.tenantId
output productionPrincipalId string = functionAppResource.identity.principalId
