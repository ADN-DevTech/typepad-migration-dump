---
layout: "post"
title: "DictionaryRecordNameTranslator - what is the usage? "
date: "2012-07-15 19:50:51"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
  - "OMF"
original_url: "https://adndevblog.typepad.com/aec/2012/07/dictionaryrecordnametranslator-what-is-the-usage.html "
typepad_basename: "dictionaryrecordnametranslator-what-is-the-usage"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I see a class called DictionaryRecordNameTranslator in the ACA .NET API.&#0160; What is the usage of it?&#0160; This would be great if I don&#39;t need to translate the property sets, etc., but just add a translator runtime.&#0160;</p>
<p><strong>Solution</strong></p>
<p>The name &quot;Translator&quot; is probably misleading and adding a confusion here.&#0160; Unfortunately this class is not meant for translation.&#0160;&#0160;&#0160;</p>
<p>DictionaryRecordnameTranslator class (or AecNameTranslator in OMF) does not actually translate anything; i.e.,&#0160;it doesn’t know how to translate English to German, for example. All it really does is provide a named object a way to identify where its global and local name are located. Some objects store their global name in the ‘Name’ field and local name in the ‘AlternateName’ field, while others are reversed. We needed some way for UI elements to say ‘give me your local name’, rather than always assuming the ‘Name’ is what they wanted. This means that the object still needs to provide translated strings! Simply put, an AecNameTranslator does not provide a translation, it just says where the translation is located.</p>
<p>Here is some additional note from OMF reference guide:</p>
<p>&quot;An AecNameTranslator is a class that contains a mapping of global names to localized names. It is intended to be used by programmatic objects that are normally defined by users, such as the classifications used in Space standards, to define a global name that is used by the creator of the programmatic object while displaying a translated name to the user.&#0160;</p>
<p>Derived classes can be used to define where the global name comes from. For example, AecNameTranslatorDictRecordName defines a displayName function that takes an AecDbDictRecord parameter, gets the dictionary name from it, and returns the corresponding localized name. Alternatively, AecNameTranslatorDictRecordName returns the localized name corresponding to the dictionary record&#39;s alternate name.&quot;&#0160;</p>
<p>This class was added in ACA 2009.</p>
