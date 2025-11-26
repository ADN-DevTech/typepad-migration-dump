---
layout: "post"
title: "View and Data API FAQs"
date: "2016-04-18 23:57:17"
author: "Shiya Luo"
categories:
  - "MongoDB"
  - "Shiya Luo"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/04/view-and-data-api-faqs.html "
typepad_basename: "view-and-data-api-faqs"
typepad_status: "Publish"
---

<p>By <a href="https://twitter.com/ShiyaLuo">@ShiyaLuo</a></p>

<p>Here are some frequently asked questions about the View and Data API. They're available somewhere in the docs, but I'm going to summarize them again here. I've been getting them from emails, but I thought these questions are general enough they should be searchable somewhere.</p>

<ol>
<li><p>How to define the deletion time of original uploaded models?</p>

<p>The bucket retention policy you defined will determine when the files are deleted. They are:</p>

<ul>
<li>Transient (24 hours)</li>
<li>Temporary (30 days)</li>
<li>Persistent (until deleted)</li>
</ul>

<p>The bucket retention policy determines when the <em>files in the buckets</em> are deleted, <em>not the buckets</em>.</p></li>
<li><p>If something is updated on the server side, does it appear on the client side as well?</p>

<p>No. You have to retranslate them for changes to appear. Whatever translated will remain the same.</p></li>
<li><p>How to force delete the client side data?</p>

<p>See the docs at <a href="https://developer.autodesk.com/api/view-and-data-api/#delete-data">Delete Data</a></p></li>
<li><p>When I upload a file, how can I hide what the user can see in the properties panel?</p>

<p>You can write your own properties panel. However, if you absolutely don't want anyone to see those files, your best bet modify them before you upload. It's harder to modify the translated files since there's no GUI (software like Revit) to edit those file.</p></li>
<li><p>Why do your tutorials use NoSQL database, can I use a relational database to store stuff?</p>

<p>The model object tree or instance tree exist in the form of a JSON object. It's much easier to directly save a JSON object to a NoSQL database. We recommend a NoSQL database for ease of use, but do what you prefer. If your use case calls for heavy relational operation, feel free to use a SQL database.</p></li>
</ol>
