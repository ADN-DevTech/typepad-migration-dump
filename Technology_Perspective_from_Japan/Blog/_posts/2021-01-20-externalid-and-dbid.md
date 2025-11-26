---
layout: "post"
title: "externalId と dbid の相互取得"
date: "2021-01-20 00:04:18"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/01/externalid-and-dbid.html "
typepad_basename: "externalid-and-dbid"
typepad_status: "Publish"
---

<p>CAD ソフトウエアでは、アドイン（プラグイン）アプリケーションの開発用に API&#0160; を用意しています。そして、API で特定の図形（ジオメトリ）などを識別する目的で識別子を使用するのが一般的です。例えば、AutoCAD の<strong><a href="https://adndevblog.typepad.com/technology_perspective/2013/10/object-identifer-of-dautocad-api.html" rel="noopener" target="_blank">エンティティ名</a></strong>や Revit の <strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/03/understanding-revit-api-for-autocad-addon-developers-part5.html" rel="noopener" target="_blank">Element Id</a></strong> などです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be430d2dd200d-pi" style="display: inline;"><img alt="Externalid" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be430d2dd200d image-full img-responsive" src="/assets/image_543913.jpg" title="Externalid" /></a></p>
<p>一方、Forge Viewer 上ではオブジェクトを識別するために <strong>dbid</strong> と呼ぶ識別子を利用することになります。例えば、表示されているオブジェクトを選択した場合など、そのオブジェクトの dbid が返されるので、関連付けられたプロパティデータベースからメタデータを取得することが出来るようになります。</p>
<p>この dbid は、Model Derivative API を使った変換処理毎に変更される可能性があるため、同じシード ファイル（デザイン ファイル）の同じオブジェクトでも常に一定になるわけではありません。別の言い方をするなら、変更のない同じシード ファイルを変換した場合でも、同じジオメトリに異なる dbid が割り当てられる可能性があります。逆に、Model Derivative API での変換を繰り返さなければ、Forge Viewer で表示する際の dbid は、同じジオメトリに対して同じになります。</p>
<p>ただ、オブジェクトと関連情報を外部データベースに保持するようなケースでは、dbid をキーにする方法は推奨されません。Forge には永続的に使用する目的に、Model Derivative API 変換に左右されない識別子も存在します。それが <strong>externalId</strong> と呼ばれる識別子です。</p>
<p>externalId は、シード ファイルに永続的な識別子があれば、それをそのまま利用です。先の例に当てはめるなら、AutoCAD のハンドル番号、Revit の Unique Id です。また、シード ファイルが COM ベースの API を持ち、明示的な識別子を設定していない場合でも、Model Derivative API 変換時に externalId が付与されることになります。</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/11/utilizeing-meta-data.html" rel="noopener" target="_blank"><strong>Model Derivative API：メタデータの活用</strong></a> でご紹介したとおり、dbid と externalId は &#0160;<strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-metadata-guid-properties-GET/" rel="noopener" target="_blank">GET :urn/metadata/:guid/properties</a></strong> endpoint が返す JSON から得ることが出来ますが、外部データベース連携等で Forge Viewer 上で externalId から dbid を得たい場合もあるはずです。</p>
<p>そのような場合、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Model/#getproperties-dbid-onsuccesscallback-onerrorcallback" rel="noopener" target="_blank">getProperties</a></strong> メソッドで dbid から externalId を得ることが可能です。また、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Model/#getexternalidmapping-onsuccesscallback-onerrorcallback" rel="noopener" target="_blank">getExternalIdMapping</a></strong> メソッドで externalId から dbid を、それぞれ得ることが可能です。</p>
<p>次のコードは、Forge Viewer 上でオブジェクトを選択して dbid から externalId を得た後に、さらに externalId から dbid を抽出してデバッグ コンソールに表示するものです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-0">            _viewer.addEventListener(Autodesk.Viewing.SELECTION_CHANGED_EVENT, onSelected);
                                     :
                                     :
    function onSelected(event) {
        var dbIdArray = event.dbIdArray;
        console.log(&quot;dbId = &quot; + <strong>dbIdArray[0])</strong>;

        var id;
        _viewer.<strong>getProperties</strong>(dbIdArray[0], function (data) {
            id = data.externalId;
            console.log(&quot;&gt;&gt; externalId = &quot; + id);
        });
        _viewer.model.<strong>getExternalIdMapping</strong>(data =&gt; console.log(&quot;&gt;&gt; dbId = &quot; + data[id]));
    }</code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e98474ad200b-pi" style="display: inline;"><img alt="Handle" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e98474ad200b image-full img-responsive" src="/assets/image_241065.jpg" title="Handle" /></a></p>
<p>By Toshiaki Isezaki</p>
