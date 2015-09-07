using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapExporter))]
public class MapExporterEditor : Editor {

	const string PATH_MAP_EXPORT = "Assets/Resources/Map/";

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
		//MapDataSO mapDataSO = new MapDataSO ();
		MapDataSO mapDataSO = GetOrCreateMapDataSO ();
		mapDataSO.ClearData ();
		MapExporter mapExporter = (MapExporter)target;
		BaseExporter[] baseExporter = mapExporter.gameObject.GetComponentsInChildren<BaseExporter> ();

		for (int i = 0; i < baseExporter.Length; i++) {
			HandleExporter(mapDataSO, baseExporter[i]);

		}

		//AssetDatabase.AddObjectToAsset (mapDataSO, PATH_MAP_EXPORT);
		EditorUtility.SetDirty(mapDataSO);
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();

	}

	void HandleExporter (MapDataSO aMapDataSO, BaseExporter aBaseExporter)
	{
		if (aBaseExporter is GroundExporter) {
			GroundExporter typedExporter = (GroundExporter)aBaseExporter;
			aMapDataSO.m_GroundDataInfo.Add (typedExporter.ToGroundDataInfo ());
		} else if (aBaseExporter is CameraLimitExporter) {
			CameraLimitExporter typedExporter = (CameraLimitExporter)aBaseExporter;
			aMapDataSO.m_CameraLimitDataInfo.Add (typedExporter.m_CameraLimitDataInfo);
		} else if (aBaseExporter is ColliderExporter) {
			ColliderExporter typedExporter = (ColliderExporter)aBaseExporter;
			aMapDataSO.m_ColliderDataInfo.Add (typedExporter.ToColliderDataInfo());
		} else {
			Debug.LogError("UnHandled Type of Exporter :: Name =" +aBaseExporter.name + " :: TypeOf =" + aBaseExporter.GetType());
		}
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
		MapDataSO mapDataSO = (MapDataSO)AssetDatabase.LoadAssetAtPath (PATH_MAP_EXPORT + mapExporter.m_MapName, typeof(MapDataSO));

		if(mapDataSO == null){
			mapDataSO = ScriptableObject.CreateInstance<MapDataSO> ();
			
			AssetDatabase.CreateAsset (mapDataSO, PATH_MAP_EXPORT + mapExporter.m_MapName + ".asset");
			
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh();
		}

		return mapDataSO;
	}
}
