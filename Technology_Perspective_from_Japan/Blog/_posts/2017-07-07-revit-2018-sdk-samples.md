---
layout: "post"
title: "Revit 2018 SDK 新規追加されたサンプルの解説"
date: "2017-07-07 01:19:48"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/07/revit-2018-sdk-samples.html "
typepad_basename: "revit-2018-sdk-samples"
typepad_status: "Publish"
---

<p>Revit 2018 SDK の最新版（ Update May 19, 2017 ）が Autodesk Developer Network のサイトにて公開されております。</p>
<p>Tools -&gt; Revit 2018 SDK (Update May 19, 2017) (msi - 355088Kb)<br /><a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=2484975">http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=2484975</a></p>
<p>この最新版には、Revit 2018 で新規に追加された DirectContext3D という API のサンプル「DuplicateGraphics」が同梱されております。</p>
<p>今回は、Revit 2017 と 2017.1、そして Revit 2018 で新規に追加されたサンプルの概要をご紹介いたします。</p>
<p>Revit 2017 と Revit 2017.1 SDK で追加されたサンプル</p>
<ul>
<li>Samples/CapitalizeAllTextNotes</li>
<li>Samples/GenericStructuralConnection</li>
<li>Samples/GeometryAPI/BRepBuilderExample</li>
<li>Samples/PlacementOptions</li>
<li>Structural Analysis SDK/Examples/CodeCheckingConcreteExample and CalculationPointsSelector</li>
<li>REX SDK/Samples/DRevitFreezeDrawing</li>
</ul>
<p>Revit 2018 SDK で追加されたサンプル</p>
<ul>
<li>MultistoryStairs</li>
<li>DuplicateGraphics</li>
</ul>
<p>下記に主要なサンプルの概要をご紹介いたします。</p>
<p><span style="font-size: 14pt;"><strong>CapitalizeAllTextNotes</strong></span><br />プロジェクトに配置されているすべての TextNotes オブジェクトのすべてのテキストを大文字に変換します。</p>
<ol>
<li>ドキュメント内にある TextNote インスタンスをすべて取得します。</li>
<li>フォーマットテキストを取得して、FormattedText.SetAllCapsStatus()メソッドを使用して、すべての文字列を大文字に変換します。</li>
</ol>
<p><span style="font-size: 14pt;"><strong>DuplicateGraphics</strong></span><br />Revit 2018 で新規に追加された DirectContext3D という API のサンプルです。<br />外部アプリケーションでは、DirectContext3D サーバーを作成します。このサーバーの実装では、選択されている Revit 要素からジオメトリを抽出して、頂点バッファとインデックスバッファのペアにエンコードし、DirectContext3D を使用して描画するという一連の処理を、Revit に登録します。<br />この処理は外部コマンドでトリガされ、オフセットされた位置に、選択された Revit 要素のジオメトリの複製を描画します。</p>
<p>※ただし、このサンプルは、Revit の要素すべてを完全に複製するサンプルではないため、一部要素は複製して描画されません。ご了承ください。</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09acb3b3970d-pi" style="display: inline;"><img alt="DirectContext3D" class="asset  asset-image at-xid-6a0167607c2431970b01bb09acb3b3970d img-responsive" src="/assets/image_981593.jpg" title="DirectContext3D" /></a><br /><br /></p>
<p><span style="font-size: 14pt;"><strong>GenericStructuralConnection</strong></span><br />一般、または詳細な構造接合の作成、読み取り、更新、削除などの基本的操作を行います。</p>
<ol>
<li>一般構造接合の作成、読み取り、更新、削除。</li>
<li>詳細な構造接合の作成、更新、コピー、プロパティマッチ、リセット。</li>
</ol>
<p><br /><span style="font-size: 14pt;"><strong>BRepBuilderExample</strong></span><br />Revit のジオメトリを生成するための BRepBuilder の使用方法に関するサンプルです。BRepBuilder で作成されたジオメトリは、DirectShape 要素にセットして、ビュー上に描画することができます。</p>
<p>単純なキューブ<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09acaecc970d-pi" style="display: inline;"><img alt="Brep1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09acaecc970d img-responsive" src="/assets/image_856655.jpg" title="Brep1" /></a></p>
<p>Nurbs サーフェス（開いたシェル）<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09acaecf970d-pi" style="display: inline;"><img alt="Brep2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09acaecf970d img-responsive" src="/assets/image_660527.jpg" title="Brep2" /></a></p>
<p>円柱や切断された円錐<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09acaed4970d-pi" style="display: inline;"><img alt="Brep3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09acaed4970d img-responsive" src="/assets/image_859617.jpg" title="Brep3" /></a><br /><br /><span style="font-size: 14pt;"><strong>MultistoryStairs</strong></span><br />複数階に連続する階段の編集ユーティリティのサンプルです。標準の階段コンポーネントから連続階段を作成し、選択したレベルに階段を追加したり、削除することができます。</p>
<ol>
<li>標準の階段コンポーネントから複数階の連続階段を作成します。</li>
<li>レベルを選択し、連続階段に階段を追加します。</li>
<li>レベルを選択し、連続階段から階段を削除します。</li>
</ol>
<p><span style="font-size: 14pt;"><strong>PlacementOptions</strong></span><br />ファミリインスタンスを配置する際のオプション設定に関するサンプルです。</p>
<ol>
<li>面ベースのファミリインスタンスを配置する際に、面、垂直面、作業面のオプションを設定します。</li>
<li>スケッチベースのファミリインスタンスを配置する際に、線分、円弧、またその他のスケッチオプションを設定します。</li>
</ol>
<p>新しい Revit 2018 SDK のサンプルをぜひお試しください。</p>
<p>By Ryuji Ogasawara</p>
