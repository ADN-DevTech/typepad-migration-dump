---
layout: "post"
title: "Model Derivative API での RVT ファイル変換について"
date: "2019-11-18 00:10:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/11/rvt-translation-enhancement-on-model-derivative-api.html "
typepad_basename: "rvt-translation-enhancement-on-model-derivative-api"
typepad_status: "Publish"
---

<p>Forge Viewer からの情報表示で長らくご要望のあった「部屋」、「スペース」、「ゾーン」といった情報を 3D ビュー上でプロパティとともに表現出来るようになっています。BIM 360 Docs 上では、少し前から可能でしたが、ようやく、Forge Viewer を使った 3rd party アプリケーションでも、同様の表現が可能になることになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49d67ae200c-pi" style="display: inline;"><img alt="Room_property" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a49d67ae200c image-full img-responsive" src="/assets/image_679285.jpg" title="Room_property" /></a></p>
<p>ただし、事前の準備が必要になる点にご注意ください。Model Derivative API の <strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST/" rel="noopener" target="_blank">POST job</a></strong> endpoint を用いて、Revit プロジェクト ファイル（.rvt）を SVF ファイルに変換する際、Request Body に <span style="color: #0000ff;"><strong>advanced</strong></span> オプションである <span style="color: #0000ff;"><strong>generateMasterViews</strong></span> を true に設定して変換をする必要があります。変換時には、同時に Request Header の <strong>x-ads-force</strong> オプションも true にしてください。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">{<br />   &quot;input&quot;: {<br />     &quot;urn&quot;: &quot;<em>&lt;Your Encoded URN&gt;</em>&quot;<br />   },<br />   &quot;output&quot;: {<br />     &quot;formats&quot;: [<br />       {<br />         &quot;type&quot;: &quot;svf&quot;,<br />         &quot;views&quot;: [<br />           &quot;2d&quot;,<br />           &quot;3d&quot;<br />         ],<br /><span style="color: #0000ff;"><strong>         &quot;advanced&quot;: {</strong></span><br /><span style="color: #0000ff;"><strong>            &quot;generateMasterViews&quot;: true</strong></span><br /><span style="color: #0000ff;"><strong>         }</strong></span><br />       }<br />     ]<br />   }<br /> }<br /></code></pre>
<p>generateMasterViews を指定しない状態、または、generateMasterViews を false にして変換された SVF には、フロアプランにラベル付けされた部屋が含まれても、3Dビューの一部ではないため、部屋はモデルに含まれません。逆に、generateMasterViews を true に指定すると、Revit モデルで作成したフェーズ毎に、「部屋」などの要素を含むマスタービュー（3D ビュー）が生成されるようになります。</p>
<p>マスタービューの表示名は、既定値として Revit モデルのフェーズ名になります。ただし、その名前のビューがすでに存在する場合、既定値の表示名に数字を追加します。例えば、Revit モデルで「新築」フェーズでビュー名に「新築」が存在する場合、「新築」フェーズのマスタービュー名は「新築_1」となります。</p>
<p>下記のそのように状態で生成されたマニフェストの抜粋です。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs ">                                             : 

               {
                    &quot;guid&quot;: &quot;e8a7e57c-85e4-ea7a-4ec6-7bc1a70b96f3&quot;,
                    &quot;type&quot;: &quot;geometry&quot;,
                    &quot;role&quot;: &quot;3d&quot;,
                    <strong>&quot;name&quot;: &quot;新築_1&quot;,</strong>
                    &quot;viewableID&quot;: &quot;c884ae1b-61e7-4f9d-0004-719e20b22d0b-0009b28f&quot;,
                    <strong>&quot;phaseNames&quot;: &quot;新築&quot;,</strong>
                    &quot;status&quot;: &quot;success&quot;,
                    &quot;hasThumbnail&quot;: &quot;true&quot;,
                    &quot;progress&quot;: &quot;complete&quot;,
                    &quot;children&quot;: [
                        {
                            &quot;guid&quot;: &quot;c884ae1b-61e7-4f9d-0004-719e20b22d0b-0009b28f&quot;,
                            &quot;type&quot;: &quot;view&quot;,
                            &quot;role&quot;: &quot;3d&quot;,
                            &quot;name&quot;: &quot;新築_1&quot;,
                            &quot;status&quot;: &quot;success&quot;,
                            &quot;progress&quot;: &quot;complete&quot;,
                            &quot;camera&quot;: [
                                206.648636,
                                -206.648636,
                                231.336945,
                                -0.000031,
                                0.000031,
                                24.688278,
                                -0.408248,
                                0.408248,
                                0.816497,
                                1.505229,
                                0,
                                1,
                                1
                            ]
                        },
                        {
                            &quot;guid&quot;: &quot;efaa682e-b04e-112f-1353-15bf6b9999c0&quot;,
                            &quot;type&quot;: &quot;resource&quot;,
                            &quot;role&quot;: &quot;graphics&quot;,
                            &quot;urn&quot;: <span style="color: #0000ff;"><strong>&quot;urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvMDQtMDQtMDIucnZ0/output/Resource/3D ビュー/08f99ae5-b8be-4f8d-881b-128675723c10/新築_1/新築_1.svf&quot;</strong></span>,
                            &quot;mime&quot;: &quot;application/autodesk-svf&quot;
                        },
                        {
                            &quot;guid&quot;: &quot;9c16cdd6-e583-2405-9fb0-6b1390d4576e&quot;,
                            &quot;type&quot;: &quot;resource&quot;,
                            &quot;role&quot;: &quot;thumbnail&quot;,
                            &quot;urn&quot;: &quot;urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvMDQtMDQtMDIucnZ0/output/Resource/3D ビュー/08f99ae5-b8be-4f8d-881b-128675723c10/新築_1/新築_11.png&quot;,
                            &quot;resolution&quot;: [
                                100,
                                100
                            ],
                            &quot;mime&quot;: &quot;image/png&quot;,
                            &quot;status&quot;: &quot;success&quot;
                        },
                        {
                            &quot;guid&quot;: &quot;de74b498-9137-337d-d636-a8ad3b324e75&quot;,
                            &quot;type&quot;: &quot;resource&quot;,
                            &quot;role&quot;: &quot;thumbnail&quot;,
                            &quot;urn&quot;: &quot;urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvMDQtMDQtMDIucnZ0/output/Resource/3D ビュー/08f99ae5-b8be-4f8d-881b-128675723c10/新築_1/新築_12.png&quot;,
                            &quot;resolution&quot;: [
                                200,
                                200
                            ],
                            &quot;mime&quot;: &quot;image/png&quot;,
                            &quot;status&quot;: &quot;success&quot;
                        },
                        {
                            &quot;guid&quot;: &quot;025a27a4-b2ca-1d14-9d23-6cbfa1cbbaaa&quot;,
                            &quot;type&quot;: &quot;resource&quot;,
                            &quot;role&quot;: &quot;thumbnail&quot;,
                            &quot;urn&quot;: &quot;urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvMDQtMDQtMDIucnZ0/output/Resource/3D ビュー/08f99ae5-b8be-4f8d-881b-128675723c10/新築_1/新築_14.png&quot;,
                            &quot;resolution&quot;: [
                                400,
                                400
                            ],
                            &quot;mime&quot;: &quot;image/png&quot;,
                            &quot;status&quot;: &quot;success&quot;
                        }
                    ]
                },
                                             : </code></pre>
<p>通常、上記青字の URN を Viewer3D.LoadModel() で指定することで、「部屋」要素等を含む マスタービュー（3D ビュー）を表示させることが出来ますが、日本語が含まれる場合、エラーとなってしまうようです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4eb5c8d200b-pi" style="display: inline;"><img alt="Loadmodel_error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4eb5c8d200b image-full img-responsive" src="/assets/image_47925.jpg" title="Loadmodel_error" /></a></p>
<p>Viewable は正しく生成されていますので、loadDocumentNode() の viewables 配列のインデックスを適切な値に変更することで、「部屋」要素等を含むフェーズ毎のマスタービューを表示させることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4eb9594200b-pi" style="display: inline;"><img alt="Developer_tools" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4eb9594200b image-full img-responsive" src="/assets/image_591025.jpg" title="Developer_tools" /></a></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">var _viewer = null;

function initializeViewer() {

    var options = {
        env: &#39;AutodeskProduction&#39;,
        api: &#39;derivativeV2&#39;,  // for models uploaded to EMEA change this option to &#39;derivativeV2_EU&#39;
        language: &#39;ja&#39;,
        getAccessToken: getCredentials
    };

    Autodesk.Viewing.Initializer(options, function () {

        _viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById(&#39;<strong>viewer3d</strong>&#39;));

        var startedCode = _viewer.start();
        if (startedCode &gt; 0) {
            console.error(&#39;Failed to create a Viewer: WebGL not supported.&#39;);
            return;
        } &#0160; &#0160; &#0160; &#0160; &#0160;console.log(&#39;Initialization complete, loading a model next...&#39;);

        var documentId = &#39;urn:&#39; + <strong>&lt;your base64 encoded urn&gt;</strong>;
        Autodesk.Viewing.Document.load(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);

    });

    function onDocumentLoadSuccess(viewerDocument) {

        var viewables = viewerDocument.getRoot().search({
            &#39;role&#39;: &#39;3d&#39;
        });

        _viewer.loadDocumentNode(viewerDocument, viewables[<span style="color: #0000ff; background-color: #ffff00; font-size: 13pt;"><strong>4</strong></span>]).then(i =&gt; {
        });

    }

    function onDocumentLoadFailure() {
        console.error(&#39;Failed fetching Forge manifest&#39;);
    }

}</code></pre>
<p>なお、「部屋」等の要素は、既定値で非表示になっていますので、モデル ブラウザで適宜、表示/非表示を切り替えてください。下図は、rac_basic_sample_project.rvt サンプル プロジェクトに定義された「Working Drawings」フェーズと「Learning Content」フェーズの内、「Working Drawings」フェーズのマスタービューから「部屋」全体と「Kitchen &amp; Dining 101」のみを表示切り替えする例です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49d6d02200c-pi" style="display: inline;"><img alt="Rooms_on_rac_basic_sample_project" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a49d6d02200c image-full img-responsive" src="/assets/image_454138.jpg" title="Rooms_on_rac_basic_sample_project" /></a></p>
<p>By Toshiaki Isezaki</p>
