---
layout: "post"
title: "Design Automation for Revit デバッグツールのご紹介"
date: "2020-11-13 20:14:19"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/11/design-automation-for-revit-local-debug-tool.html "
typepad_basename: "design-automation-for-revit-local-debug-tool"
typepad_status: "Publish"
---

<p>今回は、Design Automation for Revit 用に実装したアドインをローカルにインストールされているデスクトップ版 Revit でデバッグするためのツールをご紹介します。</p>
<p>このツールは、デスクトップ版 Revit のアドイン（外部コマンド）の Visual Studio プロジェクトとして GitHub にて公開されております。</p>
<p>下記のページから Visual Studio プロジェクトをダウンロードするか、Git for Windows をインストールしてコマンドプロンプトから git clone コマンドを通じて入手してご利用いただけます。</p>
<ul>
<li><a href="https://github.com/Autodesk-Forge/design.automation-csharp-revit.local.debug.tool">Design Automation for Revit - Local debug tool</a></li>
</ul>
<p style="padding-left: 40px;"><span style="background-color: #e6e6e6;">git clone https://github.com/Autodesk-Forge/design.automation-csharp-revit.local.debug.tool.git</span></p>
<p><br /><strong>前提条件</strong></p>
<ul>
<li>Forge アカウントは、Design Automation for Revit をご利用いただく際に必要ですが、このツールをお試し頂くだけでしたら必要ありません。</li>
<li>Visual Studio 2019 以降がインストールされていることと、.NET Framework と C# の知識が必要です。</li>
<li>Revit 2018 ～ 2021でいずれかのバージョンがインストールされていること。</li>
<li>Design Automation for Revit 用に実装したアドインの Visual Studio プロジェクト。</li>
</ul>
<p>&#0160;</p>
<p><strong>ビルド手順</strong></p>
<ol>
<li>DesignAutomationHandler ソリューションを開きます。各 Revit のバージョン毎にプロジェクトがありますので、デバッグしたいアドインの Revit バージョンのプロジェクトを展開します。現在、Design Automation for Revit は Revit 2018, 2019, 2020, 2021 に対応しております。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4238a2e200d-pi" style="display: inline;"><img alt="DesignAutomationHandler_01" class="asset  asset-image at-xid-6a0167607c2431970b026be4238a2e200d img-responsive" src="/assets/image_523761.jpg" title="DesignAutomationHandler_01" /></a><br /><br /></li>
<li>プロジェクトの参照を展開すると、DesignAutomationBridge ライブラリがインストールされていない状態となっているため、NuGet パッケージマネージャーを開いて、<a href="https://www.nuget.org/packages/Autodesk.Forge.DesignAutomation.Revit">Design Automation Bridge for Revit </a>をインストールします。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9773ec6200b-pi" style="display: inline;"><img alt="DesignAutomationHandler_02" class="asset  asset-image at-xid-6a0167607c2431970b0263e9773ec6200b img-responsive" src="/assets/image_58206.jpg" title="DesignAutomationHandler_02" /></a><br /><br />※ 下記のように[復元]ボタンが表示されている場合は、押下して再インストールすることもできます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdea49a1e200c-pi" style="display: inline;"><img alt="DesignAutomationHandler_03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdea49a1e200c image-full img-responsive" src="/assets/image_399627.jpg" title="DesignAutomationHandler_03" /></a><br /><br /></li>
<li>DesignAutomationHandler 20xx プロジェクトをビルドします。<br />ビルド後イベントのスクリプトで、自動的にリソースが %APPDATA%\Autodesk\Revit\Addins\20xx フォルダ配下にコピーされます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdea49a36200c-pi" style="display: inline;"><img alt="DesignAutomationHandler_04" class="asset  asset-image at-xid-6a0167607c2431970b026bdea49a36200c img-responsive" src="/assets/image_219741.jpg" title="DesignAutomationHandler_04" /></a><br /><br /></li>
<li>同じフォルダに Design Automation for Revit 用に作成したアドインとその関連アセンブリ、アドインマニフェストファイルを配置します。<br /><br />DesignAutomationHandler アドインは、2つ以上のアドインを同時にテスト・デバッグすることはできませんのでご注意ください。また、DesignAutomationBridge.dll アセンブリも同じフォルダに配置されているか確認してください。<br /><br /></li>
<li>Design Automation for Revit 用に作成したアドインの Visual Studio プロジェクトを開き、プロパティの [デバッグ] タブの [外部プログラムの開始] プロパティで Revit.exe ファイルのパスを指定します。<br /><br /></li>
</ol>
<p><strong>利用手順</strong></p>
<p>ここでは、Design Automation for Revit 2021 向けに作成した CountIt アドインをサンプルとして利用する手順をご紹介します。</p>
<ol>
<li>Design Automation for Revit 用に作成したアドイン（ここでは CountIt アドイン）を Visual Studio のデバッグモードで実行して Revit を起動します。<br /><br /></li>
<li>Design Automation for Revit のアドインで使用する Revit プロジェクトファイルを開きます。<br /><br />CountIt アドインでは、プロジェクトに配置されている要素の要素数をカテゴリ別に集計するシンプルなアドインのため、任意の Revit プロジェクトでお試し頂けます。<br /><br /></li>
<li>Design Automation の Activity で入力値に JSON パラメータを定義している場合は、WorkItem で JSON データを渡すことを想定されているはずです。その場合は、.json ファイルを作成して、開いている Revit プロジェクトファイルが配置されているフォルダと同じ場所に配置します。<br /><br />例えば、Activity で JSON の入力値パラメータを CountItParams という名前で定義している場合は、CountItParams.json というファイル名になります。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9773f19200b-pi" style="display: inline;"><img alt="DesignAutomationHandler_05" class="asset  asset-image at-xid-6a0167607c2431970b0263e9773f19200b img-responsive" src="/assets/image_84391.jpg" title="DesignAutomationHandler_05" /></a><br />CountItParams.json では、下記のように、どの要素カテゴリをカウントするかを True / False で指定します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9773f24200b-pi" style="display: inline;"><img alt="DesignAutomationHandler_06" class="asset  asset-image at-xid-6a0167607c2431970b0263e9773f24200b img-responsive" src="/assets/image_151985.jpg" title="DesignAutomationHandler_06" /></a><br /><br /></li>
<li>Revit のアドインタブにある[外部ツール]から[DesignAutomationHandler]コマンドを実行します。すると、自動的に CountIt アドインが実行され、処理の終了後に通知ダイアログが表示されます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdea49ad6200c-pi" style="display: inline;"><img alt="DesignAutomationHandler_11" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdea49ad6200c image-full img-responsive" src="/assets/image_656527.jpg" title="DesignAutomationHandler_11" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4238ac6200d-pi" style="display: inline;"><img alt="DesignAutomationHandler_07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be4238ac6200d img-responsive" src="/assets/image_2219.jpg" title="DesignAutomationHandler_07" /></a><br /><br /></li>
<li>カウント結果は、result.txt というファイル名で CountIt アドインのコードで指定している場所に保存されます。基本的には、開いている Revit プロジェクトファイルを同じフォルダ配下に保存されます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdea49abf200c-pi" style="display: inline;"><img alt="DesignAutomationHandler_08" class="asset  asset-image at-xid-6a0167607c2431970b026bdea49abf200c img-responsive" src="/assets/image_85271.jpg" title="DesignAutomationHandler_08" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9773f47200b-pi" style="display: inline;"><img alt="DesignAutomationHandler_09" class="asset  asset-image at-xid-6a0167607c2431970b0263e9773f47200b img-responsive" src="/assets/image_66349.jpg" title="DesignAutomationHandler_09" /></a><br /><br /></li>
<li>デバッグモードで起動している場合は、CounIt アドインの Visual Studio でブレークポイントを設定すれば、処理のデバッグに入ることができます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4238afa200d-pi" style="display: inline;"><img alt="DesignAutomationHandler_10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be4238afa200d image-full img-responsive" src="/assets/image_521989.jpg" title="DesignAutomationHandler_10" /></a></li>
</ol>
<p>今回、利用手順で使用した CountIt アドインの Visual Studio プロジェクトは、こちらからダウンロードしてご利用頂けます。</p>
<ul>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b026bdea49ae9200c img-responsive"><a href="https://adndevblog.typepad.com/files/countit.zip">CountItをダウンロード</a></span></li>
</ul>
<p>なお、Deisng Automation API for Revit につきましては、Autodesk University 2020 の下記のクラスでも詳しく解説しております。英語クラスに加え、日本語クラスを設けておりますので、ご興味ある方は、ぜひご視聴ください。</p>
<ul>
<li><a href="https://www.autodesk.com/autodesk-university/ja/class/Design-Automation-Revit-Beyond-Basics-2020">SD473692: Design Automation for Revit: Basics and beyond</a></li>
<li><a href="https://www.autodesk.com/autodesk-university/ja/class/Design-Automation-Revit-jichukarayingyonghe-2020">SD473594: Design Automation for Revit: 基礎から応用へ</a></li>
</ul>
<p>By Ryuji Ogasawara</p>
