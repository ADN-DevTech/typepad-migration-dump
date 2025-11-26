---
layout: "post"
title: "AutoCAD  内で Web ページを表示"
date: "2018-09-19 04:14:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/09/showing-web-page-in-autocad.html "
typepad_basename: "showing-web-page-in-autocad"
typepad_status: "Publish"
---

<p><a href="https://s3-ap-northeast-1.amazonaws.com/static.auj18.eventcloudmix.com/forge.html" rel="noopener noreferrer" target="_blank"><strong>Forge DevCon Japan 2018</strong></a> の <strong>H3 Webhooks API で広がるアプリ連携</strong> クラスでは、A360 や BIM 360 Docs、Fusion Team などのオートデスクのストレージ サービスで起こるユーザ イベントを利用した WebHooks API についてご紹介しました。この中で、ストレージ操作の通知を得るべき関係者には、実際に AutoCAD などのデスクトップ製品を利用中の設計者が多いのでは？とお話しました。また、その想定に基づいて、デスクトップ製品内で WebHooks を実装した Web ページを表示する例をデモしました。DevCon の終了後、実際にどのような方法で実装するのか、何名かの人に質問を受けましたので、改めてご紹介しておきたいと思います。</p>
<p>ご存知のとおり、AutoCADの 2014 バージョンから JavaScript&#0160; API を<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/06/autocad-javascript-api-part3.html" rel="noopener noreferrer" target="_blank">サポート</a></strong>しています。また、AutoCAD 2015 で登場した <a href="http://adndevblog.typepad.com/technology_perspective/2014/03/new-features-on-autocad-2015-part1.html" rel="noopener noreferrer" target="_blank"><strong>新しいタブ</strong></a> 機能と、AutoCAD 2016 で <strong>新しいタブ</strong>&#0160;から名称変更した <strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/new-features-on-autocad-2016-part1.html" rel="noopener noreferrer" target="_blank">スタートタブ </a></strong>とも、表示されるコンテンツは HTML で定義されています。スタートタブのカスタマイズはサポートされていないものの、AutoCAD は、このように、順次、Web 開発に対する親和性を高めてきています。</p>
<p>この過程で従来の AutoCAD .NET API も拡張されていて、AutoCAD にカスタム パレット（<strong>Palette</strong> クラス）を追加する <strong>Add</strong> メソッドに直接 <strong><a href="https://ja.wikipedia.org/wiki/Uniform_Resource_Identifier" rel="noopener noreferrer" target="_blank">URI</a></strong> を指定することで、ローカル コンピュータ内にある HTML ファイルや外部 Web サイトをパレット内に表示出来るようになっています。</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3929bba200d-pi" style="display: inline;"><img alt="Apiref" class="asset  asset-image at-xid-6a0167607c2431970b022ad3929bba200d img-responsive" src="/assets/image_261614.jpg" style="width: 800px;" title="Apiref" /></a></p>
<p><strong>H3 Webhooks API で広がるアプリ連携</strong> クラスでご紹介したのは、Data Management WeoHooks の<strong><a href="https://github.com/Autodesk-Forge/data.management-nodejs-webhook" rel="noopener noreferrer" target="_blank">サンプル（https://github.com/Autodesk-Forge/data.management-nodejs-webhook）</a></strong>のデモ サイトである <strong><a href="https://bim360notifier.autodesk.io/" rel="noopener noreferrer" target="_blank">BIM 360 Notifier（https://bim360notifier.autodesk.io/）</a></strong>を、このメソッドを使って AutoCAD のカスタム パレット内に表示したものとなります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad36c7391200c-pi" style="display: inline;"><img alt="Autocad" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad36c7391200c image-full img-responsive" src="/assets/image_780401.jpg" title="Autocad" /></a></p>
<p>AutoCAD アドイン側で実装すべきは、カスタム パレットを作成して特定の Web サイトを表示するコマンドを定義するだけ、という簡単な内容です。WebHooks API で通知を得るコールバックや 3-legged OAuth で使用するコールバックの実装には Web サーバーの構築が不可欠であるため、アドイン内に Web セキュリティを持ち込まなくてもすむ利点があります。</p>
<p>上図の実装で定義した <strong>Forge-Webhooks</strong> コマンドの C# コードは次のとおりです。&#0160;&#0160;</p>
<pre>        // Decraration variables for PalleteSet
        static Autodesk.AutoCAD.Windows.PaletteSet ps = null;

        // Modal Command with localized name
        [CommandMethod(&quot;MyGroup&quot;, &quot;<strong>Forge-Webhooks</strong>&quot;, &quot;<strong>Forge-Webhooks</strong>&quot;, CommandFlags.Modal)]
        public void MyCommand2()
        {

            // Create PalleteSet
            if (ps == null)
            {
                ps = new Autodesk.AutoCAD.Windows.PaletteSet(&quot;BIM 360 Notifier&quot;);
                ps.Style = PaletteSetStyles.ShowPropertiesMenu |
                            PaletteSetStyles.ShowAutoHideButton |
                            PaletteSetStyles.ShowCloseButton |
                            PaletteSetStyles.Snappable;
                ps.Visible = true;
            }
            else
            {
                ps.Visible = true;
            }

            // Create Pallete
            if (ps.Count == 0)
            {
<strong>                Uri url = new Uri(&quot;https://bim360notifier.autodesk.io/&quot;);
                Autodesk.AutoCAD.Windows.Palette p = ps.Add(&quot;BIM 360 Notifier&quot;, url);
</strong>                ps.Dock = DockSides.None;
            }

        }</pre>
<p>余談ですが、AutoCAD には JavaScript API をサポートした 2014 バージョン以降、製品内部に <strong><a href="https://ja.wikipedia.org/wiki/WebKit" rel="noopener noreferrer" target="_blank">WebKit</a></strong> が組み込まれています。上記のような Web サイト/ページを表示するカスタム パレットやスタート タブ（HTML）を表示中で<span style="text-decoration: underline;">アクティブにした状態で</span>&#0160;<strong>F12 キー</strong>を押すと、Web ブラウザ同様のデベロッパーツールを呼び出すことが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3b15a50200b-pi" style="display: inline;"><img alt="Devtool" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3b15a50200b image-full img-responsive" src="/assets/image_205678.jpg" title="Devtool" /></a></p>
<p>Web サーバー実装では、Web ブラウザ側の<strong><a href="https://en.wikipedia.org/wiki/Web_development_tools" rel="noopener noreferrer" target="_blank">デベロッパーツール</a></strong>を利用するものと思いますが、ご参考までにお知らせしておきます。&#0160;</p>
<p>By Toshiaki Isezaki</p>
