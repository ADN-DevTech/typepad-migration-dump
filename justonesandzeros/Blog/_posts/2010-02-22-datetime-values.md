---
layout: "post"
title: "DateTime values"
date: "2010-02-22 08:43:32"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/02/datetime-values.html "
typepad_basename: "datetime-values"
typepad_status: "Publish"
---

<p><img src="/assets/TipsAndTricks2.png" /> </p> <p>DateTime is a simple concept as long as you don&#39;t think about it.&#0160; I guess that&#39;s true for most things, but I don&#39;t want to get too off topic here.&#0160; When it comes to DateTime mixed with thinking, some annoying questions come up.&#0160; Such as...</p> <p><em>&quot;What happens when the Vault server is in a different time zone as the client?&quot;</em></p> <p><em>&quot;How do cultural settings affect the DateTime value?&#0160; (Example:&#0160; 01/31/2010 versus&#0160; 31.01.2010)&quot;</em></p> <br /> <p>There are basically two ways that Vault handles DateTime values.&#0160; Which way is used depends on how the information is being passed to or from the server.</p> <p><strong>Mechanism 1:&#0160; DateTime objects</strong></p> <p>If you are dealing with a DateTime object, life is simple.&#0160; The object will always reflect the local time.&#0160; Things like leap year and daylight savings time are automatically taken into account.&#0160; If the client and server are in different time zones, the web services framework automatically converts things for you.</p> <p>Example:&#0160; File.CreateDate. This value will always be the time in your local time zone.&#0160; Even if somebody from another time zone created the File, you will see the CreateDate as if the File was created from your time zone.</p> <p><strong>Mechanism 2: DateTime strings</strong></p> <p>Sometimes the DateTime value needs to be passed as a string.&#0160; In this case, the web services framework will not help you.&#0160; The Vault solution is to convert to Universal Time then write out the value in a specific format. Specifically, &quot;MM/dd/yyyy HH:mm:ss&quot; is the time format.&#0160; It&#39;s OK if this format doesn&#39;t match your current culture&#39;s format.&#0160; The point is for the Vault client and server to use a common format.</p> <p>Example:&#0160; When doing a search on a DateTime value, you need to call ToUniversalTime().ToString(&quot;MM/dd/yyyy HH:mm:ss&quot;) on the DateTime object you are using.&#0160; PartialMirrorCommand.cs in the Vault Mirror sample has an example of this search. </p>
