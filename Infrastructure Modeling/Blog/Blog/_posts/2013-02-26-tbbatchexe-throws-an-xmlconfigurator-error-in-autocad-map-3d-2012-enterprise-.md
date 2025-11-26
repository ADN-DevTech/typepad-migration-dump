---
layout: "post"
title: "TBBatch.exe throws an XmlConfigurator error in AutoCAD Map 3D 2012 Enterprise "
date: "2013-02-26 00:40:23"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2012"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/02/tbbatchexe-throws-an-xmlconfigurator-error-in-autocad-map-3d-2012-enterprise-.html "
typepad_basename: "tbbatchexe-throws-an-xmlconfigurator-error-in-autocad-map-3d-2012-enterprise-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Some of you might have noticed an issue in using the TBBatch.exe in Map 3D 2012 Enterprise.
TBBatch.exe throws exceptions like the
following -&#0160;</p>
<pre>log4net:ERROR XmlConfigurator: Error while loading XML configuration <br />System.Xml.XmlException: Ungültiges Zeichen in der angegebenen Codierung. xxxx <br />58, Position 1. <br />in System.Xml.XmlTextReaderImpl.Throw(Exception e) <br />in System.Xml.XmlTextReaderImpl.Throw(String res, String arg) <br />in System.Xml.XmlTextReaderImpl.Throw(Int32 pos, String res) <br />in System.Xml.XmlTextReaderImpl.InvalidCharRecovery(Int32&amp; bytesCount, Int32 &amp; charsCount) <br />in System.Xml.XmlTextReaderImpl.GetChars(Int32 maxCharsCount) <br />in System.Xml.XmlTextReaderImpl.ReadData() <br />in System.Xml.XmlTextReaderImpl.EatWhitespaces(BufferBuilder sb) <br />in System.Xml.XmlTextReaderImpl.ParseRootLevelWhitespace() <br />in System.Xml.XmlTextReaderImpl.ParseDocumentContent() <br />in System.Xml.XmlTextReaderImpl.Read() <br />in System.Xml.XmlLoader.LoadDocSequence(XmlDocument parentDoc) <br />in System.Xml.XmlLoader.Load(XmlDocument doc, XmlReader reader, Boolean preserveWhitespace) <br />in System.Xml.XmlDocument.Load(XmlReader reader) <br />in log4net.Config.XmlConfigurator.Configure(ILoggerRepository repository, Stream configStream)</pre>
<p>&#0160;</p>
<p>To make TBBatch.exe functions correctly, unzip the attached <span class="asset  asset-generic at-xid-6a0167607c2431970b017d4148b77a970c"><span class="asset  asset-generic at-xid-6a0167607c2431970b017ee8bca809970d"><a href="http://adndevblog.typepad.com/files/app-1.zip">config</a></span></span>&#0160;file and
then try to copy the same file to the bin directory and rename the file to
‘TBBatch.exe.config’. This should help in resolving the above mentioned issue.</p>
