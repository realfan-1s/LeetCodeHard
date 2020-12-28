using System;
namespace Leetcode4 {
    // 二分查找
    public class Solution1 {
        public double FindMedianSortedArrays (int[] nums1, int[] nums2) {
            if (nums1.Length > nums2.Length) {
                int[] tmp = nums1;
                nums1 = nums2;
                nums2 = tmp;
            }
            int m = nums1.Length;
            int n = nums2.Length;
            int totalLeft = m + (n - m + 1) / 2;

            int left = 0;
            int right = m;
            int i = 0, j = 0;

            while (left < right) {
                // 划分数组1和数组2
                // 使得nums[i - 1] <= nums[j] && nums[i] >= nums[j - 1]
                int i = left + (right - left) / 2;
                int j = totalLeft - i;
                if (nums1[i] < nums2[j - 1])
                    // 下一轮的搜索区间[i + 1, right]
                    left = i + 1;
                else
                    // 下一轮的搜索区间[left, i]
                    right = i;
            }
            i = left;
            j = totalLeft - i;
            int nums1LeftMax = (i == 0 ? int.MinValue : nums1[i - 1]);
            int nums1RightMin = (i == m ? int.MaxValue : nums1[i]);
            int nums2LeftMax = (j == 0 ? int.MinValue : nums2[j - 1]);
            int nums2RightMin = (j == n ? int.MaxValue : nums2[j]);

            if ((m + n) % 2 == 1)
                return Math.Max (nums1LeftMax, nums2LeftMax);
            else
                return (float) (Math.Max (nums1LeftMax, nums2LeftMax) + Math.Min (nums1RightMin, nums2RightMin)) / 2;
        }
    }
    // 划分数组
    public class Solution2 {
        public double FindMedianSortedArrays (int[] nums1, int[] nums2) {
            int m = nums1.Length;
            int n = nums2.Length;
            if (m > n)
                return FindMedianSortedArrays (nums2, nums1);

            int totalLeft = m + (n - m + 1) / 2;
            int left = 0, right = m, ansi = -1;
            // median1:左半边的最大值，median：右半边的最小值
            int median1 = 0, median2 = 0;

            while (left <= right) {
                int i = left + (right - left) / 2;
                int j = totalLeft - i;

                int nums1LeftMax = (i == 0 ? int.MinValue : nums1[i - 1]);
                int nums1RightMin = (i == m ? int.MaxValue : nums1[i]);
                int nums2LeftMax = (j == 0 ? int.MinValue : nums2[j - 1]);
                int nums2RightMin = (j == n ? int.MaxValue : nums2[j]);

                if (nums1LeftMax <= nums1RightMin) {
                    ansi = i;
                    left = i + 1;
                    median1 = Math.Max (nums1LeftMax, nums2LeftMax);
                    median2 = Math.Min (nums1RightMin, nums2RightMin);
                } else
                    right = i - 1;
            }

            return (m + n) % 2 == 1 ? median1 : (median2 + median1) / 2.0d;
        }
    }
}