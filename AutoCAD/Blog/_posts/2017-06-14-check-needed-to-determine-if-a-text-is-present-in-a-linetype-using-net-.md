---
layout: "post"
title: "Check needed to determine if a text is present in a Linetype using .NET "
date: "2017-06-14 05:30:12"
author: "Deepak A S Nadig"
categories:
  - ".NET"
  - "AutoCAD"
  - "Deepak A S Nadig"
original_url: "https://adndevblog.typepad.com/autocad/2017/06/check-needed-to-determine-if-a-text-is-present-in-a-linetype-using-net-.html "
typepad_basename: "check-needed-to-determine-if-a-text-is-present-in-a-linetype-using-net-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/deepak-nadig.html" target="_self">Deepak Nadig</a></p>
<p>Recently, an ADN partner requested for a method to check if a text is contained in a Linetype&#0160;before extracting the text.</p>
<p>To extract a text, LinetypeTableRecord.TextAt is used and it returns eNotApplicable error when text is not present at index in the record. To avoid this, null id check using LinetypeTableRecord.ShapeStyleAt can be used. This&#0160;method returns null, if&#0160;text is not&#0160;present at an index&#0160;in the LinetypeTableRecord.</p>
<p>Here is a quick sample to check if text is present prior to extraction of the text :&#0160;&#0160;</p>
<pre style="color: #d1d1d1; background: #000000;"><span style="color: #d2cd86;">[</span>CommandMethod<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">lineTypeText</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">]</span>
<span style="color: #e66170; font-weight: bold;">public</span> <span style="color: #e66170; font-weight: bold;">void</span> lineTypeText<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span> <span style="color: #b060b0;">{</span>
Document doc <span style="color: #d2cd86;">=</span> Application<span style="color: #d2cd86;">.</span>DocumentManager<span style="color: #d2cd86;">.</span>MdiActiveDocument<span style="color: #b060b0;">;</span>
Database db <span style="color: #d2cd86;">=</span> doc<span style="color: #d2cd86;">.</span>Database<span style="color: #b060b0;">;</span>
Transaction tr <span style="color: #d2cd86;">=</span> db<span style="color: #d2cd86;">.</span>TransactionManager<span style="color: #d2cd86;">.</span>StartTransaction<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>

Editor ed <span style="color: #d2cd86;">=</span> doc<span style="color: #d2cd86;">.</span>Editor<span style="color: #b060b0;">;</span>
<span style="color: #e66170; font-weight: bold;">using</span> <span style="color: #d2cd86;">(</span>tr<span style="color: #d2cd86;">)</span> <span style="color: #b060b0;">{</span>
DBObject tmpObj <span style="color: #d2cd86;">=</span> tr<span style="color: #d2cd86;">.</span>GetObject<span style="color: #d2cd86;">(</span>db<span style="color: #d2cd86;">.</span>LinetypeTableId<span style="color: #d2cd86;">,</span> OpenMode<span style="color: #d2cd86;">.</span>ForRead<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
LinetypeTable pLineTable <span style="color: #d2cd86;">=</span> <span style="color: #d2cd86;">(</span>LinetypeTable<span style="color: #d2cd86;">)</span>tmpObj<span style="color: #b060b0;">;</span>
<span style="color: #e66170; font-weight: bold;">if</span> <span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">(</span>pLineTable <span style="color: #d2cd86;">!</span><span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">null</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">)</span> <span style="color: #b060b0;">{</span>
	<span style="color: #e66170; font-weight: bold;">foreach</span> <span style="color: #d2cd86;">(</span>ObjectId tblRecId <span style="color: #e66170; font-weight: bold;">in</span> pLineTable<span style="color: #d2cd86;">)</span> <span style="color: #b060b0;">{</span>

		LinetypeTableRecord pLineType <span style="color: #d2cd86;">=</span> <span style="color: #d2cd86;">(</span>LinetypeTableRecord<span style="color: #d2cd86;">)</span>tr<span style="color: #d2cd86;">.</span>GetObject<span style="color: #d2cd86;">(</span>tblRecId<span style="color: #d2cd86;">,</span> OpenMode<span style="color: #d2cd86;">.</span>ForRead<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
		<span style="color: #e66170; font-weight: bold;">if</span> <span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">(</span>pLineType <span style="color: #d2cd86;">!</span><span style="color: #d2cd86;">=</span> <span style="color: #e66170; font-weight: bold;">null</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">)</span> <span style="color: #b060b0;">{</span>
			<span style="color: #e66170; font-weight: bold;">for</span> <span style="color: #d2cd86;">(</span><span style="color: #e66170; font-weight: bold;">int</span> dash <span style="color: #d2cd86;">=</span> <span style="color: #008c00;">0</span><span style="color: #b060b0;">;</span> dash <span style="color: #d2cd86;">&lt;</span> pLineType<span style="color: #d2cd86;">.</span>NumDashes<span style="color: #b060b0;">;</span> dash<span style="color: #d2cd86;">+</span><span style="color: #d2cd86;">+</span><span style="color: #d2cd86;">)</span> <span style="color: #b060b0;">{</span>
				<span style="color: #9999a9;">/* LinetypeTableRecord.ShapeStyleAt returns the ObjectId of the TextStyleTableRecord of the shape </span>
<span style="color: #9999a9;">				(or textStyle if it&#39;s a text string instead of a shape) at position index in the LinetypeTableRecord.</span>
<span style="color: #9999a9;">				If there is no shape or text at index, then Null is returned.*/</span>
				ObjectId objIdShape <span style="color: #d2cd86;">=</span> pLineType<span style="color: #d2cd86;">.</span>ShapeStyleAt<span style="color: #d2cd86;">(</span>dash<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
				<span style="color: #e66170; font-weight: bold;">if</span> <span style="color: #d2cd86;">(</span>objIdShape <span style="color: #d2cd86;">!</span><span style="color: #d2cd86;">=</span> ObjectId<span style="color: #d2cd86;">.</span>Null<span style="color: #d2cd86;">)</span> <span style="color: #b060b0;">{</span>
					<span style="color: #e66170; font-weight: bold;">string</span> pText <span style="color: #d2cd86;">=</span> <span style="color: #02d045;">&quot;</span><span style="color: #02d045;">&quot;</span><span style="color: #b060b0;">;</span>
					pText <span style="color: #d2cd86;">=</span> pLineType<span style="color: #d2cd86;">.</span>TextAt<span style="color: #d2cd86;">(</span>dash<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
					<span style="color: #e66170; font-weight: bold;">if</span> <span style="color: #d2cd86;">(</span>pText <span style="color: #d2cd86;">=</span><span style="color: #d2cd86;">=</span> <span style="color: #02d045;">&quot;</span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">)</span> <span style="color: #b060b0;">{</span>
						ed<span style="color: #d2cd86;">.</span>WriteMessage<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">no dash text </span><span style="color: #02d045;">&quot;</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
					<span style="color: #b060b0;">}</span> <span style="color: #e66170; font-weight: bold;">else</span> <span style="color: #b060b0;">{</span>
						ed<span style="color: #d2cd86;">.</span>WriteMessage<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">dash :</span><span style="color: #02d045;">&quot;</span> <span style="color: #d2cd86;">+</span> dash<span style="color: #d2cd86;">.</span>ToString<span style="color: #d2cd86;">(</span><span style="color: #d2cd86;">)</span><span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
						ed<span style="color: #d2cd86;">.</span>WriteMessage<span style="color: #d2cd86;">(</span><span style="color: #02d045;">&quot;</span><span style="color: #00c4c4;">\ndash text :</span><span style="color: #02d045;">&quot;</span> <span style="color: #d2cd86;">+</span> pText<span style="color: #d2cd86;">)</span><span style="color: #b060b0;">;</span>
					<span style="color: #b060b0;">}</span>

				<span style="color: #b060b0;">}</span>
			<span style="color: #b060b0;">}</span>
		<span style="color: #b060b0;">}</span>
	<span style="color: #b060b0;">}</span>
<span style="color: #b060b0;">}</span>

<span style="color: #b060b0;">}</span>
<span style="color: #b060b0;">}</span>
</pre>
<p>The output generated by this code snippet is as seen in this screenshot :</p>
<p>&#0160;&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09a572f3970d-pi" style="float: left;"><img alt="LineTypeText" class="asset  asset-image at-xid-6a0167607c2431970b01bb09a572f3970d img-responsive" src="/assets/image_241045.jpg" style="margin: 0px 5px 5px 0px;" title="LineTypeText" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>Update: &#0160;The above code is&#0160;not equipped to handle&#0160;shape\symbol in linetype. You can use the LinetypeTableRecord.ShapeNumberAt API&#0160;to determine if shape exists. It returns 0 if shape does not exist. Below is the modified code that Can be used for testing :&#0160;</p>

<div style="background: #ffffff; overflow: auto; width: auto; border: solid gray; border-width: .1em .1em .1em .8em; padding: .2em .6em;">
<pre style="margin: 0; line-height: 125%;"><span style="color: #0000cc;">[CommandMethod(&quot;lineTypeText&quot;)]</span>
<span style="color: #008800; font-weight: bold;">public</span> <span style="color: #008800; font-weight: bold;">void</span> <span style="color: #0066bb; font-weight: bold;">lineTypeText</span>()
{
    Document doc = Application.DocumentManager.MdiActiveDocument;
    Database db = doc.Database;
    Transaction tr = db.TransactionManager.StartTransaction();
 
    Editor ed = doc.Editor;
    <span style="color: #008800; font-weight: bold;">using</span> (tr)
    {
        DBObject tmpObj = tr.GetObject(db.LinetypeTableId, OpenMode.ForRead);
        LinetypeTable pLineTable = (LinetypeTable)tmpObj;
        <span style="color: #008800; font-weight: bold;">if</span> ((pLineTable != <span style="color: #008800; font-weight: bold;">null</span>))
        {
            <span style="color: #008800; font-weight: bold;">foreach</span> (ObjectId tblRecId <span style="color: #008800; font-weight: bold;">in</span> pLineTable)
            {
 
                LinetypeTableRecord pLineType = (LinetypeTableRecord)tr.GetObject(tblRecId, OpenMode.ForRead);
                <span style="color: #008800; font-weight: bold;">if</span> ((pLineType != <span style="color: #008800; font-weight: bold;">null</span>))
                {
                    <span style="color: #008800; font-weight: bold;">for</span> (<span style="color: #333399; font-weight: bold;">int</span> dash = <span style="color: #6600ee; font-weight: bold;">0</span>; dash &lt; pLineType.NumDashes; dash++)
                    {
                        ObjectId objIdShape = pLineType.ShapeStyleAt(dash); <span style="color: #888888;">//If there is no shape or text at index, then Null is returned.</span>
 
                        <span style="color: #333399; font-weight: bold;">int</span> shpNum = pLineType.ShapeNumberAt(dash);
                        <span style="color: #008800; font-weight: bold;">if</span> (objIdShape != ObjectId.Null)
                        {
                            ed.WriteMessage(<span style="background-color: #fff0f0;">&quot;text or shape exist;&quot;</span>);
                            <span style="color: #333399; font-weight: bold;">string</span> pText = <span style="background-color: #fff0f0;">&quot;&quot;</span>;
                            <span style="color: #888888;">//returns 0 if there is no shape\symbol at the index, which means text exist in this context</span>
                            <span style="color: #008800; font-weight: bold;">if</span> (shpNum == <span style="color: #6600ee; font-weight: bold;">0</span>)
                            {
                                pText = pLineType.TextAt(dash);                                       
                                ed.WriteMessage(<span style="background-color: #fff0f0;">&quot; dash text :&quot;</span> + pText );
                            }
                            ed.WriteMessage(<span style="background-color: #fff0f0;">&quot;\n&quot;</span>);
 
                        }
                    }
                }
            }
        }
 
    }
}
</pre>
</div>
