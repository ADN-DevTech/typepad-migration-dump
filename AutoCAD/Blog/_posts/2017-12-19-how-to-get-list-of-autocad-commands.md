---
layout: "post"
title: "How To Get List of AutoCAD Commands"
date: "2017-12-19 22:57:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "AutoCAD OEM"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2017/12/how-to-get-list-of-autocad-commands.html "
typepad_basename: "how-to-get-list-of-autocad-commands"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p>

<p>I have received a query from customer to retrieve list of AutoCAD Commands loaded in to AutoCAD Domain.<p> I don't think there is a single API which can fetch you list of AutoCAD BuiltIn Commands.<p>here are few attempts:<ul><li>You can use _.ARX with Commands flag to retrieve the list of commands of all registered&nbsp; arx programs with AutoCAD.</li></ul><p>Best way to do this, create a arx.scr script file with following contents<blockquote><p>_.ARX<p>_Commands<p>*<p>or,<p>In CLI, use<p>D:\Program Files\Autodesk\AutoCAD 2017&gt;accoreconsole /s arx.scr &gt; D:\Temp\cmds.txt</p></blockquote><ul><li>PGP files stores alias keys for all ACAD commands irrespective of technology used to create them.</li></ul><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; You can find other PGP file like Synonyms in this folder <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; C:\Users\&lt;user&gt;\AppData\Roaming\Autodesk\AutoCAD 20xx\Rxx.x\enu\Support<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; or <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; On the command line in all versions of AutoCAD except AutoCAD LT, type the following: <blockquote><p>(findfile "acad.pgp")</p></blockquote><ul><li>AcEdCommandIterator is not exposed in .NET, but you can use following code in C++ and </li></ul><p>I would like to emphasis that commands of only loaded modules can retrieved, for example using above workflow CAMERA command can't be found, because AcCamera.arx modules has not been loaded into ACAD domain, hence it is not yet registered with AcEdCommandStack.
<pre class="prettyprint">void cmdExtract()
{
AcEdCommandIterator* iter = acedRegCmds-&gt;iterator();
if (iter == NULL)
{
return;
}
int n = 0;
FILE *fptr;
fopen_s(&amp;fptr,"D:\\Temp\\Commands.txt","a");
for (;!iter-&gt;done(); iter-&gt;next())
{
const AcEdCommand* pp = iter-&gt;command();
fwprintf(fptr,_T("%s\n"), pp-&gt;globalName());
n++;
}
fclose(fptr);
}
</pre>
<p>I would like to emphasis that commands of only loaded modules can retrieved, for example using above workflow CAMERA command can't be found, because AcCamera.arx modules has not been loaded into ACAD domain, hence it is not yet registered with AcEdCommandStack.</p>
<p>For .NET, Pinvoke required little bit work.</p>

<p> C++ code </p>
<pre class="prettyprint">//We need size to allocate memory
extern "C" __declspec(dllexport) int getSizeOfCmds()
{
	AcEdCommandIterator* iter = acedRegCmds-&gt;iterator();
	if (iter == NULL)
	{
		return -1;
	}
	int size = 0;
	for (; !iter-&gt;done(); iter-&gt;next())
	{
		const AcEdCommand* pp = iter-&gt;command();
		size++;
	}
	return size;

}
/*
Using BSTR allows us to allocate the strings in the native code,
and have them deallocated in the managed code. That's because BSTR is allocated on the shared COM heap,
and the p / invoke marshaller understands them*/
extern "C" __declspec(dllexport) Acad::ErrorStatus cmdExtract(BSTR* comStringCmdNames)
{
	
	AcEdCommandIterator* iter = acedRegCmds-&gt;iterator();
	if (iter == NULL)
	{
		return Acad::eCreateFailed;
	}
	int i = 0;
	for (;!iter-&gt;done();iter-&gt;next())
	{
		const AcEdCommand* pp = iter-&gt;command();
		//the beauty is here, sometimes COM is nice 
		//Allocate here and DeAllocate in C#
		comStringCmdNames[i]  = ::SysAllocString(pp-&gt;globalName());
	    i++;
	}

	return Acad::eOk;
}
</pre>

<p> .NET code </p>
<pre class="prettyprint">  public class MyCommands
    {
        [DllImport("D:\\Arxprojects\\ListOfCommands\\extractCommands_ARX\\x64\\Debug\\extractCommands.arx",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode,
            EntryPoint = "cmdExtract")]

        private static extern ErrorStatus cmdExtract([Out] IntPtr[] cmdNames);

        [DllImport("D:\\Arxprojects\\ListOfCommands\\extractCommands_ARX\\x64\\Debug\\extractCommands.arx",
          CallingConvention = CallingConvention.Cdecl,
          CharSet = CharSet.Unicode,
          EntryPoint = "getSizeOfCmds")]

        private static extern int getSizeOfCmds();


        [CommandMethod("ExtCmd")]
        public static void cmd()
        {
            int count = getSizeOfCmds();
            IntPtr[] cmdNames = new IntPtr[count];
            ErrorStatus es = cmdExtract(cmdNames);
            string[] names = new string[count];
            for (int i = 0; i &lt; count; i++)
            {
                names[i] = Marshal.PtrToStringBSTR(cmdNames[i]);
                Marshal.FreeBSTR(cmdNames[i]);
            }
        }
    }
 </pre>
<p>Full source code is available <a href="https://github.com/MadhukarMoogala/MyBlogs/blob/master/ListOfCommands/ListOfCommands.zip">here</a></p>
