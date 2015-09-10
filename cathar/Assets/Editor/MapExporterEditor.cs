using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapExporter))]
public class MapExporterEditor : Editor {


	public override void OnInspectorGUI ()
	{

		DrawDefaultInspector();

		if (GUILayout.Button ("Export Map")) {
			ExportMap();
		}

		if (GUILayout.Button ("Snap Ground")) {
			SnapGround();
		}
	}

	void ExportMap ()
	{
		MapExporter mapExporter = (MapExporter)target;

		if (string.IsNullOrEmpty (mapExporter.m_MapName)) {
			Debug.LogError("Cannot save a map without Name");
			return;
		}
		MapDataSO mapDataSO = GetOrCreateMapDataSO ();

		mapDataSO.m_MapDataInfo = mapExporter.CreateMapDataInfo ();

		//AssetDatabase.AddObjectToAsset (mapDataSO, PATH_MAP_EXPORT);
		EditorUtility.SetDirty(mapDataSO);
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();

	}

	void SnapGround ()
	{
		MapExporter mapExporter = (MapExporter)target;
		GroundExporter[] groundExporter = mapExporter.gameObject.GetComponentsInChildren<GroundExporter> ();
		for (int i = 0; i < groundExporter.Length; i++) {
			groundExporter[i].SnapGround();
		}
	}

	MapDataSO GetOrCreateMapDataSO()
	{
		MapExporter mapExporter = (MapExporter)target;
		MapDataSO mapDataSO = (MapDataSO)AssetDatabase.LoadAssetAtPath (ResourcePaths.GetMapPathFromAssets(mapExporter.m_MapName), typeof(MapDataSO));

		if(mapDataSO == null){
			mapDataSO = ScriptableObject.CreateInstance<MapDataSO> ();
			
			AssetDatabase.CreateAsset (mapDataSO, ResourcePaths.GetMapPathFromAssets(mapExporter.m_MapName) + ".asset");
			
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh();
		}

		return mapDataSO;
	}
}
