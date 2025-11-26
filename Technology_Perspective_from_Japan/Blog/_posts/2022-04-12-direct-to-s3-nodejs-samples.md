---
layout: "post"
title: "Direct-to-S3 Node.js サンプル"
date: "2022-04-12 19:14:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/04/direct-to-s3-nodejs-samples.html "
typepad_basename: "direct-to-s3-nodejs-samples"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880787f00200d-pi" style="display: inline;"><img alt="Nodejs" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880787f00200d image-full img-responsive" src="/assets/image_457680.jpg" title="Nodejs" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/03/data-management-oss-object-storage-service-migrating-to-direct-to-s3-approach.html" rel="noopener" target="_blank"><strong>Data Management OSS (Object Storage Service) の Direct-to-S3 アプローチへの移行について</strong></a>のアナウンスがありましたので、この移行をよりスムーズにおこなっていただくための情報をご提供したいと思います。今回は、Autodesk Forge サービスにおける新しいバイナリ転送のための Node.js ユーティリティについてです。 これらのサンプルは、<a href="https://nodejs.org/en/" rel="noopener" target="_blank"><strong>LTS バージョンの Node.js</strong></a> を使用してビルドされています。</p>
<p>チームはまた、Direct-to-S3 アプローチを使用する新しいSDKの開発にも取り組んでいます。&#0160;</p>
<p>チームの <a href="https://twitter.com/ipetrbroz?s=20&amp;t=-egqZR9E872TYHjZB0-APA">Petr Broz</a> は、OSS Direct-to-S3 アプローチのために新しくリリースされたすべてのエンドポイントを含む、キュレーションされたユーティリティファイルに取り組みました。&#0160;</p>
<p>Github のリポジトリは<a href="https://github.com/orgs/Autodesk-Forge/repositories?type=all" rel="noopener" target="_blank"><strong>こちら</strong></a>、その中の Node.js ブランチは<a href="https://github.com/Autodesk-Forge/forge-directToS3/tree/node" rel="noopener" target="_blank"><strong>こちら</strong></a>で利用可能です。&#0160;</p>
<h2>Index.js （ユーティリテイ ファイル）</h2>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-0"><span class="hljs-keyword">const</span> axios = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;axios&#39;</span>);
<span class="hljs-keyword">const</span> rax = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;retry-axios&#39;</span>);

<span class="hljs-keyword">class</span> BinaryTransferClient {
    <span class="hljs-comment">/**
     * Creates a new instance of the binary transfer helper client.
     *
     * Note that the provided access token will be used for all requests initiated
     * by this client. For long-running operations the token could potentially expire,
     * so consider modifying this class to refresh the token whenever needed.
     *
     * @param {string} token Access token to use when communicating with Autodesk Forge services.
     * @param {string} [host=&quot;https://developer.api.autodesk.com&quot;] Optional Autodesk Forge host).
     */</span>
    constructor(token, host) {
        <span class="hljs-keyword">this</span>.token = token;
        <span class="hljs-keyword">this</span>.axios = axios.create({
            baseURL: (host || <span class="hljs-string">&#39;https://developer.api.autodesk.com&#39;</span>) + <span class="hljs-string">&#39;/oss/v2/&#39;</span>
        });
        <span class="hljs-comment">// Attach an interceptor to the axios instance that will retry response codes 100-199, 429, and 500-599.</span>
        <span class="hljs-comment">// For default settings, see https://github.com/JustinBeckwith/retry-axios#usage.</span>
        <span class="hljs-keyword">this</span>.axios.defaults.raxConfig = {
            instance: <span class="hljs-keyword">this</span>.axios
        };
        rax.attach(<span class="hljs-keyword">this</span>.axios);
    }

    <span class="hljs-comment">/**
     * Generates one or more signed URLs that can be used to upload a file (or its parts) to OSS,
     * and an upload key that is used to generate additional URLs or in {@see _completeUpload}
     * after all the parts have been uploaded successfully.
     *
     * Note that if you are uploading in multiple parts, each part except for the final one
     * must be of size at least 5MB, otherwise the call to {@see _completeUpload} will fail.
     *
     * @async
     * @param {string} bucketKey Bucket key.
     * @param {string} objectKey Object key.
     * @param {number} [parts=1] How many URLs to generate in case of multi-part upload.
     * @param {number} [firstPart=1] Index of the part the first returned URL should point to.
     * For example, to upload parts 10 through 15 of a file, use `firstPart` = 10 and `parts` = 6.
     * @param {string} [uploadKey] Optional upload key if this is a continuation of a previously
     * initiated upload.
     * @param {number} [minutesExpiration] Custom expiration for the upload URLs
     * (within the 1 to 60 minutes range). If not specified, default is 2 minutes.
     * @returns {Promise&lt;object&gt;} Signed URLs for uploading chunks of the file to AWS S3,
     * and a unique upload key used to generate additional URLs or to complete the upload.
     */</span>
    async _getUploadUrls(bucketKey, objectKey, parts = <span class="hljs-number">1</span>, firstPart = <span class="hljs-number">1</span>, uploadKey, minutesExpiration) {
        <span class="hljs-keyword">let</span> endpoint = `buckets/${bucketKey}/objects/${<span class="hljs-built_in">encodeURIComponent</span>(objectKey)}/signeds3upload?parts=${parts}&amp;firstPart=${firstPart}`;
        <span class="hljs-keyword">if</span> (uploadKey) {
            endpoint += `&amp;uploadKey=${uploadKey}`;
        }
        <span class="hljs-keyword">if</span> (minutesExpiration) {
            endpoint += `&amp;minutesExpiration=${minutesExpiration}`;
        }
        <span class="hljs-keyword">const</span> headers = {
            <span class="hljs-string">&#39;Content-Type&#39;</span>: <span class="hljs-string">&#39;application/json&#39;</span>,
            <span class="hljs-string">&#39;Authorization&#39;</span>: <span class="hljs-string">&#39;Bearer &#39;</span> + <span class="hljs-keyword">this</span>.token
        };
        <span class="hljs-keyword">const</span> resp = await <span class="hljs-keyword">this</span>.axios.get(endpoint, { headers });
        <span class="hljs-keyword">return</span> resp.data;
    }

    <span class="hljs-comment">/**
     * Finalizes the upload of a file to OSS.
     *
     * @async
     * @param {string} bucketKey Bucket key.
     * @param {string} objectKey Object key.
     * @param {string} uploadKey Upload key returned by {@see _getUploadUrls}.
     * @param {string} [contentType] Optinal content type that should be recorded for the uploaded file.
     * @returns {Promise&lt;object&gt;} Details of the created object in OSS.
     */</span>
    async _completeUpload(bucketKey, objectKey, uploadKey, contentType) {
        <span class="hljs-keyword">const</span> endpoint = `buckets/${bucketKey}/objects/${<span class="hljs-built_in">encodeURIComponent</span>(objectKey)}/signeds3upload`;
        <span class="hljs-keyword">const</span> payload = { uploadKey };
        <span class="hljs-keyword">const</span> headers = {
            <span class="hljs-string">&#39;Content-Type&#39;</span>: <span class="hljs-string">&#39;application/json&#39;</span>,
            <span class="hljs-string">&#39;Authorization&#39;</span>: <span class="hljs-string">&#39;Bearer &#39;</span> + <span class="hljs-keyword">this</span>.token
        };
        <span class="hljs-keyword">if</span> (contentType) {
            headers[<span class="hljs-string">&#39;x-ads-meta-Content-Type&#39;</span>] = contentType;
        }
        <span class="hljs-keyword">const</span> resp = await <span class="hljs-keyword">this</span>.axios.post(endpoint, payload, { headers });
        <span class="hljs-keyword">return</span> resp.data;
    }

    <span class="hljs-comment">/**
     * Uploads content to a specific bucket object.
     *
     * @async
     * @param {string} bucketKey Bucket key.
     * @param {string} objectKey Name of uploaded object.
     * @param {Buffer} data Object content.
     * @param {object} [options] Additional upload options.
     * @param {string} [options.contentType] Optional content type of the uploaded file.
     * @param {number} [options.minutesExpiration] Custom expiration for the upload URLs
     * (within the 1 to 60 minutes range). If not specified, default is 2 minutes.
     * @returns {Promise&lt;object&gt;} Object description containing &#39;bucketKey&#39;, &#39;objectKey&#39;, &#39;objectId&#39;,
     * &#39;sha1&#39;, &#39;size&#39;, &#39;location&#39;, and &#39;contentType&#39;.
     * @throws Error when the request fails, for example, due to insufficient rights, or incorrect scopes.
     */</span>
    async uploadObject(bucketKey, objectKey, data, options) {
        console.assert(data.byteLength &gt; <span class="hljs-number">0</span>);
        <span class="hljs-keyword">const</span> ChunkSize = <span class="hljs-number">5</span> &lt;&lt; <span class="hljs-number">20</span>;
        <span class="hljs-keyword">const</span> MaxBatches = <span class="hljs-number">25</span>;
        <span class="hljs-keyword">const</span> totalParts = <span class="hljs-built_in">Math</span>.ceil(data.byteLength / ChunkSize);
        <span class="hljs-keyword">let</span> partsUploaded = <span class="hljs-number">0</span>;
        <span class="hljs-keyword">let</span> uploadUrls = [];
        <span class="hljs-keyword">let</span> uploadKey;
        <span class="hljs-keyword">while</span> (partsUploaded &lt; totalParts) {
            <span class="hljs-keyword">const</span> chunk = data.slice(partsUploaded * ChunkSize, <span class="hljs-built_in">Math</span>.min((partsUploaded + <span class="hljs-number">1</span>) * ChunkSize, data.byteLength));
            <span class="hljs-keyword">while</span> (<span class="hljs-literal">true</span>) {
                console.debug(<span class="hljs-string">&#39;Uploading part&#39;</span>, partsUploaded + <span class="hljs-number">1</span>);
                <span class="hljs-keyword">if</span> (uploadUrls.length === <span class="hljs-number">0</span>) {
                    <span class="hljs-comment">// Automatically retries 429 and 500-599 responses</span>
                    <span class="hljs-keyword">const</span> uploadParams = await <span class="hljs-keyword">this</span>._getUploadUrls(bucketKey, objectKey, <span class="hljs-built_in">Math</span>.min(totalParts - partsUploaded, MaxBatches), partsUploaded + <span class="hljs-number">1</span>, uploadKey, options?.minutesExpiration);
                    uploadUrls = uploadParams.urls.slice();
                    uploadKey = uploadParams.uploadKey;
                }
                <span class="hljs-keyword">const</span> url = uploadUrls.shift();
                <span class="hljs-keyword">try</span> {
                    await <span class="hljs-keyword">this</span>.axios.put(url, chunk);
                    <span class="hljs-keyword">break</span>;
                } <span class="hljs-keyword">catch</span> (err) {
                    <span class="hljs-keyword">const</span> status = err.response?.status;
                    <span class="hljs-keyword">if</span> (status === <span class="hljs-number">403</span>) {
                        console.debug(<span class="hljs-string">&#39;Got 403, refreshing upload URLs&#39;</span>);
                        uploadUrls = []; <span class="hljs-comment">// Couldn&#39;t this cause an infinite loop? (i.e., could the server keep responding with 403 indefinitely?)</span>
                    } <span class="hljs-keyword">else</span> {
                        <span class="hljs-keyword">throw</span> err;
                    }
                }
            }
            console.debug(<span class="hljs-string">&#39;Part successfully uploaded&#39;</span>, partsUploaded + <span class="hljs-number">1</span>);
            partsUploaded++;
        }
        console.debug(<span class="hljs-string">&#39;Completing part upload&#39;</span>);
        <span class="hljs-keyword">return</span> <span class="hljs-keyword">this</span>._completeUpload(bucketKey, objectKey, uploadKey, options?.contentType);
    }

    <span class="hljs-comment">/**
     * Uploads content stream to a specific bucket object.
     *
     * @async
     * @param {string} bucketKey Bucket key.
     * @param {string} objectKey Name of uploaded object.
     * @param {AsyncIterable&lt;Buffer&gt;} stream Input stream.
     * @param {object} [options] Additional upload options.
     * @param {string} [options.contentType] Optional content type of the uploaded file.
     * @param {number} [options.minutesExpiration] Custom expiration for the upload URLs
     * (within the 1 to 60 minutes range). If not specified, default is 2 minutes.
     * @returns {Promise&lt;object&gt;} Object description containing &#39;bucketKey&#39;, &#39;objectKey&#39;, &#39;objectId&#39;,
     * &#39;sha1&#39;, &#39;size&#39;, &#39;location&#39;, and &#39;contentType&#39;.
     * @throws Error when the request fails, for example, due to insufficient rights, or incorrect scopes.
     */</span>
    async uploadObjectStream(bucketKey, objectKey, input, options) {
        <span class="hljs-comment">// Helper async generator making sure that each chunk has at least certain number of bytes</span>
        async <span class="hljs-function"><span class="hljs-keyword">function</span>* <span class="hljs-title">bufferChunks</span><span class="hljs-params">(input, minChunkSize)</span> {</span>
            <span class="hljs-keyword">let</span> buffer = Buffer.alloc(<span class="hljs-number">2</span> * minChunkSize);
            <span class="hljs-keyword">let</span> bytesRead = <span class="hljs-number">0</span>;
            <span class="hljs-keyword">for</span> await (<span class="hljs-keyword">const</span> chunk of input) {
                chunk.copy(buffer, bytesRead);
                bytesRead += chunk.byteLength;
                <span class="hljs-keyword">if</span> (bytesRead &gt;= minChunkSize) {
                    <span class="hljs-keyword">yield</span> buffer.slice(<span class="hljs-number">0</span>, bytesRead);
                    bytesRead = <span class="hljs-number">0</span>;
                }
            }
            <span class="hljs-keyword">if</span> (bytesRead &gt; <span class="hljs-number">0</span>) {
                <span class="hljs-keyword">yield</span> buffer.slice(<span class="hljs-number">0</span>, bytesRead);
            }
        }

        <span class="hljs-keyword">const</span> MaxBatches = <span class="hljs-number">25</span>;
        <span class="hljs-keyword">const</span> ChunkSize = <span class="hljs-number">5</span> &lt;&lt; <span class="hljs-number">20</span>;
        <span class="hljs-keyword">let</span> partsUploaded = <span class="hljs-number">0</span>;
        <span class="hljs-keyword">let</span> uploadUrls = [];
        <span class="hljs-keyword">let</span> uploadKey;
        <span class="hljs-keyword">for</span> await (<span class="hljs-keyword">const</span> chunk of bufferChunks(input, ChunkSize)) {
            <span class="hljs-keyword">while</span> (<span class="hljs-literal">true</span>) {
                console.debug(<span class="hljs-string">&#39;Uploading part&#39;</span>, partsUploaded + <span class="hljs-number">1</span>);
                <span class="hljs-keyword">if</span> (uploadUrls.length === <span class="hljs-number">0</span>) {
                    <span class="hljs-keyword">const</span> uploadParams = await <span class="hljs-keyword">this</span>._getUploadUrls(bucketKey, objectKey, MaxBatches, partsUploaded + <span class="hljs-number">1</span>, uploadKey, options?.minutesExpiration);
                    uploadUrls = uploadParams.urls.slice();
                    uploadKey = uploadParams.uploadKey;
                }
                <span class="hljs-keyword">const</span> url = uploadUrls.shift();
                <span class="hljs-keyword">try</span> {
                    await <span class="hljs-keyword">this</span>.axios.put(url, chunk);
                    <span class="hljs-keyword">break</span>;
                } <span class="hljs-keyword">catch</span> (err) {
                    <span class="hljs-keyword">const</span> status = err.response?.status;
                    <span class="hljs-keyword">if</span> (status === <span class="hljs-number">403</span>) {
                        console.debug(<span class="hljs-string">&#39;Got 403, refreshing upload URLs&#39;</span>);
                        uploadUrls = []; <span class="hljs-comment">// Couldn&#39;t this cause an infinite loop? (i.e., could the server keep responding with 403 indefinitely?</span>
                    } <span class="hljs-keyword">else</span> {
                        <span class="hljs-keyword">throw</span> err;
                    }
                }
            }
            console.debug(<span class="hljs-string">&#39;Part successfully uploaded&#39;</span>, partsUploaded + <span class="hljs-number">1</span>);
            partsUploaded++;
        }
        console.debug(<span class="hljs-string">&#39;Completing part upload&#39;</span>);
        <span class="hljs-keyword">return</span> <span class="hljs-keyword">this</span>._completeUpload(bucketKey, objectKey, uploadKey, options?.contentType);
    }

    <span class="hljs-comment">/**
     * Generates a signed URL that can be used to download a file from OSS.
     *
     * @async
     * @param {string} bucketKey Bucket key.
     * @param {string} objectKey Object key.
     * @param {number} [minutesExpiration] Custom expiration for the download URLs
     * (within the 1 to 60 minutes range). If not specified, default is 2 minutes.
     * @returns {Promise&lt;object&gt;} Download URLs and potentially other helpful information.
     */</span>
    async _getDownloadUrl(bucketKey, objectKey, minutesExpiration) {
        <span class="hljs-keyword">let</span> endpoint = `buckets/${bucketKey}/objects/${<span class="hljs-built_in">encodeURIComponent</span>(objectKey)}/signeds3download`;
        <span class="hljs-keyword">if</span> (minutesExpiration) {
            endpoint += `?minutesExpiration=${minutesExpiration}`;
        }
        <span class="hljs-keyword">const</span> headers = {
            <span class="hljs-string">&#39;Content-Type&#39;</span>: <span class="hljs-string">&#39;application/json&#39;</span>,
            <span class="hljs-string">&#39;Authorization&#39;</span>: <span class="hljs-string">&#39;Bearer &#39;</span> + <span class="hljs-keyword">this</span>.token
        };
        <span class="hljs-keyword">const</span> resp = await <span class="hljs-keyword">this</span>.axios.get(endpoint, { headers });
        <span class="hljs-keyword">return</span> resp.data;
    }

    <span class="hljs-comment">/**
     * Downloads a specific OSS object.
     *
     * @async
     * @param {string} bucketKey Bucket key.
     * @param {string} objectKey Object key.
     * @param {object} [options] Additional download options.
     * @param {number} [options.minutesExpiration] Custom expiration for the download URLs
     * (within the 1 to 60 minutes range). If not specified, default is 2 minutes.
     * @returns {Promise&lt;ArrayBuffer&gt;} Object content.
     * @throws Error when the request fails, for example, due to insufficient rights, or incorrect scopes.
     */</span>
    async downloadObject(bucketKey, objectKey, options) {
        console.debug(<span class="hljs-string">&#39;Retrieving download URL&#39;</span>);
        <span class="hljs-keyword">const</span> downloadParams = await <span class="hljs-keyword">this</span>._getDownloadUrl(bucketKey, objectKey, options?.minutesExpiration);
        <span class="hljs-keyword">if</span> (downloadParams.status !== <span class="hljs-string">&#39;complete&#39;</span>) {
            <span class="hljs-keyword">throw</span> <span class="hljs-keyword">new</span> <span class="hljs-built_in">Error</span>(<span class="hljs-string">&#39;File not available for download yet.&#39;</span>);
        }
        <span class="hljs-keyword">const</span> resp = await <span class="hljs-keyword">this</span>.axios.get(downloadParams.url, {
            responseType: <span class="hljs-string">&#39;arraybuffer&#39;</span>,
            onDownloadProgress: progressEvent =&gt; {
                <span class="hljs-keyword">const</span> downloadedBytes = progressEvent.currentTarget.response.length;
                <span class="hljs-keyword">const</span> totalBytes = <span class="hljs-built_in">parseInt</span>(progressEvent.currentTarget.responseHeaders[<span class="hljs-string">&#39;Content-Length&#39;</span>]);
                console.debug(<span class="hljs-string">&#39;Downloaded&#39;</span>, downloadedBytes, <span class="hljs-string">&#39;bytes of&#39;</span>, totalBytes);
            }
        });
        <span class="hljs-keyword">return</span> resp.data;
    }

    <span class="hljs-comment">/**
     * Downloads content stream of a specific bucket object.
     *
     * @async
     * @param {string} bucketKey Bucket key.
     * @param {string} objectKey Object name.
     * @param {object} [options] Additional download options.
     * @param {number} [options.minutesExpiration] Custom expiration for the download URLs
     * (within the 1 to 60 minutes range). If not specified, default is 2 minutes.
     * @returns {Promise&lt;ReadableStream&gt;} Object content stream.
     * @throws Error when the request fails, for example, due to insufficient rights, or incorrect scopes.
     */</span>
    async downloadObjectStream(bucketKey, objectKey, options) {
        console.debug(<span class="hljs-string">&#39;Retrieving download URL&#39;</span>);
        <span class="hljs-keyword">const</span> downloadParams = await <span class="hljs-keyword">this</span>._getDownloadUrl(bucketKey, objectKey, options?.minutesExpiration);
        <span class="hljs-keyword">if</span> (downloadParams.status !== <span class="hljs-string">&#39;complete&#39;</span>) {
            <span class="hljs-keyword">throw</span> <span class="hljs-keyword">new</span> <span class="hljs-built_in">Error</span>(<span class="hljs-string">&#39;File not available for download yet.&#39;</span>);
        }
        <span class="hljs-keyword">const</span> resp = await <span class="hljs-keyword">this</span>.axios.get(downloadParams.url, {
            responseType: <span class="hljs-string">&#39;stream&#39;</span>,
            onDownloadProgress: progressEvent =&gt; {
                <span class="hljs-keyword">const</span> downloadedBytes = progressEvent.currentTarget.response.length;
                <span class="hljs-keyword">const</span> totalBytes = <span class="hljs-built_in">parseInt</span>(progressEvent.currentTarget.responseHeaders[<span class="hljs-string">&#39;Content-Length&#39;</span>]);
                console.debug(<span class="hljs-string">&#39;Downloaded&#39;</span>, downloadedBytes, <span class="hljs-string">&#39;bytes of&#39;</span>, totalBytes);
            }
        });
        <span class="hljs-keyword">return</span> resp.data;
    }
}

module.exports = {
    BinaryTransferClient
};</code></pre>
<p>署名済み URL（Signed URL）のデフォルトの有効期限は2分です（<em>minutesExpiration</em> パラメータで最大60分まで有効期限を延長することができます）。&#0160;</p>
<h2>ダウンロード</h2>
<p>まず、ダウンロードの手順からご紹介します。AWS S3 から署名済み URL（Signed URL）を使ってファイルを直接ダウンロードするために、2つのステップを踏む必要があります。以下は、その仕組みを説明する擬似コードです。</p>
<ol dir="auto">
<li><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-signeds3download-GET" rel="nofollow noopener" target="_blank">GET buckets/:bucketKey/objects/:objectName/signeds3download</a>&#0160;エンドポイントを使ってダウンロード用の URL を生成します。</li>
<li>新しいURLを使用して、AWS S3 から直接 OSS オブジェクトをダウンロードします。<br />
<ul dir="auto">
<li>レスポンス コードが 100～199、429、500～599 の場合、ダウンロードの再試行（例えば指数関数的バックオフ）を検討する。</li>
</ul>
</li>
</ol>
<p>以下は、ストリームのダウンロードをおこなう場合のコードです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-1"><span class="hljs-keyword">const</span> fs = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;fs&#39;</span>);
<span class="hljs-keyword">const</span> { BinaryTransferClient } = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;..&#39;</span>);

async <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">downloadStream</span><span class="hljs-params">(filePath, bucketKey, objectKey, accessToken)</span> {</span>
    <span class="hljs-keyword">const</span> client = <span class="hljs-keyword">new</span> BinaryTransferClient(accessToken);
    <span class="hljs-keyword">const</span> stream = await client.downloadObjectStream(bucketKey, objectKey);
    stream.pipe(fs.createWriteStream(filePath));
}

<span class="hljs-keyword">if</span> (process.argv.length &lt; <span class="hljs-number">6</span>) {
    console.log(<span class="hljs-string">&#39;Usage:&#39;</span>);
    console.log(<span class="hljs-string">&#39;node &#39;</span> + __filename + <span class="hljs-string">&#39; &lt;path to local file&gt; &lt;bucket key&gt; &lt;object key&gt; &lt;access token&gt;&#39;</span>);
    process.exit(<span class="hljs-number">0</span>);
}

downloadStream(process.argv[<span class="hljs-number">2</span>], process.argv[<span class="hljs-number">3</span>], process.argv[<span class="hljs-number">4</span>], process.argv[<span class="hljs-number">5</span>]);</code></pre>
<p>また、オブジェクトをローカルファイルにダウンロードする場合（最初にファイル全体をメモリに受信する）には、次のようなコードを使用します。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-2"><span class="hljs-keyword">const</span> fs = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;fs&#39;</span>);
<span class="hljs-keyword">const</span> { BinaryTransferClient } = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;..&#39;</span>);

async <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">downloadBuffer</span><span class="hljs-params">(filePath, bucketKey, objectKey, accessToken)</span> {</span>
    <span class="hljs-keyword">const</span> client = <span class="hljs-keyword">new</span> BinaryTransferClient(accessToken);
    <span class="hljs-keyword">const</span> buffer = await client.downloadObject(bucketKey, objectKey);
    fs.writeFileSync(filePath, buffer);
}

<span class="hljs-keyword">if</span> (process.argv.length &lt; <span class="hljs-number">6</span>) {
    console.log(<span class="hljs-string">&#39;Usage:&#39;</span>);
    console.log(<span class="hljs-string">&#39;node &#39;</span> + __filename + <span class="hljs-string">&#39; &lt;path to local file&gt; &lt;bucket key&gt; &lt;object key&gt; &lt;access token&gt;&#39;</span>);
    process.exit(<span class="hljs-number">0</span>);
}

downloadBuffer(process.argv[<span class="hljs-number">2</span>], process.argv[<span class="hljs-number">3</span>], process.argv[<span class="hljs-number">4</span>], process.argv[<span class="hljs-number">5</span>])
    .then(_ =&gt; <span class="hljs-string">&#39;Done!&#39;</span>)
    .catch(err =&gt; console.error(err));</code></pre>
<h2>アップロード</h2>
<p>次にアップロードの手順をご紹介します。AWS S3 から署名付き URL（Signed URL）を使って直接ファイルをアップロードするには、3 つのステップを踏む必要があります。以下は、その仕組みを説明する擬似コードです。&#0160;</p>
<ol dir="auto">
<li>アップロードするファイルのパーツ数を算出<br />
<ul dir="auto">
<li>&#0160;注意：最後の 1 つを除き、アップロードする各パーツは 5 MB 以上であること</li>
</ul>
</li>
<li><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3upload-GET/">GET buckets/:bucketKey/objects/:objectKey/signeds3upload?firstPart=&lt;index of first part&gt;&amp;parts=&lt;number of parts&gt;</a>&#0160;エンドポイントを使用して特定のパーツのファイルをアップロードするための、最大 25 の URL を生成<br />
<ul dir="auto">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="9">
<p>パーツ番号は 1 から始まるものと仮定  &#0160;</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="9">
<p>例えば、10 番パーツから 15 番パーツまでのアップロード用 URL を生成するには、&lt;index of first part&gt; を 10 に、&lt;number of parts&gt;&#0160;を 6 に設定 &#0160;</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="9">
<p>このエンドポイントは、後で追加の URL を要求したり、アップロードを確定するために使用する uploadKey も返す &#0160;</p>
</li>
</ul>
</li>
<li>残りのパーツ ファイルを、対応するアップロード URL にアップロード &#0160;<br />
<ul dir="auto">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Calibri" data-leveltext="%1." data-listid="13">
<p>レスポンスコードが 100～199、429、500～599 の場合、個々のアップロードの再試行を検討する（例えば指数関数的バックオフを使用）</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Calibri" data-leveltext="%1." data-listid="13">
<p>レスポンスコードが 403 の場合、アップロード用 URL の有効期限が切れているため、上記手順 2. へ戻る &#0160;</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Calibri" data-leveltext="%1." data-listid="13">
<p>アップロード用 URL をすべて使い切ってしまい、まだアップロードする必要があるパーツが存在する場合、手順 2. に戻って URL を生成する &#0160;</p>
</li>
</ul>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="4" data-font="Calibri" data-leveltext="%1." data-listid="17">
<p><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3upload-POST/">POST buckets/:bucketKey/objects/:objectKey/signeds3upload</a>&#0160;エンドポイントを使用して、ステップ 2. からの uploadKey 値を使用してアップロードを確定させる</p>
</li>
</ol>
<p>下記は、ローカルファイルを OSS Bucket に（ストリームとして）アップロードする場合のコードです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-3"><span class="hljs-keyword">const</span> fs = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;fs&#39;</span>);
<span class="hljs-keyword">const</span> { BinaryTransferClient } = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;..&#39;</span>);

async <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">uploadStream</span><span class="hljs-params">(filePath, bucketKey, objectKey, accessToken)</span> {</span>
    <span class="hljs-keyword">const</span> client = <span class="hljs-keyword">new</span> BinaryTransferClient(accessToken);
    <span class="hljs-keyword">const</span> stream = fs.createReadStream(filePath);
    <span class="hljs-keyword">const</span> object = await client.uploadObjectStream(bucketKey, objectKey, stream);
    <span class="hljs-keyword">return</span> object;
}

<span class="hljs-keyword">if</span> (process.argv.length &lt; <span class="hljs-number">6</span>) {
    console.log(<span class="hljs-string">&#39;Usage:&#39;</span>);
    console.log(<span class="hljs-string">&#39;node &#39;</span> + __filename + <span class="hljs-string">&#39; &lt;path to local file&gt; &lt;bucket key&gt; &lt;object key&gt; &lt;access token&gt;&#39;</span>);
    process.exit(<span class="hljs-number">0</span>);
}

uploadStream(process.argv[<span class="hljs-number">2</span>], process.argv[<span class="hljs-number">3</span>], process.argv[<span class="hljs-number">4</span>], process.argv[<span class="hljs-number">5</span>])
    .then(obj =&gt; console.log(obj))
    .catch(err =&gt; console.error(err));</code></pre>
<p>また、ローカルファイルを OSS Bucket にアップロードしたい場合（最初にファイル全体をメモリに読み込む）には、次のようにします。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"><span class="hljs-keyword">const</span> fs = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;fs&#39;</span>);
<span class="hljs-keyword">const</span> { BinaryTransferClient } = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;..&#39;</span>);

async <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">uploadBuffer</span><span class="hljs-params">(filePath, bucketKey, objectKey, accessToken)</span> {</span>
    <span class="hljs-keyword">const</span> client = <span class="hljs-keyword">new</span> BinaryTransferClient(accessToken);
    <span class="hljs-keyword">const</span> buffer = fs.readFileSync(filePath);
    <span class="hljs-keyword">const</span> object = await client.uploadObject(bucketKey, objectKey, buffer);
    <span class="hljs-keyword">return</span> object;
}

<span class="hljs-keyword">if</span> (process.argv.length &lt; <span class="hljs-number">6</span>) {
    console.log(<span class="hljs-string">&#39;Usage:&#39;</span>);
    console.log(<span class="hljs-string">&#39;node &#39;</span> + __filename + <span class="hljs-string">&#39; &lt;path to local file&gt; &lt;bucket key&gt; &lt;object key&gt; &lt;access token&gt;&#39;</span>);
    process.exit(<span class="hljs-number">0</span>);
}

uploadBuffer(process.argv[<span class="hljs-number">2</span>], process.argv[<span class="hljs-number">3</span>], process.argv[<span class="hljs-number">4</span>], process.argv[<span class="hljs-number">5</span>])
    .then(obj =&gt; console.log(obj))
    .catch(err =&gt; console.error(err));</code></pre>
<p>また、Data Management API で Hub（BIM 360、Fusion Teams、ACC など）にローカルファイルをアップロードする方法も忘れてはいけません。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-5"><span class="hljs-keyword">const</span> fs = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;fs&#39;</span>);
<span class="hljs-keyword">const</span> path = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;path&#39;</span>);
<span class="hljs-keyword">const</span> { ProjectsApi, FoldersApi, ItemsApi, VersionsApi } = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;forge-apis&#39;</span>);
<span class="hljs-keyword">const</span> { BinaryTransferClient } = <span class="hljs-built_in">require</span>(<span class="hljs-string">&#39;..&#39;</span>);

async <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">getFolderContents</span><span class="hljs-params">(projectId, folderId, getAccessToken)</span> {</span>
    <span class="hljs-keyword">const</span> resp = await <span class="hljs-keyword">new</span> FoldersApi().getFolderContents(projectId, folderId, {}, <span class="hljs-literal">null</span>, getAccessToken());
    <span class="hljs-keyword">return</span> resp.body.data;
}

async <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">createStorage</span><span class="hljs-params">(projectId, folderId, displayName, getAccessToken)</span> {</span>
    <span class="hljs-keyword">const</span> body = {
        jsonapi: {
            version: <span class="hljs-string">&#39;1.0&#39;</span>
        },
        data: {
            type: <span class="hljs-string">&#39;objects&#39;</span>,
            attributes: {
                name: displayName
            },
            relationships: {
                target: {
                    data: {
                        type: <span class="hljs-string">&#39;folders&#39;</span>,
                        id: folderId
                    }
                }
            }
        }
    };
    <span class="hljs-keyword">const</span> resp = await <span class="hljs-keyword">new</span> ProjectsApi().postStorage(projectId, body, <span class="hljs-literal">null</span>, getAccessToken());
    <span class="hljs-keyword">return</span> resp.body.data;
}

async <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">createItem</span><span class="hljs-params">(projectId, folderId, objectId, displayName, getAccessToken)</span> {</span>
    <span class="hljs-keyword">const</span> body = {
        jsonapi: {
            version: <span class="hljs-string">&#39;1.0&#39;</span>
        },
        data: {
            type: <span class="hljs-string">&#39;items&#39;</span>,
            attributes: {
                displayName,
                extension: {
                    type: <span class="hljs-string">&#39;items:autodesk.core:File&#39;</span>,
                    version: <span class="hljs-string">&#39;1.0&#39;</span>
                }
            },
            relationships: {
                tip: {
                    data: {
                        type: <span class="hljs-string">&#39;versions&#39;</span>,
                        id: <span class="hljs-string">&#39;1&#39;</span>
                    }
                },
                parent: {
                    data: {
                        type: <span class="hljs-string">&#39;folders&#39;</span>,
                        id: folderId
                    }
                }
            }
        },
        included: [
            {
                type: <span class="hljs-string">&#39;versions&#39;</span>,
                id: <span class="hljs-string">&#39;1&#39;</span>,
                attributes: {
                    name: displayName,
                    extension: {
                        type: <span class="hljs-string">&#39;versions:autodesk.core:File&#39;</span>,
                        version: <span class="hljs-string">&#39;1.0&#39;</span>
                    }
                },
                relationships: {
                    storage: {
                        data: {
                            type: <span class="hljs-string">&#39;objects&#39;</span>,
                            id: objectId
                        }
                    }
                }
            }
        ]
    };
    <span class="hljs-keyword">const</span> resp = await <span class="hljs-keyword">new</span> ItemsApi().postItem(projectId, body, <span class="hljs-literal">null</span>, getAccessToken());
    <span class="hljs-keyword">return</span> resp.body.data;
}

async <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">createVersion</span><span class="hljs-params">(projectId, lineageId, objectId, displayName, getAccessToken)</span> {</span>
    <span class="hljs-keyword">const</span> body = {
        jsonapi: {
            version: <span class="hljs-string">&#39;1.0&#39;</span>
        },
        data: {
            type: <span class="hljs-string">&#39;versions&#39;</span>,
            attributes: {
                name: displayName,
                extension: {
                    type: <span class="hljs-string">&#39;versions:autodesk.core:File&#39;</span>,
                    version: <span class="hljs-string">&#39;1.0&#39;</span>
                }
            },
            relationships: {
                item: {
                    data: {
                        type: <span class="hljs-string">&#39;items&#39;</span>,
                        id: lineageId
                    }
                },
                storage: {
                    data: {
                        type: <span class="hljs-string">&#39;objects&#39;</span>,
                        id: objectId
                    }
                }
            }
        }
    };
    <span class="hljs-keyword">const</span> resp = await <span class="hljs-keyword">new</span> VersionsApi().postVersion(projectId, body, <span class="hljs-literal">null</span>, getAccessToken());
    <span class="hljs-keyword">return</span> resp.body.data;
}

async <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">upload</span><span class="hljs-params">(filePath, projectId, folderId, accessToken)</span> {</span>
    <span class="hljs-keyword">const</span> displayName = path.basename(filePath);
    <span class="hljs-keyword">const</span> getAccessToken = () =&gt; {
        <span class="hljs-keyword">return</span> { access_token: accessToken };
    };

    console.log(<span class="hljs-string">&#39;Creating storage...&#39;</span>);
    <span class="hljs-keyword">const</span> storage = await createStorage(projectId, folderId, displayName, getAccessToken);
    console.log(storage);
    <span class="hljs-keyword">const</span> match = <span class="hljs-regexp">/urn:adsk.objects:os.object:([^\/]+)\/(.+)/</span>.exec(storage.id);
    <span class="hljs-keyword">if</span> (!match || match.length &lt; <span class="hljs-number">3</span>) {
        <span class="hljs-keyword">throw</span> <span class="hljs-keyword">new</span> <span class="hljs-built_in">Error</span>(<span class="hljs-string">&#39;Unexpected storage ID&#39;</span>, storage.id);
    }
    <span class="hljs-keyword">const</span> bucketKey = match[<span class="hljs-number">1</span>];
    <span class="hljs-keyword">const</span> objectKey = match[<span class="hljs-number">2</span>];

    console.log(<span class="hljs-string">&#39;Uploading file...&#39;</span>);
    <span class="hljs-keyword">const</span> client = <span class="hljs-keyword">new</span> BinaryTransferClient(accessToken);
    <span class="hljs-keyword">const</span> object = await client.uploadObject(bucketKey, objectKey, fs.readFileSync(filePath));
    console.log(object);

    console.log(<span class="hljs-string">&#39;Checking if file already exists...&#39;</span>);
    <span class="hljs-keyword">const</span> contents = await getFolderContents(projectId, folderId, getAccessToken);
    <span class="hljs-keyword">const</span> item = contents.find(e =&gt; e.type === <span class="hljs-string">&#39;items&#39;</span> &amp;&amp; e.attributes.displayName === displayName);

    <span class="hljs-keyword">if</span> (!item) {
        console.log(<span class="hljs-string">&#39;Creating new item...&#39;</span>);
        <span class="hljs-keyword">const</span> lineage = await createItem(projectId, folderId, object.objectId, displayName, getAccessToken);
        console.log(lineage);
    } <span class="hljs-keyword">else</span> {
        console.log(<span class="hljs-string">&#39;Creating new item version...&#39;</span>);
        <span class="hljs-keyword">const</span> version = await createVersion(projectId, item.id, object.objectId, displayName, getAccessToken);
        console.log(version);
    }
}

<span class="hljs-keyword">if</span> (process.argv.length &lt; <span class="hljs-number">6</span>) {
    console.log(<span class="hljs-string">&#39;Usage:&#39;</span>);
    console.log(<span class="hljs-string">&#39;node &#39;</span> + __filename + <span class="hljs-string">&#39; &lt;path to local file&gt; &lt;project id&gt; &lt;folder id&gt; &lt;access token&gt;&#39;</span>);
    process.exit(<span class="hljs-number">0</span>);
}

upload(process.argv[<span class="hljs-number">2</span>], process.argv[<span class="hljs-number">3</span>], process.argv[<span class="hljs-number">4</span>], process.argv[<span class="hljs-number">5</span>])
    .then(obj =&gt; console.log(<span class="hljs-string">&#39;Done!&#39;</span>))
    .catch(err =&gt; console.error(err));</code></pre>
<p>ご不明な点等ございましたら、<a href="mailto:forge.help@autodesk.com">forge.help@autodesk.com</a>.までお問い合わせください。&#0160;</p>
<p><em>※ 本記事は <a href="https://forge.autodesk.com/ja/node/2222" rel="noopener" target="_blank">Direct-to-S3 Node.js samples | Autodesk Forge</a>&#0160;から転写・翻訳して一部加筆したものです。</em></p>
<p>By Toshiaki Isezaki</p>
