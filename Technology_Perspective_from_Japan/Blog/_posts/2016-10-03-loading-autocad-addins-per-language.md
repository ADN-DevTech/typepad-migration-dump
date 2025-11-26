---
layout: "post"
title: "言語別に異なる AutoCAD アドインをロードする"
date: "2016-10-03 00:01:53"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/10/loading-autocad-addins-per-language.html "
typepad_basename: "loading-autocad-addins-per-language"
typepad_status: "Publish"
---

<p>最近の AutoCAD は、製品全体をインストールせずに&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/07/use-of-autocad-language-pack.html" target="_blank">Language Pack（言語パック）</a></strong>を導入することで、メニューやコマンド メッセージに表示する言語を変更することが出来ます。これによって、多国籍企業などで AutoCAD の操作を担当される方の母国語に合わせて、製図や 3D モデリングをおこなえるようになっています。</p>
<p>一方、API カスタマイズ プラットフォームとして AutoCAD を見た場合、Launguage Pack を適用した AutoCAD の言語に合わせて、ロードするアドイン アプリケーションや表示するメニューの言語表示を切り替えたい、と言う要望も存在します。</p>
<p>AutoCAD には、その歴史上、数多くの&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/09/auto-loading-for-autocad-addon-apps.html" target="_blank">アドイン ロード メカニズム</a></strong>&#0160;が存在しますが、上記ような要望には、<strong>バンドル パッケージを使った自動ローダー </strong>の利用をお勧めします。まず、バンドル パッケージの詳細については、次のオンラインヘルプをご一読ください。なお、この方法は、AutoCAD 2013 以降のバージョンでお使いいただくことが出来ます。</p>
<p style="padding-left: 30px;"><a href="http://help.autodesk.com/view/ACD/2017/JPN/?guid=GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008" target="_blank">概要 - プラグイン アプリケーションをインストール、アンインストールする</a></p>
<p style="padding-left: 30px;"><a class="link" href="http://help.autodesk.com/cloudhelp/2017/JPN/AutoCAD-Customization/files/GUID-BC76355D-682B-46ED-B9B7-66C95EEF2BD0.htm" target="_blank" title="PackageContents.xml ファイルには、作成した開発者に関する情報など、アプリケーション パッケージに関する情報が含まれます。">PackageContents.xml 形式リファレンス</a></p>
<p style="padding-left: 30px;"><a class="link" href="http://help.autodesk.com/cloudhelp/2017/JPN/AutoCAD-Customization/files/GUID-40F5E92C-37D8-4D54-9497-CD9F0659F9BB.htm" target="_blank" title="この例は、プラグインのパッケージに含めることができる内容とディスク上の構造を示します。">例: プラグインの基本的な .bundle フォルダ構造</a></p>
<p style="padding-left: 30px;"><a class="link" href="http://help.autodesk.com/cloudhelp/2017/JPN/AutoCAD-Customization/files/GUID-CBDC037A-9B84-4ED2-B0A6-6552ECF59540.htm" target="_blank" title="この例では、プラグインのパッケージがフォルダを使用してコンポーネントを編成する様子を示します。">例: フォルダを使用してプラグインのコンポーネントを編成する</a></p>
<p style="padding-left: 30px;"><a href="http://help.autodesk.com/view/ACD/2017/JPN/?guid=GUID-90A822A0-3595-401B-8DE6-22A9BC969660" target="_blank">サポートされるロケール コード リファレンス&#0160;</a></p>
<p>ここでは、日本語版 AutoCAD 2017 と英語 Language Pack と適用した英語版 AutoCAD 2017 用に、ロードする .NET アプリケーション（.dll）とカスタマイズ ファイル（.cuix）が自動的に切り替える例をご紹介します。</p>
<p>言語の自動切換えを実現するために、次のファイルを個別に用意するものとします。</p>
<ul>
<li><strong>MyCLines_JPN.cuix</strong><br />日本語表記のリボン パネルとコマンド ボタン定義を含むカスタマイズ ファイル</li>
<li><strong>MyCLines_ENU.cuix</strong><br />英語表記のリボン パネルとコマンドボタン定義を含むカスタマイズ ファイル</li>
<li><strong>AutoCAD CSharp plug-in_JPN.dll</strong><br />日本語のコマンド プロンプトを含む MYLINES カスタム コマンド定義を含む .NET アドイン アプリケーション</li>
<li><strong>AutoCAD CSharp plug-in_ENU.dll</strong><br />英語のコマンド プロンプトを含む MYLINES カスタム コマンド定義を含む .NET アドイン アプリケーション</li>
</ul>
<p>これらのファイルは、次のようなフォルダ構造でC:\Program Files\Autodesk\ApplicationPlugins\MyCLines.bundle フォルダ下の&#0160;<strong>Contents</strong> フォルダに格納されるものとします。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d224937c970c-pi" style="display: inline;"><img alt="Folder_structure" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d224937c970c image-full img-responsive" src="/assets/image_515999.jpg" title="Folder_structure" /></a></p>
<p>また、<strong>MyCLines.bundle</strong> フォルダ直下には、次の&#0160;PackageContents.xml&#0160;ファイルを配置します。 PackageContents.xml は、自動ローダーのパッケージ バンドルで条件分岐に使用される XML ファイルです。メモ帳などのテキストエディタでも編集することが出来ます。</p>
<p style="padding-left: 30px;">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;<br />&lt;ApplicationPackage<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SchemaVersion=&quot;1.0&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AppVersion=&quot;1.0&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Author=&quot;Autodesk Japan&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Name=&quot;Centerline&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AutodeskProduct=&quot;AutoCAD&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; FriendlyVersion=&quot;1.0.0&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ProductType=&quot;Application&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SupportedLocales=&quot;Enu|Jpn&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; OnlineDocumentation=&quot;http://adndevblog.typepad.com/technology_perspective&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Description=&quot;Draw Centerline&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UpgradeCode=&quot;&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ProductCode=&quot;&quot;&gt;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;CompanyDetails<br />&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Name=&quot;Autodesk Japan&quot;<br />&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Url=&quot;http://adndevblog.typepad.com/technology_perspective&quot;<br />&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Email=&quot;&quot;<br />&#0160; &#0160;&#0160;&#0160;&#0160; /&gt;<br />&#0160; &#0160;&#0160;&#0160;&#0160; &lt;Components Description=&quot;Centerline Tool&quot;&gt;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &lt;RuntimeRequirements<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; OS=&quot;Win64&quot;<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Platform=&quot;AutoCAD&quot;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; SeriesMin=&quot;R21.0&quot;<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SeriesMax=&quot;R21.0&quot;<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; /&gt;<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &lt;ComponentEntry<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AppName=&quot;Centerline Tool&quot;<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="background-color: #ffff00;">ModuleName=&quot;./Contents/AutoCAD CSharp plug-in_ENU.dll&quot;</span><br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="background-color: #ffff00;">ModuleNameJpn=&quot;./Contents/AutoCAD CSharp plug-in_JPN.dll&quot;</span><br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; /&gt;<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &lt;ComponentEntry<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="background-color: #ffff00;">ModuleName=&quot;./Contents/MyCLines_ENU.cuix&quot;</span><br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="background-color: #ffff00;">ModuleNameJpn=&quot;./Contents/MyCLines_JPN.cuix&quot;</span><br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; /&gt;<br />&#0160; &#0160;&#0160;&#0160;&#0160; &lt;/Components&gt;<br />&#0160;&lt;/ApplicationPackage&gt;</p>
<p>この PackageContents.xml&#0160;ファイルの黄色く反転した部分が 、先のオンラインヘルプ&#0160;<a href="http://help.autodesk.com/view/ACD/2017/JPN/?guid=GUID-90A822A0-3595-401B-8DE6-22A9BC969660" target="_blank">サポートされるロケール コード リファレンス&#0160;</a>&#0160;で説明されているロケールによる分岐をすることがわかります。</p>
<p>ここまでの環境と整えて、日本語版と英語版の AutoCAD 2017 を起動すると、それぞれ、製品の言語に一致する .NET アドイン アプリケーションとカスタマイズ ファイルをロード出来ることがわかります。&#0160;</p>
<p>&#0160;<img alt="App_menu_per_language" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb093e2da5970d image-full img-responsive" src="/assets/image_906246.jpg" title="App_menu_per_language" /></p>
<p>ここでご紹介したファイルは、下記のリンクでダウンロードすることが出来ます。C:\Program Files\Autodesk\ApplicationPlugins 下にフォルダ付きで展開してお試しください。</p>
<p style="padding-left: 30px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d2249a9a970c img-responsive"><a href="http://adndevblog.typepad.com/files/myclines.bundle-1.zip">MyCLines.bundleをダウンロード</a></span></p>
<p>なお、この例では、サポートする AuoCAD バージョンを 64 ビット版 AutoCAD 2017 に限定しています。AutoCAD 2016 など、他のバージョンではお試しいただけません。もし、他のバージョンでお試しいただく場合には、SeriesMin=&quot;R21.0&quot; （サポート対象の最下位バージョン）や&#0160;&#0160;SeriesMax=&quot;R21.0&quot; （サポート対象の最上位バージョン）を、サポートする AutoCAD バージョンに合わせて書き換える必要があります。AutoCAD バージョンに合わせた値（システム レジストリ キー）は次のとおりです。</p>
<p style="padding-left: 30px;">AutoCAD 2017 ： <strong>R21.0</strong></p>
<p style="padding-left: 30px;">AutoCAD 2016 ： <strong>R20.2</strong></p>
<p style="padding-left: 30px;">AutoCAD 2015 ： <strong>R20.1</strong></p>
<p style="padding-left: 30px;">AutoCAD 2014 ：<strong>R20.0</strong></p>
<p>同様に、32ビット版もサポートしたい場合には、 OS=&quot;Win64&quot; 部分を OS=&quot;Win32|Win64&quot; にする必要があります。</p>
<p>Language Pack を使った AutoCAD でカスタマイズされている方は、ぜひ参考にしてみてください。</p>
<p>By Toshiaki Isezaki</p>
