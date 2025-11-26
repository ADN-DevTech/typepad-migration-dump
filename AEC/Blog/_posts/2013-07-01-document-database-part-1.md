---
layout: "post"
title: "Document Database - part 1"
date: "2013-07-01 02:20:26"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/07/document-database-part-1.html "
typepad_basename: "document-database-part-1"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>Document database is a kind of way to store the extension data in the document. It is not visible to the end user. The data is organized in a format of SQL database and follows the SQL syntax. So you will feel much comfortable if you are familiar with SQL. In case, you are not skillful, here is one link I think it may help you to get started with SQL.</p>
<p><a href="http://www.w3schools.com/sql/">http://www.w3schools.com/sql/</a></p>
<p>We have a very good sample in SDK <em>\api\NET\examples\Basic Examples\CSharp\WPF\DatabaseDockPane\</em>. In this article, I&#39;d like to get through the relevant objects and basic usage of the database.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01910405e20c970c-pi" style="display: inline;"><img alt="7-1-2013 4-54-34 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01910405e20c970c" src="/assets/image_642586.jpg" title="7-1-2013 4-54-34 PM" /></a><br />copyright belongs to image source: <a href="http://www.filemakerstudio.com.au/database-development">www.filemakerstudio.com.au</a>&#0160;</p>
<p>
Every document has a property 
Document.Database which is a object type <strong>DocumentDatabase</strong>. It is the main entry of the relevant APIs. The database can contain many tables. It supports the common usages of database: Create, Add,&#0160;&#0160;Query,&#0160;Modify,&#0160;Delete.</p>
<p>- <strong>NavisworksCommand</strong>: derives from .NET DbCommand. It represents an SQL statement or stored procedure to execute against a data source. It provides a base class for database-specific classes that represent commands.</p>
<p>- <strong>NavisworksDataAdapter</strong>: derives from .NET DbDataAdapter . It inheritors of DbDataAdapter implement a set of functions to provide strong typing, but inherit most of the functionality needed to fully implement a DataAdapter .</p>
<p>- <strong>NavisworksConnection</strong>: derives from .NET DbConnection. It represents a connection to a database.</p>
<p>- <strong>NavisworksTransaction</strong>: used to manipulate the transction of database.</p>
<p>Let us take a look how to create a table:</p>
<pre><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;"><span style="color: #0000ff;">void</span> createDBTable()</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;">{</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt; color: #60bf00;">    //get document database </span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;"><span style="color: #0080ff;">    DocumentDatabase</span> database =</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;">        Autodesk.Navisworks.Api.<span style="color: #0080ff;">Application</span>.ActiveDocument.Database;</span><br />&#0160;<br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt; color: #00bf00;">    // use transaction. The type for creation is Reset </span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;"><span style="color: #0000ff;">    using</span> (<span style="color: #0080ff;">NavisworksTransaction</span> trans = </span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;">        database.BeginTransaction(<span style="color: #0080ff;">DatabaseChangedAction</span>.Reset))</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;">    {</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt; color: #00bf00;">        //setup SQL command  </span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;"><span style="color: #0080ff;">        NavisworksCommand</span> cmd = trans.Connection.CreateCommand();</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt; color: #00bf00;">        //creation of SQL syntax</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;">        string sql = <span style="color: #c00000;">&quot;CREATE TABLE IF NOT EXISTS order_test(&quot;</span>+</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;"><span style="color: #c00000;">                    &quot;item_name TEXT,&quot;</span>+  </span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;"><span style="color: #c00000;">                    &quot;count INTEGER DEFAULT 1,&quot;</span>+ </span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;"><span style="color: #c00000;">                    &quot;price CURRENCY DEFAULT 100.55)&quot;</span>;</span><br /><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;">        cmd.CommandText = sql;</span><br />    <br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt; color: #00bf00;">        // do the job</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;">        cmd.ExecuteNonQuery();</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt; color: #00bf00;">        //submit the transaction</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;">        trans.Commit();</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;">    }</span><br /><span style="font-family: &#39;times new roman&#39;, times; font-size: 8pt;">}</span></pre>
<p>&#0160;</p>
<p>By this code, we created a table named &quot;order_test&quot;. It has three columns: &#0160;</p>
<p>item_name: which is a Text<br />count: which is an integer<br />price: which is a currency (double)</p>
<p>In the next post, we will see how to fill in the table and query it.</p>
<p>&#0160;</p>
