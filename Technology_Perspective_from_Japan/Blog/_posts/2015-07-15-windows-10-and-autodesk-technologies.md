---
layout: "post"
title: "Windows 10 とオートデスク テクノロジ"
date: "2015-07-15 02:26:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/07/windows-10-and-autodesk-technologies.html "
typepad_basename: "windows-10-and-autodesk-technologies"
typepad_status: "Publish"
---

<p>7 &#0160;月 29 日にリリースされる Windows 10 について、オートデスク 製品の対応状況は、以前<a href="http://adndevblog.typepad.com/technology_perspective/2015/07/windows-10-support-information-for-existing-products.html" target="_blank">ご案内</a>したとおりです。</p>
<p>この Windows 10 では、<a href="https://www.microsoft.com/microsoft-hololens/en-us" target="_blank">HoloLens</a> といった新しい仮想現実(VR)/拡張現実(AR)&#0160;体験を実現するハードウェアが用意されるはずです。まだ、詳細は明らかになっていませんが、HoloLens には 3D の活用でオートデスクも協力しているようで、こちらの<a href="https://www.microsoft.com/microsoft-hololens/en-us/commercial" target="_blank">サイト</a>下部でオートデスクのロゴをご確認いただけます。</p>
<p>Windows 10 リリースでオートデスクと関連するものに、Autodesk Spark の統合が挙げられます。 このニュースは既に 4 月 30 日にプレスリリースとして<a href="http://news.autodesk.com/press-release/autodesk-consumer-group-and-education/autodesk-accelerates-future-digital-and-physical" target="_blank">公表</a>されていて、Microsoft 社の年次デベロッパ カンファレンスである Build 2015 で発表後に日本語ブログでも<a href="http://blogs.msdn.com/b/devamm/archive/2015/05/01/10611423.aspx" target="_blank">紹介</a>されています。</p>
<p><a href="https://spark.autodesk.com/" target="_blank">Autodesk Spark</a> はオープンソースの位置づけで、どなたにも利用いただけるプラットフォームです。具体的には、3D プリントに関わる処理を標準化するだけでなく、関連する処理を 3rd party アプリケーションに組み込んだり、プロセスを自動化したりする開発者向けの <a href="https://spark.autodesk.com/about" target="_blank">API</a>&#0160;を提供する予定です。</p>
<p>Autodesk Spark は、一般ユーザの方には、<a href="https://ember.autodesk.com/software" target="_blank"><strong>Autodesk&#0160;Print Studio</strong></a>&#0160;というアプリケーションとして目する機会が出てくるものと思います。Autodesk Print Studio は、3D プリントする .stl ファイルを読み込んで、モデルの位置決めや破損したポリゴンの修正、サポートの生成から 3D プリンタへの出力をおこなうツールです。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="312" mozallowfullscreen="" src="https://player.vimeo.com/video/131603396" webkitallowfullscreen="" width="500"></iframe></p>
<p>次の動画には、冒頭の HoloLens のイメージ映像の他、Fusion 360 から出力したモデルを Autodesk Print Studio で位置決め、サポートの作成をおこなう模様が含まれています。概要としてご覧いただければと思います。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/zYCjkDYDPck?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>なお、Autodesk Print Studio は、現在、AutoCAD Labs から Technical Preview を&#0160;<a href="labs-download.autodesk.com/us/labs/trials/worldwide/Autodesk_PrintStudio_v1.1.0_Win64.exe" target="_self">ダウンロード</a>&#0160;して利用することが出来ます。興味をお持ちの方は試してみてください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d13df99c970c-pi" style="display: inline;"><img alt="Print_studio" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d13df99c970c image-full img-responsive" src="/assets/image_349256.jpg" title="Print_studio" /></a></p>
<p>Autodesk Spark に関連して、新しい 3D プリンタのためのファイル形式 。.<strong>3mf</strong> ファイル形式の策定も 3MF Consortium（3MF コンソーシアム）&#0160;によって進められています。こちらについても、詳細をご紹介するのは時期尚早ですが、現在、3D プリントで主流になっている <strong>.stl</strong> ファイル形式に補完、または、置き換わるファイル形式として、ポリゴン/メッシュ情報だけでなく、3D ベンダー毎の工夫で補ってきたリッチな情報を扱えるよう、期待が持たれるところです。</p>
<p>さて、Autodesk Spark では、初のオートデスク製 3D プリンタである <a href="https://ember.autodesk.com/" target="_blank">Ember </a>につても進展があります。Ember については、昨年、その<a href="http://adndevblog.typepad.com/technology_perspective/2014/05/announcing-participation-to-3d-printier-market.html" target="_blank">発表</a>と Autodesk University 2014 で初公開されたハードウェアを<a href="http://adndevblog.typepad.com/technology_perspective/2014/12/au-2014-3d-printer-ember.html" target="_blank">お伝え</a>しています。Ember は、すでに欧米で購入することが可能になっていますが、 残念ながら、日本語では購入することが出来ない状態です。</p>
<p>そんな Ember ですが、Ember 自身の設計データは 3D モデルとして公開されています。この 3D データは、オートデスクのクラウド API である <span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c7acff84970b img-responsive"><a href="http://adndevblog.typepad.com/files/a360-view-data-service-%E3%83%81%E3%83%A9%E3%82%B7.pdf">View and Data API</a></span>&#0160;で提供されています。View and Data API は、WebGL ベースのテクノロジなので、Web ページに埋め込んで参照いただくことが可能です。Explode 機能を使って、アセンブリの状態を分解したり、モデル階層を把握するのに十分な機能を備えているだけでなく、パーツやサブアセンブリが持つ材質や質量などのプロパティ データも表示させることが出来ます。一度、ご確認ください。&#0160;</p>
<div class="media_embed" style="text-align: center;"><iframe allowfullscreen="true" frameborder="0" height="600px" mozallowfullscreen="true" src="https://myhub.autodesk360.com/u09/shares/public/SH7f1edQT22b515c761e4e63d0ac8bfe671d?mode=embed" webkitallowfullscreen="true" width="600px"></iframe></div>
<p>HoloLens、Spark、Ember といったテクノロジは、Windows 10 との兼ね合いも含め、詳細が分かり次第、別途ご案内していく予定です。</p>
<p>By Toshiaki Isezaki&#0160;</p>
