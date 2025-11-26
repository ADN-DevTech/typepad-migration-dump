---
layout: "post"
title: "Forge Viewer での Fusion 360 アニメーション再生"
date: "2018-09-10 00:15:10"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/09/fusion-360-animation-play-on-forge-viewer.html "
typepad_basename: "fusion-360-animation-play-on-forge-viewer"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3b01452200b-pi" style="float: right;"><img alt="Thumbnail" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3b01452200b img-responsive" src="/assets/image_223674.jpg" style="margin: 0px 0px 5px 5px;" title="Thumbnail" /></a>8月2日 東京、8月24日 大阪で開催した&#0160;Fusion 360 ハンズオン＆ Forge meetup では、Fusion 360 のハンズインの他に、Fusion 360 で作成した 3D モデルに アニメーションを定義すれば、Forge Viewer で表示/再生出来ることをデモとともにご紹介しました。今回は、その方法について言及しておきたいと思います。</p>
<p>Forge では、 Fusion 360 などで作成したデザイン ファイルを変換することで Web ブラウザでストリーミング表示することが出来ます。示以前には、いくつかの RESTful API で準備をしなければなりません。具体的には、Authentication API（OAuth API）で Acccess Token を取得し、Data Management API でデザイン ファイルをアップロード、その後Model Derivative API でストリーミング用の SVF ファイルに変換します。SVF ファイルの変換完了後、クライアントとなる Web ブラウザ内で動作する Forge Viewer JavaScript API を利用して 3D モデルや 2D 図面を表示、操作することになります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad38ffafb200d-pi" style="display: inline;"><img alt="Viewer_steps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad38ffafb200d image-full img-responsive" src="/assets/image_895962.jpg" title="Viewer_steps" /></a><br />アニメーションを定義した Fusion 360 のアーカイブ ファイル（.f3d ファイル）も、この手順に沿うだけです。唯一、アニメーションを再生させる <strong>Autodesk.Fusion360.Animation</strong> Extension をロードする必要がある点が従来の 3D モデル表示と異なります。オートデスクが提供する Forge Viewer 用の&#0160; Extension については、過去のブログ記事&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/12/extensions-abailable-on-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer で利用可能な Extension</a></strong> を確認してみてください。</p>
<p>今回の meetup でご案内したデモでは、次のコードで 3D モデルを Viewer 領域（&#39;viewer3d 名を付加した &lt;div&gt;&lt;/div&gt; 領域）に表示しています。アニメーションは role を <span style="color: #0000ff;">&#39;animation&#39;</span> に設定することで viewable を取得することが出来ます（3D モデルだけの表示では <span style="color: #0000ff;">&#39;3D&#39;</span>、2D 図面の場合には <span style="color: #0000ff;">’2D’</span>）。</p>
<pre>    // Intialize Viewer
    var options = {};
    options.env = &quot;AutodeskProduction&quot;;
    options.accessToken = getToken();
    options.document = &quot;urn:&quot; + urn;
    options.language = &quot;ja&quot;;
                
    var viewerElement = document.getElementById(&#39;viewer3d&#39;);
    _viewer = new Autodesk.Viewing.Private.GuiViewer3D(viewerElement, {}); // With default UI

    Autodesk.Viewing.Initializer(options, function () {
        _viewer.initialize();
        _viewer.setTheme(&quot;light-theme&quot;);
        loadDocument(_viewer, options.document);
        _viewer.addEventListener(
            Autodesk.Viewing.GEOMETRY_LOADED_EVENT,
                function (event) {
                    loadExtensions(_viewer);
                });
        });

    }

    // Load Extension
    function loadExtensions(viewer) {
        <strong>viewer.loadExtension(&#39;Autodesk.Fusion360.Animation&#39;);</strong>
    }

    // Load viewable
    function loadDocument(viewer, documentId) {
        // Find the first 3d geometry and load that.
        Autodesk.Viewing.Document.load(documentId, function (doc) {// onLoadCallback
            var animationItems = [];
            if (animationItems.length == 0) {
                animationItems = Autodesk.Viewing.Document.getSubItemsWithProperties(doc.getRootItem(), {
                    &#39;type&#39;: &#39;folder&#39;,
                    &#39;role&#39;: <span style="color: #0000ff;">&#39;<strong>animation</strong>&#39;</span>
                }, true);
            }
            if (animationItems.length &gt; 0) {
                viewer.load(doc.getViewablePath(animationItems[0].children[0]));
            }
    }, function (errorMsg) {// onErrorCallback
        console.log(&quot;Load Error: &quot; + errorMsg);
    });

}&#0160;</pre>
<p><strong>Autodesk.Fusion360.Animation</strong> Extension をロードしたタイミングで Viewwe 内の標準ツールバーに「再生」ボタンが表示されます。アニメーション再生とは関係なく、通常の 3D モデル表示時と同じく、オブジェクトを選択してプロパティ（属性、メタデータ）を表示させたり、計測や断面生成、分解などの機能を利用することが出来ます。もちろん、これらは Forge Viewer JavaScript API 側でも制御出来るので、別の JavaScript ライブラリなどと連携させることが可能です。ま</p>
<p>次の動画で操作をチェックしてみてください。&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="270" src="https://www.youtube.com/embed/bWHb19Y3WHI" width="480"></iframe></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/08/materials-on-fusion-360-hands-on-forge-meetup.html" rel="noopener noreferrer" target="_blank">Autodesk Fusion 360 ハンズオン &amp; Forge Meetup マテリアル公開</a></strong> でも触れていますが、今回の meetup では、東京会場と大阪会場で少し異なるモデルを作成しています。それぞれ簡単なアニメーションを定義していますので、アーカイブ ファイルもダウンロードしてみてください。</p>
<p>東京モデル： <span class="asset  asset-generic at-xid-6a0167607c2431970b022ad369c8b7200c img-responsive"><a href="http://adndevblog.typepad.com/files/0802-case-practice-final---tokyo-model.f3d"><strong>0802 Case Practice Final - Tokyo model.f3d</strong> をダウンロード</a></span></p>
<p>大阪モデル： <span class="asset  asset-generic at-xid-6a0167607c2431970b022ad369c8ba200c img-responsive"><a href="http://adndevblog.typepad.com/files/0824-case-practice-final---osaka-model.f3d"><strong>0824 Case Practice Final - Osaka model.f3d</strong> をダウンロード</a></span></p>
<p>By Toshiaki Isezaki</p>
