param location string
param name string
@allowed([ 'Free', 'Standard' ])
param sku string = 'Standard'
param functionAppName string

//Get function reference
resource functionApp 'Microsoft.Web/sites@2022-03-01' existing = {
  name: functionAppName
}

resource swa 'Microsoft.Web/staticSites@2022-09-01' = {
  name: name
  location: location
  sku: {
    name: sku
    tier: sku
  }
}

resource linkedBackend 'Microsoft.Web/staticSites/linkedBackends@2022-09-01' = {
  parent: swa
  name: 'demobackend'
  properties: {
    backendResourceId: functionApp.id
    region: location
  }
}

resource userprovidedFunction 'Microsoft.Web/staticSites/userProvidedFunctionApps@2022-09-01' = {
  parent: swa
  name: 'demobackend'
  properties: {
    functionAppResourceId: functionApp.id
    functionAppRegion: location
  }
}

output swaHostName string = swa.properties.defaultHostname
