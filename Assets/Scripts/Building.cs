using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour {

    public enum BuildingType
    {
        Monument,
        Mine,
        SpeedBoost
    }
    [SerializeField]
    protected int baseCost;
    [SerializeField]
    protected float costAdjustRatio;
    protected int cost_;
    public int cost
    {
        get { return cost_; }
        protected set
        {
            cost_ = value;
            costUI.text = value + "g";
        }
    }
    protected BuildingType type;
    public Player owner { get; protected set; }
    [SerializeField]
    protected TextMesh costUI;
    [SerializeField]
    protected GameObject border;
    protected SpriteRenderer borderSr;

    // Update is called once per frame
    void Update () {
        SpecialUpdate();
	}

    protected virtual void SpecialUpdate()
    {
    }

    public void GetClaimed(Player player)
    {
        if (owner != null) OnClaimLost(owner);
        owner = player;
        OnClaim(player);
        cost = Mathf.RoundToInt(cost * costAdjustRatio);
        borderSr.color = player.color;
    }

    protected virtual void OnClaim(Player player) { }

    protected virtual void OnClaimLost(Player player) { }

    public virtual void Init(Vector2 pos)
    {
        transform.position = pos;
        cost = baseCost;
        borderSr = border.GetComponent<SpriteRenderer>();
    }

}
