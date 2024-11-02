using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] List<BuyableLand> buyableLands = new List<BuyableLand>();
    [SerializeField] GameObject gate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buyableLands[0].isBuyableLand == false && buyableLands[1].isBuyableLand == false)
        {
            gate.SetActive(true);
        }
    }
}
