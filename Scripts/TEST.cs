using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ZartoxLog.Init();
        DebugEveryTypeOfDebugInLogClasse();
    }

    private void DebugEveryTypeOfDebugInLogClasse()
    {
        ZartoxLog.Print("test debug", Enums.LogLevel.Debug);

        ZartoxLog.Print("test info", Enums.LogLevel.Info);

        ZartoxLog.Print("test warn", Enums.LogLevel.Warning);

        ZartoxLog.Print("test error", Enums.LogLevel.Error);

        ZartoxLog.Print("test valid", Enums.LogLevel.Valid);
    }
}
