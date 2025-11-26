---
layout: "post"
title: "OAuth での Scope について"
date: "2019-06-17 00:09:38"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/06/scopes-on-oauth.html "
typepad_basename: "scopes-on-oauth"
typepad_status: "Publish"
---

<p>今迄、このブログでご案内していなかったものに&#0160;<strong><a href="https://forge.autodesk.com/en/docs/oauth/v2/developers_guide/scopes/" rel="noopener" target="_blank"> Scope</a></strong> があります。Forge の正式リリース前に存在いた View and Data API や AutoCAD I/O では、当初、Scope 指定はオプション扱いでしたが、現在では <strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/10/to-developers-who-use-access-token-without-scope.html" rel="noopener" target="_blank">必須</a></strong> のパラメータとなっています。</p>
<p>日本でも 3-legged OAuth で BIM 360 Docs への統合や 、Design Automation API v3（Beta）で各種コア エンジンの利用を検討されている方も増えていますので、適切なアクセス権限を持った <strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/11/about-access-token.html" rel="noopener" target="_blank">Access Token</a></strong> を取得出来るように、改めて Scope について触れておきたいと思います。</p>
<p>ご存知のように、Forge Platform API で使用する endpoint 呼び出しでは、事前に Authentication API（OAuth API）で適切なアクセス権限が設定された Access Token を指定する必要があります。このアクセス権限を指定するのが Scope（スコープ）の役割です。アクセス権限は使用する endpoint によって異なるため、事前に API リファレンスでチェックしておく必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4858a82200d-pi" style="display: inline;"><img alt="Required_scope" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4858a82200d image-full img-responsive" src="/assets/image_198513.jpg" title="Required_scope" /></a></p>
<p>一般的な Forge アプリでは、複数の endpoint 呼び出しでタスクを実行するので、1 つの Scope 値では権限の不足した Access Token しか取得することが出来ません、このため、Scope は複数の Scope 値を ’ ’（半角スペース）で結合した文字列を指定することが可能です。</p>
<p>上図で示した Bucket の読み出しと Bucket の作成を実行するには、’bucket:read&#39; と &#39;bucket:create&#39; を半角スペース（URL ENCODE で ’%20’）で結合した &#39;bucket:read bucket:create&#39; を Scope 値として Access Token を取得出来るので、両者の endpoint を呼び出すことが可能です「。</p>
<p>この組み合わせに利用出来る Scope 文字列の値は、次のとおりです。</p>
<table border="0" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="width: 591px; border-collapse: collapse;" width="689">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 199.2px; border-width: 1pt 1pt 3pt; border-style: solid; border-color: white; border-image: initial; background: #0696d7; padding: 3.6pt 7.2pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><strong><span lang="EN-US" style="font-size: 14.0pt; mso-bidi-font-size: 11.0pt; font-family: メイリオ; color: white; mso-themecolor: background1;">Scope </span></strong><strong><span style="font-size: 14.0pt; mso-bidi-font-size: 11.0pt; font-family: メイリオ; color: white; mso-themecolor: background1;">文字列</span></strong></p>
</td>
<td style="width: 350.4px; border-top: 1pt solid white; border-left: none; border-bottom: 3pt solid white; border-right: 1pt solid white; background: #0696d7; padding: 3.6pt 7.2pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><strong><span style="font-size: 14.0pt; mso-bidi-font-size: 11.0pt; font-family: メイリオ; color: white; mso-themecolor: background1;">意味</span></strong></p>
</td>
</tr>
<tr style="mso-yfti-irow: 1; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><strong><span lang="EN-US" style="font-family: メイリオ;">user-<span class="GramE">profile:read</span></span></strong></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">プロファイル（<span lang="EN-US">Autodesk ID</span>）の表示</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 2; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">user:read</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">プロファイル（<span lang="EN-US">Autodesk ID</span>）の読み取り</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 3; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">user:write</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">プロファイル（<span lang="EN-US">Autodesk ID</span>）の書き込み</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 4; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">viewables:read</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">変換後のデザインデータ（<span lang="EN-US">SVF</span>）の読み取り（表示）</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 5; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">data:read</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">ストレージ データの読み取り</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 6; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">data:write</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">ストレージ データの書き込み（編集）</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 7; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">data:create</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">ストレージ データの作成</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 8; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">data:search</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">ストレージ データの検索</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 9; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">bucket:create</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">新しい<span lang="EN-US"> OSS Bucket </span>の作成</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 10; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">bucket:read</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt;"><span lang="EN-US" style="font-family: メイリオ;">OSS Bucket </span><span style="font-family: メイリオ;">の読み取り</span></span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 11; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">bucket:update</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt;"><span lang="EN-US" style="font-family: メイリオ;">OSS Bucket </span><span style="font-family: メイリオ;">の更新</span></span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 12; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">bucket:delete</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt;"><span lang="EN-US" style="font-family: メイリオ;">OSS Bucket </span><span style="font-family: メイリオ;">の削除</span></span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 13; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">code:all</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">コードの生成または実行（<span lang="EN-US">Design Automation API</span>）</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 14; page-break-inside: avoid; height: 27.3pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">account:read</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.3pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">アプリやサービス アカウントの読み取り</span></p>
</td>
</tr>
<tr style="mso-yfti-irow: 15; mso-yfti-lastrow: yes; page-break-inside: avoid; height: 27.35pt;">
<td style="width: 193.675px; border-right: 1pt solid white; border-bottom: 1pt solid white; border-left: 1pt solid white; border-image: initial; border-top: none; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.35pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span class="SpellE" style="font-size: 10pt;"><span class="GramE"><strong><span lang="EN-US" style="font-family: メイリオ;">account:write</span></strong></span></span></p>
</td>
<td style="width: 344.875px; border-top: none; border-left: none; border-bottom: 1pt solid white; border-right: 1pt solid white; background: #e7eff8; padding: 3.6pt 7.2pt 3.6pt 11.35pt; height: 27.35pt;">
<p class="MsoNormal" style="layout-grid-mode: char; mso-layout-grid-align: none;"><span style="font-size: 10pt; font-family: メイリオ;">アプリやサービス アカウントの書き込み</span></p>
</td>
</tr>
</tbody>
</table>
<p>よく利用される Viewer ソリューション以外で特異なのは、Design Automation API の利用時に指定する <strong>code:all</strong> です。</p>
<p>同様に、予め変換済の SVF を Viewer で表示するだけの Forge アプリでは、<strong>viewables:read</strong> だけの指定でアプリの動作をカバー出来る点に留意してください。不必要に多くのアクセス権限を設定した Access Token の運用は、セキュリティ上、お勧めしません。</p>
<p>By Toshiaki Isezaki</p>
