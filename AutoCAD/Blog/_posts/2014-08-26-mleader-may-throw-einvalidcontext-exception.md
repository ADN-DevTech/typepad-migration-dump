---
layout: "post"
title: "MLeader may throw eInvalidContext exception"
date: "2014-08-26 13:34:37"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2014/08/mleader-may-throw-einvalidcontext-exception.html "
typepad_basename: "mleader-may-throw-einvalidcontext-exception"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>The MLeader object (and the equivalent AcDbMLeader in C++) support a block as the text component, as described at this <a href="http://adndevblog.typepad.com/autocad/2012/05/how-to-create-mleader-objects-in-net.html">previous blog post</a>. One of the required steps is set the BlockPosition property (or setBlockPosition in C++), but this may throw eInvalidException.</p>  <p>In fact this exception is expected if the extents of the block cannot be defined, for instance, if there are invisible attributes defined on it. </p>  <p>To avoid this, simply set the MLeader points with AddLeaderLine and/or AddFirsVertex before setting the block data.</p>
