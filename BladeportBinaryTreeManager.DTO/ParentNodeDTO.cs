namespace BladeportBinaryTreeManager.DTO
{
    public class ParentNodeDTO
    {
        public int NodeId { get; set; }
        public string UserName { get; set; }
        public bool HasNoLeafNode { get; set; } = true; //default value
    }
}
