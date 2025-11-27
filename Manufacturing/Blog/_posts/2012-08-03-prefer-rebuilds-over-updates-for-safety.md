---
layout: "post"
title: "Prefer Rebuilds over Updates for Safety"
date: "2012-08-03 02:14:00"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/prefer-rebuilds-over-updates-for-safety.html "
typepad_basename: "prefer-rebuilds-over-updates-for-safety"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>  <p><strong>Issue</strong>     <br />Why should I prefer the Inventor API Document 'Rebuild()' method over the 'Update()' method? When should they yield the same result?</p>  <p><strong>Solution</strong>     <br />If you change a model via the Inventor API (e.g. change a constraint, or dimension or a parameter etc), then dependent entities need to be recomputed. This doesn't happen automatically (the benefit is that changes can be batched to re-compute operation to improve performance).     <br />There are two methods that can be used to trigger a re-compute:</p>  <ol>   <li>Update() </li>    <li>Rebuild() </li> </ol>  <p>Update() checks whether individual entities within the scope of the document require updating. Only those entities flagged as 'Dirty' are recomputed.    <br />Rebuild() assumes that all entities are 'Dirty', and triggers a recompute on everything.     <br />Update is quicker, because only those entities that require a re-compute receive one. However we are reliant on the logic inside Inventor being correct, and setting entities as 'Dirty' appropriately. This isn't always the case. Rebuild is slower, but it is safer.&#160; <br />So as a general rule prefer Rebuild over Update for safety.     <br />Use Update when there are performance implications to consider.</p>
