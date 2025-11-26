---
layout: "post"
title: "Forge Viewer シーンへの複数モデルの表示"
date: "2019-02-13 03:15:25"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/02/showing-mulriple-modelsson-on-forge-viewer-scene.html "
typepad_basename: "showing-mulriple-modelsson-on-forge-viewer-scene"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3df10bd200b-pi" style="float: right;"><img alt="Viewer-api-blue" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3df10bd200b img-responsive" src="/assets/image_281057.jpg" style="margin: 0px 0px 5px 5px;" title="Viewer-api-blue" /></a>Forge Viewer には Model Derivative API で SVF 変換したデザインをストリーミング配信を使って表示することが出来ます。通常、表示は変換に使用したシードファイル（CAD 毎に異なる返還前のデザイン ファイル）単位で <strong><a href="https://forge.autodesk.com/en/docs/viewer/v6/tutorials/basic-viewer/">Basic Viewer</a></strong> や <strong><a href="https://forge.autodesk.com/en/docs/viewer/v6/tutorials/basic-application/" rel="noopener" target="_blank">Basic Application</a></strong> などの方法で実装することになります。ただ、ビューア上でコンフィギュレータなど、単一のシードファイルにないモデルを Forge Viewer の同一シーンに表示させたいことがあるかもしれません。</p>
<p>Forge Viewer JavaScript ライブラリでは、そのような用途に <strong><a href="https://forge.autodesk.com/en/docs/viewer/v6/reference/javascript/viewer3d/#loadmodel-url-options-onsuccesscallback-onerrorcallback" rel="noopener" target="_blank">Viewer3D.LoadModel</a> </strong>メソッドを用意しています。もちろん、この方法で表示させたいモデルも、事前に Model Derivative API で SVF 変換しておく必要があります。</p>
<p>実際の使用では、Autodesk.Viewing.<strong>GEOMETRY_LOADED_EVENT</strong> イベントを登録、イベントを検出して、カンバス内に <strong><a href="https://forge.autodesk.com/en/docs/viewer/v6/tutorials/basic-viewer/">Basic Viewer</a></strong> や <strong><a href="https://forge.autodesk.com/en/docs/viewer/v6/tutorials/basic-application/" rel="noopener" target="_blank">Basic Application</a></strong> などでメインとなるモデルを表示、ジオメトリのロード完了時に呼び出されるコールバック関数内に LoadModel() を記載する事になります。最初のパラメータには、カンバス上に（2つめ以降に）表示させたいモデルの URN を指定します。この URN 指定では、メインモデルを表示で指定した Base64 エンコードしたシードファイルの URN ではなく、変換済の SVF を示す URN を直接指定します。分かりやすくご紹介するなら、シード ファイルを変換した際に作成されるマニフェストにある SVF を指し示す URN 文字列となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3be2b01200d-pi" style="display: inline;"><img alt="Manufest" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3be2b01200d image-full img-responsive" src="/assets/image_252252.jpg" title="Manufest" /></a></p>
<p style="text-align: left;">ここで Base64 エンコードしたシードファイルの URN を指定してしまうと、Viewer コード実行時に次のエラーが表示されるので分かりやすいはずです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3ddd0b2200b-pi" style="display: inline;"><img alt="Erroro" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3ddd0b2200b img-responsive" src="/assets/image_936981.jpg" title="Erroro" /></a></p>
<p style="text-align: left;">上記例だと、指定すべき値は &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTV<br />sc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvVG93ZXItTm9Cb3VuZG<br />FyeS5ud2Q&quot; ではなく、&quot;urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkL<br />WphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb<br />3h6NXB6b251dGktN2cvQnVpbGRpbmctTm9Cb3VuZGFyeS5ud2Q/output/0/0.svf&quot; となります。</p>
<p>LoadModel() で重要になるのは、各モデルの位置合わせです。一般に、CAD データでは尺度という概念を持ち、用途によっては、更に緯度経度といった位置情報も持ち合わせています。ただし、three.js をベースにする Forge Viewer には、そのような概念はありません。</p>
<p>例えば、Navisworks 内で複数のモデルをインポート、位置合わせしたと仮定します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3ddeb54200b-pi" style="display: inline;"><img alt="Navisworks" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3ddeb54200b image-full img-responsive" src="/assets/image_281845.jpg" title="Navisworks" /></a></p>
<p>この状態で、メインモデル、サブモデル１、サブモデル２単位に個別に NWD ファイルで出力、Model Derivative API で変換して LoadModel() で配置した場合、表示されたモデルが重なってしまったり、モデルの大きさが変わってしまう結果になってしまいます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3be3f47200d-pi" style="display: inline;"><img alt="Duplicate" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3be3f47200d image-full img-responsive" src="/assets/image_790718.jpg" title="Duplicate" /></a></p>
<p>Forge Viewer は、ロードするモデルの<span style="text-decoration: underline;">境界ボックス</span>の中心をカンバス原点に合わせて配置・表示します。つまり、モデル毎に設定される原点が異なるため、上記のように意図しない結果になってしまいます。</p>
<p>ここで必要になるのが、2 つめの options パラメータで指定する&#0160; Global Offset の値です。Global Offset は、この配置時に、どの程度オフセット（ずらして）配置するかを指定します。前述のように、Navisworks のシーン内で相対位置が決まっている場合には、LoadModel() でメインモデルと同じ Global Offset 値を継承して配置すれば、相対位置を維持したまま表示をおこなうことが可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3dde54c200b-pi" style="display: inline;"><img alt="Align" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3dde54c200b image-full img-responsive" src="/assets/image_376455.jpg" title="Align" /></a></p>
<pre>...    <br />    // Intialize Viewer
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
<strong>        _viewer.addEventListener(Autodesk.Viewing.GEOMETRY_LOADED_EVENT, onViewerGeometryLoaded);
</strong>        });
    }

<strong>    function onViewerGeometryLoaded(<span style="color: #0000ff;">event</span>) {
        var urn = &quot;urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvQnVpbGRpbmctTm9Cb3VuZGFyeS5ud2Q/output/0/0.svf&quot;;
        _viewer.loadModel(urn, <span style="color: #0000ff;">{ globalOffset: event.model.getData().globalOffset }</span>);
        urn = &quot;urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvVG93ZXItTm9Cb3VuZGFyeS5ud2Q/output/0/0.svf&quot;;
        _viewer.loadModel(urn, <span style="color: #0000ff;">{ globalOffset: event.model.getData().globalOffset }</span>);
        _viewer.removeEventListener(Autodesk.Viewing.GEOMETRY_LOADED_EVENT, onViewerGeometryLoaded);
        
    }<br />...</strong></pre>
<p>SVF 変換間に同一シーンで位置合わせをおこなっていない場合には、three.js を併用したマトリックス変換をモデル挿入後に実施し、位置や大きさを変化させる方法もあります。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/11/revised-new-forge-viewer-tutorial-part2.html" rel="noopener" target="_blank">新しい Forge Viewer チュートリアル改訂版 ～ その2</a></strong> でご紹介している Viewing.Extension.Transform.min.js ファイルで定義された Extension が参考になるかと思います。</p>
<p>同じモデル群に対して位置合わせ等をテストする場合には、コードを修正しても表示に反映されず、期待した結果にならない場合があります。コードの変更による位置等の変化が確実な場面では、前回実行した内容がブラウザ キャッシュに残っていて、適切に反映されない原因になっていることもあります。ブラウザ キャッシュをクリアすると問題が解消することがありますのでお試しください。</p>
<p>なお、英語ブログになってしまいますが、併せて次の記事もご確認いただくこともお勧めします。</p>
<p style="padding-left: 40px;"><a href="https://forge.autodesk.com/cloud_and_mobile/2016/02/model-aggregation-with-view-data-api-exposed.html" rel="noopener" target="_blank"><strong>Aggregating multiple models in the Viewer</strong></a></p>
<p style="padding-left: 40px;"><strong><a href="https://forge.autodesk.com/blog/preparing-your-viewing-application-multi-model-workflows" rel="noopener" target="_blank">Preparing your viewing application for multi-model workflows</a></strong></p>
<p style="padding-left: 40px;"><strong><a href="https://forge.autodesk.com/blog/preparing-your-viewing-application-multi-model-workflows-part-2-model-loader" rel="noopener" target="_blank">Preparing your viewing application for multi-model workflows - Part 2: Model Loader</a></strong></p>
<p>By Toshiaki Isezaki</p>
