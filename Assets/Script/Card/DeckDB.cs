using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeckDB : MonoBehaviour
{
    const int MAXDeck = 30;

    int[] DeckInven = new int[MAXDeck];
    int[] handsCard = new int[MAXDeck];
    int endposi = 5;

    List<int> Hands;

    // Start is called before the first frame update
    void Start()
    {
        System.Array.Clear(DeckInven, 0, MAXDeck);
        StartPack();

    }
    // Update is called once per frame
    void Update()
    {
     
        
    }
    void StartPack()
    {
        DeckInven[0] = 1;
        DeckInven[1] = 1;
        DeckInven[2] = 1;
        DeckInven[3] = 1;
        DeckInven[4] = 1;
        endposi = 4;
    }
    void RemoveinDeckinven(int Deckivnum)
    {
        if (Deckivnum == endposi)
        {
            DeckInven[Deckivnum] = 0;
            endposi--;
        }
        else
        {
            DeckInven[Deckivnum] = DeckInven[endposi];
            endposi--;
        }

    }


}
