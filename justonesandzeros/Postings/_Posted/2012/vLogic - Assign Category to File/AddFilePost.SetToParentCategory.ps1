#########################################################################
#  This file is part of the Autodesk Vault API Code Samples.
#
#  Copyright (C) Autodesk Inc.  All rights reserved.
#
# THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
# KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
# IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
# PARTICULAR PURPOSE.
#########################################################################


# This script updates new files not assigned a category are updated to match the category on the parent folder.
# A manual refresh is needed to see the category change.
 
# Set this value to the display name of the "default" file category 
$BaseCategory = "Base" 
 
 
if ($e.Status -eq [Autodesk.Connectivity.WebServices.EventStatus]::SUCCESS -and 
    $e.ReturnValue.Cat -and 
    $e.ReturnValue.Cat.CatName -eq $BaseCategory)
{
   $parentFolder = $serviceManager.DocumentService.GetFolderById($e.FolderId)

   if ($parentFolder.Cat)
   {
      $fileCats = $serviceManager.CategoryService.GetCategoriesByEntityClassId("FILE", $true)
      $fileCatId = $fileCats | 
            Where-Object {$_.Name -eq $parentFolder.Cat.CatName} | 
            Select-Object -ExpandProperty Id
      
      if ($fileCatId)
      {
        $temp = $serviceManager.DocumentServiceExtensions.UpdateFileCategories(@($e.ReturnValue.MasterId), @($fileCatId), 
          "Setting category to match parent folder")
      }
   }
}
