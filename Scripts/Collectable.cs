using UnityEngine;

public class Collectable : Collidable
{
    protected override void OnCollde(Collider2D coll)
    {
        if (coll.name == "Player")
            OnCollect();
    }

    protected virtual void OnCollect()
    {
        
    }
}
