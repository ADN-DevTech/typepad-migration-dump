---
layout: "post"
title: "Practise with Fusion 360 and Socket.IO – JavaScript API"
date: "2016-03-10 05:20:07"
author: "Xiaodong Liang"
categories:
  - "Javascript"
  - "View and Data API"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/03/practise-with-fusion-360-and-socketio-javascript-api.html "
typepad_basename: "practise-with-fusion-360-and-socketio-javascript-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>To investigate the ability how Fusion 360 API connects to IoT, I started to pick some relevant technologies to practice. I was imagining a scenario that a sensor produces data and transfers to Fusion 360, the data drives a specific parameter of a model. This is a video on what I have achieved.</p>
<p>&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="620" src="https://screencast.autodesk.com/Embed/Timeline/314afb48-80c6-4cac-a859-27404276bfa5" webkitallowfullscreen="" width="640"></iframe></p>
<p>Although I have not yet got a sensor, it proves a possibility of such scenario. The ‘sensor’ is simulated by a client on web or mobile. An Android app is also created, thus we can touch the screen to send the data. The source code and demo usage can be found at<br /><a href="https://github.com/xiaodongliang/Fusion360-SocketIO-Drive-Parameter">https://github.com/xiaodongliang/Fusion360-SocketIO-Drive-Parameter</a></p>
<p>From technical perspective, it is not a problem to drive parameter of Fusion model by API. While the first is how we transfer the data. I was attracted by Web Socket and Socket.IO. I am brand new with socket. This is some quotation from <a href="https://davidwalsh.name/websocket">this blog</a>:</p>
<p>&#39;<em>Socket.IO is a WebSocket API created by Guillermo Rauch, CTO of LearnBoost and lead scientist of LearnBoost Labs. Socket.IO will use feature detection to decide if the connection will be established with WebSocket, AJAX long polling, Flash, etc., making creating realtime apps that work everywhere a snap. Socket.IO also provides an API for Node.js which looks very much like the client side API.</em>&#39;</p>
<p>The <a href="http://socket.io/">official site of Socket.IO</a> provides nice tutorial to get started with. By the <a href="http://socket.io/get-started/chat/">guideline</a>, I created my server of Node.js quickly. The scenario assumes the server is listening to the data from client with the specific tag, say ‘&#39;fusion360&#39;’. It will achieve the a user name and the updated data value. Then it will emit the message to other listeners. Since Fusion 360 provides API to know who logged, the emitting will send the data with the user name. Thus only the Fusion instance of which current logged user is the specific account can respond to the data to drive the parameter. To simulate the data producing, I also created an client, an HTML page.</p>
<p><strong>Node.js server</strong>&#0160;</p>
<pre>var app = require(&#39;express&#39;)();
var server = require(&#39;http&#39;).Server(app);
var io = require(&#39;socket.io&#39;)(server);

server.listen(process.env.PORT || 3003);

// listen to the data from client
io.on(&#39;connection&#39;, function(socket){
  socket.on(&#39;fusion360&#39;, function(msg){	 
    console.log(&#39;message: &#39; + msg.user +&#39; &#39; + msg.newv);
    //convert string to number
    var recievedV = msg.newv * 1.0;
    //emit the value to other listeners
    io.emit(msg.user, recievedV);
  });
  console.log(&#39;connected&#39;);
});

// simulating the data producing on client
app.get(&#39;/driveparam&#39;, function (req, res) {
  res.sendfile(__dirname + &#39;/Fusion360/driveparam/index.html&#39;);
});<br /><br /></pre>
<p>Then, I deployed the sever to <a href="http://www.herokuapp.com/">Heroku</a>:&#0160;<a href="http://adnxdsocket.herokuapp.com/">http://adnxdsocket.herokuapp.com/</a>.</p>
<p><strong>Client to simulate data producing</strong> (some key lines)</p>
<pre>//import JS library of Socket.IO
&lt; script src=&quot;https://cdn.socket.io/socket.io-1.0.0.js&quot;&gt;&lt; / script&gt;

//initialize socket object. Input the sever URL which hosts socket transferring. 
var socket = io(&#39;http://adnxdsocket.herokuapp.com/&#39;);

//When the client produces the updated data, the code builds a json to emit to the server.
//user name
var user = $(&#39;#txtUser&#39;).val();
//new data
var newv = targetRad/iniRad;
//emit to server			
socket.emit(&#39;fusion360&#39;, {user:user,newv:newv});

//The verify the sending and listening, the client also set a listener and shows the data which is emited from the sever .
socket.on($(&#39;#txtUser&#39;).val(), function(msg){
			  $(&#39;#txtValue&#39;).val(msg); });

</pre>
<p>Now, the workflow of emitting and listening are ready. We can verify by playing the client to see the updating value in the textbox. Next, we just need to copy the workflow of client to fusion 360.</p>
<p>As we know, Fusion 360 provides Javascript API, with which, we can migrate the code pretty easily.</p>
<ol>
<li>Create an Add-in of JS.</li>
<li>Import JS library of Socket.IO at *.html</li>
</ol>
<pre>&lt; script type=&quot;text/javascript&quot; charset=&quot;UTF-8&quot; src=&quot;RemoteDriveParam.js&quot;&gt;&lt; / script&gt;<br />
<script src="https://cdn.socket.io/socket.io-1.0.0.js"></script>
</pre>
<p>&#0160; &#0160; &#0160; &#0160;3. In *.js , define the a dialog which contains some controls.</p>
<p>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; Combox: parameters list<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; Checkebox: enable or diable listening <br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;Textbox: display current value of parameter</p>
<p>&#0160; &#0160; &#0160; 4. In the Input changed handler, define the logic when listening is enabled or not.</p>
<p>&#0160;</p>
<pre>if(isToggleSocket){
	whichParamInput.isEnabled = false;
	
	
	initialV = currentParamValueInput.value;
	if(socketObj == null)
		socketObj =  io(socketServerURL);
	
	if(socketObj){	
		 socketObj.off(userName);	

		 //watch the data that is emitted with the message of this specific user.
		 socketObj.on(userName, function(msg){ 
		 
			//update the parameter with the data as a percentage of the initial data. 
			currentP.value = initialV * msg;
			args.isValidResult = true;
			//refresh the screen to see the update
			var app = adsk.core.Application.get();
			app.activeViewport.refresh(); 

			//looks there is an issue when updating the input in socket 
			//currentParamValueInput.value =  currentP.value ;

		});     
	}
}
else{
	//disable listening
	whichParamInput.isEnabled = true; 
	args.isValidResult = true;
	if(socketObj)
		socketObj.off(userName);
}
</pre>
<p><strong>Issues</strong>:</p>
<p>There is one issue at Fusion &#0160;API side. In the event of Socket, the parameter is updated, but Fusion will freeze if updating other control of the dialog (say that textbox). I think&#0160;this might be a problem of message pump. I am working on this issue.</p>
