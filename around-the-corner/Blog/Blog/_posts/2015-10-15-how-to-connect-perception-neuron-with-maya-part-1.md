---
layout: "post"
title: "How to connect Perception Neuron with Maya (Part 1)"
date: "2015-10-15 15:13:00"
author: "Zhong Wu"
categories:
  - "Maya"
  - "Reality capture"
  - "Visual Studio"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2015/10/how-to-connect-perception-neuron-with-maya-part-1.html "
typepad_basename: "how-to-connect-perception-neuron-with-maya-part-1"
typepad_status: "Publish"
---

<p>Last year, our team backed project Perception Neuron with 2 full set of Neuron Motion capture devices, and with some delay and some manufacturing issue, finally, around the end of last month, I received the devices. It was just around my birthday, so I consider them to be my birthday gift ;) </p>  <p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b7c7e42657970b-pi"><img title="clip_image001" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="clip_image001" src="/assets/image_9310be.jpg" width="502" height="284" /></a></p>  <p>The device looks well manufactured, well packaged, with simple but enough information about how to start playing with it. The Quickstart Guide at <a href="https://neuronmocap.com/quickstart">https://neuronmocap.com/quickstart</a> is well written to get you started quickly and easily. Since perception neuron also provide a NeuronDataReader SDK to capture the motion data, I was interested to try to capture the motion data in Maya and MotionBuilder. Luckily, we had lately an internal Maya Hackathon, and with my colleague Chengxi Li, we spent 2 days working on a “Real-time MotionCapture by Neuron Device in Maya” project. It was cool and very interesting, so I’d like to share our experience with you about what we did in a series of blog posts. I will also mention the limitations and future plan of our projects.</p>  <p>First of all, let’s talk about the NeuronDataReader SDK itself.</p>  <p>The NeuronDataReader SDK can be downloaded from <a href="https://neuronmocap.com/downloads">https://neuronmocap.com/downloads</a>. It includes an API documentation. Actually, the documentation is pretty small, but the API structure/workflow is very simple to understand and/or use.</p>  <p>The 1<sup>st</sup> thing you need is the Axis Neuron application that can be downloaded for free from <a href="https://neuronmocap.com/downloads">https://neuronmocap.com/downloads</a>. If you want the full enabled application with all the functionalities, you would need their professional version at the price of $499. You can check <a href="https://neuronmocap.com/content/axis-neuron-software">https://neuronmocap.com/content/axis-neuron-software</a> for the advanced functionalities list. The Axis Neuron application is very important because at the time of this post, you cannot read the data from the Perception Neuron devices directly. This application (free version is enough) is required to receive and broadcast the animation data through TCP/IP or UDP protocol. The application runs as a server for the data in your system. In this post, I won’t talk too much about the application itself - after installing and running the Axis Neuron application, you just need to follow the <a href="https://neuronmocap.com/quickstart">Quickstart Guide</a>, and you should be able to see your motion captures in the Axis Neuron real-time viewport. Please make sure the “<b>BVH</b>” check box under “File-&gt;Settings-&gt;Broadcasting” is checked so the application is ready for broadcasting animation data.</p>  <p>Ok, now the server (Axis Neuron application) is ready to broadcast, let’s introduce the APIs so you can understand how you can create your own plug-in (acting as a client) to receive and parse the data, based on NeuronDataReader SDK. The process is not complicated, and can be described into 3 steps.</p>  <p><b>Step 1</b>, the Axis neuron application communicates with your customized plug-in for 3 different types of data, and the NeuronDataReader SDK provides the callback methods that you can register to receive the data, here are the 3 types of callbacks you need to define:</p>  <pre class="brush:cpp;toolbar: false;">//BVH binary stream data which includes all the information of the skeleton 
typedef void (CALLBACK *FrameDataReceived)(void* customedObj, SOCKET_REF sender, BvhDataHeader* header, float* data); 
//Command data which is used to sync between client and server 
typedef void (CALLBACK *CommandDataReceived)(void* customedObj, SOCKET_REF sender, CommandPack* pack, void* data);
//Socket connection status data
typedef void (CALLBACK *SocketStatusChanged)(void* customedObj, SOCKET_REF sender, SocketStatus status, char* message); </pre>

<p>One important thing here, the data-processing thread in the NeuronDataReader is a worker thread separated from the UI. So the registered data-receiving callbacks cannot access the UI elements directly. If the data need to be used in the UI thread, it is recommended to save the data from the callback function to a local array, and that the UI thread can access the local-buffered data in any other place. We will show you the example in the following blog post.</p>

<p><b>Step 2</b>, when your callbacks are defined, the next step is to register the above 3 callbacks, NeuronDataReader SDK provides the following methods:</p>

<pre class="brush:cpp;toolbar: false;">void BRRegisterFrameDataCallback(void* customedObj, FrameDataReceived handle);
void BRRegisterCommandDataCallback(void* customedObj, CommandDataReceived handle);
void BRRegisterSocketStatusCallback (void* customedObj, SocketStatusChanged handle);</pre>

<p><b>Step 3</b>, after the callbacks are registered, you can connect your client to the server, with either TCP or UDP protocol as follow. When the connection is operational, your callbacks will receive the data from the server in real-time:</p>

<pre class="brush:cpp;toolbar: false;">//Connect to the server with given IP address and port 
SOCKET_REFBRConnectTo(char* serverIP, int nPort);
//Connect to the server with given port
SOCKET_REF BRStartUDPServiceAt(int nPort);
//To stop data receive service, you need to use BRCloseSocket to close the service.
void BRCloseSocket (SOCKET_REF sockRef);</pre>

<p>That is the main workflow for a customized plug-in. Of course, there are a few other methods listed below that you can be used as well.</p>

<pre class="brush:cpp;toolbar: false;">SocketStatus BRGetSocketStatus (SOCKET_REF sockRef);
BOOL BRCommandFetchAvatarDataFromServer(SOCKET_REF sockRef, int avatarIndex, CmdId cmdId);
BOOL BRCommandFetchDataFromServer(SOCKET_REF sockRef, CmdId cmdId);
BOOL BRRegisterAutoSyncParmeter(SOCKET_REF sockRef, CmdId cmdId);
BOOL BRUnregisterAutoSyncParmeter(SOCKET_REF sockRef, CmdId cmdId);
char* BRGetLastErrorMessage();</pre>

<p>In the SDK folder, there is also a C# demo sample, it demonstrates the simple workflow on how to use the NeuronDataReader SDK. To run the demo sample, do not forget to copy the file “NeuronDataReader.dll” to same folder of your application to make it work.</p>

<p>In next post, we will continue talking about how to use these APIs in your project.</p>

<p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b7c7e4265d970b-pi"><img title="clip_image002" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="clip_image002" src="/assets/image_50f6f5.jpg" width="517" height="316" /></a></p>
