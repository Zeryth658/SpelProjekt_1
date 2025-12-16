using System.Collections.Generic;
using UnityEngine;

public class NarrationManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioTrigger
    {
        public Collider2D triggerCollider;
        public AudioClip clip;

    }

    public List<AudioTrigger> audioTriggers = new List<AudioTrigger>();

}
