---
layout: "post"
title: "Inventor API 入門"
date: "2013-02-12 01:51:29"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/02/inventor-api-%E5%85%A5%E9%96%80.html "
typepad_basename: "inventor-api-入門"
typepad_status: "Publish"
---

<p>Autodesk Inventor® ソフトウェアでは、特定業種向けのアプリケーションの構築や、生産部門と下流部門のインターフェイス、製造業者用のエンタープライズ アプリケーションなどとの連携において柔軟な開発が行なえるよう様々な開発環境をご用意しています。</p>

<p><strong>APIとして公開されているタイプライブラリ</strong><br />
　Autodesk Inventor はCOMオートメーション・インタフェースを使用して、そのプログラミングインターフェース（タイプライブラリ）が公開されています。</p>

<p><strong>カスタマイズ向けに公開されているタイプライブラリは ２種類です。</strong></p>

<p><strong>１．Autodesk Inventorの フルカスタマイズ可能な Inventorタイプライブラリ。</strong><br />
　（製品と一緒にインストールされ、Inventor製品のユーザーインターフェース・モデル構造・フィーチャー・ファイルを扱うAPI）<br />
<strong>２．Inventorファイルの読み取り専用の Apprenticeタイプライブラリ（無償）。</strong><br />
　（アペレンテス サーバーとも呼ばれ、Inventor製品が未インストール環境のPC上で、 Inventorファイルを読み取り専用で扱うAPI。　<br />
　Inventor Apprenticeは自由に配布可能で、アプリケーションは、Inventor のアセンブリ モデル構造や、トポロジとジオメトリへの照会アクセスが可能です。加えて、ファイル参照やドキュメント プロパティへの照会と一部編集も行うことができます。 <br />
　Inventor Apprentice はInventor View製品　<a href="http://www.autodesk.com/inventorview">こちらから</a>ダウンロード の一環としてインストールされます。ドキュメンテーションとサンプルはInventor SDKの一部として含まれています。）</p>

<p>　２つのタイプライブラリは、利用可能なMicrosoft Visual C++®, VB, Delphi, C#を含む一般的なプログラミング言語環境内でActiveXを利用し、COMインタフェースにアクセスすることができます。<br />
尚、Autodesk Inventorソフトウェアー はMicrosoftのVBA(世界で最もポピュラーなプログラミング環境)を含んでいます。(Inventor View製品にはVBAは含まれません)</p>

<p><br />
<strong>ソフトウェア開発キット (SDK)</strong><br />
　Inventorの規定のインストールでInventorソフトウェア開発キット(SDK)はインストールされます。  SDKは、2つに分割されています；　ユーザー・ツールおよび開発者用ツール。   SDKディレクトリの中でインストールするために必要とする適切なインストーラー（DeveloperTools.msiまたはUserTools.msi）を使ってインストールします。<br />
　UserTools.msiのインストールは、多くのユーティリティーを含んでいるいくつかのサブディレクトリーを含むUserToolsディレクトリが作成されます。  これらのユーティリティーはユーザーに便利なInventorの機能を提供します。  使用のためのソース・コードとランタイムの両方が提供されます。  ソース・コードはプログラマーが例として使用することができ、個別な要求を満たすカスタマイズを可能にするために提供されています。<br />
DeveloperTools.msiのインストールは、異なる言語、多くの追加されたドキュメンテーション、C++プログラマーのためのヘッダーファイル、およびアドイン・プログラム作成のサポートのためのウィザードを含み、ユーティリティーを使用し作成された多くのサンプルプログラムを含むDeveloperToolsディレクトリを作成します。</p>

<p><br />
<strong>ドキュメンテーション</strong><br />
　プログラミングガイドとしてヘルプシステム内に大規模なアプリケーション開発のドキュメントをAutodesk Inventorは含んでいます。</p>

<p><strong>Autodesk Inventor オブジェクトモデル</strong><br />
　DeveloperTools\Docs\ <span class="asset  asset-generic at-xid-6a0167607c2431970b017ee86fd22e970d"><a href="http://adndevblog.typepad.com/files/inventor2013objectmodel.pdf">Autodesk Inventor 2013 Object Model.pdf </a></span> (pdf - 380Kb)<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee8702b4c970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee8702b4c970d image-full" alt="Blog-No1" title="Blog-No1" src="/assets/image_101246.jpg" border="0" /></a><br />
<strong>Autodesk Inventor API リファレンス (英語)</strong>　<br />
　C:\Program Files\Autodesk\Inventor 2013\Local Help\ <span class="asset  asset-generic at-xid-6a0167607c2431970b017d40fd4d54970c"><a href="http://adndevblog.typepad.com/files/admapi_17_0.chm">admapi_17_0.chm</a></span><br />
 (chm- 11,563Kb)<br />
　( 近日中に、一部日本語化した Inventor2013 API リファレンスを公開予定)</p>

<p><br />
<strong>カスタマイズ アプリケーション開発</strong> <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d40fdcdd7970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017d40fdcdd7970c" alt="Blog-No2" title="Blog-No2" src="/assets/image_147038.jpg" /></a><br />
<strong>１．Inventor動作マクロの作成 </strong><br />
<em>　（製品に含まれるVBA（VBAIDE）環境を利用）</em> <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee872162b970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee872162b970d" alt="Blog-No3" title="Blog-No3" src="/assets/image_364219.jpg" /></a></p>

<p><strong>２．Inventorアドインソフトの作成</strong> <br />
<em>　（別途、有料のMicrosoft Visual Studio環境(Professional バージョンなど)が必要）</em> <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c36cf0f49970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017c36cf0f49970b" alt="Blog-No4" title="Blog-No4" src="/assets/image_156722.jpg" /></a><br />
　<u>尚、<strong>評価用アドインソフトの作成を望まれる方</strong>は、Autodesk Technical Q&A <a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=7834">Q&A-7834 評価版 Visual Studio 2010 Express を使って、Inventor2013 の評価用アドインの作成やデバック操作をする方法</a> に、記載しています。</u></p>

<p><strong>３．外部EXEにてInventor製品を操作</strong><br />
<em>　（別途、有料のMicrosoft Visual Studio環境(Professional バージョンなど)が必要）</em> <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee8721955970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee8721955970d" alt="Blog-No5" title="Blog-No5" src="/assets/image_590569.jpg" /></a></p>

<p><strong>４．アペレンテスにてInventorファイルを操作</strong><br />
<em>　（別途、有料のMicrosoft Visual Studio環境(Professional バージョンなど)が必要）</em> <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d40fdd546970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017d40fdd546970c" alt="Blog-No6" title="Blog-No6" src="/assets/image_627785.jpg" /></a></p>
