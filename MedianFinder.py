from heapq import heapify, heappop, heappush


class MedianFinder:
    """
    查找中位数 O(1)O(1) ： 获取堆顶元素使用 O(1)O(1) 时间；
    添加数字 O(log N)O(logN) ： 堆的插入和弹出操作使用 O(log N)O(logN) 时间。
    空间复杂度 O(N)O(N) ： 其中 NN 为数据流中的元素数量，小顶堆 AA 和大顶堆 BB 最多同时保存 NN 个元素。
    """
    def __init__(self):
        """
        initialize your data structure here.
        小顶堆，保存较大的一半
        大顶堆，保存较小的一半
        """
        self.bigHeap = []
        self.smallHeap = []

    def addNum(self, num: int) -> None:
        if len(self.bigHeap) == len(self.smallHeap):
            heappush(self.smallHeap, num)
            heappush(self.bigHeap, -heappop(self.smallHeap))
        else:
            heappush(self.bigHeap, -num)
            heappush(self.smallHeap, -heappop(self.bigHeap))

    def findMedian(self) -> float:
        if len(self.bigHeap) == len(self.smallHeap):
            return (self.smallHeap[0] - self.bigHeap[0]) / 2
        else:
            return -self.bigHeap[0]