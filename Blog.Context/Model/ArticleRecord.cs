using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Context.Model
 {
     public class ArticleRecord
     {
         public int Id { get; set; }
         public string Title { get; set; }
         public DateTime Date { get; set; }
         public ArticleContentRecord Content { get; set; }
     }
     
     public class ArticleRecordConfiguration : IEntityTypeConfiguration<ArticleRecord>
     {
         public void Configure(EntityTypeBuilder<ArticleRecord> builder)
         {
             builder
                 .HasOne(e => e.Content).WithOne(e => e.Article)
                 .HasForeignKey<ArticleContentRecord>(e => e.Id);
         }
     }
 }