using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int numFeet;
    private int numEyes;
    private float[] position;

    public PlayerData(int _numFeet, int _numEyes, float[] _position)
    {
        numFeet = _numFeet;
        numEyes = _numEyes;
        position = _position;
    }

    #region GETTERS
    public int getNumFeet()
    {
        return numFeet;
    }

    public int getNumEyes()
    {
        return numEyes;
    }

    public float[] getPosition()
    {
        return position;
    }

    public Vector3 getVectorPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }
    #endregion


    #region SETTERS
    public int setNumFeet(int _numFeet)
    {
        numFeet = _numFeet;
        return numFeet;
    }

    public int setNumEyes(int _numEyes)
    {
        numEyes = _numEyes;
        return numEyes;
    }

    public void setPosition(Vector3 _position)
    {
        position = new float[3]
        {
            _position.x,
            _position.y,
            _position.z
        };
    }
    #endregion

    public void loadData(PlayerData data)
    {
        numFeet = data.numFeet;
        numEyes = data.numEyes;
        position = data.position;
    }

    public void resetData()
    {
        numFeet = 0;
        numEyes = 0;
        position = new float[3] {0f, 0f, 0f};
    }
}
