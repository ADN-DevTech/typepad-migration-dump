---
layout: "post"
title: "Access Token タイプの変更について"
date: "2017-02-20 19:40:49"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/02/about-changing-type-of-access-token.html "
typepad_basename: "about-changing-type-of-access-token"
typepad_status: "Publish"
---

<p>Forge でお使いいただいている Access Token の形式が、現行の Reference Token タイプから <a href="https://en.wikipedia.org/wiki/JSON_Web_Token" rel="noopener noreferrer" target="_blank">&#0160;<strong>JSON Web Token</strong></a>&#0160;<strong>(JWT)</strong> タイプに変更されます。主な目的は、今後のトラフィック量低減とセキュリティ強化を図るためです。ここでは、今回実施される Access Token 変更について、その詳細をご案内しておきたいと思います。</p>
<hr />
<p>JSON Web Token とは、RFC 7519 (<a class="external-link" href="https://tools.ietf.org/html/rfc7519" rel="nofollow">https://tools.ietf.org/html/rfc7519</a>) &#0160;で策定された、2 者間で要求を安全に表現するための業界標準の方式です。Token は、それ自身に含まれ、Token についての情報は JSON 文字列として Token 内に埋め込まれています。</p>
<p>具体的には、JSON Web Token は、Header、Signature（デジタル署名）、Payload の 3 つの情報を &quot;.&quot; （ドット）で区切った文字列として表現されます。</p>
<p><strong>Header：</strong></p>
<p>Header は、使用される Token のタイプと、デジタル署名の作成に使用されるハッシュ アルゴリズム（HMAC SHA256 または RSA）を指定する 2 部分から構成されています。Forge の実装では、HMAC SHA256 が利用されています。</p>
<pre>{<br /> &quot;alg&quot;: &quot;HS256&quot;,<br /> &quot;typ&quot;: &quot;JWT&quot;<br />}</pre>
<p>この部分の JSON は Base64Url でエンコードされて JSON Web Token の Header 部として含まれることになります。</p>
<p><strong>Payload：</strong></p>
<p>Payload は、Token についての情報で構成されていて、Forge では、Cliend ID、Token の有効期間、Scope、Autodesk ID（3-legged 認証の場合）を含みます。この部分の JSON も Base64Url でエンコードされて JSON Web Token の Header 部として含まれることになります。</p>
<p><strong>Signature：</strong></p>
<p>Signature 部は、Payload が改ざんされていないことを保証するために使用されます。</p>
<p>最終的な JSON Web Token は {algorithm}.{payload}.{signature} の形式になります。下記は、その例です。</p>
<pre><em>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ</em></pre>
<hr />
<p><strong>なぜ、JSON Web Token への変更が必要なのか？</strong></p>
<p>現在の Forge は、OAuth サーバーに保存された Token 情報を参照する <strong>Reference Token</strong> と呼ばれるタイプの Access Token を使用しています。Forge API ゲートウェイを介して呼び出しが実行された際には、API ゲートウェイ認証ヘッダーから Token を展開し、次に、Token の正当性をチェックするために OAuth サーバーを呼び出します。結果として、この呼び出しによって、API 呼び出し全体の応答時間と Forge インフラ上で ネットワーク トラフィックが微増することになります。現時点でも 、Token の正当性チェック等で、同じ Token による 2次的な呼び出しに備えて、キャッシュを利用する仕組みを API ゲートウェイに採用、パフォーマンスの最適化を図っています。ただ、今後の Forge 利用の増加にともなう高負荷の状態では、プラットフォームの不安定化を招く懸念もあります。</p>
<p>Token 情報を含むことが可能な JSON Web Token を採用することで、Token の正当性をチェックする追加呼び出しが必要なくなるので、結果として、API 呼び出し時の応答時間全体を改善すること出来ます。</p>
<hr />
<p><strong>何が変わるのか？</strong></p>
<p>Reference Token を採用する現在の Forge の Access Token では、Token 長が固定されています。JSON Web Token の場合、2-legged 認証時に使用する OAuth2 の Access Token 長が 350 ～ 450 桁の長さになります。また、3-legged 認証時の場合は 500 桁までの長さになります。</p>
<hr />
<p><strong>何が変わらないのか？</strong></p>
<p>2-legged 認証、3-legged 認証で使用する endpoint や、RESTful 呼び出しで指定すべき Header や Body の各パラメータに変更はありません。</p>
<p><strong>変更前：Header で Access Token を指定する Authorization パラメータ例</strong></p>
<pre>Authorization : Bearer 2twyshwswNf9iIEadwBWho2N5qXwn</pre>
<p><strong>変更後：Header で Access Token を指定する Authorization パラメータ例</strong></p>
<pre>Authorization : Bearer eyJhbGciOiJIUzI1NiIsImtpZCI6Imp3dF9zeW1tZXRyaWNfa2V5X2RldiJ9.eyJ1c2VyaWQiOiJSSk4zTjVEOUFBNEsiLCJleHAiOjE0ODU0Mzk5NzYsInNjb3BlIjpbImRhdGE6cmVhZCJdLCJjbGllbnRfaWQiOiJBallYbkpDV3R5VHNpY3lOaUFyOUdLZkZvS0daWW84TyIsImdyYW50X2lkIjoiZ2huclNXaE5RdVBOTktOZFB0T1N2UExtTFpMRHlqblQiLCJhdWQiOiJodHRwczovL2F1dG9kZXNrLmNvbS9hdWQvand0ZXhwMTQ0MCIsImp0aSI6IkQ3cm95Tlhjd3o5RndhNGg1R3ZvU25KN0JScUtpVFVLZHE2emQ2RklUTzkxRjhHMXk0TUxjSEJ3MTNoTzVFNkwifQ.wbZjkr1FnvaqwCmW215tkQTKyiAatdmi-H1dSyj9RMo</pre>
<hr />
<p><strong>いつまでに何をしなければならないのか？</strong></p>
<p>もし、固定長で確保した変数に Access Token を保存するプログラムをお持ちの場合は、変数領域が十分に確保されていることを確認いただく必要があります。</p>
<p>Reference Token から JSON Web Token への変更は、現時点で、3月7日（米国太平洋標準時間）を予定しています。日本時間では 3月8日になりますので、それまでに、Access Token を格納する変数領域が、前述の JSON Web Token に長さに対応出来るか、念のため確認していただくことをお勧めします。</p>
<p>By Toshiaki Isezaki</p>
