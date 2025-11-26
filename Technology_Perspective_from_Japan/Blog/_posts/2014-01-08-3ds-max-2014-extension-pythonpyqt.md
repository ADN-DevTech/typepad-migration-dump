---
layout: "post"
title: "3ds Max® 2014 Extension新機能 PythonスクリプティングのＰｙＱｔパッケージ追加例の御紹介"
date: "2014-01-08 23:15:45"
author: "Akira Kudo"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/01/3ds-max-2014-extension%E6%96%B0%E6%A9%9F%E8%83%BD-python%E3%82%B9%E3%82%AF%E3%83%AA%E3%83%97%E3%83%86%E3%82%A3%E3%83%B3%E3%82%B0%E3%81%AE%EF%BD%90%EF%BD%99%EF%BD%91%EF%BD%94%E3%83%91%E3%83%83%E3%82%B1%E3%83%BC%E3%82%B8%E8%BF%BD%E5%8A%A0%E4%BE%8B%E3%81%AE%E5%BE%A1%E7%B4%B9%E4%BB%8B.html "
typepad_basename: "3ds-max-2014-extension新機能-pythonスクリプティングのｐｙｑｔパッケージ追加例の御紹介"
typepad_status: "Publish"
---

<p style="text-align: left">Autodesk Developer Networkの工藤　暁です。今回は先日リリースされましたAutodesk® 3ds Max® 2014 Extension新機能の一つであるPythonスクリプティングにてPyQtを使用する為の記事が<a href="http://area.autodesk.com/blogs/chris/pyqt-ui-in-3ds-max-2014-extension">PyQt UI in 3ds Max 2014 Extension</a>として<a href="http://area.autodesk.com/">AREA</a>に記載されましたので御紹介させて頂きます。</p>
<p style="text-align: left">この記事にて紹介されておりますサンプルスクリプトですが、動作させるには下記の環境及びモジュールが必要となります。Python及びMaxPlus, osとsysモジュールは2014 Extensionに付随しますので問題ありませんが、それ以外に関しては自身で設定する必要があります。</p>
<ul>
<li>Python version 2.7.3 (default, Apr 10 2012, 23:24:47) [MSC v.1500 64 bit (AMD64)]</li>
<li>MaxPlus module</li>
<li>os module</li>
<li>sys module</li>
<li>PyQt module</li>
<li>QtWinMigrate module</li>
</ul>
<p style="text-align: left">幸いな事に<a href="http://www.blur.com/">Blur Studio</a>より、サンプルの動作に必要な必要な全てのパッケージを纏めたインストーラが<a href="https://code.google.com/p/blur-dev/downloads/list">ダウンロード可能</a>です。インストールをする際にBlur Studioが作成したプラグインをインストールされますが（私はインストールしませんでしたが）、サンプルプログラムの確認には必要ありません。動作結果は以下の通りです。</p>
<p style="text-align: center"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fbec1b11970b-pi" style="display: inline;"><img alt="Blur" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fbec1b11970b image-full img-responsive" src="/assets/image_17882.jpg" title="Blur" /></a><br />&#0160;</p>
<p style="text-align: left">しかしながら、今後の3ds Maxのバージョンアップに対応されるか不安は残りますので、ご自身で環境を設定されたい方も多いかと存じます。残念ながらQtWinMigrateのPython Bindingを実装する事は今回出来ませんでしたが、以下が各モジュールのビルド手順となります。確認に際しWindows7のクリーンインストールに対し、3ds Max及びVisual Studio 2010 SP1をインストールした後に以下の手順を行っております(全て英語版を使用しました, すいません)。</p>
<ul>
<li>Visual Studio 2010 x64 Win64 Command PromptにてC:\Program Files\Autodesk\3ds Max 2014\pythonがPath設定されている事を確認</li>
</ul>
<p style="text-align: left">&#0160;</p>
<ul>
<li>SIPのビルド
<ul>
<li>sip-4.15.3.zipを<a href="http://sourceforge.net/projects/pyqt/files/sip/sip-4.15.3/">ダウンロード</a></li>
<li>C:\sip-4.15.3に解凍</li>
<li>Visual Studio 2010 x64 Win64 Command Promptにて以下のコマンドを実行
<ul>
<li>cd C:\sip-4.15.3</li>
<li>python configure.py -p win32-msvc2010</li>
<li>nmake</li>
<li>nmake install</li>
</ul>
</li>
</ul>
</li>
</ul>
<p style="text-align: left">&#0160;</p>
<ul>
<li>Qt4のビルド
<ul>
<li>qt-win-opensource-4.8.3-vs2010.exeを<a href="http://download.qt-project.org/archive/qt/4.8/4.8.3/">ダウンロード</a></li>
<li>C:\Qt\4.8.3にインストール</li>
<li>DefaultLocalizationStrategy.cpp 325行目の全角”を半角\”に置き換え</li>
</ul>
</li>
</ul>
<p style="text-align: center">&#0160;</p>
<p style="text-align: center"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5109bb4a9970c-pi" style="display: inline;"><img alt="Qt4_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a5109bb4a9970c image-full img-responsive" src="/assets/image_681925.jpg" title="Qt4_1" /></a></p>
<p style="text-align: center"><br />注意: 全角 ”を半角 \”に置き換えない場合は下記のコンパイルエラーが発生</p>
<p style="text-align: center">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5109bb32a970c-pi" style="display: inline;"><img alt="Qt4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a5109bb32a970c image-full img-responsive" src="/assets/image_451450.jpg" title="Qt4" /></a></p>
<ul>
<li>先のVisual Studio 2010 x64 Win64 Command Promptにて以下のコマンドを実行
<ul>
<li>cd C:\Qt\4.8.3</li>
<li>set QTDIR=C:\Qt\4.8.3</li>
<li>set PATH=%PATH%;C:\Qt\4.8.3\bin</li>
<li>configure -opensource -platform win32-msvc2010</li>
<li>ライセンスの問い合わせに&quot;y&quot;と入力</li>
<li>nmake</li>
<li>nmake install</li>
</ul>
</li>
</ul>
<p style="text-align: left">&#0160;</p>
<ul>
<li>PyQt4のビルド
<ul>
<li>PyQt-win-gpl-4.10.3.zipを<a href="http://www.riverbankcomputing.com/software/pyqt/download">ダウンロード</a></li>
<li>C:\PyQt-win-gpl-4.10.3に解凍</li>
<li>先のVisual Studio 2010 x64 Win64 Command Promptにて以下のコマンドを実行
<ul>
<li>cd C:\PyQt-win-gpl-4.10.3</li>
<li>python configure.py –w</li>
<li>ライセンスの問い合わせに&quot;y&quot;と入力</li>
<li>nmake</li>
<li>nmake install</li>
</ul>
</li>
</ul>
</li>
</ul>
<p style="text-align: left">&#0160;</p>
<ul>
<li>テスト
<ul>
<li>以下のスクリプトを実行して下さい.</li>
</ul>
</li>
</ul>
<p style="text-align: left; padding-left: 90px;">import MaxPlus, ctypes</p>
<p style="text-align: left; padding-left: 90px;">from PyQt4.Qt import *</p>
<p style="text-align: left; padding-left: 90px;">from PyQt4.QtGui import *</p>
<p style="text-align: left; padding-left: 90px;">from sip import *</p>
<p style="text-align: left; padding-left: 90px;">print SIP_VERSION_STR, QT_VERSION_STR, PYQT_VERSION_STR</p>
<p style="text-align: left">&#0160;</p>
<p style="text-align: center"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b04787f1a970d-pi" style="display: inline;"><img alt="Test" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b04787f1a970d image-full img-responsive" src="/assets/image_727036.jpg" title="Test" /></a><br />&#0160;</p>
<p style="text-align: center">(Command Promptにて設定した環境変数QTDIRとPATHが有効か確認)</p>
<p style="text-align: left">&#0160;</p>
<p style="text-align: left">番外編）QtWinMigrateのビルド（Python　Bindingはされませんが）</p>
<ul>
<li>qtwinmigrate-2.8-opensource.zipを<a href="http://ftp3.ie.freebsd.org/pub/ftp.trolltech.com/pub/qt/solutions/lgpl/">ダウンロード</a></li>
<li>C:\qtwinmigrate-2.8-opensourceに解凍</li>
<ul>
<li>先のVisual Studio 2010 x64 Win64 Command Promptにて以下のコマンドを実行
<ul>
<li>cd C:\qtwinmigrate-2.8-opensource</li>
<li>configure -library</li>
<li>ライセンスの問い合わせに&quot;yes&quot;と入力</li>
<li>qmake qtwinmigrate.pro -spec win32-msvc2010</li>
<li>nmake</li>
<li>nmake install</li>
</ul>
<p>QtWinMigrateのPython　Bindingについては引き続き調査させて頂きます。</p>
</li>
</ul>
</ul>
