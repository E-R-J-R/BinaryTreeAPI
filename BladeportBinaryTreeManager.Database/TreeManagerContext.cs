using BladeportBinaryTreeManager.Database.Entities;
using BladeportBinaryTreeManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BladeportBinaryTreeManager.Database
{
    public class TreeManagerContext : DbContext, ITreeManagerContext
    {
        public TreeManagerContext(DbContextOptions options) : base(options)
        {
           
        }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<BINARYTREE> BINARYTREE { get; set; }
        public virtual DbSet<TREE> TREE { get; set; }
        public virtual DbSet<SUBTREE> SUBTREE { get; set; }
        public virtual DbSet<FORCEDMATRIX> FORCEDMATRIX { get; set; }
        public virtual DbSet<PARENTNODE> PARENTNODE { get; set; }
        public virtual DbSet<LEAFNODE> LEAFNODE { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite key.
            modelBuilder.Entity<BINARYTREE>()
                .HasKey(lc => new { lc.USERID });

            modelBuilder.Entity<USERS>()
              .HasKey(lc => new { lc.USERID });

            modelBuilder.Entity<TREE>()
              .HasKey(lc => new { lc.USERID });

            modelBuilder.Entity<SUBTREE>()
              .HasKey(lc => new { lc.USERID });

            modelBuilder.Entity<FORCEDMATRIX>()
              .HasKey(lc => new { lc.USERID });

            modelBuilder.Entity<LEAFNODE>()
              .HasNoKey();

            modelBuilder.Entity<PARENTNODE>()
              .HasKey(p => new { p.NodeId });
        }

        public void InsertMatrixMasterParentNode(ForcedMatrixNodeDTO node)
        {
            try
            {
                Database.ExecuteSqlRaw($"INSERT INTO [dbo].[ForcedMatrix{node.ChildLimit}x{node.LevelLimit}] (USERID, LFT, RGT, PARENTID, SPONSORID, ISMASTERPARENT) VALUES ({node.NodeId}, 1, 2, 0, {node.SponsorId}, 'TRUE')");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubTreeDTO> GetSubTreeDepth(int parentId, string tableName)
        {
            var parentIdParam = new SqlParameter
            {
                ParameterName = "@ParentId",
                Value = parentId
            };

            var tableNameParam = new SqlParameter
            {
                ParameterName = "@TableName",
                Value = tableName
            };

            var result = SUBTREE.FromSqlRaw("EXEC sp_GetSubTreeDepth @ParentId={0},@TableName={1}", parentIdParam.Value, tableNameParam.Value).Select(x => new SubTreeDTO
            {
                UserID = x.USERID,
                Depth = x.DEPTH,
                LFT = x.LFT,
                RGT = x.RGT,
                PARENTID = x.PARENTID
            })
            .ToList();

            return result;

        }

        public int InsertNode(int userId, int parentId, int sponsorId, string tableName)
        {
            var userIdParam = new SqlParameter
            {
                ParameterName = "@UserId",
                Value = userId
            };

            var parentIdParam = new SqlParameter
            {
                ParameterName = "@ParentId",
                Value = parentId
            };

            var sponsorIdParam = new SqlParameter
            {
                ParameterName = "@SponsorId",
                Value = sponsorId
            };

            var tableNameParam = new SqlParameter
            {
                ParameterName = "@TableName",
                Value = tableName
            };

            try
            {
                Database.EnsureCreated();
                Database.OpenConnection();
                return Database.ExecuteSqlRaw("EXEC sp_InsertNode @UserId={0},@ParentId={1},@SponsorId={2}@TableName={3}", userIdParam.Value, parentIdParam.Value, sponsorIdParam.Value, tableNameParam.Value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                Database.CloseConnection();
            }
        }

        public int InsertLeafNode(int userId, int parentId, int sponsorId, string tableName)
        {
            var userIdParam = new SqlParameter
            {
                ParameterName = "@UserId",
                Value = userId
            };

            var parentIdParam = new SqlParameter
            {
                ParameterName = "@ParentId",
                Value = parentId
            };

            var sponsorIdParam = new SqlParameter
            {
                ParameterName = "@SponsorId",
                Value = sponsorId
            };

            var tableNameParam = new SqlParameter
            {
                ParameterName = "@TableName",
                Value = tableName
            };

            try
            {
                Database.EnsureCreated();
                Database.OpenConnection();
                return Database.ExecuteSqlRaw("EXEC sp_InsertLeafNode @UserId={0},@ParentId={1},@SponsorId={2},@TableName={3}", userIdParam.Value, parentIdParam.Value, sponsorIdParam.Value, tableNameParam.Value);
               
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                Database.CloseConnection();
            }
        }

        public List<TreeDTO> GetFullTreeSchema(string tableName)
        {
            var tableNameParam = new SqlParameter
            {
                ParameterName = "@TableName",
                Value = tableName
            };

            try
            {
                Database.EnsureCreated();
                Database.OpenConnection();

                return TREE.FromSqlRaw("EXEC sp_GetFullTreeSchema @TableName={0}", tableNameParam.Value).AsEnumerable().Select(x => new TreeDTO { 
                    UserID = x.USERID,
                    UserName = x.USERNAME,
                    FullName = x.FULLNAME,
                    PARENTID = x.PARENTID,
                    SponsorId = x.SPONSORID == null ? 0 : (int)x.SPONSORID,
                    Depth = x.DEPTH
                }).ToList();

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                Database.CloseConnection();
            }

        }

        public List<TreeStructureDTO> GetLeafNodes(string tableName)
        {
            var tableNameParam = new SqlParameter
            {
                ParameterName = "@TableName",
                Value = tableName
            };

            try
            {
                return LEAFNODE.FromSqlRaw("EXEC sp_GetLeafNodes @TableName={0}", tableNameParam.Value)
                    .AsEnumerable()
                    .Select(x => new TreeStructureDTO { 
                        USERID = x.NODEID,
                        LFT = x.LFT,
                        RGT = x.RGT,
                        PARENTID = x.PARENTID
                    }).ToList();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public List<ParentNodeDTO> GetMasterParentPath(int userId, string tableName)
        {
            var userIdParam = new SqlParameter
            {
                ParameterName = "@ParentId",
                Value = userId
            };

            var tableNameParam = new SqlParameter
            {
                ParameterName = "@TableName",
                Value = tableName
            };

            var result = PARENTNODE.FromSqlRaw("EXEC sp_GetMasterParentPath @UserId={0},@TableName={1}", userIdParam.Value, tableNameParam.Value)
                                   .AsEnumerable()
                                   .Select(x => new ParentNodeDTO
                                   {
                                       NodeId = x.NodeId,
                                       UserName = x.UserName,
                                       HasNoLeafNode = false
                                   })
                                   .ToList();

            return result;
        }




    }
}
