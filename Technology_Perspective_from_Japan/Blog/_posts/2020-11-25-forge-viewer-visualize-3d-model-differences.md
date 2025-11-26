---
layout: "post"
title: "Forge Viewer：3D モデル差異の視覚化"
date: "2020-11-25 00:04:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/11/forge-viewer-visualize-3d-model-differences.html "
typepad_basename: "forge-viewer-visualize-3d-model-differences"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be426e0ef200d-pi" style="display: inline;"></a>BIM 360 Team の登場とともに導入された機能に、3D モデル バージョン間の差異をチェックする<a href="https://adndevblog.typepad.com/technology_perspective/2016/10/name-change-of-a360-team.html" rel="noopener" target="_blank">モデル比較機能</a>がありました。現在では、BIM 360 の 「<a href="https://www.autodesk.co.jp/bim-360/bim-collaboration-software/design-collaboration/change-visualization/change-visualization-features/" rel="noopener" target="_blank">変更の表示</a>」機能として利用することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdea2b59b200c-pi" style="display: inline;"><img alt="Fifftool" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdea2b59b200c image-full img-responsive" src="/assets/image_324950.jpg" title="Fifftool" /></a></p>
<p><em>この機能は </em><strong>Autodesk.DiffTool</strong> <em>エクステンション</em>で実装されていて、Forge Viewer 上で利用することで、2 つのバージョン間の変更点（差異）を視覚的に Viewer 上に表現することが出来るようになります。いまのところ、Viewer の <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/" rel="noopener" target="_blank">Extension ドキュメント</a>には記載されていませんので、参考としてご確認ください。</p>
<p>Autodesk.DiffTool <em>エクステンション</em>を使用するには、まず 2 つのモデル Viewer カンバスにロードしてから、オプションを設定して<em>エクステンション</em>をロードするだけです。</p>
<p>指定可能なオプションは次のとおりです。</p>
<ul>
<li><strong>primaryModels</strong> (必須)：</li>
</ul>
<p style="padding-left: 80px;">ロードされた Autodesk.Viewing.Model インスタンスの配列で、追加、削除、変更されたオブジェクトの点で、現在の状態として差分操作に参加します。</p>
<ul>
<li><strong>diffModels</strong> (必須)：</li>
</ul>
<p style="padding-left: 80px;">ロードされた他の Autodesk.Viewing.Model インスタンスの配列です。比較するモデルのペアを定義するには、配列長が primaryModels と一致している必要があります。</p>
<ul>
<li><strong>versionA</strong></li>
</ul>
<p style="padding-left: 80px;">モデルのバージョン識別子です。</p>
<ul>
<li><strong>versionB</strong>（必須）：</li>
</ul>
<p style="padding-left: 80px;">差分モデルのバージョン識別子です。</p>
<ul>
<li><strong>mimeType</strong> （必須）：</li>
</ul>
<p style="padding-left: 80px;">mimeType を定義することで、内部ロジックが若干適応され、モデルから分野、カテゴリ、名前をより良く抽出できるようになります。現在サポートされている MIME タイプは次のとおりです。</p>
<ul>
<li>
<ul>
<li>&#39;<strong>application/vnd.autodesk.revit&#39;</strong>：Revit プロジェクト ー .rvt</li>
<li><strong>&#39;application/vnd.autodesk.autocad.dwg&#39;</strong> ：AutoCAD 3D モデル －.dwg</li>
<li><strong>&#39;application/vnd.autodesk.navisworks&#39;</strong> ：Navisworks モデル ー .nwd</li>
</ul>
</li>
</ul>
<p>変更された要素のみを表示したりといったように、フィルタリング操作の機能を提供します。また、個々の変更点を簡単に見つけることができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9755e63200b-pi" style="display: inline;"><img alt="Overlay" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9755e63200b image-full img-responsive" src="/assets/image_531859.jpg" title="Overlay" /></a></p>
<p>Autodesk.DiffTool <em>エクステンション</em> は、「並べて表示」ビュー（&#39;sidebyside&#39;）や「オーバーレイ」ビュー（&#39;overlay&#39;）を提供します。特にコードを追加することなく、用意されたツールバー ボタンから両者を切り替えることが可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9755e2b200b-pi" style="display: inline;"><img alt="Sidebyside" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9755e2b200b image-full img-responsive" src="/assets/image_556273.jpg" title="Sidebyside" /></a></p>
<p>[変更] パネル右上には、2 つのモデルの比較結果を CSV ファイルとして保存するボタンも用意されています。 <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdea2fb43200c-pi" style="display: inline;"><img alt="Report" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdea2fb43200c image-full img-responsive" src="/assets/image_549665.jpg" title="Report" /></a></p>
<p>Autodesk.DiffTool <em>エクステンション</em>は次のようなコードでロードして利用が可能です。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">    function onViewerGeometryLoaded(event) {
        var extensionConfig = {}
        extensionConfig.mimeType = &#39;application/vnd.autodesk.revit&#39;
        extensionConfig.primaryModels = [_viewer3d_1.getVisibleModels()[1]]
        extensionConfig.diffModels = [_viewer3d_1.getVisibleModels()[0]]
        extensionConfig.diffMode = &#39;overlay&#39;
        extensionConfig.versionA = &#39;sample_architecture.rvt v1&#39;
        extensionConfig.versionB = &#39;sample_architecture.rvt v2&#39;
        _viewer3d_1.loadExtension(&#39;Autodesk.DiffTool&#39;, extensionConfig)
            .then(function (res) {
                window.DIFF_EXT = _viewer3d_1.getExtension(&#39;Autodesk.DiffTool&#39;);
                console.log(window.DIFF_EXT);
            })
            .catch(function (err) {
                console.log(err);
            });
    }
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>注意事項と制限事項:</p>
<ul>
<li>Autodesk.DiffTool <em>エクステンション</em>は SVF ベースのモデルや SVF2 ベースのモデルで動作します。</li>
<li>クラウド上でバージョンのモデル間の差異を返す REST API はありません。現状では、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/11/utilizeing-meta-data.html" rel="noopener" target="_blank">Model Derivative API：メタデータの活用</a></strong> のブログ記事でご紹介した方法でメタデータを解析することになってしまいます。</li>
</ul>
<p>By Toshiaki Isezaki</p>
