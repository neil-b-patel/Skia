using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    bool inLight = false;

    SceneBehavior sceneBehavior;
    ProgressManager progressManager;
    PlayerData playerData;

    public Sprite lightSprite;
    public Sprite shadowSprite;
    public CinemachineVirtualCamera vcam;

    void Awake()
    {
        sceneBehavior = FindObjectOfType<SceneBehavior>();
        progressManager = FindObjectOfType<ProgressManager>();
        playerData = FindObjectOfType<PlayerData>();
    }

    #region GETTERS
    public int GetNumFeet()
    {
        return playerData.numFeet;
    }

    public int GetNumEyes()
    {
        return playerData.numEyes;
    }

    public float[] GetPosition()
    {
        return playerData.position;
    }

    public Vector3 GetVectorPosition()
    {
        return new Vector3(
            playerData.position[0], 
            playerData.position[1], 
            playerData.position[2]
        );
    }
    #endregion


    #region SETTERS
    public void SetNumFeet(int _numFeet)
    {
        playerData.numFeet = _numFeet;
    }

    public void SetNumEyes(int _numEyes)
    {
        playerData.numEyes = _numEyes;
    }

    public void SetPosition(Vector3 _position)
    {
        playerData.position = new float[3]
        {
            _position.x,
            _position.y,
            _position.z
        };
    }

    public void SetItems(List<string> _items)
    {
        playerData.items = _items;
    }

    public void AddItem(string item)
    {
        playerData.items.Add(item);
    }

    public void SetMusic(bool isLightPlayer)
    {
        progressManager.SetMusic(isLightPlayer);
    }

    public void SetInLight(bool onTriggerStay)
    {
        inLight = onTriggerStay;
    }
    #endregion

    public void OnItemPickup(Collider item, string parentName)
    {
        switch (parentName)
        {
            case "Feet":
                SetNumFeet(GetNumFeet() + 1);

                break;
            case "Eyes":
                SetNumEyes(GetNumEyes() + 1);
                break;
        }

        AddItem(item.name);
        SetPosition(transform.position);
            
        progressManager.SetMusic();

        Destroy(item.gameObject);

        progressManager.EvolvePlayer();
    }

    public void OnLightEnter(bool direction, Vector3 position)
    {
        inLight = true;

        //GameObject shadowPlayer = transform.GetChild(1).gameObject;
        //shadowPlayer.SetActive(true);
        //shadowPlayer.GetComponent<ShadowPlayerController>().SetDirectionToFace(direction);
        //shadowPlayer.GetComponent<ShadowPlayerController>().SetPosition(position);

        GameObject player = transform.GetChild(0).gameObject;
        ShadowPlayerController shadowPlayer = player.GetComponent<ShadowPlayerController>();
        LightPlayerController lightPlayer = player.GetComponent<LightPlayerController>();

        player.GetComponent<SpriteRenderer>().sprite = shadowSprite;
        player.GetComponent<Animator>().enabled = false;
        shadowPlayer.enabled = true;
        shadowPlayer.SetDirectionToFace(direction);
        shadowPlayer.SetPosition(position);

        vcam.Follow = player.transform;

        lightPlayer.enabled = false;

        //GetComponentInChildren<ShadowPlayerController>().gameObject.SetActive(true);
        //GetComponentInChildren<LightPlayerController>().gameObject.SetActive(false);
    }

    public void OnLightExit(bool direction, Vector3 position)
    {
        inLight = false;

        //GameObject lightPlayer = transform.GetChild(0).gameObject;
        //GameObject shadowPlayer = transform.GetChild(1).gameObject;

        GameObject player = transform.GetChild(0).gameObject;
        ShadowPlayerController shadowPlayer = player.GetComponent<ShadowPlayerController>();
        LightPlayerController lightPlayer = player.GetComponent<LightPlayerController>();

        player.GetComponent<SpriteRenderer>().sprite = lightSprite;
        player.GetComponent<Animator>().enabled = true;
        lightPlayer.enabled = true;
        lightPlayer.SetDirectionToFace(direction);
        lightPlayer.SetPosition(position);

        vcam.Follow = lightPlayer.transform;

        shadowPlayer.enabled = false;

        //GetComponentInChildren<ShadowPlayerController>().gameObject.SetActive(false);
        //GetComponentInChildren<LightPlayerController>().SetDirectionToFace(direction);
        //GetComponentInChildren<LightPlayerController>().gameObject.SetActive(true);
    }

    public void OnWaterEnter(Collider water)
    {
        sceneBehavior.GameOver();
    }
}
