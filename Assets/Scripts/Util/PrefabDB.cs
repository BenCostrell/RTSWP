using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Prefab DB")]
public class PrefabDB : ScriptableObject {
	[SerializeField]
	private GameObject player;
	public GameObject Player { get { return player; } }

    [SerializeField]
    private GameObject[] scenes;
    public GameObject[] Scenes { get { return scenes; } }

    [SerializeField]
    private GameObject platform;
    public GameObject Platform { get { return platform; } }

    [SerializeField]
    private GameObject resource;
    public GameObject Resource { get { return resource; } }

    [SerializeField]
    private GameObject mine;
    public GameObject Mine { get { return mine; } }

    [SerializeField]
    private GameObject nexus;
    public GameObject Nexus { get { return nexus; } }

    [SerializeField]
    private GameObject speedBooster;
    public GameObject SpeedBooster { get { return speedBooster; } }
}
