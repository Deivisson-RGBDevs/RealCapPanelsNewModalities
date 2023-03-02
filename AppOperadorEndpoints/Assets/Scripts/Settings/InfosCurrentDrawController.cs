using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InfosCurrentDrawController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtOrder;
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private TextMeshProUGUI txtvalue;
  
    public void PopulateInfosCurrentDraw(int _order,string _description, float _value)
    {
        txtOrder.text = $"Ordem: {_order}";
        txtDescription.text = $"Descrição: {_description}";
        txtvalue.text = $"Valor: {GameManager.instance.FormatMoneyInfo(_value)}";
    }
  
}
