using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseManager : MonoBehaviour {

    private Vector3 _position;
    private float _volume;

    public delegate void NoiseEvent(Vector3 position, float volume);
    public static event NoiseEvent NoiseMessage;

    public static void SendNoise(Vector3 position, float volume)
    {
        if(NoiseMessage != null)
        NoiseMessage(position, volume);

        
    }



    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = new Color(1, 0, 0, 0.2f);
        UnityEditor.Handles.DrawSolidArc(_position, Vector3.up, Vector3.forward, 360, _volume);

        _volume = 0;
        _position = Vector3.zero;
    }


    // Use this for initialization
    void Start () {
        NoiseMessage += delegate (Vector3 position, float volume)
        {
            _position = position;
            _volume = volume;
        };
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
