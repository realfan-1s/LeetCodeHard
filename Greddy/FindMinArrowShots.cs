using System;
public class Solution {
    // 第一个最小end端点区间时，若想要覆盖范围最大，箭一定要在end端点取得
    public int FindMinArrowShots(int[][] points) {
        Array.Sort(points, (x, y) => x[1].CompareTo(y[1]));

        int res = 0, rightPos = int.MinValue;

        if (points.Length >= 1 && points[0][0] == int.MinValue)
            res++;

        foreach (var item in points)
        {
            if (item[0] > rightPos)
            {
                rightPos = item[1];
                res++;
            }
        }

        return res;
    }
}
