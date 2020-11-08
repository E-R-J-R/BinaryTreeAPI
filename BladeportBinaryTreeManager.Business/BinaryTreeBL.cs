using BladeportBinaryTreeManager.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BladeportBinaryTreeManager.DTO;
using System.Collections.Generic;
using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.Database.Entities;
using BladeportBinaryTreeManager.DTO.Enums;

namespace BladeportBinaryTreeManager.Business
{
    public class BinaryTreeBL : IBinaryTreeBL
    {
        private readonly ITreeManagerContext _ctx;
        private readonly IHierarchyBL _hierarchyBL;

        public BinaryTreeBL(ITreeManagerContext ctx, IHierarchyBL hierarchyBL)
        {
            _ctx = ctx;
            _hierarchyBL = hierarchyBL;
        }

        public List<TreeStructureDTO> GetBinaryTreeData()
        {
            return _ctx.BINARYTREE.AsNoTracking()
                                        .Select(x => new TreeStructureDTO
                                        {
                                            USERID = x.USERID,
                                            LFT = x.LFT,
                                            RGT = x.RGT
                                        }).ToList();
        }

        public ParentNodeDTO GetLogicalParentNode(List<SubTreeDTO> subTreeList)
        {
            var logicalParent = new ParentNodeDTO();
            var maxDepth = subTreeList.Max(x => x.Depth);
            int currentDepth = maxDepth;
            int currentNode = 0;

            //Fetch Leaf Nodes from Database
            var leafNodes = _hierarchyBL.GetLeafNodes(HierarchyType.BinaryTree).OrderBy(x => x.RGT).ToList();

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
                    
                foreach(var node in curDepthNodes)
                {
                    var childNodeCount = subTreeList.Where(x => x.PARENTID == node.UserID).Count();

                    if (childNodeCount < (int)ChildNodeLimit.BinaryTree)
                    {
                        logicalParent.NodeId = node.UserID;
                        break;
                    }
                }
            }
            else
            {
                //Traverse Tree in progressive top to bottom - left to right manner
                for (int i = 0; i < maxDepth; i++)
                {
                    //Retrieve Nodes from current depth ordered by left
                    var prevDepthNodes = subTreeList.Where(x => x.Depth == i).OrderBy(x => x.LFT);

                    foreach (var node in prevDepthNodes)
                    {
                        var childNodeCount = subTreeList.Where(x => x.PARENTID == node.UserID).Count();

                        if (childNodeCount < (int)ChildNodeLimit.BinaryTree)
                        {
                            logicalParent.NodeId = node.UserID;
                            break;
                        }
                    }
                }
            }

            return logicalParent;

        }

        public void InsertMasterParentNode(NodeDTO node)
        {
            var binaryTreeItem = new BINARYTREE();
            binaryTreeItem.USERID = node.NodeId;
            binaryTreeItem.LFT = 1;
            binaryTreeItem.RGT = 2;
            binaryTreeItem.PARENTID = node.ParentId;
            binaryTreeItem.SPONSORID = node.SponsorId;
            binaryTreeItem.ISMASTERPARENT = true;

            _ctx.BINARYTREE.Add(binaryTreeItem);
            _ctx.SaveChanges();
        }

        public void InsertNode(NodeDTO node, ParentNodeDTO parentNode)
        {
            if (parentNode.HasNoLeafNode)
                _ctx.InsertLeafNode(node.NodeId, parentNode.NodeId, node.SponsorId, HierarchyType.BinaryTree.ToString());
            else
                _ctx.InsertNode(node.NodeId, parentNode.NodeId, node.SponsorId, HierarchyType.BinaryTree.ToString());
        }

        public TreeSchemaDTO GetFullTreeSchema()
        {
            var schema = _ctx.GetFullTreeSchema(HierarchyType.BinaryTree.ToString());

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
