using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSpinDraw : MonoBehaviour
{
    [SerializeField] private List<SpinDraw> spinsDraw;
    [SerializeField] private SpinDraw spinDraw;
    [SerializeField] private GameObject content;

    public void PopulateListSpinDraw(int _amountSpins)
    {
        spinsDraw.Clear();

        for(int i = 0; i < _amountSpins; i++)
        {
            SpinDraw inst = Instantiate(spinDraw, transform.position, Quaternion.identity);
            inst.transform.SetParent(content.transform);
            inst.SetActive(false);
            inst.PopulateTitle(i+1);
            spinsDraw.Add(inst);
        }
    }
    public void ActiveCurrentDraw(int _index)
    {
        spinsDraw[_index].SetActive(true);
    }

    private void ResetGrid()
    {
        for (int i = 0; i < spinsDraw.Count; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject, 0.1f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
