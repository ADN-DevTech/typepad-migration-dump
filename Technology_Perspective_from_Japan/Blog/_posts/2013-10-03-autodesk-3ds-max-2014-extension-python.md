---
layout: "post"
title: "Autodesk® 3ds Max® 2014 Extension Pythonスクリプティング環境の御紹介"
date: "2013-10-03 00:04:43"
author: "Akira Kudo"
categories:
  - "API カスタマイズ"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/10/autodesk-3ds-max-2014-extension-python%E3%82%B9%E3%82%AF%E3%83%AA%E3%83%97%E3%83%86%E3%82%A3%E3%83%B3%E3%82%B0%E7%92%B0%E5%A2%83%E3%81%AE%E5%BE%A1%E7%B4%B9%E4%BB%8B.html "
typepad_basename: "autodesk-3ds-max-2014-extension-pythonスクリプティング環境の御紹介"
typepad_status: "Publish"
---

<p>Autodesk Developer Networkの工藤　暁です。今回は先日リリースされましたAutodesk®
3ds Max® 2014 Extension新機能の一つであるPythonスクリプティング環境を御紹介します。紙面の都合上全ての情報をお伝え出来ませんが宜しくお願い致します。</p>
<p>既にマニュアルが<a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=16707768">Autodesk
3ds Max 2014 Python API Documentation</a>としてアクセス可能となっております。SDKやMAXScript同様ダウンロードも可能ですので是非御参照下さい。またサンプルコードはインストールフォルダの\scripts\Pythonフォルダに頭文字demoで始まるファイルが存在します。</p>
<p>では早速スクリプトを試してみましょう。MAXScript Listnerよりpython.Execute()又はpython.ExecuteFile()を以下の様に実行してみて下さい。python.ExecuteFile()はフルパスを指定可能です。</p>
<ul>
<li>python.Execute &quot;print &#39;hello&#39;&quot;</li>
<li>python.ExecuteFile &quot;C:\Program Files\Autodesk\3ds Max
2014\scripts\Python\demoBentCylinder.py&quot;</li>
</ul>
<p>又、python.ExecuteFile &quot;demoBentCylinder.py&quot;の様にファイル名のみ指定した際は、次のフォルダを検索します。：</p>
<ul>
<li>ユーザスクリプトフォルダ（userscripts\python）</li>
<li>ユーザスタートアップスクリプトフォルダ　（userscripts\startup\python）</li>
<li>スクリプトフォルダ　（scripts\python）</li>
<li>スタートアップスクリプト　（scripts\startup\python）</li>
<li>WindowsのPath環境変数. コマンドプロンプトにてecho %path%として確認可能</li>
</ul>
<p>&#0160; 
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019affbed19a970c-pi" style="display: inline;"><img alt="Capture0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019affbed19a970c image-full" src="/assets/image_747395.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Capture0" /></a><br /><br /></p>
<p>折角のPython環境ですから、PyQt, PySide等の拡張ライブラリを使用されたいかと存じますが、注意として3ds
Maxは64Bit版のQt4.8.2.0とPython
2.7.3を使用しております。拡張ライブラリについても同様の環境にてビルドされたものをご利用ください。又、Pythonが認識可能な場所（環境変数sys.path）への配置をお願いします。　バイナリパッケージのインストーラを使用される際はアドミニストレータ権限を持つユーザにて行って下さい。</p>
<p>Pythonで文字列を扱う際もUnicodeで処理する必要です。これはドキュメントの<a href="http://docs.autodesk.com/3DSMAX/16/ENU/3ds-Max-Python-API-Documentation/index.html?url=files/GUID-E51F1808-8FD6-41A0-AA11-A92438CE6E3C.htm,topicNumber=d30e2970">3ds
Max Python API &gt; Using Unicode</a>として御紹介されております。サンプルプログラムもdemoUniCodeIO.pyとして存在が、サンプルプログラムにて使用されている妙な文字列は気にしないで下さい。</p>
<p>最後にPython環境を印象付けるサンプルプログラムを御紹介させて頂きます。demoTYpeCasting.pyというサンプルコードにてyieldを使用しジェネレータによる遅延リストが作成されております。</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019affbeb4bd970b-pi" style="display: inline;"><img alt="Capture1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019affbeb4bd970b image-full" src="/assets/image_58275.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Capture1" /></a><br />シーン中にノードの階層構造を作成して頂ければ、サンプルコードが動作します。</p>
<p>お楽しみ下さい。</p>
<p>&#0160;</p>
