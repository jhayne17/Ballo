using UnityEngine;

public class BallSkinLoader : MonoBehaviour
{
    public Material whiteMaterial;
    public Material blueMaterial;
    public Material purpleMaterial;

    void Start()
    {
        string selectedSkin = PlayerPrefs.GetString("SelectedSkin", "White");
        Renderer r = GetComponent<Renderer>();

        switch (selectedSkin)
        {
            case "Blue":
                r.material = blueMaterial;
                break;

            case "Purple":
                r.material = purpleMaterial;
                break;

            default:
                r.material = whiteMaterial;
                break;
        }
    }
}