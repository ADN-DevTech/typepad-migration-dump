---
layout: "post"
title: "Thumbnails when saving documents in hidden mode - change in Inventor 2017"
date: "2016-05-04 13:18:13"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/05/thumbnails-when-saving-documents-in-hidden-mode-change-in-inventor-2017.html "
typepad_basename: "thumbnails-when-saving-documents-in-hidden-mode-change-in-inventor-2017"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p> <p>Versions of Inventor before the 2017 release do not generate a thumbnail when a document is saved in hidden mode. The thumbnails may not be correct in this situation. For example if a part is opened in hidden mode and a hole feature is added with the API in Inventor 2016 and saved, the thumbnail will not show the new hole. </p> <p>Inventor 2017 has new functionality so the thumbnail is generated when the document is saved in hidden mode and the geometry of the thumbnail is correct. However the background may not be right. Currently there is a limitation where a picture background will not be used. A workaround is to open the file in visible mode and save it. (This limitation of the new implementation in Inventor 2017 has been reported to Engineering)  <p>If the background is non-pictorial Inventor 2017 will use the application setting for the color scheme for the hidden files thumbnail background. If you want the background of hidden files to be a specific color scheme (non-picture) this can be achieved by modifying the Inventor 2017 Application settings. (Color Scheme)  <p>&nbsp; <p>Note: Document settings where never used to save thumbnails. (Both the background color scheme and background picture only exists in application setting). The reason why it may seem like there’s document setting for background in previous versions is that the thumbnail never re-generated in hidden files, so the background was always the same. </p>
