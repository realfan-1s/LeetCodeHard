#include <stdc++.h>

using namespace std;

class Solution {
public:
    int Compare(vector<int>& nums1, vector<int>& nums2, int idx1, int idx2){
        int len1 = nums1.size(), len2 = nums2.size();
        while (idx1 < len1 && idx2 < len2)
        {
            int diff = nums1[idx1] - nums2[idx2];
            if (diff != 0)
                return diff;
            idx1++;
            idx2++;
        }
        return (len1 - idx1) - (len2 - idx2);
    }
    vector<int> Merge(vector<int>& nums1, vector<int>& nums2){
        int len1 = nums1.size(), len2 = nums2.size();
        if (len1 == 0)
            return nums2;
        if (len2 == 0)
            return nums1;
        int mergeLen = len1 + len2;
        vector<int> merge(mergeLen);
        int idx1 = 0, idx2 = 0;
        for (int i = 0; i < mergeLen; i++){
            if (Compare(nums1, nums2, idx1, idx2) > 0)
                merge[i] = nums1[idx1++];
            else
                merge[i] = nums2[idx2++];
        }
        return merge;
    }

    vector<int> MaxSubsequence(vector<int>& nums, int k){
        int length = nums.size();
        int top = -1;
        int remain = length - k;
        vector<int> stack(k, 0);
        for (int i = 0; i < length; i++){
            int num = nums[i];
            while (top >= 0 && stack[top] < num && remain > 0){
                top--;
                remain--;
            }
            if (top < k - 1)
                stack[++top] = num;
            else
                remain--;
        }
        return stack;
    }

    vector<int> maxNumber(vector<int>& nums1, vector<int>& nums2, int k) {
        int len1 = nums1.size(), len2 = nums2.size();
        vector<int> ans(k, 0);
        int start = max(0, k - len2);
        int end = min(k, len1);
        for (int i = start; i <= end; i++){
            vector<int> Subsequence1 = MaxSubsequence(nums1, i);
            vector<int> Subsequence2 = MaxSubsequence(nums2, k - i);
            vector<int> mergeMaxSub = Merge(Subsequence1, Subsequence2);
            if (Compare(mergeMaxSub, ans, 0, 0 ) > 0)
                ans.swap(mergeMaxSub);
        }
        return ans;
    }

};