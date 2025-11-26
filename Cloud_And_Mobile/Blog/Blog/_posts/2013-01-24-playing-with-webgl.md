---
layout: "post"
title: "Playing with WebGL"
date: "2013-01-24 05:35:53"
author: "Partha Sarkar"
categories:
  - "Javascript"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2013/01/playing-with-webgl.html "
typepad_basename: "playing-with-webgl"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>There are very good resources and tutorials available to get
started with WebGL and a quick online search will bring all those links to
you. So, why am I interested about WebGL ? &nbsp;Answer is simple : <strong>WebGL brings plugin-free 3D to the web,
implemented right into the browser</strong>. So, let's explore WebGL and my intention
here is to start with simple steps of creating some basic geometries and render
them. With progress in our WebGL learning, I want to see how we can integrate this
with Autodesk technologies.</p>
<p>First
let's see what is WebGL ? Here is the technical definition of WebGL from <strong>khronos.org</strong> (<a href="http://www.khronos.org/webgl/">http://www.khronos.org/webgl/</a></p>
<p><em>WebGL is a cross-platform, royalty-free web standard for a
low-level 3D graphics API based on OpenGL ES 2.0, exposed through the HTML5
Canvas element as Document Object Model interfaces. Developers familiar with OpenGL
ES 2.0 will recognize WebGL as a Shader-based API using GLSL, with constructs
that are semantically similar to those of the underlying OpenGL ES 2.0 API. It
stays very close to the OpenGL ES 2.0 specification, with some concessions made
for what developers expect out of memory-managed languages such as JavaScript.</em></p>
<p>After going through some of the online tutorials and
resources, here is my First WebGL sample which simply creates a triangle in a
WebGL supported browser :</p>

<html>
	<head>	
		<title>DEMO WebGL Application </title>
		<style type="text/css">
		#myCanvas {
			border: thin solid;
		}
		button {
			display: block;
			width: 100px; 
		}
		#myButtons {
			float: down;
		}
		</style>
		<script id="vertex" type="x-shader"> 
		    attribute vec2 vertexPosition;
			
		    void main() {
		        gl_Position = vec4(vertexPosition, 0.0, 1.0);
		    }			
		</script> 
		
		<script id="fragment" type="x-shader">
			#ifdef GL_ES
    		precision highp float;
    		#endif
			
			uniform vec4 uColor;
			
			void main() {
				gl_FragColor = uColor;
		    }
		</script> 
		
		<script type="text/javascript">
		
// Initialize WebGL
		
	function initWebGL(canvas) {	  
	  
	  // Initialize the global variable gl to null.
	  gl = null;
	   
	  try {
		// try to get the standard context. If it fails, fallback to experimental.
		gl = canvas.getContext("webgl") || canvas.getContext("experimental-webgl");
	  }
	  catch(e) {}
	   
	  // exit here, if you don't get a GL context
	  if (!gl) {
		alert("Unable to initialize WebGL. Your browser may not support it!");
	  }
	}
		
		
		
	function ShowGraphics(){
			
				// Initialize the Canvas 
				var canvas = document.getElementById("mycanvas");				
				initWebGL(canvas);      // Initialize the GL context
   
			  // continue if WebGL is available 
			   
			  if (gl) {
				gl.clearColor(1.0, 1.0, 0.0, 1.0);  
				gl.enable(gl.DEPTH_TEST);           
				gl.depthFunc(gl.LEQUAL);            
				gl.clear(gl.COLOR_BUFFER_BIT|gl.DEPTH_BUFFER_BIT);      
			  }

				// compile and link the shaders
				var v = document.getElementById("vertex").firstChild.nodeValue;
				var f = document.getElementById("fragment").firstChild.nodeValue;
				
				var vs = gl.createShader(gl.VERTEX_SHADER);
				gl.shaderSource(vs, v);
				gl.compileShader(vs);
				
				var fs = gl.createShader(gl.FRAGMENT_SHADER);
				gl.shaderSource(fs, f);
				gl.compileShader(fs);
								
				if (!gl.getShaderParameter(vs, gl.COMPILE_STATUS))
				{
					 alert("Error in vertex shader");
					 gl.deleteShader(vs);
					 return;
				}			
									
				if (!gl.getShaderParameter(fs, gl.COMPILE_STATUS)) 
					{
					 alert("Error in fragment shader");
					 gl.deleteShader(fs);
					 return;
					}
				
				// now create a program object
				// and link the shaders
				
				program = gl.createProgram();
				gl.attachShader(program, vs);
				gl.attachShader(program, fs);
				gl.linkProgram(program);
				
				if (!gl.getProgramParameter(program, gl.LINK_STATUS)) 
				{
					alert("Error in shaders");
					 gl.deleteProgram(program);
					 gl.deleteProgram(vs);
					 gl.deleteProgram(fs);
					 return;
				}	

				gl.useProgram(program); 
								
				//get the reference to the attribute in the shader 
				// and then enable it for rendering
				var vertexPos = gl.getAttribLocation(program, "vertexPosition");
				gl.enableVertexAttribArray(vertexPos);
				
				
				// create buffer object
				var vertexBuffer = gl.createBuffer();
				
				// bind to WebGl object's array buffer
				gl.bindBuffer(gl.ARRAY_BUFFER, vertexBuffer);
				
				//specify the data structure of the array 
				// that will be used to store the data
				
				gl.vertexAttribPointer(vertexPos, 
											 3.0, 
											 gl.FLOAT, 
											 false, 
											 0, 0);
				
				// lets draw a triangle				
				var aspect = canvas.width / canvas.height;	
							
				var vertices = new Float32Array( [-0.5,  0.2, 0.0,
								  -0.5, -0.5, 0.0, 
								   0.8, -0.5, 0.0]);
								   
				//bufferData method transfers the vertex data in the JavaScript object 
				// to the WebGL object's buffer.				
				gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);
				
				// setting uniforms and attributes				
				
				program.uColor = gl.getUniformLocation(program, "uColor");
				gl.uniform4fv(program.uColor, [0.0, 0.0, 1.0, 1.0]);
								
				// now draw the traingle
				gl.drawArrays(gl.TRIANGLES, 0, vertices.length / 3.0);
				gl.flush();
			  
			}
			
	function Clear(){
			
				// Initialize the Canvas 
				var canvas = document.getElementById("mycanvas");				
				initWebGL(canvas);      // Initialize the GL context
   
			  // continue if WebGL is available 
			   
			  if (gl) {
			  
				// turn on the scissor test.
				gl.enable(gl.SCISSOR_TEST);

				// set the scissor rectangle.
				gl.scissor(0, 0, canvas.width, canvas.height);

				// clear.
				gl.clearColor(1.0, 1.0, 1.0, 1.0);
				gl.clear(gl.COLOR_BUFFER_BIT|gl.DEPTH_BUFFER_BIT);

				// turn off the scissor test 
				gl.disable(gl.SCISSOR_TEST);				
							
			  }
			}			  
		</script>
	</head>
<!--<body onload="init()">-->
	<body>
		<canvas id="mycanvas" width="450" height="300"
		style="border:5px solid #000000;">
		</canvas>
		<form>		
			<input type="button" onclick="ShowGraphics()" value="Click to See WebGL in Action !">			
			<input type="button" onclick="Clear()" value="Clear">
		</form>		
	</body>
</html>

<p>&nbsp;</p>
<p>Here is the link to 
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c36363a7b970b"><a href="http://adndevblog.typepad.com/files/mywebgldemosample01-1.zip">Download</a></span><strong>&nbsp;</strong>the sample code if you want to give
it a try at your end.</p>
