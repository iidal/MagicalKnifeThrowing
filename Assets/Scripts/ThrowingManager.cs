using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingManager : MonoBehaviour
{

    public OrbManager orbManager;

    void Start()
    {

    }



    public void ThrowKnife(string icon)
    {
        int i = 0;
        foreach (SpellOrbController soc in orbManager.orbs)
        {
            if (soc.icon == icon)
            {
                orbManager.orbs.RemoveAt(i);
                soc.DestroyOrb();
                //Destroy(soc.gameObject);
                break;

            }
            i++;
        }


    }
}
