---
layout: "post"
title: "AutoCAD ノーコード ソリューション：ダイナミック ブロック"
date: "2025-03-19 00:30:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/03/autocad-no-code-solution-dynamic-block.html "
typepad_basename: "autocad-no-code-solution-dynamic-block"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e39420200b-pi" style="display: inline;"><img alt="Dynamic_block" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e39420200b image-full img-responsive" src="/assets/image_315950.jpg" title="Dynamic_block" /></a></p>
<p>AutoCAD の長い歴史の中で API が果たす役割は決して小さくはありません。実際、AutoLISP、ObjectARX（C#）ActiveX オートメーション（VBA）、.NET API（VB、C#）が古くから多く利用されています。</p>
<p>これら AutoCAD API を使ったカスタマイズで最初の切り口になっているのが、自動作図、あるは、作図補助をおこなうカスタム コマンドの作成です。カスタム コマンドを用意することで、標準コマンドによる対面操作の手数を減らして、より業務に特化させたり、利便性を高めたりすることが可能になります。</p>
<p>例えば、引張コイルばねを作図するために、次のような TCBANE コマンドを作成することが出来ます。</p>
<pre>(defun C:TCBANE (/) 

  (setq num nil)
  (while (&lt; num 3) 
    (progn 
      (initget 6)
      (setq num (getint &quot;\nコイル巻き数&lt;3&gt; :&quot;))
      (if (= num nil) 
        (setq num 3)
      )
      (if (&lt; num 3) 
        (princ &quot;\n3以上の値を入力してください ...&quot;)
      )
    )
  )

  (entmake 
    &#39;((0 . &quot;ARC&quot;)
      (10 -5.32907e-15 -3.55271e-15 0.0)
      (40 . 10.7226)
      (50 . 6.28319)
      (51 . 5.32325)
     )
  )
  (setq sset (ssadd (entlast)))
  (entmake 
    &#39;((0 . &quot;ARC&quot;)
      (10 -5.32907e-15 -3.55271e-15 0.0)
      (40 . 12.2226)
      (50 . 0.60179)
      (51 . 5.32325)
     )
  )
  (setq sset (ssadd (entlast) sset))
  (entmake &#39;((0 . &quot;LINE&quot;) (10 7.0106 -10.0122 0.0) (11 6.15024 -8.78345 0.0)))
  (setq sset (ssadd (entlast) sset))

  (entmake &#39;((0 . &quot;LINE&quot;) (10 9.84818 9.93789 0.0) (11 11.6924 -9.9769 0.0)))
  (setq sset2 (ssadd (entlast)))
  (entmake &#39;((0 . &quot;LINE&quot;) (10 11.3546 9.93848 0.0) (11 13.1988 -9.97631 0.0)))
  (setq sset2 (ssadd (entlast) sset2))
  (entmake 
    &#39;((0 . &quot;ARC&quot;)
      (10 10.6014 9.93818 0.0)
      (40 . 0.75001)
      (50 . 2.36844e-15)
      (51 . 3.14159)
     )
  )
  (setq sset2 (ssadd (entlast) sset2))
  (entmake 
    &#39;((0 . &quot;ARC&quot;)
      (10 12.4456 -9.9766 0.0)
      (40 . 0.75001)
      (50 . 3.14159)
      (51 . 0.0)
     )
  )
  (setq sset2 (ssadd (entlast) sset2))

  (initcommandversion)
  (command &quot;ARRAY&quot; sset2 &quot;&quot; &quot;R&quot; &quot;AS&quot; &quot;Y&quot; &quot;COU&quot; num 1 &quot;B&quot; (list 12.4456 -9.9766 0.0) &quot;S&quot; 1.95082461 1 &quot;&quot;)
  (setq sset (ssadd (entlast) sset))

  (setq num (- num 3))
  (entmake 
    (list (cons 0 &quot;ARC&quot;) 
          (cons 10 (list (+ (* num 1.95082461) 26.9435) -5.32907e-15 0.0))
          (cons 40 10.7226)
          (cons 50 3.14159)
          (cons 51 2.18166)
    )
  )
  (setq sset (ssadd (entlast) sset))
  (entmake 
    (list (cons 0 &quot;ARC&quot;) 
          (cons 10 (list (+ (* num 1.95082461) 26.9435) -5.32907e-15 0.0))
          (cons 40 12.2226)
          (cons 50 3.74338)
          (cons 51 2.18166)
    )
  )
  (setq sset (ssadd (entlast) sset))
  (entmake 
    (list (cons 0 &quot;LINE&quot;) 
          (cons 10 (list (+ (* num 1.95082461) 19.9329) 10.0122 0.0))
          (cons 11 (list (+ (* num 1.95082461) 20.7933) 8.78345 0.0))
    )
  )
  (setq sset (ssadd (entlast) sset))

  (initget 1)
  (setq pt (getpoint &quot;\n伸張コイルばねの配置位置を指示 :&quot;))

  (command &quot;MOVE&quot; sset &quot;&quot; &#39;(0.0 0.0 0.0) pt)
  (command &quot;ROTATE&quot; sset &quot;&quot; pt PAUSE)

  (princ)
)
</pre>
<p>このコマンドでは、コイルばねの巻き数と配置座標を入力・指示させることで、より汎用性を持たせた作図を実現しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e36382200b-pi" style="display: inline;"><img alt="名称未設定" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e36382200b image-full img-responsive" src="/assets/image_496805.jpg" title="名称未設定" /></a></p>
<p>一方、<strong>ダイナミック ブロック</strong>を利用すると、こういった作図系のカスタム コマンドを代替出来る可能性があります。</p>
<p>もともと AutoCAD が持っていたブロックの機能は、よく利用する形状をブロック定義（BLOCK コマンド）であらかじめ登録しておき、作図空間のモデル空間やペーパー空間（レイアウト）にブロック参照を挿入（INSERT コマンド）するものです。挿入時には、挿入位置や角度、尺度を変更出来るものの、形状そのものを可変にするようなことは出来ませんでした。このようなブロックを<strong>スタティック ブロック（静的なブロック）</strong>と呼ぶことがあります。</p>
<p>そこで用意されたのが AutoCAD 2006 で登場した<strong>ダイナミック ブロック（動的なブロック）</strong>です。ダイナミック ブロックは、ブロック エディタで<strong>パラメータ</strong>と呼ばれる可変要素と作図したオブジェクトを関連図付け、パラメータ値が変化した際の振る舞いを<strong>アクション</strong>として定義します。このブロック定義をブロック参照として作図空間に挿入すると、挿入後のブロック参照の形状を変化させることが出来るようになります。</p>
<p>例えば、伸張コイルばねの形状に直線状パラメータと回転パラメータを関連付け、ストレッチ アクションと回転アクションを振る舞いとして定義したとします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fa6c5d200d-pi" style="display: inline;"><img alt="Block_editor" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fa6c5d200d img-responsive" src="/assets/image_112663.jpg" title="Block_editor" /></a></p>
<p>このブロック参照をモデル空間などの挿入後に選択すると、ブロック エディタで定義したパラメータによってグリップが表示されます。グリップをマウスで操作すると、コイルばねの回転角度やコイルの巻き数を動的に変化させることが出来ます。もちろん、基点グリップで挿入位置を変えることも可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fa6c44200d-pi" style="display: inline;"><img alt="Dynamic_block" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fa6c44200d image-full img-responsive" src="/assets/image_150484.jpg" title="Dynamic_block" /></a></p>
<p>AutoCAD 2010 では、拘束機能導入にともない、ブロック エディタで幾何拘束・寸法拘束を使用してダイナミック ブロックを定義出来るようにもなっています。</p>
<p>お気づきと思いますが、ダイナミック ブロックは、今話題の<strong>ノーコード ソリューション</strong>と考えることも出来るのです。古くから作図系のカスタム コマンドを AutoCAD API で開発・保守されていて、まだダイナミック ブロックを未評価である場合には、一度ダイナミック ブロックの可能性を探ってみることをお勧めします。</p>
<ul>
<li><a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-2A3D92B8-20E2-47B3-92CE-FB3EB03888C3" rel="noopener" target="_blank">お試しください： ダイナミック ブロック</a></li>
<li><a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-3C2FB982-3AF6-437B-987F-4EDF81EA0662" rel="noopener" target="_blank">概要 - ダイナミック ブロック</a></li>
</ul>
<p>既製のダイナミック ブロックは、業種別にツールパレットから挿入、ブロック エディタで内容を確認することが出来ます。</p>
<ul>
<li><a href="https://help.autodesk.com/view/ACD/2025/JPN/?caas=caas/sfdcarticles/sfdcarticles/kA9Kf000000TcTn.html" rel="noopener" target="_blank">キッチン・ドア・トイレなどのサンプルデータはありますか (AutoCAD)</a></li>
</ul>
<p>もちろん、ダイナミック ブロックでブロック属性を使用することも出来ます。</p>
<p>By Toshiaki Isezaki</p>
