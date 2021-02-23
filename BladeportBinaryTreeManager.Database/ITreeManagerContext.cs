using BladeportBinaryTreeManager.Database.Entities;
using BladeportBinaryTreeManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BladeportBinaryTreeManager.Database
{
    public interface ITreeManagerContext : IDisposable
    {
        DbSet<USERS> USERS { get; set; }
        DbSet<BINARYTREE> BINARYTREE { get; set; }
        DbSet<TREE> TREE { get; set; }
        DbSet<SUBTREE> SUBTREE { get; set; }
        DbSet<FORCEDMATRIX> FORCEDMATRIX { get; set; }
        DbSet<PARENTNODE> PARENTNODE { get; set; }
        DbSet<LEAFNODE> LEAFNODE { get; set; }

        int SaveChanges();

        //Raw SQL
        void InsertMatrixMasterParentNode(ForcedMatrixNodeDTO node);
        void AddUser(UserDTO user);
        void DeleteUser(UserDTO user);
        void EditUser(UserDTO user);

        //Stored Procedures
        List<SubTreeDTO> GetSubTreeDepth(int parentId, string tableName);
        int InsertNode(int userId, int parentId, int sponsorId, string tableName);
        int InsertLeafNode(int userId, int parentId, int sponsorId, string tableName);
        List<TreeDTO> GetFullTreeSchema(string tableName);
        List<TreeStructureDTO> GetLeafNodes(string tableName);
        List<ParentNodeDTO> GetMasterParentPath(int userId, string tableName);


    }
}
