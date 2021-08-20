using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBuffer
{
    List<JumpAktion> jumpQueue = new List<JumpAktion>(2);

    public JumpingBuffer()
    {
        jumpQueue.Capacity = 2;
    }

    public void Queue(JumpAktion jumpToQueue)
    {
        if (jumpQueue.Count < 2)
        {
            jumpQueue.Add(jumpToQueue);
        }
        else
        {
            jumpQueue[1] = jumpToQueue;
        }
    }

    public JumpAktion Dequeue()
    {
        if (IsAJumpInQueue())
        {
            JumpAktion jumpToDequeue = jumpQueue[0];
            jumpQueue.RemoveAt(0);
            return jumpToDequeue;
        }
        else
        {
            return null;
        }
    }

    public JumpAktion Peek()
    {
        if (jumpQueue.Count > 0)
        {
            return jumpQueue[0];
        }
        else
        {
            return null;
        }
    }

    public bool IsAJumpInQueue()
    {
        if (jumpQueue.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
