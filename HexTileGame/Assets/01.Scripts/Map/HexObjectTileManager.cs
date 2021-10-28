using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexObjectTileManager : LoadingObj
{

    [SerializeField] GameObject[] jungleGroundObjSet;
    [SerializeField] GameObject[] plainGroundObjSet;
    [SerializeField] GameObject[] MountainGroundObjSet;

    GameObject[] objects;

    private void Awake()
    {
        start = x => { };
        finish = x => { };
    }

    public void GenerateObjects(int size, HexTilemapGenerator.GroundType groundType)
    {

        switch (groundType)
        {   
            case HexTilemapGenerator.GroundType.Jungle:
                objects = jungleGroundObjSet;
                break;
            case HexTilemapGenerator.GroundType.Plain:
                objects = plainGroundObjSet;
                break;
            case HexTilemapGenerator.GroundType.Mountain:   
                objects = MountainGroundObjSet;
                break;
            default:
                break;
        }

        List<TileScript> tiles = MainSceneManager.Instance.tileChecker.FindTilesInRange(null, size);

        for (int i = 0; i < tiles.Count; i++)
        {
            TileScript curTile = tiles[i];
            MainSceneManager.Instance.fogOfWarManager.GenerateCloudOnTile(curTile);

            GameObject randObj = objects[UnityEngine.Random.Range(1, objects.Length)];

            if (randObj != null)
            {
                if (curTile.Data.type == TileType.Plain) // 오브젝트 배치 불가능한 지형인지 체크
                {
                    GameObject temp = Instantiate(randObj, curTile.transform);

                    switch (temp.GetComponent<ObjScript>().objType)
                    {
                        case ObjType.Tree:
                            curTile.Data.SetDataToForest();
                            break;
                        case ObjType.Mountain:
                            curTile.Data.SetDataToMountain();
                            break;
                        case ObjType.Rock:
                            curTile.Data.SetDataToRock();
                            break;
                        default:
                            break;
                    }
                }

            }
        }

        MainSceneManager.Instance.StartGame();
    }
}
