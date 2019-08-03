using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

/**
 * Build settings
 */ 
public class BuildConfiger
{
	/**
	 * Compress bundles
	 */ 
	public static bool Compress
	{
		get{return BMDataAccessor.BMConfiger.compress;}
		set
		{
			if(BMDataAccessor.BMConfiger.compress != value)
			{
				BMDataAccessor.BMConfiger.compress = value;
				BundleManager.UpdateAllBundleChangeTime();
			}
		}
	}
	
	/**
	 * Build deterministic Bundles
	 */ 
	public static bool DeterministicBundle
	{
		get{return BMDataAccessor.BMConfiger.deterministicBundle;}
		set
		{
			if(BMDataAccessor.BMConfiger.deterministicBundle != value)
			{
				BMDataAccessor.BMConfiger.deterministicBundle = value;
				BundleManager.UpdateAllBundleChangeTime();
			}
		}
	}
	
	/** 
	 * Target platform
	 */ 
	public static BuildPlatform BundleBuildTarget
	{
		get
		{
			return BMDataAccessor.Urls.bundleTarget;
		}
		set
		{
			BMDataAccessor.Urls.bundleTarget = value;
		}
	}

	/** 
	 * Target platform
	 */
	public static bool UseEditorTarget
	{
		get
		{
			return BMDataAccessor.Urls.useEditorTarget;
		}
		set
		{
			BMDataAccessor.Urls.useEditorTarget = value;
		}
	}
	
	/**
	 * Bundle file's suffix
	 */ 
	public static string BundleSuffix
	{
		get{return BMDataAccessor.BMConfiger.bundleSuffix;}
		set{BMDataAccessor.BMConfiger.bundleSuffix = value;}
	}
	 
	/**
	 * Current output string for target platform
	 */
	public static string BuildOutputStr
	{
		get
		{
			return BMDataAccessor.Urls.outputs[BMDataAccessor.Urls.bundleTarget.ToString()];
		}
		set
		{
			var urls = BMDataAccessor.Urls.outputs;
			string platformStr = BMDataAccessor.Urls.bundleTarget.ToString();
			string origValue = urls[platformStr];
			urls[platformStr] = value;
			if(origValue != value)
				BMDataAccessor.SaveUrls();
		}
	}
	 
	internal static string InterpretedOutputPath
	{
		get
		{
			return BMDataAccessor.Urls.GetInterpretedOutputPath(BMDataAccessor.Urls.bundleTarget);
		}
	}
	
	internal static BuildOptions BuildOptions
	{
		get
		{
			return BMDataAccessor.BMConfiger.compress ? 0 : BuildOptions.UncompressedAssetBundle;
		}
	}
	
	internal static BuildTarget UnityBuildTarget
	{
		get
		{
			if(BuildConfiger.UseEditorTarget)
				BuildConfiger.UnityBuildTarget = EditorUserBuildSettings.activeBuildTarget;

			switch(BundleBuildTarget)
			{
			case BuildPlatform.Standalones:
				if(Application.platform == RuntimePlatform.OSXEditor)
					return BuildTarget.StandaloneOSXIntel;
				else
					return BuildTarget.StandaloneWindows;
#if !UNITY_5_4_OR_NEWER
			case BuildPlatform.WebPlayer:
				return BuildTarget.WebPlayer;
#endif
			case BuildPlatform.IOS:
#if UNITY_5 || UNITY_2017_1_OR_NEWER
				return BuildTarget.iOS;
#else
				return BuildTarget.iPhone;
#endif
			case BuildPlatform.Android:
				return BuildTarget.Android;
#if !UNITY_4_0 && !UNITY_4_1 && !UNITY_5_4_OR_NEWER
        	case BuildPlatform.WP8:
				return BuildTarget.WP8Player;
#endif
#if UNITY_5 || UNITY_2017_1_OR_NEWER
			case BuildPlatform.WebGL:
				return BuildTarget.WebGL;
			case BuildPlatform.WinStoreApp:
				return BuildTarget.WSAPlayer;
#endif
			default:
				Debug.LogError("Internal error. Cannot find BuildTarget for " + BundleBuildTarget);
				return BuildTarget.StandaloneWindows;
			}
		}
		set
		{
			switch(value)
			{
#if !UNITY_5_4_OR_NEWER
			case BuildTarget.StandaloneGLESEmu:
#endif
			case BuildTarget.StandaloneLinux:
			case BuildTarget.StandaloneLinux64:
			case BuildTarget.StandaloneLinuxUniversal:
			case BuildTarget.StandaloneOSXIntel:
			case BuildTarget.StandaloneWindows:
			case BuildTarget.StandaloneWindows64:
				BundleBuildTarget = BuildPlatform.Standalones;
				break;
#if !UNITY_5_4_OR_NEWER
			case BuildTarget.WebPlayer:
			case BuildTarget.WebPlayerStreamed:
				BundleBuildTarget = BuildPlatform.WebPlayer;
				break;
#endif
#if UNITY_5 || UNITY_2017_1_OR_NEWER
			case BuildTarget.iOS:
#else
			case BuildTarget.iPhone:
#endif
				BundleBuildTarget = BuildPlatform.IOS;
				break;
			case BuildTarget.Android:
				BundleBuildTarget = BuildPlatform.Android;
				break;
#if !UNITY_4_0 && !UNITY_4_1 && !UNITY_5_4_OR_NEWER
        	case BuildTarget.WP8Player:
				BundleBuildTarget = BuildPlatform.WP8;
				break;
#endif
#if UNITY_5 || UNITY_2017_1_OR_NEWER
			case BuildTarget.WebGL:
				BundleBuildTarget = BuildPlatform.WebGL;
				break;
			case BuildTarget.WSAPlayer:
				BundleBuildTarget = BuildPlatform.WinStoreApp;
				break;
#endif
			default:
				Debug.LogError("Internal error. Bundle Manager dosn't support for platform " + value);
				BundleBuildTarget = BuildPlatform.Standalones;
				break;
			}
		}
	}
}