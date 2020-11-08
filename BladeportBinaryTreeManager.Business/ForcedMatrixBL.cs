using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.Database;
using BladeportBinaryTreeManager.DTO;
using BladeportBinaryTreeManager.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BladeportBinaryTreeManager.Business
{
    public class ForcedMatrixBL : IForcedMatrixBL
    {
        private readonly ITreeManagerContext _ctx;
        private readonly IHierarchyBL _hierarchyBL;

        public ForcedMatrixBL(ITreeManagerContext ctx, IHierarchyBL hierarchyBL)
        {
            _ctx = ctx;
            _hierarchyBL = hierarchyBL;
        }

        public ParentNodeDTO GetLogicalParentNode(List<SubTreeDTO> subTreeList, int childLimit, int levelLimit)
        {
            var logicalParent = new ParentNodeDTO();
            var maxDepth = subTreeList.Max(x => x.Depth);
            int currentDepth = maxDepth;
            int currentNode = 0;

            //Fetch Leaf Nodes from Database
            var leafNodes = _hierarchyBL.GetLeafNodes(HierarchyType.ForcedMatrix, childLimit, levelLimit).OrderBy(x => x.RGT).ToList();

            var leafNodeData = from s in subTreeList
                               join l in leafNodes
                               on s.UserID equals l.USERID
                               orderby s.Depth, l.RGT
                               select new
                               {
                                   UserId = s.UserID,
                                   Depth = s.Depth,
                                   LFT = l.LFT,
                                   RGT = l.RGT,
                                   ParentId = l.PARENTID
                               };

            currentDepth = leafNodeData.First().Depth;
            currentNode = leafNodeData.First().UserId;

            //Set current node default for parent node for now
            logicalParent.NodeId = currentNode;

            if (currentDepth < maxDepth)
            {
                //Retrieve Nodes from current depth ordered by left
                var curDepthNodes = subTreeList.Where(x => x.Depth == currentDepth).OrderBy(x => x.LFT);

                foreach (var node in curDepthNodes)
                {
                    var childNodeCount = subTreeList.Where(x => x.PARENTID == node.UserID).Count();

                    if (childNodeCount < childLimit)
                    {
                        logicalParent.NodeId = node.UserID;
                        break;
                    }
                }
            }
            else
            {
                //Traverse Tree in progressive top to bottom - left to right manner
                for (int i=0; i < maxDepth; i++)
                {
                    //Retrieve Nodes from upper depth ordered by left
                    var prevDepthNodes = subTreeList.Where(x => x.Depth == i).OrderBy(x => x.LFT);

                    foreach (var node in prevDepthNodes)
                    {
                        var childNodeCount = subTreeList.Where(x => x.PARENTID == node.UserID).Count();

                        if (childNodeCount < childLimit)
                        {
                            logicalParent.NodeId = node.UserID;
                            return logicalParent;
                        }
                    }
                }
           
                //If parent node was not replaced, another level will be added to the matrix
                //Check for level limit
                if (logicalParent.NodeId == currentNode)
                {
                    var fullMatrixDepth = _hierarchyBL.GetMasterParentPath(currentNode, HierarchyType.ForcedMatrix, childLimit, levelLimit).Count();
                  
                    //Get the true depth of node relative to the full tree
                    if (fullMatrixDepth >= levelLimit + 1)
                    {
                        throw new ArgumentOutOfRangeException("Max Forced Matrix level has been reached");
                    }
                }
            }

            return logicalParent;

        }

        public void InsertNode(ForcedMatrixNodeDTO node, ParentNodeDTO parentNode)
        {
            var tableName = $"{HierarchyType.ForcedMatrix}{node.ChildLimit}x{node.LevelLimit}";

            if (parentNode.HasNoLeafNode)
                _ctx.InsertLeafNode(node.NodeId, parentNode.NodeId, node.SponsorId, tableName);
            else
                _ctx.InsertNode(node.NodeId, parentNode.NodeId, node.SponsorId, tableName);
        }

        public void InsertMasterParentNode(ForcedMatrixNodeDTO node)
        {
            _ctx.InsertMatrixMasterParentNode(node);
        }

        public TreeSchemaDTO GetFullTreeSchema(int childLimit, int levelLimit)
        {
            var tableName = $"{HierarchyType.ForcedMatrix}{childLimit}x{levelLimit}";

            var schema = _ctx.GetFullTreeSchema(tableName);

            var root = schema.Select(x => new OrgChartDTO
            {
                Id = x.UserID.ToString(),
                Title = x.FullName,
                Name = x.UserID.ToString(),
                Children = new List<OrgChartDTO>()
            }).First();

            var maxDepth = schema.Max(x => x.Depth) + 1;

            foreach (var user in schema)
            {
                var children = schema.Where(x => x.PARENTID == user.UserID).Select(x => new OrgChartDTO
                {
                    Id = x.UserID.ToString(),
                    Name = x.UserID.ToString(),
                    Title = x.FullName,
                    Children = new List<OrgChartDTO>()
                }).ToList();

                if (root.Children.Any())
                {
                    var node = root.Traverse().Single(x => x.Id == user.UserID.ToString());
                    node.Children.AddRange(children);
                }
                else
                {
                    root.Children.AddRange(children);
                }
            }

            var treeSchema = new TreeSchemaDTO { schema = root, MaxDepth = maxDepth };

            return treeSchema;
        }


    }
}
