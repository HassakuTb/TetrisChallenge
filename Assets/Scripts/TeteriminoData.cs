using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName ="TetriminoData", menuName ="ScriptableObject/TetriminoData")]
public class TeteriminoData : ScriptableObject{

    public int size;

    [Multiline]
    public string data;

    public bool[,] actual;

    protected void OnEnable() {
        actual = new bool[size, size];

        string[] lines = data.Split(new char[] { '\n', '\r' }).Reverse().ToArray();

        for(int x = 0; x < size; ++x) {
            for(int y = 0; y < size; ++y) {
                actual[x, y] = lines[y].ElementAt(x) == '1';
            }
        }
    }

    public void TurnRight() {
        bool[,] dst = new bool[size, size];

        for (int x = 0; x < size; ++x) {
            for (int y = 0; y < size; ++y) {
                dst[x, y] = actual[y, size - x - 1];
            }
        }

        actual = dst;
    }

    public void TurnLeft() {
        bool[,] dst = new bool[size, size];

        for (int x = 0; x < size; ++x) {
            for (int y = 0; y < size; ++y) {
                dst[x, y] = actual[size - y - 1, x];
            }
        }

        actual = dst;
    }
}
