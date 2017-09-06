using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : Building {

    [SerializeField]
    protected int resourceMagnificationFactor;
    [SerializeField]
    protected float growthFactor;
    [SerializeField]
    protected float growthRate;
    protected float timeSinceGrowth;

    protected override void SpecialUpdate()
    {
        base.SpecialUpdate();
        timeSinceGrowth += Time.deltaTime;
        if(timeSinceGrowth >= growthRate)
        {
            resourceMagnificationFactor *= Mathf.RoundToInt(growthFactor);
        }
    }

    protected override void OnClaim(Player player)
    {
        base.OnClaim(player);
        player.resourceValueFactor *= resourceMagnificationFactor;
    }

    protected override void OnClaimLost(Player player)
    {
        base.OnClaimLost(player);
        player.resourceValueFactor /= resourceMagnificationFactor;
    }

    public override void Init(Vector2 pos)
    {
        base.Init(pos);
        type = BuildingType.Market;
    }
}
