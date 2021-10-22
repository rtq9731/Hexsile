using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITopExtension : MonoBehaviour
{
    [Header("���� �ؾ��ϴ� ��ư")]
    [SerializeField] Button btnFireMissile;
    [SerializeField] Button btnReserchMissile;
    [SerializeField] Button btnMakeMissile;

    [Header("���� �Ǵ� �г�")]
    [SerializeField] GameObject panelFireMissile;
    [SerializeField] GameObject panelReserchMissile;
    [SerializeField] GameObject panelMakeMissile;

    private void Awake()
    {
        btnFireMissile.onClick.AddListener(() => panelFireMissile.SetActive(true));
        btnReserchMissile.onClick.AddListener(() => panelReserchMissile.SetActive(true));
        btnMakeMissile.onClick.AddListener(() => panelMakeMissile.SetActive(true));
    }
}