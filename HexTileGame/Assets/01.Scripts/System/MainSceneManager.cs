using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance = null;

    [Header("데이터 관련")]
    [SerializeField] MissileWarhead missileWarhead;
    [SerializeField] MissileEngine missileEngine;
    [SerializeField] Body missileBody;
    [SerializeField] public TechTreeDatas techTreeDatas;

    public BodyData GetMissileBodyData(MissileTypes.MissileBody type)
    {
        return missileBody.dataList.Find(x => x.TYPE == type);
    }

    public BodyData GetMissileBodyByIdx(int idx)
    {
        return missileBody.dataArray[idx];
    }

    public MissileWarheadData GetWarheadData(MissileTypes.MissileWarheadType type)
    {
        return missileWarhead.dataList.Find(x => x.TYPE == type);
    }

    public MissileWarheadData GetWarheadByIdx(int idx)
    {
        return missileWarhead.dataArray[idx];
    }

    public MissileEngineData GetEngineData(MissileTypes.MissileEngineType type)
    {
        return missileEngine.dataList.Find(x => x.TYPE == type);
    }

    public MissileEngineData GetEngineDataByIdx(int idx)
    {
        return missileEngine.dataArray[idx];
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    [Header("외부 참조용")]
    [SerializeField] public TileInfoScript InfoPanel;
    [SerializeField] public MissileManager missileManager;
    [SerializeField] public FogOfWarManager fogOfWarManager;
    [SerializeField] public GameObject tileVcam;
    [SerializeField] public TileChecker tileChecker;
    [SerializeField] public UITopBar uiTopBar;
    [SerializeField] public MissileEffectPool effectPool;
    [SerializeField] public PanelResearchInput researchInputPanel;
    [SerializeField] public PanelCurrentResearch curResearchPanel;
    [SerializeField] public PanelMissileMaker missileMakerPanel;
    [SerializeField] public HexTilemapGenerator tileGenerator;

    [Header("About Tile")]
    public float TileZInterval = 0.875f;
    public float TileXInterval = 1f;
    public uint turnCnt = 0;
    public uint stageCount = 1;
    public int mapSize;

    public bool isRerolled = false;

    private HexTilemapGenerator tilemapGenerator;

    [Header("About Player")]
    public string PlayerName = "COCONUT";

    List<PlayerScript> players = new List<PlayerScript>();
    public List<PlayerScript> Players { get { return players; } set { players = value; } }

    PersonPlayer player = null;

    private void Start()
    {
        tilemapGenerator = FindObjectOfType<HexTilemapGenerator>();
    }

    public void SetPlayer(PersonPlayer player)
    {
        this.player = player;
    }

    public PersonPlayer GetPlayer()
    {
        return player;
    }

    public void StartGame()
    {
        isRerolled = false;
        FindObjectOfType<AIManager>().StartStage(mapSize);
        player.AddTile(TileMapData.Instance.GetTile(0)); // 무조건 중앙땅은 플레이어꺼
        player.TurnFinishAction += CheckStageClear;
    }

    public void ClearStage()
    {
        DOTween.CompleteAll();
        while (!UIStackManager.IsUIStackEmpty())
        {
            UIStackManager.RemoveUIOnTopWithNoTime();
        }

        fogOfWarManager.ResetCloudList();
        TileMapData.Instance.ResetTileList();

        AIManager.Instance.aiPlayers.ForEach(x => x.ResetPlayer());

        turnCnt = 0;
        player.ResetPlayer();
        tilemapGenerator.GenerateNewTile();
    }

    public void RerollStage(Button btnReroll)
    {
        if(isRerolled)
        {
            return;
        }

        isRerolled = true;
        btnReroll.gameObject.SetActive(false);

        DOTween.CompleteAll();
        while (!UIStackManager.IsUIStackEmpty())
        {
            UIStackManager.RemoveUIOnTopWithNoTime();
        }

        fogOfWarManager.ResetCloudList();
        TileMapData.Instance.ResetTileList();

        AIManager.Instance.aiPlayers.ForEach(x => x.ResetPlayer());

        turnCnt = 0;
        player.ResetPlayer();
        tilemapGenerator.GenerateNewTileWihtNoExtension();
    }

    private void CheckStageClear()
    {
        if(AIManager.Instance.aiPlayers.Find(x => x.OwningTiles.Count > 1) == null)
        {
            ClearStage();
        }
    }

    public void LoadGame()
    {

    }
}
