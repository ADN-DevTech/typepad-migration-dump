---
layout: "post"
title: "Integrating gRPC.NET with AutoCAD"
date: "2024-04-25 21:46:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
original_url: "https://adndevblog.typepad.com/autocad/2024/04/integrating-grpcnet-with-autocad.html "
typepad_basename: "integrating-grpcnet-with-autocad"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p><p>Microsoft deprecated WCF with the release of .NET 5 and recommends developers transition to <a href="https://github.com/dotnet-architecture/eBooks/blob/1ed30275281b9060964fcb2a4c363fe7797fe3f3/current/grpc-for-wcf-developers/gRPC-for-WCF-Developers.pdf">more modern technologies</a>. This can be a challenge for those with existing WCF applications who want to migrate to .NET 8.0.<p>Recently, I received a question about integrating gRPC.NET, a popular modern alternative to WCF, with AutoCAD 2025.<p>In response, I've created a small application that demonstrates how to interact with a gRPC server from an AutoCAD client. <p>This sample provides a starting point for developers interested in exploring this integration approach.<p>The provided code showcases a basic example using a <code>GreeterService</code>. <p>Let's break it down:<ul><li><p><strong>Client-Side (AutoCAD Plugin):</strong><ul><li>The <code>TestGrpc</code> method establishes a gRPC channel to a server running on <code>localhost:8080</code>. 
<li>It creates a <code>GreeterClient</code> object to interact with the gRPC service. 
<li>An asynchronous <code>SayHelloAsync</code> call is made, sending a <code>HelloRequest</code> message with the name "GreeterClient". 
<li>The response (<code>HelloReply</code>) containing the server's greeting is received and written to the active AutoCAD document. </li></ul><li><p><strong>gRPC Service Definition (Protobuf):</strong><ul><li>Defines a <code>Greeter</code> service with a <code>SayHello</code> RPC method. 
<li>Clients send a <code>HelloRequest</code> message with their name. 
<li>The server responds with a <code>HelloReply</code> containing a greeting. </li></ul><li><p><strong>Server-Side:</strong><ul><li>Exposes the <code>GreeterService</code> as a gRPC service. 
<li>The <code>SayHello</code> method receives a <code>HelloRequest</code>, extracts the name, and constructs a personalized greeting in the <code>HelloReply</code> message. </li></ul></li></ul><p><strong>Running the Application</strong><p>This code creates a combined gRPC and HTTP server. Clients can use gRPC to interact with the <code>GreeterService</code>, while a basic HTTP endpoint provides a simple test or informational message.<p>Source Code : <a title="https://github.com/MadhukarMoogala/GrpcToAcad/blob/main/README.md" href="https://github.com/MadhukarMoogala/GrpcToAcad/blob/main/README.md">https://github.com/MadhukarMoogala/GrpcToAcad/blob/main/README.md</a>
