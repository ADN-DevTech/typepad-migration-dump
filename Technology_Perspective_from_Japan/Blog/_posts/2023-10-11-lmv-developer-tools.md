---
layout: "post"
title: "LMV Developer Tools"
date: "2023-10-11 00:01:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/10/lmv-developer-tools.html "
typepad_basename: "lmv-developer-tools"
typepad_status: "Publish"
---

<p>過去に<a href="https://adndevblog.typepad.com/technology_perspective/2017/02/test-and-debug-for-forge-viewer.html" rel="noopener" target="_blank">Forge Viewer のテストとデバッグ</a> や&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2018/05/display-specified-urn-on-lmv-ninja.html" rel="noopener" target="_blank">LMV Ninja を使った URN 指定表示</a> でご紹介した LMV Ninja（<a href="http://lmv.ninja.autodesk.com/" rel="noopener noreferrer" target="_blank">http://lmv.ninja.autodesk.com/</a>）が利用出来なくなっているので、オートデスク製ではありますが、Google Chrome のエクステンション（拡張機能）をご紹介しておきたいと思います。</p>
<p>ちなみに、LMV は Large Model Viewer の略で、APS Viewer（旧名 Forge Viewer）の別称です。LMVは元々オートデスク社内用語でしたが、イベントでも言及されている影響のためか、一般に広く知られるようになっています。</p>
<p>このエクステンションは、<a href="https://chrome.google.com/webstore/category/extensions" rel="noopener" target="_blank">chrome ウェブストア</a> から <a href="https://chrome.google.com/webstore/detail/lmv-developer-tools/annfeccochdhninjikchkkioemhdpjje" rel="noopener" target="_blank">LMV Developer Tools</a> の名前で検索、入手することが出来ます。</p>
<p>利用は至って簡単です。<a href="https://viewer.autodesk.com/" rel="noopener" target="_blank">Autodesk Viewer</a> や独自に開発/カスタマイズされた Viewer など、APS Viewer ベースのビューアを Google Chrome 上で開いて LMV Developer Tools を有効にするだけです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39b16f5200c-pi" style="display: inline;"><img alt="Trigger_lmv_developer_tool" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39b16f5200c image-full img-responsive" src="/assets/image_900823.jpg" title="Trigger_lmv_developer_tool" /></a></p>
<p>LMV Developer Tools パネルが表示されたら、使用したい機能トグルをオンにするだけです。LMV Developer Tools が持つ主な機能は次のとおりです。</p>
<ul>
<li>モデル表示/非表示</li>
<li>モデル境界ボックス表示/非表示</li>
<li>テストベッドでモデルを開く</li>
<li>ワールド軸の表示</li>
<li>近・遠平面表示</li>
<li>フレームレート（FPS）モニター</li>
</ul>
<p>Hit monitor をオンにすると、マウスカーソル下の dbId や External Id、Fragment Id などを表示させることも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39b1720200c-pi" style="display: inline;"><img alt="Lmv_developer_tool" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39b1720200c image-full img-responsive" src="/assets/image_474395.jpg" title="Lmv_developer_tool" /></a></p>
<p>ちょっとしたデバッグ作業に便利かと思います。</p>
<p>By Toshiaki Isezaki</p>
