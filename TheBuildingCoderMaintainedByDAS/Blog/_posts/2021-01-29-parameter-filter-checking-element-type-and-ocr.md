---
layout: "post"
title: "Parameter Filter Checking Element Type and OCR"
date: "2021-01-29 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Filters"
  - "Parameters"
  - "Philosophy"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/01/parameter-filter-checking-element-type-and-ocr.html "
typepad_basename: "parameter-filter-checking-element-type-and-ocr"
typepad_status: "Publish"
---

<p>Getting ready for a rainy weekend, here is an important insight in using a filtered element collector with a parameter filter, a handy open source OCR tool and a few productivity tips:</p>

<ul>
<li><a href="#2">Parameter filter also checks element type</a></li>
<li><a href="#2.1">Parameter property also checks element type</a></li>
<li><a href="#3">Capture2Text, a handy OCR tool</a></li>
<li><a href="#4">Productivity tips</a></li>
</ul>

<h4><a name="2"></a> Parameter Filter Also Checks Element Type</h4>

<p>We made an interesting discovery in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/string-parameter-filtering-is-retrieving-false-data/m-p/10034687">string parameter filtering retrieving false data</a>:</p>

<p>If the internal <code>ParameterValueProvider</code> class does not find the requested parameter from the element itself, will also try to read it from the element's type.</p>

<p>This undocumented behaviour can lead to some confusion and is difficult to fix perfectly in an efficient manner.</p>

<p><strong>Question:</strong> I am trying to filter walls based on their "Fire Rating" parameter using the <code>FilterStringContains</code> evaluator.</p>

<p>However, this filter retrieves both walls satisfying the rule plus other walls that don't even contain that parameter!</p>

<p>This seems like an error to me.</p>

<p>Here is a <a href="https://thebuildingcoder.typepad.com/files/testfilterstringcontains.rvt">sample project with a macro</a> providing a reproducible case using the following code:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>&nbsp;bip&nbsp;=&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.FIRE_RATING;

&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;pid&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(&nbsp;bip&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">ParameterValueProvider</span>&nbsp;provider
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ParameterValueProvider</span>(&nbsp;pid&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">FilterStringRuleEvaluator</span>&nbsp;evaluator&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterStringContains</span>();

&nbsp;&nbsp;<span style="color:#2b91af;">FilterStringRule</span>&nbsp;rule&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilterStringRule</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;provider,&nbsp;evaluator,&nbsp;<span style="color:#a31515;">&quot;/&quot;</span>,&nbsp;<span style="color:blue;">false</span>&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>&nbsp;filter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementParameterFilter</span>(&nbsp;rule&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;myWalls&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_Walls&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;.WherePasses(&nbsp;filter&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;false_positive_ids&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;();

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;wall&nbsp;<span style="color:blue;">in</span>&nbsp;myWalls&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;param&nbsp;=&nbsp;wall.get_Parameter(&nbsp;bip&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;<span style="color:blue;">null</span>&nbsp;==&nbsp;param&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;false_positive_ids.Add(&nbsp;wall.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;s&nbsp;=&nbsp;<span style="color:blue;">string</span>.Join(&nbsp;<span style="color:#a31515;">&quot;,&nbsp;&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;false_positive_ids.Select&lt;<span style="color:#2b91af;">ElementId</span>,&nbsp;<span style="color:blue;">string</span>&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;id&nbsp;=&gt;&nbsp;id.IntegerValue.ToString()&nbsp;)&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">TaskDialog</span>&nbsp;dlg&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">TaskDialog</span>(&nbsp;<span style="color:#a31515;">&quot;False&nbsp;Positives&quot;</span>&nbsp;);
&nbsp;&nbsp;dlg.MainInstruction&nbsp;=&nbsp;<span style="color:#a31515;">&quot;False&nbsp;filtered&nbsp;walls&nbsp;ids:&nbsp;&quot;</span>;
&nbsp;&nbsp;dlg.MainContent&nbsp;=&nbsp;s;
&nbsp;&nbsp;dlg.Show();
</pre>

<p>This displays a message like this in the sample model, listing walls that are not even equipped with the target parameter, let alone have a value for it:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdeb982a8200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdeb982a8200c img-responsive" style="width: 442px; display: block; margin-left: auto; margin-right: auto;" alt="Parameter filter returns false positives" title="Parameter filter returns false positives" src="/assets/image_c82eca.jpg" /></a><br /></p>

<p></center></p>

<p>What can I do to ignore walls that don't contain the parameter?</p>

<p><strong>Answer:</strong> This was logged as <em>REVIT-172990 &ndash; Parameter filter with FilterStringContains returns false positive</em> for the development team to analyse.</p>

<p>You might be able to achieve the desired result by combining the FilterStringContains evaluator with other rules and filters.</p>

<p>For instance, you may be able to add something like 'is not empty' in addition to 'contains'.</p>

<p>The development team analysed the issue and found the reason for this behaviour.</p>

<p>If the internal method <code>ParameterValueProvider::getParameterValue()</code> cannot get the parameter from the element itself, will also try to get it from the element's type:</p>

<pre class="code">
  if (m_elemOrSymbol == EOS_Symbol
    || err != ERR_SUCCESS || !oParameterValue)
  {
    ElementId typeId = pElement-&gt;getTypeId();
    if (validElementId(typeId))
    {
      const Element *pTypeElement
        = pElement-&gt;getDocument()-&gt;getElement(typeId);

      if (pTypeElement != NULL)
      {
        oParameterValue = pTypeElement-&gt;getParameterValue(
          m_parameter);

        err = getErrFromParameterValue(oParameterValue);
        if (err != ERR_SUCCESS)
        {
          return nullptr;
        }
      }
    }
  }
</pre>

<p>Therefore, yes, you cannot find the parameter in the walls.</p>

<p>However, you can find the parameters in their wall type.</p>

<p>That's why you can still get those walls.</p>

<p>This behaviour has been in effect for a long time, so the development team is not thinking about changing it.</p>

<p>However, this information needs to be made clear in the documentation and knowledge base.</p>

<p>A new issue <em>REVIT-173253 &ndash; Parameter filter with FilterStringContains returns false positive</em> has been raised for the documentation team to document this behaviour. </p>

<p><strong>Response:</strong> I am going to accept your reply as an accepted answer but it is not as expected.</p>

<ul>
<li>I think this behaviour is not acceptable logically, even though it was in effect for a long time.
At least it should return the wall type itself rather than returning all walls under that type.</li>
<li>Can it be modified to have a Boolean to search through the element's type or not?
Or, like the filtered element collector, e.g., <code>WhereElementIsElementType</code> and <code>WhereElementIsNotElementType</code>?</li>
</ul>

<p><strong>Answer:</strong> I agree that this behaviour defeats part of the purpose of the parameter filter.</p>

<p>If I were you, I would choose the simple solution of filtering for parameters that are guaranteed not to be present on the type.</p>

<p><strong>Answer 2:</strong> I always preferred the functionality as it is now. There are various solutions out there for finding elements that rely on not having to separately look through the parameters on the type or know that a parameter is on a type or an instance.</p>

<p>Imagine you want to find all instances that have a certain type parameter, you would first have to filter for all types, filter those for the parameter match, then filter again for all instances that are of one of those types. This is already a common approach for a lot of cases where parameter filters can’t be used.</p>

<p>I thought it just saved a lot of work really, I see no need for a change. If you want to know the parameter is on the instance then put the results through a further filter. You can supply a list of ids to a further element filter at any time. If you want to know parameter is on the type, filter first for types. In general, I don’t see much use of Logical AND/OR Filters sometimes I wonder if their existence is known of? People seem to have other methods but again use: LogicalAndFilter with</p>

<ul>
<li>ParameterElementFilter and 
<br/>ElementIsElementTypeFilter(Inverted = True)</li>
</ul>

<p>ElementParameterFilter is a slow filter, so you should first use another quick filter before it.</p>

<p>It works like in the UI when you set up visibility filters, i.e., there is no distinction between type and instance parameters there. I imagine this is the internal system that this filter class leverages.</p>

<p>Later: When I consider further there is a bit of a hole in functionality there.</p>

<p>Since you are forced to get a list of elements and then rule out those that have a type parameter match not an instance parameter match, i.e., there are four cases:</p>

<p>Parameter with matching name and value is on: Type, Instance, Both, Neither.</p>

<p>We can only find with filter that it is on: Either or Neither.</p>

<p>So, the question is, how would we find Elements with a matching parameter on the Instance only?</p>

<p>After filtering with ElementParameterFilter, we would have to manually check via Linq perhaps.
We only care that it is or isn't on the instance, so that could simplify things.</p>

<p><strong>Answer 3:</strong> Exactly what I meant by saying, defeats part of the purpose.</p>

<p>Thank you for fleshing it out in more detail.</p>

<p>Linq is super slow compared to the built-in parameter filter, and I see no other option to achieve the in-between distinctions.</p>

<p>Many thanks to Ameer and Richard for this very fruitful and illuminating discussion!</p>

<h4><a name="2.1"></a> Parameter Property Also Checks Element Type</h4>

<p>Our learning is never-ending.</p>

<p>I response to the above,
Frank <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518">@Fair59</a> Aarssen added:</p>

<blockquote>
  <p>I noticed the same behaviour for the Parameter properties of an Element.
  Getting a parameter on (family) instances by GUID or Definition also returns the type-parameter:</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833027880119257200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833027880119257200d img-responsive" style="width: 368px; display: block; margin-left: auto; margin-right: auto;" alt="Parameter property by GUID and Definition" title="Parameter property by GUID and Definition" src="/assets/image_7dd56a.jpg" /></a><br /></p>

<p></center></p>

<blockquote>
  <p>Please update the documentation for these properties as well.</p>
</blockquote>

<h4><a name="3"></a> Capture2Text, a Handy OCR Tool</h4>

<p>I happened upon a very handy OCR tool that looks pretty good:</p>

<p><a href="http://capture2text.sourceforge.net">Capture2Text</a> is
a free open source Windows desktop OCR application, licensed under the terms of the GNU General Public License.</p>

<p>It is implemented as a hotkey app, making it very minimalistic and efficient to use:</p>

<p>Click the Windows button + Q, mouse the source pixel rectangle to capture, and the resulting text is placed in the clipboard.</p>

<p>It can't get much more minimal or efficient than that, can it?</p>

<p>Besides that, Capture2Text supports:</p>

<ul>
<li>Almost a hundred target languages</li>
<li>Multiple simultaneous target languages</li>
<li>Immediate translation via Google translate</li>
<li>Text-to-speech voice</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e98c34f0200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e98c34f0200b image-full img-responsive" alt="Capture2Text" title="Capture2Text" src="/assets/image_104307.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a> Productivity Tips</h4>

<p>For more efficiency in personal and profession life in general, here are a couple of tips by
Endy Austin
on <a href="https://www.freecodecamp.org/news/how-to-get-things-done-lessons-in-productivity">how to be productive, feel less overwhelmed, and get things done</a>.</p>
