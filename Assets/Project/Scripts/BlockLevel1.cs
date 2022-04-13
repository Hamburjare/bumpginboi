using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLevel1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwapCamera.instance.BlockLevel();
        }

    }
}
