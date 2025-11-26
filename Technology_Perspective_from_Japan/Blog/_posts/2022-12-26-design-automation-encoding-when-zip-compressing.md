---
layout: "post"
title: "Design Automation API：ZIP 圧縮時のエンコード"
date: "2022-12-26 00:02:18"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/12/design-automation-encoding-when-zip-compressing.html "
typepad_basename: "design-automation-encoding-when-zip-compressing"
typepad_status: "Publish"
---

<p>Design Automation API では、WorkItem 実行時の素材ファイルに ZIP 圧縮されたファイルを指定することが出来ます。例えば、外部参照を持つ AutoCAD DWG の場合には、参照元の親ファイルと参照ファイルを一緒にZIP 圧縮して処理する必要があります。（アタッチしたラスター画像も外部参照です。）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a89446200b-pi" style="display: inline;"><img alt="Xref" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a89446200b image-full img-responsive" src="/assets/image_935877.jpg" title="Xref" /></a></p>
<p>この ZIP 圧縮ファイルですが、圧縮対象のファイルに日本語名が使われていると。WorkItem 処理に失敗するケースが存在します。Windows の標準機能を使って ZIP 圧縮した場合です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1486fdae200c-pi" style="display: inline;"><img alt="Zipping_on_win_ja" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1486fdae200c image-full img-responsive" src="/assets/image_327444.jpg" title="Zipping_on_win_ja" /></a></p>
<p>日本語版 Windows では、日本語環境を表すコードページ 932 に基づき、Shift-JIS コードでファイル名がエンコードされて圧縮ファイルが作成されます。</p>
<p>一方、Design Automation API が WorkItem 毎に作成する仮想環境では、英語版 Windows が使用されます。このため、Shift-JIS でエンコード、圧縮された日本語ファイル名は、文字化けを起こした状態で実行環境に展開されることになります。</p>
<p>結果として、WorkItem は指定された本来のファイルを見つけることが出来ず、&quot;failedInstructions&quot; ステータスを返して処理に失敗してしまいます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c924179200d-pi" style="display: inline;"><img alt="File_name_corruption" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c924179200d image-full img-responsive" src="/assets/image_416190.jpg" title="File_name_corruption" /></a></p>
<p>Design Automation API は、ZIP 圧縮時のファイル名エンコードに UTF-8 を前提にしますので、手動操作で ZIP 圧縮する際には、エンコード指定が可能な <a href="https://sevenzip.osdn.jp/" rel="noopener" target="_blank">7-Zip</a>&#0160;などのツールなどを使用するようにしてください。</p>
<p>7-Zip ツールの場合、既定の動作は Windows の圧縮機能と同じになってしまいますので注意してください。UTF-8 エンコーディングを指定するには、パラメータに &quot;<strong><a href="https://sevenzip.osdn.jp/chm/cmdline/switches/method.htm" rel="noopener" target="_blank">cu=on</a></strong>&quot; を指定する必要があります。</p>
<ul>
<li><strong><a href="https://sevenzip.osdn.jp/chm/cmdline/switches/method.htm" rel="noopener" target="_blank">cu=on</a></strong>：7-Zip uses UTF-8 for file names that contain non-ASCII symbols.（7-Zip は非 ASCII シンボル - 日本語 - を含むファイル名に対して UTF-8 を使用します。）</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a892b5200b-pi" style="display: inline;"><img alt="7-zip-settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a892b5200b image-full img-responsive" src="/assets/image_658604.jpg" title="7-zip-settings" /></a></p>
<p>なお、日本語版 Windows であっても、eTransmit ではエンコードに UTF-8 が使用されるので、そのまま、Design Automation API で使用することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a892ba200b-pi" style="display: inline;"><img alt="Etransmit" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a892ba200b image-full img-responsive" src="/assets/image_673529.jpg" title="Etransmit" /></a></p>
<p>By Toshiaki Isezaki</p>
