---
layout: "post"
title: "Using REST translation services from iOS"
date: "2012-12-07 08:08:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iOS"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2012/12/using-rest-translator-service-from-ios.html "
typepad_basename: "using-rest-translator-service-from-ios"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Another sample app I created for my AU presentation was one using REST translator services. </p>
<p>First I thought of using the Google translation service. So I went on this website <a href="https://developers.google.com/translate/v2/using_rest" target="_self">https://developers.google.com/translate/v2/using_rest</a>&#0160;and checked the URL I needed to access.</p>
<p>You can get back all the supported languages and their codes using&#0160;<a href="https://www.googleapis.com/language/translate/v2/languages?key=&lt;GOOGLE_APIKEY&gt;&amp;target=en" target="_self">https://www.googleapis.com/language/translate/v2/languages?key=&lt;GOOGLE_APIKEY&gt;&amp;target=en</a></p>
<p><a href="https://www.googleapis.com/language/translate/v2/languages?key=&lt;GOOGLE_APIKEY&gt;&amp;target=en" target="_self"></a>As you can spot it in the above link we&#39;ll need to get an API KEY from Google before we can make this call. So I registered on their site and got it.</p>
<p>Now we can fill up an <strong>NSMutableArray</strong> with the list of supported languages:&#0160;</p>
<span style="line-height: 120%;">
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160;&#0160;<span style="color: #703daa;">NSError</span> * err = <span style="color: #bb2ca2;">nil</span>;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&#0160; </span><span style="color: #703daa;">NSString</span><span style="color: #000000;"> * urlString = [</span><span style="color: #703daa;">NSString</span><span style="color: #000000;"> </span><span style="color: #3d1d81;">stringWithFormat</span><span style="color: #000000;">:</span>@&quot;https://www.googleapis.com/language/translate/v2/languages?key=%@&amp;target=en&quot;<span style="color: #000000;">,</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #78492a;"><span style="color: #000000;">&#0160; &#0160; </span>GOOGLE_API_KEY<span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #703daa;">NSURL</span> * url = [<span style="color: #703daa;">NSURL</span> <span style="color: #3d1d81;">URLWithString</span>:urlString];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&#0160; </span>NSMutableURLRequest<span style="color: #000000;"> * req =</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&#0160; &#0160; [</span>NSMutableURLRequest<span style="color: #000000;"> </span><span style="color: #3d1d81;">requestWithURL</span><span style="color: #000000;">:url];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&#0160; </span>// Using synchronous request just to keep things simple</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&#0160; </span><span style="color: #703daa;">NSData</span><span style="color: #000000;"> * data = [</span><span style="color: #703daa;">NSURLConnection</span><span style="color: #000000;"> </span>sendSynchronousRequest<span style="color: #000000;">:req</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&#0160; &#0160; </span>returningResponse<span style="color: #000000;">:</span><span style="color: #bb2ca2;">nil</span><span style="color: #000000;"> </span>error<span style="color: #000000;">:&amp;err];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&#0160; </span>NSDictionary<span style="color: #000000;"> * json = [</span>NSJSONSerialization</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #3d1d81;">JSONObjectWithData</span>:data&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span>options<span style="color: #000000;">:</span>NSJSONReadingMutableContainers<span style="color: #000000;">&#0160;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #3d1d81;">error</span>:&amp;err];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&#0160; </span>jsonLanguages<span style="color: #000000;"> =</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&#0160; &#0160; [[json </span>objectForKey<span style="color: #000000;">:</span><span style="color: #d12f1b;">@&quot;data&quot;</span><span style="color: #000000;">] </span>objectForKey<span style="color: #000000;">:</span><span style="color: #d12f1b;">@&quot;languages&quot;</span><span style="color: #000000;">];</span></p>
</span>
<p>Once you converted the response Json string into a tree of <strong>NSDictionary&#39;s</strong> and <strong>NSArray&#39;s</strong> using&#0160;<strong>NSJSONSerialization</strong>, you can easily check its content to see how you can retrieve specific information from there by using <strong>po</strong> in the <strong>Console</strong> window and passing in the variable&#39;s name:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee5e8d637970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="NSJSONSerialization" class="asset  asset-image at-xid-6a0167607c2431970b017ee5e8d637970d" src="/assets/image_e0229f.jpg" title="NSJSONSerialization" /></a><br />I created the following user interface. Started with a <strong>Single View Application</strong> and placed a <strong>Text Field</strong>, a <strong>Label</strong>, a <strong>Button</strong>, and a <strong>Picker View</strong> on the view in the storyboard. Then hooked those up in my code the usual way (like in&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/2012/12/picture-on-picture-manipulation-on-ios.html" target="_self">this</a>&#0160;article), by <strong>Ctrl-dragging</strong> them onto my code file, <strong>ViewControlller.h<br /></strong>- In the text field the user will be able to type the text they want to translate<br />- In the picker view we&#39;ll list the available languages that the user can select from<br />- And when the button is clicked the label will show the translated text</p>
<p>All that is left is doing the translation. I&#39;ve already implemented the translation part using the Google service when I realized that it does not provide any number of translations for free and so always returns an error - still, I kept the code in the attached project. Since this was just meant to be a sample I did not want to spend money on it and so looked for a free service. One of those was at&#0160;<a href="http://www.frengly.com" target="_self">http://www.frengly.com</a>. There as well you need to register to get a user name and password that you need to use for the translation, but at least it&#39;s for free. &#0160;</p>
<p>Now when the button is clicked I can do the following:</p>
<span style="line-height: 120%;">
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">IBAction</span>)translate:(<span style="color: #bb2ca2;">id</span>)sender</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #703daa;">NSError</span> * err = <span style="color: #bb2ca2;">nil</span>;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&#0160; </span><span style="color: #703daa;">NSInteger</span><span style="color: #000000;"> selection = [</span><span style="color: #bb2ca2;">self</span><span style="color: #000000;">.</span><span style="color: #4f8187;">languages</span><span style="color: #000000;"> </span>selectedRowInComponent<span style="color: #000000;">:</span><span style="color: #272ad8;">0</span><span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #703daa;">NSDictionary</span> * language = [<span style="color: #4f8187;">jsonLanguages</span> <span style="color: #3d1d81;">objectAtIndex</span>:selection];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #703daa;">NSString</span> * languageCode = [language <span style="color: #3d1d81;">objectForKey</span>:<span style="color: #d12f1b;">@&quot;language&quot;</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&#0160; </span><span style="color: #703daa;">NSString</span><span style="color: #000000;"> * urlString = [</span><span style="color: #703daa;">NSString</span><span style="color: #000000;"> </span><span style="color: #3d1d81;">stringWithFormat</span><span style="color: #000000;">:</span>@&quot;http://www.syslang.com/frengly/controller?action=translateREST&amp;src=en&amp;dest=%@&amp;text=%@&amp;username=%@&amp;password=%@&quot;<span style="color: #000000;">,</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; &#0160; languageCode,</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&#0160; &#0160; [</span><span style="color: #bb2ca2;">self</span><span style="color: #000000;">.</span>original<span style="color: #000000;">.</span><span style="color: #703daa;">text</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>stringByAddingPercentEscapesUsingEncoding<span style="color: #000000;">:</span>NSUTF8StringEncoding<span style="color: #000000;">],</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #78492a;"><span style="color: #000000;">&#0160; &#0160; </span>SYSLANG_USER_NAME<span style="color: #000000;">,</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #78492a;"><span style="color: #000000;">&#0160; &#0160; </span>SYSLANG_PASSWORD</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; ];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #703daa;">NSURL</span> * url = [<span style="color: #703daa;">NSURL</span> <span style="color: #3d1d81;">URLWithString</span>:urlString];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&#0160; </span>NSMutableURLRequest<span style="color: #000000;"> * req = [</span>NSMutableURLRequest<span style="color: #000000;"> </span><span style="color: #3d1d81;">requestWithURL</span><span style="color: #000000;">:url];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&#0160; </span><span style="color: #703daa;">NSData</span><span style="color: #000000;"> * data = [</span><span style="color: #703daa;">NSURLConnection</span><span style="color: #000000;"> </span>sendSynchronousRequest<span style="color: #000000;">:req</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&#0160; &#0160; </span>returningResponse<span style="color: #000000;">:</span><span style="color: #bb2ca2;">nil</span><span style="color: #000000;"> </span>error<span style="color: #000000;">:&amp;err];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&#0160; </span>// Create and init NSXMLParser object</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #703daa;">NSXMLParser</span> * nsXmlParser = [[<span style="color: #703daa;">NSXMLParser</span> <span style="color: #3d1d81;">alloc</span>] <span style="color: #3d1d81;">initWithData</span>:data];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&#0160; </span>// Create and init our delegate</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #4f8187;">XmlParser</span> * parser = [<span style="color: #4f8187;">XmlParser</span> <span style="color: #3d1d81;">alloc</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&#0160; </span>// Set delegate</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; [nsXmlParser <span style="color: #3d1d81;">setDelegate</span>:parser];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&#0160; </span>// Parsing...</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #bb2ca2;">BOOL</span> success = [nsXmlParser <span style="color: #3d1d81;">parse</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&#0160; </span>// Test the result</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #bb2ca2;">if</span> (success)</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #bb2ca2;">self</span><span style="color: #000000;">.</span>translation<span style="color: #000000;">.</span><span style="color: #703daa;">text</span><span style="color: #000000;"> = parser.</span>translation<span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
</span>
<p>Unfortunately, this service sends the information in xml format, so I needed to create a parser for that. I like checking what is available natively, without using 3rd party libraries, so I gave <strong>NSXMLParser</strong> a try.</p>
<p>XmlParser.h</p>
<span style="line-height: 120%;">
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&#0160; XmlParser.h</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&#0160; MyAWS</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&#0160; Created by Adam Nagy on 06/11/2012.</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&#0160; Copyright (c) 2012 Adam Nagy. All rights reserved.</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #78492a;">#import </span>&lt;Foundation/Foundation.h&gt;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #bb2ca2;">@interface</span><span style="color: #000000;"> XmlParser : </span>NSObject<span style="color: #000000;"> &lt;</span>NSXMLParserDelegate<span style="color: #000000;">&gt;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;">@property<span style="color: #000000;"> (</span>strong<span style="color: #000000;">, </span>nonatomic<span style="color: #000000;">) </span><span style="color: #703daa;">NSString</span><span style="color: #000000;"> * translation;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;">@end</p>
</span>
<p>XmlParser.m</p>
<span style="line-height: 120%;">
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&#0160; XmlParser.m</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&#0160; MyAWS</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&#0160; Created by Adam Nagy on 06/11/2012.</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&#0160; Copyright (c) 2012 Adam Nagy. All rights reserved.</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #78492a;">#import </span>&quot;XmlParser.h&quot;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;">@implementation<span style="color: #000000;"> XmlParser</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="color: #bb2ca2;">@synthesize</span> translation;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="color: #bb2ca2;">static</span> <span style="color: #bb2ca2;">bool</span> translationTag = <span style="color: #bb2ca2;">false</span>;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">void</span>)parser:(<span style="color: #703daa;">NSXMLParser</span> *)parser&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; didStartElement:(<span style="color: #703daa;">NSString</span> *)elementName&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; namespaceURI:(<span style="color: #703daa;">NSString</span> *)namespaceURI&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; qualifiedName:(<span style="color: #703daa;">NSString</span> *)qualifiedName&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="white-space: pre;"> </span> &#0160; &#0160; &#0160; attributes:(<span style="color: #703daa;">NSDictionary</span> *)attributeDict {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;"><span style="white-space: pre;"> </span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #bb2ca2;">if</span> ([elementName <span style="color: #3d1d81;">isEqualToString</span>:<span style="color: #d12f1b;">@&quot;translation&quot;</span>])</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&#0160; &#0160; </span>translationTag<span style="color: #000000;"> = </span><span style="color: #bb2ca2;">true</span><span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">void</span>)parser:(<span style="color: #703daa;">NSXMLParser</span> *)parser foundCharacters:(<span style="color: #703daa;">NSString</span> *)string {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&#0160; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (</span>translationTag<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (!</span>translation<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span><span style="color: #4f8187;">translation</span><span style="color: #000000;"> = [</span><span style="color: #703daa;">NSString</span><span style="color: #000000;"> </span>stringWithFormat<span style="color: #000000;">:</span><span style="color: #d12f1b;">@&quot;&quot;</span><span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #4f8187;">translation</span><span style="color: #000000;"> =&#0160; [</span><span style="color: #4f8187;">translation</span><span style="color: #000000;"> </span>stringByAppendingString<span style="color: #000000;">:string];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">void</span>)parser:(<span style="color: #703daa;">NSXMLParser</span> *)parser&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; didEndElement:(<span style="color: #703daa;">NSString</span> *)elementName</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; namespaceURI:(<span style="color: #703daa;">NSString</span> *)namespaceURI&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; qualifiedName:(<span style="color: #703daa;">NSString</span> *)qName {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #bb2ca2;">if</span> ([elementName <span style="color: #3d1d81;">isEqualToString</span>:<span style="color: #d12f1b;">@&quot;translation&quot;</span>])</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&#0160; &#0160; </span>translationTag<span style="color: #000000;"> = </span><span style="color: #bb2ca2;">false</span><span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;">@end</p>
</span>
<p>Project is attached here:&#0160;<span class="asset  asset-generic at-xid-6a0167607c2431970b017d3e7a87ec970c"><a href="http://adndevblog.typepad.com/files/translator_2012-12-05.zip">Download Translator_2012-12-05</a></span></p>
<p>Note: the code only works if you first register for the above mentioned services and then fill these variables with your credentials:&#0160;GOOGLE_API_KEY,&#0160;SYSLANG_USER_NAME and&#0160;SYSLANG_PASSWORD</p>
