#pragma strict

function Update() 
{
    // Device don't go to sleep while playing
    Screen.sleepTimeout = SleepTimeout.NeverSleep;

}