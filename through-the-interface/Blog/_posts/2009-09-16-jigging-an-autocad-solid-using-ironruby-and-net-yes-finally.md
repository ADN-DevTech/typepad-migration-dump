---
layout: "post"
title: "Jigging an AutoCAD solid using IronRuby and .NET (yes, finally)"
date: "2009-09-16 11:37:29"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "IronRuby"
  - "Jigs"
  - "Ruby"
  - "Solid modeling"
original_url: "https://www.keanw.com/2009/09/jigging-an-autocad-solid-using-ironruby-and-net-yes-finally.html "
typepad_basename: "jigging-an-autocad-solid-using-ironruby-and-net-yes-finally"
typepad_status: "Publish"
---

<p>Back in April I posted <a href="http://through-the-interface.typepad.com/through_the_interface/2009/04/jigging-an-autocad-solid-using-ironruby-and-net-well-almost.html">an IronRuby sample</a> that I had hoped would cause AutoCAD to jig a box in 3D, just like <a href="http://through-the-interface.typepad.com/through_the_interface/2009/03/jigging-an-autocad-solid-using-ironpython-and-net.html">its IronPython counterpart</a>.</p>
<p>The sample didn’t work with IronRuby 0.3, but recently David Blackmon got in touch to let me know he had a version of the code working with IronRuby 0.9. Now that I’ve started preparing for my upcoming AU class, <a href="http://au.autodesk.com/?nd=e_class&amp;session_id=5125">AutoCAD® .NET: Developing for AutoCAD® Using IronPython and IronRuby</a>, I decided to take a closer look at the update to IronRuby and more specifically at the changes to the code David made to get it to work.</p>
<p>David has been having some real fun with IronRuby and AutoCAD: for those interested, I strongly recommend checking out <a href="http://github.com/davidbl/IronRuby-Autocad-Helper/tree/master">the work he’s posted on github</a>. David claims to have been inspired by my previous IronRuby posts to develop some helper classes for AutoCAD development and the results are really interesting: having someone passionate about Ruby take a look at this is great, as it demonstrates to the rest of us some of the possibilities.</p>
<p>I haven’t used these helpers in the below code – which only borrows the tricks David used to get the jig to work – but that’s more for consistency with my previous post than for any other reason.</p>
<p>Here’s the updated IronRuby code, with the modified/additional lines marked in <font color="#ff0000">red</font> (and the full source file can be downloaded from <span class="at-xid-6a00d83452464869e20120a573bc9c970b"><a href="http://through-the-interface.typepad.com/files/solidjig.rb">here</a></span>):</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160;&#0160;&#0160; 1</span>&#0160;<span style="LINE-HEIGHT: 140%">require &#39;acmgd.dll&#39;</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160;&#0160;&#0160; 2</span>&#0160;<span style="LINE-HEIGHT: 140%">require &#39;acdbmgd.dll&#39;</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 3</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 4</span>&#0160;<span style="LINE-HEIGHT: 140%">Ai =&#0160; Autodesk::AutoCAD::Internal</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 5</span>&#0160;<span style="LINE-HEIGHT: 140%">Aiu = Autodesk::AutoCAD::Internal::Utils</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 6</span>&#0160;<span style="LINE-HEIGHT: 140%">Aas = Autodesk::AutoCAD::ApplicationServices</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 7</span>&#0160;<span style="LINE-HEIGHT: 140%">Ads = Autodesk::AutoCAD::DatabaseServices</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 8</span>&#0160;<span style="LINE-HEIGHT: 140%">Aei = Autodesk::AutoCAD::EditorInput</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160;&#0160; 9</span>&#0160;<span style="LINE-HEIGHT: 140%">Ag =&#0160; Autodesk::AutoCAD::Geometry</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 10</span>&#0160;<span style="LINE-HEIGHT: 140%">Ar =&#0160; Autodesk::AutoCAD::Runtime</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 11</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 12</span>&#0160;<span style="LINE-HEIGHT: 140%">def print_message(msg)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 13</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; app = Aas::Application</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 14</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; doc = app.DocumentManager.MdiActiveDocument</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 15</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; ed = doc.Editor</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 16</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; ed.WriteMessage(msg)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 17</span>&#0160;<span style="LINE-HEIGHT: 140%">end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 18</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 19</span>&#0160;<span style="LINE-HEIGHT: 140%"># Function to register AutoCAD commands</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 20</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 21</span>&#0160;<span style="LINE-HEIGHT: 140%">def autocad_command(cmd)&#0160; </span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 22</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; cc = Ai::CommandCallback.new method(cmd)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 23</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; Aiu.AddCommand(</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 24</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; &#39;rbcmds&#39;, cmd, cmd, Ar::CommandFlags.Modal, cc)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 25</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 26</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; # Let&#39;s now write a message to the command-line</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 27</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 28</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; print_message(&quot;\nRegistered Ruby command: &quot; + cmd)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 29</span>&#0160;<span style="LINE-HEIGHT: 140%">end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 30</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 31</span>&#0160;<span style="LINE-HEIGHT: 140%">def add_commands(names)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 32</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; names.each { |n| autocad_command n }</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 33</span>&#0160;<span style="LINE-HEIGHT: 140%">end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 34</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 35</span>&#0160;<span style="LINE-HEIGHT: 140%"># Let&#39;s do something a little more complex...</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 36</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 37</span>&#0160;<span style="LINE-HEIGHT: 140%">class SolidJig &lt; Aei::EntityJig</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 38</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160;&#0160; 39</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; # Constructor</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 40</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160;&#0160; 41</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; def SolidJig.new(ent)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 42</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; super</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 43</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 44</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 45</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; # The function called to run the jig</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 46</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160;&#0160; 47</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; def start_jig(ed, pt)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 48</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 49</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; # The start point is specified outside the jig</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 50</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 51</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; @start = pt</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 52</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; @end = pt</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160;&#0160; 53</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; @sol = self.Entity</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 54</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 55</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; return ed.Drag(self)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 56</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 57</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 58</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; # The sampler function</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 59</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160;&#0160; 60</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; def sampler(prompts)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 61</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 62</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; # Set up our selection options</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 63</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 64</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; jo = Aei::JigPromptPointOptions.new</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 65</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; jo.UserInputControls = (</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 66</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; Aei::UserInputControls.Accept3dCoordinates |</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 67</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; Aei::UserInputControls.NoZeroResponseAccepted |</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 68</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; Aei::UserInputControls.NoNegativeResponseAccepted)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 69</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; jo.Message = &quot;\nSelect end point: &quot; &#0160; </span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 70</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 71</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; # Get the end point of our box</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 72</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 73</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; res = prompts.AcquirePoint(jo)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 74</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 75</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; if @end == res.Value</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 76</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; return Aei::SamplerStatus.NoChange</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 77</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; else</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 78</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; @end = res.Value</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 79</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 80</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 81</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; return Aei::SamplerStatus.OK</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 82</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 83</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 84</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; # The update function</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 85</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160;&#0160; 86</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; def update()</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 87</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 88</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; # Recreate our Solid3d box</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 89</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 90</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; begin</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 91</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 92</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; # Get the width (x) and depth (y)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 93</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 94</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; x = @end.X - @start.X</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 95</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; y = @end.Y - @start.Y</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 96</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 97</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; # We need a non-zero Z value, so we copy Y</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 98</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160;&#0160; 99</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; z = y</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 100</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 101</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; # Create our box and move it to the right place</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 102</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 103</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; if x.abs &gt; 0 and y.abs &gt; 0</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 104</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160;&#0160;&#0160; @sol.CreateBox(x.abs,y.abs,z.abs) &#0160;&#0160;&#0160; </span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 105</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160;&#0160;&#0160; @sol.TransformBy(</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 106</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; Ag::Matrix3d.Displacement(</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 107</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160;&#0160;&#0160; Ag::Vector3d.new(</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 108</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; @start.X + x/2,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 109</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; @start.Y + y/2,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 110</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; @start.Z + z/2)))</span></p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 111</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 112</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; rescue</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 113</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; return false</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 114</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; end&#0160; </span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 115</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; return true</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 116</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 117</span>&#0160;<span style="LINE-HEIGHT: 140%">end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 118</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 119</span>&#0160;<span style="LINE-HEIGHT: 140%"># Create a box using a jig</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 120</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 121</span>&#0160;<span style="LINE-HEIGHT: 140%">def boxjig</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 122</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 123</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; app = Aas::Application</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 124</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; doc = app.DocumentManager.MdiActiveDocument</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 125</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; db = doc.Database</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 126</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; ed = doc.Editor</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 127</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 128</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; # Select the start point before entering the jig</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 129</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 130</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; ppr = ed.GetPoint(&quot;\nSelect start point: &quot;)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 131</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 132</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; if ppr.Status == Aei::PromptStatus.OK</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 133</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 134</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; # We&#39;ll add our solid to the modelspace</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 135</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 136</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; tr = doc.TransactionManager.StartTransaction</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 137</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; bt =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 138</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; tr.GetObject(</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 139</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160;&#0160;&#0160; db.BlockTableId,</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 140</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160;&#0160;&#0160; Ads::OpenMode.ForRead)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 141</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; btr =</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 142</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; tr.GetObject(db.CurrentSpaceId,Ads::OpenMode.ForWrite)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 143</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 144</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; # Make sure we&#39;re recording history to allow grip editing</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 145</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 146</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; sol = Ads::Solid3d.new</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 147</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; sol.RecordHistory = true</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 148</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 149</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; # Now we add our solid</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 150</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 151</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; btr.AppendEntity(sol)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 152</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(sol, true)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 153</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 154</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; # And call the jig before finishing</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 155</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 156</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; begin</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 157</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 158</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; sj = SolidJig.new sol</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 159</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: red">&#0160; 160</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; ppr2 = sj.start_jig(ed, ppr.Value)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 161</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 162</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; # Only commit if all completed well</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 163</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 164</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; if ppr2.Status == Aei::PromptStatus.OK</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 165</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160;&#0160;&#0160; tr.Commit</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 166</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 167</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 168</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; rescue</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 169</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 170</span>&#0160;<span style="LINE-HEIGHT: 140%"> &#0160; print_message(&quot;\nProblem found: &quot; + $! + &quot;\n&quot;)</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 171</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 172</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 173</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 174</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; tr.Dispose</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 175</span>&#0160;<span style="LINE-HEIGHT: 140%">&#0160; end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 176</span>&#0160;<span style="LINE-HEIGHT: 140%">end</span></p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 177</span>&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: #2b91af">&#0160; 178</span>&#0160;<span style="LINE-HEIGHT: 140%">add_commands [&quot;boxjig&quot;]</span></p></div>
<p>Let’s look at the specific changes:</p>
<ul>
<li>Lines 1-2 just removes the paths, to make the code more portable. I&#39;ve also removed the load of acmgdinternal.dll, as this is no longer a separate assembly with AutoCAD 2010. </li>
<li>Lines 39 and 41 turn my prior initialization attempt into a constructor. Line 53 sets our &quot;@sol&quot; member to be the entity passed into the constructor, although this code has to be in start_jig to work properly, for some reason – it can’t be in the constructor itself. </li>
<li>Lines 47, 60, 86 &amp; 160 are make the code more Rubyesque (as mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2009/05/overruling-autocad-2010s-entity-display-and-explode-using-ironruby.html">my last IronRuby post</a>, Ruby functions should&#0160; really be named with the lowercase_and_delimited convention). </li>
<li>Lines 103-4 and 111 allow a box to be created even when the cursor crosses the start point (either to its left or below it, which would previously have thrown an exception as we create a box with zero/negative height/width/depth). </li>
</ul>
<p>To load the .rb file, I simply used the RBLOAD command implemented by the C# loader application shown in previous IronRuby posts.</p>
<p>Now let’s run the BOXJIG command. After selecting a start point, we drag off to the top-right and see our box changing size (displayed using the 3D hidden visual style, in this case):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20120a573b860970b-pi"><img alt="Start of drag" border="0" height="317" src="/assets/image_316004.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Start of drag" width="485" /></a> </p>
<p>As we drag to the bottom left of our original point and select a point, we see our box created in that direction:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20120a573b86b970b-pi"><img alt="End of drag" border="0" height="317" src="/assets/image_979055.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="End of drag" width="485" /></a></p>
<p>In my next post I plan on taking the lid off some of David’s AutoCAD helper classes, as well as showing some of IronPython’s debugging capabilities when integrated with Visual Studio 2008.</p>
