using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.Database;
using BladeportBinaryTreeManager.DTO;
using BladeportBinaryTreeManager.DTO.Enums;
using System.Collections.Generic;

namespace BladeportBinaryTreeManager.Business
{
    public class HierarchyBL : IHierarchyBL
    {
        private readonly ITreeManagerContext _ctx;

        public HierarchyBL(ITreeManagerContext ctx)
        {
            _ctx = ctx;
        }

        public List<SubTreeDTO> GetSubTreeDepth(int parentId, HierarchyType hierarchyType, int childLimit = 0, int levelLimit = 0)
        {
            if (hierarchyType == HierarchyType.ForcedMatrix)
            {
                var tableName = $"{hierarchyType}{childLimit}x{levelLimit}";
                return _ctx.GetSubTreeDepth(parentId, tableName);
            }

            return _ctx.GetSubTreeDepth(parentId, hierarchyType.ToString());
        }

        public List<TreeStructureDTO> GetLeafNodes(HierarchyType hierarchyType, int childLimit = 0, int levelLimit = 0)
        {
            if (hierarchyType == HierarchyType.ForcedMatrix)
            {
                var tableName = $"{hierarchyType}{childLimit}x{levelLimit}";
                return _ctx.GetLeafNodes(tableName);
            }

            return _ctx.GetLeafNodes(hierarchyType.ToString());
        }

        public List<ParentNodeDTO> GetMasterParentPath(int userId, HierarchyType hierarchyType, int childLimit = 0, int levelLimit = 0)
        {
            if (hierarchyType == HierarchyType.ForcedMatrix)
            {
                var tableName = $"{hierarchyType}{childLimit}x{levelLimit}";
                return _ctx.GetMasterParentPath(userId, tableName);
            }

            return _ctx.GetMasterParentPath(userId, hierarchyType.ToString());
        }
    }
}
