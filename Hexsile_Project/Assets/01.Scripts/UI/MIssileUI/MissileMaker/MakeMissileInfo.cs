using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeMissileInfo : MonoBehaviour
{
    [SerializeField] Text textWarhead;
    [SerializeField] Text textEngine;
    [SerializeField] Text textBody;
    [SerializeField] Text textMissileATK;
    [SerializeField] Text textMissileRange;
    [SerializeField] Text textMissileTurn;
    [SerializeField] Text textMissilePrice;

    public void InitPanelMissileInfo(MissileData data)
    {
        textWarhead.text = $"장착된 탄두 : {MainSceneManager.Instance.GetWarheadData(data.WarheadType).Name}";
        textEngine.text = $"장착된 엔진 : {MainSceneManager.Instance.GetEngineData(data.EngineTier).Name}";
        textBody.text = $"장착된 선체 : {MainSceneManager.Instance.GetMissileBodyData(data.BodyType).Name}";
        textMissileATK.text = $"미사일 공격력 : {MainSceneManager.Instance.GetWarheadData(data.WarheadType).Atk}";
        textMissileRange.text = "미사일 사거리 : " + (data.CanMakeIt() ? data.MissileRange.ToString() : "무게 한도 초과!");
        textMissileTurn.text = $"미사일 생산에 필요한 턴  : {data.TurnForMissileReady}";
        textMissilePrice.text = $"미사일 생산에 필요한 자원 : {data.MissilePrice}";
    }
}
