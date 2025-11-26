---
layout: "post"
title: "London #AECHackathon, Revit 2016 API Leader Attachment"
date: "2015-07-23 12:20:23"
author: "Jaime Rosales"
categories: []
original_url: "https://adndevblog.typepad.com/aec/2015/07/london-aec-hackathon-revit-2016-api-leader-attachment.html "
typepad_basename: "london-aec-hackathon-revit-2016-api-leader-attachment"
typepad_status: "Draft"
---

<div>By Jaime Rosales (@afrojme)</div>
<div>&#0160;</div>
<div>This past weekend I had the opportunity &#0160;to go to the land of The Queen, the Beatles and 007. After the successful hackathon, I can now say a city of innovators and great hackers for the AEC industry in the UK as well. We started the week together with Adam Nagy (@adamthenagy), a VIP pre-hackathon workshop at our beautiful London office, a meetup to get the Hackathon participants a head start with the use of our View and Data API. The I love 3D meetup was a great success and the attendees had a wicked response to the technology. During the hackathon, 2 teams decided to use the View and Data API in order to create unique and wicked 3D and VR apps.</div>
<div>&#0160;</div>
<div>I had the chance to present to the entire audience the View and Data API as well, where a lot of interest and curiosity was sparked and I look forward to seeing the upcoming creations they make.</div>
<div>&#0160;</div>
<div>Here is the URL to the presentation if you will like to check it out.</div>
<div>http://jaimerosales.github.io/london-prehack/#/</div>
<div>&#0160;</div>
<div>The 5 Teams who compite to solve the many challenges had a successful wrap up at the end of the Hackathon, Here is a summary of who they were and what they worked on. &#0160;</div>
<div>&#0160;</div>
<div><strong>Team <a href="https://twitter.com/dwightxavier/status/622779850391191552" target="_blank">#Massive</a></strong> : Who created an app in order to Find London Hidden space to solve the problem of identification of development space via planning policy constrains. The app final result helped them identify the untapped potential for raising building heights.</div>
<div>&#0160;</div>
<div>
<div><strong>Team <a href="https://twitter.com/hashtag/Optioneers?src=hash" target="_blank">#Optioneers</a></strong>: Who focused on the challenge to help customers to make better choices of the house they buy. Their moto was &quot;Closing the loop between design options&quot;&#0160;Enabling the customer make better choices in a faster time and also enabling the engineers enhancing their models in a shorter time.</div>
<div>&#0160;</div>
<div><strong>Team <a href="https://twitter.com/hashtag/Gapathon?src=hash" target="_blank">#Gapathon</a></strong>: Who created an API in order to improve energy trust to analyse performance gaps. Their contributions were an open API, open source data consumer, data usability recommendation and also data visualization.&#0160;</div>
<div>&#0160;</div>
<div><strong>Team <a href="https://twitter.com/dwightxavier/status/622782325349351425" target="_blank">#Concur</a></strong>: With the use of View and Data API from Autodesk he used it in order to create an app that will&#0160;Improve the efficiency In context collaboration for the industry.&#0160;</div>
<div>&#0160;</div>
<div><strong>Team <a href="https://twitter.com/hashtag/GhostBIMsquad?src=hash" target="_blank">#GhostBIMSquad</a></strong>: A team with members from 16 years old to 50 years old, this team worked in such a synced collaboration creating a VR app, a Holographic image of their model, a&#0160;relational information databases, asset tagging, and a Bluetooth building navigation all during the weekend. Their best line during the presentation was &quot;I&#39;m sorry we don&#39;t have a power point presentation, we were too busy coding&quot;.&#0160;</div>
<div>&#0160;</div>
<div>Here is the list of the winners</div>
<div>&#0160;</div>
<div><strong>Best Overall Project:&#0160;Team Ghost BIM Squad</strong></div>
<div>
<p><strong>Best Open Source Project:&#0160;Team Optioneers</strong></p>
<p><strong>Innovate UK Hack Challenge&#0160;– Mind the Gap:&#0160;Team Gapathon</strong></p>
<p><strong>Autodesk VR and 3D Challenge:&#0160;Team Ghost BIM Squad</strong></p>
<p><strong>National BIM Service Challenge:&#0160;Team Massive</strong></p>
<p><strong>Future Cities Catapult Challenge:&#0160;Team Gapathon</strong></p>
<p><strong>Energy Savings Trust Challenge:&#0160;Team Gapathon</strong></p>
</div>
<div>&#0160;</div>
<div>It was a pleasure meeting everybody and congrats once again for a wicked weekend in London here is a link to the two hashtags that became top 3 trending during the weekend in London <a href="https://twitter.com/search?q=%23AECHackldn&amp;src=typd" target="_blank">#AECHackLDN </a><a href="https://twitter.com/hashtag/AECHackathon?src=hash" target="_blank">#AECHackathon </a>&#0160;</div>
<div>&#0160;</div>
<div>Now back to Revit API.&#0160;</div>
<div>&#0160;</div>
<div>This week a question was raised on our Forum which has been having a good amount of traffic and since also key players on the Revit Engineering team have access to this as well as other Revit API 3rd party developers the responses that come by are very interesting. Thanks to Arnošt Löbel&#0160;for his help with the following question.</div>
<div>&#0160;</div>
<div><strong>Question</strong>:&#0160;I&#39;m creating a new note, and needing to add a leader to it. This is updating code from a 2015 plugin to 2016. Previously I was using (where &#39;note&#39; is my TextNote.Create() object):
<pre> // Set the left and right leader attachments to Top
note.get_Parameter(&quot;Left Attachment&quot;).Set(0);
note.get_Parameter(&quot;Right Attachment&quot;).Set(0);</pre>
<p>That has become obsolete and using the new method, I&#39;m currently using:</p>
<pre>// Set the left and right leader attachments to Top
note.LeaderLeftAttachment.Equals(&quot;TopLine&quot;);
note.LeaderRightAttachment.Equals(&quot;TopLine&quot;);</pre>
<p>I pulled the &quot;TopLine&quot; from Revit Snoop. However, this is not making it into my text note when created in Revit. It is Top for the Left Attachment, but Bottom for Right Attachment. Is there a best time to pass these values (currently I do it after the leader&#39;s been created and before the command completes)?</p>
<p>Thanks in advance for any help.</p>
<p><strong>Answer</strong>: My sincere advice to you so to do less snooping. I understand that the Snoop is a popular tool and is occasionally necessary too, but at many times it does not do any good to you and your application.</p>
</div>
<div>As you can find in the RevitAPI.CHM file, text note has three methods related to Leaders. They somehow mirror the UI commands in Revit:<br />- AddLeader<br />- GetLeaders<br />- RemoveLeaders<br /><br />In your case you will probably be using:<br /><br />Leader leftLeader = myNote.AddLeader(TextNoetLeaderTypes.TNT_STRAIGHT_L);<br />and<br />Leader rightLeader = myNote.AddLeader(TextNoetLeaderTypes.TNT_STRAIGHT_R);<br /><br />The Add method returns you the newly created Leader object. You will notice, that just like in the UI, the above method add leaders with some default shape and position. However, you can use the returned leader object and using its method a properties adjust the appearance and position of each leader.<br /><br />For example:<br /><br />leftLeader.End = xyzPointSomewhereOnTheModel;<br /><br />I hope that makes it clear.</div>
<div>&#0160;</div>
<div>Thank you Arnost for the detailed answer to the developer.</div>
<div>Thanks for reading and until next time.&#0160;</div>
<div>
<h4>&#0160;</h4>
<p>&#0160;</p>
</div>
</div>
