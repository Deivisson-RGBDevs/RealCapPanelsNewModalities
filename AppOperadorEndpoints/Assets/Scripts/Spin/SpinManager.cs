using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinManager : MonoBehaviour
{
    [SerializeField] private InfosCurrentDrawController infosCurrentDraw;

    [SerializeField] private int indexDraw = 0;

    void Start()
    {
        infosCurrentDraw.PopulateInfosCurrentDraw(
            GameManager.instance.editionSettings.currentEdition.spinInfos[indexDraw].order,
            GameManager.instance.editionSettings.currentEdition.spinInfos[indexDraw].description,
            GameManager.instance.editionSettings.currentEdition.spinInfos[indexDraw].value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
