---
layout: "post"
title: "View and Data API の Live Review Extension"
date: "2016-05-18 00:09:34"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/05/live-review-extension-on-view-and-data-api.html "
typepad_basename: "live-review-extension-on-view-and-data-api"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：View and Data API は2016年6月に Viewer と &#0160;Model Derivative API に分離、及び、名称変更されました。</span></p>
<p>以前、GitHub に記載されている <strong><a href="https://github.com/Developer-Autodesk/workflow-node.js-view.and.data.api" target="_blank">Autodesk View and Data API Node.js Basic Sample リポジトリ</a></strong>&#0160; をベースにした &#0160;View and Data API のチュートリアルについて、ブログ記事を連載しました。</p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/02/view-and-data-api-tutorial.html" target="_blank">View and Data API チュートリアル</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part1-nodejs-basic.html" target="_blank">View and Data API チュートリアル ～ その1 ～ Node.js Basic</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part2-upload-and-translation.html" target="_blank">View and Data API チュートリアル ～ その2 ～ アップロードと変換</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part3-basic-extension.html" target="_blank">View and Data API チュートリアル ～ その3 ～ Basic Extension</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part4-object-selection.html" target="_blank">View and Data API チュートリアル ～ その4 ～ オブジェクト選択</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/04/view-and-data-api-tutorial-part5-displaying-panel.html" target="_blank">View and Data API チュートリアル ～ その5 ～ パネル表示</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/04/view-and-data-api-tutorial-part6-moving-camera.html" target="_blank">View and Data API チュートリアル ～ その6 ～ カメラ移動</a></strong></p>
<p>このチュートリアルをトレースすることで、View and Data API の基本的な機能や実装方法を把握したり、作成したビューアを拡張する手法である Extension についても習得することが出来ると思います。</p>
<p>ただし、作成したビューアで 3D モデルを表示した場合、1点おかしな点に気がつくかも知れません。リアルタイム コラボレーションを開始するための&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html" target="_blank">Live Review</a></strong>&#0160;のボタンが表示されない点です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c85df5ec970b-pi" style="display: inline;"><img alt="No_live_review" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c85df5ec970b image-full img-responsive" src="/assets/image_977879.jpg" title="No_live_review" /></a></p>
<p>実は、Live Review の機能は、チュートリアルで学習した Extension の形式で提供されます。つまり、明示的に Live Review を実装する Extension をロードしない限り、ビューア上のボタンが表示されることはありません。</p>
<p>チュートリアルで作成したビューアに Live Review を追加する場合には、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part3-basic-extension.html" target="_blank">View and Data API チュートリアル ～ その3 ～ Basic Extension</a></strong>&#0160;の 4. で実装した&#0160;loadExtension() で、<strong>Autodesk.Viewing.Collaboration</strong> の名前の Extension をロードするようにしてください。&#0160;</p>
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;">  <span class="pl-k">function</span> <span class="pl-en">loadExtensions</span>(<span class="pl-smi">viewer</span>) {
    <span class="pl-smi">viewer</span>.<span class="pl-en">loadExtension</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop<span class="pl-pds">&#39;</span></span>);</span><br /><span style="font-family: tahoma, arial, helvetica, sans-serif;"><strong>    viewer.loadExtension(&#39;Autodesk.Viewing.Collaboration&#39;);</strong>
  }</span>
</pre>
<p>この処理の実装後、ビューアで 3D モデルを表示すれば、「ライブ レビュー」 ボタンが表示されるようになるはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09019b74970d-pi" style="display: inline;"><img alt="Live_review" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09019b74970d image-full img-responsive" src="/assets/image_515047.jpg" title="Live_review" /></a></p>
<p>Live Review 自体の機能や操作手順は、A360 Viewer と同じです。通常は、[招待] ボタンで生成された URL をメールで遠隔地のメンバーに伝えることで、コラボレーションを開始することになります。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/bmvH5vBoeh0?feature=oembed" width="500"></iframe>&#0160;</p>
<p>必要に応じて、この Extension をロード解除することで、Live Review を利用出来ないようにすることも出来ます。</p>
<p>By Toshiaki Isezaki</p>
