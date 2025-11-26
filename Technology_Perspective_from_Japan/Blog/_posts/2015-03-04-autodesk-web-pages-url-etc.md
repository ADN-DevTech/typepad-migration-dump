---
layout: "post"
title: "オートデスクの Web ページ URL、検索など"
date: "2015-03-04 00:33:08"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/03/autodesk-web-pages-url-etc.html "
typepad_basename: "autodesk-web-pages-url-etc"
typepad_status: "Publish"
---

<p>ご存じと思いますが、オートデスクの Web ページは <strong><a href="http://www.autodesk.co.jp" target="_blank">http://www.autodesk.co.jp</a></strong> の URL で検索することが出来ます。ただし、トップ ページ配下のページに移動していくと、URL がページ ID（英数字で構成された分かり難いもの）に変化してしまうものがあります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d1b718970c-pi" style="display: inline;"><img alt="Url" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d1b718970c img-responsive" src="/assets/image_30863.jpg" title="Url" /></a></p>
<p>例えば、デスクトップ製品をまとめた <strong>オートデスク デベロッパーネットワーク（ADN）</strong> &gt;&gt; <strong>プラットフォームテクノロジー</strong> のページを表示させると、URL が&#0160;<strong><a href="http://www.autodesk.co.jp/adsk/servlet/index?siteID=1169823&amp;id=8084359" target="_blank">http://www.autodesk.co.jp/adsk/servlet/index?siteID=1169823&amp;id=8084359</a></strong> と表示されるはずです。Web 管理システム上が自動的に URL を生成するのが理由ですが、このような URL を第三者に伝達しようとする場合、とても不便です。そこで、人間が記憶しやすいかたちで URL を置き換える仕組みが導入されています。先の プラットフォームテクノロジー ページは、<a href="http://www.autodesk.co.jp/developerplatforms" target="_blank"><strong>http://www.autodesk.co.jp/developerplatforms</strong></a> で表示させることも出来ます。後者は、オートデスクが意図的に設定した <strong>Marketing URL</strong>、通称、<strong>MURL</strong> と呼ばれています。</p>
<p>中には AutoCAD のトップページのように、<strong><a href="http://www.autodesk.co.jp/autocad" target="_blank">http://www.autodesk.co.jp/autocad</a></strong>&#0160;と入力すると、<a href="http://www.autodesk.co.jp/products/autocad/overview" target="_blank"><strong>http://www.autodesk.co.jp/products/autocad/overview</strong></a>&#0160;のように、比較的よく検索されるページには、あらかじめ判読可能な単語で URL が構成されてくるものもあります。</p>
<p>開発やサポートにかかわるページは、あまり話題になりくい場合もありますが、もちろん MRUL が設定されています。せっかくなので、ここに覚えておくと便利な MURL を記しておきたいと思います。</p>
<p><strong>サポート関連</strong>&#0160;</p>
<p style="padding-left: 30px;"><strong>サポート ページ トップ</strong><br /><a href="http://www.autodesk.co.jp/support" target="_blank"><strong>http://www.autodesk.co.jp/support</strong></a></p>
<p style="padding-left: 30px;"><strong>Autodesk Technical Q&amp;A トップ ページ（FAQ）<br /><a href="http://www.autodesk.co.jp/tech-faq" target="_blank">http://www.autodesk.co.jp/tech-faq</a><br /></strong></p>
<p style="padding-left: 30px;"><strong>認定ハードウェア検索ページ<br /><a href="http://www.autodesk.co.jp/certified-hardware" target="_blank">http://www.autodesk.co.jp/certified-hardware</a><br /></strong></p>
<p style="padding-left: 30px;"><strong>インストールとライセンス関連記事 まとめページ<br /><a href="http://www.autodesk.co.jp/installation-and-licensing" target="_blank">http://www.autodesk.co.jp/installation-and-licensing</a><br /></strong></p>
<p><strong>開発関連</strong></p>
<p style="padding-left: 30px;"><strong>プラットフォームテクノロジ</strong><br /><a href="http://www.autodesk.co.jp/developerplatforms" target="_blank"><strong>http://www.autodesk.co.jp/developerplatforms</strong></a></p>
<p style="padding-left: 30px;"><strong>AutoCAD API</strong><br /><a href="http://www.autodesk.co.jp/developautocad" target="_blank"><strong>http://www.autodesk.co.jp/developautocad</strong></a></p>
<p style="padding-left: 30px;"><strong>AutoCAD OEM</strong><br /><a href="http://www.autodesk.co.jp/developoem" target="_blank"><strong>http://www.autodesk.co.jp/developoem</strong></a></p>
<p style="padding-left: 30px;"><strong>Inventor API</strong><br /><a href="http://www.autodesk.co.jp/developinventor" target="_blank"><strong>http://www.autodesk.co.jp/developinventor</strong></a></p>
<p style="padding-left: 30px;"><strong>RealDWG</strong><br /><a href="http://www.autodesk.co.jp/developrealdwg" target="_blank"><strong>http://www.autodesk.co.jp/developrealdwg</strong></a></p>
<p style="padding-left: 30px;"><strong>Revit API</strong><br /><a href="http://www.autodesk.co.jp/developrevit" target="_blank"><strong>http://www.autodesk.co.jp/developrevit</strong></a></p>
<p style="padding-left: 30px;"><strong>Autodesk 3ds Max API</strong><br /><a href="http://www.autodesk.co.jp/develop3dsmax" target="_blank"><strong>http://www.autodesk.co.jp/develop3dsmax</strong></a></p>
<p style="padding-left: 30px;"><strong>Autodesk FBX</strong><br /><a href="http://www.autodesk.co.jp/developfbx" target="_blank"><strong>http://www.autodesk.co.jp/developfbx</strong></a></p>
<p style="padding-left: 30px;"><strong>Autodesk Maya API</strong><br /><a href="http://www.autodesk.co.jp/developmaya" target="_blank"><strong>http://www.autodesk.co.jp/developmaya</strong></a></p>
<p style="padding-left: 30px;"><strong>Autodesk MotionBuilder API</strong><br /><a href="http://www.autodesk.co.jp/developmotionbuilder" target="_blank"><strong>http://www.autodesk.co.jp/developmotionbuilder</strong></a></p>
<p style="padding-left: 30px;"><strong>Exchange Apps 公開情報、手順</strong><br /><strong><a href="http://www.autodesk.co.jp/developapps" target="_blank">http://www.autodesk.co.jp/developapps</a></strong></p>
<p>さて、このブログ記事は、Google や Microsoft Bing など、一般の Web 検索エンジンを使ったキーワード検索が可能です。この時重要になるのが、そのキーワードを組み合わせて検索するかです。URL を直接伝えて検索してもらう方法もいいですが、キーワードを意図的に意識させるようなサービスも存在します。</p>
<p>そのサービスの 1 つが&#0160;<strong><a href="http://lmgtfy.com/" target="_blank">http://lmgtfy.com/</a></strong> です。例えば、このサービスを使って、このブログ記事のなかから View and Data Web サービス API についての記事を検索する場合には、キーワード検索を模倣する URL をリンクとして利用することが出来ます。次のリンクをクリックして、何が起こるか確認してみてください。</p>
<p style="padding-left: 30px;"><a href="http://lmgtfy.com/?q=autodesk+perspective+view+data+api#" target="_blank"><strong>http://lmgtfy.com/?q=autodesk+perspective+view+data+api#</strong></a>&#0160;</p>
<p>Web ページや Email 内からキーワード検索へ誘導するには便利かも知れません。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
