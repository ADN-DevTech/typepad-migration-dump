---
layout: "post"
title: "Autodesk Exchange Store "
date: "2013-03-09 13:33:51"
author: "Mikako Harada"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/03/autodesk-exchange-store-.html "
typepad_basename: "autodesk-exchange-store-"
typepad_status: "Publish"
---

<p>Autodesk Exchange Store (<a href="http://apps.exchange.autodesk.com/">http://apps.exchange.autodesk.com/</a>) をご存知でしょうか？Autodesk 製品のアドインを開発された方々が無料あるいは有料でアドインを提供し、それらのアドインをユーザーの方々がj自由にダウンロードして試したり、購入したりすることのできるサイトです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d41a8da30970c-pi" style="display: inline;"><img alt="ExchangeStore80" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017d41a8da30970c image-full" src="/assets/image_163266.jpg" title="ExchangeStore80" /></a></p>
<p>Exchange Store は、2012リリースで2年前にAutoCADからスタートし、昨年2013のリリースからはRevit、Inventor、AutoCAD のバーチカルといった製品も加わり、現在では以下に示すような15の製品のストアが存在します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c377986dd970b-pi" style="display: inline;"><img alt="Autodesk Exchange Store Selection" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017c377986dd970b" src="/assets/image_362803.jpg" title="Autodesk Exchange Store Selection" /></a></p>
<p>ただ、残念ながら現時点では英語版のストアのみの提供なので、日本の方々にとっては少し抵抗があるかもしれませんが、ストアに掲載されるアプリそのものは、英語版が提供されているのであれば、それと一緒に英語以外の言語版を含んでも良いことになっています。実際、例えば、英語と中国語がサポートされているアプリを掲載されているデベロッパーの方がいらっしゃいます。また、日本のADNメンバーであるGSA, Inc. (Global Solution Assists, Inc.) 様も日本語版で提供しているアプリの一部を英語版でストアに掲載されています。以下にRevit Exchange Store のトップページを示します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d41a8ee57970c-pi" style="display: inline;"><img alt="Exchange Store Revit" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017d41a8ee57970c image-full" src="/assets/image_925701.jpg" title="Exchange Store Revit" /></a></p>
<p>Exchange Storeは、言わば、開発者と利用者の接点の場。すでにアプリケーションを開発された方、お持ちのアプリを広くRevitのユーザーの方に使っていただきたいとお考えの方、あるいは、どうすれば将来顧客となるようなユーザーに遭遇することができるのだろうかとお考えの方が、Autodeskのサイトに一ヶ所にアプリを掲載できます。一方、ユーザーの方の中には、こまごました手作業の連続で少々面倒だと感じている機能を、もう少し独自のワークフローに沿って自動化はできないものか、そのようなツールはすで存在するのではないかとお考えの方がいらっしゃるのではないかと思います。そういったユーザーの方のために、ストアは様々なアプリを検索、発見する場を提供します。</p>
<p>これまで、ストアに関する幾つかハッピーエンディングの話も聞いています。これは、私たちのダイレクターが話してくれたのですが、例えば、イタリアのデベロッパーさんが、Vaultのアプリをストアに掲載したところ、それを試した、イギリスのユーザーさんがコンタクトしてきたというのです。ツールがほぼユーザーさんが欲している機能を持っているのだが、それを少し変更すればパーフェクトになるとことで、何百シート数のカスタマイズの依頼につながったそうなのです。双方とも、ストアがなければお互いの存在さえ知らずに終わっていたということで、双方ともに、満足な結果となりました。</p>
<p>ストアに掲載されているアプリの中には、Autodesk が提供するするものもあります。例えば、RevitやIFC、STF エクスポーターは、オープンソースをもとにしており、適宜、リリースされたRevitに含まれているものより新しいバージョンを入手することができます。DevTechのメンバーが作成したものもあります。例えば、File Upgrader はファイルをバッチ処理でアップデートするツール、Level Generator は複数のレベルをダイヤログ経由で一挙に作成するツールです。DevTechが作成したものは、すべてソースコードも提供しているので、自由にコードを変更して使用することも可能です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d41a90312970c-pi" style="display: inline;"><img alt="Store IFC Exporter" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017d41a90312970c image-full" src="/assets/image_708841.jpg" title="Store IFC Exporter" /></a></p>
<p>パートナーさんの作成した例をあげると、IMAGINiTのRoom Renumberが、Revitのストアが出てきた時の例の一つです。これは、部屋番号をマウスのクリックする順に並び替えたりする機能を持っています。</p>
<p>また、日本のデベロッパーさんであるGSA様がDetail Filter というツールを掲載されています。日本では「<a href="http://www.gsa-network.com/products/rev_cat/cathand.pdf" target="_self" title="cat hand GSA">猫の手ツール</a>」として販売されているツール群から好評のツールの一つを無料で提供しているそうです。英語版ではありますが、日本語バージョンのRevitでも作動します。以下にDetail Filter のスナップショットを示します。まだ、試されていない方、一度インストールして試されては、いかがでしょうか？もしも、気にいったら、GSAさんにコンタクトすると、日本語版を用意してくれるかもしれませんよ。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee91ca3c7970d-pi" style="display: inline;"><img alt="DetailFilter GSA" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017ee91ca3c7970d image-full" src="/assets/image_244096.jpg" title="DetailFilter GSA" /></a></p>
