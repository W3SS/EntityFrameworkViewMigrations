﻿namespace EntityFrameworkViewMigrations.PowerShellCommands.Configuration
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;

    /// <summary>
    /// Base migration class for migration scaffolding
    /// that should replace the original EF <see cref="DbMigration"/> class
    /// </summary>
    public abstract class BaseDbMigration : DbMigration
    {
        private const string DownFileName = "Down";
        private const string UpFileName = "Up";

        /// <summary>
        /// Applies sql from sql migration file {sqlFileName}.sql located in
        /// DatabaseProject\Migrations\{Migration.Id}\{sqlFileName}.sql
        /// </summary>
        /// <param name="sqlFileName"></param>
        /// <param name="suppressTransaction">
        /// A value indicating if the SQL should be executed outside of the transaction being
        /// used for the migration process. If no value is supplied the SQL will be executed
        /// within the transaction.
        /// </param>
        public void DatabaseSqlFile(string sqlFileName, string folder = "", bool suppressTransaction = false)
        {
            this.Sql(SqlDataParser.WrapSqlFileWithExec(this.GetMigrationScript(sqlFileName, folder)), suppressTransaction);
        }

        /// <summary>
        /// Applies sql from sql migration file Up.sql located in
        /// DatabaseProject\Migrations\{Migration.Id}\Up.sql
        /// </summary>
        /// <param name="suppressTransaction">
        /// A value indicating if the SQL should be executed outside of the transaction being
        /// used for the migration process. If no value is supplied the SQL will be executed
        /// within the transaction.
        /// </param>
        protected void DatabaseSqlFileUp(bool suppressTransaction = false)
        {
            this.DatabaseSqlFile(UpFileName, suppressTransaction: suppressTransaction);
        }

        /// <summary>
        /// Applies sql from sql migration file Down.sql located in
        /// DatabaseProject\Migrations\{Migration.Id}\Down.sql
        /// </summary>
        /// <param name="suppressTransaction">
        /// A value indicating if the SQL should be executed outside of the transaction being
        /// used for the migration process. If no value is supplied the SQL will be executed
        /// within the transaction.
        /// </param>
        protected void DatabaseSqlFileDown(bool suppressTransaction = false)
        {
            this.DatabaseSqlFile(DownFileName, suppressTransaction: suppressTransaction);
        }

        protected void Seed()
        {
            foreach (string sql in InitialDataParser.Parse())
            {
                this.Sql(SqlDataParser.WrapSqlFileWithExec(sql));
            }
        }

        /// <summary>
        /// Gets content of a migration sql file.
        /// We assume that the migration was scaffolded with
        /// <see cref="IMigrationMetadata"/> interface and use its
        /// Id property to target file in
        /// DatabaseProject\Migrations\{Id}\<paramref name="sqlFileName"/>.sql
        /// </summary>
        /// <param name="sqlFileName"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        private string GetMigrationScript(string sqlFileName, string folder = "")
        {
            return DbMigrationPath.CombineAndReadAll(
                DbMigrationPath.MigrationsFolderPath,
                ((IMigrationMetadata)this).Id,
                folder, // I can do this because Path.Combine("Folder1", "", "File"); returns Folder1/File
                $"{sqlFileName}.sql");
        }
    }
}