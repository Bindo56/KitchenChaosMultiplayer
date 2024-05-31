using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateVisualCounter : MonoBehaviour
{
    [SerializeField] PlateCounter plateCounter;
    [SerializeField] Transform counterTopPoint;
    [SerializeField] Transform plateVisualPrefab;

    List<GameObject> plateVisualObjectList;

    private void Awake()
    {
        plateVisualObjectList = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnPlateSpwaned += PlateCounter_OnPlateSpwaned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisualObjectList[plateVisualObjectList.Count - 1];
        plateVisualObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnPlateSpwaned(object sender, System.EventArgs e)
    {
        Transform PlateVisualTransform = Instantiate(plateVisualPrefab,counterTopPoint);
        float plateOffSetY = 0.1f;

        PlateVisualTransform.localPosition = new Vector3(0, plateOffSetY * plateVisualObjectList.Count, 0);

        plateVisualObjectList.Add(PlateVisualTransform.gameObject);
    }
}
