---
layout: "post"
title: "ACC Issues API：一部フィールドの最大長変更"
date: "2025-03-03 00:23:44"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/03/acc-issues-api-maximum-length-changes.html "
typepad_basename: "acc-issues-api-maximum-length-changes"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cbfff0200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e2e280200b-pi" style="display: inline;"><img alt="Screenshot 2025-02-15 at 16.04.18" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e2e280200b image-full img-responsive" src="/assets/image_740158.jpg" title="Screenshot 2025-02-15 at 16.04.18" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cbfff0200c-pi" style="display: inline;"><br /></a></p>
<h4><strong>Autodesk Construction Cloud（ACC）API をお使いの方へお知らせ</strong></h4>
<p style="padding-left: 40px;">オートデスクは、API を通じて ACC 指摘事項アイテムの 3 つのフィールド（title、description、locationDetails）に許容される最大文字数制限を変更する予定です。この変更により、API と Web ユーザー インターフェース（UI） に一貫した最大文字数制限が適用されます。</p>
<p style="padding-left: 40px;">API で許容される現在の最大文字数と新しい最大文字数を示します。</p>
<table border="1" cellpadding="1" cellspacing="1" style="height: 88px; width: 350px; margin-left: auto; margin-right: auto;">
<tbody style="padding-left: 40px;">
<tr style="height: 18px; padding-left: 40px;">
<td class="text-align-center" colspan="1" rowspan="2" style="height: 36px; width: 169.26px; text-align: center;"><strong>フィールド</strong></td>
<td class="text-align-center" colspan="2" rowspan="1" style="height: 18px; width: 170.98px; text-align: center;"><strong>最大文字数</strong></td>
</tr>
<tr style="height: 18px; padding-left: 40px;">
<td class="text-align-center" style="height: 18px; width: 84.79px; text-align: center;"><strong>現在</strong></td>
<td class="text-align-center" style="height: 18px; width: 82.27px; text-align: center;"><strong>変更後</strong></td>
</tr>
<tr style="height: 16px; padding-left: 40px;">
<td class="text-align-center" style="height: 16px; width: 169.26px; text-align: center;">title</td>
<td class="text-align-center" style="height: 16px; width: 45.79px; text-align: center;">4200</td>
<td class="text-align-center" style="height: 16px; width: 43.27px; text-align: center;">100</td>
</tr>
<tr style="height: 18px; padding-left: 40px;">
<td class="text-align-center" style="height: 18px; width: 169.26px; text-align: center;">description</td>
<td class="text-align-center" style="height: 18px; width: 45.79px; text-align: center;">10000</td>
<td class="text-align-center" style="height: 18px; width: 43.27px; text-align: center;">1000</td>
</tr>
<tr style="height: 18px; padding-left: 40px;">
<td class="text-align-center" style="height: 18px; width: 169.26px; text-align: center;">locationDetails</td>
<td class="text-align-center" style="height: 18px; width: 45.79px; text-align: center;">8300</td>
<td class="text-align-center" style="height: 18px; width: 82.27px; text-align: center;">250</td>
</tr>
</tbody>
</table>
<p style="padding-left: 40px;">今回の変更は、ACC Issues API の次の 2 つのエンドポイントに適用されます。</p>
<ul>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/issues-issues-POST/" rel="noopener" target="_blank">POST issues</a>&#0160;</li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/issues-issues-issueId-PATCH/" rel="noopener" target="_blank">PATCH issues/:issueId</a></li>
</ul>
<p style="padding-left: 40px;">注: この変更は、レガシー データへのアクセスを許容する次の ACC Issues API の読み取りエンドポイントのフィールド<u>には適用されません</u>。</p>
<ul>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/issues-issues-GET/" rel="noopener" target="_blank">GET issues</a>&#0160;</li>
<li><a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/issues-issues-issueId-GET/" rel="noopener" target="_blank">GET issues/:issueId&#0160;</a></li>
</ul>
<p style="padding-left: 40px;"><strong>バックグラウンド</strong></p>
<p style="padding-left: 40px;">これらの新しい最大文字数制限は、<u>Web ユーザ</u><span style="text-decoration: underline;">ー インターフェース</span> に適用されています。ただし、API では最大文字数が多くなっていたため、ユーザー エクスペリエンスに不一致が発生してしまいます（例えば、ユーザーは API を使用して長いタイトルを作成できますが、Web UI で完全なタイトルを見ることが出来ないことになってしまいます）。この変更は、ACC Issues API にのみ影響します。<u>BIM 360 Issues API は変更されません。</u>&#0160;&#0160; &#0160;&#0160;</p>
<h4><strong>必要なアクション</strong></h4>
<p style="padding-left: 40px;">ACC Issues API を使用して指摘事項アイテムを作成/更新する場合は、上記のフィールドの文字数が<strong>新しい最大文字数制限を超えないように</strong>してください。コードがより大きな最大文字数制限に合わせて記述されている場合は、<strong>2025年5月15日までに</strong>アプリケーションを更新して、特定されたフィールドの最大文字数制限を減らす必要があります。この日付以降は、新しい最大文字数制限よりも長い値でこれらのフィールドを更新出来なくなります。</p>
<p><strong>追加情報</strong></p>
<p style="padding-left: 40px;">以下は、この変更に関して予想されるいくつかの質問に対する回答のリストです。</p>
<p style="padding-left: 40px;"><strong>Q1</strong>: 新しい制限よりも長い既存のデータはどうなりますか？日付は切り捨てられますか？</p>
<p style="padding-left: 40px;"><strong>A1</strong>: 既存のデータは変更されません。データはそのまま残ります。<br />&#0160;</p>
<p style="padding-left: 40px;"><strong>Q2</strong>: 古い制限付きの長い文字を持つ API によって属性にパッチを適用または変更するとどうなりますか?</p>
<p style="padding-left: 40px;"><strong>A2</strong>:新しい制限と互換性のある新しいタイトルにのみ編集できます。<br />&#0160;</p>
<p style="padding-left: 40px;"><strong>Q3</strong>:私たちのワークフローでは、古い制限の属性がまだ必要ですが、提案は何ですか?</p>
<p style="padding-left: 40px;"><strong>A3</strong>: 新しい制限よりも長い文字列が必要な場合には、文字列を指摘事項のカスタム属性（現在、段落タイプで 2500 文字）に保存するか、指摘事項のコメントを作成（現在、100000 文字が受け入れられます）を代替としてご検討いただけます。また、長い文字列をテキストファイルに保存し、ファイルを参照または添付することもできます。<br />&#0160;</p>
<p style="padding-left: 40px;"><strong>Q4</strong>: 長いタイトル/説明/場所の詳細に問題があり、別の属性 (担当者など) を編集したいと考えています。長い文字を持つフィールドを変更せずにこれを行うことはできますか?</p>
<p style="padding-left: 40px;"><strong>A4</strong>: はい、title/description/locationDetails のみに適用されます。他の属性への影響はありません。</p>
<p>この変更に関するその他の質問をお持ちの場合には、 <a href="https://aps.autodesk.com/get-help">APS support</a> までお問い合わせください。</p>
<p>※ 本記事は&#0160;<a href="https://aps.autodesk.com/blog/register-today-autodesk-devcon-returns-2025-amsterdam" rel="noopener" target="_blank"></a><a href="https://aps.autodesk.com/blog/acc-issues-api-maximum-length-changes" rel="noopener" target="_blank">ACC Issues API: Maximum Length Changes | Autodesk Platform Services</a>&#0160;から転写・意訳・補足したものです。</p>
<p>By Toshiaki Isezaki</p>
