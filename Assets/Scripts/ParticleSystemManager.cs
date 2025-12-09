using UnityEngine;

public static class ParticleSystemManager {

    public static Sprite[] bloodStainSprites;

    public static SpriteRenderer CreateBloodStain(
        Transform parent,
        Vector3 position,
        Color color,
        Vector3 scale,
        int sortingLayerId = -1,
        int sortOrder = 10
    ) {
        GameObject go = new GameObject("Blood Stain");
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = bloodStainSprites[Random.Range(0, bloodStainSprites.Length)];
        sr.color = color;
        sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        sr.sortingLayerID = sortingLayerId > 0
            ? sortingLayerId 
            : SortingLayer.layers[SortingLayer.layers.Length - 1].id;
        sr.sortingOrder = sortOrder;
        go.transform.SetParent(parent);
        go.transform.position = position;
        go.transform.localScale = scale;
        return sr;
    }
}

