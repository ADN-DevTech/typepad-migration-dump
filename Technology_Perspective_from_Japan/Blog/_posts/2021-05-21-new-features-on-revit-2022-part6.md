---
layout: "post"
title: "Revit 2022 の新機能 その6"
date: "2021-05-21 01:12:12"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/05/new-features-on-revit-2022-part6.html "
typepad_basename: "new-features-on-revit-2022-part6"
typepad_status: "Publish"
---

<p>Revit 2022 の新機能と改良された機能をシリーズでご紹介しております。</p>
<p>今回は、API の新機能と機能向上の内容となりますが、この記事内で全ての API をご紹介すると膨大になってしまうため、ここではその一部をご紹介させて頂きます。</p>
<p>なお、3月に実施された DevDays Online 2021 の Revit API のパートにて、網羅的に把握いただける動画が配信されております。 ADN に加入されている方は、専用サイトにて限定動画（英語）と日本語プレゼンテーション資料（PDF）をダウンロードしていただくことが出来ます。詳しくは、配信済の Autodesk Developer News - 2021年3月 をご確認ください。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/03/devdays-online-2021-summary.html">DevDays Online 2021 サマリー（一部公開）</a></li>
</ul>
<hr />
<p><strong>天井と床の API</strong></p>
<p>これまで長年にわたり、Revit API では天井（Ceiling オブジェクト）を作成する方法はサポートされておりませんでした。<br />そのため、Revit 2021 までは、スラブを作成する方法が回避策として提示されておりましたが、その要素は、Floor オブジェクトとなるため、UI 操作で作成できる天井とは大きなギャップがありました。</p>
<p>そして Revit 2022 では、ついに天井を作成する API がサポートされました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded2f840200c-pi" style="display: inline;"><img alt="Revit2022-06-01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bded2f840200c image-full img-responsive" src="/assets/image_265276.jpg" title="Revit2022-06-01" /></a></p>
<p>この変更により、スラブに関連する API は廃止予定となり、Floor.Create() メソッドに統一されますのでご注意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1034ac0200b-pi" style="display: inline;"><img alt="Revit2022-06-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1034ac0200b image-full img-responsive" src="/assets/image_309851.jpg" title="Revit2022-06-02" /></a></p>
<hr />
<p><strong>壁のプロファイルスケッチ</strong></p>
<p>次のメソッドで、プロファイル スケッチを追加および削除できるようになりました。</p>
<ul>
<li>Wall.CreateProfileSketch()</li>
<li>Wall.RemoveProfileSketch()</li>
</ul>
<p>Wall.CanHaveProfileSketch() は、壁がプロファイル スケッチをサポートしている場合に True を返します。円弧壁および楕円形の壁は、編集済みのプロファイルの設定をサポートしていない壁ジオメトリです。スケッチを追加すると、<strong>SketchEditScope</strong> を使用してプロファイル スケッチを編集できます。</p>
<hr />
<p><strong>スケッチ編集 API</strong></p>
<p>さらに、Revit 2022 では、天井、床、壁、開口部のスケッチを編集できる API が追加されました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802ae8b7200d-pi" style="display: inline;"><img alt="Revit2022-06-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802ae8b7200d image-full img-responsive" src="/assets/image_573606.jpg" title="Revit2022-06-03" /></a></p>
<p>次のプロパティを使用して、要素のスケッチの要素 ID を取得することができます。</p>
<ul>
<li>Ceiling.SketchId</li>
<li>Floor.SketchId</li>
<li>Opening.SketchId</li>
<li>Wall.SketchId</li>
</ul>
<p>指定したスケッチについて、そのスケッチを所有する要素(床、壁など)を Sketch.OwnerId プロパティで取得できます。</p>
<p>スケッチを編集するには、SketchEditScope クラスを使用します。</p>
<p>スケッチ編集セッションがアクティブな間は、スケッチ要素(曲線、寸法、参照面)を追加、削除、修正できます。<br />セッションが終了すると、スケッチベースの要素が更新されます。</p>
<p>主なメソッドは次のとおりです。</p>
<ul>
<li>SketchEditScope コンストラクタ
<ul>
<li>新しい SketchEditScope を作成します。</li>
</ul>
</li>
<li>Start() メソッド
<ul>
<li>特定のスケッチの編集を開始します。関連付けられたトランザクションも開始します。</li>
</ul>
</li>
<li>Commit() メソッド
<ul>
<li>変更をコミットします。</li>
</ul>
</li>
<li>IsSketchEditingSupported() メソッド
<ul>
<li>スケッチが SketchEditScope で編集できるかどうかを確認します。</li>
</ul>
</li>
</ul>
<p>サンプルコードは、<a href="https://help.autodesk.com/view/RVT/2022/JPN/?guid=Revit_API_Revit_API_Developers_Guide_Revit_Geometric_Elements_Sketching_html">こちらのページ</a>をご参照ください。</p>
<hr />
<p><strong>カラー凡例の作成 API</strong></p>
<p>新しい Autodesk.Revit.DB.ColorFillLegend では、特定のビューのカラー凡例要素のプロパティの読み取り、作成、および修正ができるようになりました。</p>
<p>ColorFillLegend.Create() メソッド を使用すると、ビューに新しいカラー凡例を作成できます。</p>
<ul>
<li>ColorFillLegend.Create(document, viewId, categoryId, origin)</li>
<li>ColorFillLegend.GetColumnWidths()</li>
<li>ColorFillLegend.SetColumnWidths(IList&lt;double&gt; widths)</li>
<li>ColorFillLegend.ColorFillCategoryId</li>
<li>ColorFillLegend.Height</li>
<li>ColorFillLegend.Origin</li>
</ul>
<hr />
<p><strong>カラースキーム API</strong></p>
<p>新しい Revit.DB.ColorFillScheme を通じて、平面図ビューと断面図ビューにカラー スキームを適用できるようになりました。Revit.DB.ColorFillSchemeEntry は、カラースキームのエントリを表します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802ae8c4200d-pi" style="display: inline;"><img alt="Revit2022-06-04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802ae8c4200d image-full img-responsive" src="/assets/image_789066.jpg" title="Revit2022-06-04" /></a></p>
<p>ビューの特定のカテゴリに紐づくカラースキーム ID を取得・設定するために下記のメソッドが追加されています。</p>
<ul>
<li>View.Get/SetColorFillSchemeId()</li>
</ul>
<p>サンプルコードは、<a href="https://help.autodesk.com/view/RVT/2022/JPN/?guid=Revit_API_Revit_API_Developers_Guide_Revit_Geometric_Elements_Annotation_Elements_Color_Fill_html">こちらのページ、</a>及び、 Revit 2022 SDK に追加された「ColorFill サンプル」をご参照ください。</p>
<hr />
<p><strong>クラウドモデル API</strong></p>
<p><span style="text-decoration: underline;">ワークシェアされているクラウドモデルの初期化をサポート</span></p>
<p>現在のモデルを BIM 360 にクラウド モデルとして保存し、ワークシェアされているローカル ファイルを Revit Cloud Worksharing の中央モデルとして BIM 360 Design にアップロードする操作をサポートしました。</p>
<ul>
<li>Document.SaveAsCloudModel()&#0160;</li>
</ul>
<p><span style="text-decoration: underline;">クラウドモデルへの Revit リンク</span></p>
<p>既存のリンクメソッドがクラウドモデルをサポートするようになりました。</p>
<ul>
<li>RevitLinkType.Create(Document, ModelPath, RevitLinkOptions)</li>
<li>RevitLinkType.LoadFrom(ModelPath, WorksetConfiguration)</li>
<li>ModelPathUtils.ConvertCloudGUIDsToCloudPath()&#0160;</li>
</ul>
<p><span style="text-decoration: underline;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded2fa08200c-pi" style="display: inline;"><img alt="Revit2022-06-05" class="asset  asset-image at-xid-6a0167607c2431970b026bded2fa08200c img-responsive" src="/assets/image_264727.jpg" title="Revit2022-06-05" /></a><br /></span></p>
<p><span style="text-decoration: underline;">モデルの Forge ID を取得する</span></p>
<p>次のメソッドを使用して、クラウド モデルの Forge ID を取得できるようになりました。</p>
<ul>
<li>Document.GetHubId()
<ul>
<li>モデルが配置されている場所の ForgeDM ハブ ID</li>
</ul>
</li>
<li>Document.GetProjectId()
<ul>
<li>モデルが配置されている場所の ForgeDM プロジェクト ID</li>
</ul>
</li>
<li>Document.GetCloudFolderId(bool forceRefresh)
<ul>
<li>モデルが配置されている場所の ForgeDM フォルダ ID</li>
</ul>
</li>
<li>Document.GetCloudModelUrn()
<ul>
<li>モデルを特定する ForgeDM Urn。クラウド モデルではないドキュメントの場合は空の文字列を返します。</li>
</ul>
</li>
</ul>
<p>クラウドモデル API の詳細は、<a href="https://help.autodesk.com/view/RVT/2022/JPN/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Application_and_Document_CloudFiles_html">こちらのページ</a>、及び、Revit 2022 SDK に追加された「CloudAPISample」というサンプルをご参照ください。</p>
<hr />
<p>By Ryuji Ogasawara</p>
