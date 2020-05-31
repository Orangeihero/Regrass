using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    int lockCount = 0;
    // Start is called before the first frame update
    public void openLock()
    {
        lockCount--;
        if(lockCount == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void addLock()
    {
        lockCount++;
    }
}
