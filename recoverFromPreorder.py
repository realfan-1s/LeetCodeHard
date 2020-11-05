class TreeNode:
    def __init__(self, x):
        self.val = x
        self.left = None
        self.right = None


class Solution:
    def recoverFromPreorder(self, S: str) -> TreeNode:
        """
        每次解析出一个节点的深度 depth 和对应的节点值 val ，再去查找它对应的父节点，并把当前节点挂载到
        父节点对应的子节点上去（优先left，再是right）。这里的关键是怎么确定当前节点的父节点。
        """
        if not S:
            return None

        from collections import defaultdict
        # 记录访问过的深度为depth的节点列表
        depth_Node = defaultdict(list)
        start = 0
        while start < len(S):
            end = start
            # 记录深度
            while end < len(S) and S[end] == "-":
                end += 1
            depth = end - start

            # 获得对应深度的值
            start = end
            while end < len(S) and S[end] != "-":
                end += 1
            node = TreeNode(S[start:end])

            # 将深度值与节点对应设置
            depth_Node[depth].append(node)
            # 将该节点挂载到对应的父节点上
            if depth > 0:
                if depth_Node[depth - 1][-1].left:
                    depth_Node[depth - 1][-1].right = node
                else:
                    depth_Node[depth - 1][-1].left = node

            start = end

        return depth_Node[0][0]
