---
layout: "post"
title: "Revit 2018 の新機能 その2"
date: "2017-04-28 02:00:13"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/04/new-features-on-revit-2018-part2.html "
typepad_basename: "new-features-on-revit-2018-part2"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2017/04/new-features-on-revit-2018-part1.html">前回</a>に引き続き、Revit 2018 の新機能について解説させて頂きます。今回は、建築設計分野の中でもジオメトリに関連する新機能をご紹介いたします。</p>
<p><strong>コーディネーション モデル</strong></p>
<p>他のソフトウェアで作成されたモデルとのコーディネーションを効率化するために、<strong>Navisworks® のファイルをリンクして、 Revit モデルの下敷参照図として使用できる</strong>ようになりました。 この機能により、設計の周辺環境の情報を提供したり、Revit モデルと Revit 以外のモデルを比較して調整することができます。下のビデオでは、右側のスタジアムがリンクされて参照表示されている NWD のモデルです。<br />日本語のビデオ解説は<a href="http://help.autodesk.com/view/RVT/2018/JPN/?guid=GUID-A5B877B0-F587-43F1-94C8-550CBBCB8A90">こちら</a>。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/6346r4EW-Xg?feature=oembed" width="500"></iframe></p>
<p class="asset-video">コーディネーション モデルは、プロジェクトの設計、施工前、施工中の全段階にわたり、専門分野間で仮想的に相互チェックする際に役立つ 3D 設計図です。更新されたコーディネーション モデルを定期的に(たとえば週に 1 度)ロードすると、干渉のリスクを最小限に抑えることができます。これにより、設計プロセスを高速化し、変更を加える手間を省いて、設計チーム全体の時間とコストを節約することができます。</p>
<p class="asset-video">たとえば、次のような用途が想定されます。</p>
<ul>
<li>Revit を使用している意匠設計者が、構造エンジニアが他のソフトウェアで設計した構造設計を Revit で確認するために、下敷参照配置し、建築要素との位置調整に利用する。</li>
<li>他の 3D モデラーを使用して複雑なシステムのカーテンウォールを使用した建物外部のエンベロープを設計し、意匠設計者がこのモデルを Revit に下敷参照配置することで、建物の内部設計がエンベロープ内に適切にフィットしていることを確認する。</li>
<li>土木設計者が別のソフトウェア製品を使用して詳細設計した外構を Revit に下敷参照配置し、敷地上の建物配置計画が正しいか確認する。</li>
</ul>
<p class="asset-video"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2791357970c-pi" style="display: inline;"><img alt="Revit2018_part2_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2791357970c image-full img-responsive" src="/assets/image_951061.jpg" title="Revit2018_part2_01" /></a></p>
<p>API では、Revit モデルのコンテキスト上に外部のグラフィックスを表示するための APIとして、新たに <strong>DirectContext3D</strong> という名前空間と各クラスが追加されました。</p>
<p>外部グラフィックスは、<strong>頂点バッファとインデックスバッファ</strong>のペアをエンコーディングして、Revit 上でジオメトリを表示します。</p>
<p>注：ただし、この外部グラフィックスは、ストリーミングのような方法で直接的にビュー上に描画されるため、<strong>Revit モデルのファミリ要素のジオメトリデータとして保存してモデリングに利用することはできません</strong>。</p>
<p>注：<strong>API では、Navisworks のファイルを読み込んで表示用のジオメトリデータを作成することはできません。</strong>DirectContext3D で表示するジオメトリは、デベロッパ様ご自身でコーディングして頂く必要があります。</p>
<p>Revit とこの API とのコミュニケーションには、<strong>External Service Framework (ESF)</strong> を使用しています。External Service Framework (ESF) は、Revit 2013 に導入されたデベロッパ向けのフレームワークです。<br />このフレームワークを利用することで、Revit の標準機能の動作を、従来の外部コマンドのアドインでは実現できないレベルで実装することができます。</p>
<p>DirectContext3D.IDirectContext3DServer インターフェースを実装し、Revit が予め用意している ExternalServices.BuiltInExternalServices.DirectContext3DService に実装した外部サーバーと関連付けることができます。</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d279662a970c-pi" style="display: inline;"><img alt="Revit2018_part2_03" class="asset  asset-image at-xid-6a0167607c2431970b01b8d279662a970c img-responsive" src="/assets/image_181981.jpg" style="width: 700px;" title="Revit2018_part2_03" /></a><br /><br /></p>
<p><strong>3D 形状の読み込み</strong></p>
<p><a href="http://adndevblog.typepad.com/technology_perspective/2016/11/revit-2017-update-1.html">Revit 2017.1 Upadate リリース</a>でもご紹介いたしましたが、一貫性に優れた高品質な 3D の読み込みを実現するための新技術の活用により、 <strong>SAT ファイルや Rhinoceros® ファイルから 3D ジオメトリを読み込める</strong>ようになりました。日本語のビデオ解説は<a href="http://help.autodesk.com/view/RVT/2018/JPN/?guid=GUID-FDEC83AA-6C4A-4ECE-800A-3CA4724C63E4">こちら</a>。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/R28IxFmqPSk?feature=oembed" width="500"></iframe></p>
<p>API では、<strong>ShapeImporter</strong> クラスが追加されました。他のフォーマット形式 (e.g. SAT and Rhino) のジオメトリ、マテリアル、グラフィックス スタイルを Revit のジオメトリオブジェクトに変換し、<strong>DirectShape オブジェクト</strong>としてプロジェクト上に配置することができます。</p>
<p>&#0160;</p>
<p><strong>読み込んだ 3D 形状のタグ付け</strong></p>
<p>Revit 2018 では、<strong>読み込んだ 3D 形状の要素にタグを付ける</strong>ことができるようになりました。 3D 形状の切り取りエッジに寸法を設定することができます。ファミリ内に読み込まれた 3D 形状で、ジオメトリ上に MEP コネクタを配置することができます。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/C5pERmxn4Wo?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>リンクされた DWG でグラフィック データを使用</strong></p>
<p>建築設計者と土木設計者は、多くの場合、さまざまな BIM 製品で管理されている複数のモデル間で地理的位置データの一貫性を保つ必要があります。 このワークフローを簡素化するため、<strong>DWG ファイルを Revit モデルにリンク</strong>することにより、そのファイルから<strong>グリッド座標を読み込んで Revit モデル内で使用</strong>できるようになりました。日本語のビデオ解説は<a href="http://help.autodesk.com/view/RVT/2018/JPN/?guid=GUID-0C6D8890-73D9-4DAC-A0A6-84C45E54C6A8">こちら</a>。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/Xij_oWzL1f4?feature=oembed" width="500"></iframe></p>
<p>API では、SiteLocation.GeoCoordinateSystemId という読み取り専用のプロパティが追加されました。このプロパティは、AutoCAD または Civil 3D の DWG ファイルからインポートした座標系がサポートされており、地理的な座標系の ID を取得できます。<br /><br /></p>
<p>Navisworks のモデルをコーディネーションできる機能や、外部の 3D 形状を読み込む機能など、ぜひお試しください。次回は、専門分野共通のコア機能に関する新機能についてご紹介いたします。</p>
<p>&#0160;By Ryuji Ogasawara</p>
