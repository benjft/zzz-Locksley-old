using Microsoft.EntityFrameworkCore;

namespace Locksley.App.Data.Models.Interfaces; 

public interface IHasRelationship {
    static abstract void ConfigureRelationships(ModelBuilder modelBuilder);
}