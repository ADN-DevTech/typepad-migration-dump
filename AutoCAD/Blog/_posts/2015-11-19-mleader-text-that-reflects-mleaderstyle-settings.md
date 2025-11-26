---
layout: "post"
title: "MLeader text that reflects MLeaderStyle settings"
date: "2015-11-19 02:15:20"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/11/mleader-text-that-reflects-mleaderstyle-settings.html "
typepad_basename: "mleader-text-that-reflects-mleaderstyle-settings"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a short code snippet that creates an MLeader based on an existing MLeaderStyle. For the MLeader's text to correctly follow any changes made to the MLeaderStyle, it is necessary to clone the MLeaderStyle.DefaultText and use it as the MLeader's text. Creating a new MText without relying on the DefaultText can cause the text to not reflect any later changes that are made to the MLeaderStyle.</p>
<p></p>
<p>Thanks to Xin Xu from the AutoCAD engineering team for providing this tip.</p>
<pre>
[CommandMethod("MLTest")]
public void MLTestMethod()
{
    Editor ed = default(Editor);
    ed = Application.DocumentManager.MdiActiveDocument.Editor;

    PromptPointResult ppr1 = default(PromptPointResult);
    ppr1 = ed.GetPoint(
        new PromptPointOptions("Select start point"));

    if (ppr1.Status != PromptStatus.OK)
        return;

    PromptPointResult ppr2 = default(PromptPointResult);
    ppr2 = ed.GetPoint(
            new PromptPointOptions("Select end point"));
    if (ppr2.Status != PromptStatus.OK)
        return;

    Database db = HostApplicationServices.WorkingDatabase;

    ObjectId myMLeaderId = ObjectId.Null;
    using (Transaction trans 
                = db.TransactionManager.StartTransaction())
    {
        ObjectId myleaderStyleId = db.MLeaderstyle;
        MLeaderStyle mlstyle 
            = trans.GetObject(myleaderStyleId, 
                        OpenMode.ForRead) as MLeaderStyle;

        using (MLeader myMLeader = new MLeader())
        {
            myMLeader.SetDatabaseDefaults();
            myMLeader.PostMLeaderToDb(db);
            myMLeaderId = myMLeader.ObjectId;
            myMLeader.MLeaderStyle = db.MLeaderstyle;

            int leaderIndex = myMLeader.AddLeader();
            int leaderLineIndex 
                = myMLeader.AddLeaderLine(leaderIndex);
            myMLeader.AddFirstVertex(
                        leaderLineIndex, ppr1.Value);
            myMLeader.AddLastVertex(
                        leaderLineIndex, ppr2.Value);

            MText myMText 
                = mlstyle.DefaultMText.Clone() as MText;
            if (myMText != null)
            {
                myMText.SetContentsRtf("Autodesk");
                myMLeader.MText = myMText;
            }
        }
        trans.Commit();
    }
}
</pre>
