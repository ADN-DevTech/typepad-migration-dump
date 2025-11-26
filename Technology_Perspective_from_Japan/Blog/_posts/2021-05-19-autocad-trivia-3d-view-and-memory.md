---
layout: "post"
title: "AutoCAD 雑学：3D 表示とメモリ"
date: "2021-05-19 00:03:48"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/05/autocad-trivia-3d-view-and-memory.html "
typepad_basename: "autocad-trivia-3d-view-and-memory"
typepad_status: "Publish"
---

<p>AutoCAD で 3D オブジェクトを使った作業をする際、2D 図面での作業時とは異なる現象に遭遇することがあります。かなり少し記事ですが、それらの起因となる可能性について、下記のブログ記事でご案内しています。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2013/07/cpu-and-graphics-card.html" rel="noopener" target="_blank"><strong>CPU とグラフィックスカード</strong></a></p>
<p>上記記事内にあるオートデスク認定ハードウェアページ（グラフィックス カードとドライバの組み合わせの一覧検索ページ）の URL は、現在、<strong><a href="https://knowledge.autodesk.com/ja/certified-graphics-hardware" rel="noopener" target="_blank">https://knowledge.autodesk.com/ja/certified-graphics-hardware</a></strong> に移動しています。ワークステーション級のグラフィックス カードの記載が目立ちますが、3D 作業を頻繁にされる場合には、認定ハードウェアの利用をご一考ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802a81b1200d-pi" style="display: inline;"><img alt="3d_view" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802a81b1200d image-full img-responsive" src="/assets/image_122332.jpg" title="3d_view" /></a></p>
<p>3D 作業時には多くのメモリを使用することになります。AutoCAD の動作環境に、3D を含む「<strong>大規模なデータセット、点群、3D モデリングを扱う場合の追加要件</strong>」が用意されているのは、このためです。下記は、<a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/sfdcarticles/sfdcarticles/JPN/System-requirements-for-AutoCAD-2022-including-Specialized-Toolsets.html" rel="noopener" target="_blank"><strong>AutoCAD 2022 の動作環境</strong></a> からの抜粋です。</p>
<table border=".1" cellpadding="2" cellspacing="0" style="height: 104px; border-color: #b3b1b1;">
<thead>
<tr style="height: 17px;">
<th colspan="2" rowspan="1" style="height: 17px; width: 771.6px; background-color: #e6e6e6; text-align: left;"><strong>大規模なデータセット、点群、3D モデリングを扱う場合の追加要件</strong></th>
</tr>
</thead>
<tbody>
<tr style="height: 17px;">
<td colspan="1" rowspan="1" style="height: 17px; width: 142.48px;"><strong>メモリ</strong></td>
<td colspan="1" rowspan="1" style="height: 17px; width: 623.12px;">8 GB 以上の RAM</td>
</tr>
<tr style="height: 35px;">
<td colspan="1" rowspan="1" style="height: 35px; width: 142.48px; background-color: #e6e6e6;"><strong>ディスク空き容量</strong></td>
<td colspan="1" rowspan="1" style="height: 35px; width: 623.12px; background-color: #e6e6e6;">6 GB 以上のハード ディスク空き容量(インストールに必要な空き容量を除く)</td>
</tr>
<tr style="height: 35px;">
<td colspan="1" rowspan="1" style="height: 35px; width: 142.48px;"><strong>ディスプレイ カード</strong></td>
<td colspan="1" rowspan="1" style="height: 35px; width: 623.12px;">3840 x 2160 (4K)以上の True Color 対応ビデオ ディスプレイ アダプタ、4G 以上の VRAM、Pixel Shader 3.0 以上、DirectX 対応ワークステーション クラス グラフィックス カード</td>
</tr>
</tbody>
</table>
<p>お使いのコンピュータが認定ハードウェアでない場合や、搭載メモリが推奨メモリ量に届かない場合でも、まったく 3D 機能が使用出来ないわけではありません。あくまで、快適な操作と表示上の問題を抑止することが目的の情報なので、その点はご注意ください。</p>
<p>3D 作業に限りませんが、AutoCAD 使用時に使っている表示スタイルによっても、消費されるメモリ量に差が出てきます。現在の表示スタイルは図面ファイルに保存されますので、新規セッションで図面を開いた際に、パフォーマンスに影響を与える可能性もあります。この点についても、過去にブログ記事でご紹介していますので、ご参考まで、ご案内しておきます。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2014/11/display-style-in-autocad-and-memory-consumption.html" rel="noopener" target="_blank"><strong>AutoCAD の表示スタイルとメモリ消費</strong></a></p>
<p>最近では 64 ビットコンピュータが普及して、AutoCAD も 64 ビット版のみのリリースになっています。32 ビット コンピュータが主流だった頃に比べてメモリ不足による問題が起こりにくくなっている印象ですが、知っておいて損はないでしょう。</p>
<p>AutoCAD を使った 3D の作業環境やパフォーマンスは、バージョン毎に変化してきています。<a href="https://adndevblog.typepad.com/technology_perspective/2021/03/new-features-on-autocad-2022-part2.html" rel="noopener" target="_blank"><strong>AutoCAD 2022 の新機能 ～ その2</strong></a> 記事の最後でも触れていますが、今後も AutoCAD のグラフィックス システムに改良が加えられていくものと思います。</p>
<p>ちなみに、AutoCAD .NET API を使った表示スタイル情報の走査と切り替え方法を、次の Autodesk Developer Network 記事でご案内しています。必要に応じてご確認ください。</p>
<p style="padding-left: 40px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/1hsh3agx3mVEXksTc3QcfH.html" rel="noopener" target="_blank"><strong>AutoCAD .NET API ：表示スタイルの変更</strong></a></p>
<p>最後に、3D 表示の「稀な」現象もご紹介します。AutoCAD は、AutoCAD 2013 でグラフィックス キャッシュ メカニズムを導入しています。</p>
<p>具体的には、3D ソリッドを含む図面を 2013 形式以降の DWG 形式で保存すると、グラフィックス キャッシュ ファイルが、ユーザの AppData フォルダの下の &quot;GraphicsCache&quot; という名前のフォルダに自動的に保存されます（AutoCAD 2022 の場合は C:\Users\<em>&lt;ログイン ユーザ名&gt;</em>\AppData\Local\Autodesk\AutoCAD 2022\R24.1\jpn\GraphicsCache）。</p>
<p>Web ブラウザ同様、キャッシュ ファイルの目的は、同じ図面（Web ブラウザの場合は、同じページ）を 2 回目以降に開いた場合、表示パフォーマンス向上です。ただ、Forge の <strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/06/design-automation-api-for-autocad.html" rel="noopener" target="_blank">Design Automation API for AutoCAD</a></strong> を使って、クラウド上で 3D モデルを生成、同じ DWG ファイル名で同じ場所に複数回ダウンロードした場合、初回に開いた図面ファイルのグラフィックス キャッシュを使っしてしまい、2 回目以降に図面を開いた際、期待した表示をしないことがあります。</p>
<p>このような場面では、システム変数 <strong><a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-7494F1CE-253B-42AC-BD26-10FB9E8FE77B" rel="noopener" target="_blank">CACHEMAXFILES</a></strong> を <strong>0</strong> に設定して、グラフィックス キャッシュをクリアすることで、正しく 3D モデルを表示させることが出来ます。</p>
<p style="padding-left: 40px;">※ CACHEMAXFILES 値の変更後には、AutoCAD の再起動が必要です。</p>
<p>グラフィックス キャッシュをクリア（削除）については、<a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2021/JPN/AutoCAD-Core/files/GUID-4F5F6FBE-ABD0-473F-B065-2BF4C7FD242B-htm.html" rel="noopener" target="_blank"><strong>概要 - メモリの調整</strong></a> にも記載がありますのでご確認ください。</p>
<p>By Toshiaki Isezaki</p>
