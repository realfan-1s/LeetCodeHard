using System.Xml.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Collections;
public class RandomizedCollection {
    // 字典内使用 HashSet 是为了实现 O(1) 删除索引，而且索引不会重复,使用一个字典维护某个数字的所有索引列表
    private Dictionary<int, HashSet<int>> idx{ get; set; } = new Dictionary<int, HashSet<int>>();
    // nums用于保存value的值
    private List<int> nums{ get; set; } = new List<int>();
    private Random random = new Random();
    /** Initialize your data structure here. */
    public RandomizedCollection(){
        // 列表随机插入是 0(n)，因为要后移插入位置之后的元素，但追加在列表尾部是O(1)
        //列表按索引查找是 O(1)，按值查找是 O(n)
    }

    /** Inserts a value to the collection. Returns true if the collection did not already contain the specified element. */
    public bool Insert(int val) {
        bool returnVal = true;
        if (!idx.TryGetValue(val, out var indexSet))
        {
            indexSet = new HashSet<int>();
            idx.Add(val, indexSet);
            returnVal = false;
        }

        nums.Add(val);
        indexSet.Add(nums.Count - 1);
        return returnVal;
    }
    
    /** Removes a value from the collection. Returns true if the collection contained the specified element. */
    public bool Remove(int val) {
        if (!idx.TryGetValue(val, out var indexSet) || indexSet.Count == 0)
            return false;

        // 将Values最后一个元素的值替换要删除的元素的最后一个索引的位置，然后Values删除最后一个元素，即为0(1)删除
        // 被替换的值的索引列表应该移除其索引值（Values的长度-1），并追加一个被删除元素所在的索引值（因为被替换的值被替换到这个索引了）
        var lastValue = nums.Last();
        if (lastValue == val)
        {
            indexSet.Remove(nums.Count - 1);
            nums.RemoveAt(nums.Count - 1);
            return true;
        }

        var delVal = indexSet.Last<int>();
        var exchangeList = idx[lastValue];

        indexSet.Remove(delVal);
        exchangeList.Remove(nums.Count - 1);
        exchangeList.Add(delVal);

        nums[delVal] = lastValue;
        nums.RemoveAt(nums.Count - 1);
        return true;
    }
    
    /** Get a random element from the collection. */
    public int GetRandom() {
        // 返回一个随机整数。
        return nums[random.Next(0, nums.Count)];
    }
}

/**
 * Your RandomizedCollection object will be instantiated and called as such:
 * RandomizedCollection obj = new RandomizedCollection();
 * bool param_1 = obj.Insert(val);
 * bool param_2 = obj.Remove(val);
 * int param_3 = obj.GetRandom();
 */