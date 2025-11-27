---
layout: "post"
title: "Add search command in Vault API SelectEntity dialog"
date: "2018-05-23 06:34:29"
author: "Sajith Subramanian"
categories:
  - "Sajith Subramanian"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2018/05/add-search-command-in-vault-api-selectentity-dialog.html "
typepad_basename: "add-search-command-in-vault-api-selectentity-dialog"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p>
<p>You can add a search command for the SelectEntity dialog, using the <strong>DoSearch</strong> property in the <strong>SelectEntitySettings.SelectEntityOptionsExtensibility</strong> Class. If this property is not set then the search button is not shown at all.</p>
<p>Below is the SelectEntity dialog with the DoSearch property set. You can see the command button for the search functionality (Encircled top right).</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df34e265200b-pi"><img alt="image" border="0" height="517" src="/assets/image_f52422.jpg" style="display: inline; background-image: none;" title="image" width="746" /></a></p>
<p>&#0160;</p>
<p>Below is the code sample for reference. The easiest way to test the below code would be to add it in the VaultList sample that comes along with the Vault SDK.</p>
<pre>  try
    {
     VDF.Vault.Forms.Settings.SelectEntitySettings settings = new VDF.Vault.Forms.Settings.SelectEntitySettings();

     List&lt;string&gt; Entitylist = new List&lt;string&gt;();
     Entitylist.Add(&quot;FILE&quot;); // search for File entities.
     settings.OptionsExtensibility.DoSearch = (x,y,z) =&gt; searchFunc(Entitylist, false,z);
                                                                                                             VDF.Vault.Forms.Results.SelectEntityResults selresults = VDF.Vault.Forms.Library.SelectEntity(connection, settings);
     }
  catch (Exception ex)
     {
      MessageBox.Show(ex.ToString(), &quot;Error&quot;);                
     }
  VDF.Vault.Library.ConnectionManager.LogOut(connection);</pre>
<pre>&#0160; void searchFunc(IEnumerable&lt;string&gt; entityClassIds, bool multiSelect, Action&lt;IEnumerable&lt;VDF.Vault.Currency.Entities.IEntity&gt;&gt; resultFunc)<br />    {
         List&lt;VDF.Vault.Currency.Entities.IEntity&gt; searchResults = new List&lt;VDF.Vault.Currency.Entities.IEntity&gt;();
            FileIteration fileiter = GetFileIteration(entityClassIds.First(),                           &quot;Assembly1.iam&quot;);//Name of file to be searched. 
         searchResults.Add(fileiter);
         resultFunc(searchResults); 
     }</pre>
<pre> FileIteration GetFileIteration(string EntityClassID, string nameoffile)
        {
            PropDef[] filePropDefs = connection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId(EntityClassID);

           PropDef filenamePropDef = filePropDefs.Single(n =&gt; n.DispName == &quot;File Name&quot;);
         
            SrchCond condition = new SrchCond()
            {
                PropDefId = filenamePropDef.Id,
                PropTyp = PropertySearchType.SingleProperty,
                SrchOper = 3, // is not empty
                SrchRule = SearchRuleType.Must,
                SrchTxt = nameoffile
            };
            
            string bookmark = string.Empty;
            SrchStatus status = null;
           List&lt;File&gt; totalResults = new List&lt;File&gt;();
            while (status == null || totalResults.Count &lt; status.TotalHits)
            {
                File[] results = connection.WebServiceManager.DocumentService.FindFilesBySearchConditions(new SrchCond[] { condition },null, null, false, true, ref bookmark, out status);
                if (results != null)
                    totalResults.AddRange(results);
                else
                    break;
            }            
            return new FileIteration(connection ,totalResults.First());
        }</pre>
<p>When the search button is pressed, and searched file is found, the dialog then navigates to the found file, as seen below.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df34e269200b-pi"><img alt="image" border="0" height="523" src="/assets/image_cf01dd.jpg" style="display: inline; background-image: none;" title="image" width="750" /></a></p>
