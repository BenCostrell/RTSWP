using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : Building {

    [SerializeField]
    protected GameObject[] counters;
    protected TextMesh[] counterTextMeshes;

    [SerializeField]
    protected int controlTimeWinThreshold;
    protected int[] controlTimes;

    [SerializeField]
    protected float controlTickRate;
    protected float timeSinceControlTick;

    protected override void SpecialUpdate()
    {
        base.SpecialUpdate();
        if (owner != null) {
            timeSinceControlTick += Time.deltaTime;
            if (timeSinceControlTick >= controlTickRate)
            {
                controlTimes[owner.playerNum - 1] -= 1;
                counterTextMeshes[owner.playerNum - 1].text = 
                    controlTimes[owner.playerNum - 1].ToString();
                if (controlTimes[owner.playerNum -1] == 0)
                {
                    Services.Main.GameWin(owner);
                }
                timeSinceControlTick = 0;
            }
        }
    }

    public override void Init(Vector2 pos)
    {
        base.Init(pos);
        controlTimes = new int[Services.GameManager.numPlayers];
        counterTextMeshes = new TextMesh[Services.GameManager.numPlayers];
        for (int i = 0; i < Services.GameManager.numPlayers; i++)
        {
            counterTextMeshes[i] = counters[i].GetComponent<TextMesh>();
            controlTimes[i] = controlTimeWinThreshold;
            counterTextMeshes[i].text = controlTimes[i].ToString();
        }
    }
}
