---
layout: "post"
title: "AutoCAD LT にない AutoCAD 機能：API"
date: "2021-02-19 00:51:47"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/02/autocad-features-which-are-not-available-on-autocad-lt-api.html "
typepad_basename: "autocad-features-which-are-not-available-on-autocad-lt-api"
typepad_status: "Publish"
---

<p>AutoCAD には、業務でよく利用する作図や編集作業、あるいは、それらを合わせた反復作業を自動化して、独自のコマンド（カスタム コマンド）を作成する目的で&#0160;<strong>API</strong> が用意されています。<a href="https://apps.autodesk.com/?Language=JA" rel="noopener" target="_blank"><strong>Autodesk App Store</strong></a> に記載されているアドイン アプリケーションだけでなく、社内利用のカスタマイズでも利用されています。</p>
<p>38 年の AutoCAD の中で、その登場順に AutoLISP、ActiveX オートメーション（COM）/VBA、ObjectARX、.NET API、JavaScript の異なる 5 つの API が利用可能になっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99079cb200b-pi" style="display: inline;"><img alt="Apis" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99079cb200b image-full img-responsive" src="/assets/image_763496.jpg" title="Apis" /></a></p>
<p>カスタム コマンドは、API を使ってプログラムを作ることで作成します。そして、このプログラム作成を「開発」と表現したりします。</p>
<p>API を使った開発では、事前のノウハウがあると、いろいろと有利になることがあります。例えば、開発にかかる時間が短くで出来たり、AutoCAD 標準コマンドでは実現出来ない方法で業務データを DWG ファイルの中に保存したり、といった具合です。</p>
<p>このため、設計業務全体を改善したり、経理システムや受発注システムとデータ連携したりする大規模な開発では、システム開発企業に開発作業全体を委託してしまう「受託開発」と呼ばれるビジネスも存在していします。</p>
<p>ここでは、AutoCAD の各 API がどんなものか、何ができるのかを簡単にご紹介していきたいと思います。</p>
<hr />
<p><strong>AutoLISP</strong></p>
<p>AutoCAD のカスタマイズ API として最も長い歴史を持ち、現在も多くの資産が利用されています。AutoLISP は、もともと人口知能の研究用に作られた CommonLISP 言語の特徴であるリスト操作を、CAD 上の図形データに適用させた AutoCAD 固有のプログラム言語です。</p>
<p>AutoLISP を使用するには、テキストエディタで AutoLISPプログラムを作成して拡張子 .lsp ファイルとして保存し、そのファイルを AutoCAD にロードします。AutoLISP を使うと、独自のカスタム コマンドを作成するだけではなく、AutoLISP で再利用可能な AutoLISP 関数も作成することも出来ます。AutoCAD 2021 では、無償の&#0160;<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/04/autolisp-development-using-vs-code.html" rel="noopener" target="_blank">Visual Studio Code を使った編集とデバッグ</a></strong>も出来るようになっています。<br />　 <br />次のコードは、AutoLISP 内で AutoCAD コマンドを呼び出す操作例です。コマンド名は TRY1 としています。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code">(defun C:TRY1 (/ ename)
    (setq ename (entsel &quot;\nオブジェクトを選択:&quot;))
    (command &quot;CHPROP&quot; ename &quot;&quot; &quot;C&quot; &quot;RED&quot; &quot;&quot;)
    (princ)
)
</pre>
<p>また、次のコードは、AutoLISP のリスト操作による DXF データ主体の操作例です。コマンド名は TRY2 としています。同じ処理でも、TRY1 とは違ったアプローチが可能なことがわかります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code">(defun C:TRY2 (/ ename datalist newlist newcolor oldcolor)
    (setq ename (entsel &quot;\nオブジェクトを選択:&quot;))
    (setq datalist (entget (car ename)))
    (setq newcolor (cons 62 1)) ; 1=赤
    (setq oldcolor (assoc 62 datalist))
    (if (= oldcolor nil)
        (setq newlist (append datalist (list newcolor)))
        (setq newlist (subst newcolor oldcolor datalist))
    )
    (entmod newlist)
    (princ)
)
</pre>
<hr />
<p><strong>ActiveX オートメーション/VBA（COM）</strong></p>
<p>ActiveX オートメーションは、Mircosoft の提供するコンポーネント テクノロジ COM (Component Object Model) を利用した開発環境です。COM には、ソフトウェアの持つさまざまな機能を公開する COM サーバー と、公開された機能を参照して再利用する COM クライアント の 2 つの機能があります。AutoCAD の場合、AutoCAD が COM サーバーの役割を持ち、COM クライアント機能を持った VBA や、有償の Microsoft Visual Studio (別売) などから AutoCAD の機能を利用するプログラムを開発することができます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880166126200d-pi" style="display: inline;"><img alt="Com_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880166126200d img-responsive" src="/assets/image_365996.jpg" title="Com_1" /></a></p>
<p>COM は、Windows プラットフォームの共通のアーキテクチャであるため、他のソフトウェアが COM サーバーとして情報を公開していれば、COM クライアントとして AutoCAD VBA から他のアプリケーションをコントロールすることができます。例えば、Microsoft Excel は、自身の機能を COM サーバー として公開しているので、Excel VBA を COM クライアントとして使うことで、簡単に Excel 自身をコントロールすることが出来ます。同時に AutoCAD VBA を COM クライアントとして使うことで、AutoCAD 側からExcel をコントロールすることも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788016612d200d-pi" style="display: inline;"><img alt="Com_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788016612d200d image-full img-responsive" src="/assets/image_542764.jpg" title="Com_2" /></a></p>
<p>VBA では、AutoCAD のカスタム コマンドを直接作成するのではなく、いわゆる VBA マクロ を作成することになります。マクロを実行するためには、VBARUN コマンドを使う必要があります。</p>
<p>次のコードは、VBA の ActiveX オートメーションを用いた操作例です。VBA ではコマンドを定義することができないため、マクロを定義しています。マクロ名は TRY3 としています。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code">Public Sub TRY3()
　
    Dim oEnt As AcadEntity 
    Dim ptPick As Variant 
    ThisDrawing.Utility.GetEntity oEnt, ptPick, &quot;オブジェクトを選択:&quot; 
    Dim oColor As AcadAcCmColor 
    Set oColor = New AcadAcCmColor 
    oColor.ColorIndex = acRed 
    oEnt.TrueColor = oColor 
    oEnt.Update
End Sub

</pre>
<hr />
<p><strong>ObjectARX</strong></p>
<p>C++ 言語を使用するAPI です。AutoCAD R13 からは AutoCAD 自身の開発言語も C++ 言語に切り替わっています。当初、単に ARX (AutoCAD Runtime eXtension の略) と呼ばれていましたが、AutoCAD R14 から名称が変更されて ObjectARX になりました。<br />　<br />ObjectARX を利用するには、ObjectARX SDK を<a href="https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx-license-download">https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx-license-download</a> からダウンロードして入手する必要があります。入手と利用は無償です。なお、C++ 言語で記述したプログラムをビルドするために、有償のMicrosoft Visual Studio を別途購入して、同梱の Visual C++ を使用する必要があります。</p>
<p>ObjectARX は、カスタム コマンド作成のほか、LINE や CIRCLE といった標準オブジェクトと同じように振る舞う カスタム オブジェクト を定義することもできます。VBA や AutoLISP で実現出来ないパレット形式のダイアログを作成することもできます。</p>
<p>次のコードは、ObjectARX 内で AutoCAD コマンドを呼び出す操作例です。AutoLISP の例として紹介したTRY1 コマンド に対応する ObjectARX 版と言えます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code">static void AdskArxGuideTry4(void)
{
    ads_name ename;
    ads_point pt;
    acedEntSel( _T(&quot;\nオブジェクトを選択:&quot;), ename, pt );
    acedCommandS( RTSTR, _T(&quot;CHPROP&quot;),
                  RTENAME, ename,
                  RTSTR, _T(&quot;&quot;),
                  RTSTR, _T(&quot;C&quot;),
                  RTSTR, _T(&quot;RED&quot;),
                  RTSTR, _T(&quot;&quot;),
                  RTNONE );
}
</pre>
<p>次のコードは、先に紹介したTRY2 コマンドに対応する ObjectARX 版で、AutoLISP のリスト処理をリザルトバッファ形式で行なう操作例です。習得しにくいと言われている ポインタ と呼ばれる C++ 言語固有の操作をしています。</p>
<p>※ カスタム オブジェクトを使う必要がなければ、現在では、習得が用意な .NET API（後述）で代替することが出来ます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code">static void AdskArxGuideTry5(void)
{
    ads_name ename;
    ads_point pt;
    acedEntSel( _T(&quot;\nオブジェクトを選択:&quot;), ename, pt );
    struct resbuf *pRb, *pWk;
    pRb = acdbEntGet( ename );
    bool bFlag = true;
    for( pWk=pRb ; pWk-&gt;rbnext!=NULL ; pWk=pWk-&gt;rbnext ){
        if ( pWk-&gt;restype == 62 ){
            pWk-&gt;resval.rint = 1; // 1=赤
            bFlag = false;
        }
    }
    if ( bFlag ){
        pWk-&gt;rbnext = acutNewRb( RTSHORT );
        pWk = pWk-&gt;rbnext;
        pWk-&gt;restype = 62;
        pWk-&gt;resval.rint = 1; // 1=赤
        pWk-&gt;rbnext = NULL;
    }
    acdbEntMod( pRb );
    acutRelRb( pRb );
}
</pre>
<hr />
<p><strong>.NET API</strong></p>
<p>.NET Framework を利用する、AutoCAD 2005 から導入されはじめた比較的新しい API です。ObjectARX と同じように、有償の Microsoft Visual Studio を使ってプログラムを作成・ビルドして、AutoCAD のロードモジュールを作ります。.NET API は、カスタム オブジェクトの定義機能以外、ObjectARX とほぼ同等の機能を持っていて、その操作方法も似ています。</p>
<p>ただ、ObjectARX &#0160;と異なり、.NET API では C# や VB.NET（Visual Basic）の開発言語を任意に選択することができます。どちらの言語でプログラムを作成しても、ビルドして生成される DLL ファイルは、最終的に .NET Framework で理解される中間言語になるので、開発言語による処理速度や機能の差はありません。C++ 言語のポインタ操作も不要です。<br /><br /><br /></p>
<p>　 <br />次のコードは、C# で記述した色変更のサンプルコードです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code">[CommandMethod(&quot;TRY6&quot;)]
public static void Try6()
{
    Database db = HostApplicationServices.WorkingDatabase;
    Transaction tr = db.TransactionManager.StartTransaction();
    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

    try
    {
        PromptEntityResult rlt = ed.GetEntity(&quot;\nオブジェクトを選択:&quot;);
        Entity ent = tr.GetObject(rlt.ObjectId, OpenMode.ForWrite) as Entity;
        ent.ColorIndex = 1;
        tr.Commit();
    }
    catch (Autodesk.AutoCAD.Runtime.Exception ex)
    {
        ed.WriteMessage(&quot;例外エラー:&quot; + ex.Message);
    }
    finally
    {
        tr.Dispose();
    }
}
</pre>
<p>次のコードは、同様の内容を Visual Basic .NET で記述したものです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code">&lt;CommandMethod(&quot;TRY7&quot;)&gt;
Public Shared Sub Try7()
    Dim db As Database = HostApplicationServices.WorkingDatabase
    Dim tr As Transaction = db.TransactionManager.StartTransaction
    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

    Try
        Dim rlt As PromptEntityResult = ed.GetEntity(vbCrLf &amp; &quot;オブジェクトを選択:&quot;)
        Dim ent As Entity = tr.GetObject(rlt.ObjectId, OpenMode.ForWrite)
        ent.ColorIndex = 1
        tr.Commit()
    Catch ex As Autodesk.AutoCAD.Runtime.Exception
        ed.WriteMessage(&quot;例外エラー:&quot; + ex.Message)
    Finally
        tr.Dispose()
    End Try
End Sub

</pre>
<hr />
<p><strong>JavaScript API</strong></p>
<p>AutoCAD 2014 で 5 番目の API として登場した JavaScript API は、AutoCAD にとって、初めて Web 開発者に AutoCAD カスタマイズの門戸を開く役割を担います。</p>
<p>インターネット環境のクライアント プラットフォームという視点で考えるなら、Web ブラウザをプラットフォームとして考えることが出来ると思います。そんな Web プラットフォームを AutoCAD に取り込んだのが、AutoCAD JavaScript API です。Web を中心に開発をしている開発者の中には、クラウド コンピューティングに精通している方もいらっしゃるかと思います。Web プラットフォームから AutoCAD にチャレンジしていただければ、少しはハードルを下げられるのでは、という期待もあります。経験豊富な Web 開発者の方に、オートデスクが推進している <a href="https://adndevblog.typepad.com/technology_perspective/2018/05/about-autodesk-forge.html" rel="noopener" target="_blank"><strong>Forge</strong></a> を使った開発を担っていただけたらうれしい限りです。</p>
<p>AutoCAD JavaScript API もカスタムコマンドを含め、一部の機能のカスタマイズが可能です。</p>
<hr />
<p>最後に、AutoCAD にも Web テクノロジが採用されている解かりやすい例をご紹介します。AutoCAD の [スタート] タブが表示されている状態で、キーボードの [F12] キーを押してみてください。Web ブラウザの時と同じく、<a href="https://adndevblog.typepad.com/technology_perspective/2018/10/about-developer-tool-on-web-browser.html" rel="noopener" target="_blank"><strong>デベロッパーツール</strong></a>が表示さるはずです。これは、JavaScript API を実現するために <a href="https://ja.wikipedia.org/wiki/WebKit" rel="noopener" target="_blank">WebKit</a> を採用しているためです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdebe819c200c-pi" style="display: inline;"><img alt="Webkit" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdebe819c200c image-full img-responsive" src="/assets/image_648379.jpg" title="Webkit" /></a></p>
<p>AutoCAD 内部のカスタマイズに注力する AutoLISP や ObjectARX から、コンピュータ内の他のアプリケーションとの相互連携を可能にする ActiveX オートメーション、ネットワークやインターネット接続も視野に入れた .NET API や JavaScript API まで、AutoCAD API は進化し続けています。<a href="https://adndevblog.typepad.com/technology_perspective/2018/05/about-autodesk-forge.html" rel="noopener" target="_blank"><strong>Forge</strong></a> の登場で AutoCAD の&#0160; JavaScript API の広がりは限定的な状態ですが、用途に応じてさまざまに使い分けることが出来ます。</p>
<p>作成したアドイン アプリケーションは、<a href="https://apps.autodesk.com/ja" rel="noopener" target="_blank"><strong>Autodesk App Store</strong></a> で公開することも出来ます。有償版の公開には、別途、PayPal や BlueSnap の決済サービスのアカウントを作成していただく必要がありますが、App Store の公開自体にコストはかかりません。<strong><a href="https://www.autodesk.co.jp/developer-network/app-store" rel="noopener" target="_blank">無償で公開</a></strong>することが出来ます。</p>
<p>By Toshiaki Isezaki</p>
