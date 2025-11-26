---
layout: "post"
title: "Fusion 360 アドイン作成"
date: "2015-03-16 02:14:01"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/03/fusion-360-addin-creation.html "
typepad_basename: "fusion-360-addin-creation"
typepad_status: "Publish"
---

<p style="text-align: left;">Fusion 360 が更新されて、新しいバージョンが利用できるようになりました。新しいバージョンをインストールするには、Fusion 360 を起動時に自動的に表示される次のダイアログから [Update Now] ボタンをクリックするだけです。自動的に正しいバージョンをダウンロードしてインストールしてくれます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c763a4bf970b-pi" style="display: inline;"><img alt="Update_Fusion_360" class="asset  asset-image at-xid-6a0167607c2431970b01b7c763a4bf970b img-responsive" src="/assets/image_618971.jpg" title="Update_Fusion_360" /></a></p>
<p>このバージョンのもっとも大きな機能追加に、アドイン（Add-In）機能の実装があります。昨年後半には、すでにスクリプト（Script）を使った API として、JavaScript と Python をサポートし始めましたが、Script を実行する場合には、Fusion 実行中に手動で実行したい Script を直接指定する必要がありました。</p>
<p>今回登場した Add-In では、Fusion 360 のインストール フォルダ内にある場所から、マニフェストを利用したモジュールの自動ロードと実行が可能になる点が&#0160;Script と異なります。利用できる開発言語は、現在のところ、Script と同じ JavaScript と Python の2 種類です。</p>
<p>新しく Add-In を作成する場合の手順は、Script と似ています。まず、画面上部にある Quick Access Toolbar の [File] メニューで [Scripts and Add-Ins] 項目を選択します。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c763a51d970b-pi" style="display: inline;"><img alt="Qat" class="asset  asset-image at-xid-6a0167607c2431970b01b7c763a51d970b img-responsive" src="/assets/image_226196.jpg" title="Qat" /></a></p>
<p>[Scripts and Add-Ins] ダイアログが開いたら、[Add-Ins] タブをアクティブにして、[Creatie] ボタンをクリックすると [Create New Script or Add-In] ダイアログが表示されます。Add-In の名称や使用する開発言語を選択して、再度 [Create] ボタンをクリックします。この際、作成した Add-In を Fusion 360 の起動時に自動実行させるかを &quot;Run on Startup&quot; チェックボックスで指定することも出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c763a538970b-pi" style="display: inline;"><img alt="Addin creation" class="asset  asset-image at-xid-6a0167607c2431970b01b7c763a538970b img-responsive" src="/assets/image_349777.jpg" style="width: 350px;" title="Addin creation" /></a></p>
<p>&quot;Run on Startup&quot; は、Add-In 作成時だけでなく、実行時にも指定することが出来ます。ただし、動作を確認するためには、Fusion 360 を再起動する必要があります。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c763a469970b-pi" style="display: inline;"><img alt="Addin" class="asset  asset-image at-xid-6a0167607c2431970b01b7c763a469970b img-responsive" src="/assets/image_239373.jpg" title="Addin" /></a>&#0160;</p>
<p style="text-align: left;">作成が終了すると、コードの作成/編集が可能になります。[Scripts and Add-Ins] ダイアログ上で作成した直後の Add-In 名を選択してから、[Edit] ボタンをクリックします。JavaScript か Python 別に異なる編集画面が起動します。この違いは、以前、ご紹介した Script の編集と同じ手順です。</p>
<p style="text-align: left; padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2014/09/fusion-360-api.html" target="_blank">Fusion 360 と API Tech Preview</a></p>
<p style="text-align: left; padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2014/11/fusion-360-update-and-fusion-360-ultimate.html" target="_blank">Fusion 360 の更新と Fusion 360 Ultimate</a></p>
<p style="text-align: left;">さて、作成されたスケルトンコードには、Run と Stop の 2 つの関数が登録されています。簡単に説明してしまうと、Run がこの Add-In がロードされた際に実行される関数で、Stop は Add-In がロード解除された際に実行される関数です。&quot;Run on Startup&quot; を指定した場合には、Run が Fusion 360 の起動時に自動実行されるのは言うまでもありません。&#0160;</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="auto-style3" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td class="auto-style7" style="padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p><span style="font-size: 10pt;">//Author-</span><br /><span style="font-size: 10pt;">//Description-</span><br /><br /><span style="font-size: 10pt;"> function <strong>run</strong>(context) {</span></p>
<p><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&quot;use strict&quot;;</span><br /><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;if (adsk.debug === true) {</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; /*jslint debug: true*/</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; debugger;</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; /*jslint debug:&#0160;false*/</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;}</span><br /><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;var ui;</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;try {</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;var app = adsk.core.Application.get();</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ui = app.userInterface;</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ui.messageBox(&#39;Hello addin&#39;);</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;}</span><br /><span style="font-size: 10pt;"> &#0160;&#0160;&#0160;&#0160;catch (e) {</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if (ui) {</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ui.messageBox(&#39;Failed : &#39; + (e.description ? e.description : e));</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;}</span><br /><span style="font-size: 10pt;"> }</span><br /><br /><span style="font-size: 10pt;"> function <strong>stop</strong>(context) {</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;var ui;</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;try {</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;var app = adsk.core.Application.get();</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;ui = app.userInterface;</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;ui.messageBox(&#39;Stop addin&#39;);</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;}</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160; catch (e) {</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if (ui) {</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ui.messageBox(&#39;Failed : &#39; + (e.description ? e.description : e));</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}</span><br /><span style="font-size: 10pt;"> &#0160;&#0160;&#0160;&#0160;}</span><br /><span style="font-size: 10pt;"> }</span></p>
</td>
</tr>
</tbody>
</table>
<p style="text-align: left;">&#0160;</p>
<p style="text-align: left;">自動実行などの特性から、ユーザ インタフェースのカスタマイズで、この機能を利用すれば有用かも知れません。</p>
<p style="text-align: left;">By Toshiaki Isezaki</p>
<p style="text-align: left;">&#0160;</p>
