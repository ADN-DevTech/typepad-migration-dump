---
layout: "post"
title: "3ds Max Plug-in Wizardについて"
date: "2013-08-01 00:09:00"
author: "Akira Kudo"
categories:
  - "API カスタマイズ"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/08/3ds-max-plug-in-wizard%E3%81%AB%E3%81%A4%E3%81%84%E3%81%A6.html "
typepad_basename: "3ds-max-plug-in-wizardについて"
typepad_status: "Publish"
---

<p>Autodesk Developer Networkの工藤　暁です。前回はM&amp;E製品のSDKに興味がある方を対象に学習用資料の御紹介しましたが、今回は3ds MaxのPlug-in Wizardについて御紹介します。これは3ds MaxのPlug-in Wizardに現在、少し難がある為です。</p>
<p>Plug-in Wizard はVisualStudioにてPlug-inのプロジェクトを自動作成するものです。maxsdk\howto\3dsmaxPluginWizard フォルダーに存在します。設定方法は<a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=7481355">Autodesk® 3ds Max®</a>ページIntroduction to 3ds Max SDK<a href="http://images.autodesk.com/adsk/files/3dsMax_SDK_webcast_Japanese.zip">日本語版</a>、１日目・エクササイズと２日目・前日のおさらいにて解説しております。是非ご確認下さい。</p>
<p>残念ながら2014リリース現在のPlug-in Wizard にて作成されたスケルトンは、幾つかのoverload関数の追加や、ParameterBlock2宣言のendをp_endに変更する必要がありますが、コンパイル・リンクのフラグを正しく設定するにはPlug-in Wizard の利用をお薦めします。Introduction to 3ds Max SDKの<a href="http://images.autodesk.com/adsk/files/3dsMax_SDK_webcast_Japanese.zip">日本語版</a>マテリアル中のサンプルコードは、スケルトンのコードから順にコードの変更・追加をしてます。コードを比較して頂くと、作成されたスケルトンコードのビルドに必要なコード変更を理解する助けになると思います。</p>
<p>また日常の業務にてPlug-inのプロジェクトをお預かりする機会が多いのですが、問題を解決する際にPlug-in　Wizardにてプロジェクトを再作成がする機会が良くあります。以下はその例です:</p>
<ul>
<li>古いプラグインを最新版に対応させる際</li>
<li>前バージョンとバイナリ互換で無くなった際</li>
</ul>
