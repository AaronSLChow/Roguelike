using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic_Script : MonoBehaviour
{

    [SerializeField] private Text ammoText;
    [SerializeField] private RawImage rect1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayAmmo(int ammo, int maxAmmo)
    {
        ammoText.text = ammo.ToString() + "/" + maxAmmo.ToString();
    }

    public void showReload()
    {
        //-11.6 to -98.3 over reload time
        var xPos = rect1.transform.position.x;
        xPos -= 10;
    }
}
