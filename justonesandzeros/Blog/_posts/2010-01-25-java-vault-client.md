---
layout: "post"
title: "Java Vault Client"
date: "2010-01-25 10:04:17"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2010/01/java-vault-client.html "
typepad_basename: "java-vault-client"
typepad_status: "Publish"
---

<p><img src="/assets/SampleApp2.png" /> </p> <p>Sometimes I want to write a program something just for the sake of doing it.&#0160; So I decided to write a simple Java client for Vault.&#0160; As you may already know, Vault communicates over HTTP using SOAP, which means you can write a client in just about any language and platform that you want.&#0160; </p> <p>The program itself is pretty basic.&#0160; It&#39;s just a Java port of the VaultList SDK sample, which just lists all the files in the Vault.</p> <p><img src="/assets/VaultListJava.png" /> </p> <p><a href="http://justonesandzeros.typepad.com/Apps/VaultListJava/VaultListJava.jar">Click here to download the application</a>  <br /><a href="http://justonesandzeros.typepad.com/Apps/VaultListJava/VaultListJava-source.zip">Click here to download the source</a></p> <p><strong>What you need:   <br /></strong>Vault 2010 (any version)  <br /><a href="http://www.java.com">Java</a>  <br /><a href="http://www.apache.org/dyn/closer.cgi/ws/axis/1_4">Axis 1.4</a></p> <p><strong>To run:</strong></p> <ol>
  <li>Download VaultListJava</li>
  <li>Create a &quot;lib&quot; subfolder</li>
  <li>Download Axis 1.4 (axis-bin-1_4.zip)</li>
  <li>Unzip the file.</li>
  <li>Copy the lib directory contents to the lib directory under VaultListJava</li>
  <li>Execute VaultListJava.jar   </li>
 </ol>
 <p><strong>For programmers:</strong>  <br />If you are a member of the Autodesk Developer Network, you should read <a href="http://adn.autodesk.com/adn/servlet/item?siteID=4814862&amp;id=8000822&amp;linkID=4901778">Tutorial: Using Java to Access the Vault API</a>, which is the definitive guide to writing Java clients for Vault.&#0160; Although it was written for Vault 5, it&#39;s still valid for Vault 2010.</p> <p>I tried using some of the newer Java tools for web services, but I was not able to get them to work properly with Vault.&#0160; The problem was getting control of the security header, which needs to be copied from the SecurityService to all the other services.&#0160; Axis 1.4 gave me the ability to do that, but&#0160; I was not able to find a way with JAX-WS or Axis 2.&#0160; Of course, my Java is very rusty, so maybe I just missed how to do it.</p>
