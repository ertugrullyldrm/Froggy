using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroyObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyObjWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DestroyObjWithDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
