#include <stdc++.h>

using namespace std;

// 堆
class Solution1 {
public:
    vector<int> maxSlidingWindow(vector<int>& nums, int k) {
        int len = nums.size();
        vector<int> ans;
        auto Compare = [&](const pair<int, int>& a, const pair<int, int>& b){
            return a.first < b.first;
        };
        priority_queue<pair<int, int>, vector<pair<int, int>>, decltype(Compare)> heap(Compare);

        for (int i = 0; i < len; ++i)
        {
            while (!heap.empty() && heap.top().second + k <= i){
                heap.pop();
            }
            heap.push(pair<int, int>(nums[i], i));
            if (heap.size() >= k)
                ans.emplace_back(heap.top().first);
        }

        return ans;
    }
};

// 单调队列
// 滑动窗口的如果当前的滑动窗口中有两个下标 i 和 jj，其中 i 在 j 的左侧（i < j），并且 i 对应的元素不大于 j 对应的元素（nums[i] <= nums[j]),只要i还在窗口中，则j也一定在窗口中
// 此时组成的双端队列队首就是最大值，接下来是第二最大值、第三最大值......
class Solution2 {
public:
    vector<int> maxSlidingWindow(vector<int>& nums, int k) {
        int len = nums.size();
        deque<int> swdeque;

        for (int i = 0; i < k; ++i){
            while (!swdeque.empty() && nums[swdeque.back()] < nums[i]){
                swdeque.pop_back();
            }
            swdeque.emplace_back(i);
        }
        vector<int> ans = {nums[swdeque.front()]};


        for (int i = k; i < len; ++i){
            while (!swdeque.empty() && nums[swdeque.back()] < nums[i]){
                swdeque.pop_back();
            }
            swdeque.emplace_back(i);

            while (!swdeque.empty() && swdeque.front() + k <= i){
                swdeque.pop_front();
            }
            ans.emplace_back(nums[swdeque.front()]);
        }
        return ans;
    }
};