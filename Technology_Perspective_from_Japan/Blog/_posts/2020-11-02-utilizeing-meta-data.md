---
layout: "post"
title: "Model Derivative API：メタデータの活用"
date: "2020-11-02 00:01:34"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/11/utilizeing-meta-data.html "
typepad_basename: "utilizeing-meta-data"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9717d9f200b-pi" style="display: inline;"></a>Forge Viewer で 2D 図面や 3D モデルを表示する際には、Model Derivative API を使って SVF ファイルに変換して、Base64 エンコードされた URN を Viewer コードに渡す必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be41db7e6200d-pi" style="display: inline;"><img alt="Viewer_process" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be41db7e6200d image-full img-responsive" src="/assets/image_5159.jpg" title="Viewer_process" /></a></p>
<p>Model Derivative API での SVF 変換時には、viewable と呼ばれる SVF 情報の他に、derivative と呼ばれる配信可能な派生物が存在します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9717c2e200b-pi" style="display: inline;"><img alt="Model_derivative_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9717c2e200b image-full img-responsive" src="/assets/image_363192.jpg" title="Model_derivative_api" /></a></p>
<p>この中には、シードファイル（元のデザインファイル）に定義されていたビュー（2D と 3D）の情報が存在します。ビューは、そのビューで表示されるジオメトリのメタデータを取得するための窓口となる情報、<strong>ID</strong>&#0160;が含まれます。このビュー毎の GUID は、<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-metadata-GET/" rel="noopener" target="_blank"><strong>GET :urn/metadata</strong></a> endpoint で取得することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde9ed348200c-pi" style="display: inline;"><img alt="Rest1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde9ed348200c image-full img-responsive" src="/assets/image_18739.jpg" title="Rest1" /></a></p>
<p>この <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-metadata-GET/" rel="noopener" target="_blank"><strong>GET :urn/metadata</strong></a> endpoint を利用すると、デザインファイルから派生したビューの ID （<a href="https://ja.wikipedia.org/wiki/GUID" rel="noopener" target="_blank"><strong>GUID</strong></a>）の一覧を取得することが出来ます。次の例では、3D ビューの GUID が 85d0b3b7-9a90-2738-40f7-7823397e2777 であることがわかります。これと同じ ID は、変換時に生成されるマニフェストにも現れているはずです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">{
    &quot;data&quot;: {
        &quot;type&quot;: &quot;metadata&quot;,
        &quot;metadata&quot;: [
            {
                &quot;name&quot;: &quot;3D&quot;,
                &quot;role&quot;: &quot;3d&quot;,
<span style="color: #0000ff;"><strong>                &quot;guid&quot;: &quot;85d0b3b7-9a90-2738-40f7-7823397e2777&quot;</strong></span>
            },
            {
                &quot;name&quot;: &quot;A101 - Lobby&quot;,
                &quot;role&quot;: &quot;2d&quot;,
                &quot;guid&quot;: &quot;d14f28b6-4ef8-770c-f6c5-8b419eeb9a0e&quot;
            },
            {
                &quot;name&quot;: &quot;A102 - 2th Flr. Occupancy&quot;,
                &quot;role&quot;: &quot;2d&quot;,
                &quot;guid&quot;: &quot;16ff855f-a7ab-ce77-b830-2b9979704280&quot;
            },
            {
                &quot;name&quot;: &quot;A103 - 3th Flr. Occupancy&quot;,
                &quot;role&quot;: &quot;2d&quot;,
                &quot;guid&quot;: &quot;91f9b12b-ef58-96b0-9b52-f7d097cad709&quot;
            },
            {
                &quot;name&quot;: &quot;A104 - 4th Flr. Occupancy&quot;,
                &quot;role&quot;: &quot;2d&quot;,
                &quot;guid&quot;: &quot;10b80f72-973f-16a4-2814-4dd90e33eafd&quot;
            },
            {
                &quot;name&quot;: &quot;A105 - 5th Flr. Occupancy&quot;,
                &quot;role&quot;: &quot;2d&quot;,
                &quot;guid&quot;: &quot;0ead8070-e539-0fb0-02e7-6f882c9df904&quot;
            },
            {
                &quot;name&quot;: &quot;A100 - Aerial&quot;,
                &quot;role&quot;: &quot;2d&quot;,
                &quot;guid&quot;: &quot;1d4c9e63-6336-4ff8-0b2a-28bb07e39086&quot;
            },
            {
                &quot;name&quot;: &quot;A106 - Perpective_King St.&quot;,
                &quot;role&quot;: &quot;2d&quot;,
                &quot;guid&quot;: &quot;9e9e2f77-54bc-b92b-92e9-bbe0ddfcd505&quot;
            },
            {
                &quot;name&quot;: &quot;A107 - Perspective_5th Flr&quot;,
                &quot;role&quot;: &quot;2d&quot;,
                &quot;guid&quot;: &quot;3f41d1cd-41d9-45e4-e354-9ecc0025f21c&quot;
            },
            {
                &quot;name&quot;: &quot;A108 - Elevation- King St.&quot;,
                &quot;role&quot;: &quot;2d&quot;,
                &quot;guid&quot;: &quot;f02362af-5835-e0ce-5411-d490d16c34a1&quot;
            },
            {
                &quot;name&quot;: &quot;A109 - Elevation-Frederick St.&quot;,
                &quot;role&quot;: &quot;2d&quot;,
                &quot;guid&quot;: &quot;c8a7d7e0-9ea3-fda4-d129-46038a340932&quot;
            }
        ]
    }
}
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>ビューの GUID が取得出来たら、この GUID を使ってビューに表示されるツリー構造を取得することが可能になります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9717d20200b-pi" style="display: inline;"><img alt="Rest2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9717d20200b image-full img-responsive" src="/assets/image_529579.jpg" title="Rest2" /></a></p>
<p>例）</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">{
    &quot;data&quot;: {
        &quot;type&quot;: &quot;objects&quot;,
        &quot;objects&quot;: [
            {
                &quot;objectid&quot;: 1,
                &quot;name&quot;: &quot;Model&quot;,
                &quot;objects&quot;: [
                    {
                        &quot;objectid&quot;: 2,
                        &quot;name&quot;: &quot;All-Ground Level&quot;
                    },
                    {
                        &quot;objectid&quot;: 3,
                        &quot;name&quot;: &quot;BLDG 1,2,3- LEVEL 1 FLR. FIN.&quot;
                    },
                    {
                        &quot;objectid&quot;: 4,
                        &quot;name&quot;: &quot;BLDG 1,2,3- LEVEL 2 FLR. FIN.&quot;
                    },
                    {
                        &quot;objectid&quot;: 5,
                        &quot;name&quot;: &quot;BLDG. 1,2,3- LEVEL 3 FLR. FIN.&quot;
                    },
                    {
                        &quot;objectid&quot;: 6,
                        &quot;name&quot;: &quot;BLDG. 1,2,3- LEVEL 4 FLR. FIN.&quot;
                    },
                                     &lt;省略&gt;
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>ここで返されるツリー構造は、Viewer 上のモデルツリーの内容と同等です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be41dc6b4200d-pi" style="display: inline;"><img alt="Model_tree" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be41dc6b4200d image-full img-responsive" src="/assets/image_254925.jpg" title="Model_tree" /></a></p>
<p>そして、この GUID を使用して <strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-metadata-guid-properties-GET/" rel="noopener" target="_blank">GET :urn/metadata/:guid/properties</a></strong> endpoint を呼び出すと、含まれるプロパティをメタデータとして得ることが出来ます。JSON 形式でビュー配下のジオメトリのプロパティを返すので、かなりのサイズになってしまいますが、Forge Viewer JavaScript ライブラリで扱うことになりオブジェクト識別子（dbid）をキーに、プロパティをパースすることも可能です。</p>
<p style="padding-left: 40px;">&#0160; &#0160;<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be41dce6d200d-pi" style="display: inline;"><img alt="Ret3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be41dce6d200d image-full img-responsive" src="/assets/image_241365.jpg" title="Ret3" /></a></p>
<p>特に、ここで返される JSON には、元のデザインファイルで定義された（不変な扱いの）オブジェクト識別子を <strong>externalId</strong>&#0160; として取得することが出来るので、必要に応じて、後続の処理で活用することが出来ます。不変な扱いのオブジェクト識別子とは、AutoCAD API で扱うハンドル番号や Revit API で扱う Unique Id のような永続的な識別子を指します。</p>
<p>次の図は、窓のファミリ インスタンスの Unique Id が、externalId として発現している例を、Revit 上の Lookup Tool と VS Code の <strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/06/forge-development-using-vs-code.html" rel="noopener" target="_blank">Autodesk Forge Tools エクステンション</a></strong>でそれぞれ表示した例です。dbId（objectId）は SVF 変換毎に異なる値になってしまいますが、externalId はファミリタイプの変更前後でも同じ値になっている点にご着目ください。外部データベースを併用する場合には有効です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9717d9f200b-pi" style="display: inline;"><img alt="Externalid" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9717d9f200b image-full img-responsive" src="/assets/image_648979.jpg" title="Externalid" /></a></p>
<p>なお、 <strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-metadata-guid-properties-GET/" rel="noopener" target="_blank">GET :urn/metadata/:guid/properties</a></strong> endpoint でのプロパティ JSON は、生成に少し時間が必要です。SVF 変換直後には、202 エラーでこの状態を返す場合があります。</p>
<p>By Toshiaki Isezaki</p>
