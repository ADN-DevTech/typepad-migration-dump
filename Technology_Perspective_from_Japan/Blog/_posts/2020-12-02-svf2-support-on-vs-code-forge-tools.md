---
layout: "post"
title: "VS Code Forge Tools での SVF2 対応"
date: "2020-12-02 01:02:37"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/12/svf2-support-on-vs-code-forge-tools.html "
typepad_basename: "svf2-support-on-vs-code-forge-tools"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/11/svf-and-svf2.html" rel="noopener" target="_blank">Model Derivative API：SVF と SVF2</a></strong> でご案内のとおり、Beta の位置づけですが、新しい SVF2 形式を利用した大規模ファイルを Forge Viewer でお試しいただけるようになっています。VS Code Forge Tool エクステンションでも SVF2 を使った評価が出来るようになっていますので、ご案内しておきます。</p>
<p>既に <strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/09/viewer-workflow-on-vs-code-forge-extension.html" rel="noopener" target="_blank">VS Code Forge Extension を使った Viewer ワークフローの確認</a></strong> のブログ記事で手順をご紹介していますが、VS Code 上で Bucket の作成から表示までをシミュレートすることは出来ます。</p>
<p>アップロードしたデザイン ファイルの変換は至って簡単です。アップロードしたファイルを&#0160;<strong>Translate Object(Custom)</strong>&#0160; メニューから変換実行するだけです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be427f69b200d-pi" style="display: inline;"><img alt="Custom_translate" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be427f69b200d image-full img-responsive" src="/assets/image_347960.jpg" title="Custom_translate" /></a></p>
<p>[Custom Model Derivative Job] タブが表示されたら、Output Format リストから SVF2 を選択して、ページ左下の [Run] ボタンで変換処理を実行してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be427f6b0200d-pi" style="display: inline;"><img alt="Custom_translate_option" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be427f6b0200d image-full img-responsive" src="/assets/image_820502.jpg" title="Custom_translate_option" /></a></p>
<p>変換完了後は、Preview Derivative メニューを選択するだけで SVF2 を表示させることが出来ます。特に、Api や Env 値を指定する必要はりません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e97b9e36200b-pi" style="display: inline;"><img alt="Preview_derivative" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e97b9e36200b image-full img-responsive" src="/assets/image_89387.jpg" title="Preview_derivative" /></a></p>
<p>マニフェスト ファイルを表示させると、outputType 値が svf2 になっていることがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdea8f862200c-pi" style="display: inline;"><img alt="Manifest" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdea8f862200c image-full img-responsive" src="/assets/image_832802.jpg" title="Manifest" /></a></p>
<p>なお、VS Code Forge Extension の設定で Api と Env を指定している場合には、Preview Derivative でのモデル表示に、その設定値が使用されます。SVF2 として変換されたファイルを Api 値に derivativeV2&#0160; Env 値に AutodeskProduction を設定している場合には、SVF として表示されます。（コードベースでも同様です。）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdea9e73a200c-pi" style="display: inline;"><img alt="Api_env" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdea9e73a200c image-full img-responsive" src="/assets/image_52254.jpg" title="Api_env" /></a></p>
<p>By Toshiaki Isezaki</p>
