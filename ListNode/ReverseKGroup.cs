using System.Runtime.InteropServices;
using ListNode;
public class Solution {
    public ListNode ReverseKGroup (ListNode head, int k) {
        if (head == null || k == 1)
            return head;
        ListNode dummy = new ListNode (0);
        dummy.next = head;

        // 到达第k+1个节点
        int index = 1;
        ListNode node = head;
        while (node.next != null && index < k) {
            index++;
            node = node.next;
        }
        if (index < k)
            return dummy.next;

        // 预备翻转链表
        ListNode pivot = node.next;
        node.next = null;
        dummy.next = Reverse (head);
        // 拼接链表并返回
        head.next = ReverseKGroup (pivot, k);
        return dummy.next;
    }

    private ListNode Reverse (ListNode head) {
        ListNode previous = null;
        ListNode currentNode = head;
        while (currentNode != null) {
            ListNode tmp = currentNode.next;
            currentNode.next = previous;
            previous = currentNode;
            currentNode = tmp;
        }
        return previous;
    }
}