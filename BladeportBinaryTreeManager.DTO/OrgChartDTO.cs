using System.Collections.Generic;

namespace BladeportBinaryTreeManager.DTO
{
    //Use this DTO for UI display
    public class OrgChartDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }   
        public List<OrgChartDTO> Children { get; set; }
    }

    //Extension to traverse the tree
    //This is needed in order to build the JSON display
    public static class OrgChartExtensions
    {
        public static IEnumerable<OrgChartDTO> Traverse(this OrgChartDTO root)   
        {
            var stack = new Stack<OrgChartDTO>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;
                foreach (var child in current.Children)
                    stack.Push(child);
            }
        }
    }

    
}
