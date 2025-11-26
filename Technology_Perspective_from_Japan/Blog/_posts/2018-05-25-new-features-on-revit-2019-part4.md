---
layout: "post"
title: "Revit 2019 の新機能 その4"
date: "2018-05-25 01:45:21"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/05/new-features-on-revit-2019-part4.html "
typepad_basename: "new-features-on-revit-2019-part4"
typepad_status: "Publish"
---

<p>Revit 2019 の新機能についてご紹介しております。今回は、構造設計分野の新機能、更新内容、API の対応状況を解説いたします。</p>
<p><span style="font-size: 14pt;"><strong>鉄骨構造および接合</strong></span></p>
<p>鉄骨の接合部をモデル化して設計できるようになりました。これにより、ドキュメント化と製造の詳細レベルを向上させることができます。<br />一連の新しい構造ツールでは、次の機能を実行できます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df356c6c200b-pi" style="display: inline;"><img alt="Revit 2019 Part4-1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0224df356c6c200b image-full img-responsive" src="/assets/image_560823.jpg" title="Revit 2019 Part4-1" /></a></p>
<p><strong>構造接合</strong></p>
<p>旧 Steel Connections for Revit アドインが、Revit に含まれるようになりました。<br /> この機能により、Revit ライブラリに 130 の鉄骨接合が追加されました。</p>
<p style="padding-left: 30px;"><a href="http://help.autodesk.com/view/RVT/2019/JPN/?guid=GUID-FA242186-6216-40BA-9DC9-C2A347BE244D">標準鉄骨接合のタイプ一覧</a></p>
<p><br /> <strong>カスタム接合</strong></p>
<p>鉄骨パーツと標準接合を組み合わせて、カスタム接合タイプを定義します。<br /> <br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03c6b70200d-pi" style="display: inline;"><img alt="GUID-A54AFAA6-A537-4692-810E-64928E16B40E" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0224e03c6b70200d image-full img-responsive" src="/assets/image_956655.jpg" title="GUID-A54AFAA6-A537-4692-810E-64928E16B40E" /></a></p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/HUOWtNgJv5o?feature=oembed" width="500"></iframe></p>
<p class="asset-video">API では下記のメソッド・プロパティが追加されました。</p>
<ul>
<li>StructuralConnectionHandler.<strong>Create</strong><strong>(Document, </strong><strong>IList</strong><strong>&lt;</strong><strong>ElementId</strong><strong>&gt;, String)</strong></li>
</ul>
<p style="padding-left: 60px;">指定した構造部材と鉄骨接合部材からカスタムの StructuralConnectionHandler と Type を作成します。</p>
<ul>
<li>StructuralConnectionHandlerType.<strong>AddElementsToCustomConnection</strong><strong>()</strong><br /> StructuralConnectionHandlerType.<strong>RemoveMainSubelementsFromCustomConnection() </strong></li>
</ul>
<p style="padding-left: 60px;">カスタムの接合における鉄骨接合要素の追加と削除をサポートします。</p>
<ul>
<li>StructuralConnectionHandler.<strong>IsCustom</strong><strong><br /> </strong>StructuralConnectionHandlerType.<strong>IsCustom</strong><strong><br /> </strong>StructuralConnectionHandlerType.<strong>IsDetailed</strong></li>
</ul>
<p style="padding-left: 60px;">鉄骨接合ハンドラに関する情報にアクセスします。</p>
<p class="asset-video"><br /> <strong>鉄骨製造要素</strong></p>
<p class="asset-video">プレート、ボルト、アンカー、頭付スタッド、穴、および溶接を要素としてモデルに配置できます。</p>
<p class="asset-video"><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df356bad200b-pi" style="float: left;"><img alt="Revit 2019 Part4-4" class="asset  asset-image at-xid-6a0167607c2431970b0224df356bad200b img-responsive" height="275" src="/assets/image_247033.jpg" style="margin: 0px 5px 5px 0px;" title="Revit 2019 Part4-4" width="192" /></a>　　<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03c6bb8200d-pi" style="display: inline;"><img alt="GUID-8C011FA0-01E1-472F-A7CF-7201DBE13DF2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0224e03c6bb8200d img-responsive" src="/assets/image_113890.jpg" title="GUID-8C011FA0-01E1-472F-A7CF-7201DBE13DF2" /></a><br /><br /></p>
<p class="asset-video">API では、様々な Revit 要素に鉄骨製造の情報を付与できるようになりました。</p>
<p style="padding-left: 30px;"><strong>Steel.SteelElementProperties クラス</strong></p>
<ul>
<li>UniqueID <br /> GetFabricationUniqueID()</li>
</ul>
<p style="padding-left: 60px;">Advance Steel API のなかで鉄骨コア要素と一致するかどうか判別するために使用するユニークな製作IDを取得します。</p>
<ul>
<li>AddFabricationInformationForRevitElements()</li>
</ul>
<p style="padding-left: 60px;">Revit 要素に製作情報を追加します。</p>
<ul>
<li>GetReference()</li>
</ul>
<p style="padding-left: 60px;">製作ID を使用している Revit 要素を探します。</p>
<p style="padding-left: 60px;">Reference は要素またはサブ要素となります。</p>
<p class="asset-video"><strong>鉄骨要素切断ツール</strong></p>
<p class="asset-video">プレートおよび鉄骨要素を修正ツールで修正し、接合部の要素のジオメトリをより正確に適合させます。<br /> <br /> <strong>鉄骨パラメトリック切断ツール</strong></p>
<p class="asset-video">結合された要素と接合部に対応するために、カスタム パラメトリック切断を鉄骨要素に追加できるようになりました。</p>
<p class="asset-video"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c84dc237200c-pi" style="display: inline;"><img alt="Revit 2019 Part4-5" class="asset  asset-image at-xid-6a0167607c2431970b0223c84dc237200c img-responsive" src="/assets/image_940376.jpg" title="Revit 2019 Part4-5" /></a><br /> <br /> <strong>詳細な鉄骨モデリング用 API</strong></p>
<p class="asset-video">Detailed Steel Modeling for Revit 専用の API を使用して、一般的な接合、既存のまたは新しい標準鉄骨接合、カスタム接合だけでなく、個々の鉄骨要素を作成できるようになりました。</p>
<p class="asset-video"><br /><span style="font-size: 14pt;"><strong>Structural Precast for Revit の機能拡張</strong></span></p>
<p class="asset-video"><strong>プレキャスト格子梁埋込型スラブの自動化</strong></p>
<p>梁埋込型スラブが、プレキャスト自動化プロセスの一部となりました。既存の要素と似たような環境設定ルールとワークフローに従っています。<br /> Structural Precast Extension for Revit 2019 の今後のリリースで使用できます。<br /><br /></p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/Q5OD2PBbX4M?feature=oembed" width="500"></iframe><br /> <br /> <strong>プレキャスト自動化用 API</strong><br /> Precast for Revit 専用 API を使用して、プレキャスト要素の施工図の作成および修正、プレキャスト要素設定用サード パーティ アドインの統合、プレキャスト要素番号のカスタマイズを行えるようになりました。Structural Precast Extension for Revit 2019 の今後のリリースで使用できます。</p>
<p style="padding-left: 30px;"><a href="http://help.autodesk.com/view/RVT/2019/JPN/?guid=GUID-079D5ED9-AC40-4DB7-9951-B0C6C1D81BFF">Structural Precast for Revit の API について </a></p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>鉄骨要素を交換する</strong></span></p>
<p>Advance Steel Extension for Revit を使用して、Revit と Advance Steel 間で、鉄骨プレート、ボルト、アンカー、溶接部、接合部を交換できるようになりました。</p>
<p><span style="font-size: 14pt;"><strong>フリー フォーム鉄筋形状の照合</strong></span></p>
<p>ドキュメント内の鉄筋の集計および注釈を改善するために、フリー フォーム鉄筋では既存の鉄筋形状ファミリを照合するまたは鉄筋ジオメトリから新しい鉄筋形状ファミリを作成できるようになりました。</p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/RX4KTRVNEXY?feature=oembed" width="500"></iframe></p>
<p>次回は、MEP 設計分野の新機能と更新内容、API の対応状況についてご紹介いたします。</p>
<p>By Ryuji Ogasawara</p>
