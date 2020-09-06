using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public PlayerControl player;

    private void OnMouseDown()
    {
        Debug.Log(this.gameObject.name);
        player.Pull();
    }

    private void OnMouseUp()
    {
        player.NotPulled();
    }
}
