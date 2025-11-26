---
layout: "post"
title: ".NET SDK を利用した ACC へのファイル アップロード"
date: "2024-11-06 00:18:59"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/10/uploading-a-file-to-acc-using-the-net-sdk.html "
typepad_basename: "uploading-a-file-to-acc-using-the-net-sdk"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c27f8d200c-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c27f8d200c image-full img-responsive" src="/assets/image_876521.jpg" title="Aps" /></a></p>
<h4 dir="auto" tabindex="-1">はじめに</h4>
<p dir="auto">このブログ記事では、APS .NET SDK を使用して ACC にファイルをアップロードする手順について説明します。</p>
<p dir="auto">利用可能なライブラリは、<a  _istranslated="1" href="https://www.nuget.org/profiles/AutodeskPlatformServices.SDK" rel="noopener" target="_blank">NuGet ギャラリー |AutodeskPlatformServices.SDK</a></p>
<p dir="auto">このブログでは、SDK Manager、Authentication、OSS、および Data Management を使用します。<em>&#0160;</em></p>
<h4 dir="auto">仕組み</h4>
<p dir="auto"><a href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/" rel="nofollow noopener" target="_blank">Upload a File</a><a  _istranslated="1" href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/" rel="nofollow">&#0160;</a> チュートリアルで説明されている手順を実行する必要があります。</p>
<p dir="auto">このサンプルでは、Step 1 と 2 が <a  _istranslated="1" href="https://tutorials.autodesk.io/tutorials/hubs-browser/" rel="nofollow noopener" target="_blank">Hubs Browser</a> チュートリアルでカバーされているため、3 から 8 の Step に焦点を当てます。</p>
<p dir="auto">最初に対処する必要があるのは、SDK Manager、クライアント、およびアクセス トークン生成の構成です。</p>
<pre><code class=" hljs cs"><span class="hljs-keyword">string</span> client_id = Environment.GetEnvironmentVariable(<span class="hljs-string">&quot;client_id&quot;</span>);
<span class="hljs-keyword">string</span> client_secret = Environment.GetEnvironmentVariable(<span class="hljs-string">&quot;client_secret&quot;</span>);
SDKManager sdkManager = SdkManagerBuilder
  .Create() <span class="hljs-comment">// Creates SDK Manager Builder itself.</span>
  .Build();
DataManagementClient _dmClient = <span class="hljs-keyword">new</span> DataManagementClient(sdkManager);
AuthenticationClient _authClient = <span class="hljs-keyword">new</span> AuthenticationClient(sdkManager);
OssClient _ossClient = <span class="hljs-keyword">new</span> OssClient(sdkManager);
TwoLeggedToken twoLeggedToken = _authClient.GetTwoLeggedTokenAsync(client_id, client_secret, <span class="hljs-keyword">new</span> List&lt;Scopes&gt;() { Scopes.DataRead, Scopes.DataWrite, Scopes.DataCreate }).GetAwaiter().GetResult();</code></pre>
<p dir="auto">次に、以下の Step 3 から開始できます。</p>
<p dir="auto">3. <a href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/#step-3-create-a-storage-location" rel="nofollow noopener" target="_blank">Create a storage location</a> の手順では、<code>DataManagementClient</code> を通じて&#0160;<a  _istranslated="1"  _mstmutation="1" href="https://www.nuget.org/packages/Autodesk.DataManagement/2.0.0-beta3" rel="nofollow noopener" target="_blank">Data Management</a> ライブラリを使用する必要があります。</p>
<pre><code class=" hljs cs"><span class="hljs-keyword">private</span> <span class="hljs-keyword">static</span> Storage <span class="hljs-title">CreateStorage</span>(DataManagementClient _dmClient, TwoLeggedToken twoLeggedToken, <span class="hljs-keyword">string</span> project_id, <span class="hljs-keyword">string</span> file_name, <span class="hljs-keyword">string</span> folder_id)
{
  StoragePayload payload = <span class="hljs-keyword">new</span> StoragePayload()
  {
    Jsonapi = <span class="hljs-keyword">new</span> ModifyFolderPayloadJsonapi()
    {
      _Version = VersionNumber._10
    },
    Data = <span class="hljs-keyword">new</span> StoragePayloadData()
    {
      Type = Autodesk.DataManagement.Model.Type.Objects,
      Attributes = <span class="hljs-keyword">new</span> StoragePayloadDataAttributes()
      {
        Name = file_name,
      },
      Relationships = <span class="hljs-keyword">new</span> StoragePayloadDataRelationships()
      {
        Target = <span class="hljs-keyword">new</span> ModifyFolderPayloadDataRelationshipsParent()
        {
          Data = <span class="hljs-keyword">new</span> ModifyFolderPayloadDataRelationshipsParentData()
          {
            Type = Autodesk.DataManagement.Model.Type.Folders,
            Id = folder_id,
          }
        }
      }
    }
  };
  Storage storage = _dmClient.CreateStorageAsync(project_id, storagePayload: payload, accessToken: twoLeggedToken.AccessToken).GetAwaiter().GetResult();
  <span class="hljs-keyword">return</span> storage;
}</code></pre>
<p dir="auto">次の 3 つの Step は、SDK に用意された 1つのメソッドでまとめて処理されます。</p>
<p dir="auto">4. <a  _mstmutation="1" href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/#step-4-generate-a-signed-s3-url" rel="nofollow noopener" target="_blank">Generate a signed S3 url</a>&#0160;</p>
<p dir="auto">5. <a  _mstmutation="1" href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/#step-5-upload-a-file-to-the-signed-url" rel="nofollow noopener" target="_blank">Upload a file to the signed url</a>&#0160;</p>
<p dir="auto">6. <a  _mstmutation="1" href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/#step-6-complete-the-upload" rel="nofollow noopener" target="_blank">Complete the upload</a>&#0160;</p>
<p>これらの Step では、<code>OssClient</code> を通じて&#0160;<a href="https://www.nuget.org/packages/Autodesk.Oss/1.1.1">OSS</a> ライブラリを使用する必要があります。</p>
<p dir="auto"><code></code></p>
<pre><code class=" hljs cs"><span class="hljs-keyword">private</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">void</span> <span class="hljs-title">ReadAndUploadFile</span>(OssClient _ossClient, TwoLeggedToken twoLeggedToken, <span class="hljs-keyword">string</span> file_path, <span class="hljs-keyword">string</span> bucket_key, <span class="hljs-keyword">string</span> object_key)
{
  <span class="hljs-keyword">using</span> (FileStream fileStream = <span class="hljs-keyword">new</span> FileStream(file_path, FileMode.Open, FileAccess.Read))
  {
    _ossClient.Upload(bucket_key, object_key, fileStream, accessToken: twoLeggedToken.AccessToken, CancellationToken.None).GetAwaiter().GetResult();
  }
}</code></pre>
<p dir="auto">次に、新しいアイテム（v1）を作成するか、既存のアイテムに新しいバージョンを追加するかです。</p>
<p dir="auto">このサンプルでは、基本的に最初のオプションを実装しています。同じ名前の Item がすでに存在する場合は、409 ステータスの例外エラーがスローされるので、2つ目のオプションに移ることが出来ます。</p>
<blockquote>
<p><code>3-legged トークンを使用する場合、 </code><a href="「https://aps.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-folder_id-search-GET/」" rel="noopener" target="_blank">Search エンドポイント</a><code> 利用することができます。</code></p>
</blockquote>
<p>7. <a  _mstmutation="1" href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/#step-7-create-the-first-version-of-the-uploaded-file" rel="nofollow noopener" target="_blank">Create the first version of the uploaded file</a> の Step では、<code>DataManagementClient</code> を通じて <a href="https://www.nuget.org/packages/Autodesk.DataManagement/2.0.0-beta3" rel="noopener" target="_blank">Data Management</a> ライブラリを使用する必要があります。</p>
<pre><code class=" hljs cs"><span class="hljs-keyword">private</span> <span class="hljs-keyword">static</span> Item <span class="hljs-title">CreateNewItem</span>(DataManagementClient _dmClient, TwoLeggedToken twoLeggedToken, <span class="hljs-keyword">string</span> project_id, <span class="hljs-keyword">string</span> file_name, <span class="hljs-keyword">string</span> folder_id, Storage storage)
{
  ItemPayload itemPayload = <span class="hljs-keyword">new</span> ItemPayload()
  {
    Jsonapi = <span class="hljs-keyword">new</span> ModifyFolderPayloadJsonapi()
    {
      _Version = VersionNumber._10
    },
    Data = <span class="hljs-keyword">new</span> ItemPayloadData()
    {
      Type = Autodesk.DataManagement.Model.Type.Items,
      Attributes = <span class="hljs-keyword">new</span> ItemPayloadDataAttributes()
      {
        DisplayName = file_name,
        Extension = <span class="hljs-keyword">new</span> ItemPayloadDataAttributesExtension()
        {
          Type = Autodesk.DataManagement.Model.Type.ItemsautodeskBim360File,
          _Version = VersionNumber._10
        }
      },
      Relationships = <span class="hljs-keyword">new</span> ItemPayloadDataRelationships()
      {
        Tip = <span class="hljs-keyword">new</span> FolderPayloadDataRelationshipsParent()
        {
          Data = <span class="hljs-keyword">new</span> FolderPayloadDataRelationshipsParentData()
          {
            Type = Autodesk.DataManagement.Model.Type.Versions,
            Id = <span class="hljs-string">&quot;1&quot;</span>
          }
        },
        Parent = <span class="hljs-keyword">new</span> FolderPayloadDataRelationshipsParent()
        {
          Data = <span class="hljs-keyword">new</span> FolderPayloadDataRelationshipsParentData()
          {
            Type = Autodesk.DataManagement.Model.Type.Folders,
            Id = folder_id
          }
        }
      }
    },
    Included = <span class="hljs-keyword">new</span> List&lt;ItemPayloadIncluded&gt;()
    {
      <span class="hljs-keyword">new</span> ItemPayloadIncluded()
      {
        Type = Autodesk.DataManagement.Model.Type.Versions,
        Id = <span class="hljs-string">&quot;1&quot;</span>,
        Attributes = <span class="hljs-keyword">new</span> ItemPayloadIncludedAttributes()
        {
          Name = file_name,
          Extension = <span class="hljs-keyword">new</span> ItemPayloadDataAttributesExtension()
          {
            Type = Autodesk.DataManagement.Model.Type.VersionsautodeskBim360File,
            _Version = VersionNumber._10
          }
        },
        Relationships = <span class="hljs-keyword">new</span> ItemPayloadIncludedRelationships()
        {
          Storage = <span class="hljs-keyword">new</span> FolderPayloadDataRelationshipsParent()
          {
            Data = <span class="hljs-keyword">new</span> FolderPayloadDataRelationshipsParentData()
            {
              Type = Autodesk.DataManagement.Model.Type.Objects,
              Id = storage.Data.Id,
            }
          }
        }
      }
    }
  };
  Item newItem = _dmClient.CreateItemAsync(project_id, itemPayload: itemPayload, accessToken: twoLeggedToken.AccessToken).GetAwaiter().GetResult();
  <span class="hljs-keyword">return</span> newItem;
}</code></pre>
<p>また、新しいバージョンを追加する必要がある場合は、Item Id を見つける必要があります。</p>
<p>これは、次の方法でおこなわれます。</p>
<pre><code class=" hljs cs"><span class="hljs-keyword">private</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">string</span> <span class="hljs-title">GetItemId</span>(DataManagementClient _dmClient, TwoLeggedToken twoLeggedToken, <span class="hljs-keyword">string</span> project_id, <span class="hljs-keyword">string</span> folder_id, <span class="hljs-keyword">string</span> file_name)
{
  List&lt;<span class="hljs-keyword">string</span>&gt; filterExtensionType = <span class="hljs-keyword">new</span> List&lt;<span class="hljs-keyword">string</span>&gt;() { <span class="hljs-string">&quot;items:autodesk.bim360:File&quot;</span> };
  FolderContents folderContents = _dmClient.GetFolderContentsAsync(project_id, folder_id, accessToken:twoLeggedToken.AccessToken, filterExtensionType: filterExtensionType).GetAwaiter().GetResult();
  List&lt;FolderContentsData&gt; matchingItems = folderContents.Data.Where(d =&gt; d.Attributes.DisplayName == file_name).ToList();
  <span class="hljs-keyword">int</span> pageNumber = <span class="hljs-number">0</span>;
  <span class="hljs-keyword">while</span> (matchingItems.Count &gt; <span class="hljs-number">0</span> &amp; !<span class="hljs-keyword">string</span>.IsNullOrEmpty(folderContents.Links.Next?.Href)) {
    pageNumber++;
    folderContents = _dmClient.GetFolderContentsAsync(project_id, folder_id, accessToken: twoLeggedToken.AccessToken, filterExtensionType: filterExtensionType, pageNumber:pageNumber).GetAwaiter().GetResult();
    matchingItems = folderContents.Data.Where(d =&gt; d.Attributes.DisplayName == file_name).ToList();
  }
  <span class="hljs-keyword">return</span> matchingItems.First().Id;
}</code></pre>
<p>Item Idを使用して、新しいバージョンの作成に進むことができます。</p>
<p>8. <a href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/#step-8-update-the-version-of-a-file" rel="nofollow noopener" target="_blank">Update the version of a file</a> の手順では、<a  _istranslated="1"  _mstmutation="1" href="https://www.nuget.org/packages/Autodesk.DataManagement/2.0.0-beta3" rel="nofollow noopener" target="_blank">Data Management</a> ライブラリを使用する必要があります。<code>_dmClient</code></p>
<pre><code class=" hljs cs"><span class="hljs-keyword">private</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">void</span> <span class="hljs-title">CreateNewVersion</span>(DataManagementClient _dmClient, TwoLeggedToken twoLeggedToken, <span class="hljs-keyword">string</span> project_id, <span class="hljs-keyword">string</span> file_name, Storage storage, <span class="hljs-keyword">string</span> item_id)
{
  VersionPayload versionPayload = <span class="hljs-keyword">new</span> VersionPayload()
  {
    Jsonapi = <span class="hljs-keyword">new</span> ModifyFolderPayloadJsonapi()
    {
      _Version = VersionNumber._10
    },
    Data = <span class="hljs-keyword">new</span> VersionPayloadData()
    {
      Type = Autodesk.DataManagement.Model.Type.Versions,
      Attributes = <span class="hljs-keyword">new</span> VersionPayloadDataAttributes()
      {
        Name = file_name,
        Extension = <span class="hljs-keyword">new</span> RelationshipRefsPayloadDataMetaExtension()
  		{
  		  Type = Autodesk.DataManagement.Model.Type.VersionsautodeskBim360File,
  		  _Version = VersionNumber._10
  		}
      },
      Relationships = <span class="hljs-keyword">new</span> VersionPayloadDataRelationships()
      {
        Item = <span class="hljs-keyword">new</span> FolderPayloadDataRelationshipsParent()
        {
  		  Data = <span class="hljs-keyword">new</span> FolderPayloadDataRelationshipsParentData()
  		  {
  		    Type = Autodesk.DataManagement.Model.Type.Items,
  			Id = item_id
  		  }
  		},
  		Storage = <span class="hljs-keyword">new</span> FolderPayloadDataRelationshipsParent()
  		{
  		  Data = <span class="hljs-keyword">new</span> FolderPayloadDataRelationshipsParentData()
  		  {
  		    Type = Autodesk.DataManagement.Model.Type.Objects,
  			Id = storage.Data.Id,
  		  }
  		}
      }
    }
  };
  Console.WriteLine(versionPayload.ToString()); 
  ModelVersion newVersion = _dmClient.CreateVersionAsync(project_id, versionPayload: versionPayload, accessToken: twoLeggedToken.AccessToken).GetAwaiter().GetResult();
}</code></pre>
<p>完全なソースコードは次の GitHub リポジトリを参照してください。</p>
<p><a href="https://github.com/JoaoMartins-callmeJohn/aps-acc-upload-dotnet-sdk" rel="noopener" target="_blank">ソース</a></p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/uploading-file-acc-using-net-sdk" rel="noopener" target="_blank">Uploading a file to ACC using the .NET SDK | Autodesk Platform Services</a>&#0160;から転写・意訳、補足を加えたものです。</p>
<p>By Toshiaki Isezaki</p>
