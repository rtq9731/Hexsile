using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfoScript : MonoBehaviour
{
    [SerializeField] Image tileIcon = null;
    [SerializeField] Text ownerText = null;
    [SerializeField] Text groundTypeText = null;
    [SerializeField] Text InfoText = null;
    [SerializeField] Button btnBuy = null;

    public static List<TileScript> tiles = new List<TileScript>();

    public TileScript selectedTile = null;

    public static void TurnOnTileInfoPanel(TileScript tile)
    {
        if (MainSceneManager.Instance.InfoPanel != null)
        {
            MainSceneManager.Instance.InfoPanel.TurnOnMe(tile);
        }
    }

    public void RefreshTexts(TileScript tile)
    {
        string ownerName = tile.Owner != null ? tile.Owner.MyName : "None";
        ownerText.text = $"소유주 : {ownerName}";
        groundTypeText.text = $"지형 : {tile.Data.type}";

        string info;
        switch (tile.Data.type)
        {
            case TileType.Ocean:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/WaterIcon");
                btnBuy.interactable = false;
                info = "그냥 물입니다.\n경치가 참 좋네요.\n물고기도 잡힌다죠?";
                break;
            case TileType.Lake:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/WaterIcon");
                btnBuy.interactable = false;
                info = "그냥 연못입니다.\n경치가 참 좋네요.\n앉아서 쉬고싶네요.";
                break;
            case TileType.Forest:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/ForestIcon");
                btnBuy.interactable = true;
                info = "여러 나무들로 둘러싸인 지형입니다.\n방어력 + 10";
                break;
            case TileType.DigSite:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/MineIcon");
                btnBuy.interactable = true;
                info = "광산이 위치한 타일입니다.\n자원 + 1";
                break;
            case TileType.Plain:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/FieldIcon");
                btnBuy.interactable = true;
                info = "별다른 특징이 없는 평지 타일입니다.\n특이사항 없음";
                break;
            case TileType.Mountain:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/MountainIcon");
                btnBuy.interactable = true;
                info = "산위에 올라갈 수 있는 언덕 타일입니다.\n시야 + 1\n자원 - 1";
                break;
            default:
                info = "어.. 이 지형은 있으면 안되는데?";
                break;
        }
        InfoText.text = info;
    }

    private void TurnOnMe(TileScript tile)
    {
        if (selectedTile != null)
        selectedTile.RemoveSelect(MainSceneManager.Instance.tileVcam);

        FindObjectOfType<CameraMove>().enabled = false;
        selectedTile = tile;
        selectedTile.SelectTile(MainSceneManager.Instance.tileVcam);

        string ownerName = tile.Owner != null ? tile.Owner.MyName : "None";
        ownerText.text = $"소유주 : {ownerName}";
        groundTypeText.text = $"지형 : {tile.Data.type}";

        string info;
        switch (tile.Data.type)
        {
            case TileType.Ocean:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/WaterIcon");
                btnBuy.interactable = false;
                info = "그냥 물입니다.\n경치가 참 좋네요.\n물고기도 잡힌다죠?";
                break;
            case TileType.Lake:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/WaterIcon");
                btnBuy.interactable = false;
                info = "그냥 연못입니다.\n경치가 참 좋네요.\n앉아서 쉬고싶네요.";
                break;
            case TileType.Forest:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/ForestIcon");
                btnBuy.interactable = true;
                info = "여러 나무들로 둘러싸인 지형입니다.\n방어력 + 10";
                break;
            case TileType.DigSite:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/MineIcon");
                btnBuy.interactable = true;
                info = "광산이 위치한 타일입니다.\n자원 + 1";
                break;
            case TileType.Plain:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/FieldIcon");
                btnBuy.interactable = true;
                info = "별다른 특징이 없는 평지 타일입니다.\n특이사항 없음";
                break;
            case TileType.Mountain:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/MountainIcon");
                btnBuy.interactable = true;
                info = "산위에 올라갈 수 있는 언덕 타일입니다.\n시야 + 1\n자원 - 1";
                break;
            default:
                info = "어.. 이 지형은 있으면 안되는데?";
                break;
        }

        InfoText.text = info;
        gameObject.SetActive(true);

        if (MainSceneManager.Instance.tileChecker.FindTilesInRange(tile, 1).Find(x => x.Owner == MainSceneManager.Instance.GetPlayer()) == null) // 만약 플레이어의 땅과 인접한 땅이 아니면 리턴
        {
            btnBuy.interactable = false;
            return;
        }
        else if(tile.Owner != null)
        {
            btnBuy.interactable = false;
            return;
        }

        btnBuy.onClick.RemoveAllListeners();
        btnBuy.onClick.AddListener(() => {
            tile.BuyTile(MainSceneManager.Instance.GetPlayer());
            btnBuy.onClick.RemoveAllListeners();
            });
    }


    private void OnDisable()
    {
        if (selectedTile != null)
        {
            FindObjectOfType<CameraMove>().enabled = true;
            selectedTile.RemoveSelect(MainSceneManager.Instance.tileVcam);
        }
    }
}
