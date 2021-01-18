#include <stdc++.h>

using namespace std;

class Solution {
public:
    // 交换条件 max(p(i, 0), p(i, 1)) > max (p'(i, 0), p'(i, 1))
    // 假如P(i,1)最大，则max的值是P(i,1)，P(i,1)需要大于右边的最大值，假如入P(i,0)最大，则如果P(i,1)大于右边的最大值，则P(i,0)自然也大于，由于 P(i,1)本来就大于P'(i,0)，所以只需要判断P(i,1)>P'(i,1)即可。p(i, 1) > p'(i, 1)
    int minimumEffort(vector<vector<int>>& tasks) {
        sort(tasks.begin(), tasks.end(), [](const vector<int>& a, const vector<int>& b){
            return a[0] - a[1] < b[0] - b[1];
        });

        int ans = 0, consumption = 0;
        for (auto& item : tasks){
            ans = max(ans, consumption + item[1]);
            consumption += item[0];
        }

        return ans;
    }
};