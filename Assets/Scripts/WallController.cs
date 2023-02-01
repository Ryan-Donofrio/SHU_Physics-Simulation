using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class WallController : MonoBehaviour
{
    private List<IHitable> wallBlocks = new List<IHitable>();

    private void Start()
    {
        foreach(IHitable obj in GetComponentsInChildren<IHitable>())
        {
            wallBlocks.Add(obj);
            obj.Start();
        }
    }

    public void ResetWallObjects()
    {
        if (wallBlocks.Count <= 0)
            return;

        foreach(IHitable block in wallBlocks)
        {
            block.ResetSelf();
        }
    }

    public void HideWallObjects()
    {
        if (wallBlocks.Count <= 0)
            return;

        foreach (IHitable block in wallBlocks)
        {
            block.HideSelf();
        }
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasReleasedThisFrame)
        {
            Debug.Log("Resetting projectile");
            ResetWallObjects();
        }
    }
}
