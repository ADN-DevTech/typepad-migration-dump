---
layout: "post"
title: "Integrate Autodesk Viewer with SharePoint"
date: "2014-07-28 00:06:02"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "ASP .NET"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2014/07/integrate-viewer-with-sharepoint.html "
typepad_basename: "integrate-viewer-with-sharepoint"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>All the files created while writing this article are available on <a href="https://github.com/adamenagy/sharepoint-integration">github</a> and they also contain comments that provide further information.&#0160;</p>
<p>This blog post goes through the steps of integrating the Autodesk viewer technology with <strong>SharePoint</strong>, i.e. use the viewer to visualize your files inside <strong>SharePoint</strong>. The parts that need to be implemented:&#0160;</p>
<ul>
<li>log in&#0160;</li>
<li>upload the file for translation</li>
<li>start the translation process</li>
<li>monitor the process</li>
<li>show the translated file in the viewer</li>
</ul>
<p><strong>1) Install SharePoint and Microsoft SharePoint Designer</strong></p>
<p>First of all I needed to install the <a href="http://technet.microsoft.com/en-gb/library/ff607866(v=office.14).aspx" target="_self"><strong>SharePoint</strong></a> environment. It&#39;s useful to install <a href="http://office.microsoft.com/en-gb/sharepoint-designer-help/introducing-sharepoint-designer-2010-HA101782482.aspx" target="_self"><strong>Microsoft SharePoint Designer</strong></a>, that I will also be using.</p>
<p><strong>2) Add extra file property</strong></p>
<p>In order to keep track of if the document has already been uploaded and translated, we can attach each file the urn of the translated file. If that is not set then we start the upload/translate process, otherwise just simply show the file that has the urn.&#0160;</p>
<p>You could make things a bit more sophisticated by e.g. deleting the urn property of the document each time it is checked in, or you would automatically start the upload/translation process then, but that&#39;s not part of this blog post.</p>
<p>From the main page of your <strong>SharePoint</strong> site you can go <strong>Libraries &gt; Documents &gt; Library [tab] &gt; Library Settings&#0160;</strong></p>
<p><strong> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511db9ab9970c-pi" style="display: inline;"><img alt="Library settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511db9ab9970c image-full img-responsive" src="/assets/image_10b3be.jpg" title="Library settings" /></a></strong></p>
<p>Then under the <strong>Columns</strong> section you&#39;ll find <strong>Create column</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd2bf9ba970b-pi" style="display: inline;"><img alt="Create column" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd2bf9ba970b image-full img-responsive" src="/assets/image_bd77d3.jpg" title="Create column" /></a></p>
<p>We can simply name this column&#0160;<strong><strong>&quot;urn&quot;</strong>.</strong></p>
<p><strong>3) Create the viewer page</strong></p>
<p>This is a simple <strong>aspx</strong> page that will be using the viewer to show the file whose urn is passed to it. This can be easily integrated into other web pages using an iframe. It will expect two parameters in the URL: the urn of the file to show and the access token you got after authentication.&#0160;</p>
<pre>MyViewerPage.aspx?accessToken=&lt;access token&gt;&amp;
urn=&lt;urn of the file to view&gt;
e.g.: MyViewerPage.aspx?accessToken=ljvWwXkzF3zxCVfLUZhP1Q8Qk66S&amp;
urn=dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bXl0ZXN0YnVja2V0L2NoYXNzaXMuZHdm
</pre>
<p>You can have a look at the tutorial to see how to create such a page:&#0160;<br /><a href="https://developer.autodesk.com/api/view-and-data-api/#stepbystep" target="_self" title="">https://developer.autodesk.com/api/view-and-data-api/#stepbystep</a><a href="http://developer.api.autodesk.com/documentation/v1/viewers/tutorial-quick_start.html" target="_self" title=""><br /></a></p>
<p>The only thing that has been added beyond what the tutorial talks about is the part that retrieves the urn and accessToken parameters from the URL when this html page is opened: &#0160;<strong>&#0160;</strong></p>
<pre>var urn = Autodesk.Viewing.Private.getParameterByName(&quot;urn&quot;);
var accessToken = Autodesk.Viewing.Private.getParameterByName(
  &quot;accessToken&quot;);	</pre>
<p>Open <strong>Microsoft SharePoint Designer</strong>, go to <strong>Site Pages</strong>&#0160;and right-click in the list and select <strong>New &gt; ASPX</strong></p>
<p><strong> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511dba073970c-pi" style="display: inline;"><img alt="Aspx page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511dba073970c image-full img-responsive" src="/assets/image_e895c6.jpg" title="Aspx page" /></a></strong>Now you can add the rest of the <strong>html</strong>&#0160;page for the viewer. See <strong>MyViewerPage.aspx</strong> on github.</p>
<p><strong>4) SharePoint Web Part</strong>&#0160;</p>
<p>When you install <strong>SharePoint</strong> it also installs some Visual Studio project templates that will help with this part. &#0160;Inside Visual Studio select <strong>File &gt; New &gt; Project &gt; Visual Web Part</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73de6f8fd970d-pi" style="display: inline;"><img alt="Visualwebpart" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73de6f8fd970d image-full img-responsive" src="/assets/image_8457a0.jpg" title="Visualwebpart" /></a></p>
<p>You need to start <strong>Visual Studio</strong> with the <strong>&quot;Run as administrator&quot;</strong> option, so that the compiled project can be deployed to your <strong>SharePoint</strong> site.</p>
<p>The project we&#39;ve just created contains a&#0160;<strong>VisualWebPart1UserControl.ascx</strong> file and its <strong>cs</strong> part that we need to edit in order to list all the <strong>SharePoint</strong> documents.</p>
<p>First of all we need to add a <strong>GridView</strong> to the page (named <strong>GridView1</strong>). We need to hook it up to the <strong>Documents</strong> library of <strong>SharePoint</strong> so that we can list all the documents that were uploaded to <strong>SharePoint</strong>. Inside the <strong>Page_Load</strong> function of our page we need to add the following:</p>
<pre>  var web = SPContext.Current.Web;
  SPListCollection coll = web.GetListsOfType(SPBaseType.DocumentLibrary);
  DataTable dt = coll[&quot;Documents&quot;].Items.GetDataTable();
  GridView1.DataSource = dt;
  GridView1.DataBind();</pre>
<p>We also add a button (called <strong>View</strong>) to each row that the user can click to see the document in the viewer:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd2c03bb970b-pi" style="display: inline;"><img alt="View button" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd2c03bb970b image-full img-responsive" src="/assets/image_53b404.jpg" title="View button" /></a></p>
<p>We need to modify the <strong>VisualWebPart1UserControl.ascx </strong>file for it. We add a template for the columns that will contain the button and the iframe that we can show when we want to view the document:</p>
<pre>&lt;Columns&gt;
  &lt;asp:TemplateField&gt;
    &lt;HeaderTemplate&gt;
      Extra
    &lt;/HeaderTemplate&gt;
    &lt;ItemTemplate&gt;
      &lt;asp:Button ID=&quot;SendButton&quot; OnClick=&quot;SendButton_Click&quot; 
        runat=&quot;server&quot; Text=&quot;View&quot; 
        ToolTip=&#39;&lt;%#DataBinder.Eval(Container.DataItem, &quot;ID&quot;)%&gt;&#39;&gt;
      &lt;/asp:Button&gt;
      &lt;iframe runat=&quot;server&quot; ID=&#39;ViewerPart&#39; 
        style=&quot;width:500px; height: 300px; display:none&quot;&gt; 
      &lt;/iframe&gt;
    &lt;/ItemTemplate&gt;
    &lt;FooterTemplate&gt;
    &lt;/FooterTemplate&gt;
  &lt;/asp:TemplateField&gt;
&lt;/Columns&gt;</pre>
<p>In this case we&#39;ll store the ID of the Document in the button&#39;s ToolTip, so that when it&#39;s clicked we can easily find that item to get the current urn value or update it with the newly created file&#39;s urn - see the DataBinder.Eval part above.</p>
<p>This is how we get back the <strong>Document</strong> we want to view inside the button&#39;s click event handler:</p>
<pre>protected void SendButton_Click(object sender, EventArgs e)
{
  Button btn = (Button)sender;
  HtmlControl viewer = (HtmlControl)btn.Parent.FindControl(&quot;ViewerPart&quot;);

  var web = SPContext.Current.Web;
  SPListCollection coll = web.GetListsOfType(SPBaseType.DocumentLibrary);
  SPList list = coll[&quot;Documents&quot;];
  _item = list.GetItemById(int.Parse(btn.ToolTip));
  // etc.</pre>
<p>By default we do not show the <strong>iframe</strong> part with the embedded viewer. When the <strong>View</strong> button is clicked then once we have the <strong>urn</strong> of the file created for viewing the document, we can show the iframe - if it&#39;s already visible then we hide it:</p>
<pre>private void ShowInViewer(HtmlControl viewer)
{
  string encToken = HttpUtility.UrlEncode(_accessToken);
  string encUrn = HttpUtility.UrlEncode(_base64URN);

  // Show in iframe.
  // If it&#39;s already showing something, then make it disappear,
  // otherwise make it appear and show the file
  if (viewer.Style[&quot;display&quot;] == &quot;block&quot;)
  {
    viewer.Style[&quot;display&quot;] = &quot;none&quot;;
    viewer.Attributes[&quot;src&quot;] = &quot;&quot;;
  }
  else
  {
    viewer.Style[&quot;display&quot;] = &quot;block&quot;;
    viewer.Attributes[&quot;src&quot;] = &quot;MyViewerPage.aspx?accessToken=&quot; + 
      encToken + &quot;&amp;urn=&quot; + encUrn;
  }
}</pre>
<p>The project is using <strong>RestSharp Signed</strong> (the signed version<strong>&#0160;</strong>since&#0160;it can only use signed assemblies). This can be installed through <a href="http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c" target="_self"><strong>NuGet Package Manager</strong></a> that can be added to <strong>Visual Studio</strong> from <strong>Tools &gt; Extension Manager</strong>. You will probably have to <a href="http://msdn.microsoft.com/en-us/library/dkkx7f79(v=vs.110).aspx" target="_self">install</a> <strong>RestSharp</strong> assembly in the <strong>GAC</strong> in order to make thigs work.</p>
<p>For debugging purposes I also added a <strong>TextBox</strong> at the top of the page so that I can easily see all the messages I log at certain points in the code.&#0160;See&#0160;<strong>MyVisualWebPart </strong>C# project&#0160;on github.</p>
<p><strong>5) SharePoint page</strong></p>
<p>Now that we have the <strong>Web Part</strong>, we can create a <strong>SharePoint</strong> page that will use it. &#0160;&#0160;</p>
<p>From the main page of your&#0160;<strong>SharePoint</strong>&#0160;site you can go <strong>Site Actions &gt; More Options &gt; Page &gt; Web Part Page&#0160;&#0160;</strong></p>
<p><strong> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73de7005f970d-pi" style="display: inline;"><img alt="Webpartpage" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73de7005f970d image-full img-responsive" src="/assets/image_ae3f26.jpg" title="Webpartpage" /></a><br /></strong></p>
<p>Then we can add the <strong>Web Part</strong> we previously created:&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73de70322970d-pi" style="display: inline;"><img alt="Webpart1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73de70322970d image-full img-responsive" src="/assets/image_409516.jpg" title="Webpart1" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73de7032b970d-pi" style="display: inline;"><img alt="Webpart2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73de7032b970d image-full img-responsive" src="/assets/image_ed296a.jpg" title="Webpart2" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73de7032f970d-pi" style="display: inline;"><img alt="Webpart3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73de7032f970d image-full img-responsive" src="/assets/image_23d8bd.jpg" title="Webpart3" /></a></p>
<p>See&#0160;<strong>MyWebPart.aspx</strong>&#0160;on github.</p>
<p>That&#39;s it.&#0160;Now we have our web page inside <strong>SharePoint</strong> that will list our documents and enables us to view those documents with the Autodesk viewing service.</p>
<p>&#0160;</p>
