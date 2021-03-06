/* 
AutoBuilder.cs
Automatically changes the target platform and creates a build.
 
Installation
Place in an Editor folder.
 
Usage
Go to File > AutoBuilder and select a platform. These methods can also be run from the Unity command line using -executeMethod AutoBuilder.MethodName.
 
License
Copyright (C) 2011 by Thinksquirrel Software, LLC
 
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
 
The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
 
public static class AutoBuilder {
 
	static string GetProjectName()
	{
		string[] s = Application.dataPath.Split('/');
		return s[s.Length - 2];
	}
 
	static string[] GetScenePaths()
	{
		List<string> scenes = new List<string>();
 
		for(int i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
			if (EditorBuildSettings.scenes[i].enabled) 
			{
				scenes.Add (EditorBuildSettings.scenes[i].path);
			}
		}
 
		return scenes.ToArray();
	}
 
	[MenuItem("File/AutoBuilder/Windows/32-bit")]
	static void PerformWinBuild ()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
		BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Win/" + GetProjectName() + ".exe",BuildTarget.StandaloneWindows,BuildOptions.None);
	}
 
	[MenuItem("File/AutoBuilder/Windows/64-bit")]
	static void PerformWin64Build ()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
		BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Win64/" + GetProjectName() + ".exe",BuildTarget.StandaloneWindows64,BuildOptions.None);
	}
 
	[MenuItem("File/AutoBuilder/Mac OSX/Universal")]
	static void PerformOSXUniversalBuild ()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneOSXUniversal);
		BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/OSX-Universal/" + GetProjectName() + ".app",BuildTarget.StandaloneOSXUniversal,BuildOptions.None);
	}
 
	[MenuItem("File/AutoBuilder/Mac OSX/Intel")]
	static void PerformOSXIntelBuild ()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneOSXIntel);
		BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/OSX-Intel/" + GetProjectName() + ".app",BuildTarget.StandaloneOSXIntel,BuildOptions.None);
	}
 
	[MenuItem("File/AutoBuilder/iOS")]
	static void PerformiOSBuild ()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);
		BuildPipeline.BuildPlayer (GetScenePaths (), "Builds/iOS", BuildTarget.iOS, BuildOptions.None);
	}
	[MenuItem("File/AutoBuilder/Android")]
	static void PerformAndroidBuild ()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
		BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Android/debug_" + GetProjectName() + DateTime.Now.ToString("dd_MMM_yy_" + "HH_mm_t" ) +".apk",BuildTarget.Android,BuildOptions.None);
	}
	[MenuItem("File/AutoBuilder/Web/Standard")]
	static void PerformWebBuild ()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.WebPlayer);
		BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Web",BuildTarget.WebPlayer,BuildOptions.None);
	}
	[MenuItem("File/AutoBuilder/Web/Streamed")]
	static void PerformWebStreamedBuild ()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.WebPlayerStreamed);
		BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Web-Streamed",BuildTarget.WebPlayerStreamed,BuildOptions.None);
	}
}