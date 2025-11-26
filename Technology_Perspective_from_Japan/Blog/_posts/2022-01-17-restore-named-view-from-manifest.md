---
layout: "post"
title: "Forge Viewer：シードファイルのビュー復元"
date: "2022-01-17 00:02:58"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/01/restore-named-view-from-manifest.html "
typepad_basename: "restore-named-view-from-manifest"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/11/utilizeing-meta-data.html" rel="noopener" target="_blank"><strong>Model Derivative API：メタデータの活用</strong></a> でもご紹介したとおり、シードファイル（元のデザインファイル）を Forge Viewer で表示するために Model Derivative API で SVF/SVF2 に変換すると、マニフェストにビュー情報が書き出されます。</p>
<p>ビューの視点（カメラ）情報は、マニフェストの &quot;type&quot;: &quot;geometry&quot;, &quot;role&quot;: &quot;3d&quot; 属性を持つスコープ下の &quot;children&quot; 名の配列内、&quot;type&quot;: &quot;view&quot;, &quot;role&quot;: &quot;3d&quot;&#0160; 属性を持つスコープの &quot;camera&quot; 配列にシードファイルに設定されている視点（カメラ）の詳細値を見ることが出来るはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880611fdb200d-pi" style="display: inline;"><img alt="Camera_info" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880611fdb200d image-full img-responsive" src="/assets/image_65339.jpg" title="Camera_info" /></a></p>
<p>マニフェスト JSON をパースして &quot;camera&quot; 配列のみを抽出出来れば、その値を <strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#setviewfromarray-params" rel="noopener" target="_blank">setViewFromArray()</a></strong> に渡して Forge Viewer で視点を変更することが出来ます。</p>
<p>ただし、この方法で再現したビューは、シードファイルが Revit プロジェクト（.rvt）の場合、一部のビューと<span style="background-color: #ffffff;">ギャップが存在します</span>。また、シードファイルのビュー設定時に切断ボックスや前方/後方クリップが設定されていると、それらが反映されない問題が起こります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e139b263200b-pi" style="display: inline;"><img alt="Revit" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e139b263200b image-full img-responsive" src="/assets/image_534801.jpg" title="Revit" /></a></p>
<p>つまり、&quot;camera&quot; 配列のみ方法だけでは、視点を再現出来るものの、切断ボックスや前方/後方クリップを Forge Viewer で表現することが出来ません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e139b469200b-pi" style="display: inline;"><img alt="Viewer1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e139b469200b image-full img-responsive" src="/assets/image_605973.jpg" title="Viewer1" /></a></p>
<p>この方法では、マニフェストの &quot;camera&quot; 配列に後続する &quot;sectionBox&quot; 配列、&quot;sectionBoxTransform&quot; 配列を用いた <strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#setcutplanes-planes" rel="noopener" target="_blank">setCutPlanes</a></strong> メソッドでの反映が必要になってしまい煩雑です。setCutPlanes メソッドは、<a href="https://adndevblog.typepad.com/technology_perspective/2020/09/displaying-per-level-on-forge-viewer.html" rel="noopener" target="_blank"><strong>Forge Viewer：レベル別の表示</strong></a> でご紹介しています。</p>
<p>このような状態を考慮した場合、Forge Viewer の初期化時に、表示に使用する viewables の &quot;type&quot;&#0160; に &quot;geometry&quot; を指定してビューを直接表示する方法が一般的です。&quot;type&quot;&#0160; に &quot;view&quot; を指定すると、上記のマニフェストの view をフェッチすることも出来ます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">    function onDocumentLoadSuccess(viewerDocument) {

        var viewables = viewerDocument.getRoot().search({
<span style="color: #0000ff;"><strong>            &#39;type&#39;: &#39;geometry&#39;,</strong></span>
            &#39;role&#39;: &#39;3d&#39;
        });

        if (viewables.length == 0) {
            _viewer.uninitialize();
            _viewer = null;
            alert(&quot;No view type contained in the model!&quot;);
        } else {

            var index = 0;
            $(&#39;#cameras&#39;).children().remove();
            viewables.forEach(function (value) {
                $(&#39;#cameras&#39;).append($(&#39;&#39;).val(index).text(value.data.name));
                index = index + 1;
            });

            _viewerDocument = viewerDocument;
            _viewables = viewables;
            _viewer.loadDocumentNode(viewerDocument, viewables[0]).then(i =&gt; {
                _viewer.setTheme(&quot;dark-theme&quot;);
            });
        }
    }</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880612088200d-pi" style="display: inline;"><img alt="Viewer2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880612088200d image-full img-responsive" src="/assets/image_146215.jpg" title="Viewer2" /></a></p>
<p>By Toshiaki Isezaki</p>
