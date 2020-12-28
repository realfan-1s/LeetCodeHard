using System;
public class Solution {
    public int[] MaxNumber(int[] nums1, int[] nums2, int k) {
        // 维护一个从栈底到栈顶的元素单调递减的单调栈，
        // 从左向右遍历数组，并维护单调栈内元素
        // 自定义比较方法，先比较当前元素，若不同，则选取其中较大的元素
        // 否则需要比较后面的所有元素才能决定选择哪个元素
        int len1 = nums1.Length, len2 = nums2.Length;
        int start = Math.Max(0 , k - len2);
        int end = Math.Min(k, len1);
        int[] ans = new int[k];

        for (int i = start; i <= end; i++){
            int[] subsequence1 = MaxSubsequence(nums1, i);
            int[] subsequence2 = MaxSubsequence(nums2, k - i);
            int[] mergeSubsequence = Merge(subsequence1, subsequence2);
            if (Compare(mergeSubsequence, ans, 0, 0) > 0)
                Array.Copy(MaxSubsequence, ans, k);
        }

        return ans;
    }
    public int[] MaxSubsequence(int[] nums, int k){
        int length = nums.Length;
        int[] stack = new int[k];
        int top = -1, remain = length - k;
        for (var i = 0; i < length; i++)
        {
            int num = nums[i];
            // 如果下一个数比当前数大，则舍弃当前数，
            // 但若可舍弃元素小于1，证明无元素可舍弃
            while (top >= 0 && stack[top] < num && remain > 0){
                top--;
                remain--;
            }
            if (top < k - 1)
                stack[++top] = num;
            else
            // 已经取够k个，直接丢弃
                remain--;
        }
        return stack;
    }
    public int Compare(int[] nums1, int[] nums2, int idx1, int idx2){
        int len1 = nums1.Length, len2 = nums2.Length;
        // 哪个大取哪个，若当前位相同则比较下一个元素
        while (idx1 < len1 && idx2 < len2){
            int diff = nums1[idx1] - nums2[idx2];
            if (diff != 0)
                return diff;
            idx1++;
            idx2++;
        }
        return (len1 - idx1) - (len2 - idx2);
    }
    public int[] Merge(int[] nums1, int[] nums2){
        int len1 = nums1.Length, len2 = nums2.Length;
        // 若两者有一个为0
        if (len1 == 0)
            return nums2;
        if (len2 == 0)
            return nums1;

        int mergeLen = len1 + len2;
        int[] merge = new int[mergeLen];
        // 从大到小降序排列
        int pivot1 = 0, pivot2 = 0, i = 0;
        while (i < mergeLen){
            if (Compare(nums1, nums2, pivot1, pivot2) > 0)
                merge[i++] = nums1[pivot1++];
            else
                merge[i++] = nums2[pivot2++];
        }
        return merge;
    }
}