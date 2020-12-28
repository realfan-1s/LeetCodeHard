using System.Collections.Generic;
// 归并排序
// 分割+求解+合并
public class Solution1 {
    public int ReversePairs(int[] nums) {
        if (nums.Length < 2)
            return 0;
        return RescursiveSort(nums, 0, nums.Length - 1);
    }
    public int RescursiveSort(int[] nums, int left, int right){
        if (left == right)
            return 0;
        else{
            int mid = (left + right) / 2;
            int n1 = RescursiveSort(nums, left, mid);
            int n2 = RescursiveSort(nums, mid + 1, right);
            int res = n1 + n2;

            // 两个升序排列的数组 sorted 和nums[left : right]，若nums[i] > 2 * nums[j] 则后续所有的数都大于2 * nums[j]
            int i = left;
            int j = mid + 1;
            while (i <= mid){
                while (j <= right && nums[i] > 2 * (long)nums[j])
                    j++;
                res += (j - mid - 1);
                i++;
            }

            // 归并排序
            int[] sorted = new int[right - left + 1];
            int p1 = left, p2 = mid + 1;
            int p = 0;
            while (p1 <= mid || p2 <= right){
                if (p1 > mid)
                    sorted[p++] = nums[p2++];
                else if (p2 > right)
                    sorted[p++] = nums[p1++];
                else{
                    if (nums[p1] < nums[p2])
                        sorted[p++] = nums[p1++];
                    else
                        sorted[p++] = nums[p2++];
                }
            }
            for (int k = 0; k < sorted.Length; k++)
                nums[left + k] = sorted[k];

            return res;
        }
    }
}

// 树状数组,可用于区间修改，单点查询；单点修改，区间查询；区间修改，区间查询
// 数组 => 树状数组 => 前缀区间维护 => 和、异或和、最大值、最小值...
// 对于nums[i] 而言，我们首先查询 [1, 2 * nums[i]] 的数量，再求出 [1,maxValue] 的数量（其中maxValue 为数组中最大元素的二倍）。二者相减，就能够得到以 i 为右端点的翻转对数量。
public class Solution2 {
    public int ReversePairs(int[] nums) {
        SortedSet<long> allNumbers = new SortedSet<long>();
        foreach (int num in nums){
            allNumbers.Add((long)num);
            allNumbers.Add(2 * (long)num);
        }

        Dictionary<long, int> values = new Dictionary<long, int>();
        int index = 0;
        foreach (var num in allNumbers)
        {
            values.Add(num, index);
            index++;
        }

        int res = 0;
        BIT bit = new BIT(values.Count);
        for (int i = 0; i < nums.Length; i++){
            int left = values[(long)nums[i] * 2];
            int right = values.Count - 1;
            res += bit.Query(right + 1) - bit.Query(left + 1);
            bit.Update(values[(long)nums[i]] + 1, 1);
        }
        return res;
    }
    public class BIT{
        int[] tree;
        int n;
        public BIT(int n){
            this.n = n;
            this.tree = new int[n + 1];
        }
        public static int Lowbit (int x){
            return x & (-x);
        }
        public void Update(int x, int d){
            while (x <= n){
                tree[x] += d;
                // 从子节点层层向父节点更新数值
                x += Lowbit(x);
            }
        }
        // 求N项和，区间查询即query[max] - query[min]
        public int Query(int x){
            int ans = 0;
            while (x != 0){
                ans += tree[x];
                // 从这点的向左上查找到上一个节点
                x -= Lowbit(x);
            }
            return ans;
        }
    }
}