using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

///
/// !!! Machine generated code !!!
///
/// A class which deriveds ScritableObject class so all its data 
/// can be serialized onto an asset data file.
/// 
[System.Serializable]
public class Body : ScriptableObject 
{	
    [HideInInspector] [SerializeField] 
    public string SheetName = "";
    
    [HideInInspector] [SerializeField] 
    public string WorksheetName = "";
    
    // Note: initialize in OnEnable() not here.
    public BodyData[] dataArray;
    public List<BodyData> dataList = new List<BodyData>();

    void OnEnable()
    {		
//#if UNITY_EDITOR
        //hideFlags = HideFlags.DontSave;
//#endif
        // Important:
        //    It should be checked an initialization of any collection data before it is initialized.
        //    Without this check, the array collection which already has its data get to be null 
        //    because OnEnable is called whenever Unity builds.
        // 		
        if (dataArray == null)
            dataArray = new BodyData[0];

        for (int i = 0; i < dataArray.Length; i++)
        {
            dataList.Add(dataArray[i]);
        }

        for (int i = 0; i < dataList.Count; i++)
        {
            dataList[i].mySprite = Resources.Load<Sprite>($"Icons/Body/{i}");
        }
    }
    
    //
    // Highly recommand to use LINQ to query the data sources.
    //

}
