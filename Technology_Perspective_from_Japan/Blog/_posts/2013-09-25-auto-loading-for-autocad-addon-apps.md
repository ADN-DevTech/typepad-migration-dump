---
layout: "post"
title: "AutoCAD アドオンの自動ロードの方法あれこれ"
date: "2013-09-25 01:26:09"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/09/auto-loading-for-autocad-addon-apps.html "
typepad_basename: "auto-loading-for-autocad-addon-apps"
typepad_status: "Publish"
---

<p>AutoCAD API でアドオン アプリケーションを開発している段階では、主に <a href="http://docs.autodesk.com/ACD/2014/JPN/files/GUID-47621BB1-F29D-4A69-9C99-A6E1495FBA38.htm" rel="noopener" target="_blank">APPLOAD[アプリケーション ロード] コマンド</a>&#0160;や <a href="http://docs.autodesk.com/ACD/2014/JPN/files/GUID-D954BCC1-C4F0-488B-8F60-1D02D68940E0.htm" rel="noopener" target="_blank">NETLOAD[.NET アプリケーション ロード] コマンド</a>、VBALOAD コマンド、あるいは、Visual Studio や Visual LISP などの開発環境からデバッグ機能を利用して、アドオン アプリケーションが正しく動作するか確認するのが一般的かと思います。</p>
<p>ただ、開発済のアドオン アプリケーションを設計者のコンピュータ上で設計者自身が利用する場合には、APPLOAD コマンドなどをいちいち実行してアプリケーションをロードさせるのは非現実的です。設計環境の生産性や効率を高めるために使用する AutoCAD API の効果が薄れてしまい、本末転倒です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4bb6400200b-pi" style="display: inline;"><img alt="6a0167607c2431970b019aff8d7683970b" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4bb6400200b image-full img-responsive" src="/assets/image_51790.jpg" title="6a0167607c2431970b019aff8d7683970b" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff8d7683970b-pi" style="display: inline;"></a>そこで、AutoCAD には、開発したアドオン アプリケーションを自動的にロードさせるいくつかの方法が用意されています。</p>
<p><strong>スタートアップ 登録の利用</strong><strong><br /></strong></p>
<p style="padding-left: 30px;">APPLOAD コマンドにボタンのある [スタートアップ登録] 機能を使って、AutoCAD の起動時に自動ロードするアドオン アプリケーションを登録する方法です。AutoLISP/Visual LISP、VBA、ObjectARX アプリケーションを指定することが出来ます。詳細は、<strong><a href="http://docs.autodesk.com/ACD/2014/JPN/files/GUID-B38F610B-51FB-4938-BDEC-A0A737F5DB6C.htm" rel="noopener" target="_blank">こちら</a></strong> に記載されています。</p>
<p><strong>acad.lsp、acaddoc.lsp の利用</strong></p>
<p style="padding-left: 30px;">AutoLISP には、AutoCAD の起動時に1回自動的にロードされる <strong>acad.lsp</strong> と、図面を開くたびに自動的にロードされる <strong>acaddoc.lsp</strong> が用意されています。特に、acad.lsp は AutoLISP の登場当初から利用されている最も古いアプリケーションのロード方法です。ロードされた acad.lsp や acaddoc.lsp では、(S::STARTUP) 関数を使って処理を自動的に実行させるのが一般的です。このときに、AutoLISP の (load) 関数で AutLISP アプリケーションを、(arxload) 関数で ObjectARX アプリケーションをロードさせることができます。また、(command) 関数で NETLOAD コマンドや VBALOAD コマンドを実行させることで、アプリケーションをロードすることもできます。ただし、この場合は、システム変数 FILEDIA を 0 にして、ファイル選択ダイアログ ボックスの表示を抑止する必要があります。最近では、コマンド実行時にロードをさせるための (autoload) 関数や (autoarxload) 関数も用意されています。acad.lsp や acaddoc.lsp を利用した自動ロードの方法は、<strong><a href="http://docs.autodesk.com/ACD/2014/JPN/files/GUID-FDB4038D-1620-4A56-8824-D37729D42520.htm" rel="noopener" target="_blank">こちら</a></strong> に記載されています。コンパイル済の acad.fas、acaddoc.fas、acad.vlx、acaddoc.vlx でも同様の処理が可能です。</p>
<p style="padding-left: 30px;">なお、絶対パスを指定してアプリケーションの位置を指定しない場合も含め、acad.lsp や acaddoc.lsp と、それら内部でロード指定するアドオン アプリケーション ファイルは、AutoCAD の<a href="http://docs.autodesk.com/ACD/2014/JPN/files/GUID-25616676-EFF2-4060-A06F-19112622A78B.htm" rel="noopener" target="_blank">サポート検索パス</a>のいずれかのフォルダに配置されている必要があります。</p>
<p style="padding-left: 30px;">最近では、<strong><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=2568" rel="noopener" target="_blank">ウィルス</a></strong>の温床になる懸念から、あまり利用されていません。</p>
<p><strong>acad.dvb を利用</strong></p>
<p style="padding-left: 30px;">AutoCAD が起動するたびに同動的にロードされる VBA プロジェクトです。acad.dvb 内に を AutoCAD のサポート検索パスのいずれかに配置して、AcadStartup マクロを登録しておけば、AutoCAD の起動時に記述した処理を自動的に実行させることができます。ただし、acad.lsp などを利用して VBA を制御している acadvba.arx ファイルをロードしなければなりません。詳細は、<strong><a href="http://docs.autodesk.com/ACD/2014/JPN/files/GUID-30F06535-7FB5-4F03-A0EE-96973836E231.htm" rel="noopener" target="_blank">こちら</a></strong> に記述されています。</p>
<p style="padding-left: 30px;">最近では、<strong><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=2568" rel="noopener" target="_blank">ウィルス</a></strong>の温床になる懸念から、あまり利用されていません。</p>
<p><strong>acad.rx を利用</strong></p>
<p style="padding-left: 30px;">acad.rx ファイル内に自動ロードしたい ObjectARX プリケーション名記述してサポート検索パスに配置しておけば、AutoCAD の起動時に列挙された ObjectARX アプリケーションを自動ロードしてくれます。acad.rx については、<strong><a href="http://docs.autodesk.com/ACD/2014/JPN/files/GUID-409E18E6-7164-41CB-A188-97E79E42BC5A.htm" rel="noopener" target="_blank">こちら</a></strong> で紹介されています。</p>
<p><strong>システム レジストリを使ったディマンド ロードを利用&#0160;</strong></p>
<p style="padding-left: 30px;">ディマンド ロードは、システム レジストリを利用して AutoCAD にロードされていない ObjectARX アプリケーションや .NET API アプリケーションを自動的にロードする AutoCAD の機能です。システム レジストリを利用するため、通常はインストーラを作成して、インストーラによってレジストリ書き込みなどさせる必要があるため、比較的高度な知識を必要とします。ただし、ここまで紹介したロード機能よりも、多様なロード タイミングを選択することができます。</p>
<ul>
<li>ロードされていないアプリケーションによって作成されたカスタム オブジェクトを含む図面ファイルが読み込まれたとき</li>
<li>ユーザまたは他のアプリケーションが、非常駐アプリケーションのコマンドを呼び出したとき</li>
<li>AutoCAD を起動したとき</li>
</ul>
<p style="padding-left: 30px;">Autodesk では、AutoCAD のディマンド ロード機能を利用する ObjectARX アプリケーションの開発を推奨しています。ディマンド ロードには、次のような利点があります。</p>
<ul>
<li>プロキシ オブジェクト(プロキシ オブジェクトを参照)の作成を制限します。</li>
<li>ObjectARX アプリケーションのロードに、より一層の柔軟性を提供します。</li>
<li>機能が必要になった場合にのみアプリケーションがロードされ、メモリが節約されます。</li>
</ul>
<p style="padding-left: 30px;">ディマンド ロード可能なアプリケーションにするには、アプリケーションの情報が正しくシステム レジストリ上に記入されていて、かつ、DEMANDLOAD システム変数が、ディマンド ロードに適した値に設定する必要があります。</p>
<p style="padding-left: 30px;">ディマンド ロードの詳細は、<strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA93g00000008Sm.html" rel="noopener" target="_blank">こちら</a>&#0160;</strong>からダウンロード可能な ObjectARX Developers Guide（日本語） の ディマンド ロード セクションを参照してください。</p>
<p><strong><strong>バンドル パッケージを使った自動ロードを利用&#0160;</strong></strong></p>
<p style="padding-left: 30px;">バンドルと呼ばれる XML パッケージ（<strong>PackageContents.xml</strong>）を利用した自動ロードメカニズムで AutoCAD 2012 で導入された比較的新しいロード方法です。AutoCAD 2014以降では、<strong><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=7977" rel="noopener" target="_blank">セキュア ロード メカニズム</a>&#0160;</strong>による警告ダイアログを唯一、既定で防止することが可能です。</p>
<p style="padding-left: 30px;">これからアドオン アプリケーションを開発してロード方法を模索されている方には、ウィルス対策を考慮したこちらの方法をお勧めします。ただし、AutoCAD 2014 では VBA プロジェクトや JavaScript API モジュールを XML パッケージで利用することは出来ません。</p>
<p style="padding-left: 30px;">バンドル パッケージを用いたロード方法は、<a href="http://adndevblog.typepad.com/technology_perspective/2013/07/japanese-autodesk-exchange-apps-store.html" rel="noopener" target="_blank">Autodesk Exchange Apps ストア</a>に記載するアドオン アプリケーション用にも推奨されています。</p>
<p style="padding-left: 30px;">バンドル パッケージの詳細は、<a href="http://docs.autodesk.com/ACD/2014/JPN/files/GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008.htm" rel="noopener" target="_blank"><strong>こちら</strong> </a>に記載されています。</p>
<p>アドオン アプリケーションのロード方法が多過ぎるように感じるかも知れませんが、AutoCAD の歴史を考えれば極めて妥当です。つまり、API の登場時期や当時のテクノロジにあわせて拡張されてきたことがわかります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff8ded3a970c-pi" style="display: inline;"><img alt="APIHistory" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff8ded3a970c image-full" src="/assets/image_818888.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="APIHistory" /></a><br />可能であれば、最後に紹介した バンドル パッケージによる自動ロード を採用していただくことをお勧めします。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
