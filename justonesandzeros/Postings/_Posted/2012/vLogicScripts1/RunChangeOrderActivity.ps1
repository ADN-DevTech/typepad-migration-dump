# $serviceManager contains all the services for communicating with the Vault Server
# $context contains the command context
# Any output text or error will be displayed in a dialog when the command completes. 


# This script performs a state-change activity on the selected Change Order


##############################
# Add your settings here

$activityName = "Approve"



##############################
# Run the operation

$coIds = $context.CurrentSelectionSet | 
     Where-Object {$_.TypeId -eq [Autodesk.Connectivity.Explorer.Extensibility.SelectionTypeId]::ChangeOrder} | 
     Select-Object -ExpandProperty Id

if (!$coIds)
{
    "No change orders selected"
    return
}
if ($coIds.Length -and $coIds.Length -ne 1)
{
    "You cannot select more than one Change Order"
    return
}


$cos = $serviceManager.ChangeOrderService.GetChangeOrdersByIds($coIds)
$co = $cos[0]

if ($co.ActivityArray -eq $NULL -or $co.ActivityArray.Length -eq 0)
{
    "Activity " + $activityName + " cannot be performed on " + $co.Num
    return
}
    
$activityId = $co.ActivityArray | Where-Object {$_.DispName -eq $activityName} | Select-Object -ExpandProperty Id

if ($activityId -eq $NULL -or $activityId -eq 0)
{
    "Activity " + $activityName + " cannot be performed on " + $co.Num
    return
}

$editableCo = $serviceManager.ChangeOrderService.StartChangeOrderActivity($co.Id, $activityId, $co.StateId, $co.StateEntered)
$committedCo = $serviceManager.ChangeOrderService.CommitChangeOrderActivity($editableCo.Id, $activityId, $editableCo.StateId, $editableCo.StateEntered, $NULL, $NULL)
    
    
"Operation complete"
$context.ForceRefresh = 1