using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobeRaffleSettings", menuName = "Settings/GlobeRaffleSettings")]
public class GlobeDrawScriptable : ScriptableObject
{
    public List<int> bolasSorteadas;
    public int porDuasBolas;
    public List<porUmaBola> porUmaBolas;
    public float valorPremio;
    [Header("Ticket Winner")]
    public TicketInfos[] ganhadorContemplado;
    public bool[] ticketListVisible;


    public void SetNewBall(int ball)
    {
        bolasSorteadas.Add(ball);
    }

    public void RevokeBall(int ball)
    {
        bolasSorteadas.Remove(ball);
    }

    public void ResetInfos()
    {
        bolasSorteadas.Clear();
        porUmaBolas.Clear();
        ganhadorContemplado = new TicketInfos[0];
        ticketListVisible = new bool[0];
        valorPremio = 0;
        porDuasBolas = 0;
    }
    [System.Serializable]
    public class porUmaBola
    {
        public string numeroTitulo;
        public string numeroChance;
        public string numeroBola;
    }
}
