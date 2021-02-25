#include <vector>
#include <queue>
#include <unordered_map>
#include <set>
#include <stdio.h>

using namespace std;

// 红黑树,添加、删除时间复杂度都是log(n)
class Solution
{
public:
    vector<double> medianSlidingWindow(vector<int> &nums, int k)
    {
        multiset<int> mt;
        int len = nums.size();
        int left = 0, right = 0;
        vector<double> ans;
        while (right < len)
        {
            mt.insert(nums[right]);
            if (mt.size() > k)
                mt.erase(mt.find(nums[left++]));
            if (mt.size() == k)
            {
                if (k & 1 == 1)
                {
                    ans.emplace_back(*next(mt.begin(), k / 2));
                }
                else
                {
                    ans.emplace_back(((double)*next(mt.begin(), k / 2) + (double)*next(mt.begin(), k / 2 - 1)) / 2.0);
                }
            }
            ++right;
        }
        return ans;
    }
};

// 双优先队列+延迟删除
class DualHeap
{
private:
    // 大根堆，维护较小的一半，小根堆，维护较大的一半
    priority_queue<int> small;
    priority_queue<int, vector<int>, greater<int>> big;
    int smallSize, bigSize;
    // 哈希表，记录「延迟删除」的元素，key 为元素，value 为需要删除的次数
    unordered_map<int, int> delayed;
    int k;

public:
    DualHeap(int _k) : smallSize(0), bigSize(0), k(_k) {}

private:
    // 不断地弹出 heap 的堆顶元素，并且更新哈希表
    template <typename T>
    void Prune(T &heap)
    {
        while (!heap.empty())
        {
            int num = heap.top();
            // delayed减少，计数的原因是，不一定所有相同数字都要删除
            if (delayed.find(num) != delayed.end())
            {
                --delayed[num];
                if (!delayed[num])
                    delayed.erase(num);
                heap.pop();
            }
            else
            {
                break;
            }
        }
    }

    void MakeBalance()
    {
        if (smallSize > bigSize + 1)
        {
            big.push(small.top());
            small.pop();
            smallSize--;
            bigSize++;
            // 小根堆堆顶被移除，进行prune
            Prune(small);
        }
        else if (bigSize > smallSize)
        {
            small.push(big.top());
            big.pop();
            smallSize++;
            bigSize--;
            // 大根堆堆顶被移除，进行prune
            Prune(big);
        }
    }

public:
    void Insert(int num)
    {
        // 优先放小根堆，若插入值小于小根堆堆顶，也放入小根堆
        if (small.empty() || num <= small.top())
        {
            small.push(num);
            ++smallSize;
        }
        else
        {
            big.push(num);
            ++bigSize;
        }

        MakeBalance();
    }

    void Erase(int num)
    {
        delayed[num]++;
        if (num <= small.top())
        {
            --smallSize;
            if (num == small.top())
                Prune(small);
        }
        else
        {
            --bigSize;
            if (num == big.top())
                Prune(big);
        }

        MakeBalance();
    }

    double GetMedian()
    {
        if (k & 1 == 1)
            return (double)small.top();
        else
        {
            return ((double)small.top() + (double)big.top()) / 2.0;
        }
    }
};

class Solution
{
public:
    vector<double> medianSlidingWindow(vector<int> &nums, int k)
    {
        DualHeap dualHeap(k);
        int len = nums.size();
        for (int i = 0; i < k; ++i)
        {
            dualHeap.Insert(nums[i]);
        }
        vector<double> ans = {dualHeap.GetMedian()};

        for (int i = k; i < len; ++i)
        {
            dualHeap.Insert(nums[i]);
            dualHeap.Erase(nums[i - k]);
            ans.emplace_back(dualHeap.GetMedian());
        }

        return ans;
    }
};