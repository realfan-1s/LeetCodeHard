using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks.Dataflow;
using ListNode;
namespace LeetCode23 {
    // 暴力求解,时间复杂度是O（NlogN),空间复杂度O(N)
    public class Solution1 {
        public ListNode MergeKLists (ListNode[] lists) {
            ListNode dummy = new ListNode (0);
            ListNode j = dummy;
            int length = lists.Length;
            List<int> ans = new List<int> ();
            foreach (var item in lists) {
                while (item != null) {
                    ans.Add (item.val);
                    item = item.next;
                }
            }
            ans.Sort ();
            foreach (var val in ans) {
                j.next = new ListNode (val);
                j = j.next;
            }

            return dummy.next;
        }
    }

    // 逐一比较，时间复杂度O(K^2 N),空间复杂度O(1)
    public class Solution2 {
        public ListNode MergeKLists (ListNode[] lists) {
            ListNode ans = null;
            for (var i = 0; i < lists.Length; i++)
                ans = MergeTwoList (ans, lists[i]);
            return ans;
        }
        public ListNode MergeTwoList (ListNode a, ListNode b) {
            if (a == null || b == null)
                return a != null ? a : b;
            ListNode head = new ListNode (0);
            ListNode j = head, aptr = a, bptr = b;
            while (aptr != null && bptr != null) {
                if (aptr.val < bptr.val) {
                    j.next = aptr;
                    aptr = aptr.next;
                } else {
                    j.next = bptr;
                    bptr = bptr.next;
                }
                j = j.next;
            }
            j.next = (aptr != null ? aptr : bptr);
            return head.next;
        }
    }

    // 分治,时间复杂度O(KN),空间复杂度O(1)
    public class Solution3 {
        int length;
        public ListNode MergeKLists (ListNode[] lists) {
            length = lists.Length - 1;
            return merge (lists, 0, length);
        }
        public ListNode Merge (ListNode[] lists, int left, int right) {
            if (left == right)
                return lists[left];
            if (left > right)
                return null;

            int mid = (left + right) / 2;
            return MergeTwoLists (Merge (lists, left, mid), Merge (lists, mid + 1, right));
        }
        public ListNode MergeTwoLists (ListNode node1, ListNode node2) {
            ListNode dummy = new ListNode (0);
            ListNode head = dummy;
            while (node1 != null && node2 != null) {
                if (node1.val < node2.val) {
                    head.next = node1;
                    node1 = node1.next;
                } else {
                    head.next = node2;
                    node2 = node1.next;
                }
                head = head.next;
            }
            head.next = (node1 != null ? node1 : node2);
            return dummy.next;
        }
    }
}