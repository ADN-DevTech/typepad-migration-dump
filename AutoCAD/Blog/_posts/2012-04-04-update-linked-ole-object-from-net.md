---
layout: "post"
title: "Update linked OLE object from .NET"
date: "2012-04-04 05:26:51"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/update-linked-ole-object-from-net.html "
typepad_basename: "update-linked-ole-object-from-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you have a linked OLE object (referencing a bitmap, Excel sheet, etc.) inserted in your AutoCAD drawing and you know the object it referencing changed, then you may want to get this object updated programmatically.</p>
<p>The Ole2Frame .NET object has an OleObject property which gives back a wrapped pointer to the underlying COleClientItem MFC object. This object has an UpdateLink function. Having not found a .NET equivalent of that I decided to P/Invoke the UpdateLink function of the MFC class, which in case of MFC 9.0 is implemented inside mfc90u.dll</p>
<p>If you check that dll with depends.exe then you’ll see that the signature of that function is not exported. So you can use a utility called dumpbin.exe to find the ordinal number of the function that we could use. Typing this in the Visual Studio Command Prompt you’ll get back a text file that contains information about UpdateLink as well:</p>
<p>c:\Program Files\Microsoft Visual Studio 9.0\VC&gt;dumpbin /exports mfc90u.lib /out:C:\exports.txt</p>
<p>The above text file gives us the ordinal number plus the mangled name of the function:</p>
<p>6766&nbsp;&nbsp;&nbsp; ?UpdateLink@COleClientItem@@QAEHXZ (public: int __thiscall COleClientItem::UpdateLink(void))</p>
<p>Since Ole2Frame.OleObject gives back a wrapped pointer that you cannot directly pass to UpdateLink, therefore we might as well P/Invoke AcDbOle2Frame::getOleClientItem from acdb18.dll, which provides us with the pointer that we can pass in to UpdateLink. To find out the mangled name of getOleClientItem, you can simply use depends.exe to look into acdb18.dll</p>
<p>Once we have everything we need to P/Invoke those two functions, we can write the following code (this should work for AutoCAD 2010-2012 32 bit):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">DllImport</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #a31515;">"mfc90u.dll"</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; CallingConvention = </span><span style="line-height: 140%; color: #2b91af;">CallingConvention</span><span style="line-height: 140%;">.ThisCall,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; EntryPoint=</span><span style="line-height: 140%; color: #a31515;">"#6766"</span><span style="line-height: 140%;">)] </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">extern</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; COleClientItem_UpdateLink(</span><span style="line-height: 140%; color: #2b91af;">IntPtr</span><span style="line-height: 140%;"> thisClientItem);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">DllImport</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #a31515;">"acdb18.dll"</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; CallingConvention = </span><span style="line-height: 140%; color: #2b91af;">CallingConvention</span><span style="line-height: 140%;">.ThisCall,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; EntryPoint = </span><span style="line-height: 140%; color: #a31515;">"?getOleClientItem@AcDbOle2Frame@@QBEPAVCOleClientItem@@XZ"</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">extern</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">IntPtr</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbOle2Frame_getOleClientItem(</span><span style="line-height: 140%; color: #2b91af;">IntPtr</span><span style="line-height: 140%;"> thisOle2Frame);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[CommandMethod(</span><span style="line-height: 140%; color: #a31515;">"UpdateOleClient"</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> UpdateOleClient()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; Editor ed = acApp.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; PromptEntityResult per = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ed.GetEntity(</span><span style="line-height: 140%; color: #a31515;">"Select an OLE object to update"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (per.Status != PromptStatus.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; Database db = HostApplicationServices.WorkingDatabase; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (Transaction tr = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Ole2Frame o2f = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Ole2Frame)tr.GetObject(per.ObjectId, OpenMode.ForWrite);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">IntPtr</span><span style="line-height: 140%;"> ptrClientItem = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; AcDbOle2Frame_getOleClientItem(o2f.UnmanagedObject);&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; COleClientItem_UpdateLink(ptrClientItem);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; tr.Commit(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Note: the EntryPoint’s inside the DllImport attributes may need to be changed depending on the version of AutoCAD and MFC, plus the OS platform (32 vs. 64 bit) you are using.</p>
<p>The above code does not seem get the OLE object updated if it’s open in another application – e.g. the bitmap is open in Microsoft Paint.</p>
