# $serviceManager contains all the services for communicating with the Vault Server
# $context contains the command context
# Any output text or error will be displayed in a dialog when the command completes. 

# This script runs an item search


########################################
# Part 1 - setup the search conditions
########################################

$propertyDefinitions = $serviceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("ITEM")

# The search condition:  State is "Released"
$stateIsReleased = new-object Autodesk.Connectivity.WebServices.SrchCond
$stateIsReleased.PropTyp = [Autodesk.Connectivity.WebServices.PropertySearchType]::SingleProperty
$stateIsReleased.SrchRule = [Autodesk.Connectivity.WebServices.SearchRuleType]::Must
$stateIsReleased.PropDefId = $propertyDefinitions | Where-Object {$_.SysName -eq "State"} | Select-Object -ExpandProperty Id
$stateIsReleased.SrchOper = 3
$stateIsReleased.SrchTxt = "Released"

# The search condition:  Category Name is "Part"
$categoryIsPart = new-object Autodesk.Connectivity.WebServices.SrchCond
$categoryIsPart.PropTyp = [Autodesk.Connectivity.WebServices.PropertySearchType]::SingleProperty
$categoryIsPart.SrchRule = [Autodesk.Connectivity.WebServices.SearchRuleType]::Must
$categoryIsPart.PropDefId = $propertyDefinitions | Where-Object {$_.SysName -eq "CategoryName"} | Select-Object -ExpandProperty Id
$categoryIsPart.SrchOper = 3
$categoryIsPart.SrchTxt = "Part"

########################################
# Part 2 - run the search
########################################

$bookmark = [System.String]::Empty
$searchStatus = $NULL
$searchResults = $serviceManager.ItemService.FindItemRevisionsBySearchConditions(
     @($stateIsReleased, $categoryIsPart), $NULL, $True, [ref]$bookmark, [ref]$searchStatus)


########################################
# Part 3 - output the results
########################################

$searchResults | Select-Object -ExpandProperty ItemNum