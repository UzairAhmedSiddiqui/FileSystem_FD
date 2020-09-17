namespace FileSystem_Finance_Department_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.FileDataDetails", "FileDataId");
            AddForeignKey("dbo.FileDataDetails", "FileDataId", "dbo.FileDatas", "FileDataId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileDataDetails", "FileDataId", "dbo.FileDatas");
            DropIndex("dbo.FileDataDetails", new[] { "FileDataId" });
        }
    }
}
