---
layout: "post"
title: "クロスプラットフォーム AutoLISP コード"
date: "2025-02-12 00:00:37"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/02/cross-platform-autolisp-code.html "
typepad_basename: "cross-platform-autolisp-code"
typepad_status: "Publish"
---

<p>誕生から 42 年を経過した AutoCAD は、現在では <a href="https://www.autodesk.com/jp/support/technical/article/caas/sfdcarticles/sfdcarticles/JPN/System-requirements-for-AutoCAD.html" rel="noopener" target="_blank">Windows 版</a>、<a href="https://www.autodesk.com/support/technical/article/caas/sfdcarticles/sfdcarticles/System-requirements-for-AutoCAD-for-Mac.html" rel="noopener" target="_blank">Mac 版</a>、<a href="https://help.autodesk.com/view/ACADWEB/JPN/?guid=AutoCAD_Web_Help_browsers_html" rel="noopener" target="_blank">Web 版</a>といったさまざまなプラットフォームで利用することが出来ます。一方、もともと MS-Dos 版として登場して発展してきた Windows 版 AutoCAD には、<a href="https://help.autodesk.com/view/OARX/2025/JPN/?guid=GUID-A0E9D801-8BE9-4BF1-85E8-3807E15F3B71" rel="noopener" target="_blank">AutoLISP</a>、<a href="https://help.autodesk.com/view/OARX/2025/JPN/?guid=GUID-9B4F6629-8B7D-460B-802B-6D2C25966994" rel="noopener" target="_blank">ObjectARX</a>、<a href="https://help.autodesk.com/view/OARX/2025/JPN/?guid=GUID-9C082B2D-015E-43C1-9168-623A2EA91D94" rel="noopener" target="_blank">ActiveX オートメーション（COM API）</a>、<a href="https://help.autodesk.com/view/OARX/2025/JPN/?guid=GUID-C3F3C736-40CF-44A0-9210-55F6A939B6F2" rel="noopener" target="_blank">.NET API</a>、<a href="https://help.autodesk.com/view/OARX/2025/JPN/?guid=adsk_jsdev_autocad_javascript_api_about" rel="noopener" target="_blank">JavaScript API</a> が用意されていて、多彩なカスタマイズに対応することが可能になっています。</p>
<p>最近では、AutoCAD との差別化のために API 機能を搭載していなかった AutoCAD LT でも、2024 バージョン以降、AutoLISP が利用出来るようになっています。（日本では既に AutoCAD LT の新規販売を終了しています）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f80246200d-pi" style="display: inline;"><img alt="Lt_autolisp" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f80246200d image-full img-responsive" src="/assets/image_127569.jpg" title="Lt_autolisp" /></a></p>
<p>&#0160;</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/02/autocad-webautolisp-capabilities.html">AutoCAD Web：AutoLISP 機能</a>&#0160;でご案内のとおり、AutoCAD Web でも AutoLISP を利用出来るようになっています。</p>
<p>そして、<strong>多彩な環境を持つ AutoCAD で共通して動作させることが出来る API を考えると、AutoLISP が該当する API になります。</strong></p>
<p>ただ、AutoLISP の場合でも、API が利用するテクノロジの違いを意識する必要があります。過去に <a href="https://adndevblog.typepad.com/technology_perspective/2013/11/texhnologies-for-apis-on-autodesk-products.html" rel="noopener" target="_blank">オートデスク製品の API が利用するテクノロジ</a> のブログ記事で、AutoCAD をはじめとした主要なオートデスクのデスクトップ製品 API が、どのようなテクノロジを利用しているかご紹介したことがありますが、クロスプラットフォームで動作する AutoLISP を考えたとき、テクノロジによって動作しないプログラムが出てしまいます。(vla- などの関数を使用する ActiveX オートメーション（COM）を使った AutoLISP プログラムです。</p>
<p>簡単な例を見てみましょう。次の AutoLISP プログラムは、選択したオブジェクト色を TrueColor 色に設定するプログラムです。次の&#0160;<strong>ChangeColor_List コマンド</strong>はリスト操作を利用しています。また、<strong>ChangeColor_ActiveX コマンド</strong>は ActiveX オートメーション（COM）を利用しています。</p>
<pre style="padding-left: 40px;">(defun trueColor-make (r g b /) 
  (+ (lsh (fix r) 16) 
     (lsh (fix g) 8)
     (fix b)
  )
)

(defun C:<strong>ChangeColor_List</strong> (/) 
  (setq ename (entsel &quot;\nオブジェクトを選択:&quot;))
  (setq datalist (entget (car ename)))
  (setq oldcolor (assoc 420 datalist))
  (setq newcolor (cons 420 (trueColor-make 130 100 255)))
  (if (= oldcolor nil) 
    (setq newlist (append datalist (list newcolor)))
    (setq newlist (subst newcolor oldcolor datalist))
  )
  (entmod newlist)
  (princ)
)

(vl-load-com)
(defun C:<strong>ChangeColor_ActiveX</strong> (/) 
  (setq acadObj (vlax-get-acad-object))
  (setq docObj (vla-get-ActiveDocument acadObj))
  (setq utilObj (vla-get-utility docObj))
  (vla-getentity utilObj &#39;returnObj &#39;pt &quot;\nオブジェクトを選択:&quot;)
  (setq colorObj (vla-get-Truecolor returnObj))
  ;(setq colorObj (vlax-create-object &quot;AutoCAD.AcCmColor.25&quot;))
  (vla-SetRGB colorObj 130 100 255)
  (vla-put-TrueColor returnObj colorObj)
  (princ)
)</pre>
<p>このコマンドのうち、ActiveX オートメーション（COM）テクノロジは Windows でしか利用出来ないため、クロス プラットフォームで動作するのは、リスト操作でオブジェクト色を変更する <strong>ChangeColor_List コマンドのみ</strong>になります。</p>
<p>上記のプログラムを含んだ AutoLISP ファイルを <strong>ChColor.lsp</strong> のファイル名で保存、 AutoCAD Mac にロードしても、<strong>ChangeColor_ActiveX コマンド</strong>を <a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-363A3BFA-CAF2-469E-9F35-0BF64139811C" rel="noopener" target="_blank">AutoComplete</a>（入力候補の表示）機能にを認識させて実行することが出来ません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e16c2f200b-pi" style="display: inline;"><img alt="Mac" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e16c2f200b img-responsive" src="/assets/image_622007.jpg" title="Mac" /></a></p>
<p>同じく、AutoCAD Web でも <strong>ChangeColor_ActiveX コマンド</strong>は認識させて、実行することは出来ません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f87bfe200d-pi" style="display: inline;"><img alt="Web" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f87bfe200d img-responsive" src="/assets/image_722501.jpg" title="Web" /></a></p>
<p>Windows 版の AutoCAD の場合には、<strong>ChColor.lsp</strong>&#0160;をロードさせると、<strong>ChangeColor_List コマンド</strong>と<strong>ChangeColor_ActiveX コマンド</strong>の両方を認識します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cb643c200c-pi" style="display: inline;"><img alt="Windows" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3cb643c200c image-full img-responsive" src="/assets/image_763600.jpg" title="Windows" /></a></p>
<p>なお、AutoCAD Mac（Mac 版 AutoCAD ）には Windows 版と同じく ObjectARX でビルドした .arx ファイルをロードして実行することが出来ますが、アンマネージ C++ を採用する ObjectARX は SDK とビルドしたバイナリ（.arx ファイル）がプラットフォーム依存になるため、Windows 版 AutoCAD 用に用意した .arx を Mac 版 AutoCAD にロード・実行することは出来ません。その逆も同様です。</p>
<p>また、ObjectARX と .NET API （Windows 版のみ）には、AutoLISP 関数を作成する機能が与えられていますが、ビルドした .arx や .dll ファイルは、ビルドしたプラットフォーム版の AutoCAD でしかロード出来ません。このため、そのような API で定義された AutoLISP 関数を使用する AutoLISP プログラムも、クロス プラットフォームで利用することは出来ません。</p>
<ul>
<li>例：<a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/7ziCRKH19yLela4TNqbE1j.html" rel="noopener" target="_blank">AutoCAD .NET API ：AutoLISP 関数の定義</a></li>
</ul>
<p>AutoLISP をクロス プラットフォームで利用する場合には、DXF グループコードを使用してリスト操作、オブジェクト作成・編集するようなプログラムが必要になります。</p>
<ul>
<li>例：<a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/autolisp-kuo-zhangentiti-detano-fu-jia-can-zhao-xue-chu/ta-p/13176547" rel="noopener" target="_blank">AutoLISP：拡張エンティティ データの付加・参照・削除</a></li>
</ul>
<p>By Toshiaki Isezaki</p>
