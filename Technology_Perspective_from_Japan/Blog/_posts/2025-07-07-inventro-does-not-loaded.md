---
layout: "post"
title: "Inventorのアドインがロードされない。そんな時には"
date: "2025-07-07 01:18:53"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/07/inventro-does-not-loaded.html "
typepad_basename: "inventro-does-not-loaded"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861054583200d-pi" style="display: inline;"><img alt="Title" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861054583200d img-responsive" src="/assets/image_49811.jpg" title="Title" /></a></p>
<p>Inventorのアドインの開発中にアドインマネージャから「自動でロード」を指定したのにロードが解除されてしまうが、原因が分からない。といった事象に遭遇したことはありませんでしょうか。</p>
<p>今回の記事では、Inventorのアドインがロードされない場合のよくある理由や、調査方法について紹介したいと思います。</p>
<p>&#0160;</p>
<p>１．DLLファイルのセキュリティ設定</p>
<p>「他のPC（開発環境等）で動作しているアドインを、別の環境にコピーすると動かなくなった」場合に、まず確認をしていただきたい項目です。</p>
<p>ネットワークを通じて取得したした .dll ファイルは、セキュリティ機能によってロードがブロックされる場合があります。</p>
<p>コピーをしたアドインの*dllファイルを右クリックして [プロパティ] ダイアログの [全般] タブを開き、右下にある 「許可する」 にチェックを入れてください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d7acb1200c-pi" style="display: inline;"><img alt="Title" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d7acb1200c img-responsive" src="/assets/image_505307.jpg" title="Title" /></a></p>
<p>&#0160;</p>
<p>２．Activateメソッド処理中のエラー</p>
<p>ご承知の様に、InventorがアドインのDllファイルをロードすると、カスタマイズプログラムのActivate()メソッドが実行されます。</p>
<p>このActivateメソッドの処理中に、何らかの理由でExceptionが発生している場合には、アドインマネージャでロードを行っても、再度アドインマネージャを開くとアドインがロードされていないといった状態となります。</p>
<p>問題点の切り分けとして、以下の様な対応により状況の切り分けを行うことをお勧めいたします。</p>
<ul style="list-style-type: circle;">
<li>Activate メソッドの最初の行にメッセージボックスを表示する処理を追加て、Activate処理が実行されているか否かを確認する</li>
<li>Activateメソッド全体をty~catchで囲みExceptionが発生していないかを確認する。</li>
<li>Visual StudioでActivateメソッドの先頭にブレークポイントを置き、デバッグ実行をして確認をする</li>
</ul>
<p>&#0160;</p>
<p>よくあるケースとしては、過去のプラグインのコードを流用して別のプラグインを作成している場合などに、ControlDefinitions.AddButtonDefinition()メソッドに渡す「コマンド内部名」が重複しているために、Exceptionが発生している。などがあります。</p>
<p>&#0160;</p>
<p>3．利用している3rdパーティ製ライブラリが参照パスにない</p>
<p>上記切り分けの結果、Activate自体が実行されていない場合、アドインのアセンブリのロードが出来ていない可能性が高い状況です。</p>
<p>典型的には、3rdパーティ製のライブラリを利用している場合などに、そのライブラリをロードできずに失敗しているような場合が当てはまります。</p>
<p>&#0160;</p>
<p>Inventor 2025から、プラットフォームが.NET Frameworkから.NET に変更されました。</p>
<p>参考記事：<a href="https://adndevblog.typepad.com/technology_perspective/2024/05/inventor-2025-migration-to-net8.html">Inventor 2025 カスタムプログラムの.NET 8 への移植</a></p>
<p>&#0160;</p>
<p>.NET Frameworkには共有アセンブリを配置するグローバル アセンブリ キャッシュ(GAC)という仕組みがありましたが、.NET（.NET 5.0以降)ではGACが廃止されています。このためアドインが参照する3rdパーティ製のライブラリをアドインのアセンブリファイルと同じフォルダに配置するといった対応が必要となります。</p>
<p>なお、Visual Studio 2022 C#では、 Visual Studioのプロジェクトファイル（.csproj ）をテキストエディタで開き、&lt;PropertyGroup&gt;タグ配下に以下の設定を追加することで、NuGetパッケージで取得したライブラリをビルド時に出力フォルダにコピーすることが可能となります。</p>
<p>&#0160;</p>
<p>&lt;CopyLocalLockFileAssemblies&gt;true&lt;/CopyLocalLockFileAssemblies&gt;</p>
<p>&#0160;</p>
<p>ここで紹介した内容で、すべての場合に対応できるわけではありませんが、問題の切り分けには役立つかと思います。</p>
<p>参考になりましたら幸いです。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
