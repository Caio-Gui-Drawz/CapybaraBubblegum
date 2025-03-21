using UnityEngine;

public class DontDestoy : MonoBehaviour
{
   void Awake ()
   {
    DontDestroyOnLoad(this);
   }
}
