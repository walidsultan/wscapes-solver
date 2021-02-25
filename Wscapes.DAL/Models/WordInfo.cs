using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wscapes.DAL.Models
{
    public class WordInfo
    {
        [Key]
        public Guid Id { get; set; }
        public string Word { get; set; }
        public int Count { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public static void SetEntityConfiguration(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<WordInfo> wordsInfoConfig = modelBuilder.Entity<WordInfo>();

            // Entity to Table mapping
            wordsInfoConfig.ToTable("WordsInfo");

            // Entity property to column name mapping
            #region - Column Mappings -
            wordsInfoConfig
                    .Property(t => t.Id)
                    .HasColumnName("Id")
                    .HasColumnType("uniqueidentifier");

            wordsInfoConfig
                  .Property(t => t.Word)
                  .HasColumnName("Word")
                  .HasColumnType("nvarchar");


            wordsInfoConfig
                    .Property(t => t.Count)
                    .HasColumnName("Count")
                    .HasColumnType("int");

            wordsInfoConfig
                    .Property(t => t.CreatedDate)
                    .HasColumnName("CreatedDate")
                    .HasColumnType("datetime");

            wordsInfoConfig
                    .Property(t => t.UpdatedDate)
                    .HasColumnName("UpdatedDate")
                    .HasColumnType("datetime");
            #endregion

            // Set Primary key
            wordsInfoConfig.HasKey(tz => new { tz.Id });
        }

    }
}
