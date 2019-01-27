using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SortingOrderUpdater : MonoBehaviour
{
    [SerializeField]SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newOrder = transform.position.x*100;
        if(sr)
        sr.sortingOrder = (int)newOrder*100;
    }
}
