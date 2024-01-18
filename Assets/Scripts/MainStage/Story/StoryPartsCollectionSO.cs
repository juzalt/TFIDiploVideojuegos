using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class StoryPartsCollectionSO : ScriptableObject
{
    public StorySO intro;
    public StorySO intro2;
    public StorySO finishedOrderGame;
    public StorySO finishedStudyingGame;
    public StorySO finishedGame;
}
