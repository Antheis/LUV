using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleSmiles : MonoBehaviour
{
    public List<Sprite> Smiles;
    public GameObject smile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSmile(int idx)
    {
        smile.GetComponent<SpriteRenderer>().sprite = Smiles[(int)(idx * 2)];
    }

    public void SetSmileOnClick(int idx)
    {
        smile.GetComponent<SpriteRenderer>().sprite = Smiles[(int)(idx * 2) + 1];
        StartCoroutine(ResetSmile(idx));
    }

    IEnumerator ResetSmile(int idx)
    {
        yield return new WaitForSeconds(0.3f);
        SetSmile(idx);
    }
}
