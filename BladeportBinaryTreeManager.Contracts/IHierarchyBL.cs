using BladeportBinaryTreeManager.DTO;
using BladeportBinaryTreeManager.DTO.Enums;
using System.Collections.Generic;

namespace BladeportBinaryTreeManager.Contracts
{
    public interface IHierarchyBL
    {
        List<SubTreeDTO> GetSubTreeDepth(int parentId, HierarchyType hierarchyType, int childLimit = 0, int levelLimit = 0);
        List<TreeStructureDTO> GetLeafNodes(HierarchyType hierarchyType, int childLimit = 0, int levelLimit = 0);
        List<ParentNodeDTO> GetMasterParentPath(int userId, HierarchyType hierarchyType, int childLimit = 0, int levelLimit = 0);
    }
}
