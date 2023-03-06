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

    private string titleConfirm = "Clique no bot�o confirmar para validar o n�mero!";
    private string titleRevoke = "Clique no bot�o confirmar para estornar o n�mero!";

    [SerializeField] private GameObject bgBlack;
    [Space]
    [Header("<<<<<<<<<TESTE>>>>>>>>>>")]
    [SerializeField] private bool isConfirmPanel = false;

    private void OnEnable()
    {
        SelectBall.OnSelectedNumber += ShowPanel;
    }
    private void OnDisable()
    {
        SelectBall.OnSelectedNumber -= ShowPanel;
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

    private void ShowPanel(bool _revoked,int _number)
    {
        if (_revoked)
            ShowPanelRevoke(_number);
        else
            ShowPanelConfirm(_number);
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
        //Chamar endpoint de sorteio do globo passando a array de bolas que j� foram sorteadas com a inclus�o da nova bola.
        HideBgBlack();
    }
}
