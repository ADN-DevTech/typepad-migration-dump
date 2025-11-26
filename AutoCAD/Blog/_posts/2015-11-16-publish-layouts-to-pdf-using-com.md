---
layout: "post"
title: "Publish Layouts to PDF using COM"
date: "2015-11-16 00:02:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/11/publish-layouts-to-pdf-using-com.html "
typepad_basename: "publish-layouts-to-pdf-using-com"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>  <p>We don’t have a direct API to publish multiple layouts in to single PDF document, we may have to rely some hard code work</p>  <p>In this blog I will show how to create a simple DSD, and use this DSD file to execute publish command.</p>  <pre>		
		class DSDObject
		{

			public DSDObject()
			{

			}
			
			public string DWG { get; set; }
			public string Layout { get; set; }
			public string Setup { get; set; }
			public string dwgName { get; set; }
			public String createDSDEntry()
			{
			
				StringBuilder sb = new StringBuilder();
				sb.Append(&quot;[DWF6Sheet:&quot; + this.dwgName + &quot;-&quot; + Layout + &quot;]&quot;);
				sb.AppendLine();
				sb.Append(&quot;DWG=&quot; + DWG);
				sb.AppendLine();
				sb.Append(&quot;Layout=&quot; + Layout);
				sb.AppendLine();
				sb.Append(&quot;Setup=&quot;);
				return sb.ToString();
			}

		}

		public static void TP()
		{
			Autodesk.AutoCAD.Interop.AcadApplication acadCOMApp;
			acadCOMApp = (Autodesk.AutoCAD.Interop.AcadApplication)Application.AcadApplication;
			AcadDocument acadDoc = acadCOMApp.ActiveDocument;
			string drawingName = acadDoc.Name;
			string drawingPath = acadDoc.FullName;
			List<string> entries = new List<string>();


			foreach(AcadLayout alayout in acadDoc.Layouts)
			{
				DSDObject dsdObj = new DSDObject();
						
					dsdObj.DWG = drawingPath;
					dsdObj.dwgName = drawingName;
					dsdObj.Layout = alayout.Name;
					dsdObj.Setup = &quot;&quot;;

					entries.Add(dsdObj.createDSDEntry());
			}
		
			StreamWriter writer = new StreamWriter(&quot;c:\\trash\\testDSD.dsd&quot;);
			writer.WriteLine(&quot;[DWF6Version]&quot;);
			writer.WriteLine(&quot;Ver=1&quot;);
			foreach(string entry in entries)
			{
			   writer.WriteLine(entry);
			}

			writer.WriteLine(&quot;[Target]&quot;);
			writer.WriteLine(&quot;Type=6&quot;);

			writer.WriteLine(&quot;DWF=C:\\Users\\moogalm\\Desktop\\Kitchens.pdf&quot;);
			writer.WriteLine(&quot;OUT=C:\\Users\\moogalm\\Desktop\\&quot;);
			writer.WriteLine(&quot;PWD=&quot;);
			writer.Close();
			FileInfo fi = new FileInfo(&quot;C:\\Trash\\testDSD.dsd&quot;);
			if (fi.Length &gt; 0)
			{
				acadDoc.SetVariable(&quot;FILEDIA&quot;, 0);
				acadDoc.SendCommand(&quot;-PUBLISH &quot; + fi.FullName +&quot;\n&quot;);
			}
				
			
			
		}</pre>
