---
layout: "post"
title: "Working out Entity Translation data from Normal AutoCAD Commands"
date: "2012-12-11 16:18:05"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/working-out-entity-translation-data-from-normal-autocad-commands.html "
typepad_basename: "working-out-entity-translation-data-from-normal-autocad-commands"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>How do I get the arguments to the AutoCAD-commands COPY, MOVE, MIRROR, and so on, via ObjectARX. With an editor-reactor we have been able to control the events &quot;commandWillStart&quot; and &quot;commandEnded&quot;, but how can I get the translation-vector of the MOVE- or COPY-command at the &quot;commandEnded&quot;-event?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>If your own custom entities are being translated, you will receive the   <br />transformation matrix in their overloaded transformBy().</p>  <p>If they are not your entities, you can use one of these three methods:</p>  <p><strong>Method #1</strong>    <br />Attach an object reactor - either transient or persistent - to all of the    <br />entities in the drawing.</p>  <p><strong>Method #2     <br /></strong>Use a database reactor to find out when an entity is opened for modify, and when    <br />the modifications have actually been performed.</p>  <p><strong>Method #3</strong>    <br />Check the extended entity-data that is attached to the entities in order to see    <br />how they have been transformed.</p>  <p>The first method is laborious, and is functionally equivalent to the second. </p>  <p>The second method is much cleaner. You could initially set a flag in your   <br />COPY/MOVE commandWillStart method, indicating that a command you want to react to is current.    <br /></p>  <p>You should overload the following virtual functions in your database reactor:   <br /><font size="1"><strong>       <br />virtual void AcDbDatabaseReactor::objectOpenedForModify ( const AcDbDatabase*        <br />dwg, const AcDbObject* dbObj )        <br /> virtual void AcDbDatabaseReactor::objectModified ( const AcDbDatabase* dwg,        <br />const AcDbObject* dbObj )        <br /></strong></font></p>  <p>In the objectOpenedForModify() you would check whether a COPY or MOVE is active, by testing the flag I mentioned and also whether the entity being modified is one you want. Then, you would query its location and cache this in memory. On the second notification, objectModified, you would check its location, and from here you can calculate the translation.</p>  <p>In the case of a COPY, you will get an objectModified() notification when the   <br />copy is initially appended to the database (with the original's position) and    <br />then receive objectOpenedForModify() (again with the original position) and a    <br />further objectModified() with the new position.</p>  <p>With the third method, you would attach XData to the entities you want to watch as certain XData point codes get transformed along with the parent entity. </p>  <p>e.g.</p>  <p>- a world space position (group code 1011) for simple translations.   <br />- up to three world directions (group code 1013) for more complex    <br />transformations. The world directions should correspond to the three axes, and    <br />allow you to find more detailed information about the exact transformation.</p>  <p>Then on the commandEnded() notification you would check to see how this data has   <br />been transformed. This approach is complimentary to the second: you can use the    <br />database reactor to find the entities being modified and then query their XData    <br />to discover the precise changes.</p>
