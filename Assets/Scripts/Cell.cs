using UnityEngine;

[CreateAssetMenu(fileName ="Cell", menuName ="ScriptableObject/Cell")]
public class Cell : ScriptableObject{

    public bool isWall;

    public bool isDeadLine;

    public bool hasBlock;

    public GameObject block;

    public Sprite backgroundSprite;
}
