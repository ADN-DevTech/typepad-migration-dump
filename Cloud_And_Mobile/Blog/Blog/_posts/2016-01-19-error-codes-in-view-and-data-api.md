---
layout: "post"
title: "Error Codes in View and Data API"
date: "2016-01-19 23:54:27"
author: "Shiya Luo"
categories:
  - "Shiya Luo"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/01/error-codes-in-view-and-data-api.html "
typepad_basename: "error-codes-in-view-and-data-api"
typepad_status: "Publish"
---

<p>Sometime when you load a file in the viewer and it pops an alert that says &quot;Load Error: 9&quot;. What does this mean?</p>
<p>If you look at the source code in <a href="https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.js">version 1.2.21</a>, at line 58240 (!!!) you can find the reasons for all the errors. The most common two are 4 (invalid token) and 9 (file isn&#39;t translated yet).</p>
<pre><code>/**
 * Error code constants
 *
 * These constants will be used in onErrorCallbacks.
 *
 * @enum {number}
 * @readonly
 */
Autodesk.Viewing.ErrorCodes = {
    /** An unknown failure has occurred. */
    UNKNOWN_FAILURE: 1,

    /** Bad data (corrupted or malformed) was encountered. */
    BAD_DATA: 2,

    /** A network failure was encountered. */
    NETWORK_FAILURE: 3,

    /** Access was denied to a network resource (HTTP 403) */
    NETWORK_ACCESS_DENIED: 4,

    /** A network resource could not be found (HTTP 404) */
    NETWORK_FILE_NOT_FOUND: 5,

    /** A server error was returned when accessing a network resource (HTTP 5xx) */
    NETWORK_SERVER_ERROR: 6,

    /** An unhandled response code was returned when accessing a network resource (HTTP &#39;everything else&#39;) */
    NETWORK_UNHANDLED_RESPONSE_CODE: 7,

    /** Browser error: webGL is not supported by the current browser */
    BROWSER_WEBGL_NOT_SUPPORTED: 8,

    /** There is nothing viewable in the fetched document */
    BAD_DATA_NO_VIEWABLE_CONTENT: 9,

    /** Browser error: webGL is supported, but not enabled */
    BROWSER_WEBGL_DISABLED: 10,
    
    /** Collaboration server error */
    RTC_ERROR: 11

};</code></pre>
