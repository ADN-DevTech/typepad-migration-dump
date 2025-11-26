---
layout: "post"
title: "AutoCAD アドオン開発者のためのRevit API入門～アクセス"
date: "2014-03-04 20:38:06"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/03/understanding-revit-api-for-autocad-addon-developers-part5.html "
typepad_basename: "understanding-revit-api-for-autocad-addon-developers-part5"
typepad_status: "Publish"
---

<p>今回は、Revit API を使って Revit データにアクセスする際の方法や考え方について、AutoCAD API と比較してご紹介していきます。デスクトップ製品をカスタマイズするために API として、概念自体に大きな違いはありませんが、実施に利用するクラスはメソッド、プロパティは全くの別物であることを認識してください。それでは、順にご案内しましょう。</p>
<p><strong>オブジェクト識別子</strong></p>
<p>AutoCAD API と同じように Revit API でも要素（オブジェクト）を識別するための&#0160;<strong>識別子</strong>&#0160;が存在します。Revit API では、通常、ElementId クラスを用いて、ファミリ インスタンスを含む各要素を一意に識別します。ElementId は 1 つのプロジェクト ファイル内では一意ですが、複数にプロジェクト ファイルを同時に扱う場合には、重複する可能性があります。</p>
<p>要素に割り当てられた ElementId は、ワークセットを利用した場合や、Revit をバージョン アップした際に、古いバージョンの Revit で作成したプロジェクト ファイルを開いた際に実行されるファイル マイグレーションで変化することがあります。外部のデータベースと連携したアプリケーションなど、常に一意に識別を維持した場合は、Element.UniqueId プロパティが返す識別子（文字列）を利用すべきです。</p>
<p><strong>Application/UIApplication と Document/UIDocument</strong></p>
<p>Revit API では、ユーザ インタフェースから可能なデータアクセスと、すべてのデータアクセスが、系統としてクラスとして区別されています。少々ニュアンスは異なりますが、Document と UIDocument で説明するばら、AutoCAD API の Database（.NET API）/AcDbDatabase（ObjectARX）と Document（.NET API）/AcApDocument（ObectARX）と考えるといいかも知れません。UIDocument クラスで特徴的なのは、ユーザが要素を選択する際に利用する Selection プロパティです。</p>
<p>アドインを定義する際に 必ず Execute メソッドを実装しますが、通常、パラメータとして渡される ExternalCommandData オブジェクトから、次のようなかたちで 4 つのオブジェクトを取得して、すぐに利用できるようにするのが一般的です。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="auto-style3" style="mso-cellspacing: 0mm; background: #F2F2F2; mso-shading: windowtext; mso-pattern: gray-5 auto; mso-yfti-tbllook: 1184; mso-table-lspace: 7.1pt; margin-left: 4.85pt; mso-table-rspace: 7.1pt; margin-right: 4.85pt; mso-table-anchor-vertical: margin; mso-table-anchor-horizontal: margin; mso-table-left: left; mso-table-top: 26.3pt; mso-padding-alt: 0mm 5.4pt 0mm 5.4pt; width: 100%;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p style="text-align: left; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly; font-family: &#39;Courier New&#39;; font-size: small; overflow: auto;">public class Command : IExternalCommand<br /> {<br />&#0160; &#0160;public Result Execute( ExternalCommandData commandData,<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ref string message,<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ElementSet elements)<br />&#0160; &#0160;{<br />&#0160; &#0160; &#0160; <strong>UIApplication</strong> uiapp = commandData.Application;<br />&#0160; &#0160; &#0160; <strong>UIDocument</strong> uidoc = uiapp.ActiveUIDocument;<br />&#0160; &#0160; &#0160; <strong>Application</strong> app = uiapp.Application;<br />&#0160; &#0160; &#0160; <strong>Document</strong> doc = uidoc.Document;<br /> <br />&#0160; &#0160;}<br /> }</p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p><strong>モデリング/作図空間としてのコンテナ オブジェクトと作図メソッド</strong></p>
<p>AutoCAD では、作図空間としてモデル空間やペーパー空間（レイアウト）が用意されています。また、ユーザ定義ブロックも、API 上では作図空間として位置づけられています。いずれの空間も .NET API では BlockTableRecord クラス、ObjectARX では AcDbBlockTableRecord クラスとして用意されています。Revit ではどうでしょう。作図空間にオブジェクトを追加する際には、メモリ上に構築したグラフィカル&#0160;オブジェクトを追加する際には BlockTableRecord.AppendEntity メソッド（.NET API）やAcDbBlockTableRecord::appendAcDbEntity() メンバ関数（ObjectARX）を利用すれば、どんなグラフィカル オブジェクトでも追加できるシンプルさを持っています。</p>
<p>Revit API 上ではどうかと言うと、残念ながら、AutoCAD とは全く異なる考え方を持っています。要素（オブジェクト）を追加して保存する先は、Document オブジェクトで、AutoCAD API で言う Database（.NET API）/AcDbDatabase（ObjectARX）で、考え方は同じです。ただし、空間という考え方は存在しません。また、ファミリ カテゴリ毎の異なる配置ルールを含めて考えていく必要があります。</p>
<p>例えば、新しいファミリ インスタンスを作成する場合には、Document クラスの Create プロパティを静的に利用して NewFamilyInstance メソッドを呼び出す必要があります。この&#0160;NewFamilyInstance メソッドには、用途に合わせて 12 個の<a href="http://ja.wikipedia.org/wiki/%E5%A4%9A%E9%87%8D%E5%AE%9A%E7%BE%A9" target="_blank">オーバーロード</a> メソッドが用意されています。つまり、作成しようとするファミリ インスタンスが、どのような振る舞いで、どの要素にホストされるのかを、あらかじめ把握して利用する必要があるのです。具体的には、窓は壁にホストされますが、床にはホストされません。これを NewFamilyInsyance メソッドで表現するには、ホストとなる要素（床）をパラメータとして与えられるオーバーロードを利用する必要があるわけです。</p>
<p>また、システム ファミリを作成する場合には、利用するメソッドが変わります。例えば、床の作成には Document.NewFloor メソッドを、フットプリント屋根の作成には Document.NewFootPrintRoof メソッドを、それぞれ利用します。壁の場合はどうかと言うと、NewWall メソッドというメソッドはなく、Wall クラスを静的に利用した Wall.Create メソッドを利用する必要があります。Wall.Create メソッドも、用途に合わせて 5 個のオーバーロード メソッドが用意されています。</p>
<p>把握するのがなかなか難しいので、実際には、Revit SDK 内に用意された膨大なサンプル プロジェクトから、具体的な利用方法を探していく作業がどうしても必要になってしまいます。この厄介さを回避する目的で、バージョンアップ時にクラスやメソッド、プロパティの統廃合や変更を続けている、という側面もあるのも事実です。</p>
<p><strong>ドキュメントの単位</strong></p>
<p>Revit で扱うプロジェクト ファイル（.rvt）やファミリ ファイル（.rfa）内部が持つ長さ単位は、基本的にフィート単位です。このため、Revit API で各種情報にアクセスする場合には、取得した値をメートルに変換したり、設定する値をフィートに変換する処理が必要になります。</p>
<p>これを変換するために、Revit API には&#0160;UnitUtils クラスが用意されています。このクラスに含まれるメソッドを使って、各種内部単位をミリメートルやメートルに相互変換することが出来ます。&#0160;</p>
<table rules="all" style="margin-left: auto; margin-right: auto;">
<tbody>
<tr><th bgcolor="DodgerBlue"><span style="font-size: 10pt; color: #ffffff;">基本単位</span></th><th bgcolor="DodgerBlue"><span style="font-size: 10pt; color: #ffffff;">Revit の単位</span></th><th bgcolor="DodgerBlue"><span style="font-size: 10pt; color: #ffffff;">単位系</span></th></tr>
<tr>
<td bgcolor="Cyan"><span style="font-size: 10pt;">長さ</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">フィート(ft)</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">インチ/フィート単位</span></td>
</tr>
<tr>
<td bgcolor="Cyan"><span style="font-size: 10pt;">角度</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">ラジアン</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">メートル単位</span></td>
</tr>
<tr>
<td bgcolor="Cyan"><span style="font-size: 10pt;">マス</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">キログラム(kg)</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">メートル単位</span></td>
</tr>
<tr>
<td bgcolor="Cyan"><span style="font-size: 10pt;">時刻</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">秒(s)</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">メートル単位</span></td>
</tr>
<tr>
<td bgcolor="Cyan"><span style="font-size: 10pt;">電流</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">アンペア(A)</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">メートル単位</span></td>
</tr>
<tr>
<td bgcolor="Cyan"><span style="font-size: 10pt;">温度</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">ケルビン(K)</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">メートル単位</span></td>
</tr>
<tr>
<td bgcolor="Cyan"><span style="font-size: 10pt;">光度</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">カンデラ(cd)</span></td>
<td bgcolor="Cyan"><span style="font-size: 10pt;">メートル単位</span></td>
</tr>
</tbody>
</table>
<p style="text-align: left;"><strong style="text-align: left;">オブジェクトの選択</strong></p>
<p>Selection.PickObject メソッドでユーザに要素を 1 つさせることが出来ます。また、Selection.PickObjects メソッドでは、複数の要素を選択させることが出来ます。Selection.PickElementsByRectangle メソッドでは、矩形領域内の要素を複数選択することも出来ます。同様に、Selection.PickPoint メソッドで座標を指示させることも可能です。</p>
<p>AutoCAD と違って、交差選択や窓選択、ラバーバンドといった処理を意図的に設定するオプションはありません。ただし、PickPoint メソッド利用時に有効なオブジェクト スナップを指定することは出来ます。</p>
<p><strong>フィルタリング</strong></p>
<p>ファミリ インスタンスやファミリ、その他、非ファミリ要素が多数存在するプロジェクト ファイルでは、効率的に目的とする要素を取得する方法が提供されています。その方法で代表的なのが、&#0160;FilteredElementCollector クラスの利用です。AutoCAD で例えるなら、実際にオブジェクト選択をせずに、フィルタリング機能を使って選択セットを作成するようなものと考えてください。</p>
<p>FilteredElementCollector クラスは、<a href="http://ja.wikipedia.org/wiki/%E7%B5%B1%E5%90%88%E8%A8%80%E8%AA%9E%E3%82%AF%E3%82%A8%E3%83%AA" target="_blank">LINQ</a>&#0160;クエリーとともに利用することも出来ます。</p>
<p><strong>シンボル名の注意</strong></p>
<p>プロジェクト ファイル内のファミリ シンボル、要素などに名前（シンボル名）が付けられています。その名前を使ったフィルタリングをおこなうと、目的の要素を簡単に取得することが出来ます。Revit API を習得する場面では、このシンボル名が原因で、実行時エラーに遭遇することがあります。原因は、Revit SDK に含まれるサンプルの多くが、英語版の Revit で作られたプロジェクト ファイルやファミリを想定しているためです。</p>
<p>例えば、壁を作成する際に指定するレベル名が、サンプルでは &quot;Level 1&quot; となっています。このようなサンプルを日本語テンプレートから作成したプロジェクト ファイルを開いて実行すると、レベル名が &quot;レベル 1&quot; となっているため、例外が発生します。この問題は、ビュー名や参照面名など当てはまります。このため、可能な場合には、言語に依存しないように BuiltInParameter&#0160;列挙値を使ってパラメータから取得、利用するようにすることが推奨されています。&#0160;</p>
<p><strong>イベント</strong></p>
<p>AutoCAD API と同じように、ドキュメント関連やデータベース関連のイベント処理をすることが出来ます。ただし、AutoCAD ほど多彩でシステム レベルのイベントは用意されていません。例えば、 AutoCAD API では、作図領域上のマウス カーソルの動きを&#0160;Editor.PointMonitor （.NET API）や&#0160;AcEdInputPointMonitor::monitorInputPoint()（ObjectARX）でモニタして座標値をチェックしたり、その座標値を使って、マウス カーソル下にあるオブジェクトの情報を取得するといった処理実装が出来ますが、Revit API では、Revit 自身が BIM モデルの中で一定程度の拘束を処理するため、このような処理の実装はサポートされていません。</p>
<p>また、AutoCAD では、データベース イベントを使って、あるオブジェクトの変更を別のオブジェクトに反映するような処理も実装できます。非常に柔軟な処理が可能なのですが、一方で、無限ループを誘発させてしまう危険性も合わせ持っています。Revit API では、この危険性を回避しながら、同様の実装を可能にする <strong>ダイナミック モデル アップデータ</strong> と呼ばれる特別なイベント処理が提供されています。 これを利用することで、壁長を変更した際に、配置された窓を自動的に壁長の中央に配置するといった処理を安全に実装することが出来るようになります。</p>
<p><strong>拡張ストレージ</strong></p>
<p>IFC レベルにも、BIM モデルとして Revit 上にもないカスタム情報を要素に付加したい場合、AutoCAD の拡張エンティティ データや拡張ディクショナリに相当する 拡張ストレージ という機能を利用することが出来ます。拡張ストレージの利用時には、あらかじめスキーマを使って付加する情報タイプを定義しておく必要があります。</p>
<p><strong>Revit Loopup ツール</strong></p>
<p>API を使って開発やデバッグをしていく際には、Revit プロジェクト ファイル（.rvt）やファミリ ファイル（.rfa）上の様々な要素の情報を参照したい場面が多くあるはずです。こんな時には、Revit SDK で提供されている Revit Lookup ツールを利用してみてください。Revit Lookup ツールは、.NET Framework が提供する<a href="http://ja.wikipedia.org/wiki/%E3%83%AA%E3%83%95%E3%83%AC%E3%82%AF%E3%82%B7%E3%83%A7%E3%83%B3_(%E6%83%85%E5%A0%B1%E5%B7%A5%E5%AD%A6)" target="_blank"> リフレクション</a> を使って、Revit API のクラス名やプロパティ名で、各種情報を表示する機能を提供します。</p>
<p>Revit Lookup ツールは、C:\Revit SDK 2014\RevitLookup\CS フォルダに Visual Srudio プロジェクトとして提供されているので、ビルド後に同じフォルダにある RevitLookup.addin アドイン マニフェストを適切に修正して、Revit が認識するパスに保存することで、外部アプリケーションとして登録されたリボン パネルから利用できます。</p>
<p>特に、要素を選択してから [Snoop Current Selection...] を使うと、その要素が持つ情報を API レベルで参照することが出来ます。また、[Snoop DB...] を使えば、特定の要素を選択することなく、プロジェクト内の情報をマウスクリックしながら走査していくことが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b042e32c3970d-pi" style="display: inline;"><img alt="Revit_Lookup" class="asset  asset-image at-xid-6a0167607c2431970b019b042e32c3970d" src="/assets/image_667573.jpg" title="Revit_Lookup" /></a></p>
<p>例えば、下図のように、植栽のファミリ インスタンスを選択後に、&#0160;[Snoop Current Selection...] コマンドをクリックすると、[Snoop Objects] ダイアログにファミリ インスタンス（FamilyInstance クラス）が持つアクセスを閲覧することが出来ます。このダイアログ上で Category 部分をクリックすると、別のダイアログが開き、FamilyInstance.Category プロパティにアクセスして得られる情報を表示します。Name を参照することで、このカテゴリが「植栽」であることがわかります（Category.Name プロパティ相当）。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b042e339f970d-pi" style="display: inline;"><img alt="Revit_Lookup_category" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b042e339f970d image-full img-responsive" src="/assets/image_103001.jpg" title="Revit_Lookup_category" /></a></p>
<p>Revit Lookup ツールを利用することで、Revit API でアクセスできる範囲を想定することが出来ると考えて差し支えありません。&#0160;</p>
<p><strong>コマンド</strong></p>
<p>Revit に標準で組み込まれているコマンドを、すべて同じように Revit API で模倣することが出来ない場合があります。例えば、Revit API を使って天井を自動作図することは、現在できません（API が公開されていない）。このよう場面では、UIApplication.PostCommand メソッドを使って、PostableCommand 列挙値として定義されている&#0160;Revit 標準コマンドを指定することで、ユーザ インタフェースから起動するのと同じようにコマンドを実行させることが出来ます。次のコードは、独自に定義した外部コマンド内部から [天井] コマンドを呼び出すものです。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="auto-style3" style="mso-cellspacing: 0mm; background: #F2F2F2; mso-shading: windowtext; mso-pattern: gray-5 auto; mso-yfti-tbllook: 1184; mso-table-lspace: 7.1pt; margin-left: 4.85pt; mso-table-rspace: 7.1pt; margin-right: 4.85pt; mso-table-anchor-vertical: margin; mso-table-anchor-horizontal: margin; mso-table-left: left; mso-table-top: 26.3pt; mso-padding-alt: 0mm 5.4pt 0mm 5.4pt; width: 100%;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p style="text-align: left; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly; font-family: &#39;Courier New&#39;; font-size: small; overflow: auto;">public class Command : IExternalCommand<br /> {<br />&#0160; &#0160;public Result Execute( ExternalCommandData commandData,<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ref string message,<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ElementSet elements)<br />&#0160; &#0160;{<br />&#0160; &#0160; &#0160; UIApplication uiapp = commandData.Application;<br />&#0160; &#0160; &#0160; UIDocument uidoc = uiapp.ActiveUIDocument;<br />&#0160; &#0160; &#0160; Application app = uiapp.Application;<br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">&#0160; &#0160; &#0160; Document doc = uidoc.Document;</span></p>
<p><strong><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">&#0160; &#0160; &#0160;&#0160;RevitCommandId id = <br />&#0160; &#0160; &#0160;&#0160;RevitCommandId.LookupPostableCommandId(<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;PostableCommand.AutomaticCeiling);</span></strong><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;"><strong> &#0160; &#0160; &#0160;&#0160;uiapp.PostCommand(id);</strong><br /><br /></span><span style="font-family: &#39;courier new&#39;, courier;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; return Result.Succeeded;</span></span></p>
<p><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">&#0160; &#0160;</span><span style="font-size: 10pt; font-family: &#39;courier new&#39;, courier;">}</span></p>
<p style="text-align: left; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly; font-family: &#39;Courier New&#39;; font-size: small; overflow: auto;">}</p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p>なお、AutoCAD のようにコマンド オプションをあらかじめ指定したりすることは出来ません。コマンドを起動することが出来るのみです。</p>
<p><strong>まとめ</strong>&#0160;</p>
<p>Revit の持つ製品としてのルールや、BIM モデルが持っているルール（拘束、パラメータなど）は、当然、AutoCAD には存在しないものです。AutoCAD は、カスタマイズ プラットフォームとして様々な業種、業態で利用できるよう、このようなルールは最低限なものしかありません。これが、AutoCAD と Revit の最も大きな違いです。どのカスタマイズでも言えることですが、製品が持つ「文化」を重視しないと、製品の特徴や操作性を損なう結果になってしまいます。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
