namespace FileSystem_Finance_Department_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileDataDetails",
                c => new
                    {
                        FileDataDetailId = c.Int(nullable: false, identity: true),
                        FileDataId = c.Int(nullable: false),
                        FilePath = c.String(),
                        Status = c.String(),
                        UploadTime = c.String(),
                    })
                .PrimaryKey(t => t.FileDataDetailId);
            
            CreateTable(
                "dbo.FileDatas",
                c => new
                    {
                        FileDataId = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        Filename = c.String(),
                        Filenumber = c.String(),
                        Subject = c.String(),
                        Type = c.String(),
                        Givennumber = c.String(),
                        Pages = c.Int(nullable: false),
                        Addressee = c.String(),
                        Sectionoforigin = c.String(),
                        Receivedby = c.String(),
                        Status = c.String(),
                        Pdfdirectory = c.String(),
                    })
                .PrimaryKey(t => t.FileDataId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        UserType = c.String(),
                        CurrentSO = c.String(),
                        PreviousSO = c.String(),
                        Dateofjoining = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logins");
            DropTable("dbo.FileDatas");
            DropTable("dbo.FileDataDetails");
        }
    }
}
