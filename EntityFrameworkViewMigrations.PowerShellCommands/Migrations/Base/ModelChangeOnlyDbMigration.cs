﻿namespace EntityFrameworkViewMigrations.PowerShellCommands.Migrations.Base
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Use ModelChangeOnlyDbMigration class to mark a dummy
    /// migration that only changes the model but not the database.
    /// When using ModelChangeOnlyDbMigration, write a comment
    /// above the derived class and state why you use an empty migration.
    /// </summary>
    public class ModelChangeOnlyDbMigration : DbMigration
    {
        public override void Up()
        {
            // this is a dummy migration
            // no need for an implementation of up and down
        }
    }
}
