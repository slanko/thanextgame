using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class firstScript : MonoBehaviour
{
    Animator anim;
    public RectTransform crossHair;
    private Wiimote wiimote;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InitWiimotes();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("cover", true);
        }
        else
        {
            anim.SetBool("cover", false);
        }
        //wiimote stuff
            float[] wiimoteVectors = wiimote.Ir.GetPointingPosition();
            crossHair.anchorMin = new Vector2(wiimoteVectors[0], wiimoteVectors[1]);
            crossHair.anchorMax = new Vector2(wiimoteVectors[0], wiimoteVectors[1]);

    }

    void InitWiimotes()
    {
        WiimoteManager.FindWiimotes(); // Poll native bluetooth drivers to find Wiimotes
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            remote.SendPlayerLED(true, false, false, false);
            remote.SetupIRCamera(IRDataType.BASIC);     // Basic IR dot position data
        }
    }
    void FinishedWithWiimotes()
    {
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            WiimoteManager.Cleanup(remote);
        }
    }

}
