---
layout: "post"
title: "デスクトップ AutoCAD とオブジェクト イネーブラ"
date: "2022-09-12 00:02:59"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/09/desktop-vanilla-autocad-and-object-enabler.html "
typepad_basename: "desktop-vanilla-autocad-and-object-enabler"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/09/autocad-trivia-custom-object-and-object-enabler.html" rel="noopener" target="_blank">AutoCAD 雑学：カスタム オブジェクトとオブジェクト イネーブラ</a> では、AutoCAD 上のカスタム オブジェクトとプロキシ オブジェクトの関係や動作についてご案内しました。</p>
<p>その内容から、カスタム オブジェクトがプロキシ オブジェクト化してしまうのを防ぐため、オブジェクト イネーブラ（Object Enabler）と呼ばれる .dbx コンポーネント（ファイル）が用意されている点と、そのメカニズムをご理解いただけるはずです。カスタム オブジェクトやオブジェクト イネーブラは、オートデスク以外の 3rd party 開発者も ObjectARX を使って提供することが出来ます。</p>
<p><strong>業種別製品オブジェクト イネーブラの提供方法</strong></p>
<p style="padding-left: 40px;"><a href="https://www.autodesk.co.jp/products/autocad/overview" rel="noopener" target="_blank">AutoCAD Plus（AutoCAD including specialized toolsets）</a>の一部の業種別ツールセットが利用するカスタム オブジェクト用のオブジェクト イネーブラ、また、AutoCAD Civil3D 用のオブジェクト イネーブラは、Autodesk デスクトップ アプリの「マイ アップデート」で「Object Enabler」と検索するとダウンロード出来ます。</p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308dc9c85200c-pi" style="display: inline;"><img alt="Download_object_enabler" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308dc9c85200c image-full img-responsive" src="/assets/image_831661.jpg" title="Download_object_enabler" /></a></p>
<p style="padding-left: 40px;">Autodesk デスクトップ アプリも含め、オートデスクのオブジェクト イネーブラは、異なる方法で提供されています。AutoCAD 2023 ベースで主要なものをまとめると次のようになるかと思います。</p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed3ad9e200d-pi" style="display: inline;"><img alt="Oe_table" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed3ad9e200d image-full img-responsive" src="/assets/image_244421.jpg" title="Oe_table" /></a></p>
<p style="padding-left: 40px;">ダウンロードしたオブジェクト イネーブラは、AutoCAD にインストールすることで、プロキシ オブジェクト化することを抑止することが出来るようになります。</p>
<p><strong>AutoCAD 単体製品用の業種別製品オブジェクト イネーブラ</strong></p>
<p style="padding-left: 40px;">AutoCAD Architecture と AutoCAD MEP（日本語版未提供）で使用される、壁、ドア、ダクトなどのカスタム オブジェクト用オブジェクト イネーブラは、<a href="https://www.autodesk.co.jp/products/autocad/features" rel="noopener" target="_blank">AutoCAD 単体製品</a>のインストーラに含まれています。</p>
<p style="padding-left: 40px;">AutoCAD Civil 3D と AutoCAD Plant 3D で使用されるコリドー、バルブなどのカスタム オブジェクト用オブジェクト イネーブラは、Autodesk デスクトップアプリやダウンロード用の Web ページからダウンロードして AutoCAD 単体製品のインストールすることが出来ます。AutoCAD Map 用には AutoCAD Civil 3D 用オブジェクト イネーブラで代替出来ます。</p>
<p style="padding-left: 40px;">あいにく、AutoCAD Mechanical 用のオブジェクト イネーブラは、AutoCAD 単体製品での同梱やダウンロードでの提供がされていないため、AutoCAD Mechanical 製品で使用するバルーンやパーツ一覧などのカスタム オブジェクトは、プロキシ オブジェクトになってしまいます。</p>
<p style="padding-left: 40px;">ただし、それら Mechanical カスタム オブジェクトがプロキシオブジェクト化する際には、<a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2023/JPN/AutoCAD-Core/files/GUID-A1A272D8-F3E3-4B84-AF23-1AFEF732DA03-htm.html" rel="noopener" target="_blank">PROXYNITICE</a> システム変数の値が 1 に設定されていても、[プロキシ情報] ダイアログが表示されないように工夫されています。（Mechanical オブジェクトを定義するオブジェクト イネーブラでは、ObjectARX でカスタム オブジェクトを定義する ACRX_DXF_DEFINE_MEMBERS マクロで AcDbProxyEntity::kDisableProxyWarning が設定されているため。）</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed3a7d7200d-pi" style="display: inline;"><img alt="Proxy_objects" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed3a7d7200d image-full img-responsive" src="/assets/image_433434.jpg" title="Proxy_objects" /></a></p>
<p style="padding-left: 40px;">なお、AutoCAD Electrical では、カスタム オブジェクトではなく、ブロックとブロック属性を用いて回路図面を表現しているため、オブジェクト イネーブラの提供はありません。</p>
<p><strong>AutoCAD API 視点でのオブジェクト イネーブラの効果</strong></p>
<p style="padding-left: 40px;">ここで、AutoCAD API を利用する開発者目線でオブジェクト イネーブラの効果を見てみます。AutoCAD .NET API を使ったアドインが、次のような C# コードで記述されていると仮定します。このコードは、MyCommand コマンドを実行すると、モデル空間のすべてのオブジェクトを走査して、各オブジェクトの DXF 名と定義元になっている ObjectARX クラス名を表示する簡単なものです。</p>
<div>
<blockquote>
<div>Database db = Application.DocumentManager.MdiActiveDocument.Database;</div>
<div>Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;</div>
<div>using (Transaction tr = db.TransactionManager.StartTransaction())</div>
<div>{</div>
<div>&#0160; &#0160; BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);</div>
<div>&#0160; &#0160; BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[&quot;*MODEL_SPACE&quot;], OpenMode.ForRead);</div>
<div>&#0160; &#0160; Entity ent = null;</div>
<div>&#0160; &#0160; foreach (ObjectId objId in btr)</div>
<div>&#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; ent = (Entity)tr.GetObject(objId, OpenMode.ForRead);</div>
<div>&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(&quot;\n&quot; + ent.GetRXClass().DxfName + &quot; (&quot; + ent.GetRXClass().Name + &quot; クラス)&quot;);</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; &#0160; tr.Commit();</div>
<div>}</div>
</blockquote>
</div>
<p style="padding-left: 40px;">次に、対象図面です。ここでは、AutoCAD Mechanical のサンプル図面 gear_pump_subassy.dwg から六角ボルトとバルーンを 1 つづつ残してすべて削除し、比較用に線分を 2 つ追加した図面でテストしてみましょう。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed3fbfa200d-pi" style="display: inline;"><img alt="Acm_drawing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed3fbfa200d image-full img-responsive" src="/assets/image_978913.jpg" title="Acm_drawing" /></a></p>
<p style="padding-left: 40px;">この図面を Mechanical オブジェクト イネーブラがロードされている AutoCAD Mechanical で開いて、MyCommand コマンドを実行すると、次のコマンド プロンプトが示す結果が得られます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4b530a200b-pi" style="display: inline;"><img alt="Autocad_mechanical" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4b530a200b image-full img-responsive" src="/assets/image_323787.jpg" title="Autocad_mechanical" /></a></p>
<p style="padding-left: 40px;">この例では、バルーンを ACMBALOON 図形として、また、六角ボルトを STDPART2D 図形として正しく認識出来ていることがわかります。</p>
<p style="padding-left: 40px;">続いて、 Mechanical オブジェクト イネーブラがない AutoCAD 単体製品で同じ図面を開いて MyCommand コマンドを実行すると、バルーンと六角ボルトがプロキシ オブジェクトになっていることが見て取れます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed3fc30200d-pi" style="display: inline;"><img alt="Vanilla_autocad" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed3fc30200d image-full img-responsive" src="/assets/image_714227.jpg" title="Vanilla_autocad" /></a></p>
<p style="padding-left: 40px;">このように、オブジェクト イネーブラがある環境では、プロキシ オブジェクト化を抑止して特定のカスタム オブジェクトを識別出来るので、 AutoCAD アドインが特定のオブジェクトをカウントすることが出来るようになります。もちろん、AutoCAD もオブジェクトを正しく認識出来るため、正確な印刷や境界判定などを得られるようになります。</p>
<p style="padding-left: 40px;">なお、<a href="https://adndevblog.typepad.com/technology_perspective/2022/09/autocad-trivia-custom-object-and-object-enabler.html" rel="noopener" target="_blank">AutoCAD 雑学：カスタム オブジェクトとオブジェクト イネーブラ </a>でご紹介したとおり、オブジェクト イネーブラだけでカスタム オブジェクトの新規作成や編集が出来るわけではない点にご注意ください。カスタム オブジェクトのクラスを使用した API アクセスも同様です。</p>
<p>By Toshiaki Isezaki</p>
