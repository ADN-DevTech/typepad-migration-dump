---
layout: "post"
title: "LMV Ninja を使った URN 指定表示"
date: "2018-05-16 00:56:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/05/display-specified-urn-on-lmv-ninja.html "
typepad_basename: "display-specified-urn-on-lmv-ninja"
typepad_status: "Publish"
---

<p>Forge 1 Day Workshop でもご紹介している&nbsp;<a href="http://adndevblog.typepad.com/technology_perspective/2016/09/understanding-steps-to-use-viewer-on-postman.html" ping="/url?sa=t&amp;source=web&amp;rct=j&amp;url=http://adndevblog.typepad.com/technology_perspective/2016/09/understanding-steps-to-use-viewer-on-postman.html&amp;ved=0ahUKEwiKufSF9JraAhUEv5QKHQ8sD18QFggoMAA" rel="noopener noreferrer" target="_blank">Postman による Viewer 利用手順の理解 - 2 legged 認証</a>、<a href="http://adndevblog.typepad.com/technology_perspective/2016/12/understanding-steps-to-use-viewer-on-postman2.html" ping="/url?sa=t&amp;source=web&amp;rct=j&amp;url=http://adndevblog.typepad.com/technology_perspective/2016/12/understanding-steps-to-use-viewer-on-postman2.html&amp;ved=0ahUKEwiKufSF9JraAhUEv5QKHQ8sD18QFggzMAE" rel="noopener noreferrer" target="_blank">Postman による Viewer 利用手順の理解 - 3 legged 認証</a>、<a href="http://adndevblog.typepad.com/technology_perspective/2017/05/forge-nodejs-quick-start-part1.html" rel="noopener noreferrer" target="_blank">Forge Node.js クイックスタート ～ その1</a>、<a href="http://adndevblog.typepad.com/technology_perspective/2017/05/forge-nodejs-quick-start-part2.html" rel="noopener noreferrer" target="_blank">Forge Node.js クイックスタート ～ その2</a>&nbsp;では、Pottman や Forge SDK を使って取得した Access Token と BASE64 エンコードしたURN（ドキュメント ID）を作成済の <strong><a href="http://adndevblog.typepad.com/files/viewer.html">Viewer.html</a></strong> や <strong><a href="http://adndevblog.typepad.com/files/basic_application.html">Basic_Application.html</a></strong>&nbsp;で表示テストしていました。</p>
<p>開発時のテストでは、より簡単に URN をして Forge Viewer に表示してみたいことがあるかもしれません。また、同時に今後リリースされる予定の Forge Viewer バージョンでどのように表示されるかをテストしたいことがあるかもしれません。このような場面では、以前、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/02/test-and-debug-for-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer のテストとデバッグ</a></strong> でご紹介した <strong><a href="http://lmv.ninja.autodesk.com/" rel="noopener noreferrer" target="_blank">LMV Ninja</a></strong>&nbsp; を利用することが出来ます。</p>
<p>環境（1:Environment）や Endpoint（2:Endpoint）を指定したら、ページ中段の 4:Model で Custom タブを選択後、Urn と Token&nbsp; を入力して <span style="background-color: #84bb00;">&nbsp;</span><span style="background-color: #84bb84; color: #ffffff; font-family: terminal, monaco;">[Launch Viewer!]</span><span style="background-color: #84bb00;">&nbsp;</span> ボタンをクリックしてみてください。ページ下部の Viewer 領域に指定した URN のドキュメントを表示することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e77431970c-pi" style="display: inline;"><img alt="Lmv_ninja" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e77431970c image-full img-responsive" src="/assets/image_682347.jpg" title="Lmv_ninja" /></a></p>
<p>なお、モデルを表示した際には、ページ下部に各種 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/12/extensions-abailable-on-forge-viewer.html" rel="noopener noreferrer" target="_blank">Extension</a></strong> をロードしてテストするためのボタン群が表示されます。 ボタンをクリックすると、当該 Extension をロードします。ツールボタンを持つものであれば、ボタンが表示されるので、適宜。Extension のテストが可能です。<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0a023c80970d-pi" style="display: inline;"><img alt="Extension_test" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0a023c80970d image-full img-responsive" src="/assets/image_385485.jpg" title="Extension_test" /></a></p>
<p>また、Extension ボタン群の下には、これも Extension の機能や Forge Viewer グラフィックスをテストするためのタブが表示されます。</p>
<p>例えば、[Graphhcs] タブでは&nbsp;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/12/release-forge-viewer-v3_3.html" rel="noopener noreferrer" target="_blank">Forge Viewer バージョン 3.3 リリース</a></strong> でご紹介した&nbsp;<strong>非フォトグラフィックス レンダリング スタイル</strong> をテストすることが出来ます。同様に、Wireframes タブでは&nbsp;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/12/mesh-representation-on-viewer.html" rel="noopener noreferrer" target="_blank">Viewer でのメッシュ状表示</a></strong> でご案内してワイヤーフレーム（メッシュ）表示を試すことが出来るボタンが配置されています。</p>
<p>&nbsp;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c95f1cd9970b-pi" style="display: inline;"><img alt="Graphics_test" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c95f1cd9970b image-full img-responsive" src="/assets/image_369251.jpg" title="Graphics_test" /></a></p>
<p>必要に応じてお試しください。</p>
<p>By Toshiaki Isezaki&nbsp;</p>
<p>&nbsp;</p>
