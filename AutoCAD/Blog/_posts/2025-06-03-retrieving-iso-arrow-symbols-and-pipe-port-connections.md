---
layout: "post"
title: "Retrieving Iso Arrow Symbols and Pipe Port Connections"
date: "2025-06-03 01:40:44"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Plant3D"
original_url: "https://adndevblog.typepad.com/autocad/2025/06/retrieving-iso-arrow-symbols-and-pipe-port-connections.html "
typepad_basename: "retrieving-iso-arrow-symbols-and-pipe-port-connections"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>We recently received a question from a customer asking how to programmatically access an <code>ISO Arrow entity</code> and its direction within AutoCAD Plant 3D. Since detailed documentation on this specific task can be hard to find, I decided to share the code I developed to achieve this.</p>
<p>The C# snippet below demonstrates how to retrieve an ISO Arrow entity and its direction from a selected Pipe component in an AutoCAD Plant 3D environment. It leverages the Plant 3D API to access the Pipe object, its ports, and connections, then checks if these ports are linked to an ISO Arrow symbol.</p>
<pre class="prettyprint lang-csharp"><code>
        public static void GetIsoArrowEntity()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            PromptEntityResult per = ed.GetEntity(&quot;\nSelect a Pipe component:&quot;);
            if (per.Status != PromptStatus.OK)
            {
                ed.WriteMessage(&quot;\nCommand cancelled.&quot;);
                return;
            } 
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {

                ObjectId pipeId = per.ObjectId;
                if (pipeId.IsNull)
                {
                    ed.WriteMessage(&quot;\nPipe object not found.&quot;);
                    return;
                }
                Pipe pipeObj = tr.GetObject(pipeId, OpenMode.ForRead) as Pipe;

                if (pipeObj == null)
                {
                    ed.WriteMessage(&quot;\nObject is not a valid PnP3dObject.&quot;);
                    return;
                }
                //get symbolic ports
                var portCollection = pipeObj.GetPorts(PortType.Symbolic);
                foreach(Port port in portCollection)
                {
                    Vector3d dir = port.Direction;
                    ed.WriteMessage(dir.ToString());
                }
                PipingProject pipingProject = PlantApplication.CurrentProject.ProjectParts[&quot;Piping&quot;] as PipingProject;
                DataLinksManager dlm = pipingProject.DataLinksManager;               


                Autodesk.ProcessPower.PnP3dObjects.ConnectionManager connManager = new ConnectionManager();
                if (connManager == null)
                {
                    ed.WriteMessage(&quot;\nConnection Manager is not available.&quot;);
                    return;
                }

                var connections = connManager.GetConnections(pipeId);
                if (connections == null || connections.Count == 0)
                {
                    ed.WriteMessage(&quot;\nNo connections found for the selected pipe.&quot;);
                    return;
                }
                foreach (Connection connection in connections)
                {
                    ed.WriteMessage(&quot;\nConnection found between: &quot; + connection.ObjectId1.ToString() + &quot; and &quot; + connection.ObjectId2.ToString());
                    ed.WriteMessage(&quot;\nPort 1: &quot; + connection.Port1.Name + &quot;, Port 2: &quot; + connection.Port2.Name);
                }

                foreach (Port pPort in portCollection)
                {
                   
                    ed.WriteMessage(&quot;\nName of this Port = &quot; + pPort.Name);
                    ed.WriteMessage(&quot;\nDirection of this Port = &quot; + pPort.Direction.ToString());
                    Pair pair1 = new Pair
                    {
                        ObjectId = pipeId,
                        Port = pPort
                    };
                    if (connManager.IsConnected(pair1))
                    {
                        ed.WriteMessage(&quot;\n Pair is connected &quot;);
                        Pair connectedPair = connManager.GetConnectedPairAt(pair1);
                        var arrowSymbol =  tr.GetObject(connectedPair.ObjectId, OpenMode.ForRead) as IsoArrowSymbol;
                        var direction = arrowSymbol.Direction;
                        ed.WriteMessage(&quot;\nArrow Symbol Direction: &quot; + direction.ToString());                                               
                        double fixedLength = direction.Length;
                        ed.WriteMessage(&quot;\nFixed Length of Arrow Symbol: &quot; + fixedLength.ToString());


                    }
                    else
                    {
                        ed.WriteMessage(&quot;\n Pair is NOT connected &quot;);
                    }
                }
                tr.Commit();
            }
        }
  </code></pre>
<h3>Understanding the Output: Direction and Length</h3>
<p>When you run this command, it will display the <strong>direction of the ISO Arrow entity</strong> associated with the selected Pipe component. You might notice that the <code>Vector3d</code> representing the direction often has a <strong>non-unit magnitude</strong> (i.e., its length is not 1).</p>
<p>This is an important detail! Unlike a normalized vector that solely indicates direction, the <code>Direction</code> property of an <code>IsoArrowSymbol</code> in Plant 3D actually incorporates its <strong>fixed length</strong>. This means the magnitude of the <code>Vector3d</code> directly corresponds to the <strong>displayed length of the arrow symbol</strong> itself, as seen in the Properties palette of the <code>IsoArrowSymbol</code>.</p>
<p>This insight is crucial for developers who need to understand not just the orientation, but also the geometrical characteristics of these symbols within the Plant 3D drawing.</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d4ffa8200c-pi" style="float: left;"><img alt="Pipe-IsoSymbol" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d4ffa8200c img-responsive" src="/assets/image_579391.jpg" style="margin: 0px 5px 5px 0px;" title="Pipe-IsoSymbol" /></a></p>
