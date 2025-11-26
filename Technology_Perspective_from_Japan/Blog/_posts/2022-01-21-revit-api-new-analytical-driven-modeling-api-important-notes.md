---
layout: "post"
title: "Revit API - 新しい解析モデリング API への移行に関する注意点"
date: "2022-01-21 00:11:19"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/01/revit-api-new-analytical-driven-modeling-api-important-notes.html "
typepad_basename: "revit-api-new-analytical-driven-modeling-api-important-notes"
typepad_status: "Publish"
---

<p>先日ご案内した <a href="https://adndevblog.typepad.com/technology_perspective/2021/12/revit-api-new-analytical-driven-modeling-api.html"><strong>Revit の新しい解析モデリング API（Analytical Driven Modeling API）</strong></a>について、Revit の次期バージョン でのリリースが予定されております。</p>
<p>今回の大規模な改良では、新旧の API を共存させることが困難なため、従来では、既存の API は Deprecated （廃止予定）としてマイグレーションのためのメンテナンス期間が設けられておりましたが、<strong><span style="text-decoration: underline;">次期バージョンでは、既存の解析モデル API の一部はメンテナンス期間を設けずに削除される</span></strong>ことが告知されております。</p>
<p>そのため、既存の解析モデル API を利用してアドインを開発されている場合、次期バージョンに対応するためには事前にマイグレーションいただく必要がございます。</p>
<p>新しい解析モデリング API の詳細につきましては、11月24日に開催された ADN メンバー及び、Preview Release ユーザ向けの Webinar をご参照ください。<br />Revit Structure の将来のリリースに向けた大規模な機能強化の計画と進化、お客様のアプリケーションに影響を与える API の変更内容をご紹介しております。</p>
<p>ADN メンバー専用サイト（ADN Extranet）の下記ページにて収録動画をダウンロード頂けます。</p>
<p style="padding-left: 40px;"><strong>adn.autodesk.io &gt;&gt; Software &gt;&gt; Webinar Recordings &gt;&gt; Revit Structure Analytical Model API</strong></p>
<p>また、ADN メンバーのお客様は、下記の ADN Beta Portal のページから Revit プレビュープロジェクトにご参加頂けます。<br />Revit プレビュープロジェクトでは、次期 Revit のベータ版をインストールして開発中の新機能をお試し頂くことができます。</p>
<p style="padding-left: 40px;"><strong>adn.autodesk.io &gt;&gt; Software &gt;&gt; Beta Portal &gt;&gt; ADN Beta Portal &gt;&gt; Product Beta Access</strong></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f948910200c-pi" style="display: inline;"><img alt="RevitPreviewProject" class="asset  asset-image at-xid-6a0167607c2431970b02942f948910200c img-responsive" src="/assets/image_46046.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="RevitPreviewProject" /></a></p>
<p>なお、最新の Revit API リファレンスにつきましても、Revit プレビュープロジェクト内の下記ページにて公開されております。<br />併せてご確認ください。</p>
<p style="padding-left: 40px;"><strong>Revit Preview &gt;&gt; User Forums &gt;&gt; API General &gt;&gt; Revit Preview API Documentation</strong></p>
<p>By Ryuji Ogasawara</p>
