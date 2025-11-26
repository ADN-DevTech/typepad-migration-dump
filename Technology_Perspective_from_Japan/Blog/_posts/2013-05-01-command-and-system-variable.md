---
layout: "post"
title: "コマンドとシステム変数"
date: "2013-05-01 02:12:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/05/command_and_system_variable.html "
typepad_basename: "command_and_system_variable"
typepad_status: "Publish"
---

<p>今回は、AutoCAD のカスタマイズの基礎ともいえる <strong>コマンド</strong> と <strong>システム変数</strong> についてご紹介しましょう。どちらも、AutoCAD のカスタマイズでは非常に重要です。特に、AutoCAD API を用いてカスタム コマンドを作成する場合には、コマンドの特性をよく理解することで、カスタム コマンドの振る舞いや定義時の指定を理解し易くなります。</p>
<p><strong>コマンド</strong></p>
<p style="padding-left: 30px;">AutoCAD のコマンドは、作図や印刷、3D モデリングなどのさまざまな処理の最小単位です。目的に合わせたコマンドを順次実行することで、図面を完成させることができます。AutoCAD の標準コマンドでは、線分や円の作図で使用する LINE[線分] コマンドや CIRCLE[円] コマンド、図形の移動や複写で使用する MOVE[移動] コマンドや COPY[複写] コマンドなど多種多様なものが最初から用意されています。そして、これらのコマンドを組み合わせて 1 つのカスタム コマンドを作成することもできます。AutoCAD API と利用したカスタマイズでは、独自の処理内容を実装するカスタム コマンドを作成することを目的とするのが一般的です。</p>
<p style="padding-left: 30px;">現在の AutoCAD では、リボン や ツールバーにコマンドを割り当てて、ボタンをクリックすることでコマンドを実行します。もちろん、DOS 版からの AutoCAD の特性も継承しているので、コマンド ライン ウィンドウから直接コマンドを入力して実行することもできます。</p>
<p style="padding-left: 30px;"><strong>実行コンテキスト</strong></p>
<p style="padding-left: 30px;">さて、コマンドの実行に必要な <strong>実行コンテキスト</strong> という要素をご存じでしょうか？AutoCAD API、特に、ObjectARX や AutoCAD .NET API を作成する際には、実行コンテキストを正しく理解しておく必要があります。</p>
<p style="padding-left: 30px;">実行コンテキストは、簡単に言うと「コマンドを動作させる内部的な環境」のことです。多くの場合、コマンドを実行するために、AutoCAD の画面上に図面が表示されていることが前提になります。言い換えれば、コマンドの実行は、ドキュメント(図面を表示する MDI 子ウィンドウ) に依存します。</p>
<p style="padding-left: 30px;">通常、AutoCAD のコマンドを実行は、コマンドは現在アクティブなドキュメントに表示されている図面に作用します。このようなコマンドは<strong>、ドキュメント実行コンテキスト</strong> で動作するコマンドと考えてください。ドキュメント実行コンテキストで動作するコマンドは、コマンドの実行前と実行後で、対象とするドキュメントが同一である点に注意してください。AutoCAD の標準コマンドや、API を使って作成するカスタム コマンドのほとんどは、このドキュメント実行コンテキストで動作します。ドキュメント実行コンテキスト コマンドは、コマンド実行中にアクティブなドキュメントを切り替えても、ドキュメント毎に独立して実行状態を維持できる特徴があります。例えば、Drawing1.dwg を表示したドキュメントに LINE コマンドを使って線分を作図している途中でも、Drawing2,dwg を表示するドキュメントをアクティブにして、別のコマンドを入力できます。その後、Drawing1,dwg に表示を切り替えると、入力途中だった LINE コマンドを継続することができます。</p>
<p style="padding-left: 30px;">一方、コマンド実行前と後のドキュメントが異なるようなコマンドも存在します。新規図面を作成する NEW [新規作成] コマンドや OPEN [開く] コマンドが、その代表例です。このようなコマンドを <strong>アプリケーション実行コンテキスト</strong> で動作するコマンドといいます。</p>
<p style="padding-left: 30px;">カスタムコマンドを作成する場合には、コマンド フラグと呼ばれる値をコマンド定義に加えることで、定義するコマンドをドキュメント実行コンテキストで実行させるか、アプリケーション実行コンテキストで実行させるかを指定できます。具体的には、ObjectARX でアプリケーション実行コンテキスト コマンドを定義する場合にはコマンド フラグに ACRX_CMD_SESSION を、AutoCAD .NET API で同様の定義をする場合には Session をそれぞれ指定するしなければなりません。これらを特に指定しなければ、コマンドはドキュメント実行コンテキストで実行されます。</p>
<p style="padding-left: 30px;">このような状況でカスタム コマンドの問題が発生しがちです。ドキュメント実行コンテキストで定義したカスタム コマンド内で OPEN コマンドを呼び出すことを考えてみましょう。そのカスタム コマンドを実行すると、コマンドの実行途中で実行コンテキストが開いたドキュメントに移動することになります。つまり、対象となるドキュメントが変わってしまします。AutoCAD は、ドキュメント実行コンテスト コマンドが、途中で対象ドキュメントを変更する処理を許容しないため、ドキュメント実行コンテキストで定義したカスタム コマンドの中で OPEN コマンドを呼び出すと、その処理は中断してしまいます。このような場面では、このカスタム コマンドはアプリケーション実行コンテキストで定義されるべきです。</p>
<p style="padding-left: 30px;">図面を1つ1つ AutoCAD に開きながら自動製図や印刷を繰り返すコマンドを作成する場合には、明示的にアプリケーション実行コンテキストで実行されるような指定をしないと、処理途中でコマンドの実行が中断していまいますので注意してください。</p>
<p style="padding-left: 30px;">もう1点、アプリケーション実行コンテキストで定義したコマンド内で図面データベースにアクセスするような場合は、必ず対象となるドキュメントを特定する意味で、<strong>ドキュメント ロック</strong> の処理を挿入するようにしてください。アプリケーション実行コンテキストで動作するコマンドは、そのままだとドキュメントに依存しないため、明示的に作業対象のドキュメント、すなわち、ドキュメントに表示されている図面データベースを明示的に指定しなければなりません。ちょうど、<a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=7789">http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=7789</a>&#0160;のように、モードレス ダイアログからドキュメントにアクセスするようなケースと同じ理由です。</p>
<p style="padding-left: 30px;"><strong>コマンド グループ</strong></p>
<p style="padding-left: 30px;">ネイティブ コマンドとは、AutoCAD の標準コマンドと同等の働きをするコマンドを指します。ここで言う同等とは、コマンド名の前に<strong> &#39;</strong> を付加して割り込みコマンドとして動作することが可能で、AutoLISP の (command) 関数 や ObjectARX の acedCommand() 関数からの呼び出しが可能であることを意味します。ObjectARX や AutoCAD .NET API アプリケーションでは、主にこの方法でコマンドを登録を行ないます。一方、AutoCAD には、このネイティブコマンドとは別に AutoLISP の (defun) 関数や ObjectARX の acedDefun() 関数で定義する AutoLISP コマンドが存在しています。特に理由がない限り、現在では AutoLISP コマンドを利用することは稀なので、ここでは、AutoLISP コマンドについての説明は省略しておきます。</p>
<p style="padding-left: 30px;">1 つの ObectARX ファイルや .NET API ファイルで複数のコマンドを定義することができますが、各コマンドは、他のアプリケーション ファイルが定義するコマンドと重複を避ける必要があります。さもないと、せっかく実装したコマンドで、他社が定義したアプリケーションの同一コマンドが実行されています可能性があります。ネイティブ コマンドの登録時には、コマンド グループも登録できるので、企業単位やモジュール単位で一意となるコマンド グループを登録するようにしてください。コマンド グループを利用することで、同じコマンド名でコマンドが重複登録されてしまった場合でも、コマンドの呼び出し方法で、問題を回避する手法を提供します。</p>
<p style="padding-left: 30px;">AutoCAD は、ObjectARX や .NET API の各種カスタム アプリケーションが定義するコマンドを、カスタム アプリケーションがロードされた順番にスタック管理しています。ユーザがコマンドを実行のためにコマンド名を入力したり、ボタンをクリックしてコマンド名が呼び出されると、AutoCAD は、スタックの上部からコマンドを見つけにいきます。そして、そのコマンド名を見つかった段階でそのコマンドを実行をします。</p>
<p style="padding-left: 30px;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eeaaa1745970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="コマンド" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017eeaaa1745970d image-full" src="/assets/image_235299.jpg" title="コマンド" /></a><br />この仕組みでは、同じコマンド名を定義するアプリケーションが複数ロードされている場合、最後にロードされたアプリケーションのコマンドが実行されることになります。ここでは、先にロードされているアプリケーションのコマンドを呼び出すために、ピリオドを挟んでコマンド グループ名とコマンド名を一緒に呼び出すことでができます。次の例では、GRP1 コマンド グループ名と GRP2 コマンド グループ名を使って、それぞれの TEST コマンドを呼び出した場合のコマンド ライン ウィンドウでの表示です。</p>
<p style="text-align: center; padding-left: 30px;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eeaaa2bb0970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="コマンドグループ実行" class="asset  asset-image at-xid-6a0167607c2431970b017eeaaa2bb0970d" src="/assets/image_102376.jpg" title="コマンドグループ実行" /></a>&#0160;</p>
<p style="padding-left: 30px;">このように、カスタマイズを前提としてコマンドを見ると、意味深な要素が含まれることが理解いただける思います。&#0160;</p>
<p><strong>システム変数</strong></p>
<p style="padding-left: 30px;">システム変数は、AutoCAD が持つ様々なコマンドが内部的に使用する値です。システム変数を知ることで、AutoCAD をどこまで設計者の望むものにカスタマイズできるのか、その目安を把握できます。システム変数には、読み取り専用のものもありますが、多くは SETVAR[変数設定] コマンドを使って値を上書き設定することができます。また、API からもシステム変数の値を設定したり取得したりする機能を提供しています。これによって、カスタム コマンドの中からシステム変数の値を変更してしまうことできます。</p>
<p style="padding-left: 30px;">少しシステム変数の例を見ておきましょう。たとえば、UNITS[単位管理]コマンドで設定する内容は、システム変数 LUNITS、LUPREC、AUNITS、AUPREC、ANGDIR、INSUNITS、LIGHTINGUNITS の各値に反映されます。</p>
<p style="text-align: center; padding-left: 30px;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eea115736970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"></a>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c386e10b4970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="System_Variable" class="asset  asset-image at-xid-6a0167607c2431970b017c386e10b4970b" src="/assets/image_164553.jpg" title="System_Variable" /></a></p>
<p style="padding-left: 30px;">また、コマンド操作では直接用意されていない機能も、システム変数を設定することで実現できる場合があります。たとえば、” 2D ワイヤフレーム” 表示スタイルの状態でシステム変数 DISPSILH と ISOLINES の値を変更すると、同じ視点でも、3D ソリッドの球を外形線だけを表示させることができるようになります。</p>
<p style="text-align: center; padding-left: 30px;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c386e1136970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Solid_Style" class="asset  asset-image at-xid-6a0167607c2431970b017c386e1136970b" src="/assets/image_402802.jpg" title="Solid_Style" /></a></p>
<p style="padding-left: 30px;">そのほか、システム変数 SDI を 1 に設定すると、AutoCAD R14 までの AutoCAD と同じように、1つの図面しか開けないような環境(シングル ドキュメント インタフェース：SDI) に変更することもできます。この機能は、API を利用したプログラム資産の動作互換のためのものですが、複数の図面を開くことができなくなるので、現在の AutoCAD ユーザには好まれません。</p>
<p style="padding-left: 30px;">このように、システム変数の変更で、AutoCAD の振る舞いをコントロールすることができるようになります。&#0160; 特に、API によるカスタマイズを考えられているユーザは、一度、どのようなシステム変数があるか チェックすることをお勧めします。</p>
<p style="padding-left: 30px;">前述のとおり、システム変数はカスタム コマンドの中で呼び出すこともできるので、カスタム コマンド実行時に、必要に応じて作図環境を変えていくこともできるのです。</p>
<p>コマンドとシステム変数は、AutoCAD のバージョンアップに沿って新しく登場したり、場合によっては削除されたものがあります。カスタム アプリケーションをお持ちの方は、AutoCAD のバージョン アップに沿った移植作業で、この情報が重要になる場合があるはずです。最後に、AutoCAD R14 時代からのコマンドとシステム変数の履歴をご提供しておきましょう。この一覧は、昨年オートデスクを退社された高村さんから引き継いだものに AutoCAD 2014 の情報を付け加えたものです。高村さん、ありがとうございました !!</p>
<p><strong>AutoCAD R14～2014 コマンド履歴：</strong>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017eeaaa3d09970d"><a href="http://adndevblog.typepad.com/files/%E3%82%B3%E3%83%9E%E3%83%B3%E3%83%89%E5%B1%A5%E6%AD%B4.pdf">コマンド履歴をダウンロード</a></span></p>
<p><strong>AutoCAD R14～2014 システム変数履歴：
</strong><span class="asset  asset-generic at-xid-6a0167607c2431970b019101a29f89970c"><a href="http://adndevblog.typepad.com/files/%E3%82%B7%E3%82%B9%E3%83%86%E3%83%A0%E5%A4%89%E6%95%B0%E5%B1%A5%E6%AD%B4.pdf">システム変数履歴をダウンロード</a></span></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
、
