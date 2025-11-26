---
layout: "post"
title: "Absolute Simple Canvas Game"
date: "2015-01-26 10:08:33"
author: "Madhukar Moogala"
categories: []
original_url: "https://adndevblog.typepad.com/autocad/2015/01/absolute-simple-canvas-game.html "
typepad_basename: "absolute-simple-canvas-game"
typepad_status: "Draft"
---

<p>This is my first blog in C&amp;M, for sometime now I was exploring myself to understand HTML/JS/CSS,I read and got comfort my self&#160; with basic language semantics, but what now, out of abundant resources on web I was at crossroads and not sure where to start,then something struck to me that learning should be engaging so I explored for “gaming in JS” and Google amusingly returned with a towering figure of hits in fraction of seconds “<em>About 11,40,00,000 results (0.33 seconds)</em> “, patiently skimmed through various resources made cursory reading for some, detail study for others, but this piece of <a href="http://jdstraughan.com/2013/03/05/html5-snake-with-source-code-walkthrough/" target="_blank">resource</a> made my day.</p>  <p>&#160;</p>  <p>I created a simple game where player controls a box movement and tries to catch the resource that pops on the canvas.No fancy,&#160; no big deal.</p>  <div id="d2" style="position: absolute; left: 400px; z-index: 2; top: 100px"><canvas id="basecanvas" height="480" width="640"></canvas></div> <script type="text/javascript">
               
            var canvas = document.getElementById("basecanvas");
            var size = canvas.width / 40;
            var context = canvas.getContext("2d");
            var dx = 4, dy = 2;
            context.font = "20px Georgia";
            context.fillText("Use Arrow Keys", size, size);
            context.strokeRect(0, 0, canvas.width, canvas.height);
     

            /*create a Box object with two methods draw and move*/
            box = {
                x: 200, y: 200,
                key: 38,
                fps:8,
                score: -1,
                start: function () {
                    /* To be used if requestAnimateFrame isn't used*/
                    //box.draw(box.x, box.y);
                    box.fps = 8;
                    food.draw();
                },
                mass : size,
                drawScore: function () {
                    context.fillStyle = '#999';
                    context.font = (canvas.height) + 'px Impact, sans-serif';
                    context.textAlign = 'center';
                    context.fillText(box.score, canvas.width / 2, canvas.height * .9);
                },
                draw: function (x, y) {

                    /*To confine box movement within Bounds*/

                    if (x + box.mass / 2 >= canvas.width || x - box.mass / 2 <= 0)
                    {
                        x = (x + box.mass / 2 >= canvas.width) ? box.mass * 2 : canvas.width - box.mass * 2;
                        box.x = x;
                    }
                    if (y + box.mass / 2 >= canvas.height || y - box.mass / 2 <= 0)
                    {
                        y = (y + box.mass / 2 >= canvas.height) ? box.mass * 2 : canvas.height - box.mass * 2;
                        box.y = y;
                    }

                    /*For colors :http://www.w3schools.com/tags/ref_colorpicker.asp */
                    context.font = "20px Georgia";
                    context.fillText("Use Arrow Keys", size, size);
                    context.strokeRect(0, 0, canvas.width, canvas.height);
                    context.fillStyle = '#CC0000';
                    context.beginPath();
                    /*positioning box at x,y*/
                    context.moveTo(x - box.mass / 2, y - box.mass / 2);
                    context.lineTo(x + box.mass / 2, y - box.mass / 2);
                    context.lineTo(x + box.mass / 2, y + box.mass / 2);
                    context.lineTo(x - box.mass / 2, y + box.mass / 2);
                    context.closePath();
                    context.fill();
                    

                },
                move: function () {

                    switch (box.key.toString()) {
                        case '38':
                            // box.clear();
                            box.y -= size * 2;

                            break;
                        case '40':
                            // box.clear();
                            box.y += size * 2;

                            break;
                        case '37':
                            //box.clear();
                            box.x -= size * 2;

                            break;
                        case '39':
                            // box.clear();
                            box.x += size * 2;

                            break;
                    }
                    box.checkCapture();
                },
                
                /*This we will use if requestAnimateFrame is n't used*/
                clear: function () {
                    context.clearRect(0, 0, canvas.width, canvas.height);
                    context.font = "20px Georgia";
                    context.fillText("Use Arrow Keys", size, size);
                    context.strokeRect(0, 0, canvas.width, canvas.height);
                },
                reset: function () {
                    context.clearRect(0, 0, canvas.width, canvas.height);

                },

            /**/
                checkCapture: function () {
                    if (box.x < food.x + food.size &&
                        box.x + box.mass > food.x &&
                        box.y < food.y + food.size &&
                        box.mass + box.y > food.y) {
                        food.set();
                        box.score++;
                        box.drawScore();
                        return true;

                      
                    }
                    else
                    {
                        return false;
                    }
                    }
        };

            food = {
                x: 400,
                y: 300,
                size: null,
                set:function()
                {
                    food.size = canvas.width/40;
                    food.x = Math.ceil(Math.random() * canvas.width) ;
                    food.y = Math.ceil(Math.random() * canvas.height);

                },

                draw:function()
                {
                    food.size = canvas.width / 40;
                    context.fillStyle = '#66FF33';
                    context.beginPath();
                    /*positioning box at x,y*/
                    context.moveTo(food.x - food.size / 2, food.y - food.size / 2);
                    context.lineTo(food.x + food.size / 2, food.y - food.size / 2);
                    context.lineTo(food.x + food.size / 2, food.y + food.size / 2);
                    context.lineTo(food.x - food.size / 2, food.y + food.size / 2);
                    context.closePath();
                    context.fill();

                }

            };
            /*up,down,left,right,start*/
            var keys = [38, 40, 37, 39];

            /*
                             Canvas  0,0 ------------>x
                                     |
                                     |
                                     |
                                     |
                                     y
            */

            addEventListener('keydown', function (e) {
                if (keys.indexOf(e.keyCode) >= 0) {
                    box.key = e.keyCode;
                     /*if requestAnimationFrame not used*/
                    //box.move();
                }
                else if (e.keyCode == '32') {
                    /*This may not be used  if we are using requestAnimationFrame */
                    box.start();
                  
                }
                else;

            }, false);
            var w = window;
            requestAnimationFrame = w.requestAnimationFrame ||
                                    w.webkitRequestAnimationFrame ||
                                    w.msRequestAnimationFrame ||
                                    w.mozRequestAnimationF;

            function loop() {
                box.reset();
                
               box.move();
               box.draw(box.x, box.y);
               food.draw();
                setTimeout(function () {
                    requestAnimationFrame(loop);
                }, 1000 / box.fps);
            };

            requestAnimationFrame(loop);
        </script>
