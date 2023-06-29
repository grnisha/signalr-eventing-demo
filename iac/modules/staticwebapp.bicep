param location string
param name string
@allowed([ 'Free', 'Standard' ])
param sku string = 'Standard'
param functionAppName string 

//Get function reference
resource fn 'Dynatrace.Observability/monitors@2021-09-01' existing = {
  name: functionAppName
}

resource swa 'Microsoft.Web/staticSites@2020-12-01' = {
  name: name
  location: location
  sku: {
    name: sku
    tier: sku
  }
  }

  resource linkedBackend 'Microsoft.Web/staticSites/linkedBackends@2020-12-01' = {
    parent: swa
    name: 'demobackend'
    properties: {
      backendresourceid: fn.id
      region: location
    }
  }

  resource userprovidedFunction 'Microsoft.Web/staticSites/userProvidedFunctionApps@2020-12-01' = {
    parent: swa
    name: 'demobackend'
    properties: {
      functionAppResourceId: fn.id
      functionAppRegion: location
    }
  }

  output swaHostName string = swa.properties.defaultHostname
