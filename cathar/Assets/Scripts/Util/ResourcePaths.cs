using UnityEngine;
using System.Collections;

public class ResourcePaths{
	
	private const string PATH_MAP_EXPORT = "Assets/Resources/Map/";
	private const string PATH_MAP_LOAD = "Map/";

	static public string GetMapPathFromAssets(string aMapName)
	{
		return PATH_MAP_EXPORT + aMapName;
	}
	
	static public string GetMapPath(string aMapName)
	{
		return PATH_MAP_LOAD + aMapName;
	}
}
