---
layout: "post"
title: "AutoCAD 雑学：寸法"
date: "2021-08-18 00:11:25"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/08/autocad-trivia-dimension.html "
typepad_basename: "autocad-trivia-dimension"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e114f0bc200b-pi" style="display: inline;"></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278803c805e200d-pi" style="display: inline;"><img alt="Dimension" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278803c805e200d image-full img-responsive" src="/assets/image_650681.jpg" title="Dimension" /></a></p>
<p>言うまでもなく、「寸法」オブジェクトは図面に不可欠な存在です。この寸法は、業種によって異なる表現が用いられることが知られています。同時に、AutoCAD は、汎用 CAD ソフトウェアとして、英語、ブラジル-ポルトガル語、チェコ語、フランス語、ドイツ語、ハンガリー語、イタリア語、日本語、韓国語、ポーランド語、ロシア語、簡体字中国語、スペイン語、繁体字中国語に翻訳され、各国て使用されています。</p>
<p>このため、多くの国で多様な寸法表現に対応するため、数多くのシステム変数で寸法表現を細かく調整出来るようになっています。寸法に関係するシステム変数の多くは、D の頭文字を持つため、<strong><a href="https://help.autodesk.com/view/ACD/2022/JPN/?page=sysvars&amp;q=D*" rel="noopener" target="_blank">D システム変数の一覧</a></strong>で確認出来ます。</p>
<p>システム変数は、AutoCAD の振る舞いを左右する設定値であり、多くの場合、コマンド実行前に設定しておくことで、作図内容を変化させることが可能です。例えば、長さ寸法オブジェクトの作図前に <strong><a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-B6063785-B199-4A9A-8BD7-2108EB0AB7ED" rel="noopener" target="_blank">DIMLFAC&#0160;システム変数</a></strong>の値を 2.0 に設定しておくと、測点間の寸法値が 2 売された値で作図されるようになります。</p>
<p>寸法関連のシステム変数が多くなってしまったため、1992 年に登場した AutoCAD R12 では、関連システム変数をまとめてスタイル管理する寸法スタイルが導入されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278803c7d2d200d-pi" style="display: inline;"><img alt="Dim_style" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278803c7d2d200d image-full img-responsive" src="/assets/image_426791.jpg" title="Dim_style" /></a></p>
<p>寸法スタイルの導入で、作図される寸法オブジェクトは、作図時に <strong><a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-98D575CA-C7FE-4442-83F3-C2BFD7D9A112" rel="noopener" target="_blank">DIMSTYLE システム変数</a></strong>に設定されている「現在の寸法スタイル」に関連付けられて作図されます。ただ、この環境でも、寸法関連のシステム変数を直接編集して、その値を変更することが出来ます。システム変数を書き換えてしまった場合の [寸法スタイル] ダイアログに表示されるのが「<strong><a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-1EBBCFE1-AC75-49C2-A889-1B286E96C4E8" rel="noopener" target="_blank">優先寸法スタイル</a></strong>」です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278803c82c4200d-pi" style="display: inline;"><img alt="Dimoverride" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278803c82c4200d image-full img-responsive" src="/assets/image_932747.jpg" title="Dimoverride" /></a></p>
<p>優先寸法スタイルは 、AutoCAD R13（1994 年）で導入され、現在の寸法スタイルの一部を上書きした際に表示されます。優先寸法スタイルが認識されると、以降の寸法オブジェクトの作図では同スタイルの内容に沿った寸法が作図されるようになります。</p>
<p>作図された寸法オブジェクトは、[プロパティ] パレットを用いて表現内容を変更することが出来ます。もちろん、この変更は、選択した寸法オブジェクトに対してのみ適用されることになります。寸法スタイルや優先寸法スタイルへの反映はおこなわれません。</p>
<p>AutoCAD API でも寸法スタイルの内容を変更することが出来る他、[プロパティ] パレットでの編集のように、作図済の寸法オブジェクトの内容も変更することが可能です。次の Autodesk Knowledge Network 記事は、寸法オブジェクトの寸法矢印を変更する例を紹介しています。</p>
<p style="padding-left: 40px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/1zC2GJtUqJs2DsYy22Fg6w.html" rel="noopener" target="_blank"><strong>AutoCAD .NET API ：寸法スタイルに従属せずに寸法矢印を変更するには？</strong></a></p>
<p>今回のご紹介は比較的簡単な内容でしたが、寸法スタイルとは別に個別のシステム変数が用意されているのは、AutoCAD の歴史に沿った影響があることがわかります。</p>
<p>By Toshiaki Isezaki</p>
