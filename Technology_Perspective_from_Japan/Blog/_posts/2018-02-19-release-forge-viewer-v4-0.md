---
layout: "post"
title: "Forge Viewer バージョン 4.0 リリース"
date: "2018-02-19 00:01:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/02/release-forge-viewer-v4_0.html "
typepad_basename: "release-forge-viewer-v4_0"
typepad_status: "Publish"
---

<p>ご案内が遅くなりましたが、Forge Viewer の新バージョン&#0160; 4.0 がリリースされています。今回はバージョン 4.0 での新機能をご紹介したいと思います。&#0160;</p>
<hr />
<p><strong>UI テーマの切り替え</strong></p>
<p>Viewer3D.setTheme() を使用することで、ビューア内に表示されるユーザ インタフェース（UI）のテーマをダークテーマ（既定値）とライトテーマから切り替えることが出来るようになりました。setTheme() のパラメータに指定する文字列は、ダークテーマを指定する場合は&#0160;&#0160;&quot;dark-theme&quot;、ライトテーマを指定する場合は&#0160;&#0160;&quot;light-theme&quot; で、いずれかを呼び出すことで、モデル表示中に UI テーマを動的に切り替えることが可能になりました。</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2db5433970c-pi" style="display: inline;"><img alt="Themas" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2db5433970c image-full img-responsive" src="/assets/image_122789.jpg" title="Themas" /></a></p>
<hr />
<p><strong>モデル ブラウザ検索結果の表示</strong></p>
<p>API 呼び出しとは直接関係しませんが、モデルブラウザを使用して任意文字列でモデルを検索した場合、従来のバージョン（バージョン 3.3.5 以前）と 4.0 バージョンで検索結果の表現方法が改善されています。以前は、検索結果が複数存在した場合、対象のすべてのモデルがモデルブラウザ上で同時にハイライトしていました。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c950ea3b970b-pi" style="display: inline;"><img alt="Search_3.0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c950ea3b970b image-full img-responsive" src="/assets/image_266915.jpg" title="Search_3.0" /></a></p>
<p>バージョン 4.0 では、まず、候補となる一覧がモデルブラウザ上で表示され、そのうち 1 つをクリックとモデル自体も拡大表示されるようになっています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c950ea4e970b-pi" style="display: inline;"><img alt="Search_4.0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c950ea4e970b image-full img-responsive" src="/assets/image_485662.jpg" title="Search_4.0" /></a></p>
<hr />
<p><strong>ツールバー関連 API（Toolbar API）</strong></p>
<p>Viewer3D などに Extension との併用を意識した新しい機能がツールバー関連 API に追加されています。</p>
<p>API リファレンス</p>
<div class="section" id="id2">
<div class="highlight-javascript">
<div class="highlight">
<pre><span class="nx">activateExtension</span><span class="p">(</span><span class="nx">extensionID</span><span class="p">,</span> <span class="nx">mode</span><span class="p">);</span>  <span class="c1">// 指定した extensionID と mode で Extension をアクティブ化します。</span>
                                       <span class="c1">// 既定値では getExtensionModes() 最初に使用可能なモードが使用されます。</span>

<span class="nx">deactivateExtension</span><span class="p">(</span><span class="nx">extensionID</span><span class="p">);</span>      <span class="c1">// 指定した extensionID で Extension 無効化します。</span>

<span class="nx">IsExtensionActive</span><span class="p">(</span><span class="nx">extensionID</span><span class="p">);</span>        <span class="c1">// extensionID で Extension がアクティブかどうかをチェックします。</span>

<span class="nx">IsExtensionLoaded</span><span class="p">(</span><span class="nx">extensionID</span><span class="p">);</span>        <span class="c1">// extensionID で Extension がロードされているかどうかチェックします。</span>

<span class="nx">getLoadedExtensions</span><span class="p">();</span>                 <span class="c1">// 現在ロードされているすべての Extension の一覧を取得します。</span>
 
<span class="nx">getExtensionModes</span><span class="p">(</span><span class="nx">extensionID</span><span class="p">);</span>        <span class="c1">// 指定した extensionID で利用可能なすべてのモードの一覧を取得します。</span>
                                       <span class="c1">// 例えば、&quot;Autodesk.Measure&quot;（計測ツール）には &quot;distance&quot;、</span>
                                       <span class="c1">// &quot;area&quot;、&quot;angle&quot;、&quot;calibrate&quot; の各モードがあります。</span></pre>
</div>
</div>
</div>
<hr />
<p>By Toshiaki Isezaki</p>
