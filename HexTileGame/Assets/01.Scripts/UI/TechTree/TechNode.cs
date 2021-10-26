using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechNode : MonoBehaviour
{
    [SerializeField] int idx;
    [SerializeField] Sprite mySprite = null;
    [SerializeField] Button myBtn = null;

    SkillTreeNode data = null;
    PlayerScript player = null;

    private void Start()
    {
        myBtn.onClick.AddListener(OnClickCallPanelInput);
    }

    private void OnEnable()
    {
        if(player == null)
        {
            player = MainSceneManager.Instance.GetPlayer();
        }
        
        if(data == null)
        {
            data = MainSceneManager.Instance.techTreeDatas.GetDataByIdx(idx);
        }

        switch (data.Type) // 타입에 따라 플레이어가 사전 연구해야할걸 가지고 있는지 체크
        {
            case ResearchType.Warhead:
                foreach (var item in data.RequireResearches)
                {
                    if (!player.UnlockedWarheadIdx.Contains(item))
                    {
                        Debug.Log(data.RequireResearches + " " + item);
                        Debug.Log(player.UnlockedWarheadIdx.Contains(item));
                        myBtn.interactable = false;
                    }
                }
                break;
            case ResearchType.Engine:
                foreach (var item in data.RequireResearches)
                {
                    if (!player.UnlockedEngineIdx.Contains(item))
                    {
                        myBtn.interactable = false;
                    }
                }
                break;
            case ResearchType.Material:
                break;
            default:
                break;
        }
    }

    private void OnClickCallPanelInput()
    {
        MainSceneManager.Instance.researchInputPanel.InitPanelInput(data, ResearchStart);
    }

    private void ResearchStart()
    {
        MainSceneManager.Instance.GetPlayer().CurResearchData = data;
    }
}
