---
layout: "post"
title: "AutoCAD 2014 JavaScript APIと既存APIとの連携開発環境"
date: "2013-07-22 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/07/autocad-2014-javascript-api_development_preps.html "
typepad_basename: "autocad-2014-javascript-api_development_preps"
typepad_status: "Publish"
---

<p>他のブログ記事でもご紹介させていただいておりますように、AutoCAD 2014のAPI群に新たに加わったJavaScript API を皆様は大変興味を持っていただいて貰っている一方で、「自分で連携機能を評価したいが、何を準備すれば良いか？」「開発環境の費用負担は？」といった疑問を抱かれておられると思います。<br />
今までのObjectARXや.Net Managed API にはVisual Studio 2010/2012 製品が必要のように、AutoCAD 2014 製品でJavaScript APIを利用した評価や開発を行うためには、環境の用意が必要となります。<br />
今回は、連携動作の評価を目的として「評価版や無償版」を中心に開発環境を用意する情報を掲載させていただきます。</p>

<p>尚」、以下のブログ情報は　Autodesk -Technical <a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=8262">Q&A- 8262 「AutoCAD 2014 JavaScript API を使う時の開発環境は何ですか?」</a>に、Visual Studio Express 2012製品情報と共に参考にしていただく開発ツール情報(「Firefox 22.0」ブラウザ と「FireBug1.11.4」アドイン や Netbeans IDE 7.3 エディタ) と一緒に <span class="asset  asset-generic at-xid-6a0167607c2431970b01901e618fb2970b"><a href="http://adndevblog.typepad.com/files/autocad2014_javaapi_development_preps.pdf">AutoCAD2014_JavaAPI_Development_Preps.pdf</a></span> としてポストされておりますのでダウンロードしていただきご利用ください。</p>

<p>１．	開発環境のインストール</p>

<p>JavaScript関連のIDEやデバック環境は、ObjectARXやdotNetManaged カスタムコマンド開発時のC++, .Netなどの開発環境（Visual Studio製品ソフト一つ）とは異なり、オープンリソース環境であるがゆえに、それぞれ細分化して存在しておりアプリケーション開発者自身が必要なソフトを探し出して用意する事が求められています。</p>

<p>今回は、共にMicrosoft社より評価版（登録義務あり）としてリリースされているJavaScriptとhtmlソースコード作成用のVisual Studio Express 2012 for Webと、.NetカスタムコマンドとJavaScript API環境を連携させるための .NET側のカスタムコマンド作成用のVisual Studio Express for WindowsDesktopをPCに用意する（インストールの）ご案内となります。</p>

<p><br />
AutoCAD 2014 JavaScript APIと連携動作の評価＆開発に必要なソフト及びツール。</p>

<p>(1).  AutoCAD 2014日本語製品<br />
　　JavaScript APIが動作可能なAutoCAD製品プラットフォーム<br />
(2).  Visual Studio Express 2012 for Web<br />
　　JavaSctipt API を使ったjsファイルとhtmlファイル内のソースコードの開発プラット<br />
　　フォーム<br />
(3).  Visual Studio Express 2012 for Windows Desktop<br />
　　.Netカスタムコマンド内からJavaScript APIカスタムコマンドと連携させる為の<br />
　　カスタムコマンドを作成する時の開発プラットフォーム<br />
(4).  AutoCAD2014 dotNet Wizard<br />
　　.Netカスタムコマンド内からJavaScript API カスタムコマンドと連携させる為の<br />
　　カスタムコマンドを作成する際に用いるコマンドスケルト作成用Wizard<br />
(5).  ObjectARX 2014 SDK<br />
　　.Netカスタムコマンド作成時のAutoCADマネージアセンブリライブラリ<br />
　　(AcCoreMgd.dll,AcCoreMgd.dll, AcMgd.dll) の指定に用います。</p>

<p>------------------------------------------------------------------<br />
(1).  AutoCAD 2014日本語製品<br />
　　ご利用になられるAutoCAD2014日本語製品のインストール。</p>

<p>(2).  Visual Studio Express 2012 for Web<br />
(3).  Visual Studio Express 2012 for Windows Desktop<br />
　　JavaSctipt API を使ったjsファイルとhtmlファイル内のソースコードの開発プラット<br />
　　フォーム と.Netカスタムコマンド内からJavaScript API処理と連携させる為のカスタム<br />
　　コマンドを作成する時の開発プラットフォーム。<br />
　　以下よりインストールできます。<br />
　　<a href="http://www.microsoft.com/visualstudio/jpn#downloads+d-2012-express">http://www.microsoft.com/visualstudio/jpn#downloads+d-2012-express</a><br />
　　<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac20c578970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac20c578970d" alt="8262Fig-1" title="8262Fig-1" src="/assets/image_224138.jpg" /></a><br /><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac20c77f970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac20c77f970d" alt="8262Fig-2" title="8262Fig-2" src="/assets/image_2565.jpg" /></a><br />　　</p>

<p>(4).  AutoCAD2014 dotNet Wizard<br />
　　.Netカスタムコマンド内からJavaScript API 処理と連携させる為のカスタムコマンドを<br />
　　作成する際に用いる.NETカスタムコマンドスケルト作成用Wizardは、以下よりダウン<br />
　　ロードしインストールできます。<br />
　　<a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&id=1911627">http://usa.autodesk.com/adsk/servlet/index?siteID=123112&id=1911627</a><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191045778f2970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0191045778f2970c" alt="8262Fig-3" title="8262Fig-3" src="/assets/image_209257.jpg" /></a><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e618087970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01901e618087970b" alt="8262Fig-4" title="8262Fig-4" src="/assets/image_8778.jpg" /></a><br /><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019104577b9e970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b019104577b9e970c" alt="8262Fig-5" title="8262Fig-5" src="/assets/image_408932.jpg" /></a><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e618188970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01901e618188970b" alt="8262Fig-6" title="8262Fig-6" src="/assets/image_656784.jpg" /></a><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac20cd22970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac20cd22970d" alt="8262Fig-7" title="8262Fig-7" src="/assets/image_863429.jpg" /></a><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e6183ab970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01901e6183ab970b" alt="8262Fig-8" title="8262Fig-8" src="/assets/image_990883.jpg" /></a><br />
   <br />
(5).  ObjectARX 2014 SDK<br />
　　.Netカスタムコマンド作成時のAutoCADマネージアセンブリライブラリ<br />
　　　　(AcCoreMgd.dll, AcCoreMgd.dll, AcMgd.dll) の指定のみに用います。<br />
　　（無償のVisual Studio Express for WindowsDesktop を使って<br />
　　ObjectARX カスタムコマンドはVisual Studio Expressの制限より<br />
　　作成することができません）<br />
　　以下よりダウンロードしインストールできます。<br />
　　<a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=785550">http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=785550</a><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191045780c0970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0191045780c0970c" alt="8262Fig-9" title="8262Fig-9" src="/assets/image_458009.jpg" /></a><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e61869d970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01901e61869d970b" alt="8262Fig-10" title="8262Fig-10" src="/assets/image_305021.jpg" /></a><br /><br />
　　ダウンロードした、Autodesk_ObjectARX_2014_Documentation.sfx.exeを実行。<br />
　　変更ボタンで“C”ドライブ直下に変更して”OK”ボタンを押す。<br />
　　（C:\Autodesk_ObjectARX_2014_Win_64_and_32Bitホルダにインストールされる）<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac20d20b970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac20d20b970d" alt="8262Fig-11" title="8262Fig-11" src="/assets/image_603380.jpg" /></a><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019104578352970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b019104578352970c" alt="8262Fig-12" title="8262Fig-12" src="/assets/image_116887.jpg" /></a><br /><br />
   <br />
 (6).  AutoCAD JavaScript API リファレンス情報は以下のURLになります<br />
　　JavaScript API Reference:<br />
　　　　<a href="http://www.autocadws.com/jsapi/v1/docs/idx.html">http://www.autocadws.com/jsapi/v1/docs/idx.html</a><br />
　　JavaScript API:<br />
　　　　<a href="http://www.autocadws.com/jsapi/v1/Autodesk.AutoCAD.js">http://www.autocadws.com/jsapi/v1/Autodesk.AutoCAD.js</a><br />
　　( jsファイルをダウンロードしてご確認いただけます ) </p>

<p>次回は、この開発環境を使って、以下の「連動した呼び出し動作」の作成方法をご紹介します。</p>

<p>1.	.Netカスタムコマンドを使ってhtmlとJavaScriptファイルをロードしツールパレットにhtmlを表示<br />
2.	htmlのボタン -> JavaScript -> .Netマネージコードでデータベースクエリー処理の呼び出し<br />
（呼び出された.Netカスタムコマンド内で、データベースのクエリー処理）<br />
3.	htmlのボタン -> JavaScript -> .Netマネージコードでデータベース追加処理の呼び出し<br />
（呼び出された.Netカスタムコマンド内で、データベースのオブジェクト追加処理）</p>

<p>By Shigekazu Saito</p>
