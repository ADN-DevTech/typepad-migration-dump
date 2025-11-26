---
layout: "post"
title: "Forge Viewer：レベル別の表示"
date: "2020-09-30 00:02:56"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/09/displaying-per-level-on-forge-viewer.html "
typepad_basename: "displaying-per-level-on-forge-viewer"
typepad_status: "Publish"
---

<p>Revit モデルを利用する建築プロジェクトでは、よくモデル内の要素をレベル別に表示したいことがあります。今日は、そのような用途で利用出来るいくつかの方法をご紹介しておきます。</p>
<hr />
<p>Forge Viewer では、カンバス上に表示されるオブジェクトを一意な識別子で認識しているので、対象の要素が持つ識別子がわかれば、それらを使って表示に反映させることが出来ます。Forge Viewer では、この識別子を ObjectId、ないしは、dbId と呼んでいます。</p>
<p>最も容易に dbid を取得する方法には、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#search-text-onsuccesscallback-onerrorcallback-attributenames" rel="noopener" target="_blank">Viewer3D.Seaech</a> </strong>メソッドで、表示制御したいカテゴリ名やタイプ名を検索する方法があげられます。&quot;消火栓 3FL&quot; のような検索も可能性です。</p>
<p>例）検索オブジェクトを isolate（選択表示）</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">    function onSearchResult(idArray) {
        _viewer.isolate(idArray);
    }

    $(document).on(&quot;click&quot;, &quot;[id^=&#39;search&#39;]&quot;, function () {
        var keyword = document.getElementById(&#39;keyword&#39;).value;
        _viewer.search(keyword, onSearchResult);
    });
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>ただ、&quot;3FL&quot; として検索してしまうと、基準レベルのプロパティだけではく、他のプロパティに設定されている &quot;3FL&quot; もヒットしてしまうので、少し目的とは異なってしまう可能性もあるかと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be41311dd200d-pi" style="display: inline;"><img alt="Serarch" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be41311dd200d image-full img-responsive" src="/assets/image_75185.jpg" title="Serarch" /></a></p>
<p>属性に応じて、より dbid を詳細に把握するには、Model Derivative API の持つ endpoint で JSON を取得してパースする方法もあります。この方法は、<strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/tutorials/xtract-metadata/" rel="noopener" target="_blank">Extract Metadata From a Source File</a></strong>&#0160;で紹介されています。JSON の取得など、過去のブログ記事 <a href="https://adndevblog.typepad.com/technology_perspective/2020/09/viewer-workflow-on-vs-code-forge-extension.html" rel="noopener" target="_blank"><strong>VS Code Forge Extension を使った Viewer ワークフローの確認</strong> </a>でもご案内した <em><strong><a href="https://marketplace.visualstudio.com/items?itemName=petrbroz.vscode-forge-tools" rel="noopener" target="_blank">Autodesk Forge Tools</a>&#0160;</strong>エクステンション</em>をお使いいただくと、開発・調査の目的で、Postman より簡単に目的の JSON を取得することが出来ます。</p>
<hr />
<p><strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#setcutplanes-planes" rel="noopener" target="_blank">Viewer3D.setCutPlanes</a></strong> メソッドで断面解析ツールの機能を内部的に使い、視覚的に特定フロアを表示することが可能です。レベルや境界などに合わせて切断面を指定する必要があります。切断面の指定には、Forge Viewer JavaScript ライブラリのベースになっている <strong><a href="https://threejs.org/" rel="noopener" target="_blank">three.js</a></strong> ライブラリの <strong><a href="https://threejs.org/docs/index.html#api/en/math/Vector4" rel="noopener" target="_blank">Vector4</a></strong> クラス インスタンスを利用します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be413121f200d-pi" style="display: inline;"><img alt="Setcutplanes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be413121f200d image-full img-responsive" src="/assets/image_119415.jpg" title="Setcutplanes" /></a></p>
<p>例）BOX 状の 6 面を指定</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">    var planes = [
        new THREE.Vector4(1, 0, 0, -250),
        new THREE.Vector4(0, 1, 0, -110),
        new THREE.Vector4(0, 0, 1, -2.5),
        new THREE.Vector4(-1, 0, 0, -130),
        new THREE.Vector4(0, -1, 0, -106),
        new THREE.Vector4(0, 0, -1, -5.8)
    ]
    _viewer.setCutPlanes(planes);
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>Forge Viewer 上で切断解析ツールを使って切断面を手動で作成しておき、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#getcutplanes" rel="noopener" target="_blank">Viewer3D.getCutPlanes</a></strong> メソッドで切断面を構成する値を得るようなことも出来ます。また、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/12/state-api-in-forge-viewer.html" rel="noopener" target="_blank">State API</a> </strong>と併用することで、フロア毎に表示状況を記憶させて利用することも出来るかと思います。</p>
<hr />
<p>BIM 360 Docs 上のレベル ツールを実装する <strong>Autodesk.AEC.LevelsExtension</strong> をロードして、カンバス内に表示されるパネルからレベル毎に要素を表示させることが出来ます。内部的に切断面を得る目的で、AEC Model Data（AECModelData.json）と呼ばれる JSON を得る必要がある点に注意してください。</p>
<pre>    function onDocumentLoadSuccess3(viewerDocument) {

        var viewables = viewerDocument.getRoot().search({
            &#39;type&#39;: &#39;geometry&#39;,
            &#39;role&#39;: &#39;3d&#39;
        });

        var defaultModel = viewerDocument.getRoot().getDefaultGeometry();
        _viewer.loadDocumentNode(viewerDocument, defaultModel).then(i =&gt; {
            viewerDocument.downloadAecModelData();
        });

        _viewer.addEventListener(Autodesk.Viewing.GEOMETRY_LOADED_EVENT, onViewerGeometryLoaded);

    }
                                           :
    function onViewerGeometryLoaded(event) {
        _viewer3d_3.loadExtension(&#39;Autodesk.AEC.LevelsExtension&#39;);
    }
</pre>
<p>AEC Model Data は、Revit 2018 以降の RVT ファイルで使用することが出来るデータです。また、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/09/bim-360-viewer-vs-forge-viewer.html" rel="noopener" target="_blank">BIM 360 Viewer と Forge Viewer の違い</a></strong> でご紹介した意図的な視覚表現の抑制もあるため、Viewer の Extension ドキュメントには記載されていません。参考としてご確認ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e966f070200b-pi" style="display: inline;"><img alt="Levelsextension" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e966f070200b image-full img-responsive" src="/assets/image_516451.jpg" title="Levelsextension" /></a></p>
<hr />
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
