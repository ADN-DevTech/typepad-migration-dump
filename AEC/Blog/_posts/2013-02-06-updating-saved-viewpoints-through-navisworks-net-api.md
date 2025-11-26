---
layout: "post"
title: "Updating saved viewpoints through Navisworks .NET API"
date: "2013-02-06 07:38:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Navisworks"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2013/02/updating-saved-viewpoints-through-navisworks-net-api.html "
typepad_basename: "updating-saved-viewpoints-through-navisworks-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>
<p>Is it possible to update saved viewpoints in Navisworks through the .NET API after they have been created. For example, if we want to update views after hiding some objects.</p>
<p>Navisworks .NET API includes a method called DocumentSavedViewpoints.ReplaceFromCurrentView(). This method can help replace the SavedViewpoint that is being passed on to this method with an updated copy. Viewpoint, Redlines and visibility are updated to those in the current View</p>
