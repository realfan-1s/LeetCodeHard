#include <stdc++.h>

using namespace std;

// 暴力解法
// 找出每一行的前k个数的和并排序
class Solution1 {
public:
    int kthSmallest(vector<vector<int>>& mat, int k) {
        int col = mat.size();
        int row = mat[0].size();

        int arr[k * row];
        memset(arr, 0, sizeof(arr));
        int ans[k];
        memset(ans, 0, sizeof(ans));

        for (int i = 0; i < min(k, row); ++i)
            ans[i] = mat[0][i];

        for (int i = 1; i < col; ++i){
            int index = 0;
            for (int idx = 0; idx < k; ++idx){
                if (ans[idx] == 0)
                    break;
                for (int j = 0; j < row; ++j){
                    arr[index] = ans[idx] + mat[i][j];
                    ++index;
                }
            }

            sort(arr, arr + index);
            for (int j = 0; j < k; ++j)
                ans[j] = arr[j];
        }

        return ans[k - 1];
    }
};

class Solution2 {
public:
    int kthSmallest(vector<vector<int>>& mat, int k) {
        int col = mat.size();
        int row = mat[0].size();

        vector<int> ans(mat[0]);

        for (int i = 0; i < col; ++i){
            multiset<int> multiSet;
            for (auto num : ans)
                for (int j = 0; j < row; ++j)
                    multiSet.insert(num + mat[i][j]);

            ans.assign(multiSet.begin(), multiSet.end());
            ans.resize(min(k, (int)ans.size()));
        }

        return ans[k - 1];
    }
};

// BFS + 最小堆,O(k * n^2 * log(k))
class Solution3 {
public:
    int kthSmallest(vector<vector<int>>& mat, int k) {
        int index = 1;
        int col = mat.size();
        int row = mat[0].size();
        // string表示的是每一行的位置
        unordered_map<string, int> visited;
        auto Compare = [&visited] (string& a, string& b){
            return visited[a] > visited[b];
        };
        priority_queue<string, vector<string>, decltype(Compare)> heap(Compare);

        int curr = 0;
        for (int i = 0; i < col; ++i)
            curr += mat[i][0];
        visited[string(col, 0)] = curr;
        heap.push(string(col, 0));

        while (true){
            string tmp = heap.top();
            heap.pop();
            int sum = visited[tmp];
            if (index == k)
                return sum;

            for (int i = 0; i < col; ++i)
                if (tmp[i] != row - 1){
                    tmp[i]++;
                    if (visited.find(tmp) == visited.end()){
                        curr = sum + mat[i][tmp[i]] - mat[i][tmp[i] - 1];
                        visited[tmp] = curr;
                        heap.push(tmp);
                    }
                    tmp[i]--;
                }

            ++index;
        }

        return curr;
    }
};

// DFS + 二分查找
class Solution4 {
protected:
    vector<vector<int>> mat;
    int col, row;
public:
    int kthSmallest(vector<vector<int>>& mat, int k) {
        this->mat = mat;
        col = mat.size();
        row = mat[0].size();

        int left = 0, right = 0;
        for (int i = 0; i < col; ++i){
            left += mat[i][0];
            right += mat[i][row - 1];
        }
        int start = left;

        while (left < right){
            int mid = left + (right - left) / 2;
            int num = 1;
            DFS(mid, 0, start, num, k);
            if (num >= k)
                right = mid;
            else
                left = mid + 1;
        }
        return left;
    }

private:
    // index表示层数，sum表示层数之和,num表示是数组中第n小的和
    void DFS(int mid, int index, int sum, int& num, int k){
        if (sum > mid || index == col || num > k)
            return;
        DFS(mid, index + 1, sum, num, k);
        for (int i = 1; i < row; ++i){
            if (sum + mat[index][i] - mat[index][0] <= mid){
                ++num;
                DFS(mid, index + 1, sum + mat[index][i] - mat[index][0], num, k);
            } else
                break;
        }
    }
};
