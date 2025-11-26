---
layout: "post"
title: "Design Graph"
date: "2016-08-01 20:21:44"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/08/design-graph.html "
typepad_basename: "design-graph"
typepad_status: "Publish"
---

<p>ベータ版としての扱いですが、A360 に <strong>Design Graph</strong> という新しい機能が加わりました。Design Graph は、<strong><a href="https://ja.wikipedia.org/wiki/%E6%A9%9F%E6%A2%B0%E5%AD%A6%E7%BF%92" target="_blank">機械学習</a></strong>&#0160;を利用した A360 Drive 内の検索を担うものです。</p>
<p>従来の検索機能では、設計者がデザイン ファイル内に明示的に付加した<strong><a href="https://ja.wikipedia.org/wiki/%E3%83%A1%E3%82%BF%E3%83%87%E3%83%BC%E3%82%BF" target="_blank">メタデータ</a></strong>（プロパティ、属性）を主な検索対象としていました。機械学習を導入することで、デザイン ファイル内のパーツ形状やジオメトリを利用してアセンブリやパーツの継承関係を学習することをゴールとして設定しています。</p>
<p>Design Graph へのアクセスは、A360 または A360 Drive にサインインした状態で、画面左上の <span style="color: #ffffff; background-color: #000000;">&#0160;<strong>A360 ｖ</strong>&#0160;</span> ドロップダウンから指定ることが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0925b4d2970d-pi" style="display: inline;"><img alt="Design_graph" class="asset  asset-image at-xid-6a0167607c2431970b01bb0925b4d2970d img-responsive" src="/assets/image_949704.jpg" title="Design_graph" /></a></p>
<ul>
<li>A360 Drive にデザイン ファイルがアップロードされていない場合には、Design Graph の選択肢が表示されない場合があります。なお、現在のところ、Design Graph ページは日本語化されていません。また、残念ながら検索対象となるキーワードに日本語を入力しても、正しく認識してくれません。</li>
</ul>
<p>Design Graph ページが表示されたら、まずはキーワードとしてパーツ名などを入力して、リターンキーで検索を実行してみてください。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d20c195a970c-pi" style="display: inline;"><img alt="Search" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d20c195a970c image-full img-responsive" src="/assets/image_348228.jpg" title="Search" /></a></p>
<ul>
<li>アップロードしたファイルのインデックス化を含む解析が完了していない際には、Design Graph の検索ボックスにキーワードを入力できない場合があります。少し時間をおいて再度ページを表示してみてください。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d20c1971970c-pi" style="display: inline;"><img alt="Not_available" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d20c1971970c image-full img-responsive" src="/assets/image_671254.jpg" title="Not_available" /></a></li>
</ul>
<p>Design Graph は、親子関係を持つ製造系デザイン データ内の件の検索を想定しています。英数字のパーツ番号やパーツ名を持つ Fusion 360 や Inventor、CATIA、SolidWorks などのアセンブリやパーツ ファイルが A360 Drive にアップロードされている場合には、パーツ番号やパーツ名などキーワードとすることで、それらを含むファイルを A360 Drive 内から横断的に見つけ出すことが出来ます。機械学習なので、この検索の繰り返しが、すなわち、適切な検索結果を出すための形状学習となります。</p>
<p>形状から対象を見出すと言う意味では、複数の写真から特定の人物の顔を特定してリストアップする作業に似ているかも知れません。検索結果となるパーツの表示ページには、<span style="background-color: #0696d7;"><span style="color: #ffffff;"> Find Similar Parts</span> </span>&#0160; （類似するパーツを検索） というボタンがあるはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c882650e970b-pi" style="display: inline;"><img alt="Find_similar" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c882650e970b image-full img-responsive" src="/assets/image_105856.jpg" title="Find_similar" /></a></p>
<p><span style="background-color: #0696d7;"><span style="color: #ffffff;"> Find Similar Parts</span> </span>&#0160; ボタンをクリックすると、類似度合いを含めたパーツがリストアップされるはずです。検索を繰り返して、リストアップされた中から当該パーツをクリックして画面に表示させていく過程で、形状検索の精度も向上していくはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8826746970b-pi" style="display: inline;"><img alt="Parts_similarity" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8826746970b image-full img-responsive" src="/assets/image_341819.jpg" title="Parts_similarity" /></a></p>
<p>機械学習（Machine Learning）や<strong><a href="https://ja.wikipedia.org/wiki/%E3%83%87%E3%82%A3%E3%83%BC%E3%83%97%E3%83%A9%E3%83%BC%E3%83%8B%E3%83%B3%E3%82%B0" target="_blank">深層学習</a></strong>（Deep Learning）といった人工知能のテクノロジも、設計/デザインの分野に応用され始めました。今後の応用範囲の拡大に期待したいところです。</p>
<p>By Toshiaki Isezaki&#0160;</p>
