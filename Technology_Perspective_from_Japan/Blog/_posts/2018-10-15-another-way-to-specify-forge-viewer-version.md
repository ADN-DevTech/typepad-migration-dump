---
layout: "post"
title: "Forge Viewer バージョン指定のもう1つの方法"
date: "2018-10-15 01:19:57"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/10/another-way-to-specify-forge-viewer-version.html "
typepad_basename: "another-way-to-specify-forge-viewer-version"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3b8a34f200b-pi" style="float: right;"><img alt="Icon - viewer" class="asset  asset-image at-xid-6a0167607c2431970b022ad3b8a34f200b img-responsive" src="/assets/image_107155.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon - viewer" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad399055b200d-pi" style="float: right;"></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3990540200d-pi" style="float: right;"></a>Forge が提供する各 API は、それぞれリリース後からバージョン アップを繰り返しています。もっとも顕著なのは、最終的に利用することが多くなる Forge Viewer です。View and Data API から含め、頻繁なリビジョンアップやバージョン アップを繰り替えしていて、今日現在、メジャー バージョンで 6 を数えるまでになっています。それぞれの更新履歴は Forge ポータルでご確認いただけます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad372e538200c-pi" style="display: inline;"><img alt="Change_history" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad372e538200c image-full img-responsive" src="/assets/image_612253.jpg" title="Change_history" /></a></p>
<p>バージョンアップで JavaScript コードの互換性が崩れてしまう場合には、 動作を維持する目的で Style Sheet を含む Forge Viewer のバージョンを指定しておくことが出来ます。この方法は以前のブログ記事&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/specifying-version-to-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer のバージョン指定</a></strong>でご案内しています。当時、JavaScript コード側で指定する<strong><a href="https://ja.wikipedia.org/wiki/%E3%82%B3%E3%83%B3%E3%83%86%E3%83%B3%E3%83%84%E3%83%87%E3%83%AA%E3%83%90%E3%83%AA%E3%83%8D%E3%83%83%E3%83%88%E3%83%AF%E3%83%BC%E3%82%AF" rel="noopener noreferrer" target="_blank"> CDN（Content Delivery Network）</a></strong>は View and Data API 時代の <strong>viewingservice/v1</strong>/ が利用されていました。</p>
<pre>&lt;script src=&quot;https://developer.api.autodesk.com/<strong>viewingservice/v1</strong>/viewers/viewer3D.min.js<em><span style="color: #0000ff;"><strong>?v=v2.7.44</strong></span></em>&quot;&gt;&lt;/script&gt;</pre>
<p>現在では、一般的な CDN に <strong>modelderivative/v2</strong> が利用出来るようになっています。</p>
<pre>&lt;script src=&quot;https://developer.api.autodesk.com/<strong>modelderivative/v2</strong>/viewers/viewer3D.min.js<em><span style="color: #0000ff;"><strong>?v=v6.1.0</strong></span></em>&quot;&gt;&lt;/script&gt;</pre>
<p>両者では、CDN に指定する URL のクエリー パラメータ <span style="color: #0000ff;"><em><strong>?v=x.x.x</strong></em></span> として特定バージョンの JavaScript ライブラリを利用するようになっていました。</p>
<p>このバージョン指定について、クエリー パラメータではなく、URL パスとして指定する新しい記述方法が存在します。&#0160;</p>
<pre>&lt;!-- The Viewer CSS --&gt;<br />&lt;link rel=&quot;stylesheet&quot; href=&quot;https://developer.api.autodesk.com/modelderivative/v2/viewers/<em><span style="color: #0000ff;"><strong>6.1.*</strong></span></em>/style.min.css&quot; type=&quot;text/css&quot;&gt;</pre>
<pre>&lt;!-- The Viewer JS --&gt;<br />&lt;script src=&quot;https://developer.api.autodesk.com/modelderivative/v2/viewers/<em><strong><span style="color: #0000ff;">6.1.*</span></strong></em>/viewer3D.min.js&quot;&gt;&lt;/script&gt;</pre>
<p>クエリー パラメータを用いたバージョン指定もサポートされていますが、新しい記述のほうがスマートな気もします。今後の指定は、この方法をお使いいただいたほうがいいかもしれません。</p>
<p>By Toshiaki Isezaki</p>
