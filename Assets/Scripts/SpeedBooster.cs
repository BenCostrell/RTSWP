using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : Building {

    [SerializeField]
    protected float speedBoostFactor;

    protected override void OnClaim(Player player)
    {
        base.OnClaim(player);
        player.groundAccel *= speedBoostFactor;
    }

    protected override void OnClaimLost(Player player)
    {
        base.OnClaimLost(player);
        player.groundAccel /= speedBoostFactor;
    }
}
