using System;
using System.Collections.Generic;

public class Solution {
    public int CountRangeSum (int[] nums, int lower, int upper) {
        int length = nums.Length;
        long s = 0;
        List<long> sum = new List<long> () { 0 };

        foreach (var num in nums) {
            s += num;
            sum.Add (s);
        }

        return MergeSort (sum, lower, upper, 0, length);
    }

    public int MergeSort (List<long> sum, int lower, int upper, int left, int right) {
        if (left >= right)
            return 0;

        int mid = (left + right) / 2;
        int n1 = MergeSort (sum, lower, upper, left, mid);
        int n2 = MergeSort (sum, lower, upper, mid + 1, right);
        int res = n1 + n2;

        // 记录下标和在[lower, upper]之间的个数
        int i = left, l = mid + 1, r = mid + 1;
        while (i <= mid) {
            while (l <= right && sum[l] - sum[i] < lower)
                l++;
            while (r <= right && sum[r] - sum[i] <= upper)
                r++;

            res += r - l;
            i++;
        }

        // 归并排序
        List<long> sorted = new List<long> ();
        int p1 = left, p2 = mid + 1;
        while (p1 <= mid || p2 <= right) {
            if (p1 > mid)
                sorted.Add (sum[p2++]);
            else if (p2 > right)
                sorted.Add (sum[p1++]);
            else {
                if (sum[p1] < sum[p2])
                    sorted.Add (sum[p1++]);
                else
                    sorted.Add (sum[p2++]);
            }
        }

        for (int j = 0; j < sorted.Count; j++)
            sum[left + j] = sorted[j];

        return res;
    }
}