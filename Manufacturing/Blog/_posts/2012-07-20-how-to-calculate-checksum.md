---
layout: "post"
title: "How to calculate checksum"
date: "2012-07-20 16:54:36"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/how-to-calculate-checksum.html "
typepad_basename: "how-to-calculate-checksum"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>Several functions such as FindFilePathsByNameAndChecksum() in the DocumentService class take a Checksum for one of the parameters. The checksum is a 32 bit integer based on a file&#39;s binary data. Checksums can be used to determine if two files are the same.</p>
<p>This VB.NET project uses FindFilePathsByNameAndChecksum() to see if a checked out file on disk is in the vault. (It is an update to the VaultList 2013 SDK sample).</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b0177437dda48970d"><a href="http://adndevblog.typepad.com/files/vaultlist_checksum.zip">Download VaultList_CheckSum</a></span></p>
<p>Note: You will need to replace the reference to Autodesk.Connectivity.WebServices. ( In - C:\Program Files (x86)\Autodesk\Autodesk Vault 2013 SDK\bin)</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01761697a5a2970c-pi"><img alt="image" border="0" height="242" src="/assets/image_e02849.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="518" /></a></p>
<p><br />Here is a C# example for calculating the checksum.</p>
<p><span style="color: blue;">using</span> System;</p>
<div style="font-family: consolas; background: white; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">using</span> System.IO;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> DocumentSample</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">class</span> <span style="color: #2b91af;">CheckSum</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">public</span> CheckSum()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// CRC check sum data</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">//</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">private</span> <span style="color: blue;">const</span> <span style="color: blue;">int</span> m_chunkSize = 16384;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">uint</span>[] m_crc_32_tab =</p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160; 0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x076DC419, 0x706AF48F, 0xE963A535, 0x9E6495A3,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x0EDB8832, 0x79DCB8A4, 0xE0D5E91E, 0x97D2D988,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x09B64C2B, 0x7EB17CBD, 0xE7B82D07, 0x90BF1D91,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x136C9856, 0x646BA8C0, 0xFD62F97A, 0x8A65C9EC,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x14015C4F, 0x63066CD9, 0xFA0F3D63, 0x8D080DF5,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x3B6E20C8, 0x4C69105E, 0xD56041E4, 0xA2677172,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x32D86CE3, 0x45DF5C75, 0xDCD60DCF, 0xABD13D59,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x26D930AC, 0x51DE003A, 0xC8D75180, 0xBFD06116,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x21B4F4B5, 0x56B3C423, 0xCFBA9599, 0xB8BDA50F,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x76DC4190, 0x01DB7106, 0x98D220BC, 0xEFD5102A,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x71B18589, 0x06B6B51F, 0x9FBFE4A5, 0xE8B8D433,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x7807C9A2, 0x0F00F934, 0x9609A88E, 0xE10E9818,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x6C0695ED, 0x1B01A57B, 0x8208F4C1, 0xF50FC457,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x65B0D9C6, 0x12B7E950, 0x8BBEB8EA, 0xFCB9887C,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3, 0xFBD44C65,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x4369E96A, 0x346ED9FC, 0xAD678846, 0xDA60B8D0,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x44042D73, 0x33031DE5, 0xAA0A4C5F, 0xDD0D7CC9,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x5005713C, 0x270241AA, 0xBE0B1010, 0xC90C2086,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x59B33D17, 0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xEDB88320, 0x9ABFB3B6, 0x03B6E20C, 0x74B1D29A,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xEAD54739, 0x9DD277AF, 0x04DB2615, 0x73DC1683,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xF00F9344, 0x8708A3D2, 0x1E01F268, 0x6906C2FE,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xF762575D, 0x806567CB, 0x196C3671, 0x6E6B06E7,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xFED41B76, 0x89D32BE0, 0x10DA7A5A, 0x67DD4ACC,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xD1BB67F1, 0xA6BC5767, 0x3FB506DD, 0x48B2364B,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xD80D2BDA, 0xAF0A1B4C, 0x36034AF6, 0x41047A60,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xDF60EFC3, 0xA867DF55, 0x316E8EEF, 0x4669BE79,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xC5BA3BBE, 0xB2BD0B28, 0x2BB45A92, 0x5CB36A04,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xC2D7FFA7, 0xB5D0CF31, 0x2CD99E8B, 0x5BDEAE1D,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x9B64C2B0, 0xEC63F226, 0x756AA39C, 0x026D930A,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x92D28E9B, 0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x86D3D2D4, 0xF1D4E242, 0x68DDB3F8, 0x1FDA836E,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x81BE16CD, 0xF6B9265B, 0x6FB077E1, 0x18B74777,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C,</p>
<p style="margin: 0px;">&#0160;&#0160; 0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xA00AE278, 0xD70DD2EE, 0x4E048354, 0x3903B3C2,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xA7672661, 0xD06016F7, 0x4969474D, 0x3E6E77DB,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xAED16A4A, 0xD9D65ADC, 0x40DF0B66, 0x37D83BF0,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xBAD03605, 0xCDD70693, 0x54DE5729, 0x23D967BF,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xB3667A2E, 0xC4614AB8, 0x5D681B02, 0x2A6F2B94,</p>
<p style="margin: 0px;">&#0160;&#0160; 0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B, 0x2D02EF8D,</p>
<p style="margin: 0px;">&#0160; };</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">int</span> CalcCRC32(<span style="color: blue;">string</span> filename)</p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">FileStream</span> stream = <span style="color: blue;">new</span> <span style="color: #2b91af;">FileStream</span>(filename,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">FileMode</span>.Open, <span style="color: #2b91af;">FileAccess</span>.Read,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">FileShare</span>.Read, m_chunkSize);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">int</span> crc = CalcCRC32(stream);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; stream.Close();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> crc;</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">int</span> CalcCRC32(<span style="color: #2b91af;">Stream</span> stream)</p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (stream == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">throw</span> <span style="color: blue;">new</span> <span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Checksum.CalcCRC32() - Invalid stream&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (stream.CanRead == <span style="color: blue;">false</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">throw</span> <span style="color: blue;">new</span> <span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Checksum.CalcCRC32() - Cannot read stream&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">uint</span> crc32 = 0xFFFFFFFF;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; stream.Position = 0; <span style="color: green;">// beginning</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">int</span> bytesRead = 0;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">byte</span>[] streamData = <span style="color: blue;">new</span> <span style="color: blue;">byte</span>[m_chunkSize];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">do</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bytesRead = stream.Read</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (streamData, 0, m_chunkSize);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; bytesRead; i++)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; crc32 = (m_crc_32_tab[(crc32 ^</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; streamData[i]) &amp; 0xff] ^ (crc32 &gt;&gt; 8));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; } <span style="color: blue;">while</span> (bytesRead &gt; 0);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; stream.Position = 0; <span style="color: green;">// reset</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; crc32 = ~crc32;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> (<span style="color: blue;">int</span>)(crc32);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>This code snippet explains how you would use the CheckSum class above:</p>
<div style="font-family: consolas; background: white; color: black; font-size: 10pt;">
<p style="margin: 0px;">checkSum objCheckSum = <span style="color: blue;">new</span> CheckSum();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">int</span> localFCS = objCheckSum.CalcCRC32</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (localFilePath);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; FilePath[] fPaths =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; docSrv.FindFilePathsByNameAndChecksum</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Drawing1.idw&quot;</span>, localFCS);</p>
</div>
