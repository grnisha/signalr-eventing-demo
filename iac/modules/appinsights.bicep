@description('Name of Application Insights resource.')
param name string

@description('The location where the app insights will reside in.')
param rgLocation string = resourceGroup().location

@description('The log analytics worspace resource Id.')
param workspaceResourceId string


resource appIns 'Microsoft.Insights/components@2020-02-02' = {
  name:name
  kind:'web'
  location:rgLocation
  properties:{
    Application_Type:'web'
    Request_Source:'rest'
    Flow_Type:'Bluefield'
    WorkspaceResourceId:workspaceResourceId
  }

}

output appInsightsKey string = appIns.properties.InstrumentationKey
