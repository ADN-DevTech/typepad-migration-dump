---
layout: "post"
title: "Passport Local Strategy + Autodesk Access Control Management APIs"
date: "2016-03-21 01:53:31"
author: "Shiya Luo"
categories:
  - "Database"
  - "NodeJS"
  - "Shiya Luo"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/03/passport-local-strategy-autodesk-access-control-management-apis.html "
typepad_basename: "passport-local-strategy-autodesk-access-control-management-apis"
typepad_status: "Draft"
---

<p>I wrote a sample&#0160;on how to use the Access Control Management APIs that lets you grant and deny different levels of access to other users.&#0160;</p>
<p>Here&#39;s what we will build in this tutorial:</p>
<ul>
<li>Local account sign up and log in</li>
<li>Autodesk Authentication</li>
<li>Calling the access control management APIs</li>
</ul>
<p>Here is what the login page will look like:</p>
<p><a class="asset-img-link" href="http://a5.typepad.com/6a01bb07c14d1d970d01b7c8273d95970b-pi" style="display: inline;"><img alt="Screen Shot 2016-03-21 at 4.46.02 PM" class="asset  asset-image at-xid-6a01bb07c14d1d970d01b7c8273d95970b img-responsive" src="/assets/image_478c00.jpg" title="Screen Shot 2016-03-21 at 4.46.02 PM" /></a></p>
<pre><code>.
├── Procfile
├── README.md
├── app
│&#0160;&#0160; ├── api-requests.js
│&#0160;&#0160; ├── models
│&#0160;&#0160; │&#0160;&#0160; ├── adsk-properties.js
│&#0160;&#0160; │&#0160;&#0160; └── user.js
│&#0160;&#0160; └── routes.js
├── config
│&#0160;&#0160; ├── database.js
│&#0160;&#0160; └── passport.js
├── gulpfile.js
├── index.js
├── package.json
└── views
    ├── login.ejs
    ├── profile.ejs
    └── static
        ├── css
        ├── fonts
        └── js
</code></pre>
<p>The /css, /fonts and /js files are just bootstrap files. Download the latest bootstrap, or just use a CDN. It won&#39;t effect this project much.</p>
