---
layout: "post"
title: "the-getattr-command-on-a-compound-attribute-is-invalid-and-will-return-an-error-message-however-if-is-perfectly-valid-to-ac"
date: "2014-02-24 04:30:38"
author: "Cyrille Fauvel"
categories: []
original_url: "https://around-the-corner.typepad.com/adn/2014/02/the-getattr-command-on-a-compound-attribute-is-invalid-and-will-return-an-error-message-however-if-is-perfectly-valid-to-ac.html "
typepad_basename: "the-getattr-command-on-a-compound-attribute-is-invalid-and-will-return-an-error-message-however-if-is-perfectly-valid-to-ac"
typepad_status: "Draft"
---

<p>The &#39;getAttr&#39; command on a compound attribute is invalid and will return an error message. However, if is perfectly valid to access a compound because API and Maya internal code can still retrieve the compound from its plug.</p>
<p>In order to do this, get an MArrayDataBuilder to the compound multi attribute, use it to assign values to the child attributes for the various elements of the multi, then set the data builder into the MArrayDataHandle, which you can build from the MDataHandle in the way that Kevin showed you.</p>
