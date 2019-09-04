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
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            WiimoteManager.Cleanup(remote);
        }
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("next");
        }
        //wiimote stuff
        float[,] ir = wiimote.Ir.GetProbableSensorBarIR();
        for (int i = 0; i < 2; i++)
        {
            float x = (float)ir[i, 0] / Screen.width - 1;
            float y = (float)ir[i, 1] / Screen.height - 1;
        }
            if (wiimote != null)
        {
            float[] wiimoteVectors = wiimote.Ir.GetPointingPosition();
            crossHair.anchorMin = new Vector2(wiimoteVectors[0], wiimoteVectors[1]);
            crossHair.anchorMax = new Vector2(wiimoteVectors[0], wiimoteVectors[1]);
        }


    }

    void InitWiimotes()
    {
        WiimoteManager.FindWiimotes(); // Poll native bluetooth drivers to find Wiimotes
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            remote.SendPlayerLED(true, false, false, true);
            remote.SetupIRCamera(IRDataType.BASIC);     // Basic IR dot position data
            wiimote = remote;
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
