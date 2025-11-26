---
layout: "post"
title: "DBEnvironmentProperties in 'serverconfig.ini' of AIMS 2013"
date: "2012-05-03 02:17:30"
author: "Partha Sarkar"
categories:
  - "AIMS 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/dbenvironmentproperties-in-serverconfigini-of-aims-2013.html "
typepad_basename: "dbenvironmentproperties-in-serverconfigini-of-aims-2013"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you have installed AIMS 2013, it&#39;s worth taking a look into the &#39;Server Configuration File&#39; (<strong>serverconfig.ini</strong>) and you will see a new section &quot;<strong>DBEnvironmentProperties</strong>&quot; added to 2013 release. Let me copy the relevant section as-it-is from the 2013 release of ‘serverconfig.ini’ -</p>
<p>[DBEnvironmentProperties]</p>
<p># ****************************************************************</p>
<p># DB ENVIRONMENT</p>
<p># This section is to set DB Environment.</p>
<p># Determine the best sizes to improve the Server performance.</p>
<p># Property Name&#0160;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Description</p>
<p># -----------------------------------------------------------------------------</p>
<p># LibraryCacheSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; The size for library cache size in MB</p>
<p># SessionCacheSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; The size for session cache size in MB</p>
<p># DBPageSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; The size for library DB page in KB</p>
<p># DBXMLPageSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; The size for library DBXML page in KB</p>
<p># LibraryLogBufferSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; The size for library log buffer in MB</p>
<p># SessionLogBufferSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; The size for session log buffer in MB</p>
<p># DBMaxTransactions&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; The max number of DB transactions</p>
<p># SessionDBPageSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; The size for session DB page in KB</p>
<p># SessionDBXMLPageSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; The size for session DBXML page in KB</p>
<p># DBTimeout&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;The time out for lock and transaction in second</p>
<p># DBMaxLockers&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; The max number of DB lockers</p>
<p># ***************************************************************</p>
<p>LibraryCacheSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;= 32</p>
<p>SessionCacheSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;= 2</p>
<p>DBPageSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;= 32</p>
<p>DBXMLPageSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160; &#0160;= 32</p>
<p>LibraryLogBufferSize &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; = 12</p>
<p>SessionLogBufferSize&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = 1</p>
<p>DBMaxTransactions&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = 1000</p>
<p>SessionDBPageSize&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;= 2</p>
<p>SessionDBXMLPageSize &#0160; &#0160; &#0160; &#0160; &#0160; = 0.5</p>
<p>DBTimeout&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;= 0.2</p>
<p>DBMaxLockers&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;= 1000&#0160;</p>
<p>#***************************************************************</p>
<p>If you have encountered an error message / exception like this - &#39;<em>DB_LOG_BUFFER_FULL :In-memory log buffer is full</em>&#39; while uploading large package (mgp files) in 2012 or earlier release of MapGuide Enterprise, you could try changing the values of DBMaxLockers and DBMaxTransactions. Increasing the default values of these variables might help to resolve that exception in your setup.<br /><br />These new set of variables are from Berkley DB settings. In &#39;serverconfig.ini&#39; (AIMS 2013) you will see a default value is assigned to them. These default values do not suggest any minimum / maximum range as every situation / configuration will be unique to your requirement and environment. You should try changing them and setting values what is appropriate in your setup / environment for your application. I would suggest you to keep a copy of &#39;serverconfig.ini&#39; before you start changing the values in it. &#0160;And, rememeber to restart the service to make the changes effective.</p>
