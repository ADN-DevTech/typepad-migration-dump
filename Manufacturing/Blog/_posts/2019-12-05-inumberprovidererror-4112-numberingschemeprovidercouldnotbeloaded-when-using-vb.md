---
layout: "post"
title: "INumberProvider&ndash;Error 4112 &rsquo;NumberingSchemeProviderCouldNotBeLoaded&rsquo; when using VB"
date: "2019-12-05 05:16:57"
author: "Sajith Subramanian"
categories:
  - "Sajith Subramanian"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2019/12/inumberprovidererror-4112-numberingschemeprovidercouldnotbeloaded-when-using-vb.html "
typepad_basename: "inumberprovidererror-4112-numberingschemeprovidercouldnotbeloaded-when-using-vb"
typepad_status: "Publish"
---

<p><p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p><p>When converting the sample code for Custom Numbering that comes along with the SDK from C# to VB, one of our users reported that he was now facing the error <strong>4112</strong>, i.e. <strong>NumberingSchemeProviderCouldNotBeLoaded.</strong></p><p><strong>Solution:</strong></p><p>In case of VB, it seems that there is a Root Namespace that is added before every Namespace, which should also be included in the 'type' attribute in web.config. If you add that, it should be able to initialize correctly. So the following should get it to work:</p><pre>&lt;numberproviders&gt;<br>&lt;numberprovider name="CustomProvider" type="CustomNumberingProvider.CustomNumberingProvider.CustomNumberingProvider, CustomNumberingProvider" cancache="false"&gt;<br>&lt;!-- &lt;initializationParm value="somethingImportant" /&gt; -–&gt;<br>&lt;/numberprovider&gt;<br>&lt;/numberproviders&gt;</pre>
