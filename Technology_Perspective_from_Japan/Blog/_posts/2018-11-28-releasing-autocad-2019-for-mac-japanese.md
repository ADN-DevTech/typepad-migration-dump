---
layout: "post"
title: "AutoCAD 2019 for Mac 日本語版リリース"
date: "2018-11-28 00:42:33"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/11/releasing-autocad-2019-for-mac-japanese.html "
typepad_basename: "releasing-autocad-2019-for-mac-japanese"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c24a32200b-pi" style="float: right;"><img alt="Mac_26mm-to-55mm_cmyk" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c24a32200b img-responsive" src="/assets/image_11542.jpg" style="margin: 0px 0px 5px 5px;" title="Mac_26mm-to-55mm_cmyk" /></a>AutoCAD 2019 for Mac、AutoCAD LT 2019 for Mac のリリースにともない、両製品の日本語版がリリースされました。米国では <a href="https://adndevblog.typepad.com/files/autocad_mac_2011_overview_brochure_us.pdf" rel="noopener" target="_blank"><strong>AutoCAD 2011</strong></a> から Mac 版が <strong><a href="https://adndevblog.typepad.com/files/autocad_mac_2011_overview_brochure_us.pdf" rel="noopener noreferrer" target="_blank">AutoCAD for Mac</a></strong>&#0160;の名で販売されていましたが、今回のリリースまで日本語化は見送られていました。近年になって Mac 自体が一定の支持を集めていることもあり、以前、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2013/06/console-version-of-autocad.html" rel="noopener noreferrer" target="_blank">コンソール バージョンの AutoCAD</a></strong> でご紹介したマルチプラットフォーム化の改修を経て、今回のリリースとなっています。</p>
<p>Mac OS にネイティブ対応する日本語版 AutoCAD は、マルチプラットフォームを標ぼうしていた AutoCAD R12 以来のリリースとなります。当時、ビジネス用途で Windows が主流になりつつあり、残念ながら、Macintosh（当時）版の AutoCAD はあまり話題になりませんでした。その後、オブジェクト指向な開発でソースコードの一新を図った AutoCAD R13 で開発を Windows に一本化したため、17~18 年ぶりの日本語版の登場です。</p>
<p>今回リリースされた AutoCAD 2019 for Mac、AutoCAD LT 2019 for Mac は基本的に Windows 版で実現している機能を踏襲していますが、Mac の特化している機能があったり、Windows 版で利用可能な機能が使えなかったりするものも存在します。今回は、おおまかにそれら内容について触れておきたいと思います。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37d12a5200c-pi" style="display: inline;"><img alt="Autocad_for_mac" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37d12a5200c image-full img-responsive" src="/assets/image_907960.jpg" title="Autocad_for_mac" /></a></p>
<p><strong>AutoCAD 2019 for Mac の動作環境</strong></p>
<p style="padding-left: 30px;">まずは動作環境です。AutoCAD 2019 for Mac の場合、次のようになっています。</p>
<p style="padding-left: 30px;">オペレーティングシステム</p>
<p style="padding-left: 60px;">Apple® Mac® macOS v10.14 以降(Mojave)、v10.13 以降(High Sierra)、macOS v10.12 以降(Sierra)</p>
<p style="padding-left: 30px;">モデル</p>
<p style="padding-left: 60px;">Apple Mac Pro® 4.1 以降、MacBook® Pro 5.1 以降、iMac® 8.1 以降、Mac Mini® 3.1 以降、MacBook Air® 2.1 以降、MacBook 5.1 以降</p>
<p style="padding-left: 30px;">CPU タイプ</p>
<p style="padding-left: 60px;">64 ビット Intel CPU (Intel Core Duo CPU、2 GHz 以上推奨)</p>
<p style="padding-left: 30px;">メモリ</p>
<p style="padding-left: 60px;">3 GB の RAM (4 GB 以上を推奨)</p>
<p style="padding-left: 30px;">ディスプレイ解像度</p>
<p style="padding-left: 60px;">True Color 対応 1,280 × 800 ディスプレイ(Retina ディスプレイ搭載の 2880 x 1800 推奨)</p>
<p style="padding-left: 30px;">ディスク空き容量</p>
<p style="padding-left: 60px;">ダウンロードおよびインストール用に 3 GB のディスク空き容量</p>
<p style="padding-left: 30px;">ポインティングデバイス</p>
<p style="padding-left: 60px;">Apple® Mouse、Apple Magic Mouse、Magic Trackpad、MacBook® ProTrackpad、または MS 互換マウス</p>
<p style="padding-left: 30px;">プリンタ</p>
<p style="padding-left: 60px;">Mac OS X 互換プリンタ</p>
<p><strong>ユーザインタフェース</strong></p>
<p style="padding-left: 30px;">Windows 版の AutoCAD や AutoCAD LT には、もっとも頻繁に利用するユーザ インタフェースとして、リボン インタフェースがあります。このリボン インタフェースや一部のインタフェースは、Microsoft 社が .NET Framework として提供している機能を利用している関係で、Mac では利用することが出来ません。Mac OS 上で動作するソフトウェアでは、Windows とは異なるユーザ インタフェース文化が存在するため、AutoCAD/LT for Mac も、その文化を踏襲するかたちで、次のような分類のユーザ インタフェースを用意しています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a33a5b200d-pi" style="display: inline;"><img alt="User_interface_categories" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a33a5b200d image-full img-responsive" src="/assets/image_563115.jpg" title="User_interface_categories" /></a></p>
<p style="padding-left: 30px;">もし、探しているコマンドが見つけられない場合は、[ヘルプ] メニューから検索ボックスにキーワードを入力してリターンキーを押してみてください。例えば、&quot;ブロック&quot; と入力すると、該当する候補がリスト提示されるので、希望する項目をマウス カーソルでポイントすると、コマンドが割り当てられたメニュー項目がアニメーションとともに矢印表示されます。コマンドがプルダウンメニューに割り当てられている場合には、この操作で簡単に該当するメニューを見出すことが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a33a4f200d-pi" style="display: inline;"><img alt="Help_search" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a33a4f200d image-full img-responsive" src="/assets/image_283593.jpg" title="Help_search" /></a></p>
<p style="padding-left: 30px;">ユーザ インタフェースの色合いを変えるカラーテーマも利用することが出来ます。お好みに応じて、[アプリケーション基本設定] ダイアログからダークテーマとライトテーマを切り替えることが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37d1294200c-pi" style="display: inline;"><img alt="Color_thema" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37d1294200c image-full img-responsive" src="/assets/image_966853.jpg" title="Color_thema" /></a></p>
<p style="padding-left: 30px;">カラーテーマ以外にも、ユーザ インタフェースもカスタマイズすることが出来ます。詳細は、オンラインヘルプから <strong><a href="https://help.autodesk.com/view/ACDMAC/2019/JPN/?guid=GUID-8ACA5693-657B-460C-8A36-0A230CDE7A71" rel="noopener" target="_blank">概要 - Mac OS でのユーザ インタフェースのカスタマイズ</a></strong> を参照してみてください。</p>
<p style="padding-left: 30px;">なお、Mac 版には Language Pack を後から導入するオプションがありません。製品のユーザ インタフェースで利用する言語は、あらかじめインストーラに組み込まれているので、製品の起動後に [アプリケーション基本設定] ダイアログで変更することになります。ここで選択可能な言語は、英語、フランス語、ドイツ語と日本語の 4 種類です。言語設定を変更した場合には、新しい設定を有効にするために AutoCAD の再起動が必要になります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c2d737200b-pi" style="display: inline;"><img alt="Change_language" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c2d737200b image-full img-responsive" src="/assets/image_56742.jpg" title="Change_language" /></a></p>
<p style="padding-left: 30px;">ユーザ インタフェースの言語切り替えとは別に注意していただきたいのは、文字スタイルやマルチ テキスト エディタで指定する日本語フォントの選択についてです。Mac 上で動作するソフトウェア/アプリケーションでは、一般的にフォント選択の際に日本語フォントが日本語表記で表示されます。例えば、Microsoft Office 365 for Mac に含まれる PowerPoint では、次のように表示されます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37d020e200c-pi"><img alt="Powerpoint_fonts" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37d020e200c image-full img-responsive" src="/assets/image_809403.jpg" title="Powerpoint_fonts" /></a></p>
<p style="padding-left: 30px;">AutoCAD/LT for Mac では、いまのところ、日本語フォントも英語表記で表示されてしまいます。これは、日本語記入出来ないということではありません。この例では、PowerPoint で<strong> ヒラギノ角ゴ～</strong>&#0160;として表示されているフォント名が&#0160;<strong>Hiragino Kaku Gothic～</strong> と表示されてしまいます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c34ba3200b-pi" style="display: inline;"><img alt="Autocad_fonts" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c34ba3200b image-full img-responsive" src="/assets/image_619389.jpg" title="Autocad_fonts" /></a></p>
<p style="padding-left: 30px;">Mac 版固有の機能も存在します。最も目新しい機能には、2016年以降に販売が開始された MacBook Pro に搭載されている Touch Bar があります。Touch Bar は従来のファンクションキーに代わるもので、ソフトウェア/アプリケーションによって割り当てれた機能を、視覚的に表示する有機 EL を使ったボタン提供させるユーザインタフェースです。 AutoCAD/LT では、この割り当てを [表示] メニューの&#0160; [Touch Bar をカスタイズ...] メニューから変更することが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37d060c200c-pi"><img alt="Touch_bar" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37d060c200c image-full img-responsive" src="/assets/image_107566.jpg" title="Touch_bar" /></a></p>
<p style="padding-left: 30px;">Touch Bar には指紋認証をおこなう&#0160;Touch ID も搭載されているので、Mac 上にソフトウェア/アプリケーションをインストールする際に、毎回システム パスワードを入力する手間が省くことも出来ます。</p>
<p><strong>互換性</strong></p>
<p style="padding-left: 30px;">Mac 版も AutoCAD/AutoCAD LT が保存するのは DWG ファイル形式の図面に変わりありません。DWG ファイル形式に Windows 版と Mac 版の差はありませんので、Mac 版で保存した 2D 図面や 3D モデルも Windows 版で開くことが出来ます。もちろん、その逆も可能です。AutoCAD 2019/AutoCAD LT 2019 for Mac で開くことが出来る図面ファイル形式と保存することが出来る図面ファイル形式（DWG、DXF）は、Windows 版と同じです。</p>
<p style="padding-left: 30px;">Mac 版では Windows 版のようにワークスペース機能がないため、その切り替えによって表示するユーザ インタフェースを変化させることが出来ません。ただ、機能分類的には、ほぼ同じ機能をユーザ インタフェースに実装しているはずです。モデリング機能や他の 3D 機能は、[モデリング] ツールセット タブをクリックして、ツールセットをアクティブにして利用することが可能です。3D 機能の中には、ジオメトリへのマテリアルの適用や光源の作成も含まれるわけですが、残念ながら、RapitRT レンダリング エンジンを用いた製品内レンダリングの機能は実装されていません。レンダリング画像を得るには、サブスクリプション契約から入可出来る Windows 版 AutoCAD を Windows 上で用いるか、クラウド レンダリング（<a href="https://gallery.autodesk.com/a360rendering" rel="noopener noreferrer" target="_blank">https://gallery.autodesk.com/a360rendering</a>）を利用する必要があります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a5f2af200d-pi" style="display: inline;"><img alt="Autocad_for_mac_3d" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a5f2af200d image-full img-responsive" src="/assets/image_455018.jpg" title="Autocad_for_mac_3d" /></a></p>
<p style="padding-left: 30px;">Mac 版には業種別ツールセットの用意がありませんので、Windows 版 AutoCAD で業種別ツールセットを導入して固有のカスタム オブジェクトを記入した DWG ファイルは、Mac 版で編集することが出来ません。&#0160;</p>
<p><strong>API</strong></p>
<p style="padding-left: 30px;">AutoCAD for Mac では、AutoLISP と ObjectARX のみがサポートされています。Windows 固有の COM（Component Object Model）を利用した ActiveX オートメーション、別名 COM API や .NET Framework を利用した AutoCAD .NET API は利用出来ません。</p>
<p style="padding-left: 30px;">Windows 版でよく利用されている VBA は COM API を利用しているため、残念ながら、Mac 版で利用することは出来ません。同様に、AutoLISP の場合、Visual LISP として導入され、COM を間接的に利用している (vla-<em>XXX</em>) や&#0160;(vlax-<em>XXX</em>) 関数、(vlr-<em>XXX</em>) 関数も利用することは出来ません。</p>
<p style="padding-left: 30px;">ObjectARX を利用する場合には、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx-license-download" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx-license-download</a></strong> から&#0160;AutoCAD 2019 Mac OS ObjectARX SDK をダウンロード、利用してアプリケーションをビルドする必要があります。必要な開発環境は次のとおりです。</p>
<p class="wd-font-19 wd-light wd-mv-0" style="padding-left: 30px;"><strong>For 64-bit ObjectARX 2019 for Mac®</strong></p>
<ul>
<li class="wd-font-19 wd-light wd-mv-0">macOS® - Mojave 10.14 以降; High Sierra 10.13 以降; Sierra 10.12 以降</li>
<li class="wd-font-19 wd-light wd-mv-0">64-bit Intel プロセッサ</li>
<li class="wd-font-19 wd-light wd-mv-0">3 GB RAM (4 GB – 推奨)</li>
<li class="wd-font-19 wd-light wd-mv-0">20 MB の空き領域（インストールに必要）</li>
<li class="wd-font-19 wd-light wd-mv-0">True Color 1,280 x 800 ディスプレイ解像度（2880 x 1800 Retina ディスプレイ推奨）</li>
<li class="wd-font-19 wd-light wd-mv-0">Xcode 9.3 &gt;&gt; <a href="https://developer.apple.com/download/more/?=xcode" rel="noopener" target="_blank">https://developer.apple.com/download/more/?=xcode</a></li>
</ul>
<p style="padding-left: 30px;">※ ObjectARX アプリケーションをビルドするには Apple 社が提供する Xcode 統合開発環境がの旧バージョン 9.3 が必要です。旧バージョンの入手には Apple Develoer Center に Apple ID を使ってサインインする必要があります。</p>
<p style="padding-left: 30px;">AutoCAD for Mac には、Windows 版に同梱されている AcCoreConsole.exe に相当する実行形式は提供されていません。AutoCAD LT for Mac の場合には、 Windows 版 AutoCAD LT と同様に、API を使ってカスタマイズしたり、API を使ってカスタマイズされたモジュールをロードして利用することは出来ません。</p>
<p>AutoCAD for Mac は AutoCAD をサブスクリプション契約していると、AutoCAD LT for Mac は AutoCAD LT をサブスクリプション契約していると、それぞれ<strong> <a href="https://accounts.autodesk.com/" rel="noopener noreferrer" target="_blank">Autodesk Accounts ページ（https://accounts.autodesk.com/）</a></strong>からダウンロードしてインストールすることが出来ます。</p>
<p><strong> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37d8fab200c-pi" style="display: inline;"><img alt="Autodesk_account" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad37d8fab200c image-full img-responsive" src="/assets/image_857273.jpg" title="Autodesk_account" /></a></strong></p>
<p>AutoCAD 2019 for Mac、AutoCAD LT 2019 for Mac は、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/03/only-one-autocad-2.html" rel="noopener noreferrer" target="_blank">One AutoCAD</a></strong> として日本語環境に新たに加わった製品です、Mac をお持ちであれば、ぜひ、試してみてください。</p>
<p>By Toshiaki Isezaki</p>
