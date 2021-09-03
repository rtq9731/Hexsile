using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] LayerMask whatIsTile;
    [SerializeField] SimpleTileInfoPanel panel;
    [SerializeField] GameObject GameExitPanel;

    RaycastHit hit;

    bool isSimplePanelOn = false;
    bool isUIOn = false;

    TileData lastTileData;
    TileData nowData;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!UIStackManager.RemoveUIOnTop())
            {
                GameExitPanel.SetActive(true);
            }
        }

        if(UIStackManager.IsUIStackEmpty()) // �ٸ� UI�� �ƹ��͵� �ö����� ���� ��.
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whatIsTile))
                {
                    TileInfoScript.TurnOnTileInfoPanel(hit.transform.GetComponent<TileScript>());
                }
            }

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whatIsTile))
            {
                nowData = hit.transform.GetComponent<TileScript>().Data;
                if (nowData != lastTileData)
                {
                    isSimplePanelOn = false;
                    panel.RemoveSimpleTileInfoPanel();
                    lastTileData = nowData;
                }
                else
                {
                    if (isSimplePanelOn)
                        return;

                    isSimplePanelOn = true;
                    StartCoroutine(GetNextData());
                }
            }
        }
        else
        {
            isSimplePanelOn = false;
            panel.RemoveSimpleTileInfoPanel();
        }    
    }

    IEnumerator GetNextData()
    {
        yield return new WaitForSeconds(0.5f);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whatIsTile))
        {
            lastTileData = hit.transform.GetComponent<TileScript>().Data;
            if (lastTileData == nowData)
            {
                panel.CallSimpleTileInfoPanel(hit.transform.GetComponent<TileScript>());
            }
        }
    }

}