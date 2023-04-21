using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructable
{
    public void Destroyed(); 
}

public class Collectibles : MonoBehaviour
{
    public enum MyCollectible
    {
        Plaza1,
        Plaza2,
        Plaza3,
        ResArea1,
        ResArea2,
        ResArea3,
        Bio1,
        Bio2,
        Bio3
    }
}