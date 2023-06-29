param planName string
param planLocation string = resourceGroup().location
param planSku string ='Y1'
param planTier string = 'Dynamic'

resource asp 'Microsoft.Web/serverfarms@2022-03-01' = {
  name:planName
  location:planLocation
  sku:{
    name:planSku
    tier:planTier
  }
}

output planId string = asp.id
