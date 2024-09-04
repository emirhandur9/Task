using UnityEngine;
using UnityEditor;
using EmirhanDur;
using System.Collections.Generic;

public class CityRoadGeneratorEditorWindow : EditorWindow
{
    private GameObject straightRoadPrefab;
 //   private GameObject tRoadPrefab;
   // private GameObject fourWayRoadPrefab;
    private GameObject curveRoadPrefab;
    private int numberOfRoads = 3;

    [MenuItem("Tools/City Road Generator")]
    public static void ShowWindow()
    {
        GetWindow<CityRoadGeneratorEditorWindow>("City Road Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("City Road Generator", EditorStyles.boldLabel);

        straightRoadPrefab = (GameObject)EditorGUILayout.ObjectField("Straight Road Prefab", straightRoadPrefab, typeof(GameObject), false);
        curveRoadPrefab = (GameObject)EditorGUILayout.ObjectField("Curve Road Prefab", curveRoadPrefab, typeof(GameObject), false);

        numberOfRoads = EditorGUILayout.IntField("Number of Roads", numberOfRoads);

        if (GUILayout.Button("Generate"))
        {
            GenerateRoadNetwork();
        }
    }

    private void GenerateRoadNetwork()
    {
        if (straightRoadPrefab == null  || curveRoadPrefab == null)
        {
            Debug.LogError("Please assign all road prefabs.");
            return;
        }

        // Mevcut objeleri temizle
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.name != "Main Camera" && obj.name != "Directional Light")
            {
                DestroyImmediate(obj);
            }
        }

        List<RoadController> roads = new List<RoadController>();

        RoadController firstRoad = Instantiate(straightRoadPrefab).GetComponent<RoadController>();
        firstRoad.transform.position = Vector3.zero;
        roads.Add(firstRoad);

        for (int i = 0; i < numberOfRoads; i++)
        {
            int straightOrCurve = Random.Range(0, 2);
            var beforeRoad = roads[i];
            if (straightOrCurve == 0)
            {

                RoadController basicRoad2 = Instantiate(straightRoadPrefab).GetComponent<RoadController>();

                basicRoad2.transform.rotation = beforeRoad.transform.rotation;
                basicRoad2.transform.position = beforeRoad.connectionPoint.position;
                basicRoad2.transform.position +=  beforeRoad.transform.forward;

                roads.Add(basicRoad2);
            }
            else
            {
                
                RoadController curvedRoad = Instantiate(curveRoadPrefab).GetComponent<RoadController>();

                var basicRoadConnectionPoint = beforeRoad.connectionPoint;
                curvedRoad.transform.position = basicRoadConnectionPoint.position;

                int randomRotate = Random.Range(0, 2);
                Quaternion rotation = Quaternion.identity;
                float yRot = -90;

                float zRot = 0;

                if(beforeRoad.connectionPoint.forward == Vector3.right)
                {
                    yRot = 0;
                }
                else if(beforeRoad.connectionPoint.forward == Vector3.forward)
                {
                    yRot = -90;
                }
                else if(beforeRoad.connectionPoint.forward == Vector3.left)
                {
                    yRot = -180;
                }
                else if(beforeRoad.connectionPoint.forward == Vector3.back)
                {
                    yRot = -270;
                }
                if (randomRotate == 0)
                {
                    rotation = Quaternion.Euler(new Vector3(0, yRot, zRot));
                }
                else
                {
                    rotation = Quaternion.Euler(new Vector3(-180, yRot, zRot));

                }


                curvedRoad.transform.rotation = rotation;

                roads.Add(curvedRoad);

                
            }
        }



        
    }


}
