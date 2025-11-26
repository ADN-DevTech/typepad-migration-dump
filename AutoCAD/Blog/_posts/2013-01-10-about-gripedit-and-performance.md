---
layout: "post"
title: "About Gripedit and performance"
date: "2013-01-10 11:30:13"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/about-gripedit-and-performance.html "
typepad_basename: "about-gripedit-and-performance"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>On the ObjectARX SDK headers, we may notice that the implementation of 'Gripedit' is using the clone() mechanism during dragging. This means, that each time when the cursor is moved, AutoCAD performs new clones of the 'gripped' entities and throws this clones away immediately afterward. </p>  <p>This mechanism avoid loose precision. That's why we re-initialize the temporary copy each time before we call its moveGripPointsAt method, and pass the 'full' transformation from the original entity to the current position (and not just an incremental transformation).</p>  <p>In order to properly initialize the temporary object, we continuously create the temp copy and call its constructor. Then we call dwgOutFields on the original entity, pass the result into dwgInFields of the temporary copy, call the temps moveGripPointsAt method, call its worldDraw, and delete it afterwards.</p>  <p>Our heap manager is smart enough to always re-use the same memory for these temporary objects, so this mechanism is pretty efficient - basically, the local AcDbObject delete operator keeps the pointer which the new operator will return on the next call.</p>  <p>Beside this, you can do many other things to speed up the cloning process:</p>  <p>1. Make your entity's default constructor as simple (as fast) as possible.</p>  <p>2. In your dwgInFields/dwgOutFields implementation, check for kCopyFiler, kDeepCloneFiler and kWblockCloneFiler filer types. These filer types indicate an in-memory copy operation. In these cases, just write out your this pointer during dwgOutFields. In dwgInFields, read the pointer, and copy the entity's state directly by dereferencing the pointer. I used this technique in the Amodeler sample 'Asdkbodyapp', and it improved performance a lot.</p>  <p>3. Use partial undo. This way, you'll get rid of the file type 'kUndoFiler' during dwgInFields / dwgOutFields.</p>
