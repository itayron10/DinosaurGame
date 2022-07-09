using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{
    private BasicHealth health;

    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        health = GetComponent<BasicHealth>();
    }

    public override void Interacte()
    {
        base.Interacte();
        health.TakeDamage(12);
    }
}
