---
layout: "post"
title: "Entitlement API changes in Inventor 2020"
date: "2019-09-21 18:05:34"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Announcements"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/09/entitlement-api-changes-in-inventor-2020.html "
typepad_basename: "entitlement-api-changes-in-inventor-2020"
typepad_status: "Publish"
---

<p>We already have some articles on using the <strong>Entitlement API</strong> from <strong>Inventor</strong>:<br />- <a href="https://adndevblog.typepad.com/manufacturing/2015/03/entitlement-api-in-inventor.html">Entitlement API in Inventor</a><br />- <a href="https://adndevblog.typepad.com/manufacturing/2017/08/entitlement-api-in-inventor-2018.html">Entitlement API in Inventor 2018</a></p>
<p>There is now a change you have to be aware of.<br />Starting with <strong>Inventor 2020</strong>, to guarantee the <strong>Entitlement API</strong> calls work properly to get the <strong>UserName</strong>, call the <strong>Inventor API&#39;s</strong> <strong>Application.Login()</strong> function before or after <strong>webServiceMgr.Initialize()</strong> so the <strong>WebServices</strong> are fully initialized. <br />Otherwise, you may see a valid <strong>UserId</strong>, but an empty <strong>UserName</strong> when the user is already logged in:<br /><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a486c274200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="NoUserName" class="asset  asset-image at-xid-6a00e553fcbfc688340240a486c274200c img-responsive" src="/assets/image_255632.jpg" title="NoUserName" /></a><br /><strong>Note</strong>: the <strong>Application.Login()</strong> function is <strong>hidden</strong>, so <a href="https://docs.microsoft.com/en-us/visualstudio/ide/using-intellisense"><strong>IntelliSense</strong></a> in <strong>Visual Studio</strong> will not list it.</p>
<p>And, if the user attempts to login when you have not used the <strong>Login() </strong>API call, they may get an error dialog that the service is not available:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4af3a05200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Image-2019-05-09-12-26-49-479" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4af3a05200b img-responsive" src="/assets/image_849837.jpg" title="Image-2019-05-09-12-26-49-479" /></a></p>
<p>You can use the following code to get <strong>userId</strong> and <strong>userName</strong>:</p>
<pre>public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
{
  m_inventorApplication = addInSiteObject.Application;

  CWebServicesManager mgr = new CWebServicesManager();
  bool isInitialized = mgr.Initialize();

  if (isInitialized)
  {
    try
    {
      m_inventorApplication.Login();
      string userId = &quot;&quot;;
      mgr.GetUserId(ref userId);
      string userName = &quot;&quot;;
      mgr.GetLoginUserName(ref userName);
      MsgBox.Show(
        &quot;User ID = &#39;&quot; + userId + &quot;\n&quot; +
        &quot;User Name = &#39;&quot; + userName + &quot;&#39;&quot;);
    }
    catch (Exception ex)
    {
      MsgBox.Show(ex.Message);
    }
  }
}</pre>
<p>This solved the problem:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4d4a90b200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="NoUserNameFixed" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4d4a90b200b img-responsive" src="/assets/image_127576.jpg" title="NoUserNameFixed" /></a></p>
<p>As pointed out in one of the above-referenced articles, there is a <strong>GitHub</strong> repo with a project showing the usage of the <strong>Entitlement API</strong> from an <strong>Inventor add-in</strong>. I updated it with &quot;<strong>releases</strong>&quot; for <strong>Inventor 2016/2017</strong>, <strong>2018/2019</strong> and <strong>2020</strong>: <a href="https://github.com/ADN-DevTech/EntitlementAPI_Inventor">https://github.com/ADN-DevTech/EntitlementAPI_Inventor</a></p>
<p>-Adam</p>
