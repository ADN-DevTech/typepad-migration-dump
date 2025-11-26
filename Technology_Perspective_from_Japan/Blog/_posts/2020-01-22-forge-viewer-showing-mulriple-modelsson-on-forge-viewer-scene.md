---
layout: "post"
title: "Forge Viewer シーンへの複数モデルの表示（一部改定・追記）"
date: "2020-01-22 00:19:07"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/01/forge-viewer-showing-mulriple-modelsson-on-forge-viewer-scene.html "
typepad_basename: "forge-viewer-showing-mulriple-modelsson-on-forge-viewer-scene"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/showing-mulriple-modelsson-on-forge-viewer-scene.html" rel="noopener" target="_blank">Forge Viewer シーンへの複数モデルの表示</a></strong> のブログ記事でご紹介した内容について、Forge Viewer v7 を反映、一部改訂してご案内したいと思います。</p>
<p>Forge Viewer は、Model Derivative API で SVF 変換したデザインを、ストリーミング配信を使って表示することが出来ます。通常、表示は、変換に使用したシードファイル（CAD 毎に異なる返還前のデザイン ファイル）単位で実装することになります。ただ、コンフィギュレータなど、単一のシードファイルにないモデルを、同一シーンに表示させたいことがあるかもしれません。</p>
<p>Forge Viewer の JavaScript ライブラリでは、そのような用途のために <strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#loadmodel-url-options-onsuccesscallback-onerrorcallback" rel="noopener" target="_blank">LoadModel</a>&#0160;</strong>メソッドを用意しています。もちろん、この方法で表示させたいモデルも、事前に Model Derivative API で SVF 変換しておく必要があります。</p>
<p>実際の使用では、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/10/forge-viewer-v7-code.html" rel="noopener" target="_blank">Forge Viewer：v7 コード</a> </strong>などでカンバス内にメインとなるモデルを表示、ジオメトリのロード完了後に LoadModel メソッドを利用する事になります。LoadModel メソッドの最初のパラメータには、カンバス上に表示させたいサブモデルの URN を指定します。この URN 指定では、メインモデルを表示で指定した Base64 エンコードしたシードファイルの URN ではなく、変換済の SVF を示す URN を直接指定します。分かりやすくご紹介するなら、シード ファイルを変換した際に作成されるマニフェストで、SVF を指し示す次の URN 文字列となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3be2b01200d-pi"><img alt="Manufest" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3be2b01200d image-full img-responsive" src="/assets/image_252252.jpg" title="Manufest" /></a></p>
<p>ここで Base64 エンコードしたシードファイルの URN を指定してしまうと、Viewer コード実行時に次のエラーが表示されるので分かりやすいはずです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3ddd0b2200b-pi"><img alt="Erroro" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3ddd0b2200b img-responsive" src="/assets/image_936981.jpg" title="Erroro" /></a></p>
<p>上記例だと、指定すべき値は &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTV<br />sc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvVG93ZXItTm9Cb3VuZG<br />FyeS5ud2Q&quot; ではなく、&quot;urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkL<br />WphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb<br />3h6NXB6b251dGktN2cvQnVpbGRpbmctTm9Cb3VuZGFyeS5ud2Q/output/0/0.svf&quot; となります。なお、この値の前に、https://developer.api.autodesk.com/derivativeservice/v2/derivatives/ を挿入して指定することも出来ます。<a href="https://adndevblog.typepad.com/technology_perspective/2019/11/forge-viewerpdf-pdf-drawing.html" rel="noopener" target="_blank"><strong>Forge Viewer：PDF 図面 </strong></a>でご紹介した&#0160; <strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/PDFExtension/" rel="noopener" target="_blank">Autodesk.PDF Extenson</a></strong> を使った PDF ファイルのベクトル表示の場合には、https://developer.api.autodesk.com/derivativeservice/v2/derivatives/ を付加した指定以外受け付けませんのでご注意ください。</p>
<p>LoadModel() メソッドで重要になるのは、各モデルの位置合わせです。一般に、CAD データでは尺度という概念を持ち、用途によっては、更に緯度経度といった位置情報も持ち合わせています。ただし、three.js をベースにする Forge Viewer ライブラリには、そのような概念はありません。</p>
<p>この状態で、メインモデル、サブモデル単位に個別に Model Derivative API で変換、LoadModel() で配置した場合、表示されたモデルが干渉してしまったり（重なって表示されたり）、モデルの大きさが変わってしまう結果になってしまいます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4b0b50a200c-pi" style="display: inline;"><img alt="Without_alignment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4b0b50a200c image-full img-responsive" src="/assets/image_857409.jpg" title="Without_alignment" /></a></p>
<p>Forge Viewer は、ロードするモデルの境界ボックスの中心をカンバス原点に合わせて配置・表示します。つまり、モデル毎に設定される原点が異なるため、上記のように意図しない結果になってしまいます。</p>
<p>このような場合では、Navisworks にすべてのモデルをインポートして、[単位と変換] ツールで各モデル（ノード）に適切な場所に位置合わせした後に、個々に .nwd ファイル形式でエクスポート、Model Derivative API での SVF 変換を実行することで、シーン内の相対位置を維持したまま（適切な場所に位置合せした状態を維持したまま）モデルを扱うことが出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ff521d200b-pi" style="display: inline;"><img alt="Navisworks" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4ff521d200b image-full img-responsive" src="/assets/image_222104.jpg" title="Navisworks" /></a></p>
<p>位置合せが完了したら、インポート済みのノードを「非表示」にすることで、個別のモデルを保存することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4b57bf9200c-pi" style="display: inline;"><img alt="Hideen" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4b57bf9200c image-full img-responsive" src="/assets/image_580710.jpg" title="Hideen" /></a></p>
<p>Navisworks で適切に位置合せした NWD ファイルを個別に Model Derivative API で下 SVF（viewable）して、LoadModel() メソッドで挿入/表示する際には、2 つめの options パラメータで指定する&#0160; Global Offset の値を指定することで、Navisworks 上で位置合せした相対位置（オフセット量）を維持しての配置が可能となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4b0c07f200c-pi" style="display: inline;"><img alt="With_alignment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4b0c07f200c image-full img-responsive" src="/assets/image_433553.jpg" title="With_alignment" /></a></p>
<pre>var _viewer = null;<br />var _goffset = 0.0;<br /><br />function initializeViewer() {<br /><br />    var options = {
        env: &#39;AutodeskProduction&#39;,
        api: &#39;derivativeV2&#39;,  // for models uploaded to EMEA change this option to &#39;derivativeV2_EU&#39;
        language: &#39;ja&#39;,
        getAccessToken: getCredentials
    };

    Autodesk.Viewing.Initializer(options, function () {

        _viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById(&#39;viewer&#39;));
        startedCode = _viewer.start();
        if (startedCode &gt; 0) {
            console.error(&#39;Failed to create a 3D Viewer: WebGL not supported.&#39;);
            return;
        }

        urn = &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvTFJULVN0YXRpb24ubndk&quot;;
        documentId = &#39;urn:&#39; + urn;
        Autodesk.Viewing.Document.load(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);

        console.log(&#39;Initialization complete, loading a model next...&#39;);

    });

    function onDocumentLoadSuccess(viewerDocument) {

        var viewables = viewerDocument.getRoot().search({
            &#39;type&#39;: &#39;geometry&#39;,
            &#39;role&#39;: &#39;3d&#39;
        });

        _viewer.loadDocumentNode(viewerDocument, viewables[0]).then(i =&gt; {
            _viewer.setLightPreset(17);
            _viewer.setEnvMapBackground(false);
            _viewer.setGroundShadow(false);
            _viewer.navigation.toPerspective();
        });

        _viewer.setSelectionColor(new THREE.Color(0xFFFF00));

        _viewer.addEventListener(Autodesk.Viewing.GEOMETRY_LOADED_EVENT, onViewerGeometryLoaded);

    }

    function onDocumentLoadFailure() {
        console.error(&#39;Failed fetching Forge manifest&#39;);
    }

    function onViewerGeometryLoaded(event) {
<strong>        _goffset = event.model.getData().globalOffset;
</strong>    }

}

function onSuccessCallback(result) {
}

function onErrorCallback(err) {
}

$(document).on(&quot;click&quot;, &quot;[id^=&#39;<span style="color: #ff0000;"><strong>wo-align</strong></span>&#39;]&quot;, function () {
<strong>    var urn = &quot;https://developer.api.autodesk.com/derivativeservice/v2/derivatives/urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvJUUzJTgyJUFDJUUzJTgzJUE5JUUzJTgyJUI5JUU1JUIxJThCJUU2JUEwJUI5Lm53ZA/output/0/0.svf&quot;;
    _viewer.loadModel(urn, { globalOffset: _goffset }, onSuccessCallback, onErrorCallback);
</strong>});

$(document).on(&quot;click&quot;, &quot;[id^=&#39;<strong><span style="color: #ff0000;">w-align</span></strong>&#39;]&quot;, function () {
<strong>    var urn = &quot;https://developer.api.autodesk.com/derivativeservice/v2/derivatives/urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvJUUzJTgyJUFDJUUzJTgzJUE5JUUzJTgyJUI5JUU1JUIxJThCJUU2JUEwJUI5X2FsaWduLm53ZA/output/0/0.svf&quot;;
    _viewer.loadModel(urn, { globalOffset: _goffset }, onSuccessCallback, onErrorCallback);
</strong>});</pre>
<p>同じモデル群に対して位置合わせ等をテストする場合、もし、コードを修正しても表示に反映されず、期待した結果にならないなら、ブラウザ キャッシュをクリアすると問題が解消することがありますのでお試しください。</p>
<p>By Toshiaki Isezaki</p>
