using System;
// BucketSort,时间复杂度O（N），空间复杂度O(N)
public class Solution1 {
    public int MaximumGap(int[] nums) {
        int length = nums.Length;
        if (length < 2)
            return 0;
        int[] buf = new int[length];
        int exp = 1;
        int maxVal = int.MinValue;
        foreach (var item in nums)
            maxVal = Math.Max(maxVal, item);

        while (maxVal >= exp){
            int[] cnt = new int[10];
            for (int i = 0; i < length; i++){
                int digit = (nums[i] / exp) % 10;
                cnt[digit]++;
            }
            for (int i = 1; i < 10; i++)
                cnt[i] += cnt[i - 1];
            for (int i = 0; i < length; i++){
                int digit = (nums[i] / exp) % 10;
                buf[cnt[digit] - 1] = nums[i];
                cnt[digit]--;
            }
            Array.Copy(buf, nums, length);
            exp *= 10;
        }

        int res = 0;
        for (int i = 0; i < length - 1; i++)
            res = Math.Max(res, nums[i + 1] - nums[i]);
        return res;
    }
}