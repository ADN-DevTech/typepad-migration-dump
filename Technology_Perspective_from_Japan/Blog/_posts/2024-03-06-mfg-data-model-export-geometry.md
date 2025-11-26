---
layout: "post"
title: "Manufacturing Data Model：ジオメトリ書き出し"
date: "2024-03-06 00:09:09"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/03/mfg-data-model-export-geometry.html "
typepad_basename: "mfg-data-model-export-geometry"
typepad_status: "Publish"
---

<p><a href="https://aps.autodesk.com/en/docs/mfgdatamodel-publicbeta/v2/tutorials/exportgeo/" rel="noopener" target="_blank">Export Geometry | Manufacturing Data Model API (Beta) | Autodesk Platform Services</a>&#0160;に説明のあるとおり、Manufacturing Data Model で GraphQL を利用することで、Fusion 360 コンポーネントを STEP、OBJ、STL ファイル形式へのファイル書き出し出来るようになっています。</p>
<hr />
<p>例えば、<a href="https://adndevblog.typepad.com/technology_perspective/2024/02/mfg-data-modeld-use-of-v2-on-data-explorer.html" rel="noopener" target="_blank">Manufacturing Data Model：Data Explorer での v2 利用設定</a> した Data Explorer で Fusion 360 のサンプルである <strong>bike frame</strong> を STEP、OBJ、STL ファイルへ出力する場合、次のような手順を踏むことになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ab54b6200b-pi" style="display: inline;"><img alt="Fusion" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ab54b6200b image-full img-responsive" src="/assets/image_766872.jpg" title="Fusion" /></a></p>
<p>書き出すコンポーネント バージョンは、GraphQL オブジェクト同様、ID フィールドで指定します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3abbc2b200d-pi" style="display: inline;"><img alt="Voyager" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3abbc2b200d image-full img-responsive" src="/assets/image_503539.jpg" title="Voyager" /></a></p>
<p>コンポーネント バージョン ID は、MFG Data Explorer にプリセットされたタブ「GetHubs」➜「GetProjects」➜「GetFolders」➜「GetItems」 を使って取得することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3abbc4e200d-pi" style="display: inline;"><img alt="Tasks" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3abbc4e200d image-full img-responsive" src="/assets/image_390207.jpg" title="Tasks" /></a></p>
<p>Fusion 360 で Hub とコンポーネントが格納されている Project を確認したら、<a href="https://adndevblog.typepad.com/technology_perspective/2024/02/graphql-data-explorer.html">GraphQL Data Explorer</a> でご紹介した方法で、<strong>bike frame</strong> の Item を特定していきます。</p>
<p>「GetFolders」タブでは、Project 直下の Folder 情報（id、name 等）を返します。次のタスクで実行する「GetItems」タブでは、Item が格納されている Folder Id の指定が必要です。もし Item が Project 直下にある場合には、ルート フォルダの ID は返されないので、「GetFolders」の GraphQL クエリーに下記の<span style="color: #0000ff;"><strong>青字</strong></span>を追記しておきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3abbd29200d-pi" style="display: inline;"><img alt="Fusion_team" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3abbd29200d image-full img-responsive" src="/assets/image_85259.jpg" title="Fusion_team" /></a></p>
<div>
<blockquote>
<div># Task 3 – Pick Folder</div>
<div>query GetFolders($projectId:ID!) {</div>
<div>&#0160; nav {</div>
<div>&#0160; &#0160; project(projectId: $projectId) {</div>
<div>&#0160; &#0160; &#0160; folders {</div>
<div>&#0160; &#0160; &#0160; &#0160; pagination {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; cursor</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; results {</div>
<div><strong><span style="color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#0160; parentFolder{</span></strong></div>
<div><strong><span style="color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</span></strong></div>
<div><strong><span style="color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></strong></div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; }</div>
<div>}</div>
</blockquote>
</div>
<p>少し冗長ですが、このクエリーを実行すると、&quot;parentFolder&quot; スコープに Project 直下の Folder Id が返されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a782f1200c-pi" style="display: inline;"><img alt="Getrootfolderid" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a782f1200c image-full img-responsive" src="/assets/image_119126.jpg" title="Getrootfolderid" /></a></p>
<p>ここまでに取得した Project Id と Folder Id を Variables&#0160; に指定して「GetItems」の&#0160; GraphQL クエリーを実行すると、レスポンス中に <strong>bike frame</strong>&#0160;の tipRootComponentVersion id が得られます。この Id を使って、ファイル書き出しをおこないます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3abbcba200d-pi" style="display: inline;"><img alt="TipRootComponentVersion" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3abbcba200d image-full img-responsive" src="/assets/image_950760.jpg" title="TipRootComponentVersion" /></a></p>
<p>Data Explorer に新しいタブを用意したら、次の GraphQL クエリーを左側にクリップボード経由で貼り付けます。</p>
<div>
<blockquote>
<div>query GetComponentVersion ($componentVersionId: ID! ) {</div>
<div>&#0160; &#0160; mfg {</div>
<div>&#0160; &#0160; componentVersion (componentVersionId: $componentVersionId) {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; partNumber</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; isMilestone</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; materialName</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; lastModifiedOn</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; derivatives( derivativeInput: { generate: true, outputFormat:[STEP, STL, OBJ]}){</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;progress</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;status</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;signedUrl</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;outputFormat</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;expires</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160;}</div>
<div>&#0160; &#0160; }</div>
<div>&#0160;}</div>
</blockquote>
</div>
<p>左下の Vriables 欄には、componentVersionId 変数に先に「GetItems」で取得した tipRootComponentVersion id を指定します。</p>
<blockquote>
<div>{</div>
<div>&#0160; &#0160; &quot;componentVersionId&quot;: &quot;<em>&lt;tipRootComponentVersion id</em> &gt;&quot;</div>
<div>}</div>
</blockquote>
<p>この GraphQL を実行すると、STEP、OBJ、STL ファイルの作成を開始されます。書き出すコンポーネントの複雑さにもよりますが、書き出しは即座に終了しないので、レスポンスの progress と status の値が <strong>100</strong>（％）と<strong> success</strong> になるまで何回か実行を繰り返します。</p>
<p>progress と status の値が <strong>100</strong>&#0160;と<strong> success</strong> になると、ファイル毎に sigendurl に書き出されたファイルをダウンロードするための SignedURL（署名付き URL）が返されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3abbd10200d-pi" style="display: inline;"><img alt="Generate_file" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3abbd10200d image-full img-responsive" src="/assets/image_605929.jpg" title="Generate_file" /></a></p>
<p>この URL を Web ブラウザの URL 欄に入力して実行すると、書き出されたファイルをダウンロードして Inventor 等に取り込むなどして再利用することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a77dc6200c-pi" style="display: inline;"><img alt="Inventor" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a77dc6200c image-full img-responsive" src="/assets/image_858842.jpg" title="Inventor" /></a></p>
<p>By Toshiaki Isezaki</p>
