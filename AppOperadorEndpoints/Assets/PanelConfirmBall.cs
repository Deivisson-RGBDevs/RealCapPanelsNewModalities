using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PanelConfirmBall : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtNumber;
    [SerializeField] private Color colorConfirm;
    [SerializeField] private Color colorRevoke;

    private string titleConfirm = "Clique no botão confirmar para validar o número!";
    private string titleRevoke = "Clique no botão confirmar para estornar o número!";

    [SerializeField] private GameObject bgBlack;
    [Space]
    [Header("<<<<<<<<<TESTE>>>>>>>>>>")]
    [SerializeField] private bool isConfirmPanel = false;

    private void Start()
    {
        ShowPanelRevoke(10);
    }
    public void ShowPanelConfirm(int _number)
    {
        ShowBgBlack();

        txtTitle.text = titleConfirm;
        txtNumber.text = _number.ToString("00");

        txtTitle.color = colorConfirm;
        txtNumber.color = colorConfirm;
    }

    public void ShowPanelRevoke(int _number)
    {
        ShowBgBlack();

        txtTitle.text = titleRevoke;
        txtNumber.text = _number.ToString("00");

        txtTitle.color = colorRevoke;
        txtNumber.color = colorRevoke;
    }

    public void HideBgBlack()
    {
        bgBlack.SetActive(false);
    }

    private void ShowBgBlack()
    {
        bgBlack.SetActive(true);
    }

    public void ConfirmBall()
    {
        //Chamar endpoint de sorteio do globo passando a array de bolas que já foram sorteadas com a inclusão da nova bola.
        HideBgBlack();
    }
    private void Update()
    {
        if(isConfirmPanel)
            ShowPanelConfirm(10);
        else
            ShowPanelRevoke(10);
    }
}
