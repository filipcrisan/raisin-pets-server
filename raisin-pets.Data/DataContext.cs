namespace raisin_pets.Data;

public class DataContext : DbContext, IDesignTimeDbContextFactory<DataContext>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DataContext() { }

    #region DbSets

    public DbSet<User> Users { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Tutorial> Tutorials { get; set; }
    public DbSet<Checkpoint> Checkpoints { get; set; }
    public DbSet<Exercise> Exercises { get; set; }

    #endregion
    
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        
        optionsBuilder.UseNpgsql(args.ElementAt(0));
        
        return new DataContext(optionsBuilder.Options);
    }
}