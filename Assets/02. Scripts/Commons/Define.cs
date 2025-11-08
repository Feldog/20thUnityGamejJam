
using UnityEngine;

public static class Define 
{
    public static readonly int entityPoseIndexAnim = Animator.StringToHash("poseIndex");
    public static readonly int entitySetPoseAnim = Animator.StringToHash("setPose");

    public static readonly int openAnim = Animator.StringToHash("open");

    public static readonly int PoseCount = 5;

    public static readonly int entityMax = 3;
    public static readonly int floorMax = 5;

    public static readonly int maxSelected = 3;

    public enum EGameStae { Play, Pause, GameOver, GameClear, Intro}
    public enum EAudioType { MASTER, SFX, BGM }
}
