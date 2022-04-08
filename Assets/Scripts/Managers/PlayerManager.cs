using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerData playerData;
    ProgressManager progressManager;

    void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();
        progressManager = FindObjectOfType<ProgressManager>();
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
    #endregion

    public void OnItemPickup(Collider item, string parentName)
    {
        if (item.CompareTag("CanBePickedUp"))
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
        }
    }

}
