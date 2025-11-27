---
layout: "post"
title: "Understanding STL Translator Options and Value Types in Inventor API"
date: "2025-01-03 11:00:06"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/01/understanding-stl-translator-options-and-value-types-in-inventor-api.html "
typepad_basename: "understanding-stl-translator-options-and-value-types-in-inventor-api"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,</p>
<p>When exporting files to STL format in Autodesk Inventor, users often come across different translator options. These include <em>Surface Deviation</em>, <em>Normal Deviation</em>, <em>MaxEdgeLength</em>, and <em>Aspect Ratio</em>, which are important parameters in defining the quality of the exported STL file. However, a common question that arises is the discrepancy between how these options are represented in the Inventor UI and how they are handled through the Inventor API.</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c83139200c-pi" style="display: inline;"><img alt="Untitled" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c83139200c img-responsive" src="/assets/image_04708d.jpg" title="Untitled" /></a></p>
<h3>The Question</h3>
<p>In the Inventor User Interface (UI), the STL translator options—<em>Surface Deviation</em>, <em>Normal Deviation</em>, <em>MaxEdgeLength</em>, and <em>Aspect Ratio</em>—are accepted as <strong>double</strong> values. This means that you can input values with decimal precision for fine-tuning the export options, as shown in the UI settings.</p>
<p>However, the Inventor API documentation mentions that these same STL translator options are expected to be in <strong>integer</strong> format. This raises confusion, especially when developers are trying to pass these values through the API for automation or custom applications.</p>
<p>For clarity, here’s how the translator options and their expected value types are represented in the Inventor API documentation:</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c83131200c-pi" style="display: inline;"><img alt="Screenshot 2025-01-04 002343" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c83131200c img-responsive" src="/assets/image_c66c14.jpg" title="Screenshot 2025-01-04 002343" /></a></p>
<h3>The Answer: Converting Double to Integer</h3>
<p>To resolve the confusion, it is important to understand how the Inventor API processes these values. Typically, the STL translator internally converts the integer values to <strong>double</strong> types by dividing the integer value by <strong>100.0</strong>. This approach allows for greater precision in the final output while still adhering to the integer input requirement in the API.</p>
<p>For example, if you input a value of <strong>2000</strong> for the <em>Aspect Ratio</em> in the API, it will be internally converted to <strong>20.00</strong> when applied in the STL translator. Additionally, if the input value exceeds the valid range, the translator will automatically default to the maximum allowable value (21.5).</p>
<h3>Default Values and Resolution Settings</h3>
<p>The Inventor API provides default values for different resolutions, which allow you to specify the level of detail in the exported STL file. These resolution settings are divided into <strong>High</strong>, <strong>Medium</strong>, and <strong>Low</strong> categories, as shown in the code snippet below:</p>
<pre><code>
// High resolution defaults
const int HIGH_SUR_DEV = 5;
const int HIGH_NOR_DEV = 1000; // (10 degrees)
const int HIGH_MAX_EDGE_LEN = MAX_EDGE_LENGTH_SLIDER_MAX; // (100.0)
const int HIGH_ASPECT_RATIO = ASPECT_RATIO_SLIDER_MAX; // (21.5)

// Medium resolution defaults
const int MEDI_SUR_DEV = 16;
const int MEDI_NOR_DEV = 1500; // (15 degrees)
const int MEDI_MAX_EDGE_LEN = MAX_EDGE_LENGTH_SLIDER_MAX; // (100.0)
const int MEDI_ASPECT_RATIO = ASPECT_RATIO_SLIDER_MAX; // (21.5)

// Low resolution defaults
const int LOW_SUR_DEV = 40;
const int LOW_NOR_DEV = 3000; // (30 degrees)
const int LOW_MAX_EDGE_LEN = MAX_EDGE_LENGTH_SLIDER_MAX; // (100.0)
const int LOW_ASPECT_RATIO = ASPECT_RATIO_SLIDER_MAX; // (21.5)
</code></pre>
<h3>Key Points to Remember</h3>
<ul>
<li><strong>Maximum Edge Length</strong> and <strong>Aspect Ratio</strong> have default values of <strong>100.0</strong> and <strong>21.5</strong>, respectively, across all resolutions (Low, Medium, and High).</li>
<li>The <em>Surface Deviation</em>, <em>Normal Deviation</em>, <em>MaxEdgeLength</em>, and <em>Aspect Ratio</em> values are expected as integers in the API but are internally processed as doubles by dividing the integer values by <strong>100.0</strong>.</li>
<li>If input values exceed the specified range, the translator will default to the maximum allowed values.</li>
</ul>
<p>By understanding how the Inventor API handles STL export settings, developers can more accurately configure and automate their workflows, ensuring that the exported STL files meet the desired quality requirements.</p>
