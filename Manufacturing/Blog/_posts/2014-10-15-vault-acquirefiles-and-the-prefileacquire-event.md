---
layout: "post"
title: "Vault AcquireFiles and the PreFileAcquire event"
date: "2014-10-15 10:04:54"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/10/vault-acquirefiles-and-the-prefileacquire-event.html "
typepad_basename: "vault-acquirefiles-and-the-prefileacquire-event"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>The Vault Development Framework (VDF) acquire files API will download a file to disk so long as the file to be downloaded is different than the one that is currently on disk. There are some restrictions that might be enforced that could prevent the download, but that is the general idea. However, you can extend the behavior of the acquire using the events on the AcquireFilesSettings object to avoid downloading files based on your own custom criteria. </p>  <p>On the AcquireFilesSettings.AcquireFileExtensibilityOptions class (which can be accessed using the AcquireFileSettings.OptionsExtensibility property for a specific instance of the download settings), there is a PreFileAcquire event. </p>  <p>You can register a handler to this event which will fire for each file that is to be downloaded. As part of the event arguments, the handler is provided info about the file that is on disk relative to the file to be downloaded. This info can be accessed via the PreFileAcquireEventAgrs.FileAcquisitionInfo property. The property contains an enum with flags that will specify if the file on disk is newer. </p>  <p>If you don’t want to download an older version of a file, have the custom handler check for that flag and set the SkipDownload flag on the PreFileAcquisitionResult property of the event args. </p>  <p>The VDF AcquireFiles API is written to avoid excess calls to the server whenever it can and so it will not download files if it determines that the file on disk matches the file that is attempting to be downloaded. </p>
