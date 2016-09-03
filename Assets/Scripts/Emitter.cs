using UnityEngine;

[CreateAssetMenu(fileName = "Emitter", menuName = "ScriptableObject/Emitter")]
public class Emitter : ScriptableObject {

    public TeteriminoData[] table;

    public TeteriminoData RandomMinoData() {
        return ScriptableObject.Instantiate<TeteriminoData>(table[Random.Range(0, table.Length)]);
    }
}
