using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretButtonManager : MonoBehaviour
{
    [SerializeField] TowerSO[] TowerList;
    [SerializeField] Button[] ButtonList;
    [SerializeField] GridBuildingSystem grid;

    private void Start() {
        for (var i = 0; i < ButtonList.Length; i++) {
            if (i < TowerList.Length) {
                string ButtonText = TowerList[i].objectName;
                ButtonList[i].GetComponentInChildren<TextMeshProUGUI>().text = ButtonText;
            } else {
                ButtonList[i].gameObject.SetActive(false);
            }
        }
        InitializeButtons();
    }

    private void InitializeButtons() {
        for(var i = 0; i < ButtonList.Length; i++) {
            Button button = ButtonList[i];
            int buttonIndex = i;
            button.onClick.AddListener(() => ChangeTurret(buttonIndex));
        }
    }

    private void ChangeTurret(int index) {
        Debug.Log(TowerList[index].name);
        grid.ChangeTurretType(TowerList[index].prefab.transform);
    }
}
