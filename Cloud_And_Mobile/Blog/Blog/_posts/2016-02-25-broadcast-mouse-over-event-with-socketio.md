---
layout: "post"
title: "Broadcast Mouse Over event with Socket.IO"
date: "2016-02-25 10:45:53"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Javascript"
  - "NodeJS"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/02/broadcast-mouse-over-event-with-socketio.html "
typepad_basename: "broadcast-mouse-over-event-with-socketio"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>The post from last week, <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/02/changing-gethitpoint-to-return-dbid.html">about Mouse Over</a>, was a first step to create a sample with <a href="http://socket.io">Socket.IO library</a>&#0160;for NodeJS. The main idea is: a &#39;master&#39; user will have a 3D model with the location of each &#39;participant&#39;. Once the &#39;master&#39; move around, the&#0160;<em>mouse over</em> event should notify each &#39;participant&#39;. So we need a controlling server, a &#39;master&#39; page with the 3D model and, finally, a &#39;participant&#39; page that only received the notification.</p>
<p>First of all, call&#0160;<strong>npm install socket.io&#0160;</strong>and it should be ready to go.</p>
<p>The server side should have (server.js)&#0160;</p>

<div>
<pre style="margin: 0; line-height: 125%;"><span style="color: #008000; font-weight: bold;">var</span> express <span style="color: #666666;">=</span> require(<span style="color: #ba2121;">&#39;express&#39;</span>);
<span style="color: #008000; font-weight: bold;">var</span> http <span style="color: #666666;">=</span> require(<span style="color: #ba2121;">&#39;http&#39;</span>);

<span style="color: #408080; font-style: italic;">// prepare the socket.io listener</span>
<span style="color: #008000; font-weight: bold;">var</span> app <span style="color: #666666;">=</span> express();
<span style="color: #008000; font-weight: bold;">var</span> server <span style="color: #666666;">=</span> http.createServer(app);
<span style="color: #008000; font-weight: bold;">var</span> io <span style="color: #666666;">=</span> require(<span style="color: #ba2121;">&#39;socket.io&#39;</span>).listen(server);

<span style="color: #408080; font-style: italic;">// and the port number: FORGE - 36743 :-)</span>
server.listen(<span style="color: #666666;">36743</span>);

<span style="color: #408080; font-style: italic;">// now the messages</span>
io.on(<span style="color: #ba2121;">&#39;connection&#39;</span>, <span style="color: #008000; font-weight: bold;">function</span> (socket) {

    <span style="color: #408080; font-style: italic;">// this handles the JOIN message</span>
    socket.on(<span style="color: #ba2121;">&#39;join&#39;</span>, <span style="color: #008000; font-weight: bold;">function</span> (data) {
        <span style="color: #408080; font-style: italic;">// let&#39;s record each user by their email</span>
        socket.join(data.email);
    });

    <span style="color: #408080; font-style: italic;">// this handle the MOUSE OVER message, from the V&amp;D</span>
    socket.on(<span style="color: #ba2121;">&#39;mouseover&#39;</span>, <span style="color: #008000; font-weight: bold;">function</span> (data) {
        <span style="color: #008000; font-weight: bold;">var</span> connectedUser <span style="color: #666666;">=</span> io.sockets.<span style="color: #008000; font-weight: bold;">in</span>(data.email);
        <span style="color: #008000; font-weight: bold;">if</span> (connectedUser <span style="color: #666666;">!=</span> <span style="color: #008000; font-weight: bold;">null</span>)
            connectedUser.emit(<span style="color: #ba2121;">&#39;newhighlight&#39;</span>, {
                email <span style="color: #666666;">:</span> data.email
            });
    });
});
</pre>
</div>
<p>Now the master client-side script, starting from <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/02/changing-gethitpoint-to-return-dbid.html">the previous highlight post</a>: call emitMessage from onMouseOver passing the dbId.</p>
<p>Note you&#39;ll need a 3D model with a property that contains the emails you&#39;re expecting (or any other information that indicates where the mouse is):</p>

<div>
<pre style="margin: 0; line-height: 125%;"><span style="color: #008000; font-weight: bold;">var</span> _lastEmail <span style="color: #666666;">=</span> <span style="color: #ba2121;">&#39;&#39;</span>;

<span style="color: #008000; font-weight: bold;">function</span> emitMessage(dbId) {
    _viewer.model.getProperties(dbId, <span style="color: #008000; font-weight: bold;">function</span> (props) {
        props.properties.forEach(<span style="color: #008000; font-weight: bold;">function</span> (prop) {
            <span style="color: #008000; font-weight: bold;">if</span> (prop.displayName.indexOf(<span style="color: #ba2121;">&#39;email&#39;</span>) <span style="color: #666666;">&gt;=</span> <span style="color: #666666;">0</span>) {
                <span style="color: #008000; font-weight: bold;">if</span> (prop.displayValue <span style="color: #666666;">!=</span> _lastEmail) { <span style="color: #408080; font-style: italic;">// avoid duplicated messages</span>
                    _lastEmail <span style="color: #666666;">=</span> prop.displayValue;

                    <span style="color: #408080; font-style: italic;">// sent the message!</span>
                    <span style="color: #008000; font-weight: bold;">var</span> socket <span style="color: #666666;">=</span> io.connect(<span style="color: #ba2121;">&#39;http://localhost:36743&#39;</span>);
                    socket.emit(<span style="color: #ba2121;">&#39;mouseover&#39;</span>, {
                        email<span style="color: #666666;">:</span> _lastEmail;
                    });
                }
            }
        });
    });
}
</pre>
</div>
<p>Finally the &#39;participant&#39; that receives the message:</p>

<div>
<pre style="margin: 0; line-height: 125%;"><span style="color: #bc7a00;">&lt;!DOCTYPE html&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;html&gt;</span>

<span style="color: #008000; font-weight: bold;">&lt;head&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;title&gt;</span>Socket.IO sample<span style="color: #008000; font-weight: bold;">&lt;/title&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;meta</span> <span style="color: #7d9029;">name=</span><span style="color: #ba2121;">&quot;viewport&quot;</span> <span style="color: #7d9029;">content=</span><span style="color: #ba2121;">&quot;width=device-width, initial-scale=1&quot;</span><span style="color: #008000; font-weight: bold;">&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;link</span> <span style="color: #7d9029;">rel=</span><span style="color: #ba2121;">&quot;stylesheet&quot;</span> <span style="color: #7d9029;">href=</span><span style="color: #ba2121;">&quot;/css/bootstrap.min.css&quot;</span><span style="color: #008000; font-weight: bold;">&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;style&gt;</span>
        <span style="color: #0000ff; font-weight: bold;">.vertical-center</span> {
            <span style="color: #008000; font-weight: bold;">min-height</span><span style="color: #666666;">:</span> <span style="color: #666666;">100%</span>;
            <span style="color: #408080; font-style: italic;">/* Fallback for browsers do NOT support vh unit */</span>
            <span style="color: #008000; font-weight: bold;">min-height</span><span style="color: #666666;">:</span> <span style="color: #666666;">100</span>vh;
            <span style="color: #408080; font-style: italic;">/* These two lines are counted as one :-)       */</span>
            <span style="color: #008000; font-weight: bold;">display</span><span style="color: #666666;">:</span> flex;
            align<span style="color: #666666;">-</span>items<span style="color: #666666;">:</span> <span style="color: #008000; font-weight: bold;">center</span>;
        }
    <span style="color: #008000; font-weight: bold;">&lt;/style&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;/head&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;script </span><span style="color: #7d9029;">src=</span><span style="color: #ba2121;">&quot;/js/jquery.min.js&quot;</span><span style="color: #008000; font-weight: bold;">&gt;&lt;/script&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;script </span><span style="color: #7d9029;">src=</span><span style="color: #ba2121;">&quot;/js/bootstrap.min.js&quot;</span><span style="color: #008000; font-weight: bold;">&gt;&lt;/script&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;script </span><span style="color: #7d9029;">src=</span><span style="color: #ba2121;">&quot;/socket.io/socket.io.js&quot;</span><span style="color: #008000; font-weight: bold;">&gt;&lt;/script&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;script&gt;</span>
    $(<span style="color: #008000;">document</span>).ready(<span style="color: #008000; font-weight: bold;">function</span> () {
        <span style="color: #008000; font-weight: bold;">var</span> socket <span style="color: #666666;">=</span> io.connect(<span style="color: #ba2121;">&#39;http://localhost:36743&#39;</span>);

        <span style="color: #408080; font-style: italic;">// join the broadcast</span>
        $(<span style="color: #ba2121;">&#39;#join&#39;</span>).click(<span style="color: #008000; font-weight: bold;">function</span> () {
            socket.emit(<span style="color: #ba2121;">&#39;join&#39;</span>, {
                seat<span style="color: #666666;">:</span> $(<span style="color: #ba2121;">&#39;#email&#39;</span>).val()
            });
            $(<span style="color: #ba2121;">&#39;#inputForm&#39;</span>).hide();
        });

        <span style="color: #408080; font-style: italic;">// message received!</span>
        socket.on(<span style="color: #ba2121;">&#39;newhighlight&#39;</span>, <span style="color: #008000; font-weight: bold;">function</span> (data) {
            alert(<span style="color: #ba2121;">&#39;you are on the spot &#39;</span> <span style="color: #666666;">+</span> data.email);
        });
    });
<span style="color: #008000; font-weight: bold;">&lt;/script&gt;</span>

<span style="color: #008000; font-weight: bold;">&lt;body&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;div</span> <span style="color: #7d9029;">class=</span><span style="color: #ba2121;">&quot;vertical-center&quot;</span><span style="color: #008000; font-weight: bold;">&gt;</span>
        <span style="color: #008000; font-weight: bold;">&lt;div</span> <span style="color: #7d9029;">id=</span><span style="color: #ba2121;">&quot;inputForm&quot;</span> <span style="color: #7d9029;">class=</span><span style="color: #ba2121;">&quot;large container-fluid&quot;</span><span style="color: #008000; font-weight: bold;">&gt;</span>
            <span style="color: #008000; font-weight: bold;">&lt;input</span> <span style="color: #7d9029;">type=</span><span style="color: #ba2121;">&quot;text&quot;</span> <span style="color: #7d9029;">id=</span><span style="color: #ba2121;">&quot;email&quot;</span> <span style="color: #7d9029;">class=</span><span style="color: #ba2121;">&quot;form-control&quot;</span> <span style="color: #7d9029;">placeholder=</span><span style="color: #ba2121;">&quot;Enter your email&quot;</span> <span style="color: #008000; font-weight: bold;">/&gt;</span>
            <span style="color: #008000; font-weight: bold;">&lt;button</span> <span style="color: #7d9029;">id=</span><span style="color: #ba2121;">&quot;join&quot;</span> <span style="color: #7d9029;">class=</span><span style="color: #ba2121;">&quot;btn-lg btn-primary&quot;</span><span style="color: #008000; font-weight: bold;">&gt;</span>Join<span style="color: #008000; font-weight: bold;">&lt;/button&gt;</span>
        <span style="color: #008000; font-weight: bold;">&lt;/div&gt;</span>
    <span style="color: #008000; font-weight: bold;">&lt;/div&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;/body&gt;</span>
<span style="color: #008000; font-weight: bold;">&lt;/html&gt;</span>
</pre>
</div>
<p><span style="text-decoration: underline;"><strong><span style="color: #ff0033; text-decoration: underline;">Important</span></strong></span>: remember to replace &#39;<em>localhost</em>&#39; with your server address!&#0160;</p>
<p>And yes, this sample is using <a href="https://jquery.com/">jQuery</a> and <a href="http://getbootstrap.com/">Bootstrap</a>. If you haven&#39;t used them, check it out! It&#39;s a must! And <a href="http://stackoverflow.com/questions/26773767/purpose-of-installing-bootstrap-through-npm/35580597#35580597">here is how</a> server them from NPM installed packages.&#0160;</p>
<p>This still a work in progress, but soon I&#39;ll have more to show! Stay tuned!</p>
