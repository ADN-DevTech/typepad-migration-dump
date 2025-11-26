---
layout: "post"
title: "Revit 2015 マススタディとコンセプトデザイン環境について"
date: "2015-02-27 01:06:21"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/02/revit-mass-study-1.html "
typepad_basename: "revit-mass-study-1"
typepad_status: "Publish"
---

<p>Revit 2015のマススタディ機能を利用すれば、建築設計の初期段階で行われるボリュームスタディと同じように、設計アイデアを3Dモデルで検討することができます。</p>
<p>マスには、プロジェクト内で作成する「インプレイスマス」と、プロジェクト外で作成する「ロード可能なマスファミリ」があります。</p>
<p style="padding-left: 30px;">インプレイスマス：プロジェクト固有のマス フォームに使用されます。<br />ロード可能なマス：プロジェクト内にマスの複数のインスタンスを配置する場合や、複数のプロジェクトでマスファミリを使用する場合に使用されます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7556948970b-pi" style="display: inline;"><img alt="Mass-study" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7556948970b img-responsive" src="/assets/image_290860.jpg" title="Mass-study" /></a></p>
<p>Revitのマススタディは、以下のような利点が考えられます。</p>
<p style="padding-left: 30px;">・スタディ模型の制作にかかるコストを削減することができる。<br />・作成したマスを使って、体積や建ぺい率などの規定への準拠を視覚的かつ数値的に検討できる。<br />・点、エッジ、サーフェスを直接操作したり、パラメトリックコンポーネントを分割サーフェスに適合させて複雑な形状のマスを作成することができる。<br />・要素のカテゴリ、タイプ、パラメータをコントロールして、マスインスタンスから床、屋根、カーテンシステム、壁を作成することができるため、シームレスに建築モデリングに移行できる。<br />・マスが変更されると、上述の建築要素の再作成を全面的に管理することができる。</p>
<p>マスを作成する際には、コンセプトデザイン環境を使用します。</p>
<p>建築設計におけるボリュームスタディでは、敷地条件と照らし合わせながらスタディを行うことが多いと想定されます。<br />したがってそのような場合は、Revitのプロジェクトに敷地データを作成し、そのプロジェクト内からコンセプトデザイン環境を表示して、インプレイスマスを作成することで、マススタディを行うことができます。</p>
<p style="padding-left: 30px;">1. [マス &amp; 外構]タブ [コンセプト マス]パネル (インプレイス マス)をクリックします。<br />2. インプレイス マス ファミリの名前を入力して[OK]をクリックします。アプリケーション ウィンドウには、コンセプト デザイン環境が表示されます。<br />3. [描画]パネルのツールを使用して目的の形状を作成します。<br />　　・フォーム<br />　　・プロファイル<br />　　・スケッチ<br />4. 終了したら、[マスを終了]をクリックします。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/mKyZhROdr4I?feature=oembed" width="500"></iframe>&#0160;</p>
<p>コンセプトデザイン環境を利用して、パラメトリックなマスファミリを、マスの分割されたサーフェスに適用するといったこともできます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c75569cd970b-pi" style="float: left;"><img alt="Parametric-component" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c75569cd970b img-responsive" height="179" src="/assets/image_540093.jpg" style="margin: 0px 5px 5px 0px;" title="Parametric-component" width="125" /></a> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0deb29e970c-pi" style="float: left;"><img alt="Parametric-surface" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0deb29e970c img-responsive" height="181" src="/assets/image_212820.jpg" style="margin: 0px 5px 5px 0px;" title="Parametric-surface" width="334" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>マス スタディ<br /><a href="http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-B8858693-F46D-4211-8CCC-B5E88681C466" target="_blank">http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-B8858693-F46D-4211-8CCC-B5E88681C466</a><br />コンセプト デザイン環境<br /><a href="http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-A3097388-C28F-4425-877A-406038BCD55F" target="_blank">http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-A3097388-C28F-4425-877A-406038BCD55F</a></p>
<p>フォームやスケッチは、Revit APIを利用してプログラムで作成することもできます。またフォームのサーフェスを分割して、その分割サーフェスに、任意の組み込みタイルパターンを適用したり、独自のマスファミリをロードすることもできます。詳細は以下の開発者ガイドを参照してみてください。</p>
<p>コンセプトデザインに関連するRevit APIのドキュメント<br /><a href="http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-15472D43-922D-43FF-960C-C23BD5760161" target="_blank">http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-15472D43-922D-43FF-960C-C23BD5760161</a></p>
<p>By Ryuji Ogasawara</p>
