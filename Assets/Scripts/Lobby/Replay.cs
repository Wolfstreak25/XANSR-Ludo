using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour
{
    public void ReplayLevel()
    {
        LobbyManager.Instance.ChangeScene(1);
    }
}
