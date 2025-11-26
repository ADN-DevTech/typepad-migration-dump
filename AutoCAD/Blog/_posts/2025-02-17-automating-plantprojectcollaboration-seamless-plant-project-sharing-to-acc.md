---
layout: "post"
title: "Automating PLANTPROJECTCOLLABORATION: Seamless Plant Project Sharing to ACC"
date: "2025-02-17 20:06:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2025/02/automating-plantprojectcollaboration-seamless-plant-project-sharing-to-acc.html "
typepad_basename: "automating-plantprojectcollaboration-seamless-plant-project-sharing-to-acc"
typepad_status: "Publish"
---

<p>     <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>   </p>   <p>     AutoCAD Plant 3D provides a command to share a plant project to the ACC cloud:     <a href="https://help.autodesk.com/view/PLNT3D/2025/ENU/?guid=GUID-BAF5CC29-AF97-474C-90A4-05DC1B18CF52">       PLANTPROJECTCOLLABORATION     </a>.   </p>   <p>     For more details, refer to     <a href="https://help.autodesk.com/view/PLNT3D/2025/ENU/?guid=GUID-9DDB64C5-DB59-4E78-9105-2B647BABC8A6">       To Share a Plant Project to the Cloud     </a>.   </p>   <p>     However, this command poses a challenge for automation, as it requires user interaction.     To overcome this limitation, we can use the Plant 3D API to automate the process of sharing     the plant project to the cloud.   </p>   <p>     The code snippet below demonstrates how to upload a Plant3D project to Collaboration for Plant3D asynchronously.     The code emits a CLI progress indicator.     It takes the hub name, project ID, and folder ID as input parameters.   </p>   <pre class="prettyprint">
  <code>
  /// <summary>
  /// Uploads a Plant3D project to Collaboration for Plant3D asynchronously
  /// Emits a CLI progress indicator
  /// </summary>
  /// <param name="hubName"></param>
  /// <param name="projectId"></param>
  /// <param name="folderId"></param>
  /// <returns></returns>

  public static async Task UploadProjectAsync(
  string hubName, string projectId, string folderId)
  {
    using CancellationTokenSource cts = new();

    try
    {
        // TODO: Add support for Vault and SQL Server projects
        PlantProject plantPrj = PlantApplication.CurrentProject;
        string projPath = plantPrj.ProjectFolderPath;

        // Server login
        DocLogIn login = Commands.ServiceLogIn(Commands.CloudServiceName);
        if (login == null)
        {
            Commands.PlantCloudLogin();
            login = Commands.ServiceLogIn(Commands.CloudServiceName);
            if (login == null) return;
        }

        // Select Docs Hub and Project
        DocA360Project docProject = await Task.Run(() =>
        {
            var hubs = login.SelectA360Hubs(null, cts.Token);
            var hub = hubs?.FirstOrDefault(x => x.A360HubId == hubName);
            var projects = hub != null
                ? login.SelectA360ProjectsFromHub(hub, cts.Token)
                : null;
            var proj = projects?.FirstOrDefault(x => x.A360ProjectId == projectId);
            if (proj != null && proj.RootFolderUrn == folderId) return null;

            return login.SelectA360ProjectSubFolders(proj, cts.Token)
                ?.FirstOrDefault(x => x.RootFolderUrn == folderId) ?? proj;
        }, cts.Token);

        if (docProject == null) return;

        // Copy project to CollaborationCache
        string destPath = Path.Combine(
            Commands.P360WorkingFolder.Trim(), plantPrj.Name);
        Utils.BackupProjectFiles(
            new DirectoryInfo(projPath), new DirectoryInfo(destPath));

        // Convert SQL Server project to SQLite if necessary
        var pid = plantPrj.ProjectParts["PnId"];
        if (pid?.DataLinksManager.GetPnPDatabase().DBEngine.ToString()
            != PnPDatabase.SQLiteEngineClass)
        {
            ConvertSQLServerProjectToSQLiteProject(
                destPath, plantPrj.Username, plantPrj.Password, cts.Token);
        }

        plantPrj.Close();

        // Load new project
        PlantProject prj = PlantProject.LoadProject(
            Path.Combine(destPath, "Project.xml"), true, null, null);

        // Ensure project structure and collect XRefs
        EnsureProjectFoldersExist(prj);
        var assoc = CollectXrefs(prj);

        // Share project
        PnPDocumentServerFactory factory =
            PnPDocumentServerFactoryRegistry.GetFactory(
                Commands.CloudServiceName);
        DocumentServer docsrv = factory.CreateInstance(Guid.NewGuid().ToString());
        docsrv.SignIn(login, null);

        // Start progress indicator
        Task progressTask = ShowProgressAsync(cts.Token);

        try
        {
            PrintMsg("Starting EnableDocumentManagement...");

            await Task.Run(() =>
            {
                prj.EnableDocumentManagement(
                    docsrv, string.Empty, string.Empty, docProject, assoc, cts.Token);
                cts.Token.ThrowIfCancellationRequested();
            }, cts.Token);

            PrintMsg("EnableDocumentManagement completed successfully.");
        }
        catch (OperationCanceledException ex)
        {
            PrintMsg($"Upload canceled: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            PrintExceptionDetails(ex);
            throw;
        }
        finally
        {
            cts.Cancel(); // Stop progress
            await progressTask;
            prj.Close();
        }
    }
    catch (OperationCanceledException)
    {
        PrintMsg("Upload operation was canceled.");
        throw;
    }
    catch (Exception ex)
    {
        PrintExceptionDetails(ex);
        throw;
    }
}

  </code>
</pre>
  <p>Full source code and a demo working sample Run is provided in Github Project</p>
  <a href="https://github.com/MadhukarMoogala/CLIPLANT360SHARE/blob/main/Program.cs" target="_blank" rel="noopener" class="btn btn-dark d-inline-flex align-items-center">
    <img src="/assets/GitHub-Mark.png" alt="GitHub" style="width: 20px; height: 20px; margin-right: 8px;">
    View Program.cs on GitHub
  </a>
