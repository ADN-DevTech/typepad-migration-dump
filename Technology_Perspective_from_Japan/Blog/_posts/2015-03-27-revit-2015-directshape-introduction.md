---
layout: "post"
title: "Revit 2015 ソリッドやメッシュのジオメトリを高速に作図する"
date: "2015-03-27 03:10:37"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/03/revit-2015-directshape-introduction.html "
typepad_basename: "revit-2015-directshape-introduction"
typepad_status: "Publish"
---

<p>Revit 2015では、ドキュメント上にジオメトリを作成して描画する方法として、コンセプトデザイン環境からインプレイスマスでフォームを作成する方法と、ファミリインスタンスを作成する方法があります。これらはAPIを通じて実装することもできますが、どちらの方法でも、FamilyItemFactoryクラスのメソッドを利用してファミリインスタンスを作成することが基本となります。</p>
<p>ただしAPIからファミリを作成する場合、一定規模のジオメトリを作図しようとすると、作図処理に時間がかかってしまうという問題あります。これは、ジオメトリに対して、ファミリインスタンスとして利用するための機能の生成処理に時間を要するためです。</p>
<p>そこで、単純にジオメトリを作図したい場合は、ファミリを新規に作成するのではなく、ジオメトリ要素をドキュメント上で直接作図するためのDirectShapeクラスを利用することで、作図速度を改善することができます。ただし、DirectShapeオブジェクトは、ファミリインスタンスとして利用することはできませんので、用途に応じて使い分ける必要があります。<br /> <br />DirectShapeクラスは、Revit 2015から追加されたAPIで、これは外部で作成されたジオメトリ要素をストアするためのクラスです。<br />主な使用方法は、IFCなどの他のデータフォーマットから形状をインポートすることですが、ジオメトリ要素を保存する入れ物として利用できます。また作成したDirectShapeオブジェクトは、カテゴリに割り当てられます。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/ON7FlDLsMV4?feature=oembed" width="500"></iframe>&#0160;</p>
<p>DirectShapeオブジェクトにストアできるジオメトリ要素は、IListを引数としてとり、TessellatedShapeBuilderクラスを使用して作成することができます。また、GeometryCreationUtilitiesクラスで作成したSolidオブジェクト等も渡すことができます。<br /> <br />TessellatedShapeBuilderは、TessellatedFaceオブジェクトで作成された複数の平面からソリッド、ポリメッシュを作成するためのビルダークラスです。TessellatedFaceオブジェクトは、Faceオブジェクトのように個別にマテリアルを指定することができます。</p>
<p>DirectShapeクラスを応用して、OBJ形式のファイルを読み込んでDirectShepオブジェクトに変換し、ドキュメント上に配置するアドインについて、以下のブログで紹介されております。<br />英語になってしまいますが、ご興味のある方はご確認ください。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f60e83970c-pi" style="display: inline;"><img alt="Revit-directshape-obj" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f60e83970c img-responsive" src="/assets/image_858397.jpg" title="Revit-directshape-obj" /></a></p>
<p>From Hack to App – OBJ Mesh Import to DirectShape<br /><a href="http://thebuildingcoder.typepad.com/blog/2015/02/from-hack-to-app-obj-mesh-import-to-directshape.html" target="_blank">http://thebuildingcoder.typepad.com/blog/2015/02/from-hack-to-app-obj-mesh-import-to-directshape.html</a></p>
<p>DirectShapeクラス、TessellatedShapeBuilderクラスについては、SDKフォルダ直下にあるRevit Platform API Changes and Additions.docxのImport APIに概要が記載されております。</p>
<p><br />またサンプルコードは以下のAutodesk Technical Q&amp;A に掲載しております。<br />ご興味のある方は、ご確認ください。</p>
<p>QA-9589　ソリッドやメッシュのジオメトリを高速に作図する方法<br /><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=9589" target="_blank">http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=9589</a></p>
<p>By Ryuji Ogasawara</p>
