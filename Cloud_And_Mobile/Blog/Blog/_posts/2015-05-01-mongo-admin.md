---
layout: "post"
title: "A MongoDB administration tool using Node and Angular"
date: "2015-05-01 01:27:37"
author: "Philippe Leefsma"
categories:
  - "Javascript"
  - "Philippe Leefsma"
  - "Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/05/mongo-admin.html "
typepad_basename: "mongo-admin"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>

<a href="https://github.com/Developer-Autodesk/mongo-admin">Source on github</a>
<br/>
<br/>
<p>Today's post is featuring one of my latest sample: a simple administration console I created to interact with a local or remote mongo database. One of my main motivation to create that sample was to be able to easily import records into an existing database, typically in order to migrate my data from a local db to a cloud hosted db such as <a title="mongolab" href="https://mongolab.com/" target="_self">mongolab</a>.</p>
<p>Before going through the code highlights, let's take a look at the main features of the tool:</p>
<p>Once you specified the correct credentials and database information - more on that later - you can browse the list of collections existing in that database, you also have the possibility to create new collections.</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d10c728b970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d10c728b970c image-full img-responsive" title="Collections management" src="/assets/image_50e270.jpg" alt="Collections management" border="0" /></a></p>
<p>Once you selected a collection a treeview control lets you visualize the content of your collection:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0826f01c970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0826f01c970d image-full img-responsive" title="Treeview" src="/assets/image_1f76ff.jpg" alt="Treeview" border="0" /></a></p>
<p>You can drag and drop a file containing some json formatted data in order to import the records into the current collection. Options are available through the context menu:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0826f063970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0826f063970d image-full img-responsive" title="Imports" src="/assets/image_31ceec.jpg" alt="Imports" border="0" /></a></p>
<p>From the selected collection context menu, it is possible to edit or suppress existing records:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0826f07a970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0826f07a970d image-full img-responsive" title="Edit and suppress records" src="/assets/image_0fe1db.jpg" alt="Edit and suppress records" border="0" /></a></p>
<p>In order to edit records, which are json objects, I used a pretty neat JavaScript component, the <a href="https://github.com/josdejong/jsoneditor/" target="_self">jsoneditor</a>:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c782ea64970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c782ea64970b image-full img-responsive" title="Json editor" src="/assets/image_e8314e.jpg" alt="Json editor" border="0" /></a></p>
<p>Finally displayed records can be filtered, that's a filter applied on the frontend treeview control or a mongo query can be performed directly on the database. You can also edit the query with the same jsoneditor control, just double-click the query input field:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0826f12e970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0826f12e970d img-responsive" title="Filter" src="/assets/image_614d4f.jpg" alt="Filter" border="0" /></a></p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0826f132970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0826f132970d image-full img-responsive" title="Query" src="/assets/image_d9a5e6.jpg" alt="Query" border="0" /></a></p>
<p>As you can see the sample is rather basic but I'm planning adding features along the way as I need it.
<p>Let's take a look at few code highlights now:</p>
<p>The first thing I did differently from the past in that project was to implement a cleaner structure for my backend Node APIs. I've got two APIs collections and items which are split in two different files:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d10c741c970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d10c741c970c img-responsive" title="APIs" src="/assets/image_d99ec6.jpg" alt="APIs" border="0" /></a></p>

<p>Each API needs a database access, so in order to avoid duplicating the code in each API file, I isolated the connection in another file: dbConnector.js:</p>



<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> mongo = require(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'mongodb'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> util = require(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'util'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 
 4 </span><span style="background-color:#ffffff;">module.exports = {
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 
 6 </span><span style="background-color:#ffffff;">  initializeDb: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(config, onSuccess, onError){
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 
 8 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> url = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">''</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 
10 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(config.user.length && config.pass.length) {
</span><span style="color:#800000;background-color:#f0f0f0;">11 
12 </span><span style="background-color:#ffffff;">      url = util.format(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'mongodb://%s:%s@%s:%d/%s'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">        config.user,
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">        config.pass,
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">        config.host,
</span><span style="color:#800000;background-color:#f0f0f0;">16 </span><span style="background-color:#ffffff;">        config.port,
</span><span style="color:#800000;background-color:#f0f0f0;">17 </span><span style="background-color:#ffffff;">        config.db);
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">else</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;">20 
21 </span><span style="background-color:#ffffff;">      url = util.format(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'mongodb://%s:%d/%s'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">        config.host,
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">        config.port,
</span><span style="color:#800000;background-color:#f0f0f0;">24 </span><span style="background-color:#ffffff;">        config.db);
</span><span style="color:#800000;background-color:#f0f0f0;">25 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">26 
27 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> client = mongo.MongoClient;
</span><span style="color:#800000;background-color:#f0f0f0;">28 
29 </span><span style="background-color:#ffffff;">    client.connect(url, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(err, db) {
</span><span style="color:#800000;background-color:#f0f0f0;">30 
31 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(err) {
</span><span style="color:#800000;background-color:#f0f0f0;">32 
33 </span><span style="background-color:#ffffff;">        onError(err);
</span><span style="color:#800000;background-color:#f0f0f0;">34 </span><span style="background-color:#ffffff;">      }
</span><span style="color:#800000;background-color:#f0f0f0;">35 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">else</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;">36 
37 </span><span style="background-color:#ffffff;">        console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"Connected to "</span><span style="background-color:#ffffff;"> + config.db);
</span><span style="color:#800000;background-color:#f0f0f0;">38 
39 </span><span style="background-color:#ffffff;">        onSuccess(db);
</span><span style="color:#800000;background-color:#f0f0f0;">40 </span><span style="background-color:#ffffff;">      }
</span><span style="color:#800000;background-color:#f0f0f0;">41 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">42 </span><span style="background-color:#ffffff;">  }
</span><span style="color:#800000;background-color:#f0f0f0;">43 </span><span style="background-color:#ffffff;">}</span></pre>


Using dbConnector in each API section looks as follow:
<br>
<br>
<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> dbConnector = require(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'../../dbConnector'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> config = require(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'../../config-server'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 
 4 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> db = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 
 6 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Initializes database connection
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">dbConnector.initializeDb(config,
</span><span style="color:#800000;background-color:#f0f0f0;">11 
12 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(databse){
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">    db = databse;
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">  },
</span><span style="color:#800000;background-color:#f0f0f0;">15 
16 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(error){
</span><span style="color:#800000;background-color:#f0f0f0;">17 </span><span style="background-color:#ffffff;">    console.log(error);
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">  });</span></pre>

Having the APIs split in multiple routes makes it pretty clean to handle using Express, here is a part of the server section:
<br>
<br>
<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> collectionsApi = require(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'./routes/api/collections'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> itemsApi = require(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'./routes/api/items'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> express = require(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'express'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 
 5 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> app = express();
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//API routes
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 </span><span style="background-color:#ffffff;">app.use(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'/node/mongo-admin/api/collections'</span><span style="background-color:#ffffff;">, 
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">  collectionsApi);
</span><span style="color:#800000;background-color:#f0f0f0;">10 
11 </span><span style="background-color:#ffffff;">app.use(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'/node/mongo-admin/api/items'</span><span style="background-color:#ffffff;">, 
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">  itemsApi);</span></pre>

On the frontend side, even if the project doesn't have that many files, I tried to use a clean and modular structure, following Angular approach. It will be easy to extend the code in a modular way, with for example multiple people working a different files:

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d10c74af970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d10c74af970c img-responsive" alt="Frontend" title="Frontend" src="/assets/image_9e968e.jpg" border="0" /></a>
<br />
<br />
If you want to test the project on your side, the prerequisites are either to install a local instance of <a href="https://www.mongodb.org/">MongoDb</a> or sign up for a cloud hosted mongo service like <a href="https://mongolab.com/">mongolab</a> for example. 

You would then edit the config-server.js file in the project with appropriate credentials. 

Let's mention that mongolab provide a free tier for any hosted data below 500Mb, so it worth trying:
<br />
<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0826f243970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0826f243970d image-full img-responsive" alt="Mongolab free tier" title="Mongolab free tier" src="/assets/image_00d09b.jpg" border="0" /></a><br />
