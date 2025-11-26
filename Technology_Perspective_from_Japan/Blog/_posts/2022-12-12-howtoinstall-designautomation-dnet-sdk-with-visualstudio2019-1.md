---
layout: "post"
title: "Visual Studio 2019でAutodesk.Forge.DesignAutomation .net SDK （.NET 6 ) を利用する方法"
date: "2022-12-12 00:03:11"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/12/howtoinstall_designautomation_dnet_sdk_with_visualstudio2019-1.html "
typepad_basename: "howtoinstall_designautomation_dnet_sdk_with_visualstudio2019-1"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a9ee89200b-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02acc60f8aaf200b-pi" style="display: inline;"><img alt="image from adndevblog.typepad.com" border="0" class="asset  asset-image at-xid-6a025d9b32eb0b200c02af1c939c4d200d image-full img-responsive" src="/assets/image_215636.jpg" title="image from adndevblog.typepad.com" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a9ee89200b-pi" style="display: inline;"><br /></a></p>
<p>ご承知のように、APS (=Autodesk Platform Service 旧Forge）のAPIはWebサービスという形でAPIを公開しており、HTTP/HTTPSのリクエストを送信しレスポンスを受け取るという形式でAPIを利用することが出来ます。</p>
<p>これは、インターネット越しにAPIを公開するという点においては、既存のWebの仕組みを利用することが出来るため非常に便利である反面、プログラムからWebサービスAPIを実行するにあたっては、HTTPの書式に合わせてリクエストを構築、レスポンスを受信して結果を処理する必要があるため、プログラムの記述が複雑でわかりにくくなる傾向があります。</p>
<p>APSのWebサービスAPIでは、このWebサービスAPIをプログラムから実行する際のわずらわしさを軽減し、親和性高く記述することを可能とするSDKが、各言語向けに提供されております。</p>
<p>そのようなSDKの一つとして、APSのDesign Automation APIを.net プログラムから使用する際に利用可能なAutodesk.Forge.DesignAutomation .net SDK が公開されております。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://github.com/Autodesk-Forge/forge-api-dotnet-design.automation" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 200px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_544577.jpg" style="width: 100%; height: auto; max-height: 200px; min-width: 0; border: 0 none; margin: 0;" width="200" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">GitHub - Autodesk-Forge/forge-api-dotnet-design.automation: Forge Design Automation .NET SDK: Provides .NET SDK to help you easily integrate Forge Design Automation v3 REST APIs into the application</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Forge Design Automation .NET SDK: Provides .NET SDK to help you easily integrate Forge Design Automation v3 REST APIs into the application - GitHub - Autodesk-Forge/forge-api-dotnet-design.automati...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>このAutodesk.Forge.DesignAutomation .net SDKのGitHubページでは、Autodesk.Forge.DesignAutomation .net SDKの環境はnet 6.0以降となっております。</p>
<p>ところで、AutoCAD やRevit、Inventor等のエディタにロードして利用するカスタムプラグインや、Design Automationでコアエンジンにロードしてカスタムプログラムを実行するためのカスタムプラグインの開発には、Visul Studio 2019を利用しているかと思います。</p>
<p>ところが、このVisual Studio 2019は.net 5.0までがサポート対象となっているため、.net 6.0 以降を対象としてるAutodesk.Forge.DesignAutomation .net SDKを、インストールすることが出来ません。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://learn.microsoft.com/ja-jp/visualstudio/releases/2019/compatibility" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 200px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_832075.jpg" style="width: 100%; height: auto; max-height: 200px; min-width: 0; border: 0 none; margin: 0;" width="200" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Visual Studio 2019 の互換性</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Visual Studio 2019 の .NET 開発サポート<br />Visual Studio 2019 は、.NET の実装のいずれかを使うアプリの開発をサポートします。 .NET Framework、.NET Core、Mono、ユニバーサル Windows プラットフォーム (UWP) 用 .NET Native、C#、F#、Visual Basic をサポートするワークロードとプロジェクトの種類があります。 Visual Studio 2019 は次の実装をサポートします。<br /><br />.NET バージョン 5 (Visual Studio 16.8 以降)<br />.NET Framework バージョン 4.8、4.7.2、4.7.1、4.7、4.6.2、4.6.1、4.6、4.5.2、4.5.1、4.5、および 4.0<br />.NET Core 3.1、3.0、2.2、2.1、および1.1。<br />.NET ネイティブ</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>正しくはVisual Studio2022を利用する必要があるのです、開発環境などでカスタムプラグイン開発のソリューションと一緒にAutodesk.Forge.DesignAutomation .net SDK （.NET 6 ) を利用したい場合等があるかと思います。</p>
<p>&#0160;</p>
<p>そこで、今回の記事では、Visual Studio 2019でAutodesk.Forge.DesignAutomation .net SDK （.NET 6 ) を利用する方法について解説をしたいと思います。</p>
<p>&#0160;</p>
<p>なお、留意点ですがこの記事でご紹介する方法は、あくまでも経験的にVisual Studio 2019で.NET 6を動かすことが出来た方法となります。ありていに申しますと”やってみたら動いた”というTipsをご紹介するものです。</p>
<p>Microsoft社が公式にサポートしている方法ではなく、またすべての環境で同じ方法が適用可能であることを保証するものではありませんので、ご利用については、自己責任でお願いをいたします。</p>
<p>&#0160;</p>
<p>それでは、設定の手順についてご紹介いたします。</p>
<p>1．https://dotnet.microsoft.com/ja-jp/download/dotnet/6.0 より、.net 6.0 SDKをダウンロードしてインストール。（本記事での方法の適用には、6.0.2以前の.net 6.0が必要となります）</p>
<p>&#0160;</p>
<p>2．Visual Studio2019で.net Core Webアプリケーションを作成。</p>
<p>作成したアプリケーションのプロジェクトのプロパティを確認すると、以下の様に.net Core 5.0までしか選択できないことが分かります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c926715200d-pi" style="display: inline;"><img alt="1" class="asset  asset-image at-xid-6a0167607c2431970b02af1c926715200d img-responsive" src="/assets/image_975019.jpg" title="1" /></a></p>
<p>また、この状態でNuGetパッケージマネージャからAutodesk.Forge.DesignAutomationをプロジェクトの参照に追加しようとすると以下の様に互換性がないというエラーが出てしまうと思います。これはプロジェクトのターゲットとするフレームワークが.net 6.0以前であるためとなります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c92673b200d-pi" style="display: inline;"><img alt="2" class="asset  asset-image at-xid-6a0167607c2431970b02af1c92673b200d img-responsive" src="/assets/image_512723.jpg" title="2" /></a></p>
<p>3．Visual Studio 2019のGUIから.net 6.0を選択できない状態のため、一旦作成したプロジェクトを閉じてプロジェクトファイルをテキストエディタで開きます。</p>
<p>テキストエディタで開くと以下のようなXMLが記述されているかと思いいます。</p>
<pre><code>
&lt;Project Sdk=&quot;Microsoft.NET.Sdk.Web&quot;&gt;<br />&lt;PropertyGroup&gt;  
    &lt;TargetFramework&gt;netcoreapp3.1&lt;/TargetFramework&gt;netcoreapp3.1
&lt;/PropertyGroup&gt;<br />&lt;/Project&gt; 
</code></pre>
<p>ここで、TargetFramework タグ内に書かれているnetcoreapp3.1をnet6.0-windowsに書き換えて保存します。</p>
<p>&#0160;</p>
<p>4．Visual Studioでプロジェクトを再度開き、プロジェクトのプロパティを確認すると、以下の画像のように対象のフレームワークが空欄になっているかと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c92679a200d-pi" style="display: inline;"><img alt="3" class="asset  asset-image at-xid-6a0167607c2431970b02af1c92679a200d img-responsive" src="/assets/image_313505.jpg" title="3" /></a></p>
<p>5．この状態でNuGetパッケージマネージャからAutodesk.Forge.DesignAutomationをプロジェクトの参照に追加すると、正常にパッケージをインストールできます。</p>
<p>&#0160;</p>
<p>既存のアプリケーションで、古いバージョンのAutodesk.Forge.DesignAutomation SDKをご利用の場合にも、プロジェクトファイルのTargetFrameworkの内容を編集することで、Autodesk.Forge.DesignAutomation SDKの最新版を利用することが可能となります。</p>
<p>&#0160;</p>
<p>本記事の冒頭でも述べましたが、ここで紹介した手法はMicrosoft社が公式にサポートしている方法ではなく、経験的にVisual Studio 2019で.NET 6を動かすことが出来た方法のご紹介となります点にご留意ください。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
