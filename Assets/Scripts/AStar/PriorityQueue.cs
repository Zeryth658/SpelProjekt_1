using System;
using System.Collections.Generic;


public class PriorityQueue
{
    private List<GridNode> _heap = new List<GridNode>();

    public int Count => _heap.Count;

    public void Enqueue(GridNode node)
    {
        _heap.Add(node);
        BubbleUp(_heap.Count - 1);
    }

    public GridNode Dequeue()
    {
        if (_heap.Count == 0) return null;
        GridNode root = _heap[0];
        int last = _heap.Count - 1;
        _heap[0] = _heap[last];
        _heap.RemoveAt(last);
        if (_heap.Count > 0) BubbleDown(0);
        return root;
    }

    public bool Contains(GridNode node) => _heap.Contains(node);

    private void BubbleUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (Compare(_heap[index], _heap[parent]) < 0)
            {
                Swap(index, parent);
                index = parent;
            }
            else break;
        }
    }

    private void BubbleDown(int index)
    {
        int last = _heap.Count - 1;
        while (true)
        {
            int left = index * 2 + 1;
            int right = index * 2 + 2;
            int smallest = index;

            if (left <= last && Compare(_heap[left], _heap[smallest]) < 0) smallest = left;
            if (right <= last && Compare(_heap[right], _heap[smallest]) < 0) smallest = right;

            if (smallest == index) break;
            Swap(index, smallest);
            index = smallest;
        }
    }

    private int Compare(GridNode a, GridNode b)
    {
        int compare = a.FScore.CompareTo(b.FScore);
        if (compare != 0) return compare;
        
        return a.HScore.CompareTo(b.HScore);
    }

    private void Swap(int i, int j)
    {
        (_heap[i], _heap[j]) = (_heap[j], _heap[i]);
    }
}