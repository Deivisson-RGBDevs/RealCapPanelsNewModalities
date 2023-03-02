using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListTicketsController : MonoBehaviour
{
    [SerializeField] private List<BtTicketList> btTickets;
    [SerializeField] private BtTicketList btTicket;
    [Space]
    [SerializeField] private GameObject content;

    [SerializeField] List<string> testeInfos;
    void Start()
    {
        PopulateListTickets(testeInfos,true);
    }

    public void PopulateListTickets(List<string> _infos, bool _hasWinner)
    {
        btTickets.Clear();
        for (int i = 0; i < _infos.Count; i++)
        {
            BtTicketList inst = Instantiate(btTicket, transform.position, Quaternion.identity);
            inst.transform.SetParent(content.transform);
            inst.PopulateInfos(_infos[i]);
            if (_hasWinner)
            {
                inst.SetInteractableButton(true);
            }
            else
            {
                inst.SetInteractableButton(false);
            }

            btTickets.Add(inst);
        }
    }
    private void ResetGrid()
    {
        for (int i = 0; i < btTickets.Count; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject, 0.1f);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
