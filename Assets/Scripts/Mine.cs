using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building {

    [SerializeField]
    protected int baseResourcesPerTick;
    protected int resourcesPerTick_;
    protected int resourcesPerTick
    {
        get { return resourcesPerTick_; }
        set
        {
            resourcesPerTick_ = value;
            payoutRateUI.text = value + "g/s";
        }
    }
    [SerializeField]
    protected float tickRate;
    [SerializeField]
    protected float growthFactor;
    [SerializeField]
    protected float growthRate;
    protected float timeSincePayout;
    protected float timeSinceGrowth;
    [SerializeField]
    protected TextMesh payoutRateUI;

    protected override void OnClaim(Player player) {}

    protected override void OnClaimLost(Player player) {}

    protected override void SpecialUpdate()
    {
        if (owner != null)
        {
            timeSincePayout += Time.deltaTime;
            if (timeSincePayout >= tickRate)
            {
                PayOutToPlayer();
                timeSincePayout = 0;
            }
        }
        timeSinceGrowth += Time.deltaTime;
        if (timeSinceGrowth >= growthRate)
        {
            IncreasePayoutRate();
            timeSinceGrowth = 0;
        }
    }

    void PayOutToPlayer()
    {
        owner.GetPayout(resourcesPerTick);
    }

    public override void Init(Vector2 pos)
    {
        base.Init(pos);
        resourcesPerTick = baseResourcesPerTick;
        timeSincePayout = 0;
        type = BuildingType.Mine;
    }

    void IncreasePayoutRate()
    {
        resourcesPerTick = Mathf.RoundToInt(resourcesPerTick * growthFactor);
    }
}
