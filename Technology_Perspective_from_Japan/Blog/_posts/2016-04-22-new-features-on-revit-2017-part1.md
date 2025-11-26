---
layout: "post"
title: "Revit 2017 の新機能 その1"
date: "2016-04-22 01:57:41"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/04/new-features-on-revit-2017-part1.html "
typepad_basename: "new-features-on-revit-2017-part1"
typepad_status: "Publish"
---

<div>先日、Revit 2017 がリリースされました。</div>
<div>今回から複数回にわたって、2016 R2 及び、2017 の新機能と更新内容、API の対応状況をご紹介していきます。今回は、Revit の各専門分野に共通の新機能について解説致します。</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>サブスクリプションメンバー向けの機能をすべての Revit 2017 のお客様が利用可能になりました</strong></span></div>
<div>従来、Revit 2016 R2 リリース時にオートデスク サブスクリプションメンバーのみが利用可能であった新機能と拡張機能を、Revit 2017 のすべてのお客様にご利用いただけるようになりました。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08e70f3a970d-pi" style="display: inline;"><img alt="Design-construction-software-banner-1504x623" class="asset  asset-image at-xid-6a0167607c2431970b01bb08e70f3a970d img-responsive" src="/assets/image_161692.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Design-construction-software-banner-1504x623" /></a></div>
<div>&#0160;</div>
<div>
<div><span style="font-size: 14pt;"><strong>グローバル パラメータ</strong></span></div>
<div>Revit 2016 R2 から、グローバル パラメータの機能が追加されました。</div>
<div>グローバル パラメータは、ファミリのパラメトリックな特性をプロジェクト環境に応用したもので、プロジェクト全体で共通して利用できる新しいパラメータです。</div>
<div>&#0160;</div>
<div>グローバル パラメータを使用すると、モデルの寸法や拘束の値を制御したり、要素インスタンスのプロパティに関連付けて値を決定したり、寸法の値をリアルタイムに通知(レポート)できます。これらの値を他のグローバル パラメータの計算式で使用することもできます。</div>
<div>プロジェクト全体に適用されるパラメータを使用することで、設計者の設計意図をパラメータとして保持することができます。</div>
<div>&#0160;</div>
<ul>
<li>寸法または拘束の値を制御する</li>
<li>要素インスタンスまたはタイプ プロパティに関連付けて値を制御する</li>
<li>インスタンス パラメータまたはタイプ プロジェクト パラメータに関連付ける</li>
<li>この値を他のグローバル パラメータの計算式で使用できるように、寸法の値を取得して通知する</li>
</ul>
<div>&#0160;</div>
<div>グローバル パラメータにはさまざまな使用方法がありますが、ここではいくつかの例を紹介します。</div>
<p style="padding-left: 30px;"><strong>隣接していない均等</strong></p>
<p style="padding-left: 30px;">モデルを拘束する均等寸法の使用は非常に便利ですが、同じスペースが隣接する状況に限られています。グローバル パラメータを使用すると、隣接していない複数の寸法に同じ値を割り当てることができます。<br /><br /></p>
<p style="padding-left: 30px;"><strong>別の要素のサイズを使用して要素の位置を設定する</strong></p>
<div style="padding-left: 30px;">位置合わせと寸法ロックを使用すると、別の要素のサイズに基づいて要素を配置することができますが、グローバル パラメータを使用すると作業が簡単になります。たとえば、床の下に梁を配置するのが難しい場合があります。梁が床と同じレベルに参照されている場合、それぞれの梁をスラブの厚さでオフセットする必要があります。床を変更する場合は、それぞれの梁の更新が必要になります。グローバル パラメータを使用すると、この調整を自動的に行うことができます。レポートの寸法を作成し、梁のオフセット値をそのレポート パラメータで制御するように設定します。これで床を変更するときには、梁が自動的に調整されます。</div>
<div>&#0160;</div>
<div style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/Sb82V0rmANs?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>API では、GlobalParameterManager クラス、GlobalParameter クラスが新規に追加されました。またGlobalParameter&#0160;は、ParameterValue クラスの値を保持します。<br /><br /></div>
<div><strong>&#0160;</strong></div>
<div><span style="font-size: 14pt;"><strong>文字注記</strong></span></div>
<div>Revit 2017 では、テキスト エディタで文字注記をキャンバスに直接追加できるようになりました。編集中、文字は表示されている通りのフォントとスタイルで編集することができます。他にも、テキスト エディタは、次のように改善されています。</div>
<div>&#0160;</div>
<ul>
<li>リボンにある新しいコンテキスト文字編集ツールを使用してすばやくアクセスすることができます。</li>
<li>編集中にすべての文字が表示されます。</li>
<li>期待通りに文字を納まり、スケール、スクロールできます。</li>
<li>文字注記を編集しながらズームや画面移動を行えます。文字のラッピングはズーム スケールの影響を受けません。</li>
<li>複数のレベル、インデント、番号を持つリスト、箇条書きを作成できます。</li>
<li>注釈を編集する際、テキスト エディタを指定して、不透明な背景や罫線を表示できるようになりました。このオプションを使用すると、複雑な図面上に入力する際に文字が見やすくなります。</li>
</ul>
<div>&#0160;</div>
<div style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/L2NAdl4Z_9Q?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/b7BlT6dv68s?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>API では、TextNode から、FormattedText オブジェクトを取得して、テキストの読み/書きとテキストノートのフォーマットを操作することができるようになりました。また TextEditorOptions クラスでは、テキスト エディタのアピアランスと機能を制御することができるようになりました。</div>
<div><strong>&#0160;</strong></div>
<div><span style="font-size: 14pt;"><strong>ファミリ エディタ</strong></span></div>
<div>Revit 2016 R2 では、&#0160;ファミリ表示のプレビュー機能が追加されました。詳細レベル、表示パラメータ設定、ビュー タイプについて、ファミリ エディタでのファミリ ジオメトリの表現の表示が改善されました。プロジェクトにファミリのジオメトリを繰り返しロードしなくても、ジオメトリを作成、テスト、編集できます。</div>
<div>&#0160;</div>
<div>表示されないジオメトリをフィルタで除外することにより、設計意図がわかりやすくなります。ファミリをプロジェクトにロードする前に、ジオメトリをプレビューして調整することにより、迅速に作業を進めることができます。特定の要素に絞る場合もそうでない場合も、プレビュー表示ウィンドウから表示を確認することができます。</div>
<div><strong>&#0160;</strong></div>
<div><span style="font-size: 14pt;"><strong>Revit リンク</strong></span></div>
<div>Revit 2016 R2 では、Revit リンク機能に対するご要望の多かった機能が追加されました。 ホスト モデルで Revit リンクを使用する際のワークフローと生産性が向上します。</div>
<div>&#0160;</div>
<ul>
<li>Revit リンクを挿入するときに、新しい配置オプション[自動 - プロジェクトの基点をプロジェクトの基準点へ]を使用できるようになりました。このオプションは、モデルのプロジェクト基準点を挿入点として使用してリンク ファイルを配置し、ホスト モデルのプロジェクト基準点に位置合わせします。</li>
<li>Revit リンクの挿入後は[プロジェクト基準点に再配置]と[内部基準点に再配置]の 2 つのオプションを使用してリンクの位置を変更できます。</li>
</ul>
<div>&#0160;</div>
<div>API では、RevitLinkInstance.MoveBasePointToHostBasePoint() メソッド、RevitLinkInstance.MoveOriginToHostOrigin() メソッドが追加されました。</div>
<div>&#0160;</div>
<ul>
<li>ワークシェアされているモデルのローカル コピーで作業している場合は、[ロード解除]コマンドで 2 つのオプション、 [すべてのユーザを対象とする]及び[現在のユーザを対象とする] を使用できます。また、Revit リンクをロード解除すると、パフォーマンスが向上し、メモリ使用率が改善することがあります。&#0160;</li>
</ul>
<div>&#0160;</div>
<div>API では、RevitLinkType.UnloadLocally() メソッド、RevitLinkType.RevertLocalUnloadStatus() メソッドが追加されました。</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>パフォーマンス</strong></span></div>
<div>Revit 2016 R2 及び、2017 では、ソフトウェア パフォーマンスが改善されました。</div>
<div>&#0160;</div>
<ul>
<li>ビューが開くまでの時間を短縮するため、[オプション]ダイアログの[グラフィックス]タブに[表示されている要素のみを描画]という新しい設定が追加されました。この機能は、オクルージョン カリングと呼ばれます。この機能をオンにすると、Revit は非表示の要素を描画しません。パフォーマンスの向上は、非透過要素を多数含む 3D ビューの場合に最も顕著になります。</li>
<li>塗り潰しはバックグラウンドで処理されるため、ビューを更新しながらモデルでの作業を継続して行えます。Revit は複数の CPU を使用しながら部屋、スペース、HVAC(暖房換気空調)ゾーン、ダクト、配管の塗り潰しを更新するため、これらの計算によりモデルに生じる遅延が軽減されます。</li>
<li>複数の DWF または DWFx ファイルにビュー/シートを書き出すときの時間を大幅に短縮するために、複数の RevitWorker プロセスを使用するようになりました。</li>
<li>バックグラウンド処理は、Revit アプリケーションのバックグラウンドで実行されるプロセスです。実行中のバックグラウンド処理のリストを表示するには、ステータスバーを使用します。この処理はRevit 2016 から導入され、RevitWorker という名称の別のプロセスとして実行されます。</li>
</ul>
<div>&#0160;</div>
<div style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/_FUyy0nNjyA?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>接線のロック</strong></span></div>
<div>Revit 2017 では、 ファミリ エディタでスケッチする際、モデル線分、シンボル線分、参照線、スケッチ線に接線のロックを配置できるようになりました。 この改善により、接線の関係を駆動するために、複雑な計算式やパラメトリックな関連付けを事前に設定する必要がなくなりました。</div>
<div>&#0160;</div>
<div style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/VANuYPhqpv0?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>次回は、建築分野の機能強化についてご紹介いたします。</div>
<div>&#0160;</div>
<div>By Ryuji Ogasawara</div>
</div>
<div>&#0160;</div>
