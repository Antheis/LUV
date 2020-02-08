using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerInfo : MonoBehaviour
{
    bool m_upgrade = false;
    int m_idx = 0;

    public void setInfo(bool isUpgrade, int idx)
    {
        m_upgrade = isUpgrade;
        m_idx = idx;
    }
}
