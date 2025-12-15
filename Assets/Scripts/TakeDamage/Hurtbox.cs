using UnityEngine;
using System.Collections.Generic;
public class Hurtbox : MonoBehaviour
{

    public float iFrameDuration = 0.1f;
    private float iFrameTimer;
    

    private HashSet<int> recentAttackIDs = new HashSet<int>();

    public bool CanTakeHit(int attackID)
    {
        if (iFrameTimer > 0f) return false;
        if (recentAttackIDs.Contains(attackID)) return false;
        

        recentAttackIDs.Add(attackID);
        return true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Must have a hurtbox
        if (!other.TryGetComponent(out Hitbox hitbox))
        {
            return;
        }
        
        if (hitbox.owner != null)
        {
            // Prevent hitting itself
            if (hitbox.owner == gameObject)
                return;

            // Prevent friendly fire
            if (hitbox.owner.CompareTag(gameObject.tag))
                return;
        }

        // Checks Immunity frames
        if (!CanTakeHit(hitbox.attackID))
        {
            return;
        }
        
      
            
        
        // Must also have a damageable body
        if (TryGetComponent<IDamageable>(out var dmg))
        {
            Debug.Log($"{gameObject.name} im hit by {hitbox.owner}");
            dmg.TakeDamage(hitbox.damage, hitbox.owner);
            TriggerIFrames(); // optional
            if (other.TryGetComponent<IDestroyOnImpact>(out var hit))
            {
                hit.DestroyMe();
            }
        }
        
    }
    public void TriggerIFrames()
    {
        iFrameTimer = iFrameDuration;
        recentAttackIDs.Clear();
    }
    // Update is called once per frame

    void Update()
    {
        if (iFrameTimer > 0f)
        {
            iFrameTimer -= Time.deltaTime;
            if (iFrameTimer <= 0f)
            {
                recentAttackIDs.Clear();            
            }
        }
    }
}
