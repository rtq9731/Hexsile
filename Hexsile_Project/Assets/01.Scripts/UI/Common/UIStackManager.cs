using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIStackManager
{
    static Stack<GameObject> UIStack = new Stack<GameObject>();

    public static void ClearUIStack()
    {
        SceneManager.sceneLoaded += (x, y) => UIStack = new Stack<GameObject>();
    }

    public static void AddUIToStack(GameObject item)
    {
        UIStack.Push(item);
    }

    public static bool IsUIStackEmpty()
    {
        return UIStack.Count <= 0 ? true : false;
    }

    public static GameObject GetTopUI()
    {
        if(UIStack.Count <= 0)
        {
            return null;
        }
        return UIStack.Peek();
    }

    public static void Clear()
    {
        while(!IsUIStackEmpty())
        {
            RemoveUIOnTop();
        }
    }

    public static bool RemoveUIOnTop()
    {
        if (!IsUIStackEmpty())
        {
            GameObject obj = UIStack.Pop();
            DOTween.Kill(obj);
            obj.transform.DOScale(0, 0.3f).OnComplete(() => obj.SetActive(false));
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void RemoveUIOnTopWithNoTime()
    {
        if (!IsUIStackEmpty())
        {
            GameObject obj = UIStack.Pop();
            DOTween.Kill(obj);
            obj.SetActive(false);
        }
    }

}
