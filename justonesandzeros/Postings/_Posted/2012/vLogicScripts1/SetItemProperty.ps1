# $serviceManager contains all the services for communicating with the Vault Server
# $context contains the command context
# Any output text or error will be displayed in a dialog when the command completes. 


# This script sets a property value on the selected Items


##############################
# Add your settings here

$propertyName = "MyProp"
$newValue = "new value"



##############################
# Run the operation

$itemIds = $context.CurrentSelectionSet | 
     Where-Object {$_.TypeId -eq [Autodesk.Connectivity.Explorer.Extensibility.SelectionTypeId]::Item -or 
        $_.TypeId -eq [Autodesk.Connectivity.Explorer.Extensibility.SelectionTypeId]::Bom} | 
     Select-Object -ExpandProperty Id

if (!$itemIds)
{
    "No items selected"
    return
}

$items = $serviceManager.ItemService.GetItemsByIds($itemIds)
$itemRevIds = $items | Select-Object -ExpandProperty RevId

$items = $serviceManager.ItemService.EditItems($itemRevIds)

$propertyDefinitions = $serviceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("ITEM")
$propDefId = $propertyDefinitions | Where-Object {$_.DispName -eq $propertyName} | Select-Object -ExpandProperty Id

$serviceManager.ItemService.UpdateItemProperties($itemRevIds, @($propDefId), @($newValue))

"Operation complete"
$context.ForceRefresh = 1
