---
layout: "post"
title: "Fusion 360 のメニューカスタマイズ ～ Quick Access Toolbar"
date: "2015-03-18 00:33:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/03/menu-customize-on-fusion-360-qat.html "
typepad_basename: "menu-customize-on-fusion-360-qat"
typepad_status: "Publish"
---

<p>Fusion 360 API では、JavaScript や Python でドキュメント内にボディやコンポーネント、アセンブリを構築、編集したり、パラメータ操作を自動化することが出来ます。以前の <a href="http://adndevblog.typepad.com/technology_perspective/2014/11/fusion-360-update-and-fusion-360-ultimate.html" target="_blank">ブログ記事</a> でもご紹介しているとおり、簡単なサンプルは、Fusion 360 のインストールと同時にインストールされるので、[Script Manager] ダイアログからいつでも実行して試すことが出来ます。また、簡単なサンプルを含む、Fusion 360 API の概要やオブジェクトモデル等は、オンライン ヘルプの <a href="http://fusion360.autodesk.com/resources" target="_blank"><strong>Programming Interface</strong>&#0160;章</a>にも詳しく解説されています。</p>
<p>ただ、現時点で Fusion 360 のメニューを API でカスタマイズする方法について、詳細に紹介されたヘルプ トピックがありません。ここでは、簡単に Fusion 360 のメニューを API で操作する方法をご案内しておきます。まずは、Fusion 360 上での名称を確認しましょう。</p>
<p>Fusion 360 の画面左には、<strong>Data Panel</strong> と呼ばれる領域があり、これが A360 上にプロジェクト ベースで作成されたフォルダ内容を表示します。画面上部には、<strong>Quick Access Toolbar</strong> と呼ばれるドッキングされたツールバーがあります。</p>
<p>モデリングをおこなう編集画面に目を移すと、まず、ドキュメント タイトルの下に <strong>Toolbar Panel</strong> と呼ばれるメニューバーが表示されます。この Toolbar Panel は、左端の切り替えボタンで切り替え可能な <strong>Workspace</strong> 毎に異なる内容を表示します。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7616ba7970b-pi" style="display: inline;"><img alt="Fusion_360_ui" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7616ba7970b image-full img-responsive" src="/assets/image_28115.jpg" title="Fusion_360_ui" /></a></p>
<p>最初に、Fusion 360 API で Quick Access Toolbar の項目をリストアップしてみます。 オブジェクト指向である Fusion 360 API には&#0160;<strong><a href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/images/Fusion.pdf" target="_blank">オブジェクト モデル</a></strong>&#0160;が用意されているので、すべての API アクセスの入口となる Application オブジェクトを取得してから、userInterface プロパティを介してユーザ インタフェースを現す UserInterface オブジェクトを取得します。こういった階層構造による作業は、Inventor API などの同じです。</p>
<p>Fusion 360 上のユーザ インタフェースは、すべて一意な ID（文字列）が与えられているので、この ID で取得したインタフェースを指定することが出来ます。Quick Access Toolbar &#0160;には、&quot;QAT&quot; という ID が与えられているので、これを指定して Toolbar オブジェクトを取得します。Quick Access Toolbar には、複数のボタンが配置されていますが、各要素にアクセスするには Toolbar.controls プロパティにアクセスして、それぞれのメニュー要素となる ToolbarControl オブジェクトを取得することになります。</p>
<p style="text-align: center;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0eaeab2970c-pi" style="display: inline;"><img alt="Qat" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0eaeab2970c img-responsive" src="/assets/image_209862.jpg" title="Qat" /></a>&#0160;</p>
<p>次の JavaScript コードは、Quick Access Toolbar に現在表示されているメニュー要素の ID を表示するものです。&#0160;</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="auto-style3" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td class="auto-style7" style="padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p>//Author-<br />//Description-<br />/*globals adsk*/<br />(function () {</p>
<p>&#0160;&#0160;&#0160;&#0160;&quot;use strict&quot;;<br /> <br /> &#0160;&#0160;&#0160;&#0160;if (adsk.debug === true) {<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;/*jslint debug: true*/<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;debugger;<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;/*jslint debug: false*/<br /> &#0160;&#0160;&#0160;&#0160;}<br /> <br /> &#0160;&#0160;&#0160;&#0160;var ui;<br /> &#0160;&#0160;&#0160;&#0160;try {<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;var app = adsk.core.Application.get();<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;ui = app.userInterface;<br /> <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;var qat = ui.toolbars.itemById(&#39;QAT&#39;);<br />&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;var toolbarCtrls = qat.controls;<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;var toolbarCtrl = null;<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;for(var index=0; index&lt;toolbarCtrls.count; index++)<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;{<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;toolbarCtrl = toolbarCtrls.item(index);<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;if(toolbarCtrl.isVisible)<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;{<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;console.log(toolbarCtrl.id);<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}<br />&#0160;&#0160;&#0160;&#0160;}<br /> &#0160;&#0160;&#0160;&#0160;catch (e) {<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;if (ui) {<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;ui.messageBox(&#39;Failed : &#39; + (e.description ? e.description : e));<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}<br /> &#0160;&#0160;&#0160;&#0160;}<br /> <br /> &#0160;&#0160;&#0160;&#0160;adsk.terminate();<br />}());</p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p>ID の出力には JavaScript の&#0160;<a href="https://developer.mozilla.org/ja/docs/Web/API/Console/log" target="_blank">console.log</a>&#0160;メソッドを利用しているので、Fusion 360 の [Script Manager] ダイアログから Debug モードでスクリプトを実行してください。Web ブラウザが起動したら、F12 キーを押してコンソール画面を表示させてると、次のように表示されるはずです。</p>
<p>&#0160;&#0160;&#0160;&#0160;DashboardModeCloseCommand<br />&#0160;&#0160;&#0160;&#0160;FileSubMenuCommand<br />&#0160;&#0160;&#0160;&#0160;PLM360SaveCommand<br />&#0160;&#0160;&#0160;&#0160;UndoDropDown<br />&#0160;&#0160;&#0160;&#0160;RedoDropDown<br />&#0160;&#0160;&#0160;&#0160;PublisherTechPreview</p>
<p>このコードを応用すると、既存のメニュー要素を消してしまうようなことも出来ます。具体的な内容については、次のドキュメントをご参照ください。&#0160;</p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c80f6ee6970b img-responsive"><a href="http://adndevblog.typepad.com/files/qa-9571.pdf" target="_blank">QA-9571 Share メニューを表示させないようにしたい（スクリプト）をダウンロード</a></span></strong></p>
<p>なお、API で変更した内容はセッション間のみ有効です。Fusion 360 を再起動すると初期化されてしまうので、注意が必要です。 もし、Fusion 360 の起動と同時に処理を自動実行させたい場合には、Script ではなく Add-In としてアプリケーションを実装して、&quot;Run on Startup&quot; を指定する必要があります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0807a8d3970d-pi" style="display: inline;"><img alt="Addin" class="asset  asset-image at-xid-6a0167607c2431970b01bb0807a8d3970d img-responsive" src="/assets/image_281000.jpg" title="Addin" /></a></p>
<p><strong>QA-9571 Share メニューを表示させないようにしたい<strong>（スクリプト）</strong>&#0160;</strong>の内容をAdd-In として実装したドキュメントも用意していますので、併せてご参照ください。</p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d1998f6f970c img-responsive"><a href="http://adndevblog.typepad.com/files/qa-9574.pdf" target="_blank">QA-9574 Share メニューを表示させないようにしたい（アドイン）</a></span></strong></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
