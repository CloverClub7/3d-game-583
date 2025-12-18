using UnityEngine;

public class Bottles : MonoBehaviour
{
    /* 0: health
     * 1: 2x damage
     * 2: 2x score
     */
    public int bottleType = 0;
    public Mesh red;
    public Mesh green;
    public Mesh blue;
    public BottleSpawner spawnedFrom;

    public void ChangeColour()
    {
        MeshFilter currentMesh = GetComponent<MeshFilter>();
        switch (bottleType)
        {
            case 0:
                currentMesh.mesh = red;
                break;
            case 1:
                currentMesh.mesh = green;
                break;
            case 2:
                currentMesh.mesh = blue;
                break;
            default:
                Debug.Log("Invalid integer value for bottle.");
                break;
        }
    }

    void OnDestroy()
    {
        spawnedFrom.bottleCount--;
    }
}
