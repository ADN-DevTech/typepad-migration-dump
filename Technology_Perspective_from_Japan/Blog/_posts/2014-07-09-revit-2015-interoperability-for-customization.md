---
layout: "post"
title: "Revit 2015 のカスタマイズ互換性"
date: "2014-07-09 00:09:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/07/revit-2015-interoperability-for-customization.html "
typepad_basename: "revit-2015-interoperability-for-customization"
typepad_status: "Publish"
---

<p>従前と同様に、新しい Revit バージョンでは旧バージョン用に作成したアドイン アプリケーションやマクロとの互換性がありません。既存のモジュールを Revit 2015 で動作させるためには、移植作業が必要となります。今回は、開発環境も一新されていますので、まずは、準備が必要な新しい開発環境 について、ご紹介しましょう。</p>
<p><strong>Revit 2015 の開発環境</strong>&#0160;</p>
<ul>
<li>.NET Framework : .NET Framework 4.5</li>
<li>コンパイラ : Visual Studio 2012 Update 4</li>
<li>サポート OS：Windows 7（x86、ｘ64）、Windows 8（x64）<br />※ Windows XP、Vista と Windows 8（x86） はサポートなし</li>
</ul>
<p>移植作業では、最低でも上記開発環境でプロジェクトを開いて、Revit 2015 に含まれる Revit API アセンブリ、<strong>RevitAPI.dll</strong> と <strong>RevitAPIUI.dll</strong> を参照設定して、再ビルドする必要があります。Revit SDK やツールに関しては、<a href="http://adndevblog.typepad.com/technology_perspective/2014/06/localized-revit-api-document-revit-sdk.html" target="_blank"><strong>こちら</strong></a> をご参照ください。</p>
<p><strong>基本的な移植作業</strong></p>
<p>Revit 2015 では、クラスやメソッド、プロパティなど、前バージョンで旧式にマークされたものが利用できなくなっています。これらは、置き換えられた代替クラス、メソッド、プロパティに書き換える必要があります。</p>
<p>個々に列挙するには数が多すぎるので、詳細は、Revit 2015 SDK に含まれる RevitAPI.chm から、What&#39;s New 項を参照してみてください。</p>
<p><strong>新しく利用できる API</strong></p>
<p>ここでは、Revit 2015 になって利用できるようになった API &#0160;機能をいくつかご案内します。</p>
<p style="padding-left: 30px;"><strong>アクティブ グラフィカル ビュー</strong></p>
<p style="padding-left: 30px;">UIDocument.ActiveGraphicalView プロパティは、アクティブ ドキュメントのアクティブなビューを返します。UIDocument.ActiveView とは異なり、このプロパティは、プロジェクト ブラウザやシステム ブラウザのようなビューを返さないので、グラフィカル ビューだけを抽出した場合に便利です。</p>
<p style="padding-left: 30px;"><strong>既定の作成タイプ</strong></p>
<p style="padding-left: 30px;">PostCommand メソッドで Revit はカテゴリの要素の作成時に関連するコマンドが呼び出された際に、次のメソッド、プロパティで、ユーザ インタフェース上に表示される既定タイプを指定できるようになりました。</p>
<div style="padding-left: 30px;">
<ul>
<li>Document.GetDefaultFamilyTypeId(categoryId)：カテゴリの既定タイプを取得</li>
<li>Document.SetDefaultFamilyTypeId(categoryId, familyTypeId)：カテゴリの既定タイプを設定</li>
<li>Document.IsDefaultFamilyTypeIdValid(categoryId, familyTypeId)：既定タイプの有効性をチェック</li>
<li>ElementType.IsValidDefaultFamilyType(familyCategoryId)</li>
</ul>
</div>
<p style="text-align: center;">&#0160;<iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/sO7u8532uQA?feature=oembed" width="500"></iframe>&#0160;</p>
<p style="padding-left: 30px;"><strong>パラメータ</strong></p>
<p style="padding-left: 30px;">次のメソッドを利用することで、与えられたファミリのグループ内でパラメータ順序の変更が可能になっています。</p>
<ul>
<ul>
<li>FamilyManager.GetParameters()</li>
<li>Element.GetOrderedParameters()&#0160;</li>
<li>FamilyManager.ReorderParameters()：指定された入力に従ってファミリ内でファミリ パラメータの順序を再指定</li>
</ul>
</ul>
<p style="padding-left: 30px;"><strong>外部リソース サーバー API</strong></p>
<p style="padding-left: 30px;">キーノート データや Revit リンクのような外部ファイルに保存されてモデル内で利用されているリソース内容を、アプリケーションが Revit に提供する新しいサービスです。</p>
<ul>
<li>IExternalResourceServer：リンクされたファイルなど、アプリケーションが任意の場所から外部リソースを提供することを可能にする</li>
<li>ExternalResourceLoadContent：IExternalResourceServer がその LoadResource() メソッドから Revit 内で利用されるはずのデータを返すことを可能にする&#0160;</li>
<li>ExternalResourceLoadContext：外部リソースをロードするリクエストの実行コンテキストについての追加情報を提供する</li>
<li>ExternalResourceBrowserData：IExternalResourceServerが特定の場所から利用可能なリソースとサブフォルダをエミュレートすることを可能ぬする</li>
<li>ExternalResourceReference：IExternalResourceServer によって提供される外部リソースを識別する</li>
<li>ExternalResourceServiceUtils：外部リソース サービスに関連するユーティリティを含む</li>
<li>IExternalResourceUIServer：外部リソース サービスに関連する操作用のカスタム UI を提供する</li>
</ul>
<p style="padding-left: 30px;"><strong>&#0160;鉄筋の番号付け</strong></p>
<ul>
<li>NumberingSchema クラス：特定の種類と範囲がどのように鉄筋の番号付け/タグ付けに利用されるかを定義する</li>
<li>&#0160;NumberingSchemaType 列挙型：NumberingSchemaType は Revit で利用可能な番号付けスキーマのタイプをリストする</li>
</ul>
<p style="padding-left: 30px;"><strong>注釈機能（雲マーク）</strong></p>
<p style="padding-left: 30px;">Revit 2015 はプロジェクト注釈の設定と関連付けられた雲マークへアクセスする新しい API クラス メンバが導入されています。</p>
<p style="padding-left: 60px;"><strong>RevisionSettings クラス</strong></p>
<p style="padding-left: 60px;">注釈と雲マークに影響するプロジェクト全体の設定情報を含む</p>
<ul>
<ul>
<li>RevisionAlphabet：アルファべット順注釈の注釈番号パラメータの操作に使われる文字を決定する</li>
<li>RevisionCloudSpacing：プロジェクト内の雲マーク用グラフィックスのサイズを決定する</li>
<li>RevisionNumbering：注釈番号がシート毎かプロジェクト毎かを判断する</li>
</ul>
</ul>
<p style="padding-left: 60px;"><strong>新しい RevisionCloud クラス</strong></p>
<div>
<ul>
<ul>
<li>モデル内に表現されている雲マークと新しい雲マークの作成に関する情報にアクセス</li>
<li>RevisionCloud.Create()、 RevisionId、IsRevisionIssued() とGetSheetIds()</li>
<li>Element.Geometry は拡張され、雲マークを作成する実際のカーブラインを返す</li>
</ul>
</ul>
</div>
<div style="padding-left: 60px;">ViewSheet.GetRevisionCloudNumberOnSheet() はプロジェクト内の番号がシート毎に番号付けられた際に RevisionCloud の注釈番号へのアクセスを提供する</div>
<p style="padding-left: 60px;">RevisionCloud.GetSketchLines() はアプリケーションが雲マークのスケッチから線分を読み取ることを可能にする。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/sR9x3HfvNJk?feature=oembed" width="500"></iframe>&#0160;</p>
<p style="padding-left: 30px;"><strong>ネーミング ユーティリティ</strong></p>
<ul>
<li>NamingUtils.IsValidName()：入力値が Revit 内でオブジェクト名としての利用に妥当か識別したり、文字列中に禁止された文字用だけのルーチンチェックをおこなう</li>
<li>NamingUtils.CompareNames(string nameA, string nameB)：Revit 比較ルールに従って2つの名前を比較する</li>
</ul>
<div style="padding-left: 30px;"><strong>ダイナミック モデルアップデート</strong></div>
<div style="padding-left: 30px;">&#0160;</div>
<div style="padding-left: 30px;">
<p style="display: inline !important;">UpdaterRegister 上の変更はアップデータの一時的な</p>
有効化と無効化を可能にすることが出来ます。</div>
<div style="padding-left: 30px;">
<ul>
<li>UpdaterRegister.EnableUpdater()</li>
<li>UpdaterRegister.DisableUpdater()</li>
<li>UpdaterRegister.IsUpdaterEnabled(UpdaterId id)</li>
</ul>
</div>
<div style="padding-left: 30px;">
<p>変更に基づいてにアプリケーションが不要にアップデータが起動されることをコントロールできるようになります。</p>
<p><strong>マテリアル</strong></p>
<ul>
<li>Material.IsNameUnique(Document document, String name)</li>
<ul>
<li>マテリアル名がドキュメント内で一意かどうかチェックする</li>
<li>Material.Create() や Material.Duplicate() を介して新しいマテリアルを作成する前に名前の正当性の確認に使用</li>
</ul>
<li>Material.UseRenderAppearanceForShading</li>
<ul>
<li>シェードされたビューでマテリアルの外観が使われるかを決定</li>
</ul>
<li>CustomExporter.IsRenderingSupported()</li>
<ul>
<li>アプリケーションがレンダリングをサポートするライブラリが必要か、3D エクスポートがインストールされて利用できるかがテストすることを可能にする</li>
</ul>
</ul>
</div>
<div style="padding-left: 30px;"><strong>壁</strong></div>
<div style="padding-left: 30px;">&#0160;</div>
<div style="padding-left: 30px;">重ね壁の新しいメンバは重ね壁メンバについての情報の
<p style="display: inline !important;">サポートを提供します。</p>
<p style="display: inline !important;">&#0160;</p>
<ul>
<li>GetStackedWallMemberIds() は重ね壁に沿ったサブウォールを取得する（下部から上部の順）</li>
<li>IsStackedWall、IsStackedWallMember</li>
<li>StackedWallOwnerId – isStackedWallMember は ture ならオーナー重ね壁</li>
</ul>
Wall Function</div>
<div style="padding-left: 30px;">
<ul>
<li>WallType.Function – 壁タイプの Function プロパティへの読み込み/書き込みアクセス</li>
</ul>
</div>
<div>列挙にとどまってしまいますが、AutoCAD と異なり、残念ながら Revit はバージョンが変わるたびにドラスティックにクラスやメンバ書式が変わってしまします。地道な移植作業が必須です。特に旧式（Obsolute）にマークされたものが、なるべく早めに新しいものに置き換えておくことをお勧めします。</div>
<div>&#0160;</div>
<div>By Toshiaki Isezaki</div>
<div>&#0160;</div>
