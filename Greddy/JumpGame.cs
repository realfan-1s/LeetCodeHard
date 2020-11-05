using System;
// f[i]一定是单调递增的
public class Solution {
    public int Jump(int[] nums) {
        int length = nums.Length;
        int end = 0;
        int now = 0;
        int maxPos = 0;
        int steps = 0;
        while (end < length - 1)
        {
            maxPos = Math.Max(maxPos, now + nums[now]);
            if (now == end)
            {
                steps++;
                end = maxPos;
            }

            now++;
        }

        return steps;
    }
}