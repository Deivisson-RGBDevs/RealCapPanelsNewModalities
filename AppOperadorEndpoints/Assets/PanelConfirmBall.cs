using System;
using UnityEngine;
using TMPro;
public class PanelConfirmBall : MonoBehaviour
{
    public static event Action<bool, int> OnConfirmSelection;

    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtNumber;
    [SerializeField] private Color colorConfirm;
    [SerializeField] private Color colorRevoke;

    private string titleConfirm = "Clique no botão confirmar para validar o número!";
    private string titleRevoke = "Clique no botão confirmar para estornar o número!";

    [SerializeField] private GameObject bgBlack;
    [Space]
    [SerializeField] private int selectedNumber;
    [SerializeField] private bool isRevoked;

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

    private void ShowPanel(bool _isDrawn, int _number)
    {
        if (_isDrawn)
            ShowPanelRevoke(_number);
        else
            ShowPanelConfirm(_number);

        selectedNumber = _number;
        isRevoked = _isDrawn;
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

        OnConfirmSelection?.Invoke(isRevoked, selectedNumber);

        //Chamar endpoint de sorteio do globo passando a array de bolas que já foram sorteadas com a inclusão da nova bola.

        HideBgBlack();
    }
}
