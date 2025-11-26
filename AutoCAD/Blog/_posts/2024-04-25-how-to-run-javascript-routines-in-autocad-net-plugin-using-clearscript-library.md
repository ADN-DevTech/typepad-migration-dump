---
layout: "post"
title: "How to Run Javascript Routines in AutoCAD .NET Plugin Using ClearScript Library"
date: "2024-04-25 19:53:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/04/how-to-run-javascript-routines-in-autocad-net-plugin-using-clearscript-library.html "
typepad_basename: "how-to-run-javascript-routines-in-autocad-net-plugin-using-clearscript-library"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p><p>This code demonstrates how to execute JavaScript functions or routines in your AutoCAD .NET plugin using ClearScript library from Microsoft.</p><p>This project is targeted to work with .NET 8.0&nbsp; platform in AutoCAD 2025.</p><ul><li>Exposing AutoCAD .NET Classes: </li><ul><ul><li> Enables access to AutoCAD functionality from JavaScript scripts.</li></ul></ul><li>Executing JavaScript Functions: </li><ul><ul><li>Allows you to run JavaScript functions within your AutoCAD application.</li></ul></ul><li>Calling C# Functions from JavaScript:</li><ul><ul><li> Facilitates interaction between your JavaScript code and C# functions.</li></ul></ul></ul>



<pre class="prettyprint">//Exposing AutoCAD .NET classes to JavaScript
public class PrintMessage
{
    private static Editor? _ed
    {
        get
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc != null)
            {
                return doc.Editor;
            }
            return null;
        }
    }
    public static void Print(string message)
    {
        _ed?.WriteMessage($"{message}\n");
    }
}

public class Commands
{
    private const string _script = @"
                        function square(x) {
                            return x*x;
                        }";
        
    //Execute JS functions in DotNet runtime


    [CommandMethod("JSRoutine")]
    public static void JSRoutine()
    {
        Document doc = Application.DocumentManager.MdiActiveDocument;
        if (doc is null) return;
        Editor ed = doc.Editor;

        // Create aV8ScriptEngine instance, 
        using (var engine = new V8ScriptEngine())
        {
            engine.AccessContext = typeof(Commands);

                //Now expose the PrintMessage class from .NET to the script engine,
            // and then execute a JavaScript statement that writes a message to the AutoCAD Editor.
            engine.AddHostType("PrintMessage", typeof(PrintMessage));              
            engine.Execute("PrintMessage.Print('Hello from JavaScript!');");

            //Call the JS function square and retrieve the result.
            engine.Execute(_script);
            dynamic? result = engine.Script.square(5);
            ed.WriteMessage($"{Convert.ToString(result)}\n");
                

            //Execute the JS code and print the result to the AutoCAD Editor                
            engine.Execute("function print(x) {PrintMessage.Print(x); }");
            engine.Script.print(DateTime.Now.DayOfWeek.ToString());

            //Calling C# Functions from JavaScript:              
            engine.AddHostObject("Greet", new Func<string string="" ,="">((name) =&gt; $"Hello, {name}!"));              
            engine.Execute("var message = Greet('World'); PrintMessage.Print(message);");

            // examine a script object            
            engine.Execute("var person = { name: 'Fred', age: 5 }");
            ed.WriteMessage($"From Script: \n{Convert.ToString(engine.Script.person.name)}");
        }
    }
}
</string></pre>


Source Code: <a title="https://github.com/MadhukarMoogala/AcadPlugin/blob/main/README.md" href="https://github.com/MadhukarMoogala/AcadPlugin/blob/main/README.md">https://github.com/MadhukarMoogala/AcadPlugin/blob/main/README.md</a>
